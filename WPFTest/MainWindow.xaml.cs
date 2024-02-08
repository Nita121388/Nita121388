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
using WPFTest.Util;
using static System.Net.Mime.MediaTypeNames;

namespace WPFTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region 构造函数
        public MainWindow()
        {
            InitializeComponent();
            Images = new ObservableCollection<ImageSource>();
        }

        #endregion

        #region 字段
        private int num = 0;
        private string _buttonTestStr;
        private ObservableCollection<ImageSource> Images;
        public string ButtonTestStr
        {
            get { return _buttonTestStr; }
            set
            {
                _buttonTestStr = value;
                OnPropertyChanged(nameof(ButtonTestStr));
            }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region 私有方法
        private void GetImages()
        {
            Images.Add(ColorHelper.GetResourceByKey("Add"));
            Images.Add(ColorHelper.GetResourceByKey("Files"));
        }
        #endregion
        private void myClick(object sender, RoutedEventArgs e)
        {
            ++num;
            ButtonTestStr = "ButtonTestStr"+num;
        } 
    }
}
