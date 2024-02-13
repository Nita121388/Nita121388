using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPFTest.DataBinding;
using WPFTest.Enity;

namespace WPFTest.NitaCustomControl
{
    [TemplatePart(Name = "PART_ScrollViewer", Type = typeof(ScrollViewer))]
    public class NitaTabControl: TabControl
    {
        #region Fields
        private ScrollViewer _scrollViewer;
        private Visibility lastVerticalScrollBarVisibility = Visibility.Collapsed;
        #endregion

        #region Construcotrs
        static NitaTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaTabControl),
                new FrameworkPropertyMetadata(typeof(NitaTabControl)));
        }
        #endregion

        #region DependencyProperty
        #region Orientation
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(NitaTabControl), new PropertyMetadata());
        #endregion

        #region AutoScroll
        public bool AutoScroll
        {
            get { return (bool)GetValue(AutoScrollProperty); }
            set { SetValue(AutoScrollProperty, value); }
        }
        public static readonly DependencyProperty AutoScrollProperty =
            DependencyProperty.Register("AutoScroll", typeof(bool), typeof(NitaTabControl), new PropertyMetadata(false));
        #endregion

        #endregion

        #region Override方法
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new NitaTabItem();
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _scrollViewer = GetTemplateChild("PART_ScrollViewer") as ScrollViewer;
            if (_scrollViewer == null)
            {
                throw new Exception("Hey！ PART_ScrollViewer从模板中丢失，或者不是NitaClickCanvas。抱歉，但是需要此Grid。");
            }
           // _scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
        }
        #endregion

        #region Events

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            double delta = e.Delta;
            double offset = _scrollViewer.VerticalOffset;
            double newOffset = offset - delta * 0.1;
            _scrollViewer.ScrollToVerticalOffset(newOffset);
            e.Handled = true;
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var currentVerticalScrollBarVisibility = _scrollViewer.ComputedVerticalScrollBarVisibility;
            if (currentVerticalScrollBarVisibility == Visibility.Visible
                && lastVerticalScrollBarVisibility != Visibility.Visible)
            {
                _scrollViewer.PreviewMouseWheel += ScrollViewer_PreviewMouseWheel;
            }
            else if (currentVerticalScrollBarVisibility != Visibility.Visible
                && lastVerticalScrollBarVisibility == Visibility.Visible)
            {
                _scrollViewer.PreviewMouseWheel -= ScrollViewer_PreviewMouseWheel;
            }
            lastVerticalScrollBarVisibility = currentVerticalScrollBarVisibility;
        }
        #endregion

    }
}
