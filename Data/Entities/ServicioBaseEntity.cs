#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

namespace QuattroX.Data.Entities;


/// <summary>
/// Representa la base para la creación del resto de servicios.
/// </summary>
public class ServicioBaseEntity : EntityBase {


    public string Linea { get; set; }


    public string TextoLinea { get; set; }


    public string Servicio { get; set; }


    public int Turno { get; set; }


    public TimeSpan Inicio { get; set; }


    public TimeSpan Final { get; set; }


    public string LugarInicio { get; set; }


    public string LugarFinal { get; set; }

}
