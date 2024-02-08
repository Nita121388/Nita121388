using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WPFTest.Enity;

namespace WPFTest.Converter
{
    public class ButtonStyleToBorderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ButtonStyle bs)
            {
                if (bs == ButtonStyle.Flat)
                {
                    return Application.Current.FindResource("FlatBorder");
                }
                return Application.Current.FindResource("DefaultBorder") ;
            }
            else
            {
                throw new ArgumentException("value must be a ButtonStyle");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
