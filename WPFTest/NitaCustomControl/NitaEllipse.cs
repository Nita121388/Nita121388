using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using WPFTest.Enity;

namespace WPFTest.NitaCustomControl
{
    public class NitaEllipse : ContentControl
    {
        #region Contructor

        static NitaEllipse()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaEllipse), new FrameworkPropertyMetadata(typeof(NitaEllipse)));
        }
        #endregion

        #region Fields

        #endregion

        #region DependencyProperty

        #region Color

        public string Color
        {
            get { return (string)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(string), typeof(NitaEllipse), new PropertyMetadata("#DCDCDC"));
        #endregion

        #region SizeType
        public SizeType SizeType
        {
            get { return (SizeType)GetValue(SizeTypeProperty); }
            set { SetValue(SizeTypeProperty, value); }
        }

        public static readonly DependencyProperty SizeTypeProperty =
            DependencyProperty.Register("SizeType", typeof(SizeType), typeof(NitaEllipse), new PropertyMetadata(SizeType.Medium));
        #endregion

        #endregion
    }
}
