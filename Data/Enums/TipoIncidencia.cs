#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

namespace QuattroX.Data.Enums;


public enum TipoIncidencia {
    Ninguna = 0,
    Trabajo = 1,
    FranqueoTrabajado = 2,
    FiestaPorOtroDia = 3,
    Franqueo = 4,
    TrabajoSinAcumular = 5,
    JornadaMedia = 6,
    Huelga = 7,
}
