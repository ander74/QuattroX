#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System.Globalization;

namespace QuattroX.View.Converters;


public class IndexToColorConverter : IValueConverter {

    public Color ColorPar { get; set; }

    public Color ColorImpar { get; set; }


    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

        var index = (int)value;
        return index % 2 == 0 ? ColorPar : ColorImpar;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
