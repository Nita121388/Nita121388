using System;
using System.Collections.Generic;
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
using WPFTest.ViewModel;

namespace WPFTest.NitaUserControl.NitaControlSamplesControl
{
    /// <summary>
    /// NitaItemSample.xaml 的交互逻辑
    /// </summary>
    public partial class NitaItemSample : UserControl, INotifyPropertyChanged
    {
        public NitaItemSample()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private NitaItemModel _itemModel1 = new NitaItemModel()
        {
            Text = "NitaItemModel1",
            Mode = Enity.ContentMode.ColorAndText,
        };
        private NitaItemModel _itemModel2 = new NitaItemModel()
        {
            Text = "NitaItemModel2",
            Mode = Enity.ContentMode.IconAndText,
            Color = "#414ca6"
        };
        private NitaItemModel _itemModel3 = new NitaItemModel()
        {
            Text = "NitaItemModel3",
            Icon = (ImageSource)Application.Current.FindResource("Message"),
            Mode = Enity.ContentMode.IconAndText,
        };
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
        public NitaItemModel ItemModel3
        {
            get
            {
                return _itemModel3;
            }
            set
            {
                _itemModel3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemModel3)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
