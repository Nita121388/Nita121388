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

namespace WPFTest.DataBinding
{
    /// <summary>
    /// BindingByProp.xaml 的交互逻辑
    /// </summary>
    public partial class BindingByProp : UserControl,INotifyPropertyChanged
    {
        public BindingByProp()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        private string _sourceStr = "默认值";
        public string SourceStr
        { 
            get
            {
                return _sourceStr;
            } 
            set
            {
                _sourceStr = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SourceStr)));
            } 
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
