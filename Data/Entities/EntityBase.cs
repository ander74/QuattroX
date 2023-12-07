#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using SQLite;

namespace QuattroX.Data.Entities;


public class EntityBase {


    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

}
