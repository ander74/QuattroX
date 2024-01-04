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

public partial class ServicioLineaPopup : Popup {


    private readonly ServicioLineaPopupViewModel viewModel;

    public ServicioLineaPopup(ServicioLineaPopupViewModel viewModel) {
        this.viewModel = viewModel;
        BindingContext = viewModel;
        InitializeComponent();
    }

    private async void Aceptar_Clicked(object sender, EventArgs e) {
        if (viewModel.Servicio is null) viewModel.Servicio = new();
        //await viewModel.AceptarAsync();
        await CloseAsync(viewModel.Servicio);
    }


    private async void Cancelar_Clicked(object sender, EventArgs e) {
        await CloseAsync();
    }


}