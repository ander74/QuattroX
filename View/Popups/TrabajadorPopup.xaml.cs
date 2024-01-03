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


public partial class TrabajadorPopup : Popup {


    private readonly TrabajadorPopupViewModel viewModel;


    public TrabajadorPopup(TrabajadorPopupViewModel vm) {
        this.viewModel = vm;
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void Aceptar_Clicked(object sender, EventArgs e) {
        if (viewModel.Trabajador is null) viewModel.Trabajador = new();
        await viewModel.AceptarAsync();
        await CloseAsync(viewModel.Trabajador);
    }


    private async void Cancelar_Clicked(object sender, EventArgs e) {
        await CloseAsync();
    }
}