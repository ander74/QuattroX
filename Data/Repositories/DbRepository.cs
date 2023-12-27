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
    #region ServicioSecundario
    // ====================================================================================================


    public async Task<List<ServicioSecundarioEntity>> GetServiciosSecundariosAsync(int servicioId) {
        var servicios = await dbService.Db.Table<ServicioSecundarioEntity>().Where(s => s.ServicioId == servicioId).OrderBy(t => t.Servicio).ToListAsync();
        if (servicios is null) servicios = new();
        return servicios;
    }


    public async Task<int> SaveServicioSecundarioAsync(ServicioSecundarioEntity servicio) {
        if (servicio is null) return 0;
        if (servicio.Id == 0) {
            await dbService.Db.InsertAsync(servicio);
            var id = await dbService.GetLastIdAsync();
            servicio.Id = id;
        } else {
            await dbService.Db.UpdateAsync(servicio);
        }
        return servicio.Id;
    }


    public async Task SaveServiciosSecundariosAsync(IEnumerable<ServicioSecundarioEntity> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await SaveServicioSecundarioAsync(item);
        }
    }


    public async Task DeleteServicioSecundarioAsync(int servicioId) {
        await dbService.Db.DeleteAsync<ServicioSecundarioEntity>(servicioId);
    }


    public async Task DeleteServiciosSecundariosAsync(IEnumerable<ServicioSecundarioEntity> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await DeleteServicioSecundarioAsync(item.Id);
        }
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region ServicioLínea
    // ====================================================================================================


    public async Task<List<ServicioLineaEntity>> GetServiciosLineaAsync(int lineaId, bool incluirSecundarios = true) {
        var servicios = await dbService.Db.Table<ServicioLineaEntity>().Where(s => s.LineaId == lineaId).OrderBy(t => t.Servicio).ToListAsync();
        if (servicios is null) servicios = new();
        if (incluirSecundarios) {
            foreach (var servicio in servicios) {
                servicio.Servicios = await GetServiciosSecundariosAsync(servicio.Id);
                if (servicio.Servicios is null) servicio.Servicios = new();
            }
        }
        return servicios;
    }


    public async Task<int> SaveServicioLineaAsync(ServicioLineaEntity servicio) {
        if (servicio is null) return 0;
        if (servicio.Id == 0) {
            await dbService.Db.InsertAsync(servicio);
            var id = await dbService.GetLastIdAsync();
            servicio.Id = id;
        } else {
            await dbService.Db.UpdateAsync(servicio);
        }
        if (servicio.Servicios is not null) {
            foreach (var serv in servicio.Servicios) {
                serv.ServicioId = servicio.Id;
            }
        }
        await SaveServiciosSecundariosAsync(servicio.Servicios);
        return servicio.Id;
    }


    public async Task SaveServiciosLineaAsync(IEnumerable<ServicioLineaEntity> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await SaveServicioLineaAsync(item);
        }
    }


    public async Task DeleteServicioLineaAsync(int servicioId) {
        await dbService.Db.DeleteAsync<ServicioLineaEntity>(servicioId);
    }


    public async Task DeleteServiciosLineaAsync(IEnumerable<ServicioLineaEntity> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await DeleteServicioLineaAsync(item.Id);
        }
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Línea
    // ====================================================================================================


    public async Task<List<LineaEntity>> GetLineasAsync(bool incluirServicios = true) {
        var lineas = await dbService.Db.Table<LineaEntity>().OrderBy(t => t.Linea).ToListAsync();
        if (lineas is null) lineas = new();
        if (incluirServicios) {
            foreach (var linea in lineas) {
                linea.Servicios = await GetServiciosLineaAsync(linea.Id, incluirServicios);
                if (linea.Servicios is null) linea.Servicios = new();
            }
        }
        return lineas;
    }


    public async Task<int> SaveLineaAsync(LineaEntity linea) {
        if (linea is null) return 0;
        if (linea.Id == 0) {
            await dbService.Db.InsertAsync(linea);
            var id = await dbService.GetLastIdAsync();
            linea.Id = id;
        } else {
            await dbService.Db.UpdateAsync(linea);
        }
        if (linea.Servicios is not null) {
            foreach (var servicio in linea.Servicios) {
                servicio.LineaId = linea.Id;
            }
        }
        await SaveServiciosLineaAsync(linea.Servicios);
        return linea.Id;
    }


    public async Task SaveLineasAsync(IEnumerable<LineaEntity> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await SaveLineaAsync(item);
        }
    }


    public async Task DeleteLineaAsync(int lineaId) {
        await dbService.Db.DeleteAsync<LineaEntity>(lineaId);
    }


    public async Task DeleteLineasAsync(IEnumerable<LineaEntity> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await DeleteLineaAsync(item.Id);
        }
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Incidencia
    // ====================================================================================================


    public async Task<List<IncidenciaEntity>> GetIncidenciasAsync() {
        var incidencias = await dbService.Db.Table<IncidenciaEntity>().OrderBy(t => t.Codigo).ToListAsync();
        if (incidencias is null) incidencias = new();
        return incidencias;
    }


    public async Task<IncidenciaEntity> GetIncidenciaByIdAsync(int incidenciaId) {
        var incidencia = await dbService.Db.Table<IncidenciaEntity>().Where(i => i.Id == incidenciaId).FirstOrDefaultAsync();
        if (incidencia is null) incidencia = new();
        return incidencia;
    }


    public async Task<int> SaveIncidenciaAsync(IncidenciaEntity incidencia) {
        if (incidencia is null) return 0;
        if (incidencia.Id == 0) {
            await dbService.Db.InsertAsync(incidencia);
            var id = await dbService.GetLastIdAsync();
            incidencia.Id = id;
        } else {
            await dbService.Db.UpdateAsync(incidencia);
        }
        return incidencia.Id;
    }


    public async Task SaveIncidenciasAsync(IEnumerable<IncidenciaEntity> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await SaveIncidenciaAsync(item);
        }
    }


    public async Task DeleteIncidenciaAsync(int incidenciaId) {
        await dbService.Db.DeleteAsync<IncidenciaEntity>(incidenciaId);
    }


    public async Task DeleteIncidenciasAsync(IEnumerable<IncidenciaEntity> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await DeleteIncidenciaAsync(item.Id);
        }
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Trabajador
    // ====================================================================================================


    public async Task<List<TrabajadorEntity>> GetTrabajadoresAsync() {
        var trabajadores = await dbService.Db.Table<TrabajadorEntity>().OrderBy(t => t.Matricula).ToListAsync();
        if (trabajadores is null) trabajadores = new();
        return trabajadores;
    }


    public async Task<TrabajadorEntity> GetTrabajadorByIdAsync(int relevoId) {
        var trabajador = await dbService.Db.Table<TrabajadorEntity>().Where(t => t.Id == relevoId).FirstOrDefaultAsync();
        if (trabajador is null) trabajador = new();
        return trabajador;
    }


    public async Task<TrabajadorEntity> GetTrabajadorByMatriculaAsync(int matricula) {
        var trabajador = await dbService.Db.Table<TrabajadorEntity>().Where(t => t.Matricula == matricula).FirstOrDefaultAsync();
        if (trabajador is null) trabajador = new();
        return trabajador;
    }


    public async Task<int> GetDiasHagoATrabajadorAsync(int id) {
        var incidencia = await dbService.Db.Table<IncidenciaEntity>().Where(i => i.Codigo == 12).FirstOrDefaultAsync();
        if (incidencia is null) return 0;
        return await dbService.Db.Table<DiaEntity>().CountAsync(t => t.SustiId == id && t.IncidenciaId == incidencia.Id);
    }


    public async Task<int> GetDiasMeHaceTrabajadorAsync(int id) {
        var incidencia = await dbService.Db.Table<IncidenciaEntity>().Where(i => i.Codigo == 11).FirstOrDefaultAsync();
        if (incidencia is null) return 0;
        return await dbService.Db.Table<DiaEntity>().CountAsync(t => t.SustiId == id && t.IncidenciaId == incidencia.Id);
    }


    public async Task<bool> ExisteTrabajadorByIdAsync(int relevoId) {
        return await dbService.Db.Table<TrabajadorEntity>().CountAsync(t => t.Id == relevoId) > 0;
    }


    public async Task<bool> ExisteTrabajadorByMatriculaAsync(int matricula) {
        return await dbService.Db.Table<TrabajadorEntity>().CountAsync(t => t.Matricula == matricula) > 0;
    }


    public async Task<int> SaveTrabajadorAsync(TrabajadorEntity trabajador) {
        if (trabajador is null) return 0;
        if (trabajador.Id == 0) {
            await dbService.Db.InsertAsync(trabajador);
            var id = await dbService.GetLastIdAsync();
            trabajador.Id = id;
        } else {
            await dbService.Db.UpdateAsync(trabajador);
        }
        return trabajador.Id;
    }


    public async Task SaveTrabajadoresAsync(IEnumerable<TrabajadorEntity> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await SaveTrabajadorAsync(item);
        }
    }


    public async Task DeleteTrabajadorAsync(int trabajadorId) {
        await dbService.Db.DeleteAsync<TrabajadorEntity>(trabajadorId);
    }


    public async Task DeleteTrabajadoresAsync(IEnumerable<TrabajadorEntity> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await DeleteTrabajadorAsync(item.Id);
        }
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region ServicioDía
    // ====================================================================================================


    public async Task<List<ServicioDiaEntity>> GetServiciosDiaAsync(int diaId) {
        var servicios = await dbService.Db.Table<ServicioDiaEntity>().Where(s => s.DiaId == diaId).OrderBy(t => t.Servicio).ToListAsync();
        if (servicios is null) servicios = new();
        return servicios;
    }


    public async Task<int> SaveServicioDiaAsync(ServicioDiaEntity servicio) {
        if (servicio is null) return 0;
        if (servicio.Id == 0) {
            await dbService.Db.InsertAsync(servicio);
            var id = await dbService.GetLastIdAsync();
            servicio.Id = id;
        } else {
            await dbService.Db.UpdateAsync(servicio);
        }
        return servicio.Id;
    }


    public async Task SaveServiciosDiaAsync(IEnumerable<ServicioDiaEntity> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await SaveServicioDiaAsync(item);
        }
    }


    public async Task DeleteServicioDiaAsync(int servicioId) {
        await dbService.Db.DeleteAsync<ServicioDiaEntity>(servicioId);
    }


    public async Task DeleteServiciosDiaAsync(IEnumerable<ServicioDiaEntity> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await DeleteServicioDiaAsync(item.Id);
        }
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Día
    // ====================================================================================================


    public async Task<List<DiaEntity>> GetDiasAsync(DateTime fecha, bool incluirServicios = true) {
        var fechaAnterior = new DateTime(fecha.Year, fecha.Month, 1);
        var fechaPosterior = fecha.AddMonths(1);
        var dias = await dbService.Db.Table<DiaEntity>().Where(d => d.Fecha >= fechaAnterior && d.Fecha < fechaPosterior).OrderBy(t => t.Fecha).ToListAsync();
        if (dias is null) dias = new();
        if (incluirServicios) {
            foreach (var dia in dias) {
                dia.Servicios = await GetServiciosDiaAsync(dia.Id);
                if (dia.Servicios is null) dia.Servicios = new();
            }
        }
        return dias;
    }


    public async Task<DiaEntity> GetDiaByIdAsync(int diaId, bool incluirServicios = true) {
        var dia = await dbService.Db.Table<DiaEntity>().Where(d => d.Id == diaId).OrderBy(t => t.Fecha).FirstOrDefaultAsync();
        if (dia is null) dia = new();
        if (incluirServicios) {
            dia.Servicios = await GetServiciosDiaAsync(dia.Id);
            if (dia.Servicios is null) dia.Servicios = new();
        }
        return dia;
    }


    public async Task<int> SaveDiaAsync(DiaEntity dia) {
        if (dia is null) return 0;
        if (dia.Id == 0) {
            await dbService.Db.InsertAsync(dia);
            var id = await dbService.GetLastIdAsync();
            dia.Id = id;
        } else {
            await dbService.Db.UpdateAsync(dia);
        }
        if (dia.Servicios is not null) {
            foreach (var servicio in dia.Servicios) {
                servicio.DiaId = dia.Id;
            }
        }
        await SaveServiciosDiaAsync(dia.Servicios);
        return dia.Id;
    }


    public async Task SaveDiasAsync(IEnumerable<DiaEntity> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await SaveDiaAsync(item);
        }
    }


    public async Task DeleteDiaAsync(int diaId) {
        await dbService.Db.DeleteAsync<DiaEntity>(diaId);
    }


    public async Task DeleteDiasAsync(IEnumerable<DiaEntity> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await DeleteDiaAsync(item.Id);
        }
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Regulación
    // ====================================================================================================


    public async Task<List<RegulacionEntity>> GetRegulacionesByDiaAsync(DateTime fecha) {
        var regulaciones = await dbService.Db.Table<RegulacionEntity>().Where(s => s.Fecha == fecha).ToListAsync();
        if (regulaciones is null) regulaciones = new();
        return regulaciones;
    }


    public async Task<List<RegulacionEntity>> GetRegulacionesByMesAsync(DateTime fecha) {
        var fechaInicio = new DateTime(fecha.Year, fecha.Month, 1);
        var fechaFinal = fechaInicio.AddMonths(1);
        var regulaciones = await dbService.Db.Table<RegulacionEntity>().Where(s => s.Fecha >= fechaInicio && s.Fecha < fechaFinal).ToListAsync();
        if (regulaciones is null) regulaciones = new();
        return regulaciones;
    }


    public async Task<int> SaveRegulacionAsync(RegulacionEntity regulacion) {
        if (regulacion is null) return 0;
        if (regulacion.Id == 0) {
            await dbService.Db.InsertAsync(regulacion);
            var id = await dbService.GetLastIdAsync();
            regulacion.Id = id;
        } else {
            await dbService.Db.UpdateAsync(regulacion);
        }
        return regulacion.Id;
    }


    public async Task SaveRegulacionesAsync(IEnumerable<RegulacionEntity> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await SaveRegulacionAsync(item);
        }
    }


    public async Task DeleteRegulacionAsync(int regulacionId) {
        await dbService.Db.DeleteAsync<RegulacionEntity>(regulacionId);
    }


    public async Task DeleteRegulacionesAsync(IEnumerable<RegulacionEntity> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await DeleteRegulacionAsync(item.Id);
        }
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Resumen
    // ====================================================================================================


    public async Task<ResumenEntity> GetResumenAsync(DateTime fecha) {
        var fechaAjustada = new DateTime(fecha.Year, fecha.Month, 1);
        var resumen = await dbService.Db.Table<ResumenEntity>().Where(s => s.Fecha == fechaAjustada).FirstOrDefaultAsync();
        return resumen;
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


    public async Task<int> SaveResumenAsync(ResumenEntity resumen) {
        if (resumen is null) return 0;
        if (resumen.Id == 0) {
            await dbService.Db.InsertAsync(resumen);
            var id = await dbService.GetLastIdAsync();
            resumen.Id = id;
        } else {
            await dbService.Db.UpdateAsync(resumen);
        }
        return resumen.Id;
    }


    public async Task SaveResumenesAsync(IEnumerable<ResumenEntity> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await SaveResumenAsync(item);
        }
    }


    public async Task DeleteResumenAsync(int resumenId) {
        await dbService.Db.DeleteAsync<ResumenEntity>(resumenId);
    }


    public async Task DeleteResumenesAsync(IEnumerable<ResumenEntity> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await DeleteResumenAsync(item.Id);
        }
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Opciones
    // ====================================================================================================


    public async Task<OpcionesEntity> GetOpcionesAsync() {
        var opciones = await dbService.Db.Table<OpcionesEntity>().FirstOrDefaultAsync();
        if (opciones is null) opciones = new();
        return opciones;
    }


    public async Task<int> SaveOpcionesAsync(OpcionesEntity opciones) {
        if (opciones is null) return 0;
        if (opciones.Id == 0) {
            await dbService.Db.InsertAsync(opciones);
            var id = await dbService.GetLastIdAsync();
            opciones.Id = id;
        } else {
            await dbService.Db.UpdateAsync(opciones);
        }
        return opciones.Id;
    }


    public async Task SaveOpcionesAsync(IEnumerable<OpcionesEntity> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await SaveOpcionesAsync(item);
        }
    }


    public async Task DeleteOpcionesAsync(int opcionesId) {
        await dbService.Db.DeleteAsync<OpcionesEntity>(opcionesId);
    }


    public async Task DeleteOpcionesAsync(IEnumerable<OpcionesEntity> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await DeleteOpcionesAsync(item.Id);
        }
    }


    #endregion
    // ====================================================================================================


}
