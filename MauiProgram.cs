using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using QuattroX.Data.Repositories;
using QuattroX.Services;
using QuattroX.View;
using QuattroX.View.Popups;
using QuattroX.ViewModel;
using QuattroX.ViewModel.Popups;

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


        // Páginas generales
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<CalendarioPage>();
        builder.Services.AddSingleton<TrabajadoresPage>();
        builder.Services.AddSingleton<LineasPage>();
        builder.Services.AddSingleton<ConfigPage>();
        builder.Services.AddSingleton<OpcionesConvenioPage>();
        builder.Services.AddSingleton<OpcionesGeneralesPage>();

        // Páginas detalle
        builder.Services.AddTransient<DetalleDiaPage>();
        builder.Services.AddTransient<DetalleTrabajadorPage>();
        builder.Services.AddTransient<DetalleLineaPage>();
        builder.Services.AddTransient<DetalleServicioLineaPage>();


        // ViewModels generales
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<CalendarioViewModel>();
        builder.Services.AddSingleton<TrabajadoresViewModel>();
        builder.Services.AddSingleton<ConfigViewModel>();
        builder.Services.AddSingleton<LineasViewModel>();

        // ViewModels detalle
        builder.Services.AddTransient<DetalleDiaViewModel>();
        builder.Services.AddTransient<DetalleTrabajadorViewModel>();
        builder.Services.AddTransient<DetalleLineaViewModel>();
        builder.Services.AddTransient<DetalleServicioLineaViewModel>();

        // Popups
        builder.Services.AddTransientPopup<ServicioBasePopup, ServicioBasePopupViewModel>();
        builder.Services.AddTransientPopup<TrabajadorPopup, TrabajadorPopupViewModel>();
        builder.Services.AddTransientPopup<ServicioLineaPopup, ServicioLineaPopupViewModel>();


#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
