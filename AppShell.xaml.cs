using QuattroX.View;
using QuattroX.ViewModel;

namespace QuattroX;

public partial class AppShell : Shell {


    public AppShell(MainViewModel viewModel) {
        InitializeComponent();
        this.BindingContext = viewModel;

        Routing.RegisterRoute(nameof(DetalleDiaPage), typeof(DetalleDiaPage));
        Routing.RegisterRoute(nameof(DetalleTrabajadorPage), typeof(DetalleTrabajadorPage));
        Routing.RegisterRoute(nameof(OpcionesConvenioPage), typeof(OpcionesConvenioPage));
        Routing.RegisterRoute(nameof(OpcionesGeneralesPage), typeof(OpcionesGeneralesPage));
    }
}
