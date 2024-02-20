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
    /// NitaComboBoxSamples.xaml 的交互逻辑
    /// </summary>
    public partial class NitaComboBoxSamples : UserControl, INotifyPropertyChanged
    {
        private NitaItemModels _nitaItemModels = new NitaItemModels();
        private NitaItemModels _nitaItemModels1 = new NitaItemModels();
        private NitaItemModels _nitaItemModels2 = new NitaItemModels();
        private NitaItemModels _nitaItemModels3 = new NitaItemModels();
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

        public NitaComboBoxSamples()
        {
            InitializeComponent();
            this.DataContext = this;
            NitaItemModels1.Add(_itemModel1);
            NitaItemModels1.Add(_itemModel2);
            NitaItemModels1.Add(_itemModel3);
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
        public NitaItemModels NitaItemModels1
        {
            get
            {
                return _nitaItemModels1;
            }
            set
            {
                _nitaItemModels1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NitaItemModels1)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
