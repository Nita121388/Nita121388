using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPFTest.Enity;
using WPFTest.Util;

namespace WPFTest.NitaCustomControl
{
    /// <summary>
    /// 
    /// </summary>
    [TemplatePart(Name = "PART_NitaClickCanvas", Type = typeof(NitaClickCanvas))]
    [TemplatePart(Name = "PART_Text", Type = typeof(TextBox))]
    public class NitaTextBox: ContentControl
    {
        #region Fields
        private NitaClickCanvas _nitaClickCanvas;
        private TextBox _textBox;
        #endregion

        #region RoutedEvent

        #region Submit

        public event RoutedEventHandler Submit
        {
            add { AddHandler(SubmitEvent, value); }
            remove { RemoveHandler(SubmitEvent, value); }
        }

        public static readonly RoutedEvent SubmitEvent =
            EventManager.RegisterRoutedEvent(
                "Submit", 
                RoutingStrategy.Bubble, 
                typeof(RoutedEventHandler), 
                typeof(NitaTextBox));


        #endregion

        #endregion

        #region Constructors

        static NitaTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaTextBox),
                new FrameworkPropertyMetadata(typeof(NitaTextBox)));
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
            DependencyProperty.Register("SizeType", typeof(SizeType), typeof(NitaTextBox), new PropertyMetadata(SizeType.Medium));
        #endregion

        #region Icon
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(NitaTextBox), new PropertyMetadata(OnIconChanged));
        #endregion

        #region Text
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(NitaTextBox), new PropertyMetadata("TextBox"));
        #endregion

        #region 鼠标悬浮样式 MouseOverStyle
        public static readonly DependencyProperty MouseOverStyleProperty =
            DependencyProperty.Register("MouseOverStyle", typeof(MouseOverStyle), typeof(NitaTextBox), new PropertyMetadata(MouseOverStyle.ChangeColor));
        public MouseOverStyle MouseOverStyle
        {
            get { return (MouseOverStyle)GetValue(MouseOverStyleProperty); }
            set { SetValue(MouseOverStyleProperty, value); }
        }
        #endregion

        #region IsReadOnly
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(NitaTextBox), new PropertyMetadata(false));
        #endregion

        #region ContentMode
        public ContentMode ContentMode
        {
            get { return (ContentMode)GetValue(ContentModeProperty); }
            set { SetValue(ContentModeProperty, value); }
        }

        public static readonly DependencyProperty ContentModeProperty =
            DependencyProperty.Register("ContentMode", typeof(ContentMode), typeof(NitaTextBox), new PropertyMetadata(ContentMode.IconAndText));
        #endregion

        #region ClickColor
        public Brush ClickColor
        {
            get { return (Brush)GetValue(ClickColorProperty); }
            set { SetValue(ClickColorProperty, value); }
        }

        public static readonly DependencyProperty ClickColorProperty =
            DependencyProperty.Register("ClickColor", typeof(Brush), typeof(NitaTextBox), new PropertyMetadata((Brush)Application.Current.Resources["DefaultClickColor"]));
        #endregion

        #endregion

        #region 私有方法
        private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ImageSource newIcon = (ImageSource)e.NewValue;
            var nitaTextBox = (NitaTextBox)d;

            DrawingImage resource = (DrawingImage)d.GetValue(IconProperty);
            nitaTextBox.ClickColor = ColorHelper.GetDominantColorBrush(resource, 1);
        }
        #endregion

        #region Override
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _nitaClickCanvas = GetTemplateChild("PART_NitaClickCanvas") as NitaClickCanvas;
            if (_nitaClickCanvas == null)
            {
                throw new Exception("嘿！ PART_NitaClickCanvas从模板中丢失，或者不是NitaClickCanvas。抱歉，但是需要此Grid。");
            }
            //_nitaClickCanvas.PreviewKeyDown += NitaClickCanvas_PreviewKeyDown;
            _textBox = GetTemplateChild("PART_Text") as TextBox; 
            if (_textBox == null)
            {
                throw new Exception("嘿！ PART_Text 从模板中丢失，或者不是 TextBox。");
            }
            _textBox.PreviewKeyDown += NitaClickCanvas_PreviewKeyDown;
        }
        #endregion

        private void NitaClickCanvas_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RaiseEvent(new RoutedEventArgs(SubmitEvent, this));

                _nitaClickCanvas.EnableActiveStyle();
            }
        }
    }
}
