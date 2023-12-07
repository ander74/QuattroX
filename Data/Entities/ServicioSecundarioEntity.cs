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
/// Representa cada uno de los servicios secundarios que tienen los servicios de una línea.
/// </summary>
[Table(name: "ServiciosSecundarios")]
public class ServicioSecundarioEntity : ServicioBaseEntity {


    public int ServicioId { get; set; }


}
