#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace QuattroX.Data.Model;


public partial class DiaModel : ModelBase {


    // ====================================================================================================
    #region Propiedades
    // ====================================================================================================


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(EsDiaPar))]
    DateTime fecha;


    [ObservableProperty]
    bool esFranqueo;


    [ObservableProperty]
    bool esFestivo;


    [ObservableProperty]
    int incidenciaId;


    [ObservableProperty]
    IncidenciaModel incidencia = new();


    [ObservableProperty]
    int servicioPrincipalId;


    [ObservableProperty]
    ServicioDiaModel servicioPrincipal = new();


    [ObservableProperty]
    ObservableCollection<ServicioSecundarioDiaModel> servicios = new();


    [ObservableProperty]
    ObservableCollection<RegulacionModel> regulaciones = new();


    [ObservableProperty]
    decimal trabajadas;


    [ObservableProperty]
    decimal acumuladas;


    [ObservableProperty]
    decimal nocturnas;


    [ObservableProperty]
    bool desayuno;


    [ObservableProperty]
    bool comida;


    [ObservableProperty]
    bool cena;


    [ObservableProperty]
    decimal tomaDeje;


    [ObservableProperty]
    decimal euros;


    [ObservableProperty]
    bool huelgaParcial;


    [ObservableProperty]
    decimal horasHuelga;


    [ObservableProperty]
    int relevoId;


    [ObservableProperty]
    TrabajadorModel relevo = new();


    [ObservableProperty]
    int sustiId;


    [ObservableProperty]
    TrabajadorModel susti = new();


    [ObservableProperty]
    string bus;


    [ObservableProperty]
    string notas;


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades no enlazadas
    // ====================================================================================================


    public bool EsDiaPar => Fecha.Day % 2 == 0;


    #endregion
    // ====================================================================================================


}
