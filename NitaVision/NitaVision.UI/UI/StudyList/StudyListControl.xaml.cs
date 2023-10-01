using NitaVision.UI.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// StudyListControl.xaml 的交互逻辑
    /// </summary>
    public partial class StudyListControl : UserControl,INotifyPropertyChanged
    {
        private double scaleFactor = 0.6;
        private Point offSet;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Point OffSet
        {
            get { return offSet; }
            set { 
                offSet = value;
                OnPropertyChanged(nameof(OffSet));
            }
        }
        public double ScaleFactor
        {
            get { return scaleFactor; }
            set
            {
                scaleFactor = value;
                OnPropertyChanged(nameof(ScaleFactor));
            }
        }
        public StudyListControl()
        {
            InitializeComponent();
            this.DataContext = this;
            offSet = new Point(0, TopRow.ActualHeight);
            ScaleFactor = 0.6;
            LogUtil.Log($"StudyListControl:{offSet.X}-{offSet.Y}");
        }
        private void CollapseButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as ToggleButton;
            var tag = button.Tag as DrawingImage;

            if (tag != null)
            {
                if (button.IsChecked == true)
                {
                    LeftColumn.Width = new GridLength(20);
                    button.Tag = FlipTag(tag);
                }
                else
                {
                    LeftColumn.Width = new GridLength(1, GridUnitType.Star);
                    button.Tag = FlipTag(tag);
                    //tag.RenderTransform = new ScaleTransform(1, 1);
                }
            }
        }
        private DrawingImage FlipTag(DrawingImage tag)
        {
            Drawing drawing = tag.Drawing;
            DrawingImage drawingImage = new DrawingImage(drawing);
            drawingImage.Freeze();
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.PushTransform(new ScaleTransform(-1, 1));
                drawingContext.DrawImage(drawingImage, new Rect(new Point(-drawing.Bounds.Right, 0), new Point(0, drawing.Bounds.Bottom)));
            }
            return new DrawingImage(drawingVisual.Drawing);
        }
        private void TopRow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double topRowHeight = TopRow.ActualHeight;
            offSet = new Point(0, topRowHeight);
            LogUtil.Log($"TopRow_SizeChanged:{offSet.X}-{offSet.Y}");
        }
    }
}
