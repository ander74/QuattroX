#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Enums;

namespace QuattroX.Data.Model;


public partial class TrabajadorModel : ModelBase {


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    int matricula;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    string nombre;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    string apellidos;


    [ObservableProperty]
    string telefono;


    [ObservableProperty]
    string email;


    [ObservableProperty]
    CalificacionTrabajador calificacion;


    [ObservableProperty]
    int deudaInicial;


    [ObservableProperty]
    string notas;


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades no enlazadas
    // ====================================================================================================


    public string NombreCompleto => $"{Nombre} {Apellidos}";


    public int RowIndex { get; set; }



    #endregion
    // ====================================================================================================


}
