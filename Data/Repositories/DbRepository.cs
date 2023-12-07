#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Entities;
using QuattroX.Services;

namespace QuattroX.Data.Repositories;


public class DbRepository {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================

    private readonly DatabaseService dbService;


    public DbRepository(DatabaseService dbService) {
        this.dbService = dbService;
    }

    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Servicios secundarios
    // ====================================================================================================


    public async Task<List<ServicioSecundarioEntity>> GetServiciosSecundariosByServicioAsync(int servicioId) {
        return await dbService.Db.Table<ServicioSecundarioEntity>().Where(s => s.ServicioId == servicioId).ToListAsync();
    }


    public async Task SaveServicioSecundarioAsync(ServicioSecundarioEntity servicio) {
        if (servicio is null) return;
        if (servicio.Id == 0) {
            await dbService.Db.InsertAsync(servicio);
            var id = await dbService.GetLastIdAsync();
            servicio.Id = id;
        } else {
            await dbService.Db.UpdateAsync(servicio);
        }
    }


    public async Task SaveServiciosSecundariosAsync(IEnumerable<ServicioSecundarioEntity> servicios) {
        if (servicios is null || servicios.Count() == 0) return;
        foreach (var servicio in servicios) {
            await SaveServicioSecundarioAsync(servicio);
        }
    }


    public async Task DeleteServicioSecundarioAsync(int id) {
        await dbService.Db.DeleteAsync<ServicioSecundarioEntity>(id);
    }


