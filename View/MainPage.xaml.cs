using QuattroX.ViewModel;

namespace QuattroX.View;

public partial class MainPage : ContentPage {

    public MainPage(MainViewModel viewmodel) {
        InitializeComponent();
        BindingContext = viewmodel;
    }

}

