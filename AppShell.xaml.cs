using QuattroX.View;

namespace QuattroX;

public partial class AppShell : Shell {

    public AppShell() {
        InitializeComponent();

        Routing.RegisterRoute(nameof(DetalleDiaPage), typeof(DetalleDiaPage));
    }
}
