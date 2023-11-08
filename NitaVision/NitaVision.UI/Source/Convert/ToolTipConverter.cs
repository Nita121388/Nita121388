using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NitaVision.UI.Source.Convert
{
    public class ToolTipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //value是NitaButton的ToolTip属性的值
            //parameter是NitaButton的Text属性的值
            //如果value为空或空字符串，返回parameter，否则返回value
            if (string.IsNullOrEmpty(value as string))
            {
                return parameter;
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
