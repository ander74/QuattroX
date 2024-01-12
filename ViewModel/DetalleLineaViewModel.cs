#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

using CommunityToolkit.Maui.Core;
using QuattroX.Data.Model;

#if IOS
using UIKit;
#endif
using QuattroX.Data.Repositories;
using QuattroX.View.CustomViews;
using QuattroX.ViewModel.Popups;
using QuattroX.View;

namespace QuattroX.ViewModel;


[QueryProperty(nameof(Linea), "Linea")]
public partial class DetalleLineaViewModel : BaseViewModel {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================

    private readonly DbRepository dbRepository;
    private readonly IPopupService popupService;

    public DetalleLineaViewModel(DbRepository dbRepository, IPopupService popupService) {
        this.dbRepository = dbRepository;
        this.popupService = popupService;

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
    async Task AbrirServicioAsync(ServicioLineaModel servicio) {
        if (IsSelectionMode || servicio is null) return;
        await Shell.Current.GoToAsync(nameof(DetalleServicioLineaPage), true, new Dictionary<string, object> { { "ServicioLinea", servicio } });
    }


    [RelayCommand]
    async Task CrearServicioAsync() {
        try {
            if (Linea is null) return;
            if (Linea.Servicios is null) Linea.Servicios = new();
            var resultado = await popupService.ShowPopupAsync<ServicioLineaPopupViewModel>(async (vm) => {
                vm.Title = $"Nuevo servicio {Linea.Linea}";
                vm.Servicio = new ServicioLineaModel { Linea = Linea.Linea, TextoLinea = Linea.Texto, LineaId = Linea.Id, };
            });
            if (resultado is ServicioLineaModel model) {
                if (string.IsNullOrWhiteSpace(model.Linea) || string.IsNullOrWhiteSpace(model.Servicio) || model.Turno == 0) return;
                model.RowIndex = Linea.Servicios.Count + 1;
                var id = await dbRepository.SaveServicioLineaAsync(model);
                model.Modified = false;
                Linea.Servicios.Add(model);
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
            foreach (ServicioLineaModel servicio in ServiciosSeleccionados) {
                await dbRepository.DeleteServicioLineaAsync(servicio.Id);
                Linea.Servicios.Remove(servicio);
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
