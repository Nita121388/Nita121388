using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace NitaVision.UI.Source.Convert
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string colorString)
            {
                if (ColorConverter.TryParseColor(colorString, out Color color))
                {
                    return new SolidColorBrush(color);
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush brush)
            {
                Color color = brush.Color;
                return $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
            }
            return null;
        }

        private static bool TryParseColor(string colorString, out Color color)
        {
            color = default;
            if (colorString.StartsWith("#") && (colorString.Length == 7 || colorString.Length == 9))
            {
                try
                {
                    int a = colorString.Length == 9 ? System.Convert.ToInt32(colorString.Substring(1, 2), 16) : 255;
                    int r = System.Convert.ToInt32(colorString.Substring(colorString.Length - 6, 2), 16);
                    int g = System.Convert.ToInt32(colorString.Substring(colorString.Length - 4, 2), 16);
                    int b = System.Convert.ToInt32(colorString.Substring(colorString.Length - 2, 2), 16);
                    color = Color.FromArgb((byte)a, (byte)r, (byte)g, (byte)b);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
