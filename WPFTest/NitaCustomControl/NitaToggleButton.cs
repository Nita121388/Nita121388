using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using WPFTest.Enity;
using WPFTest.Util;

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


        #region IsChangeIcon
        public bool IsChangeIcon
        {
            get { return (bool)GetValue(IsChangeIconProperty); }
            set { SetValue(IsChangeIconProperty, value); }
        }

        public static readonly DependencyProperty IsChangeIconProperty =
            DependencyProperty.Register("IsChangeIcon", 
                typeof(bool), 
                typeof(NitaToggleButton), 
                new PropertyMetadata(true));
        #endregion


        #region UnCheckIcon
        public ImageSource UnCheckIcon
        {
            get { return (ImageSource)GetValue(IsChangeIconProperty); }
            set { SetValue(IsChangeIconProperty, value); }
        }

        public static readonly DependencyProperty UnCheckIconProperty =
            DependencyProperty.Register("UnCheckIcon",
                typeof(ImageSource),
                typeof(NitaToggleButton),
                new PropertyMetadata(
                    (ImageSource)Application.Current.Resources["LeftTriangle"]
                   ));
        #endregion


        #region CheckedIcon
        public ImageSource CheckedIcon
        {
            get { return (ImageSource)GetValue(CheckedIconProperty); }
            set { SetValue(CheckedIconProperty, value); }
        }

        public static readonly DependencyProperty CheckedIconProperty =
            DependencyProperty.Register("CheckedIcon",
                typeof(ImageSource),
                typeof(NitaToggleButton),
                new PropertyMetadata(
                    (ImageSource)Application.Current.Resources["DownTriangle"]
                    ));
        #endregion





        #endregion

        /*private static void OnUnCheckIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ImageSource newIcon = (ImageSource)e.NewValue;
            var button = (NitaToggleButton)d;
            if (!button.IsChangeIcon)
            {
                button.CheckedIcon = newIcon;
            }
        }
        private static void OnCheckedIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ImageSource newIcon = (ImageSource)e.NewValue;
            var button = (NitaToggleButton)d;
            if (!button.IsChangeIcon)
            {
                button.UnCheckIcon = newIcon;
            }
        }*/
    }
}
