using System;
using System.Collections.Generic;
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

namespace NitaVision.UI.UI
{
    /// <summary>
    /// SubTitleControl.xaml 的交互逻辑
    /// </summary>
    public partial class SubTitleControl : UserControl
    {
        public SubTitleControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public static readonly DependencyProperty SelectedProperty = DependencyProperty.Register("Selected", typeof(bool), typeof(SubTitleControl), new PropertyMetadata(false));
        public static readonly DependencyProperty IsPlayProperty = DependencyProperty.Register("IsPlay", typeof(bool), typeof(SubTitleControl), new PropertyMetadata(false));

        public static readonly RoutedEvent SelectedChangedEvent = EventManager.RegisterRoutedEvent("SelectedChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<bool>), typeof(SubTitleControl));
        public static readonly RoutedEvent PlayEvent = EventManager.RegisterRoutedEvent("PlayEvent", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<bool>), typeof(SubTitleControl));

        public bool Selected
        {
            get { return (bool)GetValue(SelectedProperty); }
            set { SetValue(SelectedProperty, value); }
        }
        public bool IsPlay
        {
            get { return (bool)GetValue(IsPlayProperty); }
            set { SetValue(IsPlayProperty, value); }
        }
        public event RoutedPropertyChangedEventHandler<bool> SelectedChanged
        {
            add { AddHandler(SelectedChangedEvent, value); }
            remove { RemoveHandler(SelectedChangedEvent, value); }
        }
        public event RoutedPropertyChangedEventHandler<bool> Play
        {
            add { AddHandler(PlayEvent, value); }
            remove { RemoveHandler(PlayEvent, value); }
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            Selected = !Selected;
            RaiseEvent(new RoutedPropertyChangedEventArgs<bool>(!Selected, Selected, SelectedChangedEvent));
        }
        public void PlayThis()
        {
            IsPlay = !IsPlay;
            RaiseEvent(new RoutedPropertyChangedEventArgs<bool>(!IsPlay, IsPlay, PlayEvent));
        }
    }
}
