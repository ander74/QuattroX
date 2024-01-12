#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Entities;
using QuattroX.Data.Interfaces;
using QuattroX.Data.Model;

namespace QuattroX.Data.Helpers;


public static class Extensions {


    // ====================================================================================================
    #region Línea
    // ====================================================================================================


    public static LineaModel ToModel(this LineaEntity entity) {
        if (entity is null) return null;
        var model = new LineaModel {
            Id = entity.Id,
            Linea = entity.Linea,
            Texto = entity.Texto,
            Servicios = entity.Servicios == null ? new() : entity.Servicios.Select(s => s.ToServicioLineaModel()).ToObservableCollection(),
        };
        model.Modified = false;
        return model;
    }


    public static LineaEntity ToEntity(this LineaModel model) {
        if (model is null) return null;
        var linea = new LineaEntity();
        linea.Id = model.Id;
        linea.Linea = model.Linea;
        linea.Texto = model.Texto;
        linea.Servicios = model.Servicios == null ? new() : model.Servicios.Select(s => s.ToServicioLineaEntity()).ToList();
        return linea;
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region ServicioBase
    // ====================================================================================================


    public static ServicioBaseModel ToServicioBaseModel(this IServicio servicio) {
        if (servicio is null) return null;
        var model = new ServicioBaseModel {
            Linea = servicio.Linea,
            TextoLinea = servicio.TextoLinea,
            Servicio = servicio.Servicio,
            Turno = servicio.Turno,
            Inicio = servicio.Inicio,
            Final = servicio.Final,
            LugarInicio = servicio.LugarInicio,
            LugarFinal = servicio.LugarFinal,
        };
        model.Modified = false;
        return model;
    }


    public static ServicioBaseEntity ToServicioBaseEntity(this IServicio servicio) {
        if (servicio is null) return null;
        var entity = new ServicioBaseEntity {
            Linea = servicio.Linea,
            TextoLinea = servicio.TextoLinea,
            Servicio = servicio.Servicio,
            Turno = servicio.Turno,
            Inicio = servicio.Inicio,
            Final = servicio.Final,
            LugarInicio = servicio.LugarInicio,
            LugarFinal = servicio.LugarFinal,
        };
        return entity;
    }


    public static void FromServicioBase(this IServicio servicio, IServicio newServicio) {
        if (servicio is null || newServicio is null) return;
        servicio.Linea = newServicio.Linea;
        servicio.TextoLinea = newServicio.TextoLinea;
        servicio.Servicio = newServicio.Servicio;
        servicio.Turno = newServicio.Turno;
        servicio.Inicio = newServicio.Inicio;
        servicio.Final = newServicio.Final;
        servicio.LugarInicio = newServicio.LugarInicio;
        servicio.LugarFinal = newServicio.LugarFinal;
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region ServicioLinea
    // ====================================================================================================


    public static ServicioLineaModel ToServicioLineaModel(this ServicioLineaEntity entity) {
        if (entity is null) return null;
        var model = new ServicioLineaModel();
        model.Linea = entity.Linea;
        model.TextoLinea = entity.TextoLinea;
        model.Servicio = entity.Servicio;
        model.Turno = entity.Turno;
        model.Inicio = entity.Inicio;
        model.Final = entity.Final;
        model.LugarInicio = entity.LugarInicio;
        model.LugarFinal = entity.LugarFinal;
        model.Id = entity.Id;
        model.LineaId = entity.LineaId;
        model.Servicios = entity.Servicios == null ? new() : entity.Servicios.Select(s => s.ToServicioSecundarioModel()).ToObservableCollection();
        model.Modified = false;
        return model;
    }


    public static ServicioLineaEntity ToServicioLineaEntity(this ServicioLineaModel model) {
        if (model is null) return null;
        var entity = new ServicioLineaEntity();
        entity.Linea = model.Linea;
        entity.TextoLinea = model.TextoLinea;
        entity.Servicio = model.Servicio;
        entity.Turno = model.Turno;
        entity.Inicio = model.Inicio;
        entity.Final = model.Final;
        entity.LugarInicio = model.LugarInicio;
        entity.LugarFinal = model.LugarFinal;

        entity.Id = model.Id;
        entity.LineaId = model.LineaId;
        entity.Servicios = model.Servicios == null ? new() : model.Servicios.Select(s => s.ToServicioSecundarioEntity()).ToList();
        return entity;
    }


    public static ServicioLineaModel ToServicioLineaModel(this ServicioLineaModel servicioModel) {
        if (servicioModel is null) return null;
        var model = new ServicioLineaModel();
        model.Linea = servicioModel.Linea;
        model.TextoLinea = servicioModel.TextoLinea;
        model.Servicio = servicioModel.Servicio;
        model.Turno = servicioModel.Turno;
        model.Inicio = servicioModel.Inicio;
        model.Final = servicioModel.Final;
        model.LugarInicio = servicioModel.LugarInicio;
        model.LugarFinal = servicioModel.LugarFinal;
        model.Id = servicioModel.Id;
        model.LineaId = servicioModel.LineaId;
        model.Servicios = servicioModel.Servicios == null ? new() : servicioModel.Servicios.Select(s => s.ToServicioSecundarioModel()).ToObservableCollection();
        model.Modified = false;
        return model;
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region ServicioDia
    // ====================================================================================================


    public static ServicioDiaModel ToServicioDiaModel(this ServicioDiaEntity entity) {
        if (entity is null) return null;
        var model = new ServicioDiaModel();
        model.Linea = entity.Linea;
        model.TextoLinea = entity.TextoLinea;
        model.Servicio = entity.Servicio;
        model.Turno = entity.Turno;
        model.Inicio = entity.Inicio;
        model.Final = entity.Final;
        model.LugarInicio = entity.LugarInicio;
        model.LugarFinal = entity.LugarFinal;
        model.Id = entity.Id;
        model.DiaId = entity.DiaId;
        model.Modified = false;
        return model;
    }


    public static ServicioDiaModel ToServicioDiaModel(this ServicioDiaModel model, int diaId = 0) {
        if (model is null) return null;
        var newModel = new ServicioDiaModel();
        newModel.Linea = model.Linea;
        newModel.TextoLinea = model.TextoLinea;
        newModel.Servicio = model.Servicio;
        newModel.Turno = model.Turno;
        newModel.Inicio = model.Inicio;
        newModel.Final = model.Final;
        newModel.LugarInicio = model.LugarInicio;
        newModel.LugarFinal = model.LugarFinal;
        newModel.DiaId = diaId;
        newModel.Modified = false;
        return newModel;
    }


    public static ServicioDiaEntity ToServicioDiaEntity(this ServicioDiaModel model) {
        if (model is null) return null;
        var entity = new ServicioDiaEntity();
        entity.Linea = model.Linea;
        entity.TextoLinea = model.TextoLinea;
        entity.Servicio = model.Servicio;
        entity.Turno = model.Turno;
        entity.Inicio = model.Inicio;
        entity.Final = model.Final;
        entity.LugarInicio = model.LugarInicio;
        entity.LugarFinal = model.LugarFinal;
        entity.Id = model.Id;
        entity.DiaId = model.DiaId;
        return entity;
    }


    public static ServicioSecundarioModel ToServicioSecundarioModel(this ServicioDiaModel servicioModel) {
        if (servicioModel is null) return null;
        var model = new ServicioSecundarioModel();
        model.Linea = servicioModel.Linea;
        model.TextoLinea = servicioModel.TextoLinea;
        model.Servicio = servicioModel.Servicio;
        model.Turno = servicioModel.Turno;
        model.Inicio = servicioModel.Inicio;
        model.Final = servicioModel.Final;
        model.LugarInicio = servicioModel.LugarInicio;
        model.LugarFinal = servicioModel.LugarFinal;
        model.Id = servicioModel.Id;
        model.Modified = false;
        return model;
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region ServicioSecundario
    // ====================================================================================================


    public static ServicioSecundarioModel ToServicioSecundarioModel(this ServicioSecundarioEntity entity) {
        if (entity is null) return null;
        var model = new ServicioSecundarioModel();
        model.Linea = entity.Linea;
        model.TextoLinea = entity.TextoLinea;
        model.Servicio = entity.Servicio;
        model.Turno = entity.Turno;
        model.Inicio = entity.Inicio;
        model.Final = entity.Final;
        model.LugarInicio = entity.LugarInicio;
        model.LugarFinal = entity.LugarFinal;
        model.Id = entity.Id;
        model.ServicioId = entity.ServicioId;
        model.Modified = false;
        return model;
    }


    public static ServicioSecundarioEntity ToServicioSecundarioEntity(this ServicioSecundarioModel model) {
        if (model is null) return null;
        var entity = new ServicioSecundarioEntity();
        entity.Linea = model.Linea;
        entity.TextoLinea = model.TextoLinea;
        entity.Servicio = model.Servicio;
        entity.Turno = model.Turno;
        entity.Inicio = model.Inicio;
        entity.Final = model.Final;
        entity.LugarInicio = model.LugarInicio;
        entity.LugarFinal = model.LugarFinal;
        entity.Id = model.Id;
        entity.ServicioId = model.ServicioId;
        return entity;
    }


    public static ServicioSecundarioModel ToServicioSecundarioModel(this ServicioSecundarioModel servicioModel) {
        if (servicioModel is null) return null;
        var model = new ServicioSecundarioModel();
        model.Linea = servicioModel.Linea;
        model.TextoLinea = servicioModel.TextoLinea;
        model.Servicio = servicioModel.Servicio;
        model.Turno = servicioModel.Turno;
        model.Inicio = servicioModel.Inicio;
        model.Final = servicioModel.Final;
        model.LugarInicio = servicioModel.LugarInicio;
        model.LugarFinal = servicioModel.LugarFinal;
        model.Id = servicioModel.Id;
        model.ServicioId = servicioModel.ServicioId;
        //model.Modified = false;
        return model;
    }


    /// <summary>
    /// Devuelve un nuevo <see cref="ServicioDiaModel"/> con los datos del <see cref="ServicioSecundarioModel"/>, SIN INCLUIR Id e INCUYENDO el DiaId proporcionado.
    /// </summary>
    public static ServicioDiaModel ToServicioDiaModel(this ServicioSecundarioModel secundarioModel, int diaId = 0) {
        if (secundarioModel is null) return null;
        var diaModel = new ServicioDiaModel();
        diaModel.DiaId = diaId;
        diaModel.Linea = secundarioModel.Linea;
        diaModel.TextoLinea = secundarioModel.TextoLinea;
        diaModel.Servicio = secundarioModel.Servicio;
        diaModel.Turno = secundarioModel.Turno;
        diaModel.Inicio = secundarioModel.Inicio;
        diaModel.Final = secundarioModel.Final;
        diaModel.LugarInicio = secundarioModel.LugarInicio;
        diaModel.LugarFinal = secundarioModel.LugarFinal;
        //diaModel.Modified = false;
        return diaModel;
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Incidencia
    // ====================================================================================================


    public static IncidenciaModel ToModel(this IncidenciaEntity entity) {
        if (entity is null) return null;
        var model = new IncidenciaModel {
            Id = entity.Id,
            Tipo = entity.Tipo,
            Codigo = entity.Codigo,
            Descripcion = entity.Descripcion,
            Comportamiento = entity.Comportamiento,
        };
        model.Modified = false;
        return model;
    }


    public static IncidenciaEntity ToEntity(this IncidenciaModel model) {
        if (model is null) return null;
        return new IncidenciaEntity {
            Id = model.Id,
            Tipo = model.Tipo,
            Codigo = model.Codigo,
            Descripcion = model.Descripcion,
            Comportamiento = model.Comportamiento,
        };
    }


    public static void FromModel(this IncidenciaModel model, IncidenciaModel newModel) {
        if (model is null || newModel is null) return;
        model.Id = newModel.Id;
        model.Tipo = newModel.Tipo;
        model.Codigo = newModel.Codigo;
        model.Descripcion = newModel.Descripcion;
        model.Comportamiento = newModel.Comportamiento;
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Regulacion
    // ====================================================================================================


    public static RegulacionModel ToModel(this RegulacionEntity entity) {
        if (entity is null) return null;
        var model = new RegulacionModel {
            Id = entity.Id,
            Fecha = entity.Fecha,
            Tipo = entity.Tipo,
            Horas = entity.Horas,
            Motivo = entity.Motivo,
        };
        model.Modified = false;
        return model;
    }


    public static RegulacionEntity ToEntity(this RegulacionModel model) {
        if (model is null) return null;
        return new RegulacionEntity {
            Id = model.Id,
            Fecha = model.Fecha,
            Tipo = model.Tipo,
            Horas = model.Horas,
            Motivo = model.Motivo,
        };
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Dia
    // ====================================================================================================


    /// <summary>
    /// Hace una copia exacta de <see cref="DiaEntity"/> en un <see cref="DiaModel"/> INCLUYENDO el Id.
    /// </summary>
    public static DiaModel ToModel(this DiaEntity entity) {
        if (entity is null) return null;
        var model = new DiaModel {
            Id = entity.Id,
            Fecha = entity.Fecha,
            EsFranqueo = entity.EsFranqueo,
            EsFestivo = entity.EsFestivo,
            IncidenciaId = entity.IncidenciaId,
            Linea = entity.Linea,
            TextoLinea = entity.TextoLinea,
            Servicio = entity.Servicio,
            Turno = entity.Turno,
            Inicio = entity.Inicio,
            Final = entity.Final,
            LugarInicio = entity.LugarInicio,
            LugarFinal = entity.LugarFinal,
            Servicios = entity.Servicios is null ? new() : entity.Servicios.Select(s => s.ToServicioDiaModel()).ToObservableCollection(),
            Trabajadas = entity.Trabajadas,
            Acumuladas = entity.Acumuladas,
            Nocturnas = entity.Nocturnas,
            Desayuno = entity.Desayuno,
            Comida = entity.Comida,
            Cena = entity.Cena,
            TomaDeje = entity.TomaDeje,
            Euros = entity.Euros,
            HuelgaParcial = entity.HuelgaParcial,
            HorasHuelga = entity.HorasHuelga,
            RelevoId = entity.RelevoId,
            SustiId = entity.SustiId,
            Bus = entity.Bus,
            Notas = entity.Notas,
        };
        model.Modified = false;
        return model;
    }


    /// <summary>
    /// Hace una copia exacta de <see cref="DiaModel"/> en un <see cref="DiaEntity"/> INCUYENDO el Id.
    /// </summary>
    public static DiaEntity ToEntity(this DiaModel model) {
        if (model is null) return null;
        return new DiaEntity {
            Id = model.Id,
            Fecha = model.Fecha,
            EsFranqueo = model.EsFranqueo,
            EsFestivo = model.EsFestivo,
            IncidenciaId = model.IncidenciaId,
            Linea = model.Linea,
            TextoLinea = model.TextoLinea,
            Servicio = model.Servicio,
            Turno = model.Turno,
            Inicio = model.Inicio,
            Final = model.Final,
            LugarInicio = model.LugarInicio,
            LugarFinal = model.LugarFinal,
            Servicios = model.Servicios is null ? new() : model.Servicios.Select(s => s.ToServicioDiaEntity()).ToList(),
            Trabajadas = model.Trabajadas,
            Acumuladas = model.Acumuladas,
            Nocturnas = model.Nocturnas,
            Desayuno = model.Desayuno,
            Comida = model.Comida,
            Cena = model.Cena,
            TomaDeje = model.TomaDeje,
            Euros = model.Euros,
            HuelgaParcial = model.HuelgaParcial,
            HorasHuelga = model.HorasHuelga,
            RelevoId = model.RelevoId,
            SustiId = model.SustiId,
            Bus = model.Bus,
            Notas = model.Notas,
        };
    }


    /// <summary>
    /// Copia todos los valores del <see cref="DiaEntity"/> en el <see cref="DiaModel"/> que lo llama, SIN INCLUIR el Id.
    /// </summary>
    public static void FromEntity(this DiaModel model, DiaEntity entity) {
        if (model is null || entity is null) return;
        model.Fecha = entity.Fecha;
        model.EsFranqueo = entity.EsFranqueo;
        model.EsFestivo = entity.EsFestivo;
        model.IncidenciaId = entity.IncidenciaId;
        model.Linea = entity.Linea;
        model.TextoLinea = entity.TextoLinea;
        model.Servicio = entity.Servicio;
        model.Turno = entity.Turno;
        model.Inicio = entity.Inicio;
        model.Final = entity.Final;
        model.LugarInicio = entity.LugarInicio;
        model.LugarFinal = entity.LugarFinal;
        model.Servicios = entity.Servicios is null ? new() : entity.Servicios.Select(s => s.ToServicioDiaModel()).ToObservableCollection();
        model.Trabajadas = entity.Trabajadas;
        model.Acumuladas = entity.Acumuladas;
        model.Nocturnas = entity.Nocturnas;
        model.Desayuno = entity.Desayuno;
        model.Comida = entity.Comida;
        model.Cena = entity.Cena;
        model.TomaDeje = entity.TomaDeje;
        model.Euros = entity.Euros;
        model.HuelgaParcial = entity.HuelgaParcial;
        model.HorasHuelga = entity.HorasHuelga;
        model.RelevoId = entity.RelevoId;
        model.SustiId = entity.SustiId;
        model.Bus = entity.Bus;
        model.Notas = entity.Notas;
    }


    /// <summary>
    /// Copia todos los valores del <see cref="DiaModel"/> en el <see cref="DiaEntity"/> que lo llama, SIN INCLUIR el Id.
    /// </summary>
    public static void FromModel(this DiaModel model, DiaModel newModel) {
        if (model is null || newModel is null) return;
        model.Fecha = newModel.Fecha;
        model.EsFranqueo = newModel.EsFranqueo;
        model.EsFestivo = newModel.EsFestivo;
        model.IncidenciaId = newModel.IncidenciaId;
        model.Linea = newModel.Linea;
        model.TextoLinea = newModel.TextoLinea;
        model.Servicio = newModel.Servicio;
        model.Turno = newModel.Turno;
        model.Inicio = newModel.Inicio;
        model.Final = newModel.Final;
        model.LugarInicio = newModel.LugarInicio;
        model.LugarFinal = newModel.LugarFinal;
        model.Servicios = newModel.Servicios is null ? new() : newModel.Servicios.Select(s => s.ToServicioDiaModel(model.Id)).ToObservableCollection();
        model.Trabajadas = newModel.Trabajadas;
        model.Acumuladas = newModel.Acumuladas;
        model.Nocturnas = newModel.Nocturnas;
        model.Desayuno = newModel.Desayuno;
        model.Comida = newModel.Comida;
        model.Cena = newModel.Cena;
        model.TomaDeje = newModel.TomaDeje;
        model.Euros = newModel.Euros;
        model.HuelgaParcial = newModel.HuelgaParcial;
        model.HorasHuelga = newModel.HorasHuelga;
        model.RelevoId = newModel.RelevoId;
        model.SustiId = newModel.SustiId;
        model.Bus = newModel.Bus;
        model.Notas = newModel.Notas;
        model.Matricula = newModel.Matricula;
        model.Apellidos = newModel.Apellidos;
    }


    /// <summary>
    /// Copia SÓLO LOS DATOS DE SERVICIO del <see cref="ServicioBaseEntity"/> en el <see cref="DiaModel"/> que lo llama.
    /// </summary>
    public static void FromServicioEntity(this DiaModel model, ServicioBaseEntity entity) {
        if (model is null || entity is null) return;
        model.Linea = entity.Linea;
        model.TextoLinea = entity.TextoLinea;
        model.Servicio = entity.Servicio;
        model.Turno = entity.Turno;
        model.Inicio = entity.Inicio;
        model.Final = entity.Final;
        model.LugarInicio = entity.LugarInicio;
        model.LugarFinal = entity.LugarFinal;
    }


    /// <summary>
    /// Copia SÓLO LOS DATOS DE SERVICIO del <see cref="ServicioBaseModel"/> en el <see cref="DiaEntity"/> que lo llama.
    /// </summary>
    public static void FromServicioModel(this DiaEntity entity, ServicioBaseModel model) {
        if (entity is null || model is null) return;
        entity.Linea = model.Linea;
        entity.TextoLinea = model.TextoLinea;
        entity.Servicio = model.Servicio;
        entity.Turno = model.Turno;
        entity.Inicio = model.Inicio;
        entity.Final = model.Final;
        entity.LugarInicio = model.LugarInicio;
        entity.LugarFinal = model.LugarFinal;
    }


    /// <summary>
    /// Copia SÓLO LOS DATOS DE SERVICIO (incluyendo los servicios secundarios) del <see cref="ServicioLineaModel"/> en el <see cref="DiaEntity"/> que lo llama.
    /// </summary>
    public static void FromServicioLineaModel(this DiaModel diaModel, ServicioLineaModel servicioModel) {
        if (diaModel is null || servicioModel is null) return;
        diaModel.Linea = servicioModel.Linea;
        diaModel.TextoLinea = servicioModel.TextoLinea;
        diaModel.Servicio = servicioModel.Servicio;
        diaModel.Turno = servicioModel.Turno;
        diaModel.Inicio = servicioModel.Inicio;
        diaModel.Final = servicioModel.Final;
        diaModel.LugarInicio = servicioModel.LugarInicio;
        diaModel.LugarFinal = servicioModel.LugarFinal;
        diaModel.Servicios = new();
        if (servicioModel.Servicios is not null && servicioModel.Servicios.Count > 0) {
            diaModel.Servicios = servicioModel.Servicios.Select(s => s.ToServicioDiaModel(diaModel.Id)).ToObservableCollection();
        }
    }


    /// <summary>
    /// Copia SÓLO LOS DATOS DE SERVICIO (incluyendo los servicios secundarios) del <see cref="ServicioLineaModel"/> en el <see cref="DiaEntity"/> que lo llama.
    /// </summary>
    public static ServicioLineaModel ToServicioLineaModel(this DiaModel diaModel) {
        if (diaModel is null) return null;
        var servicioModel = new ServicioLineaModel() {
            Linea = diaModel.Linea,
            TextoLinea = diaModel.TextoLinea,
            Servicio = diaModel.Servicio,
            Turno = diaModel.Turno,
            Inicio = diaModel.Inicio,
            Final = diaModel.Final,
            LugarInicio = diaModel.LugarInicio,
            LugarFinal = diaModel.LugarFinal,
        };
        servicioModel.Servicios = new();
        if (diaModel.Servicios is not null && diaModel.Servicios.Count > 0) {
            servicioModel.Servicios = diaModel.Servicios.Select(s => s.ToServicioSecundarioModel()).ToObservableCollection();
        }
        return servicioModel;
    }


    /// <summary>
    /// Pone a cero todos los valores del día, MANTENIENDO INTACTOS Id y Fecha.
    /// </summary>
    public static void Vaciar(this DiaModel model) {
        if (model is null) return;
        model.EsFranqueo = false;
        model.EsFestivo = false;
        model.IncidenciaId = 0;
        model.Incidencia = new();
        model.Linea = string.Empty;
        model.TextoLinea = string.Empty;
        model.Servicio = string.Empty;
        model.Turno = 0;
        model.Inicio = TimeSpan.Zero;
        model.Final = TimeSpan.Zero;
        model.LugarInicio = string.Empty;
        model.LugarFinal = string.Empty;
        model.Servicios = new();
        model.Trabajadas = 0m;
        model.Acumuladas = 0m;
        model.Nocturnas = 0m;
        model.Desayuno = false;
        model.Comida = false;
        model.Cena = false;
        model.TomaDeje = 0m;
        model.Euros = 0m;
        model.HuelgaParcial = false;
        model.HorasHuelga = 0m;
        model.RelevoId = 0;
        model.SustiId = 0;
        model.Bus = string.Empty;
        model.Notas = string.Empty;
        model.Apellidos = string.Empty;
        model.Matricula = 0;

    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Resumen
    // ====================================================================================================


    public static ResumenModel ToModel(this ResumenEntity entity) {
        if (entity is null) return null;
        var model = new ResumenModel {
            Id = entity.Id,
            Fecha = entity.Fecha,
            Trabajadas = entity.Trabajadas,
            TrabajadasConvenio = entity.TrabajadasConvenio,
            Acumuladas = entity.Acumuladas,
            Nocturnas = entity.Nocturnas,
            TomaDeje = entity.TomaDeje,
            Euros = entity.Euros,
            Regulaciones = entity.Regulaciones,
        };
        model.Modified = false;
        return model;
    }


    public static ResumenEntity ToEntity(this ResumenModel model) {
        if (model is null) return null;
        return new ResumenEntity {
            Id = model.Id,
            Fecha = model.Fecha,
            Trabajadas = model.Trabajadas,
            TrabajadasConvenio = model.TrabajadasConvenio,
            Acumuladas = model.Acumuladas,
            Nocturnas = model.Nocturnas,
            TomaDeje = model.TomaDeje,
            Euros = model.Euros,
            Regulaciones = model.Regulaciones,
        };
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Trabajador
    // ====================================================================================================


    public static TrabajadorModel ToModel(this TrabajadorEntity entity) {
        if (entity is null) return null;
        var model = new TrabajadorModel {
            Id = entity.Id,
            Matricula = entity.Matricula,
            Nombre = entity.Nombre,
            Apellidos = entity.Apellidos,
            Telefono = entity.Telefono,
            Email = entity.Email,
            Calificacion = entity.Calificacion,
            DeudaInicial = entity.DeudaInicial,
            Notas = entity.Notas,
        };
        model.Modified = false;
        return model;
    }


    public static TrabajadorEntity ToEntity(this TrabajadorModel model) {
        if (model is null) return null;
        return new TrabajadorEntity {
            Id = model.Id,
            Matricula = model.Matricula,
            Nombre = model.Nombre,
            Apellidos = model.Apellidos,
            Telefono = model.Telefono,
            Email = model.Email,
            Calificacion = model.Calificacion,
            DeudaInicial = model.DeudaInicial,
            Notas = model.Notas,
        };
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Opciones
    // ====================================================================================================


    public static OpcionesModel ToModel(this OpcionesEntity entity) {
        if (entity is null) return null;
        var model = new OpcionesModel {
            Id = entity.Id,
            PrimerMesMostrado = entity.PrimerMesMostrado,
            AcumuladasAnteriores = entity.AcumuladasAnteriores,
            RelevoFijo = entity.RelevoFijo,
            RellenarSemanaAutomaticamente = entity.RellenarSemanaAutomaticamente,
            JornadaMedia = entity.JornadaMedia,
            JornadaMinima = entity.JornadaMinima,
            JornadaAnual = entity.JornadaAnual,
            LimiteEntreServicios = entity.LimiteEntreServicios,
            RegularJornadaAnual = entity.RegularJornadaAnual,
            RegularAñosBisiestos = entity.RegularAñosBisiestos,
            InicioNocturnas = entity.InicioNocturnas,
            FinalNocturnas = entity.FinalNocturnas,
            HoraLimiteDesayuno = entity.HoraLimiteDesayuno,
            HoraLimiteComida1 = entity.HoraLimiteComida1,
            HoraLimiteComida2 = entity.HoraLimiteComida2,
            HoraLimiteCena = entity.HoraLimiteCena,
            InferirTurnos = entity.InferirTurnos,
            FechaReferenciaTurnos = entity.FechaReferenciaTurnos,
            AcumularTomaDeje = entity.AcumularTomaDeje,
        };
        model.Modified = false;
        return model;
    }


    public static OpcionesEntity ToEntity(this OpcionesModel model) {
        if (model is null) return null;
        return new OpcionesEntity {
            Id = model.Id,
            PrimerMesMostrado = model.PrimerMesMostrado,
            AcumuladasAnteriores = model.AcumuladasAnteriores,
            RelevoFijo = model.RelevoFijo,
            RellenarSemanaAutomaticamente = model.RellenarSemanaAutomaticamente,
            JornadaMedia = model.JornadaMedia,
            JornadaMinima = model.JornadaMinima,
            JornadaAnual = model.JornadaAnual,
            LimiteEntreServicios = model.LimiteEntreServicios,
            RegularJornadaAnual = model.RegularJornadaAnual,
            RegularAñosBisiestos = model.RegularAñosBisiestos,
            InicioNocturnas = model.InicioNocturnas,
            FinalNocturnas = model.FinalNocturnas,
            HoraLimiteDesayuno = model.HoraLimiteDesayuno,
            HoraLimiteComida1 = model.HoraLimiteComida1,
            HoraLimiteComida2 = model.HoraLimiteComida2,
            HoraLimiteCena = model.HoraLimiteCena,
            InferirTurnos = model.InferirTurnos,
            FechaReferenciaTurnos = model.FechaReferenciaTurnos,
            AcumularTomaDeje = model.AcumularTomaDeje,
        };
    }


    #endregion
    // ====================================================================================================




}
