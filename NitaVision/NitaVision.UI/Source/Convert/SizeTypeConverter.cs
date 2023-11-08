using NitaVision.UI.Source.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace NitaVision.UI.Source.Convert
{
    internal class SizeTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SizeType sizeType)
            {
                // Convert parameter to string
                string param = parameter as string;
                if (param == null)
                {
                    throw new ArgumentException("parameter must be a string");
                }
                // Concatenate parameter with sizeType
                string resourceName = sizeType.ToString() + param;
                object resource = Application.Current.FindResource(resourceName);
                return (double)resource;
            }
            else
            {
                throw new ArgumentException("value must be a SizeType");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                // Convert parameter to string
                string param = parameter as string;
                if (param == null)
                {
                    throw new ArgumentException("parameter must be a string");
                }
                foreach (SizeType sizeType in Enum.GetValues(typeof(SizeType)))
                {
                    // Concatenate parameter with sizeType
                    string resourceName = sizeType.ToString() + param;
                    object resource = Application.Current.FindResource(resourceName);
                    if (resource is double resourceValue && resourceValue == doubleValue)
                    {
                        return sizeType;
                    }
                }
                throw new ArgumentException("value must match a SizeType resource");
            }
            else
            {
                throw new ArgumentException("value must be a double");
            }
        }
    }

}
