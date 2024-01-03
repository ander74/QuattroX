#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Enums;

namespace QuattroX.Data.Model;


public partial class IncidenciaModel : ModelBase {


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    TipoIncidencia tipo;


    [ObservableProperty]
    int codigo;


    [ObservableProperty]
    string descripcion;


    [ObservableProperty]
    ComportamientoIncidencia comportamiento;


    #endregion
    // ====================================================================================================


}
