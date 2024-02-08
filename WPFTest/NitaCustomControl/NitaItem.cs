using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPFTest.Converter;
using WPFTest.Enity;
using WPFTest.Util;
using WPFTest.ViewModel;

namespace WPFTest.NitaCustomControl
{
    [TemplatePart(Name = "PART_Text", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_NitaClickCanvas", Type = typeof(NitaClickCanvas))]
    public class NitaItem : ContentControl
    {
        #region Fields
        private NitaItemModel _model;
        private TextBox _textBox;
        private NitaClickCanvas _nitaClickCanvas;
        #endregion

        #region RoutedEvent

        #region Click

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent(
                "Click",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(NitaItem));


        #endregion

        #endregion

        #region DependencyProperty

        #region 鼠标悬浮样式 MouseOverStyle

        public static readonly DependencyProperty MouseOverStyleProperty =
            DependencyProperty.Register("MouseOverStyle", 
                typeof(MouseOverStyle), 
                typeof(NitaItem), 
                new PropertyMetadata(MouseOverStyle.ChangeColor));
        public MouseOverStyle MouseOverStyle
        {
            get { return (MouseOverStyle)GetValue(MouseOverStyleProperty); }
            set { SetValue(MouseOverStyleProperty, value); }
        }
        #endregion

        #region SizeType
        public SizeType SizeType
        {
            get { return (SizeType)GetValue(SizeTypeProperty); }
            set { SetValue(SizeTypeProperty, value); }
        }

        public static readonly DependencyProperty SizeTypeProperty =
            DependencyProperty.Register("SizeType", typeof(SizeType), typeof(NitaItem), new PropertyMetadata(SizeType.Medium));
        #endregion

        #region Model

        public NitaItemModel Model
        {
            get { return (NitaItemModel)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Model", 
                typeof(NitaItemModel), 
                typeof(NitaItem), 
                new PropertyMetadata(OnModeChanged));

        #endregion

        #region IsReadOnly

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(NitaItem), new PropertyMetadata(false));

        #endregion

        #region ClickColor
        public Brush ClickColor
        {
            get { return (Brush)GetValue(ClickColorProperty); }
            set { SetValue(ClickColorProperty, value); }
        }

        public static readonly DependencyProperty ClickColorProperty =
            DependencyProperty.Register("ClickColor", typeof(Brush), typeof(NitaItem), new PropertyMetadata((Brush)Application.Current.Resources["DefaultClickColor"]));
        #endregion

        #endregion

        #region Contructors

        static NitaItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaItem),
                new FrameworkPropertyMetadata(typeof(NitaItem)));
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

            _textBox = GetTemplateChild("PART_Text") as TextBox;
            if (_textBox == null)
            {
                throw new Exception("嘿！ PART_Text 从模板中丢失，或者不是 TextBox。");
            }
            _textBox.PreviewKeyDown += NitaClickCanvas_PreviewKeyDown;
        }
        #endregion

        #region 私有方法
        private static void OnModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var nitaItem = (NitaItem)d;

            NitaItemModel mode = (NitaItemModel)d.GetValue(ModeProperty);
            if (mode != null)
            {

                if (mode.Mode == ContentMode.ColorOnly
                    || mode.Mode == ContentMode.ColorAndText
                    || mode.Color != (String)Application.Current.FindResource("DefaultClickColorStr"))
                {
                    nitaItem.ClickColor = ColorConverterHelper.ConvertStringToBrush(mode.Color);
                }
                else if (mode.Mode == ContentMode.IconOnly
                    || mode.Mode == ContentMode.IconAndText)
                {
                    //nitaItem.ClickColor = ColorConverterHelper.ConvertStringToBrush(mode.Color);
                    nitaItem.ClickColor = ColorHelper.GetDominantColorBrush(mode.Icon);
                }
            }

        }
        private void NitaClickCanvas_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RaiseEvent(new RoutedEventArgs(ClickEvent, this));

                _nitaClickCanvas.EnableActiveStyle();
            }
        }
        #endregion

    }

}
