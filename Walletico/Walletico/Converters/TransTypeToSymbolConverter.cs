using System;
using System.Globalization;
using Xamarin.Forms;

namespace Walletico.Converters
{
    public class TransTypeToSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((byte)value == 0) ? '+' : '-';
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
