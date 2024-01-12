#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace QuattroX.Data.Model;


public partial class LineaModel : ModelBase {


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextoParaPickr))]
    string linea;


    [ObservableProperty]
    string texto;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextoServicios))]
    ObservableCollection<ServicioLineaModel> servicios = new();

    partial void OnServiciosChanged(ObservableCollection<ServicioLineaModel> value) {
        Servicios.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TextoServicios));
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades no mapeadas
    // ====================================================================================================


    public int RowIndex { get; set; }


    public string TextoParaPickr => string.IsNullOrWhiteSpace(Linea) ? $"Nueva línea" : Linea;


    public string TextoServicios {
        get {
            if (Servicios is null || Servicios.Count == 0) return "No hay servicios";
            if (Servicios.Count == 1) return "Un servicio";
            return $"{Servicios.Count} servicios";
        }
    }



    #endregion
    // ====================================================================================================


}
