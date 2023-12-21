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

    private readonly CalendarioViewModel viewModel;

    private bool resumenExtendido = false;

    public CalendarioPage(CalendarioViewModel viewModel) {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }


    protected override bool OnBackButtonPressed() {
        base.OnBackButtonPressed();
        var resultado = viewModel.IsSelectionMode;
        if (viewModel.BackCommand.CanExecute(null)) {
            viewModel.BackCommand.Execute(null);
        }
        return resultado;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
        resumenExtendido = !resumenExtendido;
        ResumenExtendido.IsVisible = resumenExtendido;
        Horas1.IsVisible = !resumenExtendido;
        Horas2.IsVisible = !resumenExtendido;
    }
}