#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

using QuattroX.Data.Model;

#if IOS
using UIKit;
#endif
using QuattroX.Data.Repositories;
using QuattroX.View.CustomViews;

namespace QuattroX.ViewModel;


[QueryProperty(nameof(Linea), "Linea")]
public partial class DetalleLineaViewModel : BaseViewModel {

    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================

    private readonly DbRepository dbRepository;


    public DetalleLineaViewModel(DbRepository dbRepository) {
        this.dbRepository = dbRepository;

        ServiciosSeleccionados.CollectionChanged += (sender, e) => {
            var num = ServiciosSeleccionados.Count;
            IsSelectionMode = num > 0;
            if (num > 0) {
                Title = num == 1 ? "1 servicio sel." : $"{num} servicios sel.";
            } else {
                Title = "Líneas";
            }
        };

        HandlerLongPress();
    }

    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    LineaModel linea;
    partial void OnLineaChanged(LineaModel oldValue, LineaModel newValue) {
        if (Linea is null) return;
        Title = Linea.Linea;
        var index = 1;
        if (Linea.Servicios is not null) {
            foreach (var servicio in Linea.Servicios) servicio.RowIndex = index++;
        }
    }


    // Propiedades que gestionan la selección

    [ObservableProperty]
    ObservableCollection<object> serviciosSeleccionados = new();


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotSelectionMode))]
    bool isSelectionMode;


    public bool IsNotSelectionMode => !IsSelectionMode;


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos públicos
    // ====================================================================================================


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos de plataforma
    // ====================================================================================================

    private void HandlerLongPress() {

        Microsoft.Maui.Handlers.ButtonHandler.Mapper.AppendToMapping("BotonLongPress", (handler, view) => {
#if ANDROID

            if (view is ServicioLineaButton) {
                handler.PlatformView.LongClick += (sender, e) => {
                    if (!IsSelectionMode) {
                        IsSelectionMode = true;
                    }
                    handler.PlatformView.CancelLongPress();
                };
            }
#endif
#if IOS
            if (view is ServicioLineaButton) {
                handler.PlatformView.UserInteractionEnabled = true;
                handler.PlatformView.AddGestureRecognizer(new UILongPressGestureRecognizer((s) => {
                    if (!IsSelectionMode) {
                        IsSelectionMode = true;
                    }
                }));
            }
#endif
        });

    }


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
    void Dummy() {
        // Comando que no hace nada para evitar que tocar sobre un área afecte al área por debajo.
    }


    [RelayCommand]
    async Task CloseAsync() {
        await dbRepository.SaveLineaAsync(Linea);
    }


    [RelayCommand]
    void Back() {
        if (IsSelectionMode) {
            ServiciosSeleccionados.Clear();
        }
    }


    [RelayCommand]
    async Task DesactivarSeleccionAsync() {
        try {
            IsBusy = true;
            ServiciosSeleccionados.Clear();
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task CrearServicioAsync() {

    }


    [RelayCommand]
    async Task BorrarServiciosAsync() {

    }


    #endregion
    // ====================================================================================================


}
