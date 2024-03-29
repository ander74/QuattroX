﻿#region COPYRIGHT
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


public partial class ServicioBasePopupViewModel : BaseViewModel {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================

    private bool servicioModificado;

    public ServicioBasePopupViewModel() {
        Title = "Nuevo servicio";
        Lineas = Messenger.Send(new LineasRequest(true));
    }

    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    ServicioBaseModel servicio = new();

    partial void OnServicioChanged(ServicioBaseModel value) {
        if (Servicio is null) return;
        Servicio.PropertyChanged += (s, e) => {
            if (servicioModificado) return;
            if (e.PropertyName == nameof(ServicioBaseModel.Modified)) return;
            if (e.PropertyName == nameof(ServicioBaseModel.Linea)) {
                Servicio.TextoLinea = string.Empty;
            }
            ServicioSeleccionado = null;
            LineaSeleccionada = null;
            servicioModificado = true;
            // De esta manera (los parciales de linea y servicio seleccionados) sólo cuando se edita manualmente el servicio
            // se produce el cambio en esta propiedad.
            // Usemoslo para determinar si un servicio es editado o cogido de la base de datos.
        };
    }


    [ObservableProperty]
    ObservableCollection<LineaModel> lineas;


    [ObservableProperty]
    LineaModel lineaSeleccionada;

    partial void OnLineaSeleccionadaChanged(LineaModel value) {
        if (value is null) return;
        Servicio = new ServicioBaseModel {
            Linea = LineaSeleccionada.Linea,
            TextoLinea = LineaSeleccionada.Texto,
        };
        servicioModificado = false;
    }


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NotBloquearLinea))]
    bool bloquearLinea;


    public bool NotBloquearLinea => !BloquearLinea;


    [ObservableProperty]
    ServicioLineaModel servicioSeleccionado;

    partial void OnServicioSeleccionadoChanged(ServicioLineaModel value) {
        if (value is null) return;
        Servicio = ServicioSeleccionado.ToServicioBaseModel();
        servicioModificado = false;
    }


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos públicos
    // ====================================================================================================


    public void OnClose() {
        if (servicioModificado) {
            var servicioLinea = new ServicioLineaModel();
            servicioLinea.FromServicioBase(Servicio);
            LineaModel linea = Messenger.Send(new LineaRequest(servicioLinea.Linea));
            if (linea is null) {
                var newLinea = new LineaModel {
                    Linea = servicioLinea.Linea,
                    Texto = servicioLinea.TextoLinea
                };
                newLinea.Servicios.Add(servicioLinea);
                Messenger.Send(new AddLineaRequest(newLinea));
                return;
            }
            servicioLinea.LineaId = linea.Id;
            if (string.IsNullOrWhiteSpace(servicioLinea.TextoLinea)) servicioLinea.TextoLinea = linea.Texto;
            var serv = linea.Servicios.FirstOrDefault(s => s.Servicio.ToUpper() == servicioLinea.Servicio.ToUpper() && s.Turno == servicioLinea.Turno);
            if (serv is null) {
                linea.Servicios.Add(servicioLinea);
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
