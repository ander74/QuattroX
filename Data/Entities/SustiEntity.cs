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
/// Representa a un susti en un día.
/// </summary>
[Table(name: "Sustis")]
public class SustiEntity : TrabajadorEntity {

    public int DiaId { get; set; }


}
