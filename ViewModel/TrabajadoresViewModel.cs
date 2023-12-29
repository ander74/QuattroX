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
using QuattroX.View;
using QuattroX.View.CustomViews;

namespace QuattroX.ViewModel;


public partial class TrabajadoresViewModel : BaseViewModel {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================

    private readonly DbRepository dbRepository;


    public TrabajadoresViewModel(DbRepository dbRepository) {
        this.dbRepository = dbRepository;
        Title = "Trabajador@s";

        TrabajadoresSeleccionados.CollectionChanged += (sender, e) => {
            var num = TrabajadoresSeleccionados.Count;
            IsSelectionMode = num > 0;
            if (num > 0) {
                Title = num == 1 ? "1 persona sel." : $"{num} personas sel.";
            } else {
                Title = "Trabajador@s";
            }
        };

        // Responde a la petición de un trabajador por su id.
        Messenger.Register<TrabajadorRequest>(this, (r, m) => {
            var trabajador = Trabajadores?.FirstOrDefault(t => t.Id == m.TrabajadorId);
            m.Reply(trabajador);
        });

        HandlerLongPress();

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
    [NotifyPropertyChangedFor(nameof(IsNotSelectionMode))]
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
            Trabajadores = new();
            foreach (var trabajador in lista) {
                var model = trabajador.ToModel();
                model.RowIndex = index++;
                model.DiasLeHago = await dbRepository.GetDiasHagoATrabajadorAsync(trabajador.Id);
                model.DiasMeHace = await dbRepository.GetDiasMeHaceTrabajadorAsync(trabajador.Id);
                model.Modified = false;
                Trabajadores.Add(model);
            }
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
            if (Trabajadores is null || Trabajadores.Count == 0) {
                await CargarTrabajadores();
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
            TrabajadoresSeleccionados.Clear();
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


    [RelayCommand]
    async Task AbrirTrabajadorAsync(TrabajadorModel trabajador) {
        if (IsSelectionMode || trabajador is null) return;
        await Shell.Current.GoToAsync(nameof(DetalleTrabajadorPage), true, new Dictionary<string, object> { { "Trabajador", trabajador } });
    }


    [RelayCommand]
    async Task CrearTrabajadorAsync() {
        if (Trabajadores is null) return;
        var resultado = await Shell.Current.DisplayPromptAsync("Nuevo trabajador/a", "Introduce la matrícula", "Crear", "Cancelar");
        if (resultado is null) return;
        if (int.TryParse(resultado, out int matricula)) {
            if (await dbRepository.ExisteTrabajadorByMatriculaAsync(matricula)) {
                await Shell.Current.DisplayAlert("ERROR", $"Ya existe una persona registrada con la matrícula {matricula}.", "Cerrar");
                return;
            }
            var newTrabajador = new TrabajadorModel { Matricula = matricula };
            var id = await dbRepository.SaveTrabajadorAsync(newTrabajador.ToEntity());
            newTrabajador.Id = id;
            Trabajadores.Add(newTrabajador);
            await AbrirTrabajadorAsync(newTrabajador);
        }
    }


    [RelayCommand]
    async Task BorrarTrabajadoresAsync() {
        if (TrabajadoresSeleccionados is null || TrabajadoresSeleccionados.Count == 0) return;
        var mensaje = TrabajadoresSeleccionados.Count > 1 ?
            $"Vas a borrar {TrabajadoresSeleccionados.Count} trabajador@s.\n\n¿Estás segur@?\n\nEsta acción no se puede deshacer." :
            $"Vas a borrar un trabajador/a.\n\n¿Estás segur@?\n\nEsta acción no se puede deshacer.";
        var resultado = await Shell.Current.DisplayAlert("Borrar trabajador@s", mensaje, "Borrar", "Cancelar");
        if (!resultado) return;
        try {
            IsBusy = true;
            foreach (TrabajadorModel trabajador in TrabajadoresSeleccionados) {
                await dbRepository.DeleteTrabajadorAsync(trabajador.Id);
                Trabajadores.Remove(trabajador);
            }
            TrabajadoresSeleccionados.Clear();
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand(CanExecute = nameof(CanLlamar))]
    void Llamar(TrabajadorModel trabajador) {
        if (trabajador is null) return;
        if (string.IsNullOrWhiteSpace(trabajador.Telefono)) return;
        if (PhoneDialer.Default.IsSupported) {
            PhoneDialer.Default.Open(trabajador.Telefono);
        }
    }
    private bool CanLlamar(TrabajadorModel trabajador) => !string.IsNullOrWhiteSpace(trabajador?.Telefono);


    [RelayCommand(CanExecute = nameof(CanMandarEmail))]
    async Task MandarEmail(TrabajadorModel trabajador) {
        if (trabajador is null) return;
        if (string.IsNullOrWhiteSpace(trabajador.Email)) return;
        if (Email.Default.IsComposeSupported) {
            var message = new EmailMessage {
                Subject = "From Quattro X",
                Body = string.Empty,
                BodyFormat = EmailBodyFormat.PlainText,
                To = [trabajador.Email],
            };
            await Email.Default.ComposeAsync(message);
        }
    }
    private bool CanMandarEmail(TrabajadorModel trabajador) => !string.IsNullOrWhiteSpace(trabajador?.Telefono);


    #endregion
    // ====================================================================================================



}
