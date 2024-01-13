#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

namespace QuattroX.Data.Messages;


public class RellenarSemanaRequest {


    public RellenarSemanaRequest(DateTime fechaModificada) {
        FechaModificada = fechaModificada;
    }


    public DateTime FechaModificada { get; set; }

}
