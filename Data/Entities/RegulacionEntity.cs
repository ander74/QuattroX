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
/// Representa una regulación para un día en concreto.
/// </summary>
[Table(name: "Regulaciones")]
public class RegulacionEntity : EntityBase {


    public DateTime Fecha { get; set; }


    [EnumDataType(typeof(TipoRegulacion))]
    public TipoRegulacion Tipo { get; set; }


    public decimal Horas { get; set; }


    [SQLite.MaxLength(2048)]
    public string Motivo { get; set; }



}
