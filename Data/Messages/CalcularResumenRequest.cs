#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace QuattroX.Data.Messages;


public class CalcularResumenRequest : RequestMessage<DateTime> {

    public CalcularResumenRequest() {

    }


    public CalcularResumenRequest(DateTime fechaResumen) {
        FechaResumen = fechaResumen;
    }


    public DateTime FechaResumen { get; set; }
}
