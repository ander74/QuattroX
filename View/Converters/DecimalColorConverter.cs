#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System.Globalization;

namespace QuattroX.View.Converters;


public class DecimalColorConverter : IValueConverter {

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

        var valor = (decimal)value;
        if (valor > 0) return Colors.DarkGreen;
        if (valor < 0) return Colors.DarkRed;
        return Colors.Black;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}