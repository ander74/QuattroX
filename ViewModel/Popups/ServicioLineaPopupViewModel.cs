﻿#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

using QuattroX.Data.Model;
using QuattroX.Data.Repositories;

namespace QuattroX.ViewModel.Popups;


public partial class ServicioLineaPopupViewModel : BaseViewModel {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================


    private readonly DbRepository dbRepository;


    public ServicioLineaPopupViewModel(DbRepository dbRepository) {
        Title = "Nuevo servicio";
        this.dbRepository = dbRepository;
    }

    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    ServicioLineaModel servicio = new();


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


    #endregion
    // ====================================================================================================


}
