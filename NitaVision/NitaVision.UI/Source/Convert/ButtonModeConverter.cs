using NitaVision.UI.Source.Style;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace NitaVision.UI.Source.Convert
{
    public class ButtonModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ButtonMode mode)
            {
                string param = parameter as string;

                if (param == "Icon")
                {
                    return mode == ButtonMode.IconOnly || mode == ButtonMode.IconAndText ? Visibility.Visible : Visibility.Collapsed;
                }
                else if (param == "Text")
                {
                    return mode == ButtonMode.TextOnly || mode == ButtonMode.IconAndText ? Visibility.Visible : Visibility.Collapsed;
                }
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
