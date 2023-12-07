#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

using CommunityToolkit.Maui.Alerts;
using Nelya.Core.Helpers;
using QuattroX.Data.Entities;
using QuattroX.Data.Enums;
using QuattroX.Data.Helpers;
using QuattroX.Data.Model;
using QuattroX.Data.Repositories;
using System.Collections.ObjectModel;

namespace QuattroX.ViewModel;


public partial class CalendarioViewModel : BaseViewModel {



    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================

    private readonly DbRepository dbRepository;

    private DiaEntity diaCopiado;

    public CalendarioViewModel(DbRepository dbRepository) {
        this.dbRepository = dbRepository;
        Title = "Calendario";

        DiasSeleccionados.CollectionChanged += (sender, e) => {
            var num = DiasSeleccionados.Count;
            IsSelectionMode = num > 0;
            if (num > 0) {
                Title = num == 1 ? "1 día sel." : $"{num} días sel.";
            } else {
                Title = "Calendario";
            }
            OnPropertyChanged(nameof(OnlyOneItemSelected));
        };
    }

    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextoMesActual))]
    DateTime fechaActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);


    [ObservableProperty]
    ObservableCollection<DiaModel> dias;


    [ObservableProperty]
    ResumenModel resumen;


    [ObservableProperty]
    List<IncidenciaModel> incidencias;


    // Propiedades que gestionan la selección

    [ObservableProperty]
    ObservableCollection<object> diasSeleccionados = new();


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotSelectionMode))]
    [NotifyPropertyChangedFor(nameof(OnlyOneItemSelected))]
    bool isSelectionMode;


    public bool IsNotSelectionMode => !IsSelectionMode;


    public string TextoMesActual => $"{Textos.Meses[FechaActual.Month]} - {FechaActual.Year}";


    public bool OnlyOneItemSelected => IsSelectionMode && (DiasSeleccionados?.Count == 1) == true;



    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos privados
    // ====================================================================================================


    private async Task CrearMesAsync(DateTime fecha) {
        var dias = new List<DiaEntity>();
        for (int dia = 1; dia <= DateTime.DaysInMonth(fecha.Year, fecha.Month); dia++) {
            dias.Add(new DiaEntity {
                Fecha = new DateTime(fecha.Year, fecha.Month, dia),
            });
        }
        await dbRepository.SaveDiasAsync(dias);
    }


    private async Task CargarMes() {
        var dias = await dbRepository.GetDiasByMesAsync(FechaActual);
        if (!dias.Any()) {
            await CrearMesAsync(FechaActual);
            dias = await dbRepository.GetDiasByMesAsync(FechaActual);
        }
        Dias = dias.Select(d => d.ToModel()).ToObservableCollection();
        var resumen = await dbRepository.GetResumenByFechaAsync(FechaActual);
        if (resumen == null) resumen = new();
        Resumen = resumen.ToModel();
    }


    private async Task CargarIncidencias() {
        if (Incidencias is null || Incidencias.Count == 0) {
            var incid = await dbRepository.GetIncidenciasAsync();
            if (incid.Any()) {
                Incidencias = incid.Select(i => i.ToModel()).ToList();
            }
        }
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Comandos
    // ====================================================================================================


    [RelayCommand]
    async Task LoadAsync() {
        await CargarMes();
        await CargarIncidencias();
    }


    [RelayCommand]
    void ActivarFranqueoFestivo(DiaModel dia) {
        if (!dia.EsFranqueo && !dia.EsFestivo) {
            dia.EsFranqueo = true;
        } else {
            if (dia.EsFranqueo) {
                dia.EsFranqueo = false;
                dia.EsFestivo = true;
            } else {
                dia.EsFranqueo = false;
                dia.EsFestivo = false;
            }
        }
        if (dia.EsFranqueo && dia.Incidencia == null) {
            var franqueo = Incidencias.FirstOrDefault(i => i.Codigo == 2);
            dia.Incidencia = franqueo;
            dia.IncidenciaId = franqueo.Id;
        }
        if (dia.EsFestivo && (dia.Incidencia == null || dia.Incidencia.Codigo == 2)) {
            var festivo = Incidencias.FirstOrDefault(i => i.Codigo == 9);
            dia.Incidencia = festivo;
            dia.IncidenciaId = festivo.Id;
        }
        if ((!dia.EsFranqueo && !dia.EsFestivo) && (dia.Incidencia.Codigo == 2 || dia.Incidencia.Codigo == 9)) {
            dia.Incidencia = null;
            dia.IncidenciaId = 0;
        }
        //PRUEBA
        dia.ServicioPrincipal = new ServicioDiaModel { Turno = Random.Shared.Next(0, 3) };
    }


    [RelayCommand]
    void ActivarSeleccion(DiaModel dia) {
        if (!IsSelectionMode) {
            IsSelectionMode = true;
        }
    }


    [RelayCommand]
    void DesactivarSeleccion() {
        DiasSeleccionados.Clear();
    }


    [RelayCommand]
    async Task CopiarDiaAsync() {
        if (DiasSeleccionados is null || DiasSeleccionados.Count == 0) return;
        var dia = (DiaModel)DiasSeleccionados.FirstOrDefault();
        diaCopiado = dia.ToEntity();
        await Shell.Current.DisplaySnackbar("Se ha copiado el día");
    }


    [RelayCommand]
    void PegarDia() {
        if (DiasSeleccionados is null || DiasSeleccionados.Count == 0) return;
        if (diaCopiado is null) return;
        foreach (DiaModel dia in DiasSeleccionados) {
            var id = dia.Id;
            var fecha = dia.Fecha;
            dia.FromEntity(diaCopiado);
            dia.Id = id;
            dia.Fecha = fecha;
        }
    }


    [RelayCommand]
    void AlternarFranqueoFestivo() {
        if (DiasSeleccionados is null || DiasSeleccionados.Count == 0) return;
        foreach (DiaModel dia in DiasSeleccionados) {
            ActivarFranqueoFestivo(dia);
        }
    }


    [RelayCommand]
    async Task CrearRegulacionAsync() {
        if (DiasSeleccionados is null || DiasSeleccionados.Count == 0) return;
        var dia = (DiaModel)DiasSeleccionados.FirstOrDefault();
        var horas = await Shell.Current.DisplayPromptAsync(
            "Nueva regulación", "Introduce las horas a regular.", "Siguiente", "Cancelar");
        if (horas is null) return;
        var horasDecimal = horas.ToDecimal();
        if (horasDecimal == 0) {
            await Shell.Current.DisplayAlert("Error", "No se puede crear una regulación con cero horas.", "Cerrar");
            return;
        }
        var motivo = await Shell.Current.DisplayPromptAsync(
           "Nueva regulación", "Introduce el motivo de la regulación.", "Crear", "Cancelar");
        if (motivo is null) return;
        var regulacion = new RegulacionModel {
            DiaId = dia.Id,
            Horas = horasDecimal,
            Motivo = motivo,
            Tipo = TipoRegulacion.Manual,
        };
        await dbRepository.SaveRegulacionAsync(regulacion.ToEntity());
        await Shell.Current.DisplaySnackbar("Regulación creada correctamente.");
    }


    [RelayCommand]
    void VaciarDia() {
        if (DiasSeleccionados is null || DiasSeleccionados.Count == 0) return;
        foreach (DiaModel dia in DiasSeleccionados) {
            dia.Vaciar();
        }
    }






    #endregion
    // ====================================================================================================


}
