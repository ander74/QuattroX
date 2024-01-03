#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

namespace QuattroX.Data.Interfaces;


public interface IServicio {


    string Linea { get; set; }


    string TextoLinea { get; set; }


    string Servicio { get; set; }


    int Turno { get; set; }


    TimeSpan Inicio { get; set; }


    TimeSpan Final { get; set; }


    string LugarInicio { get; set; }


    string LugarFinal { get; set; }


}
