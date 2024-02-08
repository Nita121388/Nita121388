using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTest.NitaCustomControl
{
    public class NitaIconSample:ContentControl
    {
        static NitaIconSample()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaIconSample), new FrameworkPropertyMetadata(typeof(NitaIconSample)));
        }



        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(NitaIconSample), new PropertyMetadata());


        public string IconKey
        {
            get { return (string)GetValue(IconKeyProperty); }
            set { SetValue(IconKeyProperty, value); }
        }

        public static readonly DependencyProperty IconKeyProperty =
            DependencyProperty.Register("IconKey", typeof(string), typeof(NitaIconSample), new PropertyMetadata(""));


    }
}
