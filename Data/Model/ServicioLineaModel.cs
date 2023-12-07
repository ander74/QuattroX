#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System.Collections.ObjectModel;

namespace QuattroX.Data.Model;


public partial class ServicioLineaModel : ServicioBaseModel {


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    int lineaId;


    [ObservableProperty]
    ObservableCollection<ServicioSecundarioModel> servicios;


    #endregion
    // ====================================================================================================


}
