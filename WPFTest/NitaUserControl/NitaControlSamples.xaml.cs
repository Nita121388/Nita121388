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
using WPFTest.Enity;
using WPFTest.NitaCustomControl;
using WPFTest.ViewModel;
using static System.Net.Mime.MediaTypeNames;
using static WPFTest.NitaCustomControl.MultiComboBox;
using Application = System.Windows.Application;

namespace WPFTest.NitaUserControl
{
    /// <summary>
    /// NitaControlSamples.xaml 的交互逻辑
    /// </summary>
    public partial class NitaControlSamples : UserControl, INotifyPropertyChanged
    {
        public NitaControlSamples()
        {
            InitializeComponent();
           // this.NitaComboBoxs.DataContext = this;
            this.MultiComboBoxs.DataContext = this;
            _nitaItemModels.Add(_itemModel);
            _nitaItemModels.Add(_itemModel1);
            _nitaItemModels.Add(_itemModel2);
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

        }
        private NitaItemModel _itemModel = new NitaItemModel() 
        {
            Text = "NitaItemModel",
            Mode = Enity.ContentMode.ColorAndText,
        };
        private NitaItemModel _itemModel1 = new NitaItemModel()
        {
            Text = "NitaItemModel",
            Mode = Enity.ContentMode.IconAndText,
            Color= "#414ca6"
        };
        private NitaItemModel _itemModel2 = new NitaItemModel()
        {
            Text = "NitaItemModel_ToolTip",
            Icon = (ImageSource)Application.Current.FindResource("Message"),
            Mode = Enity.ContentMode.IconAndText,
        };

        private NitaItemModels _nitaItemModels = new NitaItemModels();
        private ObservableCollection<MultiCbxBaseData> _multiComboBoxList = new ObservableCollection<MultiCbxBaseData>();
        public NitaItemModel ItemModel
        {
            get
            {
                return _itemModel;
            }
            set
            {
                _itemModel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemModel)));
            }
        }
        public NitaItemModel ItemModel1
        {
            get
            {
                return _itemModel1;
            }
            set
            {
                _itemModel1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemModel1)));
            }
        }
        public NitaItemModel ItemModel2
        {
            get
            {
                return _itemModel2;
            }
            set
            {
                _itemModel2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemModel2)));
            }
        }
        
        public NitaItemModels NitaItemModels
        {
            get
            {
                return _nitaItemModels;
            }
            set
            {
                _nitaItemModels = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NitaItemModels)));
            }
        }
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
