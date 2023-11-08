using NitaVision.UI.Source.Style;
using NitaVision.UI.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace NitaVision.UI.Source.CoreUI
{
    /// <summary>
    /// NitaCheckBox.xaml 的交互逻辑
    /// 1. 依赖属性 IsCheck,表示是否选中
    /// 2. 默认不选中
    /// 3. 该控件对外提供一个点击事件
    /// 3. 点击该图片，变化为另一个图片
    /// 6. 该控件支持禁用IsEnabled，禁用时Icon透明度0.5
    /// 7. 该控件对外提供一个点击事件✅
    /// </summary>
    public partial class NitaCheckBox : UserControl, INotifyPropertyChanged
    {
        #region 字段
        private ObservableCollection<ImageSource> images;
        private int imageIndex = 0;
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public NitaCheckBox()
        {
            InitializeComponent();
            Images = new ObservableCollection<ImageSource>();
            InitIcons();
        }
        private void InitIcons()
        {
            Images.Add(ColorHelper.GetResourceByKey("ComboBox"));
            Images.Add(ColorHelper.GetResourceByKey("Check"));
            Images = Images;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static readonly DependencyProperty IsCheckProperty =
            DependencyProperty.Register("IsCheck",
                typeof(bool),
                typeof(NitaCheckBox),
                new PropertyMetadata(false, OnCheckChanged));

        public static readonly DependencyProperty SizeTypeProperty =
           DependencyProperty.Register("SizeType", typeof(SizeType), typeof(NitaCheckBox), new PropertyMetadata(SizeType.Medium));

        public static readonly DependencyProperty ToolTipTextProperty =
          DependencyProperty.Register("ToolTipText", typeof(string), typeof(NitaCheckBox), new PropertyMetadata("未选中"));

        public SizeType SizeType
        {
            get { return (SizeType)GetValue(SizeTypeProperty); }
            set
            {
                SetValue(SizeTypeProperty, value);
            }
        }
        public string ToolTipText
        {
            get { return (string)GetValue(ToolTipTextProperty); }
            set
            {
                SetValue(ToolTipTextProperty, value);
            }
        }
        public bool IsCheck
        {
            get { return (bool)GetValue(IsCheckProperty); }
            set { SetValue(IsCheckProperty, value); }
        }

        public int ImageIndex
        {
            get { return imageIndex; }
            set 
            {
                imageIndex = value;
                OnPropertyChanged(nameof(imageIndex));
            }
        }
        public ObservableCollection<ImageSource> Images 
        {
            get{ return images; }
            set 
            { 
                images = value;
                OnPropertyChanged(nameof(Images));
            } 
        }
       
        private static void OnCheckChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NitaCheckBox target = (NitaCheckBox)d;
            if (target != null)
            {
                bool isCheck = (bool)e.NewValue;
                target.ImageIndex = isCheck ? 1 : 0;
                target.ToolTipText = isCheck ? "已选中" : "未选中";
            }
        }
        #region 对外提供事件
        // 注册路由事件
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(
            "Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NitaCheckBox));

        // 添加或移除事件处理器
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        // 触发Click事件
        protected virtual void OnClick()
        {
            RoutedEventArgs args = new RoutedEventArgs(ClickEvent, this);
            RaiseEvent(args);
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OnClick();
        }
        #endregion
    }
}
