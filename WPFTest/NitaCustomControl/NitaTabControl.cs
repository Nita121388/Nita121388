using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFTest.DataBinding;
using WPFTest.Enity;

namespace WPFTest.NitaCustomControl
{
    public class NitaTabControl: TabControl
    {
        static NitaTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaTabControl),
                new FrameworkPropertyMetadata(typeof(NitaTabControl)));
        }

        #region DependencyProperty
        #region Orientation
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(NitaTabControl), new PropertyMetadata());
        #endregion
        #endregion

        #region Override方法
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new NitaTabItem();
        }
        #endregion



    }
}
