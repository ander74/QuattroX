#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

using SQLite;

namespace QuattroX.Data.Entities;


[Table(name: "Opciones")]
public class OpcionesEntity : EntityBase {


    public DateTime PrimerMesMostrado { get; set; }


    public decimal AcumuladasAnteriores { get; set; }


    public int RelevoFijo { get; set; }


    public bool RellenarSemanaAutomaticamente { get; set; }


    public decimal JornadaMedia { get; set; }


    public decimal JornadaMinima { get; set; }


    public int JornadaAnual { get; set; }


    public int LimiteEntreServicios { get; set; }


    public bool RegularJornadaAnual { get; set; }


    public bool RegularAñosBisiestos { get; set; }


    public TimeSpan InicioNocturnas { get; set; }


    public TimeSpan FinalNocturnas { get; set; }


    public TimeSpan HoraLimiteDesayuno { get; set; }


    public TimeSpan HoraLimiteComida1 { get; set; }


    public TimeSpan HoraLimiteComida2 { get; set; }


    public TimeSpan HoraLimiteCena { get; set; }


    public bool InferirTurnos { get; set; }


    public DateTime FechaReferenciaTurnos { get; set; }


    public bool AcumularTomaDeje { get; set; }





}
