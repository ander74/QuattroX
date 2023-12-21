#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Enums;
using QuattroX.Data.Model;

namespace QuattroX.Services;


public class CalculosService {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================

    private readonly ConfigService configService;


    public CalculosService(ConfigService configService) {
        this.configService = configService;
    }

    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos públicos
    // ====================================================================================================


    /// <summary>
    /// Calcula las horas trabajadas, acumuladas y nocturnas, así como las dietas del día proporcionado.<br/>
    /// Si el día tiene servicios auxiliares, se calcula en función a estos, incluido el principal.
    /// </summary>
    public void CalcularHoras(DiaModel dia) {
        if (dia.Inicio == dia.Final || dia.Turno == 0) {
            dia.Trabajadas = 0m;
            dia.Acumuladas = 0m;
            dia.Nocturnas = 0m;
            dia.Desayuno = false;
            dia.Comida = false;
            dia.Cena = false;
            return;
        }
        if (dia.Servicios?.Any() == true) {
            CalcularHorasCompleto(dia);
            return;
        }
        CalcularHorasSingle(dia);
    }


    /// <summary>
    /// Infiere el turno de una fecha, en función del turno que se tiene la fecha de referencia.
    /// </summary>
    public int InferirTurno(DateTime fecha, DateTime fechaReferencia, int turnoReferencia) {
        var dias = (fecha - fechaReferencia).Days;
        var semanas = 0;
        if (fecha < fechaReferencia) {
            dias = dias - 1;
            semanas = 1;
        }
        semanas += Math.Abs(dias / 7);
        if (semanas % 2 != 0) return turnoReferencia == 1 ? 2 : 1;
        return turnoReferencia;
    }


    public decimal GetTrabajadasConvenio(TipoIncidencia tipoIncidencia) {
        return tipoIncidencia switch {
            TipoIncidencia.Trabajo => configService.Opciones.JornadaMedia,
            TipoIncidencia.TrabajoSinAcumular => configService.Opciones.JornadaMedia,
            TipoIncidencia.FranqueoTrabajado => configService.Opciones.JornadaMedia,
            TipoIncidencia.FiestaPorOtroDia => 0m,
            TipoIncidencia.Franqueo => 0m,
            TipoIncidencia.Huelga => configService.Opciones.JornadaMedia,
            TipoIncidencia.JornadaMedia => configService.Opciones.JornadaMedia,
            TipoIncidencia.Ninguna => 0m,
            _ => 0m,
        };
    }

    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos privados
    // ====================================================================================================


    /// <summary>
    /// Calcula el tiempo trabajado, acumuladas, nocturnas y dietas únicamente para el servicio principal, ignorando los servicios secundarios.
    /// </summary>
    private void CalcularHorasSingle(DiaModel dia) {
        // Horas de inicio y final
        var horaInicio = dia.Inicio;
        var horaFinal = dia.Final < dia.Inicio ? dia.Final + TimeSpan.FromDays(1) : dia.Final;
        // Horas trabajadas.
        dia.Trabajadas = (horaFinal - horaInicio).ToDecimal();
        if (dia.Trabajadas < configService.Opciones.JornadaMinima) dia.Trabajadas = configService.Opciones.JornadaMinima;
        // Horas acumuladas.
        dia.Acumuladas = GetAcumuladas(dia.Trabajadas, dia.Incidencia.Tipo);
        // Horas nocturnas.
        dia.Nocturnas = 0m;
        if (dia.Turno == 1) {
            dia.Nocturnas += (horaInicio < configService.Opciones.FinalNocturnas ? configService.Opciones.FinalNocturnas - horaInicio : TimeSpan.Zero).ToDecimal();
        }
        if (dia.Turno == 2) {
            dia.Nocturnas += (horaFinal > configService.Opciones.InicioNocturnas ? horaFinal - configService.Opciones.InicioNocturnas : TimeSpan.Zero).ToDecimal();
        }
        // Dieta de desayuno.
        dia.Desayuno = horaInicio < configService.Opciones.HoraLimiteDesayuno;
        // Dieta de comida.
        dia.Comida = false;
        if (dia.Turno == 1) {
            dia.Comida = horaFinal > configService.Opciones.HoraLimiteComida1;
        }
        if (dia.Turno == 2) {
            dia.Comida = horaInicio < configService.Opciones.HoraLimiteComida2;
        }
        // Dieta de cena.
        var limiteCena = configService.Opciones.HoraLimiteCena < configService.Opciones.HoraLimiteDesayuno ?
            configService.Opciones.HoraLimiteCena + TimeSpan.FromDays(1) : configService.Opciones.HoraLimiteCena;
        dia.Cena = horaFinal > limiteCena;
    }


