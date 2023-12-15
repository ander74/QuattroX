#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Entities;
using QuattroX.Data.Enums;
using SQLite;

namespace QuattroX.Services;


public class DatabaseService {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================

    private const int DB_VERSION = 1;

    public DatabaseService() {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "quattrox.db");
        Db = new SQLiteAsyncConnection(dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
    }

    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    public SQLiteAsyncConnection Db { get; }


    public int Version { get; set; }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos públicos
    // ====================================================================================================


    public async Task InitAsync() {
        Version = await GetVersionAsync();
        if (Version < DB_VERSION) {
            // Creación de tablas
            await Db.CreateTableAsync<IncidenciaEntity>();
            await Db.CreateTableAsync<RegulacionEntity>();
            await Db.CreateTableAsync<TrabajadorEntity>();
            await Db.CreateTableAsync<ServicioSecundarioEntity>();
            await Db.CreateTableAsync<ServicioDiaEntity>();
            await Db.CreateTableAsync<ServicioSecundarioDiaEntity>();
            await Db.CreateTableAsync<ServicioLineaEntity>();
            await Db.CreateTableAsync<LineaEntity>();
            await Db.CreateTableAsync<DiaEntity>();
            await Db.CreateTableAsync<ResumenEntity>();
            await Db.CreateTableAsync<OpcionesEntity>();
            // Asignar la versión
            //TODO: Activar la siguiente línea en producción.
            //await SetVersionAsync(DB_VERSION); 
            //TODO: Llamar al seed inicial de las tablas
            await SeedDb();
        }
    }


    public async Task<int> GetVersionAsync() {
        return await Db.ExecuteScalarAsync<int>("PRAGMA user_version;");
    }


    public async Task SetVersionAsync(int newVersion) {
        await Db.ExecuteAsync($"PRAGMA user_version = {newVersion};");
    }


    public async Task<int> GetLastIdAsync() {
        return await Db.ExecuteScalarAsync<int>("SELECT last_insert_rowid();");
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos privados
    // ====================================================================================================


    private async Task SeedDb() {
        var incidencias = await Db.Table<IncidenciaEntity>().CountAsync();
        if (incidencias == 0) {
            await Db.InsertAllAsync(GetIncidenciasFijas());
        }
        var opciones = await Db.Table<OpcionesEntity>().CountAsync();
        if (opciones == 0) {
            await Db.InsertAsync(GetOpcionesPorDefecto());
        }
    }


    private List<IncidenciaEntity> GetIncidenciasFijas() {
        return new List<IncidenciaEntity> {
            new IncidenciaEntity { Codigo = 0, Descripcion = "Repite día anterior", Tipo = TipoIncidencia.Ninguna },
            new IncidenciaEntity { Codigo = 1, Descripcion = "Trabajo", Tipo = TipoIncidencia.Trabajo },
            new IncidenciaEntity { Codigo = 2, Descripcion = "Franqueo", Tipo = TipoIncidencia.Franqueo },
            new IncidenciaEntity { Codigo = 3, Descripcion = "Vacaciones", Tipo = TipoIncidencia.Franqueo },
            new IncidenciaEntity { Codigo = 4, Descripcion = "F.O.D.", Tipo = TipoIncidencia.FiestaPorOtroDia },
            new IncidenciaEntity { Codigo = 5, Descripcion = "Franqueo a trabajar", Tipo = TipoIncidencia.FranqueoTrabajado },
            new IncidenciaEntity { Codigo = 6, Descripcion = "Enferm@", Tipo = TipoIncidencia.Franqueo },
            new IncidenciaEntity { Codigo = 7, Descripcion = "Accidentad@", Tipo = TipoIncidencia.Franqueo },
            new IncidenciaEntity { Codigo = 8, Descripcion = "Permiso", Tipo = TipoIncidencia.JornadaMedia },
            new IncidenciaEntity { Codigo = 9, Descripcion = "F.N.R. año actual", Tipo = TipoIncidencia.Franqueo },
            new IncidenciaEntity { Codigo = 10, Descripcion = "F.N.R. año anterior", Tipo = TipoIncidencia.Franqueo },
            new IncidenciaEntity { Codigo = 11, Descripcion = "Nos hacen el día", Tipo = TipoIncidencia.Trabajo },
            new IncidenciaEntity { Codigo = 12, Descripcion = "Hacemos el día", Tipo = TipoIncidencia.TrabajoSinAcumular },
            new IncidenciaEntity { Codigo = 13, Descripcion = "Sanción", Tipo = TipoIncidencia.Franqueo },
            new IncidenciaEntity { Codigo = 14, Descripcion = "En otro destino", Tipo = TipoIncidencia.Franqueo },
            new IncidenciaEntity { Codigo = 15, Descripcion = "Huelga", Tipo = TipoIncidencia.Trabajo },
            new IncidenciaEntity { Codigo = 16, Descripcion = "Día por horas acumuladas", Tipo = TipoIncidencia.FiestaPorOtroDia }
        };
    }


    private OpcionesEntity GetOpcionesPorDefecto() {
        return new OpcionesEntity {
            PrimerMesMostrado = new DateTime(2024, 1, 1),
            AcumuladasAnteriores = 0m,
            RelevoFijo = 0,
            RellenarSemanaAutomaticamente = false,
            JornadaMedia = 7.75m,
            JornadaMinima = 7m,
            JornadaAnual = 1592,
            LimiteEntreServicios = 60,
            RegularJornadaAnual = false,
            RegularAñosBisiestos = false,
            InicioNocturnas = new TimeSpan(22, 0, 0),
            FinalNocturnas = new TimeSpan(6, 30, 0),
            HoraLimiteDesayuno = new TimeSpan(4, 30, 0),
            HoraLimiteComida1 = new TimeSpan(15, 30, 0),
            HoraLimiteComida2 = new TimeSpan(13, 30, 0),
            HoraLimiteCena = new TimeSpan(0, 30, 0),
            InferirTurnos = false,
            FechaReferenciaTurnos = new DateTime(2024, 1, 1),
            AcumularTomaDeje = false,
        };
    }
}

#endregion
// ====================================================================================================





