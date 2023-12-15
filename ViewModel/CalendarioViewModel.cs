#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Entities;
using QuattroX.Data.Enums;
using QuattroX.Data.Helpers;
using QuattroX.Data.Model;
using QuattroX.Data.Repositories;
#if IOS
using UIKit;
#endif
using QuattroX.View;
using QuattroX.View.CustomViews;

namespace QuattroX.ViewModel;


public partial class CalendarioViewModel : BaseViewModel {



    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================

    private readonly DbRepository dbRepository;

    private DiaEntity diaCopiado;

    public CalendarioViewModel(DbRepository dbRepository) {
        this.dbRepository = dbRepository;
        Title = "Calendario";

        DiasSeleccionados.CollectionChanged += (sender, e) => {
            var num = DiasSeleccionados.Count;
            IsSelectionMode = num > 0;
            if (num > 0) {
                Title = num == 1 ? "1 día sel." : $"{num} días sel.";
            } else {
                Title = "Calendario";
            }
            OnPropertyChanged(nameof(OnlyOneItemSelected));
        };

        HandlerLongPress();
    }

    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextoMesActual))]
    DateTime fechaActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);


    [ObservableProperty]
    ObservableCollection<DiaModel> dias;


    [ObservableProperty]
    ResumenModel resumen;


    [ObservableProperty]
    List<IncidenciaModel> incidencias;


    // Propiedades que gestionan la selección

    [ObservableProperty]
    ObservableCollection<object> diasSeleccionados = new();


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotSelectionMode))]
    [NotifyPropertyChangedFor(nameof(OnlyOneItemSelected))]
    bool isSelectionMode;


    public bool IsNotSelectionMode => !IsSelectionMode;


    public string TextoMesActual => $"{Textos.Meses[FechaActual.Month]} - {FechaActual.Year}";


    public bool OnlyOneItemSelected => IsSelectionMode && (DiasSeleccionados?.Count == 1) == true;



    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos de plataforma
    // ====================================================================================================

    private void HandlerLongPress() {
        Microsoft.Maui.Handlers.BorderHandler.Mapper.AppendToMapping("BorderLongPress", (handler, view) => {
#if ANDROID

            if (view is CalendarioCellBorder) {
                handler.PlatformView.LongClick += (sender, e) => {
                    handler.PlatformView.CancelLongPress();
                };
                //handler.PlatformView.Click += Border_Click;
            }
#endif
#if IOS
            if (view is CalendarioCellBorder) {
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


    private async Task CrearMesAsync(DateTime fecha) {
        try {
            IsBusy = true;
            var dias = new List<DiaEntity>();
            for (int dia = 1; dia <= DateTime.DaysInMonth(fecha.Year, fecha.Month); dia++) {
                dias.Add(new DiaEntity {
                    Fecha = new DateTime(fecha.Year, fecha.Month, dia),
                });
            }
            await dbRepository.SaveDiasAsync(dias);
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    private async Task CargarMes() {
        try {
            IsBusy = true;
            var dias = await dbRepository.GetDiasByMesAsync(FechaActual);
            if (!dias.Any()) {
                await CrearMesAsync(FechaActual);
                dias = await dbRepository.GetDiasByMesAsync(FechaActual);
            }
            Dias = dias.Select(d => d.ToModel()).ToObservableCollection();
            var resumen = await dbRepository.GetResumenByFechaAsync(FechaActual);
            if (resumen == null) resumen = new();
            Resumen = resumen.ToModel();
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    private async Task CargarIncidencias() {
        try {
            IsBusy = true;
            if (Incidencias is null || Incidencias.Count == 0) {
                var incid = await dbRepository.GetIncidenciasAsync();
                if (incid.Any()) {
                    Incidencias = incid.Select(i => i.ToModel()).ToList();
                }
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
    void Dummy() {
        // Comando que no hace nada para evitar que tocar sobre un área afecte al área por debajo.
    }


    [RelayCommand]
    async Task LoadAsync() {
        try {
            IsBusy = true;
            await CargarMes();
            await CargarIncidencias();
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task AbrirDia(DiaModel dia) {
        if (IsSelectionMode || dia is null) return;
        await Shell.Current.GoToAsync(nameof(DetalleDiaPage), true, new Dictionary<string, object> { { "Dia", dia } });
    }


    [RelayCommand]
    async Task ActivarFranqueoFestivoAsync(DiaModel dia) {
        try {
            IsBusy = true;
            if (!dia.EsFranqueo && !dia.EsFestivo) {
                dia.EsFranqueo = true;
            } else {
                if (dia.EsFranqueo) {
                    dia.EsFranqueo = false;
                    dia.EsFestivo = true;
                } else {
                    dia.EsFranqueo = false;
                    dia.EsFestivo = false;
                }
            }
            if (dia.EsFranqueo && dia.Incidencia?.Codigo == 0) {
                var franqueo = Incidencias.FirstOrDefault(i => i.Codigo == 2);
                dia.Incidencia = franqueo;
                dia.IncidenciaId = franqueo.Id;
            }
            if (dia.EsFestivo && (dia.Incidencia?.Codigo == 0 || dia.Incidencia.Codigo == 2)) {
                var festivo = Incidencias.FirstOrDefault(i => i.Codigo == 9);
                dia.Incidencia = festivo;
                dia.IncidenciaId = festivo.Id;
            }
            if ((!dia.EsFranqueo && !dia.EsFestivo) && (dia.Incidencia.Codigo == 2 || dia.Incidencia.Codigo == 9)) {
                dia.Incidencia = new();
                dia.IncidenciaId = 0;
            }
            if (dia.Modified) await dbRepository.SaveDiaAsync(dia.ToEntity());
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
            DiasSeleccionados.Clear();
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task CopiarDiaAsync() {
        try {
            IsBusy = true;
            if (DiasSeleccionados is null || DiasSeleccionados.Count == 0) return;
            var dia = (DiaModel)DiasSeleccionados.FirstOrDefault();
            diaCopiado = dia.ToEntity();
            await Shell.Current.DisplaySnackbar("Se ha copiado el día");
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task PegarDiaAsync() {
        try {
            IsBusy = true;
            if (DiasSeleccionados is null || DiasSeleccionados.Count == 0) return;
            if (diaCopiado is null) return;
            foreach (DiaModel dia in DiasSeleccionados) {
                var id = dia.Id;
                var fecha = dia.Fecha;
                dia.FromEntity(diaCopiado);
                dia.Id = id;
                dia.Fecha = fecha;
                await dbRepository.SaveDiaAsync(dia.ToEntity());
            }
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task AlternarFranqueoFestivoAsync() {
        try {
            IsBusy = true;
            if (DiasSeleccionados is null || DiasSeleccionados.Count == 0) return;
            foreach (DiaModel dia in DiasSeleccionados) {
                await ActivarFranqueoFestivoAsync(dia);
            }
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task CrearRegulacionAsync() {
        if (DiasSeleccionados is null || DiasSeleccionados.Count == 0) return;
        var dia = (DiaModel)DiasSeleccionados.FirstOrDefault();
        var horas = await Shell.Current.DisplayPromptAsync(
            "Nueva regulación", "Introduce las horas a regular.", "Siguiente", "Cancelar");
        if (horas is null) return;
        var horasDecimal = horas.ToDecimal();
        if (horasDecimal == 0) {
            await Shell.Current.DisplayAlert("Error", "No se puede crear una regulación con cero horas.", "Cerrar");
            return;
        }
        var motivo = await Shell.Current.DisplayPromptAsync(
           "Nueva regulación", "Introduce el motivo de la regulación.", "Crear", "Cancelar");
        try {
            IsBusy = true;
            if (motivo is null) return;
            var regulacion = new RegulacionModel {
                DiaId = dia.Id,
                Horas = horasDecimal,
                Motivo = motivo,
                Tipo = TipoRegulacion.Manual,
            };
            await dbRepository.SaveRegulacionAsync(regulacion.ToEntity());
            await Shell.Current.DisplaySnackbar("Regulación creada correctamente.");
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task VaciarDiaAsync() {
        if (DiasSeleccionados is null || DiasSeleccionados.Count == 0) return;
        var resultado = await Shell.Current.DisplayAlert("Vaciar días", "¿Estás seguro de vaciar los días?", "Sí", "Cancelar");
        if (!resultado) return;
        try {
            IsBusy = true;
            foreach (DiaModel dia in DiasSeleccionados) {
                dia.Vaciar();
                await dbRepository.SaveDiaAsync(dia.ToEntity());
            }
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }






    #endregion
    // ====================================================================================================


}
