#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Model;
using QuattroX.Data.Repositories;
using QuattroX.Services;
using QuattroX.View;
using QuattroX.Data.Messages;


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

        LineasSeleccionadas.CollectionChanged += (sender, e) => {
            var num = LineasSeleccionadas.Count;
            IsSelectionMode = num > 0;
            if (num > 0) {
                Title = num == 1 ? "1 línea sel." : $"{num} líneas sel.";
            } else {
                Title = "Líneas";
            }
        };

        // Responde a la petición de las líneas.
        Messenger.Register<LineasRequest>(this, (r, m) => {
            if (m.IncluirServicios) {
                m.Reply(Lineas);
                return;
            }
            m.Reply(Lineas.Select(l => new LineaModel { Id = l.Id, Linea = l.Linea, Texto = l.Texto }).ToObservableCollection());
        });

        // Responde a la petición de una línea.
        Messenger.Register<LineaRequest>(this, (r, m) => {
            var linea = Lineas.FirstOrDefault(l => l.Linea.ToUpper() == m.Linea.ToUpper());
            m.Reply(linea);
        });

        // Responde a la petición de añadir una línea.
        Messenger.Register<AddLineaRequest>(this, async (r, m) => {
            if (m.Linea is null) return;
            await dbRepository.SaveLineaAsync(m.Linea);
            m.Linea.RowIndex = Lineas.Count + 1;
            if (!Lineas.Contains(m.Linea)) Lineas.Add(m.Linea);
        });

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
    ObservableCollection<object> lineasSeleccionadas = new();


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotSelectionMode))]
    bool isSelectionMode;


    public bool IsNotSelectionMode => !IsSelectionMode;

    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos públicos
    // ====================================================================================================


    public async Task InitAsync() {
        if (Lineas is null || Lineas.Count == 0) {
            await CargarLineas();
        }
    }

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
        Lineas = await dbRepository.GetLineasAsync();
        var index = 1;
        foreach (var linea in Lineas) {
            linea.RowIndex = index++;
            linea.Modified = false;
        }
    }


    private void RenumerarLineas() {
        var index = 1;
        foreach (var linea in Lineas) {
            linea.RowIndex = index++;
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
    async Task BackAsync() {
        if (IsSelectionMode) {
            LineasSeleccionadas.Clear();
        } else {
            await Shell.Current.GoToAsync("///CalendarioPage");
        }
    }


    [RelayCommand]
    async Task DesactivarSeleccionAsync() {
        try {
            IsBusy = true;
            LineasSeleccionadas.Clear();
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task AbrirLineaAsync(LineaModel linea) {
        if (IsSelectionMode || linea is null) return;
        await Shell.Current.GoToAsync(nameof(DetalleLineaPage), true, new Dictionary<string, object> { { "Linea", linea } });
    }


    [RelayCommand]
    async Task CrearLineaAsync() {
        if (Lineas is null) return;
        var resultado = await Shell.Current.DisplayPromptAsync("Nueva línea",
            "Introduce la línea", "Siguiente", "Cancelar", null, -1, Keyboard.Text);
        if (resultado is null) return;
        if (await dbRepository.ExisteLineaAsync(resultado)) {
            await Shell.Current.DisplayAlert("ERROR", $"La línea {resultado} ya está registrada.", "Cerrar");
            return;
        }
        var descripcion = await Shell.Current.DisplayPromptAsync("Nueva línea",
            "Introduce la descripción", "Crear", "Cancelar", null, -1, Keyboard.Chat);
        if (descripcion is null) return;
        var newLinea = new LineaModel { Linea = resultado, Texto = descripcion };
        var id = await dbRepository.SaveLineaAsync(newLinea);
        newLinea.Id = id;
        newLinea.RowIndex = Lineas.Count + 1;
        Lineas.Add(newLinea);
        await AbrirLineaAsync(newLinea);
    }


    [RelayCommand]
    async Task BorrarLineasAsync() {
        if (LineasSeleccionadas is null || LineasSeleccionadas.Count == 0) return;
        var mensaje = LineasSeleccionadas.Count > 1 ?
            $"Vas a borrar {LineasSeleccionadas.Count} líneas.\n\n¿Estás segur@?\n\nEsta acción no se puede deshacer." :
            $"Vas a borrar una línea.\n\n¿Estás segur@?\n\nEsta acción no se puede deshacer.";
        var resultado = await Shell.Current.DisplayAlert("Borrar líneas", mensaje, "Borrar", "Cancelar");
        if (!resultado) return;
        try {
            IsBusy = true;
            foreach (LineaModel linea in LineasSeleccionadas) {
                await dbRepository.DeleteLineaAsync(linea.Id);
                Lineas.Remove(linea);
            }
            LineasSeleccionadas.Clear();
            await CargarLineas();
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }



    #endregion
    // ====================================================================================================


}
