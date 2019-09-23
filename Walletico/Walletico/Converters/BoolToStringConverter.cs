using System;
using System.Globalization;
using Xamarin.Forms;

namespace Walletico.Converters
{
    public class BoolToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string parameterString = parameter as string;

            string[] parameters = parameterString.Split(new char[] { '|' });
            if ((bool)value)
            {
                return parameters[0];
            }
            return parameters[1];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
