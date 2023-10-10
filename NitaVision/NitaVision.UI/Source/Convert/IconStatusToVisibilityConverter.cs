using NitaVision.UI.Source.CoreUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Shapes;

namespace NitaVision.UI.Source.Convert
{
    public class IconStatusToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
        { 
            if (value is IconStatusMode mode && parameter is string type) 
            { 
                switch (mode) 
                { 
                    case IconStatusMode.IconText: 
                        if (type == "Icon" || type == "Text") 
                        { 
                            return Visibility.Visible; 
                        } 
                        else 
                        { 
                            return Visibility.Collapsed; 
                        } 
                    case IconStatusMode.EllipseText: 
                        if (type == "Ellipse" || type == "Text") 
                        { 
                            return Visibility.Visible; 
                        } 
                        else 
                        { 
                            return Visibility.Collapsed; 
                        } 
                    default: 
                        return Visibility.Collapsed; 
                } 
            } 
            else 
            { 
                return Visibility.Collapsed; 
            } 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
