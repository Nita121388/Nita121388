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
using static WPFTest.NitaCustomControl.MultiComboBox;

namespace WPFTest.NitaUserControl
{
    /// <summary>
    /// MainTabControl.xaml 的交互逻辑
    /// </summary>
    public partial class MainTabControl : UserControl,INotifyPropertyChanged
    {
        public MainTabControl()
        {
            InitializeComponent();

            MultiComboBoxList = new ObservableCollection<MultiCbxBaseData>()
            {
                new MultiCbxBaseData(){
                    ID=0,
                    ViewName="张三",
                    IsCheck=false
                },
                new MultiCbxBaseData(){
                    ID=1,
                    ViewName="李四",
                    IsCheck=false
                },
                new MultiCbxBaseData(){
                    ID=2,
                    ViewName="王五",
                    IsCheck=false
                },
                new MultiCbxBaseData(){
                    ID=3,
                    ViewName="马六",
                    IsCheck=false
                },
                 new MultiCbxBaseData(){
                    ID=4,
                    ViewName="赵七",
                    IsCheck=false
                },
            };
            this.DataContext = this;
        }

        private ObservableCollection<MultiCbxBaseData> _multiComboBoxList = new ObservableCollection<MultiCbxBaseData>();
        public ObservableCollection<MultiCbxBaseData> MultiComboBoxList
        {
            get
            {
                return _multiComboBoxList;
            }
            set
            {
                _multiComboBoxList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MultiComboBoxList)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
