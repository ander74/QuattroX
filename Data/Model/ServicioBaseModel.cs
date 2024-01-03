#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Interfaces;

namespace QuattroX.Data.Model;


public partial class ServicioBaseModel : ModelBase, IServicio {


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    string linea;


    [ObservableProperty]
    string textoLinea;


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


    // ====================================================================================================
    #region Propiedades no enlazadas
    // ====================================================================================================

    public string TextoServicio => Turno > 0 ? $"{Servicio}/{Turno}" : Servicio;


    #endregion
    // ====================================================================================================


}
