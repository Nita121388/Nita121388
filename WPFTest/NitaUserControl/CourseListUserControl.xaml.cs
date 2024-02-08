using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// CourseListUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class CourseListUserControl : UserControl
    {
        const int INDICATOR_HEIGHT = 16;
        readonly ObservableCollection<double> _indicatorOffsets;
        public CourseListUserControl()
        {
            InitializeComponent();
            _indicatorOffsets = new ObservableCollection<double>();
            _indicatorList.ItemsSource = _indicatorOffsets;
            _listBox.SelectionChanged += delegate
            {
                this.UpdateIndicators();
            };
            _listBox.AddHandler(ScrollViewer.ScrollChangedEvent,
                new ScrollChangedEventHandler(delegate
                { this.UpdateIndicators(); }
                  ));
        }
        public Brush IndicatorBrush
        {
            get { return (Brush)GetValue(IndicatorBrushProperty); }
            set { SetValue(IndicatorBrushProperty, value); }
        }
        public static readonly DependencyProperty IndicatorBrushProperty
          = DependencyProperty.Register("IndicatorBrush",
            typeof(Brush), typeof(CourseListUserControl), new
            PropertyMetadata(SystemColors.HighlightBrush));

        public static readonly DependencyProperty ListBoxStyleProperty =
              DependencyProperty.Register("ListBoxStyle",
              typeof(Style), typeof(CourseListUserControl));

        public ListBox ListBox
        {
            get { return _listBox; }
        }
        public Style ListBoxStyle
        {
            get { return (Style)GetValue(ListBoxStyleProperty); }
            set { SetValue(ListBoxStyleProperty, value); }
        }
        private void UpdateIndicators()
        {
            if (_indicatorOffsets.Count > 0)
                _indicatorOffsets.Clear();
            if (_listBox.SelectedItems.Count == 0) return;
            ItemContainerGenerator gen = _listBox.ItemContainerGenerator;
            if (gen.Status != GeneratorStatus.ContainersGenerated)
                return;
            foreach (object selectedItem in _listBox.SelectedItems)
            {
                ListBoxItem? lbItem = gen.ContainerFromItem(selectedItem)
                  as ListBoxItem;
                if (lbItem == null) continue;
                GeneralTransform trans =
                  lbItem.TransformToAncestor(_listBox);
                Point location = trans.Transform(new Point(0, 0));
                double offset = location.Y + (lbItem.ActualHeight / 2)
                    - (INDICATOR_HEIGHT / 2);
                _indicatorOffsets.Add(offset);
            }
        }
    }
}
