﻿#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Services;
using QuattroX.View.Helpers;

namespace QuattroX.ViewModel;


public partial class MainViewModel : BaseViewModel {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================

    private readonly DatabaseService dbService;
    private readonly ConfigService configService;

    public MainViewModel(DatabaseService dbService, ConfigService configService) {
        this.dbService = dbService;
        this.configService = configService;
        Title = "Cargando";
    }

    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================



    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos privados
    // ====================================================================================================



    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Comandos
    // ====================================================================================================


    [RelayCommand]
    async Task LoadAsync() {

        // Personalizamos los controles visuales.
        CustomizePlatformViews.CustomizeViews();

        // Poner aquí todo lo que tiene que pasar al iniciar la aplicación.
        await dbService.InitAsync();
        await configService.InitAsync();

        // Vamos a la página del calendario
        await Shell.Current.GoToAsync("///CalendarioPage");
    }


    #endregion
    // ====================================================================================================


}
