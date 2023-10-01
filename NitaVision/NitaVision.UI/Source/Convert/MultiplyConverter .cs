using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NitaVision.UI.Source.Convert
{
    // 这个转换器用于从绑定值中乘以一个值。
    public class MultiplyConverter : IValueConverter
    {
        // Convert方法将绑定值转换为目标值。
        // 它接受绑定值、目标类型、参数和文化作为输入。
        // 它返回转换后的值。
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 检查输入值和参数是否是有效的数字。
            if (value is double && parameter is double)
            {
                // 将输入值乘以参数并返回结果。
                return (double)value * (double)parameter;
            }
            else
            {
                // 如果输入值不是有效的数字，就返回输入值。
                return value;
            }
        }

        // ConvertBack方法将目标值转换回绑定值。
        // 它接受目标值、绑定类型、参数和文化作为输入。
        // 它返回转换后的值。
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 检查输入值和参数是否是有效的数字。
            if (value is double && parameter is double)
            {
                // 如果参数不等于零，就将输入值除以参数并返回结果。
                if ((double)parameter != 0)
                {
                    return (double)value / (double)parameter;
                }
                else
                {
                    // 如果参数等于零，就返回输入值。
                    return value;
                }
            }
            else
            {
                // 如果输入值不是有效的数字，就返回输入值。
                return value;
            }
        }
    }
}
