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
/// Representa cada uno de los trabajadores que nos relevan o nos hacen el día.
/// </summary>
[Table(name: "Trabajadores")]
public class TrabajadorEntity : EntityBase {


    public int Matricula { get; set; }


    public string Nombre { get; set; }


    public string Apellidos { get; set; }


    public string Telefono { get; set; }


    public string Email { get; set; }


    [EnumDataType(typeof(CalificacionTrabajador))]
    public CalificacionTrabajador Calificacion { get; set; }


    public int DeudaInicial { get; set; }


    [SQLite.MaxLength(2048)]
    public string Notas { get; set; }


}
