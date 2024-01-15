#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Enums;
using QuattroX.Data.Helpers;
using QuattroX.Data.Model;
using QuattroX.Data.Repositories;
using QuattroX.Services;
using QuattroX.View.Helpers;

namespace QuattroX.ViewModel;


public partial class MainViewModel : BaseViewModel {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================

    private readonly DatabaseService dbService;
    private readonly ConfigService configService;
    private readonly DbRepository dbRepository;
    private readonly LineasViewModel lineasViewModel;
    private readonly TrabajadoresViewModel trabajadoresViewModel;
    private readonly DropboxService dropboxService;

    public MainViewModel(DatabaseService dbService, ConfigService configService, DbRepository dbRepository, LineasViewModel lineasViewModel,
        TrabajadoresViewModel trabajadoresViewModel, DropboxService dropboxService) {
        this.dbService = dbService;
        this.configService = configService;
        this.dbRepository = dbRepository;
        this.lineasViewModel = lineasViewModel;
        this.trabajadoresViewModel = trabajadoresViewModel;
        this.dropboxService = dropboxService;
        Title = "Cargando";
    }

    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    public string TextoVersion => $"Versión {AppInfo.Current.VersionString}";


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos privados
    // ====================================================================================================


    private List<TrabajadorModel> GetTrabajadoresPruebas() {
        List<TrabajadorModel> lista = new();
        lista.Add(new TrabajadorModel { Matricula = 5060, Nombre = "Andrés", Apellidos = "Herrero Módenes", Calificacion = CalificacionTrabajador.Normal });
        lista.Add(new TrabajadorModel { Matricula = 4935, Nombre = "Joseba", Apellidos = "Moyano Reyero", Calificacion = CalificacionTrabajador.Buena });
        lista.Add(new TrabajadorModel { Matricula = 5190, Nombre = "Arturo", Apellidos = "Gómez Torres", Calificacion = CalificacionTrabajador.Mala });
        lista.Add(new TrabajadorModel { Matricula = 8008, Nombre = "Erika", Apellidos = "Carrillo Luengas", Calificacion = CalificacionTrabajador.Buena });
        return lista;
    }


