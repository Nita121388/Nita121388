using System;
using System.Collections.Generic;
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

namespace WPFTest.NitaUserControl
{
    /// <summary>
    /// UseCourseListUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class UseCourseListUserControl : UserControl
    {
        public UseCourseListUserControl()
        {
            InitializeComponent();
            this._selectionModeCombo.SelectionChanged += this.OnSelectionModeChanged;
        }
        void OnSelectionModeChanged(object sender,
          SelectionChangedEventArgs e)
        {
            if (_selectionModeCombo.SelectedIndex < 0)
                return;
            SelectionMode mode =
                  (SelectionMode)_selectionModeCombo.SelectedItem;
            _listBoxWithIndicator.ListBox.SelectionMode = mode;
        }
    }
}
