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
    public class ButtonModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ContentMode mode)
            {
                string param = parameter as string;

                if (param == "Icon")
                {
                    return mode == ContentMode.IconOnly 
                        || mode == ContentMode.IconAndText 
                        ? Visibility.Visible : Visibility.Collapsed;
                }
                else if (param == "Color")
                {
                    return mode == ContentMode.ColorOnly 
                        || mode == ContentMode.ColorAndText 
                        ? Visibility.Visible : Visibility.Collapsed;
                }
                else if (param == "Text")
                {
                    return mode == ContentMode.TextOnly 
                        || mode == ContentMode.IconAndText
                        || mode == ContentMode.ColorAndText
                        ? Visibility.Visible : Visibility.Collapsed;
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
