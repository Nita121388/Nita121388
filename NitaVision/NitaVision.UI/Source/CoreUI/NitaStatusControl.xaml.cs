using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NitaVision.UI.Source.CoreUI
{
    /// <summary>
    /// NitaStatusControl.xaml 的交互逻辑
    /// </summary>
    public partial class NitaStatusControl : UserControl, INotifyPropertyChanged
    {
        #region 字段
        #endregion
        #region 构造函数
        public NitaStatusControl()
        {
            InitializeComponent();
            //DataContext = this;
        }
        #endregion
        #region 依赖属性
        public static readonly DependencyProperty NitaColorStatusProperty =
           DependencyProperty.Register("NitaColorStatus", typeof(ColorStatus), typeof(NitaStatusControl), new PropertyMetadata(new ColorStatus(), ColorStatusChanged));

        public static readonly DependencyProperty NitaIconStatusModeProperty =
            DependencyProperty.Register("NitaIconStatusMode", typeof(IconStatusMode), typeof(NitaStatusControl), new PropertyMetadata(IconStatusMode.EllipseText));

        #endregion
        #region 属性
        public IconStatusMode NitaIconStatusMode
        {
            get
            {
                return (IconStatusMode)GetValue(NitaIconStatusModeProperty);
            }
            set
            {
                SetValue(NitaIconStatusModeProperty, value);
                OnPropertyChanged(nameof(NitaIconStatusMode));
            }
        }
        public ColorStatus NitaColorStatus
        {
            get
            {
                return (ColorStatus)GetValue(NitaColorStatusProperty);
            }
            set
            {
                SetValue(NitaColorStatusProperty, value);
                OnPropertyChanged(nameof(NitaColorStatus));
            }
        }
        #endregion
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event MouseButtonEventHandler OptionMouseLeftButtonDown
        {
            add { OptionGrid.MouseLeftButtonDown += value; }
            remove { OptionGrid.MouseLeftButtonDown -= value; }
        }
        private static void ColorStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NitaStatusControl? control = d as NitaStatusControl;
        }
    }
    public class ColorStatus:INotifyPropertyChanged
    {
        private string _color = "#DCDCDC";
        private string _status = "默认状态";
        private ImageSource _icon = (ImageSource)Application.Current.FindResource("DownTriangle");
        public string Status
        {
            get { return _status; }
            set
            {
                if (value != _status)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }
        public string Color
        {
            get { return _color; }
            set
            {
                if (value != _color)
                {
                    _color = value;
                    OnPropertyChanged(nameof(Color));
                }
            }
        }
        public ImageSource Icon
        {
            get { return _icon; }
            set
            {
                if (value != _icon)
                {
                    _icon = value;
                    OnPropertyChanged(nameof(Icon));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public enum IconStatusMode
    {
        EllipseText = 0,
        IconText = 1,
    }
}
