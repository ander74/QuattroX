#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
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


    // ====================================================================================================
    #region Propiedades no mapeadas
    // ====================================================================================================


    public int RowIndex { get; set; }


    #endregion
    // ====================================================================================================


}
