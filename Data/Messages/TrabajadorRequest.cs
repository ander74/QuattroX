#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using CommunityToolkit.Mvvm.Messaging.Messages;
using QuattroX.Data.Model;

namespace QuattroX.Data.Messages;


public class TrabajadorRequest : RequestMessage<TrabajadorModel> {


    public TrabajadorRequest() : base() {

    }


    public TrabajadorRequest(int trabajadorId) : base() {
        TrabajadorId = trabajadorId;
    }


    public int TrabajadorId { get; set; }

}
