#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Messages;
using QuattroX.Data.Model;

namespace QuattroX.ViewModel.Popups;
public partial class TrabajadorPopupViewModel : BaseViewModel {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================


    public TrabajadorPopupViewModel() {
        Title = "Nuevo trabajador";
        Trabajadores = Messenger.Send(new TrabajadoresRequest());
    }

    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    TrabajadorModel trabajador = new();

    partial void OnTrabajadorChanged(TrabajadorModel value) {
        if (Trabajador is null) return;
        Trabajador.PropertyChanged += (s, e) => {
            if (TrabajadorModificado) return;
            if (e.PropertyName == nameof(TrabajadorModel.Matricula)) {
                Trabajador.Nombre = string.Empty;
                Trabajador.Apellidos = string.Empty;
                TrabajadorSeleccionado = null;
                TrabajadorModificado = true;
            }
            // De esta manera (los parciales de linea y servicio seleccionados) sólo cuando se edita manualmente el servicio
            // se produce el cambio en esta propiedad.
            // Usemoslo para determinar si un servicio es editado o cogido de la base de datos.

        };
    }

    [ObservableProperty]
    ObservableCollection<TrabajadorModel> trabajadores;


    [ObservableProperty]
    TrabajadorModel trabajadorSeleccionado;

    partial void OnTrabajadorSeleccionadoChanged(TrabajadorModel value) {
        if (value is null) return;
        Trabajador = new TrabajadorModel {
            Id = TrabajadorSeleccionado.Id,
            Matricula = TrabajadorSeleccionado.Matricula,
            Nombre = TrabajadorSeleccionado.Nombre,
            Apellidos = TrabajadorSeleccionado.Apellidos,
        };
        TrabajadorModificado = false;
    }


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NotTrabajadorModificado))]
    bool trabajadorModificado;


    public bool NotTrabajadorModificado => !TrabajadorModificado;

    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos públicos
    // ====================================================================================================


    public void OnClose() {
        if (TrabajadorModificado) {
            TrabajadorModel trabajador = Trabajadores.FirstOrDefault(t => t.Matricula == Trabajador.Matricula);
            if (trabajador is null) {
                var newtrabajador = new TrabajadorModel {
                    Matricula = Trabajador.Matricula,
                    Nombre = Trabajador.Nombre,
                    Apellidos = Trabajador.Apellidos,
                };
                Messenger.Send(new AddTrabajadorRequest(newtrabajador));
            }
        }
    }

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


    #endregion
    // ====================================================================================================


}
