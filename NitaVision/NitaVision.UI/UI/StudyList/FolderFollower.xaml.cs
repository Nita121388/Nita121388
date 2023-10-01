using NitaVision.UI.Util;
using System;
using System.Collections.Generic;
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

namespace NitaVision.UI.UI.StudyList
{
    /// <summary>
    /// FolderFollower.xaml 的交互逻辑
    /// </summary>
    public partial class FolderFollower : UserControl
    {
        public FolderFollower()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.Register("IsVisible", typeof(bool), typeof(FolderFollower), new PropertyMetadata(false, OnIsVisibleChanged));

        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(Point), typeof(FolderFollower), new PropertyMetadata(new Point(0, 0), OnPositionChanged));

        public static readonly DependencyProperty OffSetProperty =
            DependencyProperty.Register("OffSet", typeof(Point), typeof(FolderFollower), new PropertyMetadata(new Point(0, 0), OnPositionChanged));

        public static readonly DependencyProperty ScaleFactorProperty =
           DependencyProperty.Register("ScaleFactor", typeof(double), typeof(FolderFollower), new PropertyMetadata(1.0));

        public static readonly DependencyProperty FolderProperty =
           DependencyProperty.Register("Folder", typeof(Folder), typeof(FolderFollower));

        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }
        public Folder Folder
        {
            get { return (Folder)GetValue(FolderProperty); }
            set { SetValue(FolderProperty, value); }
        }

        public Point Position
        {
            get { return (Point)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        public double ScaleFactor
        {
            get { return (double)GetValue(ScaleFactorProperty); }
            set { SetValue(ScaleFactorProperty, value); }
        }
        public Point OffSet
        {
            get { return (Point)GetValue(OffSetProperty); }
            set { SetValue(OffSetProperty, value); }
        }

        private static void OnIsVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FolderFollower;
            var isVisible = (bool)e.NewValue;
            control.follwer.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        private static void OnPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FolderFollower;
            if (control == null) return;
            control.follwer.Width *= control.ScaleFactor;
            control.follwer.Height *= control.ScaleFactor;
            control.follwer.Margin = new Thickness(control.Position.X, control.Position.Y, 0, 0);
        }
    }
}
