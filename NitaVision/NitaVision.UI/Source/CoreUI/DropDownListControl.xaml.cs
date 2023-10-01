using NitaVision.UI.Source.Style;
using NitaVision.UI.UI.StudyList;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NitaVision.UI.Source.CoreUI
{
    /// <summary>
    /// DropDownListControl.xaml 的交互逻辑
    /// </summary>
    public partial class DropDownListControl : UserControl,INotifyPropertyChanged
    {
        #region 字段
        private bool _isCheck;
        private ObservableCollection<ColorStatus> _statusOptions;
        #endregion

        public DropDownListControl()
        {
            InitializeComponent();
            StatusOptions = new ObservableCollection<ColorStatus>();
            DataContext = this;
        }
        public static readonly DependencyProperty DefaultOptionProperty =
            DependencyProperty.Register("DefaultOption", typeof(ColorStatus), typeof(DropDownListControl), new PropertyMetadata(null)); 

        public static readonly DependencyProperty IconStatusModeProperty =
            DependencyProperty.Register("IconStatusMode", typeof(IconStatusMode), typeof(DropDownListControl), new PropertyMetadata(null));

        public static readonly DependencyProperty StatusOptionsProperty =
           DependencyProperty.Register("StatusOptions", typeof(ObservableCollection<ColorStatus>), typeof(DropDownListControl), new PropertyMetadata(null));

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #region 属性
        public bool IsCheck
        {
            get { return _isCheck; }
            set
            {
                _isCheck = value;
                OnPropertyChanged(nameof(IsCheck));
            }
        }
        public ColorStatus DefaultOption
        {
            get { return (ColorStatus)GetValue(DefaultOptionProperty); }
            set { SetValue(DefaultOptionProperty, value); }
        }
        public IconStatusMode IconStatusMode
        {
            get { return (IconStatusMode)GetValue(IconStatusModeProperty); }
            set { SetValue(IconStatusModeProperty, value); }
        }
        public ObservableCollection<ColorStatus> StatusOptions
        {
            get { return _statusOptions; }
            set
            {
                _statusOptions = value;
                OnPropertyChanged(nameof(StatusOptions));
            }
        }
        #endregion
        
        #region 事件
        private void Btn_ExpandClick(object sender, RoutedEventArgs e)
        {
            Image img = new Image();
            img.Source = (ImageSource)FindResource("LeftTriangle");
            if (Btn_Expand.Icon.GetValue(Image.SourceProperty) == FindResource("LeftTriangle"))
            {
                IsCheck = true;
                Btn_Expand.Icon = (ImageSource)FindResource("DownTriangle");
            }
            else
            {
                IsCheck = false;
                Btn_Expand.Icon = (ImageSource)FindResource("LeftTriangle");
            }

        }

        #endregion
    }
}
