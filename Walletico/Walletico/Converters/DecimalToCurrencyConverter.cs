using System;
using System.Globalization;
using Xamarin.Forms;

namespace Walletico.Converters
{
    public class DecimalToCurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal amount = (value is null) ? default : decimal.Parse(value.ToString());
            return $"₡ {amount.ToString("F2")}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
