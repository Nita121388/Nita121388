using CommunityToolkit.Mvvm.Input;
using NitaVision.BLL;
using NitaVision.SPI.Constant;
using NitaVision.SPI.Entity;
using NitaVision.UI.Source.CoreUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Path = System.IO.Path;

namespace NitaVision.UI.UI.StudyList
{
    /// <summary>
    /// StudyGrid.xaml 的交互逻辑
    /// </summary>
    public partial class StudyGrid : UserControl,INotifyPropertyChanged
    {
        #region 字段
        private ObservableCollection<Item> items;
        private ColorStatus _defaultColorStatus;
        private IconStatusMode _defaultIconStatusMode;
        public RelayCommand<object> PlayCommand { get; set; }
        #endregion
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public StudyGrid()
        {
            InitializeComponent();
            Items = new ObservableCollection<Item>();
            /*listView.SelectedIndex = 0;
            listView.Focus();*/
            this.DataContext = this;
            //PlayCommand = new RelayCommand<object>(PlayCommand_Executed);
        }
        #region 属性
        public ColorStatus DefaultColorStatus
        { 
            get { return _defaultColorStatus; }
            set { _defaultColorStatus = value;
                OnPropertyChanged(nameof(DefaultColorStatus));
            }
        }
        public IconStatusMode DefaultIconStatusMode
        {
            get { return _defaultIconStatusMode; }
            set
            {
                _defaultIconStatusMode = value;
                OnPropertyChanged(nameof(DefaultIconStatusMode));
            }
        }
        public ObservableCollection<Item> Items { 
            get { return items; }
            set { 
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        #endregion
        #region 事件
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Title = "选择视频文件";
            openFileDialog.Filter = "视频文件 (*.mp4;*.avi;*.wmv;*.mov;*.mkv;)|*.mp4;*.avi;*.wmv;*.mov;*.mkv;|所有文件 (*.*)|*.*";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                AddItem(filePath);
            }
        }
        private void AddItem(string filePath)
        {
            DefaultColorStatus = new ColorStatus()
            {
                Color = "#F0E68C",
                Status = "解析中"
            };
            DefaultIconStatusMode = IconStatusMode.EllipseText;
            Item item = new Item();
            item.Number = Items.Count + 1;
            item.FileName = Path.GetFileName(filePath);
            item.Duration = TimeSpan.Zero;
            item.Status = Status.Resolving;
            item.ItemColorStatus = DefaultColorStatus;
            item.ItemIconStatusMode = DefaultIconStatusMode;
            Items.Add(item);
            listView.SelectedItem = item;
            listView.ScrollIntoView(item);
            listView.UpdateLayout();
            var videoFile = new VideoFile();
            videoFile.FileName = item.FileName;
            videoFile.Status = Status.Resolving;
            videoFile.SourcePath = filePath;
            videoFile.ConvertedPath = filePath;
            //VideoFileManager.Current.AddVideoFile(videoFile);
            //ResolveItem(item); //调用解析方法，传入新建的数据项作为参数

            /*var row = listView.ItemContainerGenerator.ContainerFromIndex(0);
            NitaStatusControl nsc = FindVisualChild<NitaStatusControl>(row);
            if (nsc != null)
            {
                nsc.NitaColorStatus = DefaultColorStatus;
                nsc.NitaIconStatusMode = DefaultIconStatusMode;
            }*/
        }
        private UIElement GetListViewCellControl(int rowIndex, int cellIndex)
        {
            // rowIndex 和 cellIndex 基于 0.
            var row = listView.ItemContainerGenerator.ContainerFromIndex(0);

            ContentPresenter cp = FindVisualChild<ContentPresenter>(row);

            var Generator = listView?.ItemContainerGenerator;
            if (Generator != null)
            {
                var temp = Generator.ContainerFromIndex(0);
            }
            //var nsc = FindVisualChild<NitaStatusControl>(item);
            var dp = listView?.ItemContainerGenerator?.ContainerFromIndex(0);
            UIElement u = listView?.ItemContainerGenerator?.ContainerFromIndex(rowIndex) as UIElement;
            if (u == null) return null;
            while ((u = (VisualTreeHelper.GetChild(u, 0) as UIElement)) != null)
                if (u is GridViewRowPresenter)
                    return VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(u, cellIndex), 0) as UIElement;
            return u;
        }
        #endregion
        #region 私有方法
        #endregion

