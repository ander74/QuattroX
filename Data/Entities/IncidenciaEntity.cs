#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Enums;
using SQLite;
using System.ComponentModel.DataAnnotations;

namespace QuattroX.Data.Entities;


/// <summary>
/// Representa cada una de las incidencias existentes.
/// </summary>
[Table(name: "Incidencias")]
public class IncidenciaEntity : EntityBase {


    [EnumDataType(typeof(TipoIncidencia))]
    public TipoIncidencia Tipo { get; set; }


    public int Codigo { get; set; }


    public string Descripcion { get; set; }


    [EnumDataType(typeof(ComportamientoIncidencia))]
    public ComportamientoIncidencia Comportamiento { get; set; }


}
