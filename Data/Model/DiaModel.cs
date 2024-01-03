#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using QuattroX.Data.Enums;
using QuattroX.Data.Interfaces;

namespace QuattroX.Data.Model;


public partial class DiaModel : ModelBase, IServicio {


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
    [NotifyPropertyChangedFor(nameof(TextoIncidencia))]
    [NotifyPropertyChangedFor(nameof(TextoServicio))]
    string linea;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextoIncidencia))]
    [NotifyPropertyChangedFor(nameof(TextoServicio))]
    string textoLinea;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextoIncidencia))]
    [NotifyPropertyChangedFor(nameof(TextoServicio))]
    string servicio;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextoIncidencia))]
    [NotifyPropertyChangedFor(nameof(TextoServicio))]
    int turno;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextoHoras))]
    [NotifyPropertyChangedFor(nameof(TextoInicio))]
    TimeSpan inicio;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextoHoras))]
    [NotifyPropertyChangedFor(nameof(TextoFinal))]
    TimeSpan final;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextoInicio))]
    string lugarInicio;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextoFinal))]
    string lugarFinal;


    [ObservableProperty]
    ObservableCollection<ServicioDiaModel> servicios = new();


    [ObservableProperty]
    decimal trabajadas;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HayHoras))]
    decimal acumuladas;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HayHoras))]
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
    [NotifyPropertyChangedFor(nameof(EsHuelgaParcial))]
    bool huelgaParcial;


    [ObservableProperty]
    decimal horasHuelga;


    [ObservableProperty]
    int relevoId;


    [ObservableProperty]
    int sustiId;


    [ObservableProperty]
    string bus;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasNotas))]
    string notas;


    #endregion
    // ====================================================================================================


    // ====================================================================================================
    #region Propiedades no enlazadas
    // ====================================================================================================


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextoRelevo))]
    int matricula;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextoRelevo))]
    string apellidos;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextoSusti))]
    int matriculaSusti;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextoSusti))]
    string apellidosSusti;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextoIncidencia))]
    [NotifyPropertyChangedFor(nameof(HaySusti))]
    [NotifyPropertyChangedFor(nameof(EsHuelga))]
    [NotifyPropertyChangedFor(nameof(EsHuelgaParcial))]
    [NotifyPropertyChangedFor(nameof(TextoHaySusti))]
    IncidenciaModel incidencia;


    public bool EsDiaPar => Fecha.Day % 2 == 0;


    public string TextoRelevo => Matricula > 0 ? $"{Matricula}: {Apellidos}" : string.Empty;


    public string TextoSusti => MatriculaSusti > 0 ? $"{MatriculaSusti}: {ApellidosSusti}" : "No hay una persona definida.";


    public string TextoHoras => Inicio == Final ? string.Empty : $"{Inicio.ToTexto()} - {Final.ToTexto()}";


    public bool HasNotas => !string.IsNullOrWhiteSpace(Notas);


    public bool HayHoras => Acumuladas != 0 || Nocturnas > 0;


    public string TextoServicio {
        get {
            if (string.IsNullOrEmpty(Linea) || string.IsNullOrEmpty(Servicio) || Turno == 0) {
                return "No hay servicio";
            } else {
                return $"{Servicio} / {Turno} - {Linea} : {TextoLinea}";
            }
        }
    }


    public string TextoInicio {
        get {
            if (string.IsNullOrEmpty(Linea) || string.IsNullOrEmpty(Servicio) || Turno == 0) {
                return string.Empty;
            } else {
                return $"{Inicio.ToTexto()} - {LugarInicio}";
            }
        }
    }


    public string TextoFinal {
        get {
            if (string.IsNullOrEmpty(Linea) || string.IsNullOrEmpty(Servicio) || Turno == 0) {
                return string.Empty;
            } else {
                return $"{Final.ToTexto()} - {LugarFinal}";
            }
        }
    }


    public string TextoIncidencia {
        get {
            if (string.IsNullOrEmpty(Linea) || string.IsNullOrEmpty(Servicio) || Turno == 0) {
                return Incidencia.Descripcion;
            }
            return TextoServicio;
        }
    }


    public bool HaySusti => Incidencia?.Comportamiento == ComportamientoIncidencia.HacemosDia || Incidencia?.Comportamiento == ComportamientoIncidencia.NosHacenDia;


    public string TextoHaySusti {
        get {
            return Incidencia?.Comportamiento switch {
                ComportamientoIncidencia.HacemosDia => "Compañer@ al que hacemos el día",
                ComportamientoIncidencia.NosHacenDia => "Compañer@ que nos hace el día",
                _ => "Compañer@",
            };
        }
    }

    public bool EsHuelga => Incidencia?.Tipo == TipoIncidencia.Huelga;


    public bool EsHuelgaParcial => EsHuelga && HuelgaParcial == true;


    public decimal DeudaDia {
        get {
            if (Incidencia?.Codigo == 11) return -1;
            if (Incidencia?.Codigo == 12) return 1;
            return 0;
        }
    }


    #endregion
    // ====================================================================================================


}
