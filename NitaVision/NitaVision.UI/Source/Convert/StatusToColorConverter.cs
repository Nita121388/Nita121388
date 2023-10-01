using NitaVision.SPI.Constant;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace NitaVision.UI.Source.Convert
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Status status)
            {
                switch (status)
                {
                    case Status.Undo:
                        return Brushes.Gray; //灰色
                    case Status.Resolving:
                        return Brushes.Yellow; //黄色
                    case Status.Failed:
                        return Brushes.Red; //红色
                    case Status.Resolved:
                        return Brushes.Green; //绿色
                    case Status.New:
                        return Brushes.Blue; //蓝色
                    case Status.Ing:
                        return Brushes.Orange; //橙色
                    case Status.Playing:
                        return Brushes.Purple; //紫色
                    case Status.Down:
                        return Brushes.Black; //黑色
                    default:
                        return Brushes.White; //白色
                }
            }
            else
            {
                return Brushes.White; //白色
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
}
