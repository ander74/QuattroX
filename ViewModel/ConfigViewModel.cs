﻿#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using CommunityToolkit.Maui.Core;
using QuattroX.Data.Model;
using QuattroX.Services;
using QuattroX.ViewModel.Popups;

namespace QuattroX.ViewModel;


public partial class ConfigViewModel : BaseViewModel {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================


    private readonly IPopupService popupService;


    public ConfigViewModel(ConfigService configService, IPopupService popupService) {

        Title = "Configuración";
        ConfigService = configService;
        this.popupService = popupService;
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================

    public ConfigService ConfigService { get; }



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
    async Task CloseAsync() {
        await ConfigService.SaveAsync();
    }


    [RelayCommand]
    async Task BackAsync() {
        await Shell.Current.GoToAsync("///CalendarioPage");
    }


    [RelayCommand]
    async Task GoToOpcionesGeneralesAsync() {
        await Shell.Current.GoToAsync("OpcionesGeneralesPage");
    }


    [RelayCommand]
    async Task GoToConvenioAsync() {
        await Shell.Current.GoToAsync("OpcionesConvenioPage");
    }


    [RelayCommand]
    async Task CambiarJornadaMediaAsync() {
        var resultado = await Shell.Current.DisplayPromptAsync("Jornada media",
            "Introduce la jornada media en decimal.", "Aceptar", "Cancelar",
            null, -1, Keyboard.Numeric, ConfigService.Opciones.JornadaMedia.ToTexto());
        if (resultado is null) return;
        ConfigService.Opciones.JornadaMedia = resultado.ToDecimal();
    }


    [RelayCommand]
    async Task CambiarJornadaMinimaAsync() {
        var resultado = await Shell.Current.DisplayPromptAsync("Jornada mínima",
            "Introduce la jornada mínima en decimal.", "Aceptar", "Cancelar",
            null, -1, Keyboard.Numeric, ConfigService.Opciones.JornadaMinima.ToTexto());
        if (resultado is null) return;
        ConfigService.Opciones.JornadaMinima = resultado.ToDecimal();
    }


    [RelayCommand]
    async Task CambiarJornadaAnualAsync() {
        var resultado = await Shell.Current.DisplayPromptAsync("Jornada anual",
            "Introduce la jornada anual en horas.", "Aceptar", "Cancelar",
            null, -1, Keyboard.Numeric, ConfigService.Opciones.JornadaAnual.ToString());
        if (resultado is null) return;
        if (int.TryParse(resultado, out int valor)) {
            ConfigService.Opciones.JornadaAnual = valor;
        }
    }


    [RelayCommand]
    async Task CambiarLimiteServiciosAsync() {
        var resultado = await Shell.Current.DisplayPromptAsync("Límite entre servicios",
            "Introduce el límite entre servicios en minutos.", "Aceptar", "Cancelar",
            null, -1, Keyboard.Numeric, ConfigService.Opciones.LimiteEntreServicios.ToString());
        if (resultado is null) return;
        if (int.TryParse(resultado, out int valor)) {
            ConfigService.Opciones.LimiteEntreServicios = valor;
        }
    }


    [RelayCommand]
    async Task CambiarAcumuladasAnterioresAsync() {
        var resultado = await Shell.Current.DisplayPromptAsync("Acumuladas anteriores",
            "Introduce las horas acumuladas anteriores en decimal.", "Aceptar", "Cancelar",
            null, -1, Keyboard.Numeric, ConfigService.Opciones.AcumuladasAnteriores.ToTexto());
        if (resultado is null) return;
        ConfigService.Opciones.AcumuladasAnteriores = resultado.ToDecimal();
    }


    [RelayCommand]
    async Task CambiarRelevoFijoAsync() {
        try {
            var resultado = await popupService.ShowPopupAsync<TrabajadorPopupViewModel>(async vm => {
                vm.Title = "Relevo fijo";
                TrabajadorModel trab = vm.Trabajadores.FirstOrDefault(t => t.Matricula == ConfigService.Opciones.RelevoFijo);
                if (trab is null) trab = new();
                vm.TrabajadorSeleccionado = trab;
            });
            if (resultado is TrabajadorModel model) {
                ConfigService.Opciones.RelevoFijo = model.Matricula;
            }
        } catch (Exception ex) {
            await Shell.Current.DisplaySnackbar(ex.Message);
        } finally {
            IsBusy = false;
        }
    }


    #endregion
    // ====================================================================================================


}
