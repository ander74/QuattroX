#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

using QuattroX.Data.Entities;
using QuattroX.Data.Helpers;
using QuattroX.Data.Model;
using QuattroX.Data.Repositories;

namespace QuattroX.ViewModel;


[QueryProperty(nameof(Dia), "Dia")]
public partial class DetalleDiaViewModel : BaseViewModel {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================

    private readonly DbRepository dbRepository;


    public DetalleDiaViewModel(DbRepository dbRepository) {
        this.dbRepository = dbRepository;
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
        Dia.PropertyChanged += (s, e) => {
            if (e.PropertyName == nameof(DiaModel.HuelgaParcial)) OnPropertyChanged(nameof(EsHuelgaParcial));
        };
    }


    [ObservableProperty]
    List<IncidenciaEntity> incidencias;


    [ObservableProperty]
    IncidenciaEntity incidenciaSeleccionada;

    partial void OnIncidenciaSeleccionadaChanged(IncidenciaEntity oldValue, IncidenciaEntity newValue) {
        //TODO: Si la incidencia seleccionada es cero, copiar el día anterior.
        Dia.IncidenciaId = IncidenciaSeleccionada.Id;
        Dia.Incidencia = IncidenciaSeleccionada.ToModel();
        OnPropertyChanged(nameof(EsHuelga));
        OnPropertyChanged(nameof(EsHuelgaParcial));
    }


    public bool EsHuelga => Dia?.Incidencia?.Codigo == 15;


    public bool EsHuelgaParcial => Dia?.Incidencia?.Codigo == 15 && Dia?.HuelgaParcial == true;


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
            if (Dia.IncidenciaId > 0) {
                IncidenciaSeleccionada = Incidencias.Where(i => i.Id == Dia.IncidenciaId).FirstOrDefault();
            }
            Dia.ServicioPrincipal.Inicio = TimeSpan.MaxValue;
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task CloseAsync() {
        await dbRepository.SaveDiaAsync(Dia.ToEntity());
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


    #endregion
    // ====================================================================================================


}
