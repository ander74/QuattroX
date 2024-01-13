#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Entities;
using QuattroX.Data.Helpers;
using QuattroX.Data.Model;
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


    //public async Task<List<ServicioSecundarioEntity>> GetServiciosSecundariosAsync(int servicioId) {
    //    var servicios = await dbService.Db.Table<ServicioSecundarioEntity>().Where(s => s.ServicioId == servicioId).OrderBy(t => t.Servicio).ToListAsync();
    //    if (servicios is null) servicios = new();
    //    return servicios;
    //}


    public async Task<ObservableCollection<ServicioSecundarioModel>> GetServiciosSecundariosAsync(int servicioId) {
        var lista = await dbService.Db.Table<ServicioSecundarioEntity>().Where(s => s.ServicioId == servicioId).OrderBy(t => t.Servicio).ToListAsync();
        if (lista is null) lista = new();
        var servicios = lista.Select(s => s.ToServicioSecundarioModel()).ToObservableCollection();
        return servicios;
    }


    public async Task<int> SaveServicioSecundarioAsync(ServicioSecundarioModel servicio) {
        if (servicio is null) return 0;
        if (servicio.Id == 0) {
            await dbService.Db.InsertAsync(servicio.ToServicioSecundarioEntity());
            var id = await dbService.GetLastIdAsync();
            servicio.Id = id;
        } else {
            await dbService.Db.UpdateAsync(servicio.ToServicioSecundarioEntity());
        }
        return servicio.Id;
    }


    public async Task SaveServiciosSecundariosAsync(IEnumerable<ServicioSecundarioModel> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await SaveServicioSecundarioAsync(item);
        }
    }


    public async Task DeleteServicioSecundarioAsync(int servicioId) {
        await dbService.Db.DeleteAsync<ServicioSecundarioEntity>(servicioId);
    }


    public async Task DeleteServiciosSecundariosAsync(IEnumerable<ServicioSecundarioModel> lista) {
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


    public async Task<ObservableCollection<ServicioLineaModel>> GetServiciosLineaAsync(int lineaId, bool incluirSecundarios = true) {
        var lista = await dbService.Db.Table<ServicioLineaEntity>().Where(s => s.LineaId == lineaId).OrderBy(t => t.Servicio).ToListAsync();
        if (lista is null) lista = new();
        var servicios = lista.Select(s => s.ToServicioLineaModel()).ToObservableCollection();
        if (incluirSecundarios) {
            foreach (var servicio in servicios) {
                servicio.Servicios = await GetServiciosSecundariosAsync(servicio.Id);
                if (servicio.Servicios is null) servicio.Servicios = new();
            }
        }
        return servicios;
    }


    public async Task<ServicioLineaModel> GetServicioLineaAsync(string linea, string servicio, int turno) {
        var servicioLinea = await dbService.Db.Table<ServicioLineaEntity>()
            .FirstOrDefaultAsync(s => s.Linea == linea && s.Servicio.ToUpper() == servicio.ToUpper() && s.Turno == turno);
        if (servicioLinea is null) return new ServicioLineaModel();
        var model = servicioLinea.ToServicioLineaModel();
        model.Servicios = await GetServiciosSecundariosAsync(servicioLinea.Id);
        if (model.Servicios is null) model.Servicios = new();
        return model;
    }


    public async Task<int> SaveServicioLineaAsync(ServicioLineaModel servicio) {
        if (servicio is null) return 0;
        if (servicio.Id == 0) {
            await dbService.Db.InsertAsync(servicio.ToServicioLineaEntity());
            var id = await dbService.GetLastIdAsync();
            servicio.Id = id;
        } else {
            await dbService.Db.UpdateAsync(servicio.ToServicioLineaEntity());
        }
        if (servicio.Servicios is not null) {
            foreach (var serv in servicio.Servicios) {
                serv.ServicioId = servicio.Id;
            }
        }
        await SaveServiciosSecundariosAsync(servicio.Servicios);
        return servicio.Id;
    }


    public async Task SaveServiciosLineaAsync(IEnumerable<ServicioLineaModel> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await SaveServicioLineaAsync(item);
        }
    }


    public async Task DeleteServicioLineaAsync(int servicioId) {
        await dbService.Db.DeleteAsync<ServicioLineaEntity>(servicioId);
    }


    public async Task DeleteServiciosLineaAsync(IEnumerable<ServicioLineaModel> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await DeleteServicioLineaAsync(item.Id);
        }
    }


    public async Task<bool> ExisteServicioLineaAsync(string linea, string servicio, int turno) {
        return await dbService.Db.Table<ServicioLineaEntity>()
            .CountAsync(s => s.Linea.ToUpper() == linea.ToUpper() && s.Servicio.ToUpper() == servicio.ToUpper() && s.Turno == turno) > 0;
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Línea
    // ====================================================================================================


    public async Task<ObservableCollection<LineaModel>> GetLineasAsync(bool incluirServicios = true) {
        var lista = await dbService.Db.Table<LineaEntity>().OrderBy(t => t.Linea).ToListAsync();
        if (lista is null) lista = new();
        var lineas = lista.Select(l => l.ToModel()).ToObservableCollection();
        if (incluirServicios) {
            foreach (var linea in lineas) {
                linea.Servicios = await GetServiciosLineaAsync(linea.Id, incluirServicios);
                if (linea.Servicios is null) linea.Servicios = new();
            }
        }
        return lineas;
    }


    public async Task<bool> ExisteLineaAsync(string linea) {
        return await dbService.Db.Table<LineaEntity>().CountAsync(t => t.Linea.ToUpper() == linea.ToUpper()) > 0;
    }


    public async Task<int> SaveLineaAsync(LineaModel linea) {
        if (linea is null) return 0;
        if (linea.Id == 0) {
            await dbService.Db.InsertAsync(linea.ToEntity());
            var id = await dbService.GetLastIdAsync();
            linea.Id = id;
        } else {
            await dbService.Db.UpdateAsync(linea.ToEntity());
        }
        if (linea.Servicios is not null) {
            foreach (var servicio in linea.Servicios) {
                servicio.LineaId = linea.Id;
            }
        }
        await SaveServiciosLineaAsync(linea.Servicios);
        return linea.Id;
    }


    public async Task SaveLineasAsync(IEnumerable<LineaModel> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await SaveLineaAsync(item);
        }
    }


    public async Task DeleteLineaAsync(int lineaId) {
        await dbService.Db.DeleteAsync<LineaEntity>(lineaId);
    }


    public async Task DeleteLineasAsync(IEnumerable<LineaModel> lista) {
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


    public async Task<ObservableCollection<IncidenciaModel>> GetIncidenciasAsync() {
        var lista = await dbService.Db.Table<IncidenciaEntity>().OrderBy(t => t.Codigo).ToListAsync();
        if (lista is null) lista = new();
        var incidencias = lista.Select(i => i.ToModel()).ToObservableCollection();
        return incidencias;
    }


    public async Task<IncidenciaModel> GetIncidenciaByIdAsync(int incidenciaId) {
        var incidencia = await dbService.Db.Table<IncidenciaEntity>().Where(i => i.Id == incidenciaId).FirstOrDefaultAsync();
        if (incidencia is null) incidencia = new();
        return incidencia.ToModel();
    }


    public async Task<int> SaveIncidenciaAsync(IncidenciaModel incidencia) {
        if (incidencia is null) return 0;
        if (incidencia.Id == 0) {
            await dbService.Db.InsertAsync(incidencia.ToEntity());
            var id = await dbService.GetLastIdAsync();
            incidencia.Id = id;
        } else {
            await dbService.Db.UpdateAsync(incidencia.ToEntity());
        }
        return incidencia.Id;
    }


    public async Task SaveIncidenciasAsync(IEnumerable<IncidenciaModel> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await SaveIncidenciaAsync(item);
        }
    }


    public async Task DeleteIncidenciaAsync(int incidenciaId) {
        await dbService.Db.DeleteAsync<IncidenciaEntity>(incidenciaId);
    }


    public async Task DeleteIncidenciasAsync(IEnumerable<IncidenciaModel> lista) {
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


    public async Task<ObservableCollection<TrabajadorModel>> GetTrabajadoresAsync() {
        var lista = await dbService.Db.Table<TrabajadorEntity>().OrderBy(t => t.Matricula).ToListAsync();
        if (lista is null) lista = new();
        var trabajadores = lista.Select(t => t.ToModel()).ToObservableCollection();
        return trabajadores;
    }


    public async Task<TrabajadorModel> GetTrabajadorByIdAsync(int relevoId) {
        var trabajador = await dbService.Db.Table<TrabajadorEntity>().Where(t => t.Id == relevoId).FirstOrDefaultAsync();
        if (trabajador is null) trabajador = new();
        return trabajador.ToModel();
    }


    public async Task<TrabajadorModel> GetTrabajadorByMatriculaAsync(int matricula) {
        var trabajador = await dbService.Db.Table<TrabajadorEntity>().Where(t => t.Matricula == matricula).FirstOrDefaultAsync();
        if (trabajador is null) trabajador = new();
        return trabajador.ToModel();
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


    public async Task<int> SaveTrabajadorAsync(TrabajadorModel trabajador) {
        if (trabajador is null) return 0;
        if (trabajador.Id == 0) {
            await dbService.Db.InsertAsync(trabajador.ToEntity());
            var id = await dbService.GetLastIdAsync();
            trabajador.Id = id;
        } else {
            await dbService.Db.UpdateAsync(trabajador.ToEntity());
        }
        return trabajador.Id;
    }


    public async Task SaveTrabajadoresAsync(IEnumerable<TrabajadorModel> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await SaveTrabajadorAsync(item);
        }
    }


    public async Task DeleteTrabajadorAsync(int trabajadorId) {
        await dbService.Db.DeleteAsync<TrabajadorEntity>(trabajadorId);
    }


    public async Task DeleteTrabajadoresAsync(IEnumerable<TrabajadorModel> lista) {
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


    public async Task<ObservableCollection<ServicioDiaModel>> GetServiciosDiaAsync(int diaId) {
        var lista = await dbService.Db.Table<ServicioDiaEntity>().Where(s => s.DiaId == diaId).OrderBy(t => t.Servicio).ToListAsync();
        if (lista is null) lista = new();
        var servicios = lista.Select(s => s.ToServicioDiaModel()).ToObservableCollection();
        return servicios;
    }


    public async Task<int> SaveServicioDiaAsync(ServicioDiaModel servicio) {
        if (servicio is null) return 0;
        if (servicio.Id == 0) {
            await dbService.Db.InsertAsync(servicio.ToServicioDiaEntity());
            var id = await dbService.GetLastIdAsync();
            servicio.Id = id;
        } else {
            await dbService.Db.UpdateAsync(servicio.ToServicioDiaEntity());
        }
        return servicio.Id;
    }


    public async Task SaveServiciosDiaAsync(IEnumerable<ServicioDiaModel> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await SaveServicioDiaAsync(item);
        }
    }


    public async Task DeleteServicioDiaAsync(int servicioId) {
        await dbService.Db.DeleteAsync<ServicioDiaEntity>(servicioId);
    }


    public async Task DeleteServiciosDiaAsync(IEnumerable<ServicioDiaModel> lista) {
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


    public async Task<ObservableCollection<DiaModel>> GetDiasAsync(DateTime fecha, bool incluirServicios = true) {
        var fechaAnterior = new DateTime(fecha.Year, fecha.Month, 1);
        var fechaPosterior = fecha.AddMonths(1);
        var lista = await dbService.Db.Table<DiaEntity>().Where(d => d.Fecha >= fechaAnterior && d.Fecha < fechaPosterior).OrderBy(t => t.Fecha).ToListAsync();
        if (lista is null) lista = new();
        var dias = lista.Select(d => d.ToModel()).ToObservableCollection();
        if (incluirServicios) {
            foreach (var dia in dias) {
                dia.Servicios = await GetServiciosDiaAsync(dia.Id);
                if (dia.Servicios is null) dia.Servicios = new();
            }
        }
        return dias;
    }



    /// <summary>
    /// Devuelve los días que hay entre las fechas de inicio y final, ambas incluidas.
    /// </summary>
    public async Task<ObservableCollection<DiaModel>> GetDiasAsync(DateTime fechaInicio, DateTime fechaFinal, bool incluirServicios = true) {
        var lista = await dbService.Db.Table<DiaEntity>().Where(d => d.Fecha >= fechaInicio && d.Fecha <= fechaFinal).OrderBy(t => t.Fecha).ToListAsync();
        if (lista is null) lista = new();
        var dias = lista.Select(d => d.ToModel()).ToObservableCollection();
        if (incluirServicios) {
            foreach (var dia in dias) {
                dia.Servicios = await GetServiciosDiaAsync(dia.Id);
                if (dia.Servicios is null) dia.Servicios = new();
            }
        }
        return dias;
    }


    public async Task<DiaModel> GetDiaByIdAsync(int diaId, bool incluirServicios = true) {
        var dia = await dbService.Db.Table<DiaEntity>().Where(d => d.Id == diaId).OrderBy(t => t.Fecha).FirstOrDefaultAsync();
        if (dia is null) dia = new();
        var diaModel = dia.ToModel();
        if (incluirServicios) {
            diaModel.Servicios = await GetServiciosDiaAsync(dia.Id);
            if (diaModel.Servicios is null) diaModel.Servicios = new();
        }
        return diaModel;
    }


    public async Task<int> SaveDiaAsync(DiaModel dia) {
        if (dia is null) return 0;
        if (dia.Id == 0) {
            await dbService.Db.InsertAsync(dia.ToEntity());
            var id = await dbService.GetLastIdAsync();
            dia.Id = id;
        } else {
            await dbService.Db.UpdateAsync(dia.ToEntity());
        }
        if (dia.Servicios is not null) {
            foreach (var servicio in dia.Servicios) {
                servicio.DiaId = dia.Id;
            }
        }
        await SaveServiciosDiaAsync(dia.Servicios);
        return dia.Id;
    }


    public async Task SaveDiasAsync(IEnumerable<DiaModel> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await SaveDiaAsync(item);
        }
    }


    public async Task DeleteDiaAsync(int diaId) {
        await dbService.Db.DeleteAsync<DiaEntity>(diaId);
    }


    public async Task DeleteDiasAsync(IEnumerable<DiaModel> lista) {
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


    public async Task<ObservableCollection<RegulacionModel>> GetRegulacionesByDiaAsync(DateTime fecha) {
        var lista = await dbService.Db.Table<RegulacionEntity>().Where(s => s.Fecha == fecha).ToListAsync();
        if (lista is null) lista = new();
        var regulaciones = lista.Select(r => r.ToModel()).ToObservableCollection();
        return regulaciones;
    }


    public async Task<ObservableCollection<RegulacionModel>> GetRegulacionesByMesAsync(DateTime fecha) {
        var fechaInicio = new DateTime(fecha.Year, fecha.Month, 1);
        var fechaFinal = fechaInicio.AddMonths(1);
        var lista = await dbService.Db.Table<RegulacionEntity>().Where(s => s.Fecha >= fechaInicio && s.Fecha < fechaFinal).ToListAsync();
        if (lista is null) lista = new();
        var regulaciones = lista.Select(r => r.ToModel()).ToObservableCollection();
        return regulaciones;
    }


    public async Task<int> SaveRegulacionAsync(RegulacionModel regulacion) {
        if (regulacion is null) return 0;
        if (regulacion.Id == 0) {
            await dbService.Db.InsertAsync(regulacion.ToEntity());
            var id = await dbService.GetLastIdAsync();
            regulacion.Id = id;
        } else {
            await dbService.Db.UpdateAsync(regulacion.ToEntity());
        }
        return regulacion.Id;
    }


    public async Task SaveRegulacionesAsync(IEnumerable<RegulacionModel> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await SaveRegulacionAsync(item);
        }
    }


    public async Task DeleteRegulacionAsync(int regulacionId) {
        await dbService.Db.DeleteAsync<RegulacionEntity>(regulacionId);
    }


    public async Task DeleteRegulacionesAsync(IEnumerable<RegulacionModel> lista) {
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


    public async Task<ResumenModel> GetResumenAsync(DateTime fecha) {
        var fechaAjustada = new DateTime(fecha.Year, fecha.Month, 1);
        var resumen = await dbService.Db.Table<ResumenEntity>().Where(s => s.Fecha == fechaAjustada).FirstOrDefaultAsync();
        return resumen.ToModel();
    }


    public async Task<ResumenModel> GetResumenHastaFechaAsync(DateTime fecha) {
        var resumenes = await dbService.Db.Table<ResumenEntity>().Where(r => r.Fecha <= fecha).ToListAsync();
        return new ResumenModel {
            Trabajadas = resumenes.Sum(r => r.Trabajadas),
            TrabajadasConvenio = resumenes.Sum(r => r.TrabajadasConvenio),
            Acumuladas = resumenes.Sum(r => r.Acumuladas),
            Nocturnas = resumenes.Sum(r => r.Nocturnas),
            TomaDeje = resumenes.Sum(r => r.TomaDeje),
            Euros = resumenes.Sum(r => r.Euros),
            Regulaciones = resumenes.Sum(r => r.Regulaciones),
        };
    }


    public async Task<int> SaveResumenAsync(ResumenModel resumen) {
        if (resumen is null) return 0;
        if (resumen.Id == 0) {
            await dbService.Db.InsertAsync(resumen.ToEntity());
            var id = await dbService.GetLastIdAsync();
            resumen.Id = id;
        } else {
            await dbService.Db.UpdateAsync(resumen.ToEntity());
        }
        return resumen.Id;
    }


    public async Task SaveResumenesAsync(IEnumerable<ResumenModel> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await SaveResumenAsync(item);
        }
    }


    public async Task DeleteResumenAsync(int resumenId) {
        await dbService.Db.DeleteAsync<ResumenEntity>(resumenId);
    }


    public async Task DeleteResumenesAsync(IEnumerable<ResumenModel> lista) {
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


    public async Task<OpcionesModel> GetOpcionesAsync() {
        var opciones = await dbService.Db.Table<OpcionesEntity>().FirstOrDefaultAsync();
        if (opciones is null) opciones = new();
        return opciones.ToModel();
    }


    public async Task<int> SaveOpcionesAsync(OpcionesModel opciones) {
        if (opciones is null) return 0;
        if (opciones.Id == 0) {
            await dbService.Db.InsertAsync(opciones.ToEntity());
            var id = await dbService.GetLastIdAsync();
            opciones.Id = id;
        } else {
            await dbService.Db.UpdateAsync(opciones.ToEntity());
        }
        return opciones.Id;
    }


    public async Task SaveOpcionesAsync(IEnumerable<OpcionesModel> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await SaveOpcionesAsync(item);
        }
    }


    public async Task DeleteOpcionesAsync(int opcionesId) {
        await dbService.Db.DeleteAsync<OpcionesEntity>(opcionesId);
    }


    public async Task DeleteOpcionesAsync(IEnumerable<OpcionesModel> lista) {
        if (lista is null) return;
        foreach (var item in lista) {
            await DeleteOpcionesAsync(item.Id);
        }
    }


    #endregion
    // ====================================================================================================


}
