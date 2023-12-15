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
    #region Trabajador
    // ====================================================================================================


    public static TrabajadorModel ToModel(this TrabajadorEntity entity) {
        if (entity is null) return null;
        var model = new TrabajadorModel {
            Id = entity.Id,
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
    #region Relevo
    // ====================================================================================================


    public static RelevoModel ToModel(this RelevoEntity entity) {
        if (entity is null) return null;
        var model = new RelevoModel {
            Id = entity.Id,
            DiaId = entity.DiaId,
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


    public static RelevoEntity ToEntity(this RelevoModel model) {
        if (model is null) return null;
        return new RelevoEntity {
            Id = model.Id,
            DiaId = model.DiaId,
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
    #region Susti
    // ====================================================================================================


    public static SustiModel ToModel(this SustiEntity entity) {
        if (entity is null) return null;
        var model = new SustiModel {
            Id = entity.Id,
            DiaId = entity.DiaId,
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


    public static SustiEntity ToEntity(this SustiModel model) {
        if (model is null) return null;
        return new SustiEntity {
            Id = model.Id,
            DiaId = model.DiaId,
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
    #region Línea
    // ====================================================================================================


    public static LineaModel ToModel(this LineaEntity entity) {
        if (entity is null) return null;
        var model = new LineaModel {
            Id = entity.Id,
            Linea = entity.Linea,
            Texto = entity.Texto,
            Servicios = entity.Servicios.Select(s => s.ToModel()).ToObservableCollection(),
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
            Servicios = model.Servicios.Select(s => s.ToEntity()).ToList(),
        };
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
            Servicio = model.Servicio,
            Turno = model.Turno,
            Inicio = model.Inicio,
            Final = model.Final,
            LugarInicio = model.LugarInicio,
            LugarFinal = model.LugarFinal,
        };
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
            Servicio = entity.Servicio,
            Turno = entity.Turno,
            Inicio = entity.Inicio,
            Final = entity.Final,
            LugarInicio = entity.LugarInicio,
            LugarFinal = entity.LugarFinal,
            Servicios = entity.Servicios.Select(s => s.ToModel()).ToObservableCollection(),
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
            Servicio = model.Servicio,
            Turno = model.Turno,
            Inicio = model.Inicio,
            Final = model.Final,
            LugarInicio = model.LugarInicio,
            LugarFinal = model.LugarFinal,
            Servicios = model.Servicios.Select(s => s.ToEntity()).ToList(),
        };
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
            Servicio = model.Servicio,
            Turno = model.Turno,
            Inicio = model.Inicio,
            Final = model.Final,
            LugarInicio = model.LugarInicio,
            LugarFinal = model.LugarFinal,
        };
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region ServicioSecundarioDia
    // ====================================================================================================


    public static ServicioSecundarioDiaModel ToModel(this ServicioSecundarioDiaEntity entity) {
        if (entity is null) return null;
        var model = new ServicioSecundarioDiaModel {
            Id = entity.Id,
            DiaId = entity.DiaId,
            Linea = entity.Linea,
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


    public static ServicioSecundarioDiaEntity ToEntity(this ServicioSecundarioDiaModel model) {
        if (model is null) return null;
        return new ServicioSecundarioDiaEntity {
            Id = model.Id,
            DiaId = model.DiaId,
            Linea = model.Linea,
            Servicio = model.Servicio,
            Turno = model.Turno,
            Inicio = model.Inicio,
            Final = model.Final,
            LugarInicio = model.LugarInicio,
            LugarFinal = model.LugarFinal,
        };
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
            Servicio = model.Servicio,
            Turno = model.Turno,
            Inicio = model.Inicio,
            Final = model.Final,
            LugarInicio = model.LugarInicio,
            LugarFinal = model.LugarFinal,
        };
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


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region IncidenciaDia
    // ====================================================================================================


    public static IncidenciaDiaModel ToModel(this IncidenciaDiaEntity entity) {
        if (entity is null) return null;
        var model = new IncidenciaDiaModel {
            Id = entity.Id,
            DiaId = entity.DiaId,
            Tipo = entity.Tipo,
            Codigo = entity.Codigo,
            Descripcion = entity.Descripcion,
        };
        model.Modified = false;
        return model;
    }


    public static IncidenciaDiaEntity ToEntity(this IncidenciaDiaModel model) {
        if (model is null) return null;
        return new IncidenciaDiaEntity {
            Id = model.Id,
            DiaId = model.DiaId,
            Tipo = model.Tipo,
            Codigo = model.Codigo,
            Descripcion = model.Descripcion,
        };
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
            DiaId = entity.DiaId,
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
            DiaId = model.DiaId,
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


    public static DiaModel ToModel(this DiaEntity entity) {
        if (entity is null) return null;
        var model = new DiaModel {
            Id = entity.Id,
            Fecha = entity.Fecha,
            EsFranqueo = entity.EsFranqueo,
            EsFestivo = entity.EsFestivo,
            IncidenciaId = entity.IncidenciaId,
            Incidencia = entity.Incidencia is null ? null : entity.Incidencia.ToModel(),
            ServicioPrincipalId = entity.ServicioPrincipalId,
            ServicioPrincipal = entity.ServicioPrincipal is null ? null : entity.ServicioPrincipal.ToModel(),
            Servicios = entity.Servicios is null ? null : entity.Servicios.Select(s => s.ToModel()).ToObservableCollection(),
            Regulaciones = entity.Regulaciones is null ? null : entity.Regulaciones.Select(s => s.ToModel()).ToObservableCollection(),
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
            Relevo = entity.Relevo is null ? null : entity.Relevo.ToModel(),
            SustiId = entity.SustiId,
            Susti = entity.Susti is null ? null : entity.Susti.ToModel(),
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
            Incidencia = model.Incidencia is null ? null : model.Incidencia.ToEntity(),
            ServicioPrincipalId = model.ServicioPrincipalId,
            ServicioPrincipal = model.ServicioPrincipal is null ? null : model.ServicioPrincipal.ToEntity(),
            Servicios = model.Servicios is null ? null : model.Servicios.Select(s => s.ToEntity()).ToList(),
            Regulaciones = model.Regulaciones is null ? null : model.Regulaciones.Select(s => s.ToEntity()).ToList(),
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
            Relevo = model.Relevo is null ? null : model.Relevo.ToEntity(),
            SustiId = model.SustiId,
            Susti = model.Susti is null ? null : model.Susti.ToEntity(),
            Bus = model.Bus,
            Notas = model.Notas,
        };
    }


    public static void Vaciar(this DiaModel model) {
        if (model is null) return;
        model.EsFranqueo = false;
        model.EsFestivo = false;
        model.IncidenciaId = 0;
        model.Incidencia = null;
        model.ServicioPrincipalId = 0;
        model.ServicioPrincipal = null;
        model.Servicios = new();
        model.Regulaciones = new();
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
        model.Relevo = null;
        model.SustiId = 0;
        model.Susti = null;
        model.Bus = string.Empty;
        model.Notas = string.Empty;
    }


    public static void FromEntity(this DiaModel model, DiaEntity entity) {
        if (model is null || entity is null) return;
        model.Id = entity.Id;
        model.Fecha = entity.Fecha;
        model.EsFranqueo = entity.EsFranqueo;
        model.EsFestivo = entity.EsFestivo;
        model.IncidenciaId = entity.IncidenciaId;
        model.Incidencia = entity.Incidencia is null ? null : entity.Incidencia.ToModel();
        model.ServicioPrincipalId = entity.ServicioPrincipalId;
        model.ServicioPrincipal = entity.ServicioPrincipal is null ? null : entity.ServicioPrincipal.ToModel();
        model.Servicios = entity.Servicios is null ? null : entity.Servicios.Select(s => s.ToModel()).ToObservableCollection();
        model.Regulaciones = entity.Regulaciones is null ? null : entity.Regulaciones.Select(s => s.ToModel()).ToObservableCollection();
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
        model.Relevo = entity.Relevo is null ? null : entity.Relevo.ToModel();
        model.SustiId = entity.SustiId;
        model.Susti = entity.Susti is null ? null : entity.Susti.ToModel();
        model.Bus = entity.Bus;
        model.Notas = entity.Notas;
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


    #endregion
    // ====================================================================================================




}
