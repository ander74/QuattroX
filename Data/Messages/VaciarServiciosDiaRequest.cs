#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

namespace QuattroX.Data.Messages;


public class VaciarServiciosDiaRequest {

    public VaciarServiciosDiaRequest() {

    }


    public VaciarServiciosDiaRequest(int diaId) {
        DiaId = diaId;
    }


    public int DiaId { get; set; }

}
