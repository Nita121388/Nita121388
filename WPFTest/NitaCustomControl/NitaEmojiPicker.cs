using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using WPFTest.Emoji.Internal;
using WPFTest.Enity;
using WPFTest.NitaCustomControl.ControlUtil;

namespace WPFTest.NitaCustomControl
{
    [TemplatePart(Name = "PART_PickerButton", Type = typeof(ToggleButton))]
    [TemplatePart(Name = "PART_PickerImage", Type = typeof(Image))]
    [TemplatePart(Name = "PART_NitaEmojiPopup", Type = typeof(NitaEmojiPopup))]
    public class NitaEmojiPicker : ContentControl
    {
        #region 字段
        private ToggleButton _PickerButton;
        private Image _PickerImage;
        private NitaEmojiPopup _NitaEmojiPopup;
        #endregion

        #region Contructors
        static NitaEmojiPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaEmojiPicker),
                new FrameworkPropertyMetadata(typeof(NitaEmojiPicker)));
        }
        #endregion

        #region Property

        #region SizeType
        public SizeType SizeType
        {
            get { return (SizeType)GetValue(SizeTypeProperty); }
            set { SetValue(SizeTypeProperty, value); }
        }

        public static readonly DependencyProperty SizeTypeProperty =
            DependencyProperty.Register("SizeType", typeof(SizeType), 
                typeof(NitaEmojiPicker), 
                new PropertyMetadata(SizeType.Medium));
        #endregion
        #region EmojiPickIcon

        #endregion

        #region FontSize
        public new double FontSize
        {
            get
            {
                if (_PickerImage != null)
                {
                    return _PickerImage.Height * 0.75;
                }
                return 0;
            }
            set
            {
                if (_PickerImage != null)
                {
                    _PickerImage.Height = value / 0.75;
                }
            }
        }
        #endregion

        #region Selection
        public string Selection
        {
            get => (string)GetValue(SelectionProperty);
            set => SetValue(SelectionProperty, value);
        }

        public static readonly DependencyProperty SelectionProperty = DependencyProperty.Register(
         nameof(Selection), typeof(string), typeof(NitaEmojiPicker),
             new FrameworkPropertyMetadata("☺"));
        #endregion

        #endregion

        #region override
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _PickerButton = GetTemplateChild<ToggleButton>("PART_PickerButton");
            _PickerImage = GetTemplateChild<Image>("PART_PickerImage");

            _NitaEmojiPopup = GetTemplateChild<NitaEmojiPopup>("PART_NitaEmojiPopup");

            _NitaEmojiPopup.SelectionChanged += NitaEmojiPopup_SelectionChanged;
        }
        private void NitaEmojiPopup_SelectionChanged(object? sender, PropertyChangedEventArgs e)
        {
            this.Selection = _NitaEmojiPopup.Selection;
        }
        #endregion


        #region GetTemplateChild
        /// <summary>
        /// 获取模板中的子元素。
        /// </summary>
        /// <typeparam name="T">子元素的类型。</typeparam>
        /// <param name="childName">子元素的名称。</param>
        /// <returns>返回指定类型的子元素。</returns>
        public T GetTemplateChild<T>(string childName) where T : class
        {
            T child = GetTemplateChild(childName) as T;
            if (child == null)
            {
                throw new Exception($"Error: {childName} is missing from the template or is not a {typeof(T).Name}. A {typeof(T).Name} is required.");
            }
            return child;
        }
        #endregion
    }
}
