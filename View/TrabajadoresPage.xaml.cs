#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.ViewModel;

namespace QuattroX.View;


public partial class TrabajadoresPage : ContentPage {

    private readonly TrabajadoresViewModel viewModel;

    public TrabajadoresPage(TrabajadoresViewModel viewModel) {
        InitializeComponent();
        BindingContext = viewModel;
        this.viewModel = viewModel;
    }


    protected override bool OnBackButtonPressed() {
        base.OnBackButtonPressed();
        if (viewModel.BackCommand.CanExecute(null)) {
            viewModel.BackCommand.Execute(null);
        }
        return true;
    }

}