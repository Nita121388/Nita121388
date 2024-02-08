using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using WPFTest.Enity;

namespace WPFTest.Converter
{
    public class ButtonStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var buttonStyle = (ButtonStyle)value;
            switch (buttonStyle)
            {
                case ButtonStyle.Default:
                    return Application.Current.FindResource("DefaultMargin");
                case ButtonStyle.Flat:
                    return Application.Current.FindResource("FlatMargin");
                default:
                    throw new NotSupportedException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
