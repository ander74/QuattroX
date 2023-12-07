#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

namespace QuattroX.Data.Model;


public partial class OpcionesModel : ModelBase {


    [ObservableProperty]
    DateTime primerMesMostrado;


    [ObservableProperty]
    decimal acumuladasAnteriores;


    [ObservableProperty]
    int relevoFijo;


    [ObservableProperty]
    bool rellenarSemanaAutomaticamente;


    [ObservableProperty]
    decimal jornadaMedia;


    [ObservableProperty]
    decimal jornadaMinima;


    [ObservableProperty]
    int jornadaAnual;


    [ObservableProperty]
    int limiteEntreServicios;


    [ObservableProperty]
    bool regularJornadaAnual;


    [ObservableProperty]
    bool regularAñosBisiestos;


    [ObservableProperty]
    TimeSpan inicioNocturnas;


    [ObservableProperty]
    TimeSpan finalNocturnas;


    [ObservableProperty]
    TimeSpan horaLimiteDesayuno;


    [ObservableProperty]
    TimeSpan horaLimiteComida1;


    [ObservableProperty]
    TimeSpan horaLimiteComida2;


    [ObservableProperty]
    TimeSpan horaLimiteCena;


    [ObservableProperty]
    bool inferirTurnos;


    [ObservableProperty]
    DateTime fechaReferenciaTurnos;


    [ObservableProperty]
    bool acumularTomaDeje;


}