    private List<LineaModel> GetLineasPruebas() {
        List<LineaModel> lista = new();

        // 2314
        lista.Add(new LineaModel {
            Linea = "2314",
            Texto = "Plaza Cirular - UPV",
            Servicios = new() {
                new ServicioLineaModel {
                    Inicio = new TimeSpan(6,15,0),
                    Final = new TimeSpan(14,15,0),
                    LugarInicio = "Aparcabisa",
                    LugarFinal = "Plaza Circular",
                    Linea = "2314",
                    Servicio = "01",
                    Turno = 1,
                    TextoLinea = "Plaza Cirular - UPV",
                },
                new ServicioLineaModel {
                    Inicio = new TimeSpan(14,15,0),
                    Final = new TimeSpan(21,45,0),
                    LugarInicio = "Plaza Circular",
                    LugarFinal = "Aparcabisa",
                    Linea = "2314",
                    Servicio = "01",
                    Turno = 2,
                    TextoLinea = "Plaza Cirular - UPV",
                },
                new ServicioLineaModel {
                    Inicio = new TimeSpan(6,45,0),
                    Final = new TimeSpan(14,45,0),
                    LugarInicio = "Aparcabisa",
                    LugarFinal = "Plaza Circular",
                    Linea = "2314",
                    Servicio = "02",
                    Turno = 1,
                    TextoLinea = "Plaza Cirular - UPV",
                },
                new ServicioLineaModel {
                    Inicio = new TimeSpan(14,45,0),
                    Final = new TimeSpan(22,15,0),
                    LugarInicio = "Plaza Circular",
                    LugarFinal = "Aparcabisa",
                    Linea = "2314",
                    Servicio = "02",
                    Turno = 2,
                    TextoLinea = "Plaza Cirular - UPV",
                },
            }
        });

        // 2318
        lista.Add(new LineaModel {
            Linea = "2318",
            Texto = "Termibus - UPV",
            Servicios = new() {
                new ServicioLineaModel {
                    Inicio = new TimeSpan(6,30,0),
                    Final = new TimeSpan(14,10,0),
                    LugarInicio = "Aparcabisa",
                    LugarFinal = "Termibus",
                    Linea = "2318",
                    Servicio = "01",
                    Turno = 1,
                    TextoLinea = "Termibus - UPV",
                },
                new ServicioLineaModel {
                    Inicio = new TimeSpan(14,10,0),
                    Final = new TimeSpan(21,35,0),
                    LugarInicio = "Termibus",
                    LugarFinal = "Aparcabisa",
                    Linea = "2318",
                    Servicio = "01",
                    Turno = 2,
                    TextoLinea = "Termibus - UPV",
                },
                new ServicioLineaModel {
                    Inicio = new TimeSpan(7,05,0),
                    Final = new TimeSpan(14,25,0),
                    LugarInicio = "Aparcabisa",
                    LugarFinal = "Termibus",
                    Linea = "2318",
                    Servicio = "02",
                    Turno = 1,
                    TextoLinea = "Termibus - UPV",
                },
                new ServicioLineaModel {
                    Inicio = new TimeSpan(14,25,0),
                    Final = new TimeSpan(22,00,0),
                    LugarInicio = "Termibus",
                    LugarFinal = "Aparcabisa",
                    Linea = "2318",
                    Servicio = "02",
                    Turno = 2,
                    TextoLinea = "Termibus - UPV",
                },
            }
        });

        // 2321
        lista.Add(new LineaModel {
            Linea = "2321",
            Texto = "Santutxu - UPV",
            Servicios = new() {
                new ServicioLineaModel {
                    Inicio = new TimeSpan(6,40,0),
                    Final = new TimeSpan(14,35,0),
                    LugarInicio = "Aparcabisa",
                    LugarFinal = "Santutxu",
                    Linea = "2321",
                    Servicio = "01",
                    Turno = 1,
                    TextoLinea = "Santutxu - UPV",
                },
                new ServicioLineaModel {
                    Inicio = new TimeSpan(14,35,0),
                    Final = new TimeSpan(21,20,0),
                    LugarInicio = "Santutxu",
                    LugarFinal = "Aparcabisa",
                    Linea = "2321",
                    Servicio = "01",
                    Turno = 2,
                    TextoLinea = "Santutxu - UPV",
                },
                new ServicioLineaModel {
                    Inicio = new TimeSpan(7,10,0),
                    Final = new TimeSpan(15,05,0),
                    LugarInicio = "Aparcabisa",
                    LugarFinal = "Santutxu",
                    Linea = "2321",
                    Servicio = "02",
                    Turno = 1,
                    TextoLinea = "Santutxu - UPV",
                },
                new ServicioLineaModel {
                    Inicio = new TimeSpan(15,05,0),
                    Final = new TimeSpan(22,10,0),
                    LugarInicio = "Santutxu",
                    LugarFinal = "Aparcabisa",
                    Linea = "2321",
                    Servicio = "02",
                    Turno = 2,
                    TextoLinea = "Santutxu - UPV",
                },
            }
        });

        return lista;
    }



    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Comandos
    // ====================================================================================================


    [RelayCommand]
    async Task LoadAsync() {

        // Personalizamos los controles visuales.
        CustomizePlatformViews.CustomizeViews();

        // Poner aquí todo lo que tiene que pasar al iniciar la aplicación.
        await dbService.InitAsync();
        await configService.InitAsync();
        await trabajadoresViewModel.InitAsync();
        await lineasViewModel.InitAsync();

        //TODO: Autenticación de Dropbox.



        //TODO: Quitar los mocks en producción.
        //var trabajadores = await dbService.Db.Table<TrabajadorEntity>().CountAsync();
        //if (trabajadores == 0) {
        //    await dbRepository.SaveTrabajadoresAsync(GetTrabajadoresPruebas());
        //}

        //var lineas = await dbService.Db.Table<LineaEntity>().CountAsync();
        //if (lineas == 0) {
        //    await dbRepository.SaveLineasAsync(GetLineasPruebas());
        //}

        // Si no se ha aceptado la licencia vamos a la página de licencia, si no, a la de calendario.
        if (Preferences.Default.Get(SettingsNames.LICENCIA_ACEPTADA, false)) {
            await Shell.Current.GoToAsync("///CalendarioPage");
        } else {
            await Shell.Current.GoToAsync("LicenciaPage");
        }
    }


    #endregion
    // ====================================================================================================


}
