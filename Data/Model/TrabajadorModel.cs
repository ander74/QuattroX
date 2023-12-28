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
    [NotifyPropertyChangedFor(nameof(DiasDeuda))]
    [NotifyPropertyChangedFor(nameof(TextoDeuda))]
    int deudaInicial;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HayNotas))]
    string notas;


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades no enlazadas
    // ====================================================================================================


    public string NombreCompleto => $"{Nombre} {Apellidos}";


    public int RowIndex { get; set; }


    public bool HayNotas => !string.IsNullOrWhiteSpace(Notas);


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DiasDeuda))]
    [NotifyPropertyChangedFor(nameof(TextoDeuda))]
    int diasMeHace;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DiasDeuda))]
    [NotifyPropertyChangedFor(nameof(TextoDeuda))]
    int diasLeHago;


    public decimal DiasDeuda => DeudaInicial + DiasLeHago - DiasMeHace;


    public string TextoDeuda {
        get {
            return DiasDeuda switch {
                1 => $"Me debe un día",
                > 1 => $"Me debe {DiasDeuda} días",
                -1 => $"Le debo un día",
                < -1 => $"Le debo {-DiasDeuda} días",
                _ => "",
            };
        }
    }



    #endregion
    // ====================================================================================================


}
