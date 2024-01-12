#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using CommunityToolkit.Maui.Views;
using QuattroX.ViewModel.Popups;

namespace QuattroX.View.Popups;


public partial class ServicioDiaPopup : Popup {


    private readonly ServicioDiaPopupViewModel viewModel;

    public ServicioDiaPopup(ServicioDiaPopupViewModel viewModel) {
        this.viewModel = viewModel;
        BindingContext = viewModel;
        InitializeComponent();
    }

    private async void Aceptar_Clicked(object sender, EventArgs e) {
        if (viewModel.Servicio is null) viewModel.Servicio = new();
        viewModel.OnClose();
        await CloseAsync(viewModel.Servicio);
    }


    private async void Cancelar_Clicked(object sender, EventArgs e) {
        await CloseAsync();
    }

}