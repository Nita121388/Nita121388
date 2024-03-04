//------------------------------------------
// Author: @Nita
// Description: 这是一个NitaEmojiPopup
// Time: 2024/3/2 13:58:28
//------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using WPFTest.Emoji.Internal;
using WPFTest.NitaCustomControl.ControlUtil;
using WPFTest.Util;
using Emoji = WPFTest.Emoji.Internal.EmojiData.Emoji;

namespace WPFTest.NitaCustomControl
{
    [TemplatePart(Name = "PART_EmojiPopup", Type = typeof(Popup))]
    [TemplatePart(Name = "PART_VariationButton", Type = typeof(ToggleButton))]
    [TemplatePart(Name = "PART_TabControl", Type = typeof(TabControl))]
    [TemplatePart(Name = "PART_EmojiListView", Type = typeof(ListView))]
    [TemplatePart(Name = "PART_VariationPopup", Type = typeof(Popup))]
    [TemplatePart(Name = "PART_PickButton", Type = typeof(ToggleButton))]
    public class NitaEmojiPopup : ContentControl
    {
        #region 字段
        private Popup _EmojiPopup;
        private ToggleButton _PickButton;
        private ListView _EmojiListView;
        private TabControl _tabControl;
        #endregion

        #region Contructors
        static NitaEmojiPopup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaEmojiPopup),
                new FrameworkPropertyMetadata(typeof(NitaEmojiPopup)));
        }
        #endregion

        #region Property

        #region EmojiGroups
        public IList<EmojiData.Group> EmojiGroups
        {
            get 
            {
                return EmojiData.AllGroups;
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
         nameof(Selection), typeof(string), typeof(NitaEmojiPopup),
             new FrameworkPropertyMetadata("☺", OnSelectionPropertyChanged));

        private static void OnSelectionPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            (source as NitaEmojiPopup)?.OnSelectionChanged(e.NewValue as string);
        }

        private void OnSelectionChanged(string s)
        {
            var is_disabled = string.IsNullOrEmpty(s);
            SelectionChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selection)));
        }
        #endregion

        #region IsOpen
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(NitaEmojiPopup), 
                new PropertyMetadata(false));

        #endregion

        #endregion

        #region Support  Events
        public event EmojiPickedEventHandler Picked;
        public event PropertyChangedEventHandler SelectionChanged;
        #endregion

        #region Events
        private void VariationButton_OnEmojiPicked(object sender, RoutedEventArgs e)
        {
            var toggle = sender as ToggleButton;
            if (toggle == null) return;
            LoadPickButton(toggle);
            {
                if (toggle.DataContext is EmojiData.Emoji emoji)
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
            _EmojiPopup = GetTemplateChild<Popup>("PART_EmojiPopup");
            _EmojiPopup.Loaded += OnPopupLoaded;
            _EmojiPopup.KeyDown += OnPopupKeyDown;
            _EmojiPopup.Opened += _EmojiPopup_Opened;

            _tabControl = GetTemplateChild<TabControl>("PART_TabControl");
            _tabControl.SelectionChanged += _tabControl_SelectionChanged;

        }

        private void _EmojiPopup_Opened(object? sender, EventArgs e)
        {
            _EmojiListView = FindVisualChild<ListView>(_tabControl);
            if (_EmojiListView != null)
            {
               /* MessageBox.Show(
                    $"{ _EmojiListView.ItemsSource.GetType().Name}\r\n" +
                    $"{_EmojiListView.ItemsSource.GetType().Name}\r\n" +
                    $"{_EmojiListView.Items.GetType().Name}\r\n" +
                    $"{_EmojiListView.Items[0].GetType().Name}\r\n"
                );*/
                LoadVariationButton();

               /* var toggles = FindVariationButton(_EmojiListView);
                foreach (var toggle in toggles)
                {
                    MessageBox.Show(
                    $"{toggle.DataContext.GetType().Name}\r\n"
                    );
                    break;
                }*/
                //_EmojiListView.Items[0].GetType().Name;
            }
        }

        private void _tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadVariationButton();
        }

        private void LoadVariationButton()
        {
            if (_EmojiListView == null)
                return;
            var toggles = FindVariationButton(_EmojiListView);
            foreach (var toggle in toggles)
            {
                toggle.Click += VariationButton_OnEmojiPicked;
            }
        }

        private void LoadPickButton(ToggleButton pickToggleButton)
        {
            if (pickToggleButton == null)
                return;

            var buttons = FindVisualChildrenByName<ToggleButton>(pickToggleButton, "PART_PickButton");


        }

        public static List<ToggleButton> FindVariationButton(ListView listView)
        {
            if (listView == null)
                return null;

            List<ToggleButton> toggles = new List<ToggleButton>();
            foreach (var item in listView.Items)
            { 
                
            }
            for (int i = 0; i < listView.Items.Count; i++)
            {
                var listItem = listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;
                if (listItem == null) continue;
                //var contentPresenter = FindVisualChild<ContentPresenter>(listItem);
                var variationButtons = FindVisualChildrenByName<ToggleButton>(listItem, "PART_VariationButton");
                //var dataTemplate = contentPresenter?.ContentTemplate as DataTemplate;
                //var variationButton = dataTemplate?.FindName("PART_VariationButton", contentPresenter) as ToggleButton;
                toggles.AddRange(variationButtons);
            }
            /*
            foreach (var item in listView.Items)
            {
                var listItem = item as ListViewItem;//listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;
                if (listItem == null) continue;
                var contentPresenter = FindVisualChild<ContentPresenter>(listItem);
                var dataTemplate = contentPresenter?.ContentTemplate as DataTemplate;
                var variationButton = dataTemplate?.FindName("PART_VariationButton", contentPresenter) as ToggleButton;
                toggles.Add(variationButton);
            }*/
            return toggles;
        }

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

        /// <summary>
        /// 在给定的DependencyObject中查找第一个匹配的子元素。
        /// </summary>
        /// <typeparam name="T">要查找的子元素的类型。</typeparam>
        /// <param name="parent">要在其中查找子元素的DependencyObject。</param>
        /// <returns>返回找到的第一个匹配的子元素，如果没有找到则返回null。</returns>
        public static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null)
                return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T result)
                    return result;

                var foundChild = FindVisualChild<T>(child);
                if (foundChild != null)
                    return foundChild;
            }

            return null;
        }

        /// <summary>
        /// 在给定的DependencyObject中查找具有指定名称的子元素。
        /// </summary>
        /// <typeparam name="T">要查找的子元素的类型。</typeparam>
        /// <param name="parent">要在其中查找子元素的DependencyObject。</param>
        /// <param name="name">要查找的子元素的名称。</param>
        /// <returns>返回找到的具有指定名称的子元素，如果没有找到则返回null。</returns>
        public static T FindVisualChildByName<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            if (parent == null)
                return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T result && (child as FrameworkElement)?.Name == name)
                    return result;

                var foundChild = FindVisualChildByName<T>(child, name);
                if (foundChild != null)
                    return foundChild;
            }

            return null;
        }

        /// <summary>
        /// 在给定的DependencyObject中查找所有具有指定名称的子元素。
        /// </summary>
        /// <typeparam name="T">要查找的子元素的类型。</typeparam>
        /// <param name="parent">要在其中查找子元素的DependencyObject。</param>
        /// <param name="name">要查找的子元素的名称。</param>
        /// <returns>返回一个包含所有找到的具有指定名称的子元素的列表。</returns>
        public static List<T> FindVisualChildrenByName<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            List<T> foundChildren = new List<T>();

            if (parent == null)
                return foundChildren;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T result && (child as FrameworkElement)?.Name == name)
                    foundChildren.Add(result);

                foundChildren.AddRange(FindVisualChildrenByName<T>(child, name));
            }

            return foundChildren;
        }
        #endregion

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
