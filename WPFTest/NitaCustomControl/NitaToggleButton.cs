using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WPFTest.Enity;

namespace WPFTest.NitaCustomControl
{
    public class NitaToggleButton : ToggleButton
    {
        #region Constructor

        static NitaToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(NitaToggleButton),
                new FrameworkPropertyMetadata(typeof(NitaToggleButton)));
        }
        #endregion

        #region Override
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
        #endregion

        #region DependencyProperty
        #region SizeType
        public SizeType SizeType
        {
            get { return (SizeType)GetValue(SizeTypeProperty); }
            set { SetValue(SizeTypeProperty, value); }
        }

        public static readonly DependencyProperty SizeTypeProperty =
            DependencyProperty.Register("SizeType", typeof(SizeType), typeof(NitaToggleButton), new PropertyMetadata(SizeType.Small));
        #endregion
        #endregion
    }
}
