#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

using SQLite;

namespace QuattroX.Data.Entities;


/// <summary>
/// Representa a un día en el calendario
/// </summary>
[Table(name: "Calendario")]
public class DiaEntity : EntityBase {


    public DateTime Fecha { get; set; }


    public bool EsFranqueo { get; set; }


    public bool EsFestivo { get; set; }


    public int IncidenciaId { get; set; }


    [Ignore]
    public IncidenciaEntity Incidencia { get; set; }


    public int ServicioPrincipalId { get; set; }


    [Ignore]
    public ServicioDiaEntity ServicioPrincipal { get; set; }


    [Ignore]
    public List<ServicioDiaEntity> Servicios { get; set; }


    [Ignore]
    public List<RegulacionEntity> Regulaciones { get; set; }


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


    [Ignore]
    public TrabajadorEntity Relevo { get; set; }


    public int SustiId { get; set; }


    [Ignore]
    public TrabajadorEntity Susti { get; set; }


    public string Bus { get; set; }


    [SQLite.MaxLength(2048)]
    public string Notas { get; set; }


}
