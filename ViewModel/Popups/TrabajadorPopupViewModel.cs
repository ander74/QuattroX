#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

using QuattroX.Data.Model;
using QuattroX.Data.Repositories;

namespace QuattroX.ViewModel.Popups;
public partial class TrabajadorPopupViewModel : BaseViewModel {


    // ====================================================================================================
    #region Campos privados y constructor
    // ====================================================================================================


    private readonly DbRepository dbRepository;


    public TrabajadorPopupViewModel(DbRepository dbRepository) {
        Title = "Nuevo trabajador";
        this.dbRepository = dbRepository;
    }

    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    TrabajadorModel trabajador = new();


    [ObservableProperty]
    ObservableCollection<TrabajadorModel> trabajadores;


    [ObservableProperty]
    TrabajadorModel trabajadorSeleccionado;

    partial void OnTrabajadorSeleccionadoChanged(TrabajadorModel value) {
        if (value is null) {
            IsNuevoTrabajador = false;
            return;
        }
        if (value.Matricula == 0) {
            IsNuevoTrabajador = true;
        } else {
            IsNuevoTrabajador = false;
        }
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotNuevoTrabajador))]
    bool isNuevoTrabajador;


    public bool IsNotNuevoTrabajador => !IsNuevoTrabajador;


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Métodos públicos
    // ====================================================================================================

    public async Task InitAsync() {
        Trabajadores = await dbRepository.GetTrabajadoresAsync();
        Trabajadores.Insert(0, new TrabajadorModel { Matricula = 0 });
    }

    public async Task AceptarAsync() {
        if (IsNuevoTrabajador && TrabajadorSeleccionado.Matricula == 0) {
            TrabajadorSeleccionado = null;
            return;
        }
        if (!(await dbRepository.ExisteTrabajadorByMatriculaAsync(TrabajadorSeleccionado.Matricula))) {
            await dbRepository.SaveTrabajadorAsync(TrabajadorSeleccionado);
        }
        Trabajador = TrabajadorSeleccionado;
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
