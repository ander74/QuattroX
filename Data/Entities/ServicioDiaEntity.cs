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
/// Representa cada uno de los servicios que se hacen en un día.
/// </summary>
[Table(name: "ServiciosDia")]
public class ServicioDiaEntity : ServicioBaseEntity {


    public int DiaId { get; set; }


    public bool EsPrincipal { get; set; }


}
