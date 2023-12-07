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
/// Representa cada uno de los servicios que tiene una línea.
/// </summary>
[Table(name: "ServiciosLinea")]
public class ServicioLineaEntity : ServicioBaseEntity {


    public int LineaId { get; set; }


    [Ignore]
    public List<ServicioSecundarioEntity> Servicios { get; set; }


}
