using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFTest.Enity;
using WPFTest.Util;

namespace WPFTest.NitaCustomControl
{
    public  class NitaControlSample : ContentControl
    {

        #region Constructors

        static NitaControlSample()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaControlSample),
                new FrameworkPropertyMetadata(typeof(NitaControlSample)));
        }
        #endregion

        #region Fileds
        #endregion

        #region 依赖属性

        #region Description
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(NitaControlSample), new PropertyMetadata(OnDescriptionChanged));

        #endregion

        #region Title
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(NitaControlSample), new PropertyMetadata(""));
        #endregion

        #region ExtDescription 
        public string ExtDescription
        {
            get { return (string)GetValue(ExtDescriptionProperty); }
            set { SetValue(ExtDescriptionProperty, value); }
        }

        public static readonly DependencyProperty ExtDescriptionProperty =
            DependencyProperty.Register("ExtDescription", typeof(string), typeof(NitaControlSample), new PropertyMetadata(OnExtOnDescriptionChanged));
        #endregion

        #region 

        #endregion

        #endregion

        #region Override
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion

        private static void OnDescriptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string value = (string)e.NewValue;
            var control = (NitaControlSample)d;
            if (control != null && !string.IsNullOrEmpty(value))
            {
                control.Description = GetTextValue(value);
            }
        }
        private static void OnExtOnDescriptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string value = (string)e.NewValue;
            var control = (NitaControlSample)d;
            if (control != null && !string.IsNullOrEmpty(value))
            {
                control.ExtDescription = GetTextValue(value);
            }
        }

        private static string GetTextValue(string text)
        {
            return text.Replace("|", "\n");
        }
    }
}
