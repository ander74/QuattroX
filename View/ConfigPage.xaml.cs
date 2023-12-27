#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

using QuattroX.ViewModel;

namespace QuattroX.View;


public partial class ConfigPage : ContentPage {

    public ConfigPage(ConfigViewModel viewModel) {
        InitializeComponent();
        BindingContext = viewModel;
    }
}