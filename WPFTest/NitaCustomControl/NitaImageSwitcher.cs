using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using WPFTest.Enity;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Windows.Controls.Image;

namespace WPFTest.NitaCustomControl
{
    /// <summary>
    /// NitaImageSwitcher.xaml 的交互逻辑
    /// 1. 依赖属性 Images 存放ImageSource的集合✅
    /// 2. 默认展示一个图片✅
    /// 3. 点击该图片，变化为另一个图片✅
    /// 7. 该控件对外提供一个点击事件✅
    /// 支持SizeType 快速定义空间大小✅
    /// 6. 该控件支持禁用，禁用时图片透明度0.5✅
    /// 4. 变化过程有动画效果,默认为淡入淡出
    /// 5. 该控件可以设置图片的变化效果：1.依次变化 从头变到尾就不再变化 2. 循环变化 
    /// 8. 该控件支持设置展示第几张图片
    /// </summary>
    [TemplatePart(Name = "PART_Image", Type = typeof(Image))]
    public class NitaImageSwitcher : ContentControl, INotifyPropertyChanged
    {
        #region 字段
        public event RoutedEventHandler Click;
        private int currentIndex = 0;
        private ImageSource _image;
        private Image _imagePart;
        #endregion

        #region 构造函数
        static NitaImageSwitcher()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaImageSwitcher), new FrameworkPropertyMetadata(typeof(NitaImageSwitcher)));
        }
        public NitaImageSwitcher()
        {
            this.Loaded += new RoutedEventHandler(NitaImageSwitcher_Loaded);
        }
        private void NitaImageSwitcher_Loaded(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region 属性

        #region SizeType
        public SizeType SizeType
        {
            get { return (SizeType)GetValue(SizeTypeProperty); }
            set { SetValue(SizeTypeProperty, value); }
        }

        public static readonly DependencyProperty SizeTypeProperty =
            DependencyProperty.Register("SizeType", typeof(SizeType), typeof(NitaImageSwitcher), new PropertyMetadata(SizeType.Medium));
        #endregion

        #region Images 、Image
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
        public ImageSource Icon
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Icon));
            }
        }
        #endregion

        #region IsRandom 是否随机切换
        public static readonly DependencyProperty IsRandomProperty =
            DependencyProperty.Register("IsRandom", typeof(bool), typeof(NitaImageSwitcher), new PropertyMetadata(false));
        public bool IsRandom
        {
            get { return (bool)GetValue(IsRandomProperty); }
            set { SetValue(IsRandomProperty, value); }
        }
        #endregion

        #region ChangeEffect 图片切换模式 
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
        #endregion

        #region ImageIndex 当前图片索引 
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
            set
            {
                SetValue(ImageIndexProperty, value);
                currentIndex = value;
            }
        }
        #endregion

        #endregion
        #region 事件
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _imagePart = GetTemplateChild("PART_Image") as Image;
            if (_imagePart == null)
            {
                throw new Exception("嘿！ PART_Image从模板中丢失，或者不是Image。抱歉，但是需要此Image。");
            }
            this._imagePart.MouseLeftButtonDown += _imagePart_MouseLeftButtonDown;
        }

        private void _imagePart_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SwitchToNextImage();

            Click?.Invoke(this, new RoutedEventArgs());//TODO
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
                target.Icon = image;
            }
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
                    target.Icon = target.Images[cIndex];
                    target.currentIndex = cIndex;
                    target.ImageIndex = cIndex;
                }
            }
        }
        #endregion
        #region 私有方法
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
            this.Icon = Images[nextIndex];
            currentIndex = nextIndex;
        }
        #endregion
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
