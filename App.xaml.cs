using QuattroX.ViewModel;

namespace QuattroX;

public partial class App : Application {
    public App(MainViewModel viewModel) {

        InitializeComponent();

        MainPage = new AppShell(viewModel);
    }
}
