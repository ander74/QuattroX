#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System.Globalization;

namespace QuattroX.View.Converters;


public class DecimalConverter : IValueConverter {


    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        if (value is decimal valor) {
            return valor.ToTexto();
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        if (value is string valor) {
            return valor.ToDecimal();
        }
        return 0m;
    }
}
