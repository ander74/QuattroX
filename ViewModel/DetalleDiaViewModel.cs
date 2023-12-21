#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Entities;
using QuattroX.Data.Helpers;
using QuattroX.Data.Messages;
using QuattroX.Data.Model;
using QuattroX.Data.Repositories;
using QuattroX.Services;
using System.ComponentModel;

namespace QuattroX.ViewModel;


[QueryProperty(nameof(Dia), "Dia")]
public partial class DetalleDiaViewModel : BaseViewModel {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================

    private readonly DbRepository dbRepository;
    private readonly CalculosService calculosService;

    public DetalleDiaViewModel(DbRepository dbRepository, CalculosService calculosService) {
        this.dbRepository = dbRepository;
        this.calculosService = calculosService;
    }

    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    DiaModel dia;
    partial void OnDiaChanged(DiaModel oldValue, DiaModel newValue) {
        Title = $"{Dia.Fecha.Day:00} - {Textos.MesesAbr[Dia.Fecha.Month]} - {Dia.Fecha.Year}";
        Dia.PropertyChanged += Dia_PropertyChanged;
    }


    [ObservableProperty]
    string turnoSeleccionado;
    partial void OnTurnoSeleccionadoChanged(string value) {
        if (value == "Mañana") Dia.Turno = 1;
        if (value == "Tarde") Dia.Turno = 2;
    }

    [ObservableProperty]
    List<IncidenciaEntity> incidencias;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(EsHuelga))]
    [NotifyPropertyChangedFor(nameof(EsHuelgaParcial))]
    IncidenciaEntity incidenciaSeleccionada;
    partial void OnIncidenciaSeleccionadaChanged(IncidenciaEntity oldValue, IncidenciaEntity newValue) {
        if (IncidenciaSeleccionada.Codigo > 0) {
            Dia.IncidenciaId = IncidenciaSeleccionada.Id;
            Dia.Incidencia = IncidenciaSeleccionada.ToModel();
            Calcular();
        } else {
            //TODO: Si la incidencia seleccionada es cero, copiar el día anterior.
        }
    }


    public bool EsHuelga => Dia?.Incidencia?.Codigo == 15;


    public bool EsHuelgaParcial => Dia?.Incidencia?.Codigo == 15 && Dia?.HuelgaParcial == true;


    public string[] Turnos => ["Mañana", "Tarde"];


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos públicos
    // ====================================================================================================


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos privados
    // ====================================================================================================


    /// <summary>
    /// Método que se lanza al cambiar una propiedad del Día.
    /// </summary>
    private void Dia_PropertyChanged(object sender, PropertyChangedEventArgs e) {
        if (e.PropertyName == nameof(DiaModel.HuelgaParcial)) OnPropertyChanged(nameof(EsHuelgaParcial));
        if (e.PropertyName == nameof(DiaModel.Inicio) ||
            e.PropertyName == nameof(DiaModel.Final) ||
            e.PropertyName == nameof(DiaModel.Turno)) {
            Calcular();
        }
    }



    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Comandos
    // ====================================================================================================


    [RelayCommand]
    async Task LoadAsync() {
        try {
            IsBusy = true;
            Incidencias = await dbRepository.GetIncidenciasAsync();
            if (Incidencias is null) Incidencias = new();
#pragma warning disable MVVMTK0034
            if (Dia.IncidenciaId > 0) {
                // Usamos el campo en lugar de la propiedad para no hacer que
                // se dispare el método parcial OnIncidenciaChanged inicialmente.
                incidenciaSeleccionada = Incidencias.Where(i => i.Id == Dia.IncidenciaId).FirstOrDefault();
            } else {
                IncidenciaSeleccionada = Incidencias.Where(i => i.Codigo == 1).FirstOrDefault();
            }
            OnPropertyChanged(nameof(IncidenciaSeleccionada));
#pragma warning restore MVVMTK0034
            if (Dia.Turno == 1) TurnoSeleccionado = Turnos[0];
            if (Dia.Turno == 2) TurnoSeleccionado = Turnos[1];
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task CloseAsync() {
        await dbRepository.SaveDiaAsync(Dia.ToEntity());
        Messenger.Send(new CalcularResumenRequest());
    }


    [RelayCommand]
    void CambiaDietaDesayuno() {
        Dia.Desayuno = !Dia.Desayuno;
    }


    [RelayCommand]
    void CambiaDietaComida() {
        Dia.Comida = !Dia.Comida;
    }


    [RelayCommand]
    void CambiaDietaCena() {
        Dia.Cena = !Dia.Cena;
    }


    [RelayCommand]
    void Calcular() {
        calculosService.CalcularHoras(Dia);
    }


    [RelayCommand]
    async Task EditarHoraAsync(string param) {
        var valor = param switch {
            "Trabajadas" => Dia.Trabajadas,
            "Acumuladas" => Dia.Acumuladas,
            "Nocturnas" => Dia.Nocturnas,
            _ => 0m,
        };
        var mensaje = $"Introduce horas {param}";
        var resultado = await Shell.Current.DisplayPromptAsync(param, mensaje, "Aceptar", "Cancelar", null, -1, Keyboard.Numeric, valor.ToTexto());
        if (string.IsNullOrWhiteSpace(resultado)) return;
        switch (param) {
            case "Trabajadas":
                Dia.Trabajadas = resultado.ToDecimal();
                break;
            case "Acumuladas":
                Dia.Acumuladas = resultado.ToDecimal();
                break;
            case "Nocturnas":
                Dia.Nocturnas = resultado.ToDecimal();
                break;
        }
    }


    [RelayCommand]
    async Task BuscarRelevoAsync() {
        try {
            IsBusy = true;
            var relevo = await dbRepository.GetTrabajadorByMatriculaAsync(Dia.Matricula);
            if (relevo is null) return;
            Dia.RelevoId = relevo.Id;
            Dia.Apellidos = relevo.Apellidos;
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task CrearRelevoAsync() {
        try {
            IsBusy = true;
            if (Dia.Matricula <= 0) return;
            if (await dbRepository.ExisteTrabajadorByMatriculaAsync(Dia.Matricula)) return;
            var trabajador = new TrabajadorEntity {
                Matricula = Dia.Matricula,
                Apellidos = Dia.Apellidos,
            };
            await dbRepository.SaveTrabajadorAsync(trabajador);
            Dia.RelevoId = trabajador.Id;
            await dbRepository.SaveDiaAsync(Dia.ToEntity());
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }

    #endregion
    // ====================================================================================================


}
