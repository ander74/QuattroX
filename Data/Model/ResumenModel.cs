#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

namespace QuattroX.Data.Model;


public partial class ResumenModel : ModelBase {


    [ObservableProperty]
    DateTime fecha;


    [ObservableProperty]
    decimal trabajadas;


    [ObservableProperty]
    decimal trabajadasConvenio;


    [ObservableProperty]
    decimal acumuladas;


    [ObservableProperty]
    decimal nocturnas;


    [ObservableProperty]
    decimal tomaDeje;


    [ObservableProperty]
    decimal euros;


    [ObservableProperty]
    decimal regulaciones;


}
