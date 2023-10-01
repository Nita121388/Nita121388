using NitaVision.BLL;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace NitaVision.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }
        private void GridSplitter_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var grid = (Grid)VisualTreeHelper.GetParent((GridSplitter)sender);
            var leftColumn = grid.ColumnDefinitions[0];
            var gridSplitter = grid.ColumnDefinitions[1];
            var delta = e.VerticalChange;
            leftColumn.Width = new GridLength(leftColumn.ActualWidth + delta);
            gridSplitter.Width = new GridLength(3);
        }
        private void Init()
        {
            VideoFileManager.GetInstance();
        }
    }
}
