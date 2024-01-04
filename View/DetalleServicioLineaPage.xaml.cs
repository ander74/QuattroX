#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

using QuattroX.ViewModel;

namespace QuattroX.View;

public partial class DetalleServicioLineaPage : ContentPage {


    private readonly DetalleServicioLineaViewModel viewModel;


    public DetalleServicioLineaPage(DetalleServicioLineaViewModel viewModel) {
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
}