using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media;
using WPFTest.Enity;
using Application = System.Windows.Application;
using WPFTest.Util;

namespace WPFTest.NitaCustomControl
{
    class NitaButton : Button
    {
        static NitaButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaButton),
                new FrameworkPropertyMetadata(typeof(NitaButton)));
        }
        public NitaButton()
        { 
            this.Content = "Button";
        }
        #region 属性

        #region SizeType
        public SizeType SizeType
        {
            get { return (SizeType)GetValue(SizeTypeProperty); }
            set { SetValue(SizeTypeProperty, value); }
        }

        public static readonly DependencyProperty SizeTypeProperty =
            DependencyProperty.Register("SizeType", typeof(SizeType), typeof(NitaButton), new PropertyMetadata(SizeType.Medium));
        #endregion

        #region Icon
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(NitaButton), new PropertyMetadata(OnIconChanged));
        #endregion

        #region ClickColor
        public Brush ClickColor
        {
            get { return (Brush)GetValue(ClickColorProperty); }
            set { SetValue(ClickColorProperty, value); }
        }

        public static readonly DependencyProperty ClickColorProperty =
            DependencyProperty.Register("ClickColor", typeof(Brush), typeof(NitaButton), new PropertyMetadata());
        #endregion

        #region ButtonMode
        public ContentMode ButtonMode
        {
            get { return (ContentMode)GetValue(ContentModeProperty); }
            set { SetValue(ContentModeProperty, value); }
        }

        public static readonly DependencyProperty ContentModeProperty =
            DependencyProperty.Register("ContentMode", typeof(ContentMode), typeof(NitaButton), new PropertyMetadata(ContentMode.IconAndText));
        #endregion

        #region NoBorder
        public bool NoBorder
        {
            get { return (bool)GetValue(NoBorderProperty); }
            set { SetValue(NoBorderProperty, value); }
        }
        public static readonly DependencyProperty NoBorderProperty =
            DependencyProperty.Register("NoBorder", typeof(bool), typeof(NitaButton), new PropertyMetadata(false, OnNoBorderChanged));
        #endregion

        #region 鼠标悬浮样式 MouseOverStyle
        /// <summary>
        /// 鼠标悬浮样式
        /// </summary>
        public static readonly DependencyProperty MouseOverStyleProperty =
            DependencyProperty.Register("MouseOverStyle", typeof(MouseOverStyle), typeof(NitaButton), new PropertyMetadata(MouseOverStyle.ZoomAndChangeColor));
        public MouseOverStyle MouseOverStyle
        {
            get { return (MouseOverStyle)GetValue(MouseOverStyleProperty); }
            set { SetValue(MouseOverStyleProperty, value); }
        }
        #endregion

        #region BottonStyle
        public ButtonStyle ButtonStyle
        {
            get { return (ButtonStyle)GetValue(ButtonStyleProperty); }
            set { SetValue(ButtonStyleProperty, value); }
        }

        public static readonly DependencyProperty ButtonStyleProperty =
            DependencyProperty.Register("ButtonStyle", typeof(ButtonStyle), typeof(NitaButton), new PropertyMetadata(ButtonStyle.Default));
        #endregion


        #endregion

        #region 私有方法
        private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ImageSource newIcon = (ImageSource)e.NewValue;
            var button = (NitaButton)d;
            if (button.ClickColor == null)
            {
                DrawingImage resource = (DrawingImage)d.GetValue(IconProperty);
                button.ClickColor = ColorHelper.GetDominantColorBrush(resource, 1);
            }
        }
        private static void OnNoBorderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            /*bool NoBorder = (bool)e.NewValue;
            var button = (NitaButton)d;
            DrawingImage resource = (DrawingImage)d.GetValue(IconProperty);
            if (NoBorder)
            {
                button.ClickColor = Brushes.Red;
                //button.ClickColor = ColorHelper.GetDominantColorBrush(resource, 2);
            }
            else
            {
                button.ClickColor = ColorHelper.GetDominantColorBrush(resource, 1);
            }*/
        }
        #endregion

    }
}
