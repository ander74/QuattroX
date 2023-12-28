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
using QuattroX.Services;
#if IOS
using UIKit;
#endif
using QuattroX.View.CustomViews;

namespace QuattroX.ViewModel;


public partial class LineasViewModel : BaseViewModel {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================

    private readonly DbRepository dbRepository;
    private readonly ConfigService configService;
    private readonly CalculosService calculosService;

    public LineasViewModel(DbRepository dbRepository, ConfigService configService, CalculosService calculosService) {
        this.dbRepository = dbRepository;
        this.configService = configService;
        this.calculosService = calculosService;
        Title = "Líneas";

        LineassSeleccionadas.CollectionChanged += (sender, e) => {
            var num = LineassSeleccionadas.Count;
            IsSelectionMode = num > 0;
            if (num > 0) {
                Title = num == 1 ? "1 línea sel." : $"{num} líneas sel.";
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
    ObservableCollection<LineaModel> lineas;


    // Propiedades que gestionan la selección

    [ObservableProperty]
    ObservableCollection<object> lineassSeleccionadas = new();


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

            if (view is LineaButton) {
                handler.PlatformView.LongClick += (sender, e) => {
                    if (!IsSelectionMode) {
                        IsSelectionMode = true;
                    }
                    handler.PlatformView.CancelLongPress();
                };
            }
#endif
#if IOS
            if (view is LineaButton) {
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

    private async Task CargarLineas() {
        var lista = await dbRepository.GetLineasAsync();
        if (lista.Any()) {
            Lineas = lista.Select(l => l.ToModel()).ToObservableCollection();
        } else {
            Lineas = new();
        }
    }


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
    async Task LoadAsync() {
        try {
            IsBusy = true;
            if (Lineas is null || Lineas.Count == 0) {
                await CargarLineas();
            }
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand]
    void Back() {
        if (IsSelectionMode) {
            LineassSeleccionadas.Clear();
        }
    }


    [RelayCommand]
    async Task DesactivarSeleccionAsync() {
        try {
            IsBusy = true;
            LineassSeleccionadas.Clear();
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task AbrirLineaAsync(LineaModel linea) {
        if (IsSelectionMode || linea is null) return;
        //await Shell.Current.GoToAsync(nameof(DetalleLineaPage), true, new Dictionary<string, object> { { "Linea", linea } });
    }



    #endregion
    // ====================================================================================================


}
