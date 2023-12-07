#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System.Collections.ObjectModel;

namespace QuattroX.Data.Model;


public partial class LineaModel : ModelBase {


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    string linea;


    [ObservableProperty]
    string texto;


    [ObservableProperty]
    ObservableCollection<ServicioLineaModel> servicios;


    #endregion
    // ====================================================================================================


}
