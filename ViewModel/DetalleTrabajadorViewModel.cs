#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

using QuattroX.Data.Enums;
using QuattroX.Data.Helpers;
using QuattroX.Data.Model;
using QuattroX.Data.Repositories;
namespace QuattroX.ViewModel;


[QueryProperty(nameof(Trabajador), "Trabajador")]
public partial class DetalleTrabajadorViewModel : BaseViewModel {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================

    private readonly DbRepository dbRepository;

    public DetalleTrabajadorViewModel(DbRepository dbRepository) {
        this.dbRepository = dbRepository;
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    TrabajadorModel trabajador;
    partial void OnTrabajadorChanged(TrabajadorModel oldValue, TrabajadorModel newValue) {
        if (Trabajador is null) return;
        Title = Trabajador.Matricula.ToString();
        Trabajador.PropertyChanged += (s, e) => {
            if (e.PropertyName == nameof(Trabajador.Telefono)) LlamarCommand.NotifyCanExecuteChanged();
            if (e.PropertyName == nameof(Trabajador.Email)) MandarEmailCommand.NotifyCanExecuteChanged();
        };
    }


    public Array Calificaciones => Enum.GetValues(typeof(CalificacionTrabajador));


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
        await dbRepository.SaveTrabajadorAsync(Trabajador.ToEntity());
    }


    [RelayCommand(CanExecute = nameof(CanMandarEmail))]
    async Task MandarEmailAsync() {
        if (Email.Default.IsComposeSupported) {
            var message = new EmailMessage {
                Subject = "From Quattro X",
                Body = string.Empty,
                BodyFormat = EmailBodyFormat.PlainText,
                To = [Trabajador.Email],
            };
            await Email.Default.ComposeAsync(message);
        }
    }
    private bool CanMandarEmail() => !string.IsNullOrWhiteSpace(Trabajador?.Email);


    [RelayCommand(CanExecute = nameof(CanLlamar))]
    void Llamar() {
        if (PhoneDialer.Default.IsSupported) {
            PhoneDialer.Default.Open(Trabajador.Telefono);
        }
    }

    private bool CanLlamar() => !string.IsNullOrWhiteSpace(Trabajador?.Telefono);


    #endregion
    // ====================================================================================================


}
