#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para m�s detalles 
// ===============================================
#endregion

using QuattroX.ViewModel;

namespace QuattroX.View;


public partial class ConfigPage : ContentPage {

    private readonly ConfigViewModel viewModel;

    public ConfigPage(ConfigViewModel viewModel) {
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