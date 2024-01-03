#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Helpers;
using QuattroX.Data.Model;
using QuattroX.Data.Repositories;

namespace QuattroX.ViewModel.Popups;


public partial class ServicioBasePopupViewModel : BaseViewModel {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================


    private readonly DbRepository dbRepository;


    public ServicioBasePopupViewModel(DbRepository dbRepository) {
        Title = "Nuevo servicio";
        this.dbRepository = dbRepository;
    }

    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    ServicioBaseModel servicio = new();

    partial void OnServicioChanged(ServicioBaseModel value) {
        if (Servicio is null) return;
        LineaSeleccionada = Lineas.FirstOrDefault(l => l.Linea == Servicio.Linea);
        ServicioSeleccionado = LineaSeleccionada?.Servicios?.FirstOrDefault(s => s.Servicio == Servicio.Servicio && s.Turno == Servicio.Turno);
    }


    [ObservableProperty]
    bool bloquearLinea;


    [ObservableProperty]
    ObservableCollection<LineaModel> lineas;


    [ObservableProperty]
    LineaModel lineaSeleccionada;

    async partial void OnLineaSeleccionadaChanged(LineaModel value) {
        if (value is null) return;
        if (value.Linea == "Nueva") {
            var id = await CrearNuevaLineaAsync();
            LineaSeleccionada = Lineas.FirstOrDefault(l => l.Id == id);
        } else {
            Servicios = LineaSeleccionada.Servicios;
            if (Servicios is null) Servicios = new();
            Servicios.Insert(0, new ServicioLineaModel { Servicio = "Nuevo" });
        }
    }


    [ObservableProperty]
    ObservableCollection<ServicioLineaModel> servicios;


    [ObservableProperty]
    ServicioLineaModel servicioSeleccionado;

    partial void OnServicioSeleccionadoChanged(ServicioLineaModel value) {
        if (value is null) {
            IsNuevoServicio = false;
            return;
        }
        if (value.Servicio == "Nuevo") {
            IsNuevoServicio = true;
        } else {
            IsNuevoServicio = false;
        }
    }


    [ObservableProperty]
    bool isNuevoServicio;


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos públicos
    // ====================================================================================================

    public async Task InitAsync() {
        Lineas = await dbRepository.GetLineasAsync();
        Lineas.Insert(0, new LineaModel { Linea = "Nueva" });
    }

    public async Task AceptarAsync() {
        if (IsNuevoServicio && ServicioSeleccionado.Servicio == "Nuevo") {
            ServicioSeleccionado = null;
            return;
        }
        if (!(await dbRepository.ExisteServicioLineaAsync(ServicioSeleccionado.Linea, ServicioSeleccionado.Servicio, ServicioSeleccionado.Turno))) {
            ServicioSeleccionado.LineaId = LineaSeleccionada.Id;
            ServicioSeleccionado.Linea = LineaSeleccionada.Linea;
            ServicioSeleccionado.TextoLinea = LineaSeleccionada.Texto;
            await dbRepository.SaveServicioLineaAsync(ServicioSeleccionado);
        }
        Servicio.FromServicioBase(ServicioSeleccionado);
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos privados
    // ====================================================================================================


    private async Task<int> CrearNuevaLineaAsync() {
        var resultado = await Shell.Current.DisplayPromptAsync("Nueva línea", "Introduce la línea", "Siguiente", "Cancelar");
        if (resultado is null) return -1;
        if (await dbRepository.ExisteLineaAsync(resultado)) {
            await Shell.Current.DisplayAlert("ERROR", $"La línea {resultado} ya está registrada.", "Cerrar");
            return -1;
        }
        var descripcion = await Shell.Current.DisplayPromptAsync("Nueva línea", "Introduce la descripción", "Crear", "Cancelar");
        if (descripcion is null) return -1;
        var newLinea = new LineaModel { Linea = resultado, Texto = descripcion };
        await dbRepository.SaveLineaAsync(newLinea);
        Lineas.Add(newLinea);
        return newLinea.Id;
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Comandos
    // ====================================================================================================




    #endregion
    // ====================================================================================================


}
