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
                fonts.AddFont("FontAwesome.ttf", "FontAwesome");
                fonts.AddFont("Material.ttf", "Material");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });


        // Servicios
        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddSingleton<DbRepository>();
        builder.Services.AddSingleton<ConfigService>();
        builder.Services.AddSingleton<CalculosService>();
        builder.Services.AddSingleton<DropboxService>();


        // Páginas
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<CalendarioPage>();
        builder.Services.AddTransient<DetalleDiaPage>();
        builder.Services.AddSingleton<TrabajadoresPage>();
        builder.Services.AddSingleton<ConfigPage>();
        builder.Services.AddSingleton<OpcionesConvenioPage>();
        builder.Services.AddSingleton<OpcionesGeneralesPage>();
        builder.Services.AddTransient<DetalleTrabajadorPage>();
        builder.Services.AddTransient<LineasPage>();


        // ViewModels
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<CalendarioViewModel>();
        builder.Services.AddTransient<DetalleDiaViewModel>();
        builder.Services.AddSingleton<TrabajadoresViewModel>();
        builder.Services.AddSingleton<ConfigViewModel>();
        builder.Services.AddTransient<DetalleTrabajadorViewModel>();
        builder.Services.AddTransient<LineasViewModel>();


#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
