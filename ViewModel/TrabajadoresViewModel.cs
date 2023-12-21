#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Helpers;
using QuattroX.Data.Messages;
using QuattroX.Data.Model;
#if IOS
using UIKit;
#endif
using QuattroX.Data.Repositories;
using QuattroX.View.CustomViews;

namespace QuattroX.ViewModel;


public partial class TrabajadoresViewModel : BaseViewModel {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================

    private readonly DbRepository dbRepository;


    public TrabajadoresViewModel(DbRepository dbRepository) {
        this.dbRepository = dbRepository;

        // Responde a la petición de un trabajador por su id.
        Messenger.Register<TrabajadorRequest>(this, (r, m) => {
            var trabajador = Trabajadores?.FirstOrDefault(t => t.Id == m.TrabajadorId);
            m.Reply(trabajador);
        });

        HandlerLongPress();

        Title = "Trabajador@s";
    }

    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    ObservableCollection<TrabajadorModel> trabajadores;



    [ObservableProperty]
    ObservableCollection<object> trabajadoresSeleccionados = new();


    [ObservableProperty]
    bool isSelectionMode;


    public bool IsNotSelectionMode => !IsSelectionMode;



    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos de plataforma
    // ====================================================================================================

    private void HandlerLongPress() {

        Microsoft.Maui.Handlers.ButtonHandler.Mapper.AppendToMapping("BotonLongPress", (handler, view) => {
#if ANDROID

            if (view is TrabajadorButton) {
                handler.PlatformView.LongClick += (sender, e) => {
                    if (!IsSelectionMode) {
                        IsSelectionMode = true;
                    }
                    handler.PlatformView.CancelLongPress();
                };
                //handler.PlatformView.Click += Border_Click;
            }
#endif
#if IOS
            if (view is CalendarioButton) {
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
    #region Métodos públicos
    // ====================================================================================================


    public async Task InitAsync() {
        if (Trabajadores is null || Trabajadores.Count == 0) {
            await CargarTrabajadores();
        }
    }

    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos privados
    // ====================================================================================================


    private async Task CargarTrabajadores() {
        try {
            IsBusy = true;
            var lista = await dbRepository.GetTrabajadoresAsync();
            var index = 1;
            Trabajadores = lista.Select(t => {
                var model = t.ToModel();
                model.RowIndex = index++;
                return model;
            }).ToObservableCollection();
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
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
            await CargarTrabajadores();
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task DesactivarSeleccionAsync() {
        try {
            IsBusy = true;
            TrabajadoresSeleccionados.Clear();
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    #endregion
    // ====================================================================================================



}
