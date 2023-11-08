using Microsoft.EntityFrameworkCore.Metadata;
using NitaVision.UI.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Image = System.Windows.Controls.Image;
using Path = System.Windows.Shapes.Path;

namespace NitaVision.UI.Source.Style
{
    /// <summary>
    /// NitaButton.xaml 的交互逻辑
    /// </summary>
    [TemplatePart(Name = "PART_Icon", Type = typeof(Image))]
    public partial class NitaButton : UserControl
    {
        public NitaButton()
        {
            InitializeComponent();
        }
        #region 
        #region 依赖属性

        #endregion
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(NitaButton), new PropertyMetadata(OnIconChanged));

        public static readonly DependencyProperty TextProperty =
       DependencyProperty.Register("Text", typeof(string), typeof(NitaButton), new PropertyMetadata("按钮", OnTextChanged));

        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(ButtonMode), typeof(NitaButton));

        public static readonly DependencyProperty MainColorProperty =
            DependencyProperty.Register("MainColor", typeof(string), typeof(NitaButton), new PropertyMetadata("#ed2929"));

        public static readonly DependencyProperty SizeTypeProperty =
            DependencyProperty.Register("SizeType", typeof(SizeType), typeof(NitaButton), new PropertyMetadata(SizeType.Large));

        public static readonly DependencyProperty NitaBackgroundProperty =
            DependencyProperty.Register("BackgroundProperty", typeof(System.Windows.Media.Brush), typeof(NitaButton));

        public static readonly DependencyProperty IsEnableProperty =
      DependencyProperty.Register("IsEnable", typeof(bool), typeof(NitaButton), new PropertyMetadata(true));

        public  static readonly DependencyProperty ToolTipTextProperty =
     DependencyProperty.Register("ToolTipText", typeof(string), typeof(NitaButton), new PropertyMetadata(""));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
            }
        }
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { 
                SetValue(IconProperty, value);
            }
        }
        public ButtonMode Mode
        {
            get { return (ButtonMode)GetValue(ModeProperty); }
            set
            {
                SetValue(ModeProperty, value);
            }
        }
        public string MainColor
        {
            get { return (string)GetValue(MainColorProperty); }
            set { 
                SetValue(MainColorProperty, value); 
            }
        }
        public System.Windows.Media.Brush NitaBackground
        {
            get { return (System.Windows.Media.Brush)GetValue(NitaBackgroundProperty); }
            set
            {
                SetValue(NitaBackgroundProperty, value);
            }
        }
        public SizeType SizeType 
        {
            get { return (SizeType)GetValue(SizeTypeProperty); }
            set
            {
                SetValue(SizeTypeProperty, value);
            }
        } 
        public bool IsEnable
        {
            get { return (bool)GetValue(IsEnableProperty); }
            set
            {
                SetValue(IsEnableProperty, value);
            }
        }
        public string ToolTipText
        {
            get 
            {
                return (string)GetValue(ToolTipTextProperty);
            } 
            set
            {
                SetValue(ToolTipTextProperty, value);
            }
        }
        #endregion
        #region 对外提供事件
        // 注册路由事件
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(
            "Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NitaButton));

        // 添加或移除事件处理器
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        // 触发Click事件
        protected virtual void OnClick()
        {
            RoutedEventArgs args = new RoutedEventArgs(ClickEvent, this);
            RaiseEvent(args);
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OnClick();
        }
        #endregion

        #region 点击
        /// <summary>
        /// 按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var width = this.NitaButtonBorder.ActualWidth;
            var target = this.ButtonDownEllipse;
            if (target == null) return;
            target.Center = Mouse.GetPosition(this);
            var animation = new DoubleAnimation()
            {
                From = 0,
                To = 150,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };
            target.BeginAnimation(EllipseGeometry.RadiusXProperty, animation);
            var animation2 = new DoubleAnimation()
            {
                From = 0.3,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };
            var target2 = this.ButtonDownPath;
            if (target2 == null) return;

            target2.BeginAnimation(Path.OpacityProperty, animation2);
        }
        private void TextBlock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            /*double textWidth = e.NewSize.Width;
            double gridWidth = NitaButtonGrid.ActualWidth;
            NitaButtonGrid.Width = textWidth + 20;*/
        }
        private void UpdateUIbyMode()
        {
            // 获取模板中的图标和文字元素
            Image icon = this.PART_Icon;
            Label text = this.PART_Text;

            // 根据展示模式调整内容显示
            switch (Mode)
            {
                case ButtonMode.IconOnly:
                    icon.Visibility = Visibility.Visible;
                    text.Visibility = Visibility.Collapsed;
                    break;
                case ButtonMode.TextOnly:
                    icon.Visibility = Visibility.Collapsed;
                    text.Visibility = Visibility.Visible;
                    break;
                case ButtonMode.IconAndText:
                    icon.Visibility = Visibility.Visible;
                    text.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }
        private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ImageSource newIcon = (ImageSource)e.NewValue;
            var button = (NitaButton)d;
            DrawingImage resource = (DrawingImage)d.GetValue(IconProperty);
            button.MainColor = ColorHelper.GetDominantColor(resource);
        }
        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string text = (string)e.NewValue;
            var button = (NitaButton)d;
            button.ToolTipText = text;
        }
        #endregion
    }
    public enum ButtonMode : ushort
    {
        IconOnly = 0,
        TextOnly = 1,
        IconAndText = 2
    }

    public enum ButtonSize
    {
        Small = 0,
        Medium = 1,
        Large = 2
    }

    public enum SizeType
    {
        Small = 0, Medium = 1, Large = 2
    }
    public class test 
    {
        public string str1 = "123456";

        public string Str1
        {
            get { return str1; }
            set
            {
                str1 = value;
            }
        }
    }
}
