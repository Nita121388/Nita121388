using NitaVision.UI.Source.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NitaVision.UI.Source.Convert
{
    internal class SizeTypeToSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SizeType sizeType)
            {
                switch (sizeType)
                {
                    case SizeType.Small:
                        return new Size(20, 20);
                    case SizeType.Medium:
                        return new Size(25, 25);
                    case SizeType.Large:
                        return new Size(30, 30);
                    default:
                        return new Size(0, 0);
                }
            }
            else
            {
                throw new ArgumentException("value must be a SizeType");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Size size)
            {
                if (size.Width == 20 && size.Height == 20)
                {
                    return SizeType.Small;
                }
                else if (size.Width == 25 && size.Height == 25)
                {
                    return SizeType.Medium;
                }
                else if (size.Width == 30 && size.Height == 30)
                {
                    return SizeType.Large;
                }
                else
                {
                    return default(SizeType);
                }
            }
            else
            {
                throw new ArgumentException("value must be a Size");
            }
        }
    }
}
