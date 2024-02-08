using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFTest.Enity;

namespace WPFTest.NitaCustomControl
{
    public class NitaIcon : Image
    {
        static NitaIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaIcon), 
                new FrameworkPropertyMetadata(typeof(NitaIcon)));
        }

        #region 属性

        #region SizeType

        public static readonly DependencyProperty SizeTypeProperty =
            DependencyProperty.Register("SizeType", typeof(SizeType), typeof(NitaIcon), new PropertyMetadata(SizeType.Medium));

        public SizeType SizeType
        {
            get { return (SizeType)GetValue(SizeTypeProperty); }
            set { SetValue(SizeTypeProperty, value); }
        }

        #endregion

        #endregion
    }
}
