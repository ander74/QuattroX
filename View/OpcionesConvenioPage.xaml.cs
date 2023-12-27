#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para m�s detalles 
// ===============================================
#endregion
using QuattroX.ViewModel;

namespace QuattroX.View;


public partial class OpcionesConvenioPage : ContentPage {

    public OpcionesConvenioPage(ConfigViewModel viewModel) {
        InitializeComponent();
        BindingContext = viewModel;
    }

}