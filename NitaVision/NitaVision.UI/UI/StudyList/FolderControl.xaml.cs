using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic.Logging;
using Newtonsoft.Json;
using NitaVision.UI.Source.Style;
using NitaVision.UI.Util;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace NitaVision.UI.UI.StudyList
{
    /// <summary>
    /// FolderControl.xaml 的交互逻辑
    /// </summary>
    public partial class FolderControl : UserControl,INotifyPropertyChanged
    {
        #region 字段
        private ObservableCollection<Folder> folders;
        private Folder selectedFolder;
        private bool isDragging = false;
        private Point startPoint; //表示拖拽开始时的鼠标位置
        private Folder draggedFolder; //表示被拖拽的文件夹对象
        private int _draggedIndex; //表示被拖拽的文件夹在列表中的索引
        private int _selectedIndex; //表示被拖拽的文件夹在列表中的索引
        private int _dropIndex; //表示目标位置的索引
        private bool _isPress = false;
        private ContentPresenter draggedItem;
        private Point mousePosition;
        public Point draggedPostion;
        public RelayCommand<object> DeleteCommand { get; set; }
        #endregion
        #region 初始化
        public FolderControl()
        {
            InitializeComponent();
            Folders = new ObservableCollection<Folder>();
            DataContext = this;
            DeleteCommand = new RelayCommand<object>(DeleteFolder);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region 属性
        public ObservableCollection<Folder> Folders
        {
            get { return folders; }
            set
            {
                folders = value;
                OnPropertyChanged(nameof(Folders));
            }
        }
        public Folder SelectedFolder
        {
            get { return selectedFolder; }
            set
            {
                selectedFolder = value;
                OnPropertyChanged(nameof(SelectedFolder));
            }
        }
        public bool IsDragging
        {
            get { return isDragging; }
            set
            {
                isDragging = value;
                OnPropertyChanged(nameof(IsDragging));
            }
        }
        public Point MousePosition
        {
            get { return mousePosition; }
            set
            {
                mousePosition = value;
                OnPropertyChanged(nameof(MousePosition));
            }
        }
        public ContentPresenter DraggedItem
        {
            get { return draggedItem; }
            set
            {
                draggedItem = value;
                OnPropertyChanged(nameof(DraggedItem));
            }
        }
        public Folder DraggedFolder
        {
            get { return draggedFolder; }
            set
            {
                draggedFolder = value;
                OnPropertyChanged(nameof(DraggedFolder));
            }
        }
        public Point DraggedPostion
        {
            get { return draggedPostion; }
            set
            {
                draggedPostion = value;
            }
        }
        #endregion

        #region 事件
        #region 新增
        /// <summary>
        /// 向Folders集合中添加一个新的文件夹对象，并滚动到最后一个元素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddClick(object sender, RoutedEventArgs e)
        {
            Folders.Add(new Folder(GetNewFolderName()));
            FolderScrollViewer.ScrollToEnd();
            /*var scrollViewer = FindVisualChild<ScrollViewer>(sender as IconButton);
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToEnd();
            }*/
        }
        public string GetNewFolderName()
        {
            string name = "新建文件夹";
            int i = 1;
            while (Folders.Any(folder => folder.Name == name))
            {
                name = $"新建文件夹{i}";
                i++;
            }
            return name;
        }
        #endregion
        #region 重命名
        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.IsReadOnly = false;
                textBox.SelectAll();
            }
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                if (e.Key == Key.Enter)
                {
                    textBox.IsReadOnly = true;
                    BindingOperations.GetBindingExpression(textBox, TextBox.TextProperty).UpdateSource();
                }
                else if (e.Key == Key.Escape)
                {
                    textBox.IsReadOnly = true;
                    BindingOperations.GetBindingExpression(textBox, TextBox.TextProperty).UpdateTarget();
                }
            }
        }
        #endregion
        #region 删除
        private void DeleteFolder(object parameter)
        {
            //将参数转换为文件夹对象
            Folder folder = parameter as Folder;

            MessageBoxResult result = MessageBox.Show($"确定要删除{folder.Name}及其内容吗？", "确认", MessageBoxButton.YesNo, MessageBoxImage.Question);

            //如果用户选择了是，则从文件夹列表中移除该文件夹
            if (result == MessageBoxResult.Yes)
            {
                Folders.Remove(folder);
            }
        }
        #endregion
        #region 点击
        /// <summary>
        /// 点击文件夹
        /// 切换背景颜色、展示拖拽icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemsControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var source = e.OriginalSource as DependencyObject;
            if (source == null) return;
            ItemsControl itemsControl = FindVisualParent<ItemsControl>(source);
            if (itemsControl == null) return;
            if (e.OriginalSource is Grid)
            {
                ContentPresenter item = FindVisualParent<ContentPresenter>(source);
                var selectedFolder = item.DataContext as Folder;
                if (selectedFolder == null) return;
                _selectedIndex = Folders.IndexOf(selectedFolder);
                SetItemSelected(itemsControl);
            }
        }
        #endregion
        #region 拖拽文件夹
        private void FolderItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReleaseDraggedItem();
            var source = e.OriginalSource as DependencyObject;
            if (source == null) return;
            ItemsControl itemsControl = FindVisualParent<ItemsControl>(source);
            if (itemsControl == null) return;
            
            if (e.OriginalSource is Image)
            {
                startPoint = e.GetPosition(itemsControl);
                var cp = FindVisualParent<ContentPresenter>(source);
                DraggedItem = cp;
                DraggedFolder = cp.DataContext as Folder;
                if (draggedFolder == null) return;
                _draggedIndex = Folders.IndexOf(draggedFolder);
                _dropIndex = _draggedIndex;
                _isPress = true;
            }
            else
            {
                LogUtil.Log("e.OriginalSource不是Image");
            }
        }
        private void ItemsControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isPress) return;
            var source = e.OriginalSource as DependencyObject;
            if (source == null) return;
            ItemsControl itemsControl = FindVisualParent<ItemsControl>(source);
            if (DraggedItem == null) return;

            Point currentPoint = e.GetPosition(this);
            if (IsDragging) MousePosition = currentPoint;
            
            Vector delta = currentPoint - startPoint;
            LogUtil.Log($"开始位置{startPoint.X}、{startPoint.Y}");
            LogUtil.Log($"当前位置{currentPoint.X}、{currentPoint.Y}");
            var valiXDelta = DraggedItem.ActualWidth;
            var valiYDelta = DraggedItem.ActualHeight;
            var XDelta = Math.Abs(delta.X);
            var YDelta = Math.Abs(delta.Y);
            LogUtil.Log($"移动范围{XDelta}、{YDelta}");
            LogUtil.Log($"有效移动范围{valiXDelta}、{valiYDelta}");
            if (YDelta >= valiYDelta && 
                YDelta <= itemsControl.ActualHeight && 
                XDelta <= valiXDelta)
            {
                FrameworkElement element = itemsControl.InputHitTest(currentPoint) 
                    as FrameworkElement;
                if (element != null && (element is Grid || element is Image || element is TextBox))
                {
                    Folder folder = element.DataContext as Folder;
                    if (folder == null) return;
                    var newIndex = Folders.IndexOf(folder);
                    if (_dropIndex != newIndex)
                    {
                        LogUtil.Log($"目标位置变化{_dropIndex}{newIndex}");
                        ContentPresenter newItem = itemsControl.ItemContainerGenerator.ContainerFromIndex(newIndex) as ContentPresenter;
                        if (newItem == null) return;
                        _dropIndex = newIndex;
                        IsDragging = true;
                        LogUtil.Log($"IsDragging-{IsDragging}");
                        if (DraggedItem != null)
                            DraggedItem.Opacity = 0.5;

                    }
                    else
                    {
                        LogUtil.Log($"目标位置没有变化{_dropIndex}{newIndex}");
                        if (_dropIndex == 1)
                        { 
                            
                        }
                    }
                }
                else
                {
                    LogUtil.Log("element不是Grid、Image或TextBox");
                }
            }
            else if (YDelta > itemsControl.ActualHeight || XDelta > valiXDelta)
            {
                LogUtil.Log("鼠标移动超出有效范围");
                ReleaseDraggedItem();
            }
        }
        private void ItemsControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var source = e.OriginalSource as DependencyObject;
            if (source == null) return;
            ItemsControl itemsControl = FindVisualParent<ItemsControl>(source);
            if (itemsControl != null && IsDragging)
            {
                if (_dropIndex != _draggedIndex)
                {
                    Folders.Move(_draggedIndex, _dropIndex);
                    itemsControl.Items.Refresh();
                }
            }
            ReleaseDraggedItem();
        }
        private void ItemsControl_LostFocus(object sender, RoutedEventArgs e)
        {
            ReleaseDraggedItem();
        }
        private void SetItemSelected(ItemsControl itemsControl)
        {
            if (itemsControl == null) return;
            var generator = itemsControl.ItemContainerGenerator;
            for (int i = 0; i < itemsControl.Items.Count; i++)
            {
                var item = generator.ContainerFromIndex(i) as ContentPresenter;
                var grid = FindVisualChild<Grid>(item);
                var folderItem = FindVisualChild<FolderItem>(item); 
                if (grid == null) return;
                var folder = folderItem.DataContext as Folder;
                grid.Background = grid.Background.Clone();
                if (i == _selectedIndex)
                {
                    ColorAnimation animation = new ColorAnimation();
                    animation.From = ((SolidColorBrush)grid.Background).Color;
                    animation.To = Color.FromRgb(175, 238, 238);
                    animation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                    grid.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                    folder.IsSelected = true;
                    BindingOperations.GetBindingExpression(folderItem, FolderItem.FolderIsSelectedProperty).UpdateSource();
                    continue;
                }
                grid.Background = Brushes.Transparent;
                folder.IsSelected = false;
                BindingOperations.GetBindingExpression(folderItem, FolderItem.FolderIsSelectedProperty).UpdateSource();
            }
        }
        private void ReleaseDraggedItem()
        {
            LogUtil.Log("重置拖拽数据");
            DraggedFolder = null;
            IsDragging = false;
            _draggedIndex = -1;
            _dropIndex = -1;
            _isPress = false;
            if (DraggedItem == null) return;
            DraggedItem.Opacity = 1;
            DraggedItem.ReleaseMouseCapture();
            {
                LogUtil.Log("重置拖拽数据完成!");
            }
        }
        #endregion
        #endregion
        public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            if (obj == null)
            {
                return null;
            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);

                if (child is T)
                {
                    return (T)child;
                }

                var childOfChild = FindVisualChild<T>(child);

                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }
            return null;
        }
        private T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                return FindVisualParent<T>(parentObject);
            }
        }
    }
    public class Folder:INotifyPropertyChanged 
    {
        public string Name { get; set; }
        public bool IsReadOnly { get; set; }
        public bool isSelected{ get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Folder(string name)
        {
            Name = name;
            IsReadOnly = true;
            IsSelected = false;
        }
        public bool IsSelected
        { 
            get { return isSelected; }
            set { 
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
    }
}