    /// <summary>
    /// Calcula el tiempo trabajado, acumuladas, nocturnas y dietas para todos los servicios, incluido el principal.
    /// </summary>
    private void CalcularHorasCompleto(DiaModel dia) {
        // Extraemos las horas de los servicios
        var servicios = dia.Servicios.Select(s => new {
            Inicio = s.Inicio.ToDecimal(),
            Final = (s.Final < s.Inicio ? s.Final + TimeSpan.FromDays(1) : s.Final).ToDecimal()
        }).ToList();
        servicios.Add(new { Inicio = dia.Inicio.ToDecimal(), Final = dia.Final.ToDecimal() });
        // Definimos las variables en local para acortar nombres
        var jornada = configService.Opciones.JornadaMedia;
        var jornadaMinima = configService.Opciones.JornadaMinima;
        var limiteServicios = configService.Opciones.LimiteEntreServicios / 60m;
        var inicioNocturnas = configService.Opciones.InicioNocturnas.ToDecimal();
        var finalNocturnas = configService.Opciones.FinalNocturnas.ToDecimal();
        var desayuno = configService.Opciones.HoraLimiteDesayuno.ToDecimal();
        var comida1 = configService.Opciones.HoraLimiteComida1.ToDecimal();
        var comida2 = configService.Opciones.HoraLimiteComida2.ToDecimal();
        var limiteCena = configService.Opciones.HoraLimiteCena.ToDecimal();
        var cena = limiteCena < desayuno ? limiteCena + 24 : limiteCena;
        // Variables temporales:
        var priInicio = servicios.Min(s => s.Inicio);
        var ultFinal = servicios.Max(s => s.Inicio);
        var minutoActual = 0m;
        var minutosTrabajados = 0m;
        var intermedio = 0m;
        var intermedioParcial = 0m;
        var minutosNocturnos = 0m;
        var intermedioNocturno = 0m;
        var intermedioNocturnoParcial = 0m;
        var dietaDesayuno = false;
        var dietaComida = false;
        var dietaCena = false;
        var esTrabajado = false;
        var esNocturno = false;
        var minTotales = ultFinal - priInicio;
        // Comenzamos el primer bucle: Paso minuto a minuto por el tiempo total.
        for (decimal m = 0.01m; m <= minTotales; m += 0.01m) {
            minutoActual = priInicio + m;
            // Comenzamos el segundo bucle: Recorrido por las horas de los servicios.
            //En este caso es un foreach en lugar de un for.
            foreach (var hora in servicios) {
                // Solo iteramos si el inicio y el final son diferentes y no son nulos.
                if (hora.Inicio != hora.Final && hora.Inicio != TimeSpan.MaxValue.ToDecimal() && hora.Final != TimeSpan.MaxValue.ToDecimal()) {
                    // Si el minuto actual es trabajado
                    if (minutoActual > hora.Inicio && minutoActual <= hora.Final) {
                        minutosTrabajados += 0.01m;
                        // Evaluamos si hay alguna dieta.
                        switch (dia.Turno) {
                            case 1:
                                if (minutoActual < desayuno) dietaDesayuno = true;
                                if (minutoActual > comida1) dietaComida = true;
                                break;
                            case 2:
                                if (minutoActual <= comida2) dietaComida = true;
                                if (minutoActual > cena) dietaCena = true;
                                break;
                        }
                        // Evaluamos si es un minuto nocturno.
                        if ((minutoActual - 0.01m > 0 && minutoActual - 0.01m < finalNocturnas) || (minutoActual > inicioNocturnas && minutoActual < finalNocturnas + 24)) {
                            minutosNocturnos += 0.01m;
                            if (intermedioNocturnoParcial > 0 && intermedioNocturnoParcial < limiteServicios) {
                                intermedioNocturno += intermedioNocturnoParcial;
                            }
                            esNocturno = true;
                        }
                        // Si el tiempo parcial no supera el límite, se suma al tiempo intermedio.
                        if (intermedioParcial > 0 && intermedioParcial < limiteServicios) {
                            intermedio += intermedioParcial;
                        }
                        // Volvemos a poner el tiempo parcial en cero.
                        intermedioParcial = 0m;
                        intermedioNocturnoParcial = 0m;
                        // Marcamos el minuto como trabajado
                        esTrabajado = true;
                        break; //TODO: Comprobar que se sale bien del loop interno y se mantiene el externo.
                    } else {
                        //Establecemos el minuto como no trabajado
                        esTrabajado = false;
                    }
                }
            }
            // Si es un minuto no trabajado, se añade al intermedio parcial, y al intermedio parcial nocturno si procede
            if (!esTrabajado) {
                intermedioParcial += 0.01m;
                if (esNocturno) intermedioNocturnoParcial += 0.01m;
            }
        }
        // Sumamos los intermedios a los totales.
        minutosTrabajados += intermedio;
        minutosNocturnos += intermedioNocturno;
        // Establecemos los resultados.
        dia.Trabajadas = minutosTrabajados;
        dia.Acumuladas = GetAcumuladas(dia.Trabajadas, dia.Incidencia.Tipo);
        dia.Nocturnas = minutosNocturnos;
        dia.Desayuno = dietaDesayuno;
        dia.Comida = dietaComida;
        dia.Cena = dietaCena;
    }


    /// <summary>
    /// Devuelve las horas que se acumulan en función de las horas trabajadas y el tipo de incidencia.
    /// </summary>
    private decimal GetAcumuladas(decimal trabajadas, TipoIncidencia tipoIncidencia) {
        return tipoIncidencia switch {
            TipoIncidencia.Trabajo => trabajadas - configService.Opciones.JornadaMedia,
            TipoIncidencia.FranqueoTrabajado => trabajadas,
            TipoIncidencia.FiestaPorOtroDia => -configService.Opciones.JornadaMedia,
            _ => 0m,
        };
    }

    #endregion
    // ====================================================================================================


}
