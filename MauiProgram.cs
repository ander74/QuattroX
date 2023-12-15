using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using QuattroX.Data.Repositories;
using QuattroX.Services;
using QuattroX.View;
using QuattroX.ViewModel;

namespace QuattroX;

// NOTA: Primero se ejecuta MauiProgram, despues App y finalmente AppShell.

public static class MauiProgram {

    public static MauiApp CreateMauiApp() {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            // Initialize the .NET MAUI Community Toolkit by adding the below line of code
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts => {
                fonts.AddFont("Material.ttf", "Material");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });


        // Servicios
        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddSingleton<DbRepository>();
        builder.Services.AddSingleton<ConfigService>();
        builder.Services.AddSingleton<CalculosService>();


        // Páginas
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<CalendarioPage>();
        builder.Services.AddTransient<DetalleDiaPage>();
        //builder.Services.AddTransient<DiaTabPage>();


        // ViewModels
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<CalendarioViewModel>();
        builder.Services.AddTransient<DetalleDiaViewModel>();
        //builder.Services.AddTransient<DiaTabViewModel>();


#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
