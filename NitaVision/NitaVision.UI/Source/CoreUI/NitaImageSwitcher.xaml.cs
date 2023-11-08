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
using System.Windows.Threading;

namespace NitaVision.UI.Source.CoreUI
{
    /// <summary>
    /// NitaImageSwitcher.xaml 的交互逻辑
    /// 1. 依赖属性 Images 存放ImageSource的集合✅
    /// 2. 默认展示一个图片✅
    /// 3. 点击该图片，变化为另一个图片✅
    /// 4. 变化过程有动画效果✅ ,默认为淡入淡出
    /// 5. 该控件可以设置图片的变化效果：1.依次变化 从头变到尾就不再变化 2. 循环变化 
    /// 6. 该控件支持禁用，禁用时图片透明度0.5
    /// 7. 该控件对外提供一个点击事件✅
    /// 8. 该控件支持设置展示第几张图片
    /// </summary>
    public partial class NitaImageSwitcher : UserControl,INotifyPropertyChanged
    {
        #region 字段
        private DispatcherTimer timer = new DispatcherTimer();
        private int currentIndex = 0;
        public event RoutedEventHandler Click;
        #endregion
        public NitaImageSwitcher()
        {
            InitializeComponent();
            //this.DefaultStyleKey = typeof(NitaImageSwitcher);
            //this.Loaded += ImageSwitcher_Loaded;
            //this.Unloaded += ImageSwitcher_Unloaded;
            //this.MouseLeftButtonDown += ImageSwitcher_MouseLeftButtonDown;
        }
        /// <summary>
        /// 要显示的图片
        /// </summary>
        public static readonly DependencyProperty ImagesProperty =
            DependencyProperty.Register("Images", 
                typeof(ObservableCollection<ImageSource>), 
                typeof(NitaImageSwitcher), 
                new PropertyMetadata(new ObservableCollection<ImageSource>(), OnImagesChanged));
        public ObservableCollection<ImageSource> Images
        {
            get { return (ObservableCollection<ImageSource>)GetValue(ImagesProperty); }
            set
            {
                SetValue(ImagesProperty, value);
            }
        }
        private ImageSource _image;
        public ImageSource Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
        /// <summary>
        /// 图片切换模式
        /// </summary>
        public static readonly DependencyProperty ChangeEffectProperty =
            DependencyProperty.Register("ChangeEffect", 
                typeof(ChangeEffectEnum), 
                typeof(NitaImageSwitcher), 
                new PropertyMetadata(ChangeEffectEnum.Loop));

        public ChangeEffectEnum ChangeEffect
        {
            get { return (ChangeEffectEnum)GetValue(ChangeEffectProperty); }
            set { SetValue(ChangeEffectProperty, value); }
        }

        /// <summary>
        /// 设置图片变化的动画类型
        /// </summary>
        public static readonly DependencyProperty AnimationTypeProperty =
            DependencyProperty.Register("AnimationType", 
                typeof(AnimationTypeEnum), 
                typeof(NitaImageSwitcher), 
                new PropertyMetadata(AnimationTypeEnum.Fade));

        public AnimationTypeEnum AnimationType
        {
            get { return (AnimationTypeEnum)GetValue(AnimationTypeProperty); }
            set { SetValue(AnimationTypeProperty, value); }
        }

        public static readonly DependencyProperty ImageIndexProperty =
            DependencyProperty.Register("ImageIndex", 
                typeof(int), 
                typeof(NitaImageSwitcher), 
                new PropertyMetadata(0, OnImageIndex));

        public int ImageIndex
        {
            get 
            {
                return (int)GetValue(ImageIndexProperty); 
            }
            set { 
                SetValue(ImageIndexProperty, value);
                currentIndex = value;
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// Images值发生改变后
        /// 为默认展示图片赋值
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnImagesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NitaImageSwitcher target = (NitaImageSwitcher)d;
            if (target != null)
            { 
                var images = e.NewValue as ObservableCollection<ImageSource>;
                var image = images?.FirstOrDefault();
                if (image == null) return;
                target.Image = image;
                //ColorHelper.SaveImageSourceToFile(target.Image);
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SwitchToNextImage();
        }

        /// <summary>
        ///  切换到下一张图片的方法
        /// </summary>
        private void SwitchToNextImage()
        {
            if (Images == null || Images.Count == 0)
                return;

            int nextIndex = 0;
            if (IsRandom)
            {
                Random random = new Random();
                do
                {
                    nextIndex = random.Next(Images.Count);
                } while (nextIndex == currentIndex);
            }
            else
            {
                nextIndex = currentIndex + 1;
                if (nextIndex >= Images.Count)
                {
                    if (ChangeEffect == ChangeEffectEnum.Loop)
                        nextIndex = 0;
                    else
                        return;
                }
            }
            this.Image = Images[nextIndex];
            currentIndex = nextIndex;
        }

        /// <summary>
        /// 当设置ImageIndex时切换图片
        /// </summary>
        private static void OnImageIndex(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NitaImageSwitcher target = (NitaImageSwitcher)d;
            if (target != null)
            {
                int cIndex = (int)e.NewValue;
                if (target.currentIndex < target.Images.Count())
                {
                    target.Image = target.Images[cIndex];
                    target.currentIndex = cIndex;
                    target.ImageIndex = cIndex;
                }
            }
        }
        #region 变化效果
       

        // Interval属性，用于设置图片变化的间隔时间
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(TimeSpan), typeof(NitaImageSwitcher), new PropertyMetadata(TimeSpan.FromSeconds(3)));

        public TimeSpan Interval
        {
            get { return (TimeSpan)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        public static readonly DependencyProperty IsRandomProperty =
            DependencyProperty.Register("IsRandom", typeof(bool), typeof(NitaImageSwitcher), new PropertyMetadata(false));

        public bool IsRandom
        {
            get { return (bool)GetValue(IsRandomProperty); }
            set { SetValue(IsRandomProperty, value); }
        }
        #endregion
        #region 事件

        #endregion
        #region 私有方法
        private void ImageSwitcher_Loaded()
        {
            if (Images == null || Images.Count == 0)
                return;

            Image image = this.Template.FindName("PART_Image", this) as Image;

            if (image == null)
                return;

            // 翻转动画
            image.RenderTransformOrigin = new Point(0.5, 0.5);
            image.RenderTransform = new ScaleTransform();
            if (Interval != TimeSpan.Zero)
            {
                timer.Interval = Interval;
                timer.Tick += Timer_Tick;
                timer.Start();
            }
        }
        #endregion
        /// <summary>
        /// 控件加载时的事件处理器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageSwitcher_Loaded(object sender, RoutedEventArgs e)
        {
            ImageSwitcher_Loaded();
        }

        /// <summary>
        /// 控件卸载时的事件处理器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageSwitcher_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        /// <summary>
        /// 定时器触发时的事件处理器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            SwitchToNextImage();
        }
        private void ImageSwitcher_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // 如果Interval属性为零，切换到下一张图片
            if (Interval == TimeSpan.Zero)
                SwitchToNextImage();

            // 触发Click事件
            Click?.Invoke(this, new RoutedEventArgs());
        }
    }
    public enum ChangeEffectEnum
    {
        Loop = 0,
        Once = 1,
    }
    public enum AnimationTypeEnum
    {
        // 淡入淡出
        Fade,
        // 滑动
        Slide,
        // 翻转
        Flip
    }
}
