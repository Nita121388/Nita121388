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

namespace WPFTest.NitaCustomControl
{
    [TemplatePart(Name = "PART_PickerImage", Type = typeof(Image))]
    [TemplatePart(Name = "PART_EmojiPopup", Type = typeof(Popup))]
    [TemplatePart(Name = "PART_VariationButton", Type = typeof(ToggleButton))]
    [TemplatePart(Name = "PART_PickButton", Type = typeof(ToggleButton))]
    public class NitaEmojiPicker : ContentControl
    {
        #region 字段
        private Image _PickerImage;
        private Popup _EmojiPopup;
        private ToggleButton _VariationButton;
        private ToggleButton _PickButton;
        //public RelayCommand OnEmojiPickedRelayCommand { get; set; }
        #endregion

        #region Contructors
        static NitaEmojiPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaEmojiPicker),
                new FrameworkPropertyMetadata(typeof(NitaEmojiPicker)));
        }
        #endregion

        #region Property

        #region EmojiGroups
        public IList<EmojiData.Group> EmojiGroups => EmojiData.AllGroups;

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

        public event PropertyChangedEventHandler SelectionChanged;
        public string Selection
        {
            get => (string)GetValue(SelectionProperty);
            set => SetValue(SelectionProperty, value);
        }

        public static readonly DependencyProperty SelectionProperty = DependencyProperty.Register(
          nameof(Selection), typeof(string), typeof(NitaEmojiPicker),
              new FrameworkPropertyMetadata("☺", OnSelectionPropertyChanged));

        private static void OnSelectionPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            (source as NitaEmojiPicker)?.OnSelectionChanged(e.NewValue as string);
        }


        #endregion

        #region Picked
        public event EmojiPickedEventHandler Picked;
        #endregion

        #endregion

        #region Events
        

        private void OnSelectionChanged(string s)
        {
            var is_disabled = string.IsNullOrEmpty(s);
            if (_PickerImage == null) return;
            _PickerImage.SetValue(NitaEmojiImage.SourceProperty, is_disabled ? "???" : s);
            _PickerImage.Opacity = is_disabled ? 0.3 : 1.0;
            SelectionChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selection)));
        }
        //[RelayCommand]
        private void OnEmojiPicked(object sender, RoutedEventArgs e)
        {
            if (sender is Control control && control.DataContext is EmojiData.Emoji emoji)
            {
                if (emoji.VariationList.Count == 0 || sender is Button)
                {
                    Selection = emoji.Text;
                    _PickButton.IsChecked = false;
                    e.Handled = true;
                    Picked?.Invoke(this, new EmojiPickedEventArgs(Selection));
                }
            }
        }

        private void OnPopupLoaded(object sender, RoutedEventArgs e)
        {
            if (!(sender is Popup popup))
                return;

            var child = popup.Child;
            IInputElement old_focus = null;
            child.Focusable = true;
            child.IsVisibleChanged += (o, ea) =>
            {
                if (child.IsVisible)
                {
                    old_focus = Keyboard.FocusedElement;
                    Keyboard.Focus(child);
                }
            };

            popup.Closed += (o, ea) => Keyboard.Focus(old_focus);
        }

        private void OnPopupKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && sender is Popup popup)
            {
                popup.IsOpen = false;
                e.Handled = true;
            }
        }

        #endregion

        #region override
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //OnEmojiPickedRelayCommand = new RelayCommand(OnEmojiPicked);

            _PickerImage = GetTemplateChild("PART_PickerImage") as Image;
            if (_PickerImage == null)
            {
                throw new Exception("嘿！PART_Image从模板中丢失，或者不是Image。抱歉，但是需要此Image。");
            }
            _EmojiPopup = GetTemplateChild("PART_EmojiPopup") as Popup;
            if (_EmojiPopup == null)
            {
                throw new Exception("嘿！PART_Button 从模板中丢失，或者不是 ToggleButton。抱歉，但是需要此 ToggleButton。");
            }
            _EmojiPopup.KeyDown += OnPopupKeyDown;
            _EmojiPopup.Loaded += OnPopupLoaded;

            _VariationButton = GetTemplateChild("PART_VariationButton") as ToggleButton;
            if (_VariationButton == null)
            {
                throw new Exception("嘿！PART_VariationButton 从模板中丢失，或者不是 Button。抱歉，但是需要此 PART_VariationButton。");
            }
            _VariationButton.Click += OnEmojiPicked;

            _PickButton = GetTemplateChild("PART_PickButton") as ToggleButton;
            if (_PickButton == null)
            {
                throw new Exception("嘿！PART_PickButton 从模板中丢失，或者不是 Button。抱歉，但是需要此 PART_PickButton。");
            }
            _PickButton.Click += OnEmojiPicked;

           
        }
        #endregion
    }

    #region EventArgs
    public class EmojiPickedEventArgs : EventArgs
    {
        public string Emoji;
        public EmojiPickedEventArgs() { }
        public EmojiPickedEventArgs(string emoji) => Emoji = emoji;
    }

    public delegate void EmojiPickedEventHandler(object sender, EmojiPickedEventArgs e);
    #endregion
}
