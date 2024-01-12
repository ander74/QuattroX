﻿#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

using QuattroX.Data.Model;

namespace QuattroX.Data.Messages;


public class AddTrabajadorRequest {


    public AddTrabajadorRequest(TrabajadorModel trabajador) {
        Trabajador = trabajador;
    }


    public TrabajadorModel Trabajador { get; set; }
}
