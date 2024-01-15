#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

using QuattroX.Data.Helpers;

namespace QuattroX.ViewModel;


public partial class LicenciaViewModel : BaseViewModel {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================


    public LicenciaViewModel() {
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    string textoLicencia;


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos públicos
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
        using var stream = await FileSystem.OpenAppPackageFileAsync("licencia_gpl.txt");
        using var reader = new StreamReader(stream);
        TextoLicencia = reader.ReadToEnd();
    }


    [RelayCommand]
    async Task AceptarLicenciaAsync() {
        Preferences.Default.Set(SettingsNames.LICENCIA_ACEPTADA, true);
        await Shell.Current.GoToAsync("///CalendarioPage");
    }


    [RelayCommand]
    async Task CancelarAsync() {
        var resultado = await Shell.Current.DisplayAlert("ATENCIÓN",
            "Si no aceptas la licencia no podrás usar Quattro X.", "Volver", "Salir");
        if (!resultado) Application.Current.Quit();
    }


    #endregion
    // ====================================================================================================


}
