#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

namespace QuattroX.Data.Model;


public partial class OpcionesModel : ModelBase {


    /// <summary>
    /// Primer mes que se muestra en la aplicación.
    /// </summary>
    [ObservableProperty]
    DateTime primerMesMostrado;


    /// <summary>
    /// Horas acumuladas con las que se empieza en la aplicación en decimal.
    /// </summary>
    [ObservableProperty]
    decimal acumuladasAnteriores;


    /// <summary>
    /// Matrícula del relevo que se pondrá por defecto. Si es cero, no se pone ninguno.
    /// </summary>
    [ObservableProperty]
    int relevoFijo;


    /// <summary>
    /// Si es true, al introducir un día, la semana se rellena automáticamente.<br/>
    /// Únicamente se rellenan los días que no tengan servicio puesto.
    /// </summary>
    [ObservableProperty]
    bool rellenarSemanaAutomaticamente;


    /// <summary>
    /// Jornada media en horas decimales.
    /// </summary>
    [ObservableProperty]
    decimal jornadaMedia;


    /// <summary>
    /// Jornada mínima en horas decimales.
    /// </summary>
    [ObservableProperty]
    decimal jornadaMinima;


    /// <summary>
    /// Jornada anual en días.
    /// </summary>
    [ObservableProperty]
    int jornadaAnual;


    /// <summary>
    /// Tiempo límite entre dos servicios que se acumula en minutos.<br/>
    /// </summary>
    [ObservableProperty]
    int limiteEntreServicios;


    /// <summary>
    /// Si es true, al guardar el último día del año se crea una regulación con las horas que faltan para completar la jornada anual.
    /// </summary>
    [ObservableProperty]
    bool regularJornadaAnual;


    /// <summary>
    /// Si es true, al guardar el día 29 de febrero, se crea una regulación que compensa el exceso de jornada del año, por ser bisiesto.
    /// </summary>
    [ObservableProperty]
    bool regularAñosBisiestos;


    /// <summary>
    /// Hora en <see cref="TimeSpan"/> en la que se inicia el período de computo de nocturnidad.
    /// </summary>
    [ObservableProperty]
    TimeSpan inicioNocturnas;


    /// <summary>
    /// Hora en <see cref="TimeSpan"/> en la que finaliza el período de computo de nocturnidad.
    /// </summary>
    [ObservableProperty]
    TimeSpan finalNocturnas;


    /// <summary>
    /// Hora en <see cref="TimeSpan"/> que define el cobro de la dieta de desayuno.
    /// </summary>
    [ObservableProperty]
    TimeSpan horaLimiteDesayuno;


    /// <summary>
    /// Hora en <see cref="TimeSpan"/>que define el cobro de la dieta de comida estando de mañana.
    /// </summary>
    [ObservableProperty]
    TimeSpan horaLimiteComida1;


    /// <summary>
    /// Hora en <see cref="TimeSpan"/> que define el cobro de la dieta de comida estando de tarde.
    /// </summary>
    [ObservableProperty]
    TimeSpan horaLimiteComida2;


    /// <summary>
    /// Hora en <see cref="TimeSpan"/> que define el cobro de la dieta de cena.
    /// </summary>
    [ObservableProperty]
    TimeSpan horaLimiteCena;


    /// <summary>
    /// Si está en true, cada vez que se carga un mes se infieren los turnos para los días de ese mes.
    /// </summary>
    [ObservableProperty]
    bool inferirTurnos;


    /// <summary>
    /// Fecha en <see cref="DateTime"/> que define el día en que empiezas el período de mañana para la inferencia de turnos.
    /// </summary>
    [ObservableProperty]
    DateTime fechaReferenciaTurnos;


    /// <summary>
    /// Si está true, el tiempo de toma-deje se acumula en el contador de horas.
    /// </summary>
    [ObservableProperty]
    bool acumularTomaDeje;


}
