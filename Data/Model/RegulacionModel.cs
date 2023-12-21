#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Enums;

namespace QuattroX.Data.Model;


public partial class RegulacionModel : ModelBase {


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    DateTime fecha;


    [ObservableProperty]
    TipoRegulacion tipo;


    [ObservableProperty]
    decimal horas;


    [ObservableProperty]
    string motivo;


    #endregion
    // ====================================================================================================


}
