#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using CommunityToolkit.Maui.Core;
using QuattroX.Data.Helpers;
using QuattroX.Data.Model;

#if IOS
using UIKit;
#endif
using QuattroX.Data.Repositories;
using QuattroX.ViewModel.Popups;
using QuattroX.View.CustomViews;

namespace QuattroX.ViewModel;


[QueryProperty(nameof(ServicioLinea), "ServicioLinea")]
public partial class DetalleServicioLineaViewModel : BaseViewModel {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================


    private readonly DbRepository dbRepository;
    private readonly IPopupService popupService;

    public DetalleServicioLineaViewModel(DbRepository dbRepository, IPopupService popupService) {
        this.dbRepository = dbRepository;
        this.popupService = popupService;

        ServiciosSeleccionados.CollectionChanged += (sender, e) => {
            var num = ServiciosSeleccionados.Count;
            IsSelectionMode = num > 0;
            if (num > 0) {
                Title = num == 1 ? "1 servicio sel." : $"{num} servicios sel.";
            } else {
                Title = $"{ServicioLinea.Linea}: {ServicioLinea.Servicio}/{ServicioLinea.Turno}";
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
    ServicioLineaModel servicioLinea;

    partial void OnServicioLineaChanged(ServicioLineaModel value) {
        if (ServicioLinea is null) return;
        Title = $"{ServicioLinea.Linea}: {ServicioLinea.Servicio}/{ServicioLinea.Turno}";
        if (ServicioLinea.Servicios is null) {
            ServicioLinea.Servicios = new();
        }
        var index = 1;
        if (ServicioLinea.Servicios is not null) {
            foreach (var servicio in ServicioLinea.Servicios) servicio.RowIndex = index++;
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

            if (view is ServicioSecundarioButton) {
                handler.PlatformView.LongClick += (sender, e) => {
                    if (!IsSelectionMode) {
                        IsSelectionMode = true;
                    }
                    handler.PlatformView.CancelLongPress();
                };
            }
#endif
#if IOS
            if (view is ServicioSecundarioButton) {
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
        await dbRepository.SaveServicioLineaAsync(ServicioLinea);
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
        try {
            if (ServicioLinea is null) return;
            if (ServicioLinea.Servicios is null) ServicioLinea.Servicios = new();
            var resultado = await popupService.ShowPopupAsync<ServicioBasePopupViewModel>((vm) => {
                vm.Title = $"Nuevo servicio";
            });
            if (resultado is ServicioBaseModel model) {
                if (string.IsNullOrWhiteSpace(model.Linea) || string.IsNullOrWhiteSpace(model.Servicio) || model.Turno == 0) return;
                var servicio = new ServicioSecundarioModel();
                servicio.FromServicioBase(model);
                servicio.ServicioId = ServicioLinea.Id;
                servicio.RowIndex = ServicioLinea.Servicios.Count + 1;
                var id = await dbRepository.SaveServicioSecundarioAsync(servicio);
                servicio.Modified = false;
                ServicioLinea.Servicios.Add(servicio);
            }
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task BorrarServiciosAsync() {
        if (ServiciosSeleccionados is null || ServiciosSeleccionados.Count == 0) return;
        var mensaje = ServiciosSeleccionados.Count > 1 ?
            $"Vas a borrar {ServiciosSeleccionados.Count} servicios.\n\n¿Estás segur@?\n\nEsta acción no se puede deshacer." :
            $"Vas a borrar un servicio.\n\n¿Estás segur@?\n\nEsta acción no se puede deshacer.";
        var resultado = await Shell.Current.DisplayAlert("Borrar servicios", mensaje, "Borrar", "Cancelar");
        if (!resultado) return;
        try {
            IsBusy = true;
            foreach (ServicioSecundarioModel servicio in ServiciosSeleccionados) {
                await dbRepository.DeleteServicioSecundarioAsync(servicio.Id);
                ServicioLinea.Servicios.Remove(servicio);
            }
            ServiciosSeleccionados.Clear();
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    #endregion
    // ====================================================================================================


}
