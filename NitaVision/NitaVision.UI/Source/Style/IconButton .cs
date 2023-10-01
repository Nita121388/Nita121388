using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NitaVision.UI.Source.Style
{
    public class IconButton : Button
    {
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register("IsEnabled", typeof(bool), typeof(IconButton), new PropertyMetadata(true));

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(IconButton));

        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(ButtonMode), typeof(IconButton));
        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public ButtonMode Mode
        {
            get { return (ButtonMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        static IconButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconButton), new FrameworkPropertyMetadata(typeof(IconButton)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // 获取模板中的图标和文字元素
            Image icon = GetTemplateChild("PART_Icon") as Image;
            TextBlock text = GetTemplateChild("PART_Text") as TextBlock;

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

        private void IconButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked the icon button.");
        }
    }
    public enum ButtonMode : ushort
    {
        IconOnly = 0,
        TextOnly =1,
        IconAndText =2
    }
}
