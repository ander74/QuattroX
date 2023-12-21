#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Entities;
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
            Servicios = entity.Servicios == null ? new() : entity.Servicios.ToModelObservable(),
        };
        model.Modified = false;
        return model;
    }


    public static LineaEntity ToEntity(this LineaModel model) {
        if (model is null) return null;
        return new LineaEntity {
            Id = model.Id,
            Linea = model.Linea,
            Texto = model.Texto,
            Servicios = model.Servicios == null ? new() : model.Servicios.ToEntity(),
        };
    }


    public static List<LineaModel> ToModel(this IEnumerable<LineaEntity> lista) {
        return lista.Select(item => item.ToModel()).ToList();
    }


    public static ObservableCollection<LineaModel> ToModelObservable(this IEnumerable<LineaEntity> lista) {
        return lista.Select(item => item.ToModel()).ToObservableCollection();
    }


    public static List<LineaEntity> ToEntity(this IEnumerable<LineaModel> lista) {
        return lista.Select(item => item.ToEntity()).ToList();
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region ServicioBase
    // ====================================================================================================


    public static ServicioBaseModel ToModel(this ServicioBaseEntity entity) {
        if (entity is null) return null;
        var model = new ServicioBaseModel {
            Id = entity.Id,
            Linea = entity.Linea,
            TextoLinea = entity.TextoLinea,
            Servicio = entity.Servicio,
            Turno = entity.Turno,
            Inicio = entity.Inicio,
            Final = entity.Final,
            LugarInicio = entity.LugarInicio,
            LugarFinal = entity.LugarFinal,
        };
        model.Modified = false;
        return model;
    }


    public static ServicioBaseEntity ToEntity(this ServicioBaseModel model) {
        if (model is null) return null;
        return new ServicioBaseEntity {
            Id = model.Id,
            Linea = model.Linea,
            TextoLinea = model.TextoLinea,
            Servicio = model.Servicio,
            Turno = model.Turno,
            Inicio = model.Inicio,
            Final = model.Final,
            LugarInicio = model.LugarInicio,
            LugarFinal = model.LugarFinal,
        };
    }


    public static void FromEntity(this ServicioBaseModel model, ServicioBaseEntity entity) {
        if (model is null || entity is null) return;
        model.Id = entity.Id;
        model.Linea = entity.Linea;
        model.TextoLinea = entity.TextoLinea;
        model.Servicio = entity.Servicio;
        model.Turno = entity.Turno;
        model.Inicio = entity.Inicio;
        model.Final = entity.Final;
        model.LugarInicio = entity.LugarInicio;
        model.LugarFinal = entity.LugarFinal;
    }


    public static void FromModel(this ServicioBaseEntity entity, ServicioBaseModel model) {
        if (entity is null || model is null) return;
        entity.Id = model.Id;
        entity.Linea = model.Linea;
        entity.TextoLinea = model.TextoLinea;
        entity.Servicio = model.Servicio;
        entity.Turno = model.Turno;
        entity.Inicio = model.Inicio;
        entity.Final = model.Final;
        entity.LugarInicio = model.LugarInicio;
        entity.LugarFinal = model.LugarFinal;
    }


    public static List<ServicioBaseModel> ToModel(this IEnumerable<ServicioBaseEntity> lista) {
        return lista.Select(item => item.ToModel()).ToList();
    }


    public static ObservableCollection<ServicioBaseModel> ToModelObservable(this IEnumerable<ServicioBaseEntity> lista) {
        return lista.Select(item => item.ToModel()).ToObservableCollection();
    }


    public static List<ServicioBaseEntity> ToEntity(this IEnumerable<ServicioBaseModel> lista) {
        return lista.Select(item => item.ToEntity()).ToList();
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region ServicioLinea
    // ====================================================================================================


    public static ServicioLineaModel ToModel(this ServicioLineaEntity entity) {
        if (entity is null) return null;
        var model = new ServicioLineaModel {
            Id = entity.Id,
            LineaId = entity.LineaId,
            Linea = entity.Linea,
            TextoLinea = entity.TextoLinea,
            Servicio = entity.Servicio,
            Turno = entity.Turno,
            Inicio = entity.Inicio,
            Final = entity.Final,
            LugarInicio = entity.LugarInicio,
            LugarFinal = entity.LugarFinal,
            Servicios = entity.Servicios == null ? new() : entity.Servicios.ToModelObservable(),
        };
        model.Modified = false;
        return model;
    }


    public static ServicioLineaEntity ToEntity(this ServicioLineaModel model) {
        if (model is null) return null;
        return new ServicioLineaEntity {
            Id = model.Id,
            LineaId = model.LineaId,
            Linea = model.Linea,
            TextoLinea = model.TextoLinea,
            Servicio = model.Servicio,
            Turno = model.Turno,
            Inicio = model.Inicio,
            Final = model.Final,
            LugarInicio = model.LugarInicio,
            LugarFinal = model.LugarFinal,
            Servicios = model.Servicios == null ? new() : model.Servicios.ToEntity(),
        };
    }


    public static List<ServicioLineaModel> ToModel(this IEnumerable<ServicioLineaEntity> lista) {
        return lista.Select(item => item.ToModel()).ToList();
    }


    public static ObservableCollection<ServicioLineaModel> ToModelObservable(this IEnumerable<ServicioLineaEntity> lista) {
        return lista.Select(item => item.ToModel()).ToObservableCollection();
    }


    public static List<ServicioLineaEntity> ToEntity(this IEnumerable<ServicioLineaModel> lista) {
        return lista.Select(item => item.ToEntity()).ToList();
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region ServicioDia
    // ====================================================================================================


    public static ServicioDiaModel ToModel(this ServicioDiaEntity entity) {
        if (entity is null) return null;
        var model = new ServicioDiaModel {
            Id = entity.Id,
            DiaId = entity.DiaId,
            Linea = entity.Linea,
            TextoLinea = entity.TextoLinea,
            Servicio = entity.Servicio,
            Turno = entity.Turno,
            Inicio = entity.Inicio,
            Final = entity.Final,
            LugarInicio = entity.LugarInicio,
            LugarFinal = entity.LugarFinal,
        };
        model.Modified = false;
        return model;
    }


    public static ServicioDiaEntity ToEntity(this ServicioDiaModel model) {
        if (model is null) return null;
        return new ServicioDiaEntity {
            Id = model.Id,
            DiaId = model.DiaId,
            Linea = model.Linea,
            TextoLinea = model.TextoLinea,
            Servicio = model.Servicio,
            Turno = model.Turno,
            Inicio = model.Inicio,
            Final = model.Final,
            LugarInicio = model.LugarInicio,
            LugarFinal = model.LugarFinal,
        };
    }


    public static List<ServicioDiaModel> ToModel(this IEnumerable<ServicioDiaEntity> lista) {
        return lista.Select(item => item.ToModel()).ToList();
    }


    public static ObservableCollection<ServicioDiaModel> ToModelObservable(this IEnumerable<ServicioDiaEntity> lista) {
        return lista.Select(item => item.ToModel()).ToObservableCollection();
    }


    public static List<ServicioDiaEntity> ToEntity(this IEnumerable<ServicioDiaModel> lista) {
        return lista.Select(item => item.ToEntity()).ToList();
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region ServicioSecundario
    // ====================================================================================================


    public static ServicioSecundarioModel ToModel(this ServicioSecundarioEntity entity) {
        if (entity is null) return null;
        var model = new ServicioSecundarioModel {
            Id = entity.Id,
            ServicioId = entity.ServicioId,
            Linea = entity.Linea,
            TextoLinea = entity.TextoLinea,
            Servicio = entity.Servicio,
            Turno = entity.Turno,
            Inicio = entity.Inicio,
            Final = entity.Final,
            LugarInicio = entity.LugarInicio,
            LugarFinal = entity.LugarFinal,
        };
        model.Modified = false;
        return model;
    }


    public static ServicioSecundarioEntity ToEntity(this ServicioSecundarioModel model) {
        if (model is null) return null;
        return new ServicioSecundarioEntity {
            Id = model.Id,
            ServicioId = model.ServicioId,
            Linea = model.Linea,
            TextoLinea = model.TextoLinea,
            Servicio = model.Servicio,
            Turno = model.Turno,
            Inicio = model.Inicio,
            Final = model.Final,
            LugarInicio = model.LugarInicio,
            LugarFinal = model.LugarFinal,
        };
    }


    public static List<ServicioSecundarioModel> ToModel(this IEnumerable<ServicioSecundarioEntity> lista) {
        return lista.Select(item => item.ToModel()).ToList();
    }


    public static ObservableCollection<ServicioSecundarioModel> ToModelObservable(this IEnumerable<ServicioSecundarioEntity> lista) {
        return lista.Select(item => item.ToModel()).ToObservableCollection();
    }


    public static List<ServicioSecundarioEntity> ToEntity(this IEnumerable<ServicioSecundarioModel> lista) {
        return lista.Select(item => item.ToEntity()).ToList();
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
        };
    }


    public static void FromModel(this IncidenciaModel model, IncidenciaModel newModel) {
        if (model is null || newModel is null) return;
        model.Id = newModel.Id;
        model.Tipo = newModel.Tipo;
        model.Codigo = newModel.Codigo;
        model.Descripcion = newModel.Descripcion;
    }


    public static List<IncidenciaModel> ToModel(this IEnumerable<IncidenciaEntity> lista) {
        return lista.Select(item => item.ToModel()).ToList();
    }


    public static ObservableCollection<IncidenciaModel> ToModelObservable(this IEnumerable<IncidenciaEntity> lista) {
        return lista.Select(item => item.ToModel()).ToObservableCollection();
    }


    public static List<IncidenciaEntity> ToEntity(this IEnumerable<IncidenciaModel> lista) {
        return lista.Select(item => item.ToEntity()).ToList();
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


    public static List<RegulacionModel> ToModel(this IEnumerable<RegulacionEntity> lista) {
        return lista.Select(item => item.ToModel()).ToList();
    }


    public static ObservableCollection<RegulacionModel> ToModelObservable(this IEnumerable<RegulacionEntity> lista) {
        return lista.Select(item => item.ToModel()).ToObservableCollection();
    }


    public static List<RegulacionEntity> ToEntity(this IEnumerable<RegulacionModel> lista) {
        return lista.Select(item => item.ToEntity()).ToList();
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Dia
    // ====================================================================================================


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
            Servicios = entity.Servicios is null ? new() : entity.Servicios.ToModelObservable(),
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
            Servicios = model.Servicios is null ? new() : model.Servicios.ToEntity(),
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


    public static void FromEntity(this DiaModel model, DiaEntity entity) {
        if (model is null || entity is null) return;
        model.Id = entity.Id;
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
        model.Servicios = entity.Servicios is null ? new() : entity.Servicios.ToModelObservable();
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


    public static void FromModel(this DiaModel model, DiaModel newModel) {
        if (model is null || newModel is null) return;
        model.Id = newModel.Id;
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
        model.Servicios = newModel.Servicios is null ? new() : newModel.Servicios.ToObservableCollection();
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


    public static void FromServicioEntity(this DiaModel model, ServicioBaseEntity entity) {
        if (model is null || entity is null) return;
        model.Id = entity.Id;
        model.Linea = entity.Linea;
        model.TextoLinea = entity.TextoLinea;
        model.Servicio = entity.Servicio;
        model.Turno = entity.Turno;
        model.Inicio = entity.Inicio;
        model.Final = entity.Final;
        model.LugarInicio = entity.LugarInicio;
        model.LugarFinal = entity.LugarFinal;
    }


    public static void FromServicioModel(this DiaEntity entity, ServicioBaseModel model) {
        if (entity is null || model is null) return;
        entity.Id = model.Id;
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
    /// Pone a cero todos los valores del día, manteniendo Id y Fecha intactas.
    /// </summary>
    public static void Vaciar(this DiaModel model) {
        if (model is null) return;
        model.EsFranqueo = false;
        model.EsFestivo = false;
        model.IncidenciaId = 0;
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

    }


    public static List<DiaModel> ToModel(this IEnumerable<DiaEntity> lista) {
        return lista.Select(item => item.ToModel()).ToList();
    }


    public static ObservableCollection<DiaModel> ToModelObservable(this IEnumerable<DiaEntity> lista) {
        return lista.Select(item => item.ToModel()).ToObservableCollection();
    }


    public static List<DiaEntity> ToEntity(this IEnumerable<DiaModel> lista) {
        return lista.Select(item => item.ToEntity()).ToList();
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


    public static List<ResumenModel> ToModel(this IEnumerable<ResumenEntity> lista) {
        return lista.Select(item => item.ToModel()).ToList();
    }


    public static ObservableCollection<ResumenModel> ToModelObservable(this IEnumerable<ResumenEntity> lista) {
        return lista.Select(item => item.ToModel()).ToObservableCollection();
    }


    public static List<ResumenEntity> ToEntity(this IEnumerable<ResumenModel> lista) {
        return lista.Select(item => item.ToEntity()).ToList();
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


    public static List<TrabajadorModel> ToModel(this IEnumerable<TrabajadorEntity> lista) {
        return lista.Select(item => item.ToModel()).ToList();
    }


    public static ObservableCollection<TrabajadorModel> ToModelObservable(this IEnumerable<TrabajadorEntity> lista) {
        return lista.Select(item => item.ToModel()).ToObservableCollection();
    }


    public static List<TrabajadorEntity> ToEntity(this IEnumerable<TrabajadorModel> lista) {
        return lista.Select(item => item.ToEntity()).ToList();
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Opciones
    // ====================================================================================================


    public static OpcionesModel ToModel(this OpcionesEntity entity) {
        if (entity is null) return null;
        var model = new OpcionesModel {
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


    public static List<OpcionesModel> ToModel(this IEnumerable<OpcionesEntity> lista) {
        return lista.Select(item => item.ToModel()).ToList();
    }


    public static ObservableCollection<OpcionesModel> ToModelObservable(this IEnumerable<OpcionesEntity> lista) {
        return lista.Select(item => item.ToModel()).ToObservableCollection();
    }


    public static List<OpcionesEntity> ToEntity(this IEnumerable<OpcionesModel> lista) {
        return lista.Select(item => item.ToEntity()).ToList();
    }


    #endregion
    // ====================================================================================================




}
