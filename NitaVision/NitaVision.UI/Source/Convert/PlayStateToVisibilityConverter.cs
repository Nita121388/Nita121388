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
    public class PlayStateToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PlayState playState = (PlayState)value;
            var flag = Visibility.Visible;
            if (playState == PlayState.Play)
            {
                flag = Visibility.Visible;
            }
            else if (playState == PlayState.Unknow || playState == PlayState.Pause || playState == PlayState.Stop)
            {
                flag = Visibility.Collapsed;
            }
            else
            {
                flag = Visibility.Hidden;
            }
            if (parameter != null && parameter.ToString() == "Inverse")
            {
                if (flag == Visibility.Visible) { flag = Visibility.Collapsed; }
                if (flag == Visibility.Collapsed) { flag = Visibility.Visible; }
            }
            return flag;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public enum PlayState
    {
        Unknow = 0,
        Play = 1,
        Pause = 2,
        Stop = 3
    }
}
