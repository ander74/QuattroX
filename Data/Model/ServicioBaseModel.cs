#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace QuattroX.Data.Model;


public partial class ServicioBaseModel : ModelBase {


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    string linea;


    [ObservableProperty]
    string servicio;


    [ObservableProperty]
    int turno;


    [ObservableProperty]
    TimeSpan inicio;


    [ObservableProperty]
    TimeSpan final;


    [ObservableProperty]
    string lugarInicio;


    [ObservableProperty]
    string lugarFinal;


    #endregion
    // ====================================================================================================


}
