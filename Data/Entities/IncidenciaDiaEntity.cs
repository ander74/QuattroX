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
/// Representa la incidencia para un día concreto del calendario
/// </summary>
[Table(name: "IncidenciasDia")]
public class IncidenciaDiaEntity : IncidenciaEntity {


    public int DiaId { get; set; }


}
