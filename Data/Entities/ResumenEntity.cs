#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

using SQLite;

namespace QuattroX.Data.Entities;


[Table(name: "Resumenes")]
public class ResumenEntity : EntityBase {


    public DateTime Fecha { get; set; }


    public decimal Trabajadas { get; set; }


    public decimal TrabajadasConvenio { get; set; }


    public decimal Acumuladas { get; set; }


    public decimal Nocturnas { get; set; }


    public decimal TomaDeje { get; set; }


    public decimal Euros { get; set; }


    public decimal Regulaciones { get; set; }


}