        /// <summary>
        /// 解析视频
        /// </summary>
        /// <param name="item"></param>
        private async void ResolveItem(Item item)
        {
            await Task.Delay(3000); //延迟3秒，模拟解析过程 TODO

            Random random = new Random();
            int result = random.Next(0, 2);

            if (result == 0) //如果结果为0，表示解析成功
            {
                item.Duration = TimeSpan.FromSeconds(random.Next(10, 600)); //随机生成一个10到600之间的整数，作为时长（秒）
                item.Status = Status.Resolved; //设置状态为已解析
            }
            else //解析失败
            {
                item.Status = Status.Failed; //设置状态为解析失败
            }
        }
        /// <summary>
        /// 播放视频
        /// </summary>
        /// <param name="item"></param>
        private async void PlayItem(Item item)
        {
            item.Status = Status.Playing;
            await Task.Delay(item.Duration); //延迟时长的时间，模拟播放过程
            item.Status = Status.Playing;
        }
        /// <summary>
        /// 暂停一行数据的方法
        /// </summary>
        /// <param name="item"></param>
        private async void PauseItem(Item item)
        {
            item.Status = Status.Ing;
            await Task.Delay(1000); //延迟1秒，模拟暂停过程
        }
        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="item"></param>
        private void DeleteItem(Item item)
        {
            Items.Remove(item);
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Number = i + 1;
            }
        }
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="fileName"></param>
        private void OpenFolder(string fileName)
        {
            string folderPath = System.IO.Path.GetDirectoryName(fileName);
            if (Directory.Exists(folderPath))
            {
                System.Diagnostics.Process.Start("explorer.exe", folderPath);
            }
            else
            {
                MessageBox.Show("文件夹不存在");
            }
        }
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem listViewItem = sender as ListViewItem;

            if (listViewItem != null) //如果列表行对象不为空
            {
                Item item = listViewItem.Content as Item; //获取列表行对象绑定的数据项对象

                if (item != null) //如果数据项对象不为空
                {
                    DragDrop.DoDragDrop(listViewItem, item, DragDropEffects.Move); 
                    //开始拖放操作，传入列表行对象、数据项对象和移动效果作为参数
                }

            }
        }
        private void ListViewItem_Drop(object sender, DragEventArgs e)
        {
            ListViewItem listViewItem = sender as ListViewItem; //获取触发事件的列表行对象

            if (listViewItem != null) //如果列表行对象不为空
            {
                Item targetItem = listViewItem.Content as Item; //获取列表行对象绑定的数据项对象

                if (targetItem != null) //如果数据项对象不为空
                {
                    Item sourceItem = e.Data.GetData(typeof(Item)) as Item; //获取拖放操作传递的数据项对象

                    if (sourceItem != null) //如果数据项对象不为空
                    {
                        int sourceIndex = Items.IndexOf(sourceItem); //获取源数据项在数据集合中的索引位置

                        int targetIndex = Items.IndexOf(targetItem); //获取目标数据项在数据集合中的索引位置

                        if (sourceIndex != targetIndex) //如果两个索引位置不相同，表示需要交换位置
                        {
                            Items.Move(sourceIndex, targetIndex); //调用数据集合的移动方法，传入两个索引位置作为参数

                            for (int i = 0; i < Items.Count; i++) //遍历数据集合，更新序号
                            {
                                items[i].Number = i + 1;
                            }
                        }

                    }

                }

            }

        }
        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            ContextMenu contextMenu = sender as ContextMenu;

            if (contextMenu != null)
            {
                Item item = contextMenu.DataContext as Item;

                if (item != null)
                {
                    MenuItem openFolderMenuItem = contextMenu.Items[0] as MenuItem;

                    if (openFolderMenuItem != null)
                    {
                        openFolderMenuItem.CommandParameter = item.FileName;
                    }

                }

            }

        }
        #region 命令行事件
        private void OpenFolderCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string fileName = e.Parameter as string;

            if (!string.IsNullOrEmpty(fileName))
            {
                OpenFolder(fileName);
            }

        }
        private void DeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Item item = e.Parameter as Item;

            if (item != null)
            {
                DeleteItem(item);
            }
        }
        private void ResolveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Item item = e.Parameter as Item;

            if (item != null)
            {
                ResolveItem(item);
            }

        }
        private void ResolveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Item item = e.Parameter as Item;

            if (item != null)
            {
                e.CanExecute = item.Status == Status.Failed;
            }

        }
        private void PlayCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Item item = e.Parameter as Item;

            if (item != null)
            {
                PlayItem(item);
            }

        }
        private void PlayCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Item item = e.Parameter as Item;

            if (item != null)
            {
                e.CanExecute = item.Status == Status.Resolved;
            }

        }
        private void PauseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Item item = e.Parameter as Item;

            if (item != null)
            {
                PauseItem(item);
            }
        }
        private void PauseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Item item = e.Parameter as Item;

            if (item != null)
            {
                e.CanExecute = item.Status == Status.Playing;
            }

        }
        private void ListView_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                Item item = listView.SelectedItem as Item;

                if (item != null)
                {
                    if (item.Status == Status.Resolved)
                    {
                        PlayItem(item);
                    }
                    else if (item.Status == Status.Playing)
                    {
                        PauseItem(item);
                    }

                }

            }
        }
        #endregion


        #region 私有方法

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
        #endregion
    }

    public class Item : INotifyPropertyChanged
    {
        private int number; //序号
        private string fileName; //文件名
        private TimeSpan duration; //时长
        private Status status; //状态
        private ColorStatus colorStatus;
        private IconStatusMode iconStatusMode;
        public int Number
        {
            get { return number; }
            set { number = value; OnPropertyChanged(nameof(Number)); }
        }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; OnPropertyChanged(nameof(FileName)); }
        }

        public TimeSpan Duration
        {
            get { return duration; }
            set { duration = value; OnPropertyChanged(nameof(Duration)); }
        }

        public Status Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(nameof(Status)); }
        }

        public ColorStatus ItemColorStatus
        {
            get { return colorStatus; }
            set { 
                colorStatus = value; 
                OnPropertyChanged(nameof(ItemColorStatus)); }
        }
        public IconStatusMode ItemIconStatusMode
        {
            get { return iconStatusMode; }
            set { iconStatusMode = value; 
                OnPropertyChanged(nameof(ItemIconStatusMode)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
