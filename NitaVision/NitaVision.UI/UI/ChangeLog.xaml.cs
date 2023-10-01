using NitaVision.UI.Source.CoreUI;
using NitaVision.UI.UI.StudyList;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace NitaVision.UI.UI
{
    /// <summary>
    /// ChangeLog.xaml 的交互逻辑
    /// </summary>
    public partial class ChangeLog : UserControl, INotifyPropertyChanged
    {
        public ChangeLog()
        {
            InitializeComponent();
            ChangeLogNitaControls = new ObservableCollection<UserControl>();
            DefaultColorStatus = new ColorStatus()
            {
                Color = "#F0E68C",
                Status = "解析中"
            };
            DefaultIconStatusMode = IconStatusMode.EllipseText;

            var control1 = new NitaStatusControl();
            control1.NitaColorStatus = DefaultColorStatus;
            control1.NitaIconStatusMode = DefaultIconStatusMode;

            var defaultColorStatus2 = new ColorStatus()
            {
                Color = "#F0E68C",
                Status = "解析完成"
            };
            var control2 = new NitaStatusControl();
            control2.NitaColorStatus = defaultColorStatus2;
            control2.NitaIconStatusMode = DefaultIconStatusMode;

            this.DataContext = this;

            var temp = new ObservableCollection<UserControl>();
            temp.Add(control1);
            temp.Add(control2);
            ChangeLogNitaControls = temp;
        }
        private ColorStatus _defaultColorStatus;
        private IconStatusMode _defaultIconStatusMode;
        private ObservableCollection<UserControl> _controls;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<UserControl> ChangeLogNitaControls
        { 
            get 
            {
                return _controls; 
            }
            set 
            { 
                _controls = value;
                OnPropertyChanged(nameof(ChangeLogNitaControls));
            }
        }
        public ColorStatus DefaultColorStatus
        {
            get { return _defaultColorStatus; }
            set
            {
                _defaultColorStatus = value;
                OnPropertyChanged(nameof(DefaultColorStatus));
            }
        }
        public IconStatusMode DefaultIconStatusMode
        {
            get { return _defaultIconStatusMode; }
            set
            {
                _defaultIconStatusMode = value;
                OnPropertyChanged(nameof(DefaultIconStatusMode));
            }
        }
    }
}
