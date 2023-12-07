#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System.Collections.ObjectModel;

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
    int incidenciaId; //Puede no ser necesario


    [ObservableProperty]
    IncidenciaModel incidencia;


    [ObservableProperty]
    int servicioPrincipalId; //Puede no ser necesario


    [ObservableProperty]
    ServicioDiaModel servicioPrincipal;


    [ObservableProperty]
    ObservableCollection<ServicioDiaModel> servicios;


    [ObservableProperty]
    ObservableCollection<RegulacionModel> regulaciones;


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
    int relevoId; //Puede no ser necesario


    [ObservableProperty]
    TrabajadorModel relevo;


    [ObservableProperty]
    int sustiId; //Puede no ser necesario


    [ObservableProperty]
    TrabajadorModel susti;


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
