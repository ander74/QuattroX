﻿#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

using CommunityToolkit.Mvvm.Messaging.Messages;
using QuattroX.Data.Model;

namespace QuattroX.Data.Messages;


public class LineaRequest : RequestMessage<LineaModel> {


    public LineaRequest(string linea) : base() {
        Linea = linea;
    }


    public string Linea { get; set; }


}
