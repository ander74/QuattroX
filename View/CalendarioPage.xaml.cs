#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

using QuattroX.ViewModel;

namespace QuattroX.View;

public partial class CalendarioPage : ContentPage {
    public CalendarioPage(CalendarioViewModel viewModel) {
        InitializeComponent();
        BindingContext = viewModel;
    }
}