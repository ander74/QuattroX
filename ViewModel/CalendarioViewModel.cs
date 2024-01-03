#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Enums;
using QuattroX.Data.Helpers;
using QuattroX.Data.Messages;
using QuattroX.Data.Model;
using QuattroX.Data.Repositories;
using QuattroX.Services;
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
    private readonly ConfigService configService;
    private readonly CalculosService calculosService;

    private DiaModel diaCopiado;

    public CalendarioViewModel(DbRepository dbRepository, ConfigService configService, CalculosService calculosService) {
        this.dbRepository = dbRepository;
        this.configService = configService;
        this.calculosService = calculosService;
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

        // Responde a la petición de recalcular el resumen.
        Messenger.Register<CalcularResumenRequest>(this, async (r, m) => await CalcularResumen());

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
    [NotifyPropertyChangedFor(nameof(AcumuladasHastaMes))]
    [NotifyPropertyChangedFor(nameof(NocturnasHastaMes))]
    [NotifyPropertyChangedFor(nameof(TrabajadasHastaMes))]
    [NotifyPropertyChangedFor(nameof(TrabajadasConvenioHastaMes))]
    [NotifyPropertyChangedFor(nameof(EurosHastaMes))]
    [NotifyPropertyChangedFor(nameof(TomaDejeHastaMes))]
    [NotifyPropertyChangedFor(nameof(RegulacionesHastaMes))]
    ResumenModel resumenMes;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AcumuladasHastaMes))]
    [NotifyPropertyChangedFor(nameof(NocturnasHastaMes))]
    [NotifyPropertyChangedFor(nameof(TrabajadasHastaMes))]
    [NotifyPropertyChangedFor(nameof(TrabajadasConvenioHastaMes))]
    [NotifyPropertyChangedFor(nameof(EurosHastaMes))]
    [NotifyPropertyChangedFor(nameof(TomaDejeHastaMes))]
    [NotifyPropertyChangedFor(nameof(RegulacionesHastaMes))]
    ResumenModel resumenHastaMes;


    [ObservableProperty]
    ObservableCollection<IncidenciaModel> incidencias;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AcumuladasHastaMes))]
    ObservableCollection<RegulacionModel> regulaciones;


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


    public decimal AcumuladasHastaMes =>
        (configService?.Opciones?.AcumuladasAnteriores ?? 0m) +
        (ResumenHastaMes?.Acumuladas ?? 0m) +
        (ResumenHastaMes?.Regulaciones ?? 0m) +
        (ResumenMes?.Acumuladas ?? 0m) +
        (ResumenMes?.Regulaciones ?? 0m);


    public decimal NocturnasHastaMes =>
        (ResumenHastaMes?.Nocturnas ?? 0m) +
        (ResumenMes?.Nocturnas ?? 0m);


    public decimal TrabajadasHastaMes =>
        (ResumenHastaMes?.Trabajadas ?? 0m) +
        (ResumenMes?.Trabajadas ?? 0m);


    public decimal TrabajadasConvenioHastaMes =>
        (ResumenHastaMes?.TrabajadasConvenio ?? 0m) +
        (ResumenMes?.TrabajadasConvenio ?? 0m);


    public decimal EurosHastaMes =>
        (ResumenHastaMes?.Euros ?? 0m) +
        (ResumenMes?.Euros ?? 0m);


    public decimal TomaDejeHastaMes =>
        (ResumenHastaMes?.TomaDeje ?? 0m) +
        (ResumenMes?.TomaDeje ?? 0m);


    public decimal RegulacionesHastaMes =>
        (ResumenHastaMes?.Regulaciones ?? 0m) +
        (ResumenMes?.Regulaciones ?? 0m);


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos de plataforma
    // ====================================================================================================

    private void HandlerLongPress() {

        Microsoft.Maui.Handlers.ButtonHandler.Mapper.AppendToMapping("BotonLongPress", (handler, view) => {
#if ANDROID

            if (view is CalendarioButton) {
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
    #region Métodos privados
    // ====================================================================================================


    private async Task CrearMesAsync(DateTime fecha) {
        var dias = new List<DiaModel>();
        for (int dia = 1; dia <= DateTime.DaysInMonth(fecha.Year, fecha.Month); dia++) {
            dias.Add(new DiaModel {
                Fecha = new DateTime(fecha.Year, fecha.Month, dia),
            });
        }
        await dbRepository.SaveDiasAsync(dias);
    }


    private async Task CargarMes() {
        Dias = await dbRepository.GetDiasAsync(FechaActual, true);
        if (!Dias.Any()) {
            await CrearMesAsync(FechaActual);
            Dias = await dbRepository.GetDiasAsync(FechaActual, true);
        }
        foreach (var dia in Dias) {
            if (dia.Incidencia == null) dia.Incidencia = new();
            dia.Incidencia.FromModel(Incidencias.FirstOrDefault(i => i.Id == dia.IncidenciaId));
            if (dia.RelevoId > 0) {
                TrabajadorModel relevo = Messenger.Send(new TrabajadorRequest(dia.RelevoId));
                if (relevo is not null) {
                    dia.Matricula = relevo.Matricula;
                    dia.Apellidos = relevo.Apellidos;
                }
            }
            if (dia.SustiId > 0) {
                TrabajadorModel susti = Messenger.Send(new TrabajadorRequest(dia.SustiId));
                if (susti is not null) {
                    dia.MatriculaSusti = susti.Matricula;
                    dia.ApellidosSusti = susti.Apellidos;
                }
            }
            if (dia.Servicios is not null) {
                var index = 1;
                foreach (var serv in dia.Servicios) {
                    serv.RowIndex = index++;
                }
            }
            if (configService.Opciones.InferirTurnos) {
                if (dia.Turno == 0) dia.Turno = calculosService.InferirTurno(dia.Fecha, configService.Opciones.FechaReferenciaTurnos, 1);
            }
        }
    }


    private async Task CargarIncidencias() {
        if (Incidencias is null || Incidencias.Count == 0) {
            Incidencias = await dbRepository.GetIncidenciasAsync();
        }
    }


    private async Task CargarResumenes() {
        ResumenMes = await dbRepository.GetResumenAsync(FechaActual);
        if (ResumenMes is null) {
            ResumenMes = new() { Fecha = FechaActual };
            await CalcularResumen();
        }
        var fechaHasta = FechaActual.AddDays(-1);
        ResumenHastaMes = await dbRepository.GetResumenHastaFechaAsync(fechaHasta);
    }


    private async Task CargarRegulaciones() {
        Regulaciones = await dbRepository.GetRegulacionesByMesAsync(FechaActual);
    }


    private async Task CalcularResumen() {
        if (ResumenMes is null) ResumenMes = new() { Fecha = FechaActual };
        if (Regulaciones is null) Regulaciones = new();
        ResumenMes.Trabajadas = Dias.Sum(d => Math.Round(d.Trabajadas, 2));
        ResumenMes.Acumuladas = Dias.Sum(d => Math.Round(d.Acumuladas, 2));
        ResumenMes.Nocturnas = Dias.Sum(d => Math.Round(d.Nocturnas, 2));
        ResumenMes.TrabajadasConvenio = Dias.Sum(d => calculosService.GetTrabajadasConvenio(d.Incidencia.Tipo));
        ResumenMes.Euros = Dias.Sum(d => Math.Round(d.Euros, 2));
        ResumenMes.TomaDeje = Dias.Sum(d => Math.Round(d.TomaDeje, 2));
        ResumenMes.Regulaciones = Regulaciones.Sum(r => Math.Round(r.Horas, 2));
        var id = await dbRepository.SaveResumenAsync(ResumenMes);
        ResumenMes.Id = id;
        OnPropertyChanged(nameof(ResumenMes));
        OnPropertyChanged(nameof(AcumuladasHastaMes));
        OnPropertyChanged(nameof(NocturnasHastaMes));
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
            if (Incidencias is null || Incidencias.Count == 0) await CargarIncidencias();
            if (Dias is null || Dias.Count == 0) {
                await CargarMes();
                await CargarResumenes();
                await CargarRegulaciones();
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
            DiasSeleccionados.Clear();
        }
    }


    [RelayCommand]
    async Task MesAnteriorAsync() {
        if (IsBusy) return;
        if (FechaActual.AddMonths(-1) < configService.Opciones.PrimerMesMostrado) {
            await Shell.Current.DisplaySnackbar("No puedes ir más atrás de este mes.");
            return;
        }
        try {
            IsBusy = true;
            FechaActual = FechaActual.AddMonths(-1);
            await CargarMes();
            await CargarResumenes();
            await CargarRegulaciones();
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task MesSiguienteAsync() {
        if (IsBusy) return;
        try {
            IsBusy = true;
            FechaActual = FechaActual.AddMonths(1);
            await CargarMes();
            await CargarResumenes();
            await CargarRegulaciones();
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
            if (dia.Modified) await dbRepository.SaveDiaAsync(dia);
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
            if (diaCopiado is null) diaCopiado = new();
            diaCopiado.FromModel(dia);
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
                dia.FromModel(diaCopiado);
                dia.Id = id;
                dia.Fecha = fecha;
                await dbRepository.SaveDiaAsync(dia);
            }
            await CalcularResumen();
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
                Fecha = dia.Fecha,
                Horas = horasDecimal,
                Motivo = motivo,
                Tipo = TipoRegulacion.Manual,
            };
            await dbRepository.SaveRegulacionAsync(regulacion);
            await CargarRegulaciones();
            await CalcularResumen();
            DiasSeleccionados.Clear();
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
                if (configService.Opciones.InferirTurnos) {
                    dia.Turno = calculosService.InferirTurno(dia.Fecha, configService.Opciones.FechaReferenciaTurnos, 1);
                }
                await dbRepository.SaveDiaAsync(dia);
            }
            await CalcularResumen();
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }






    #endregion
    // ====================================================================================================


}
