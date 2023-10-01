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

namespace NitaVision.UI.UI
{
    /// <summary>
    /// SubTitleGrid.xaml 的交互逻辑
    /// </summary>
    public partial class SubTitleGrid : UserControl
    {
        private int visibleCount = 3;
        // 绑定数据源
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(IEnumerable<object>), typeof(SubTitleGrid), new PropertyMetadata(null));

        // 用来设置每行的最小高度
        public static readonly DependencyProperty MinItemHeightProperty =
            DependencyProperty.Register("MinItemHeight", typeof(double), typeof(SubTitleGrid), new PropertyMetadata(0.0));

        // 设置列表的高度
        public static readonly DependencyProperty ListHeightProperty =
            DependencyProperty.Register("ListHeight", typeof(double), typeof(SubTitleGrid), new PropertyMetadata(double.NaN));

        // 当前选择的行索引
        private int selectedIndex = -1;

        // 当前选择的行控件
        private SubTitleControl selectedItem = null;

        // 总行数
        private int itemCount = 0;

        // 当前滚动到的行索引
        private int topIndex = 0;
        public SubTitleGrid()
        {
            InitializeComponent();
            this.DataContext = this;
            var titleControl = new List<SubTitleControl>();
            titleControl.Add(new SubTitleControl());
            Items = titleControl;
            itemCount = Items.Count();
        }
        public IEnumerable<object> Items
        {
            get { return (IEnumerable<object>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }
        public double MinItemHeight
        {
            get { return (double)GetValue(MinItemHeightProperty); }
            set { SetValue(MinItemHeightProperty, value); }
        }
        public double ListHeight
        {
            get { return (double)GetValue(ListHeightProperty); }
            set { SetValue(ListHeightProperty, value); }
        }
        private void UpdateStatusText()
        {
            double progress = itemCount > 0 ? (topIndex + 1.0) / itemCount * 100 : 0;
            this.statusText.Text = string.Format("总行数：{0}，当前行数：{1}，进度：{2:F2}%",
                itemCount, topIndex + 1, progress);
        }

        private void UpdateSelectedItemStyle()
        {
            // 如果有选择的行，取消其样式
            if (selectedItem != null)
            {
                selectedItem.Background = Brushes.Transparent;
                selectedItem.BorderBrush = Brushes.Transparent;
                selectedItem.BorderThickness = new Thickness(0);
            }
            // 如果有新的选择的行，设置其样式
            if (selectedIndex >= 0 && selectedIndex < itemCount)
            {
                selectedItem = this.itemsControl.ItemContainerGenerator.ContainerFromIndex(selectedIndex) as SubTitleControl;
                if (selectedItem != null)
                {
                    selectedItem.Background = Brushes.LightBlue;
                    selectedItem.BorderBrush = Brushes.Blue;
                    selectedItem.BorderThickness = new Thickness(1);
                }
            }
        }


        // 选择变化事件
        private void OnSelectionChanged(int oldIndex, int newIndex)
        {
            var oldItem = GetSubTitleControlByIndex(oldIndex);
            var newItem = GetSubTitleControlByIndex(newIndex);
            if (oldItem != null) oldItem.Selected = false;
            if (newItem != null) newItem.Selected = true;
        }
        //触发播放事件
        private void OnPlayCurrent(int oldIndex, int newIndex)
        {
            var oldItem = GetSubTitleControlByIndex(oldIndex);
            var newItem = GetSubTitleControlByIndex(newIndex);
            if (oldItem != null) oldItem.IsPlay = false;
            if (newItem != null) newItem.IsPlay = true;
        }
        public SubTitleControl? GetSubTitleControlByIndex(int index)
        {
            DependencyObject obj = VisualTreeHelper.GetChild(itemsControl, index);
            return obj as SubTitleControl;
        }
        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            //todo
            if (e.VerticalChange != 0)
            {
                double offset = this.scrollViewer.VerticalOffset;
                double itemHeight = this.itemsControl.ActualHeight / itemCount;
                topIndex = (int)Math.Floor(offset / itemHeight);
                UpdateStatusText();
            }
        }

        // 尺寸变化事件
        private void ItemsControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //todo
            // 如果ItemsControl的高度发生变化，更新每行的高度
            if (e.HeightChanged)
            {
                // 获取ItemsControl的高度
                double height = this.itemsControl.ActualHeight;
                // 获取每行的高度
                double itemHeight = height / itemCount;
                // 如果每行的高度小于最小高度，设置每行的高度为最小高度，并居中对齐
                if (itemHeight < MinItemHeight)
                {
                    itemHeight = MinItemHeight;
                    foreach (var item in this.itemsControl.Items)
                    {
                        SubTitleControl control =
                            this.itemsControl.ItemContainerGenerator.ContainerFromItem(item) as SubTitleControl;
                        if (control != null)
                        {
                            control.VerticalAlignment = VerticalAlignment.Center;
                        }
                    }
                }
                // 设置ItemsControl的高度为每行的高度乘以总行数，以避免出现空白区域
                this.itemsControl.Height = itemHeight * itemCount;
            }
        }

        // 处理加载完成事件
        private void ItemsControl_Loaded(object sender, RoutedEventArgs e)
        {
            itemCount = this.itemsControl.Items.Count;
            UpdateStatusText();
            UpdateSelectedItemStyle();
        }

        // 上键或下键 切换选择的行
        private void scrollViewer_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down)
            {
                int oldIndex = selectedIndex;
                if (e.Key == Key.Up)
                {
                    selectedIndex--;
                    if (selectedIndex < 0) selectedIndex = 0;
                }
                else if (e.Key == Key.Down)
                {
                    selectedIndex++;
                    if (selectedIndex >= itemCount) selectedIndex = itemCount - 1;
                    // 更新选择行的样式
                    UpdateSelectedItemStyle();
                    OnSelectionChanged(oldIndex, selectedIndex);
                    if (selectedIndex < topIndex || selectedIndex >= topIndex + visibleCount)
                    {
                        this.scrollViewer.ScrollToVerticalOffset(selectedIndex * this.itemsControl.ActualHeight / itemCount);
                    }
                    // 设置按键事件已处理，避免触发其他事件
                    e.Handled = true;
                }
                else if (e.Key == Key.Enter)
                {
                    OnPlayCurrent(oldIndex, selectedIndex);
                    e.Handled = true;
                }
            }
        }
    }
}
