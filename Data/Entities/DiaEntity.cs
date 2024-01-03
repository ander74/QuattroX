#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

using QuattroX.Data.Interfaces;
using SQLite;

namespace QuattroX.Data.Entities;


/// <summary>
/// Representa a un día en el calendario
/// </summary>
[Table(name: "Calendario")]
public class DiaEntity : EntityBase, IServicio {


    public DateTime Fecha { get; set; }


    public bool EsFranqueo { get; set; }


    public bool EsFestivo { get; set; }


    public int IncidenciaId { get; set; }


    public string Linea { get; set; }


    public string TextoLinea { get; set; }


    public string Servicio { get; set; }


    public int Turno { get; set; }


    public TimeSpan Inicio { get; set; }


    public TimeSpan Final { get; set; }


    public string LugarInicio { get; set; }


    public string LugarFinal { get; set; }


    [Ignore]
    public List<ServicioDiaEntity> Servicios { get; set; } = new();


    public decimal Trabajadas { get; set; }


    public decimal Acumuladas { get; set; }


    public decimal Nocturnas { get; set; }


    public bool Desayuno { get; set; }


    public bool Comida { get; set; }


    public bool Cena { get; set; }


    public decimal TomaDeje { get; set; }


    public decimal Euros { get; set; }


    public bool HuelgaParcial { get; set; }


    public decimal HorasHuelga { get; set; }


    public int RelevoId { get; set; }


    public int SustiId { get; set; }


    public string Bus { get; set; }


    [SQLite.MaxLength(2048)]
    public string Notas { get; set; }


}
