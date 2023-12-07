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
/// Representa cada una de las líneas existentes.
/// </summary>
[Table(name: "Lineas")]
public class LineaEntity : EntityBase {


    public string Linea { get; set; }


    public string Texto { get; set; }


    [Ignore]
    public List<ServicioLineaEntity> Servicios { get; set; }


}
