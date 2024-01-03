#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

using QuattroX.View.Templates;
using QuattroX.ViewModel;

namespace QuattroX.View;

public partial class DetalleDiaPage : ContentPage {



    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================

    private readonly DetalleDiaViewModel viewModel;

    private DiaTabPage diaPage;
    private ServiciosTabPage serviciosPage;
    private Color colorSelected;
    private Color colorNotSelected;
    private Color colorTextSelected;
    private Color colorTextNotSelected;
    private bool isDiaSelected = true;


    public DetalleDiaPage(DetalleDiaViewModel viewModel) {
        InitializeComponent();

        this.viewModel = viewModel;
        BindingContext = this.viewModel;

        colorSelected = (Color)((App.Current.Resources.TryGetValue("PrimaryDark", out object color1)) ? color1 : Colors.Purple);
        colorNotSelected = (Color)((App.Current.Resources.TryGetValue("Primary", out object color2)) ? color2 : Colors.DarkMagenta);
        colorTextSelected = (Color)((App.Current.Resources.TryGetValue("PrimaryDarkText", out object color3)) ? color3 : Colors.Gray);
        colorTextNotSelected = (Color)((App.Current.Resources.TryGetValue("Secondary", out object color4)) ? color4 : Colors.LightPink);

        diaPage = new DiaTabPage();
        diaPage.BindingContext = this.viewModel;

        serviciosPage = new ServiciosTabPage();
        serviciosPage.BindingContext = this.viewModel;

        Contenedor.Content = diaPage;
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos de eventos
    // ====================================================================================================


    private void LabelDia_Tapped(object sender, TappedEventArgs e) {
        Contenedor.Content = diaPage;
        LabelDia.BackgroundColor = colorSelected;
        LabelDia.TextColor = colorTextSelected;
        LabelServicios.BackgroundColor = colorNotSelected;
        LabelServicios.TextColor = colorTextNotSelected;
    }


    private void LabelServicios_Tapped(object sender, TappedEventArgs e) {
        Contenedor.Content = serviciosPage;
        LabelServicios.BackgroundColor = colorSelected;
        LabelServicios.TextColor = colorTextSelected;
        LabelDia.BackgroundColor = colorNotSelected;
        LabelDia.TextColor = colorTextNotSelected;
    }

    private void Contenedor_RightSwiped(object sender, SwipedEventArgs e) {
        if (!isDiaSelected) {
            Contenedor.Content = diaPage;
            LabelDia.BackgroundColor = colorSelected;
            LabelDia.TextColor = colorTextSelected;
            LabelServicios.BackgroundColor = colorNotSelected;
            LabelServicios.TextColor = colorTextNotSelected;
            isDiaSelected = true;
        }
    }


    private void Contenedor_LeftSwiped(object sender, SwipedEventArgs e) {
        if (isDiaSelected) {
            Contenedor.Content = serviciosPage;
            LabelServicios.BackgroundColor = colorSelected;
            LabelServicios.TextColor = colorTextSelected;
            LabelDia.BackgroundColor = colorNotSelected;
            LabelDia.TextColor = colorTextNotSelected;
            isDiaSelected = false;
        }
    }




    #endregion
    // ====================================================================================================

}