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
using WPFTest.Enity;
using WPFTest.ViewModel;

namespace WPFTest.NitaUserControl.NitaControlSamplesControl
{
    /// <summary>
    /// NitaControlSample.xaml 的交互逻辑
    /// </summary>
    public partial class NitaControlSample : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        #region Contructors
        public NitaControlSample()
        {
            InitializeComponent();
            Permissions.Add(Permission.Deletable);
            this.DataContext = this;
        }
        #endregion


        private List<Permission> _permissions = new List<Permission>();
        private NitaItemModel _itemModel = new NitaItemModel()
        {
            Text = "NitaItemModel_ToolTip",
            Icon = (ImageSource)Application.Current.FindResource("Message"),
            Mode = Enity.ContentMode.IconAndText,
        };


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

        public List<Permission> Permissions
        {
            get
            {
                return _permissions;
            }
            set
            {
                _permissions = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Permissions)));
            }
        }

    }
}
