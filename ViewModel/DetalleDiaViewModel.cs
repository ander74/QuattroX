#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using CommunityToolkit.Maui.Core;
using QuattroX.Data.Helpers;
using QuattroX.Data.Messages;
using QuattroX.Data.Model;
using QuattroX.Data.Repositories;
using QuattroX.Services;
using QuattroX.View.CustomViews;
using QuattroX.ViewModel.Popups;
#if IOS
using UIKit;
#endif
using System.ComponentModel;

namespace QuattroX.ViewModel;


[QueryProperty(nameof(Dia), "Dia")]
public partial class DetalleDiaViewModel : BaseViewModel {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================

    private readonly DbRepository dbRepository;
    private readonly CalculosService calculosService;
    private readonly IPopupService popupService;

    public DetalleDiaViewModel(DbRepository dbRepository, CalculosService calculosService, IPopupService popupService) {
        this.dbRepository = dbRepository;
        this.calculosService = calculosService;
        this.popupService = popupService;

        ServiciosSeleccionados.CollectionChanged += (sender, e) => {
            var num = ServiciosSeleccionados.Count;
            IsSelectionMode = num > 0;
            if (num > 0) {
                Title = num == 1 ? "1 servicio sel." : $"{num} servicios sel.";
            } else {
                Title = $"{Dia.Fecha.Day:00} - {Textos.MesesAbr[Dia.Fecha.Month]} - {Dia.Fecha.Year}";
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
    DiaModel dia;
    partial void OnDiaChanged(DiaModel oldValue, DiaModel newValue) {
        Title = $"{Dia.Fecha.Day:00} - {Textos.MesesAbr[Dia.Fecha.Month]} - {Dia.Fecha.Year}";
        Dia.PropertyChanged += Dia_PropertyChanged;
    }


    [ObservableProperty]
    string turnoSeleccionado;
    partial void OnTurnoSeleccionadoChanged(string value) {
        if (value == "Mañana") Dia.Turno = 1;
        if (value == "Tarde") Dia.Turno = 2;
    }

    [ObservableProperty]
    ObservableCollection<IncidenciaModel> incidencias;


    [ObservableProperty]
    IncidenciaModel incidenciaSeleccionada;
    partial void OnIncidenciaSeleccionadaChanged(IncidenciaModel oldValue, IncidenciaModel newValue) {
        if (IncidenciaSeleccionada.Codigo > 0) {
            Dia.IncidenciaId = IncidenciaSeleccionada.Id;
            Dia.Incidencia = IncidenciaSeleccionada;
            Calcular();
        } else {
            //TODO: Si la incidencia seleccionada es cero, copiar el día anterior.
        }
    }


    public string[] Turnos => ["Mañana", "Tarde"];


    // Propiedades relacionadas con la selección de servicios

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

        Microsoft.Maui.Handlers.ButtonHandler.Mapper.AppendToMapping("BotonServicioDiaLongPress", (handler, view) => {
#if ANDROID

            if (view is ServicioDiaButton) {
                handler.PlatformView.LongClick += (sender, e) => {
                    if (!IsSelectionMode) {
                        IsSelectionMode = true;
                    }
                    handler.PlatformView.CancelLongPress();
                };
            }
#endif
#if IOS
            if (view is ServicioDiaButton) {
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


    /// <summary>
    /// Método que se lanza al cambiar una propiedad del Día.
    /// </summary>
    private void Dia_PropertyChanged(object sender, PropertyChangedEventArgs e) {
        if (e.PropertyName == nameof(DiaModel.Inicio) ||
            e.PropertyName == nameof(DiaModel.Final) ||
            e.PropertyName == nameof(DiaModel.Turno)) {
            Calcular();
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
            Incidencias = await dbRepository.GetIncidenciasAsync();
            if (Incidencias is null) Incidencias = new();
#pragma warning disable MVVMTK0034
            if (Dia.IncidenciaId > 0) {
                // Usamos el campo en lugar de la propiedad para no hacer que
                // se dispare el método parcial OnIncidenciaChanged inicialmente.
                incidenciaSeleccionada = Incidencias.Where(i => i.Id == Dia.IncidenciaId).FirstOrDefault();
            } else {
                IncidenciaSeleccionada = Incidencias.Where(i => i.Codigo == 1).FirstOrDefault();
            }
            OnPropertyChanged(nameof(IncidenciaSeleccionada));
#pragma warning restore MVVMTK0034
            if (Dia.Turno == 1) TurnoSeleccionado = Turnos[0];
            if (Dia.Turno == 2) TurnoSeleccionado = Turnos[1];
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task CloseAsync() {
        await dbRepository.SaveDiaAsync(Dia);
        Messenger.Send(new CalcularResumenRequest());
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


    [RelayCommand]
    void Calcular() {
        calculosService.CalcularHoras(Dia);
    }


    [RelayCommand]
    async Task EditarHoraAsync(string param) {
        var valor = param switch {
            "Trabajadas" => Dia.Trabajadas,
            "Acumuladas" => Dia.Acumuladas,
            "Nocturnas" => Dia.Nocturnas,
            _ => 0m,
        };
        var mensaje = $"Introduce horas {param}";
        var resultado = await Shell.Current.DisplayPromptAsync(param, mensaje, "Aceptar", "Cancelar", null, -1, Keyboard.Numeric, valor.ToTexto());
        if (string.IsNullOrWhiteSpace(resultado)) return;
        switch (param) {
            case "Trabajadas":
                Dia.Trabajadas = resultado.ToDecimal();
                break;
            case "Acumuladas":
                Dia.Acumuladas = resultado.ToDecimal();
                break;
            case "Nocturnas":
                Dia.Nocturnas = resultado.ToDecimal();
                break;
        }
    }


    [RelayCommand]
    async Task EditarServicioPrincipalAsync() {
        try {
            var resultado = await popupService.ShowPopupAsync<ServicioBasePopupViewModel>(async vm => {
                await vm.InitAsync();
                vm.Title = "Servicio principal";
                vm.Servicio = Dia.ToServicioBaseModel();
            });
            if (resultado is ServicioBaseModel servicioDia) {
                var servicio = await dbRepository.GetServicioLineaAsync(servicioDia.Linea, servicioDia.Servicio, servicioDia.Turno);
                if (string.IsNullOrWhiteSpace(servicio.Linea)) return;
                Dia.FromServicioBase(servicioDia);
                if (Dia.Servicios?.Any() == true) {
                    foreach (var sDia in Dia.Servicios) {
                        await dbRepository.DeleteServicioDiaAsync(sDia.Id);
                    }
                    Dia.Servicios.Clear();
                }
                if (servicio.Servicios?.Any() == true) {
                    var index = 1;
                    foreach (var servS in servicio.Servicios) {
                        var serv = new ServicioDiaModel();
                        serv.FromServicioBase(servS);
                        serv.DiaId = Dia.Id;
                        serv.RowIndex = index++;
                        await dbRepository.SaveServicioDiaAsync(serv);
                        Dia.Servicios.Add(serv);
                    }
                }
                Calcular();
            }
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task EditarRelevoAsync() {
        try {
            var resultado = await popupService.ShowPopupAsync<TrabajadorPopupViewModel>(async vm => {
                await vm.InitAsync();
                vm.Title = "Relevo";
                vm.TrabajadorSeleccionado = vm.Trabajadores.FirstOrDefault(t => t.Id == Dia.RelevoId);
            });
            if (resultado is TrabajadorModel model) {
                Dia.RelevoId = model.Id;
                Dia.Matricula = model.Matricula;
                Dia.Apellidos = model.Apellidos;
            }
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task EditarSustiAsync() {
        try {
            var resultado = await popupService.ShowPopupAsync<TrabajadorPopupViewModel>(async vm => {
                await vm.InitAsync();
                vm.Title = "Compañer@";
                vm.TrabajadorSeleccionado = vm.Trabajadores.FirstOrDefault(t => t.Id == Dia.SustiId);
            });
            if (resultado is TrabajadorModel model) {
                Dia.SustiId = model.Id;
                Dia.MatriculaSusti = model.Matricula;
                Dia.ApellidosSusti = model.Apellidos;
            }
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task AbrirServicioAsync(ServicioDiaModel servicio) {
        var model = servicio.ToServicioBaseModel();
        var resultado = await popupService.ShowPopupAsync<ServicioBasePopupViewModel>(async vm => {
            await vm.InitAsync();
            vm.Title = "Editar servicio";
            vm.Servicio = model;
        });
        if (resultado is ServicioBaseModel servicioDia) {
            var pos = Dia.Servicios.IndexOf(servicio);
            Dia.Servicios.Remove(servicio);
            servicio.FromServicioBase(servicioDia);
            Dia.Servicios.Insert(pos, servicio);
        }
    }


    [RelayCommand]
    async Task CrearServicioAsync() {
        try {
            if (Dia is null) return;
            if (Dia.Servicios is null)
                Dia.Servicios = new();
            var resultado = await popupService.ShowPopupAsync<ServicioBasePopupViewModel>(async (vm) => {
                await vm.InitAsync();
                vm.Title = "Nuevo servicio";
            });
            if (resultado is ServicioBaseModel model) {
                if (string.IsNullOrWhiteSpace(model.Linea) || string.IsNullOrWhiteSpace(model.Servicio) || model.Turno == 0) return;
                var servicio = new ServicioDiaModel();
                servicio.FromServicioBase(model);
                servicio.Id = 0;
                servicio.DiaId = Dia.Id;
                servicio.RowIndex = Dia.Servicios.Count + 1;
                var id = await dbRepository.SaveServicioDiaAsync(servicio);
                servicio.Modified = false;
                Dia.Servicios.Add(servicio);
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
        var resultado = await Shell.Current.DisplayAlert("Borrar servicio", mensaje, "Borrar", "Cancelar");
        if (!resultado) return;
        try {
            IsBusy = true;
            foreach (ServicioDiaModel servicio in ServiciosSeleccionados) {
                await dbRepository.DeleteServicioDiaAsync(servicio.Id);
                Dia.Servicios.Remove(servicio);
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
