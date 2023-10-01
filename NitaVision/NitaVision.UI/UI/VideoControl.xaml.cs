using NitaVision.UI.Source.Convert;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace NitaVision.UI.UI
{
    /// <summary>
    /// VideoControl.xaml 的交互逻辑
    /// </summary>
    public partial class VideoControl : UserControl,INotifyPropertyChanged
    {
        private bool _isPlay = false;
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsPlay
        {
            get { return _isPlay; }
            set
            {
                _isPlay = value;
                OnPropertyChanged(nameof(IsPlay));
            }
        }
        public VideoControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void GridSplitter_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var grid = (Grid)VisualTreeHelper.GetParent((GridSplitter)sender);
            var topRow = grid.RowDefinitions[0];
            var gridSplitter = grid.RowDefinitions[1];
            var delta = e.VerticalChange;
            topRow.Height = new GridLength(topRow.ActualHeight + delta);
            gridSplitter.Height = new GridLength(2);
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                ImageAnimation();
                var dialog = new Microsoft.Win32.OpenFileDialog();
                dialog.Title = "选择视频文件";
                dialog.Filter = "视频文件 (*.mp4;*.avi;*.wmv;*.mov;*.mkv;)|*.mp4;*.avi;*.wmv;*.mov;*.mkv;|所有文件 (*.*)|*.*";
                dialog.Multiselect = true;
                var result = dialog.ShowDialog();
                if (result == true)
                {
                    IsPlay = true;
                    var fileNames = dialog.FileNames;
                    this.VideoPlayControl.filePath = fileNames[0];
                    this.VideoPlayControl.VideoPlayState = PlayState.Stop;
                    /*var whispter = new WhisperHelper();
                    var videoManager = new VideoManager();
                    foreach (var file in fileNames)
                    {
                        var fileName = Path.GetFileName(file);
                        var outFileName = ReplaceFileExtension(fileName);
                        //videoManager.ConvertToWav(fileName,outFileName);
                        //whispter.GetSubtitle(Core.ASRModelType.WhisperTiny, outFileName);
                    }*/
                }
            }
        }
        private string ReplaceFileExtension(string inputPath)
        {
            string extension = Path.GetExtension(inputPath);
            string newPath = inputPath.Replace(extension, ".wav");
            return newPath;
        }
        private void ImageAnimation()
        {
            Image image = this.Import;
            // 创建一个缩放变换对象
            ScaleTransform scale = new ScaleTransform();
            // 设置缩放中心为图片中心
            scale.CenterX = image.ActualWidth / 2;
            scale.CenterY = image.ActualHeight / 2;
            // 将缩放变换应用到图片
            image.RenderTransform = scale;

            DoubleAnimation animation = new DoubleAnimation();
            // 设置动画的开始值为1，结束值为0.5，持续时间为0.1秒
            animation.From = 1; animation.To = 0.2;
            animation.Duration = TimeSpan.FromSeconds(0.1);
            animation.AutoReverse = true;
            // 开始播放动画，将缩放变换的X和Y属性绑定到动画
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
