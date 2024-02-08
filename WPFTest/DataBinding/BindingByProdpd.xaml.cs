using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Packaging;
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
    /// BindingByProdpd.xaml 的交互逻辑
    /// </summary>
    public partial class BindingByProdpd : UserControl
    {
        public BindingByProdpd()
        {
            InitializeComponent();
            this.stackPanel.DataContext = this;
        }
        public string SourceStr
        {
            get 
            {
                return (string)GetValue(SourceStrProperty); 
            }
            set 
            {
                SetValue(SourceStrProperty, value);
            }
        }

        public static readonly DependencyProperty SourceStrProperty =
            DependencyProperty.Register("SourceStr", typeof(string), typeof(BindingByProdpd), new PropertyMetadata("SourceStr默认值"));

    }
}
