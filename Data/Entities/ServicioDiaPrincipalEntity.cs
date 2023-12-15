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
/// Representa cada uno de los servicios secundarios de un día.
/// </summary>
[Table(name: "ServiciosSecundariosDia")]
public class ServicioSecundarioDiaEntity : ServicioBaseEntity {

    public int DiaId { get; set; }


}
