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
/// Representa a cada uno de los relevos que tenemos en un día.
/// </summary>
[Table(name: "Relevos")]
public class RelevoEntity : TrabajadorEntity {


    public int DiaId { get; set; }

}