    public async Task DeleteServiciosSecundariosAsync(IEnumerable<ServicioSecundarioEntity> servicios) {
        if (servicios is null || servicios.Count() == 0) return;
        foreach (var servicio in servicios) {
            await dbService.Db.DeleteAsync(servicio);
        }
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Servicios Línea
    // ====================================================================================================

    public async Task<List<ServicioLineaEntity>> GetServiciosLineaByLineaAsync(int lineaId) {
        var servicios = await dbService.Db.Table<ServicioLineaEntity>().Where(s => s.LineaId == lineaId).ToListAsync();
        foreach (var servicio in servicios) {
            servicio.Servicios = await GetServiciosSecundariosByServicioAsync(servicio.Id);
        }
        return servicios;
    }


    public async Task SaveServicioLineaAsync(ServicioLineaEntity servicio) {
        if (servicio is null) return;
        if (servicio.Servicios?.Any() == true) {
            await SaveServiciosSecundariosAsync(servicio.Servicios);
        }
        if (servicio.Id == 0) {
            await dbService.Db.InsertAsync(servicio);
            var id = await dbService.GetLastIdAsync();
            servicio.Id = id;
        } else {
            await dbService.Db.UpdateAsync(servicio);
        }
    }


    public async Task SaveServiciosLineaAsync(IEnumerable<ServicioLineaEntity> servicios) {
        if (servicios is null || servicios.Count() == 0) return;
        foreach (var servicio in servicios) {
            await SaveServicioLineaAsync(servicio);
        }
    }


    public async Task DeleteServicioLineaAsync(int id) {
        var servicio = await dbService.Db.Table<ServicioLineaEntity>().Where(s => s.Id == id).FirstOrDefaultAsync();
        if (servicio is null) return;
        if (servicio.Servicios?.Any() == true) {
            await DeleteServiciosSecundariosAsync(servicio.Servicios);
        }
        await dbService.Db.DeleteAsync(servicio);
    }


    public async Task DeleteServiciosLineaAsync(IEnumerable<ServicioLineaEntity> servicios) {
        if (servicios is null || servicios.Count() == 0) return;
        foreach (var servicio in servicios) {
            await DeleteServicioLineaAsync(servicio.Id);
        }
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Líneas
    // ====================================================================================================


    public async Task<List<LineaEntity>> GetLineasAsync() {
        var lineas = await dbService.Db.Table<LineaEntity>().ToListAsync();
        foreach (var linea in lineas) {
            linea.Servicios = await GetServiciosLineaByLineaAsync(linea.Id);
        }
        return lineas;
    }


    public async Task SaveLineaAsync(LineaEntity linea) {
        if (linea is null) return;
        if (linea.Servicios?.Any() == true) {
            await SaveServiciosLineaAsync(linea.Servicios);
        }
        if (linea.Id == 0) {
            await dbService.Db.InsertAsync(linea);
            var id = await dbService.GetLastIdAsync();
            linea.Id = id;
        } else {
            await dbService.Db.UpdateAsync(linea);
        }
    }


    public async Task SaveLineasAsync(IEnumerable<LineaEntity> lineas) {
        if (lineas is null || lineas.Count() == 0) return;
        foreach (var linea in lineas) {
            await SaveLineaAsync(linea);
        }
    }


    public async Task DeleteLineaAsync(int id) {
        var linea = await dbService.Db.Table<LineaEntity>().Where(l => l.Id == id).FirstOrDefaultAsync();
        if (linea is null) return;
        if (linea.Servicios?.Any() == true) {
            await DeleteServiciosLineaAsync(linea.Servicios);
        }
        await dbService.Db.DeleteAsync(linea);
    }


    public async Task DeleteLineasAsync(IEnumerable<LineaEntity> lineas) {
        if (lineas is null || lineas.Count() == 0) return;
        foreach (var linea in lineas) {
            await DeleteLineaAsync(linea.Id);
        }
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Trabajadores
    // ====================================================================================================


    public async Task<List<TrabajadorEntity>> GetTrabajadoresAsync() {
        return await dbService.Db.Table<TrabajadorEntity>().ToListAsync();
    }


    public async Task<TrabajadorEntity> GetTrabajadorByIdAsync(int id) {
        return await dbService.Db.Table<TrabajadorEntity>().Where(t => t.Id == id).FirstOrDefaultAsync();
    }


    public async Task<TrabajadorEntity> GetTrabajadorByMatriculaAsync(int matricula) {
        return await dbService.Db.Table<TrabajadorEntity>().Where(t => t.Matricula == matricula).FirstOrDefaultAsync();
    }


    public async Task<int> AddTrabajadorAsync(TrabajadorEntity trabajador) {
        await dbService.Db.InsertAsync(trabajador);
        return await dbService.GetLastIdAsync();
    }


    public async Task UpdateTrabajadorAsync(TrabajadorEntity trabajador) {
        await dbService.Db.UpdateAsync(trabajador);
    }


    public async Task DeleteTrabajadorAsync(int id) {
        await dbService.Db.DeleteAsync<TrabajadorEntity>(id);
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Regulaciones
    // ====================================================================================================


    public async Task<List<RegulacionEntity>> GetRegulacionesByDiaAsync(int diaId) {
        return await dbService.Db.Table<RegulacionEntity>().Where(r => r.DiaId == diaId).ToListAsync();
    }


    public async Task SaveRegulacionAsync(RegulacionEntity regulacion) {
        if (regulacion is null) return;
        if (regulacion.Id == 0) {
            await dbService.Db.InsertAsync(regulacion);
            var id = await dbService.GetLastIdAsync();
            regulacion.Id = id;
        } else {
            await dbService.Db.UpdateAsync(regulacion);
        }
    }


    public async Task SaveRegulacionesAsync(IEnumerable<RegulacionEntity> regulaciones) {
        if (regulaciones is null || regulaciones.Count() == 0) return;
        foreach (var regulacion in regulaciones) {
            await SaveRegulacionAsync(regulacion);
        }
    }


    public async Task DeleteRegulacionAsync(int id) {
        await dbService.Db.DeleteAsync<RegulacionEntity>(id);
    }


    public async Task DeleteRegulacionesAsync(IEnumerable<RegulacionEntity> regulaciones) {
        if (regulaciones is null || regulaciones.Count() == 0) return;
        foreach (var regulacion in regulaciones) {
            await dbService.Db.DeleteAsync(regulacion);
        }
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Incidencias
    // ====================================================================================================


    public async Task<List<IncidenciaEntity>> GetIncidenciasAsync() {
        return await dbService.Db.Table<IncidenciaEntity>().ToListAsync();
    }


    public async Task<IncidenciaEntity> GetIncidenciaByIdAsync(int id) {
        return await dbService.Db.Table<IncidenciaEntity>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }


    public async Task SaveIncidenciaAsync(IncidenciaEntity incidencia) {
        if (incidencia is null) return;
        if (incidencia.Id == 0) {
            await dbService.Db.InsertAsync(incidencia);
            var id = await dbService.GetLastIdAsync();
            incidencia.Id = id;
        } else {
            await dbService.Db.UpdateAsync(incidencia);
        }
    }


    public async Task SaveIncidenciasAsync(IEnumerable<IncidenciaEntity> incidencias) {
        if (incidencias is null || incidencias.Count() == 0) return;
        foreach (var incidencia in incidencias) {
            await SaveIncidenciaAsync(incidencia);
        }
    }


    public async Task DeleteIncidenciaAsync(int id) {
        await dbService.Db.DeleteAsync<IncidenciaEntity>(id);
    }


    public async Task DeleteIncidenciaAsync(IEnumerable<IncidenciaEntity> incidencias) {
        if (incidencias is null || incidencias.Count() == 0) return;
        foreach (var incidencia in incidencias) {
            await dbService.Db.DeleteAsync(incidencia);
        }
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Calendario
    // ====================================================================================================


    public async Task<List<DiaEntity>> GetDiasByMesAsync(DateTime fecha) {
        var fechaPosterior = fecha.AddMonths(1);
        return await dbService.Db.Table<DiaEntity>().Where(d => d.Fecha >= fecha && d.Fecha < fechaPosterior).ToListAsync();
    }


    public async Task<DiaEntity> GetDiaByIdAsync(int id) {
        return await dbService.Db.Table<DiaEntity>().Where(d => d.Id == id).FirstOrDefaultAsync();
    }


    public async Task SaveDiaAsync(DiaEntity dia) {
        if (dia is null) return;
        if (dia.Id == 0) {
            await dbService.Db.InsertAsync(dia);
            var id = await dbService.GetLastIdAsync();
            dia.Id = id;
        } else {
            await dbService.Db.UpdateAsync(dia);
        }
    }


    public async Task SaveDiasAsync(IEnumerable<DiaEntity> dias) {
        if (dias is null || dias.Count() == 0) return;
        foreach (var dia in dias) {
            await SaveDiaAsync(dia);
        }
    }


    // NOTA: Quattro X no contempla eliminar días, por lo que no ponemos los métodos correspondientes.


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Resúmenes
    // ====================================================================================================


    public async Task<ResumenEntity> GetResumenByFechaAsync(DateTime fecha) {
        return await dbService.Db.Table<ResumenEntity>().Where(r => r.Fecha == fecha).FirstOrDefaultAsync();
    }


    public async Task<ResumenEntity> GetResumenHastaFechaAsync(DateTime fecha) {
        var resumenes = await dbService.Db.Table<ResumenEntity>().Where(r => r.Fecha <= fecha).ToListAsync();
        return new ResumenEntity {
            Trabajadas = resumenes.Sum(r => r.Trabajadas),
            TrabajadasConvenio = resumenes.Sum(r => r.TrabajadasConvenio),
            Acumuladas = resumenes.Sum(r => r.Acumuladas),
            Nocturnas = resumenes.Sum(r => r.Nocturnas),
            TomaDeje = resumenes.Sum(r => r.TomaDeje),
            Euros = resumenes.Sum(r => r.Euros),
            Regulaciones = resumenes.Sum(r => r.Regulaciones),
        };
    }


    public async Task SaveResumenAsync(ResumenEntity resumen) {
        if (resumen == null) return;
        if (resumen.Id == 0) {
            await dbService.Db.InsertAsync(resumen);
            var id = await dbService.GetLastIdAsync();
            resumen.Id = id;
        } else {
            await dbService.Db.UpdateAsync(resumen);
        }
    }


    public async Task SaveResumenesAsync(IEnumerable<ResumenEntity> resumenes) {
        if (resumenes is null || resumenes.Count() == 0) return;
        foreach (var resumen in resumenes) {
            await SaveResumenAsync(resumen);
        }
    }


    public async Task DeleteResumenAsync(int id) {
        await dbService.Db.DeleteAsync<ResumenEntity>(id);
    }


    public async Task DeleteResumenesAsync(IEnumerable<ResumenEntity> resumenes) {
        if (resumenes is null || resumenes.Count() == 0) return;
        foreach (var resumen in resumenes) {
            await DeleteResumenAsync(resumen.Id);
        }
    }


    #endregion
    // ====================================================================================================



}
