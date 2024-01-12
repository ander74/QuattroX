#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Helpers;
using QuattroX.Data.Messages;
using QuattroX.Data.Model;

namespace QuattroX.ViewModel.Popups;


public partial class ServicioDiaPopupViewModel : BaseViewModel {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================

    private bool servicioModificado;

    public ServicioDiaPopupViewModel() {
        Title = "Nuevo servicio";
        Lineas = Messenger.Send(new LineasRequest(true));
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HayServiciosLinea))]
    ServicioLineaModel servicio = new();

    partial void OnServicioChanged(ServicioLineaModel oldValue, ServicioLineaModel newValue) {
        if (Servicio is null) return;
        Servicio.PropertyChanged += (s, e) => {
            if (servicioModificado) return;
            if (e.PropertyName == nameof(ServicioLineaModel.Modified)) return;
            if (e.PropertyName == nameof(ServicioLineaModel.Linea)) {
                Servicio.TextoLinea = string.Empty;
            }
            Servicio.Servicios.Clear();
            ServicioSeleccionado = null;
            LineaSeleccionada = null;
            OnPropertyChanged(nameof(HayServiciosLinea));
            servicioModificado = true;
            // De esta manera (los parciales de linea y servicio seleccionados) sólo cuando se edita manualmente el servicio
            // se produce el cambio en esta propiedad.
            // Usemoslo para determinar si un servicio es editado o cogido de la base de datos.
        };
    }


    public bool HayServiciosLinea => Servicio.Servicios.Count > 0;


    [ObservableProperty]
    ObservableCollection<LineaModel> lineas;


    [ObservableProperty]
    LineaModel lineaSeleccionada;

    partial void OnLineaSeleccionadaChanged(LineaModel value) {
        if (value is null) return;
        Servicio = new ServicioLineaModel {
            LineaId = LineaSeleccionada.Id,
            Linea = LineaSeleccionada.Linea,
            TextoLinea = LineaSeleccionada.Texto,
        };
        servicioModificado = false;
    }


    [ObservableProperty]
    ServicioLineaModel servicioSeleccionado;

    partial void OnServicioSeleccionadoChanged(ServicioLineaModel value) {
        if (value is null) return;
        Servicio = ServicioSeleccionado.ToServicioLineaModel();
        servicioModificado = false;
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos públicos
    // ====================================================================================================


    public void OnClose() {
        if (servicioModificado) {
            LineaModel linea = Messenger.Send(new LineaRequest(Servicio.Linea));
            if (linea is null) {
                var newLinea = new LineaModel {
                    Linea = Servicio.Linea,
                    Texto = Servicio.TextoLinea
                };
                newLinea.Servicios.Add(Servicio);
                Messenger.Send(new AddLineaRequest(newLinea));
                return;
            }
            Servicio.LineaId = linea.Id;
            if (string.IsNullOrWhiteSpace(Servicio.TextoLinea)) Servicio.TextoLinea = linea.Texto;
            var serv = linea.Servicios.FirstOrDefault(s => s.Servicio.ToUpper() == Servicio.Servicio.ToUpper() && s.Turno == Servicio.Turno);
            if (serv is null) {
                linea.Servicios.Add(Servicio);
                Messenger.Send(new AddLineaRequest(linea));
                return;
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
