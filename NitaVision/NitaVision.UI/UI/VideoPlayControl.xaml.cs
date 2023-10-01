using NitaVision.UI.Source.Convert;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System.Windows.Threading;
using Vlc.DotNet.Core;

namespace NitaVision.UI.UI
{
    /// <summary>
    /// VideoPlayControl.xaml 的交互逻辑
    /// </summary>
    public partial class VideoPlayControl : UserControl,INotifyPropertyChanged
    {
        #region 变量
        private DirectoryInfo vlcLibDirectory;
        public PlayState _playState = PlayState.Unknow;
        private Uri mediaUri;  // 保存媒体源
        private DispatcherTimer timer; // 用于记录播放位置
        private float position = 0; // 保存播放位置
        public event PropertyChangedEventHandler PropertyChanged;
        public PlayState VideoPlayState
        {
            get { return _playState; }
            set
            {
                _playState = value;
                OnPropertyChanged(nameof(VideoPlayState));
            }
        }
        private float lastPlayTime = 0;
        private float lastPlayTimeGlobal = 0;
        public string filePath;
        #endregion
        #region 构造函数
        public VideoPlayControl()
        {
            InitializeComponent();
            this.DataContext = this;
            var currentAssembly = Assembly.GetEntryAssembly();
            if (currentAssembly == null)
            {
                //todo
                return;
            }
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            if (currentDirectory == null)
            {
                //todo
                return;
            }
            vlcLibDirectory = new DirectoryInfo(System.IO.Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));
            this.VlcControl.SourceProvider.CreatePlayer(vlcLibDirectory);
            VideoPlayState = PlayState.Unknow;
        }
        #endregion 构造函数
        private void MediaPlayer_LengthChanged(object? sender, VlcMediaPlayerLengthChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                this.VideoSlider.Minimum = position;
                var maxValue = this.VlcControl.SourceProvider.MediaPlayer.Length;
                this.VideoSlider.Maximum = maxValue;
                TimeSpan ts = TimeSpan.FromMilliseconds(maxValue);
                this.EndTextBlock.Text = ts.ToString(@"dd\:hh\:mm\:ss"); ;
            }), DispatcherPriority.Normal);
        }
        public void OnPlay(string filePath)
        {
            try
            {
                this.VlcControl.SourceProvider.MediaPlayer.SetMedia(new Uri(filePath)); // 本地文件。
                this.VlcControl.SourceProvider.MediaPlayer.Play();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.VlcControl.Dispose();
            }
        }
        #region 进度条
        private void SliderDragStarted(object sender, DragStartedEventArgs e)
        {
        }
        private void SliderDragDelta(object sender, DragDeltaEventArgs e)
        {
            var position = (float)(this.VideoSlider.Value / this.VideoSlider.Maximum);
            if (position == 1)
            {
                position = 0.99f;
            }
            this.VlcControl.SourceProvider.MediaPlayer.Position = position;
        }
        private void SliderDragCompleted(object sender, DragCompletedEventArgs e)
        {

        }
        private void VideoSliderMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender == null) return;
            Slider slider = sender as Slider;
            if (slider == null) return;
            var value = (e.GetPosition(slider).X / slider.ActualWidth) * (slider.Maximum - slider.Minimum);
            this.VideoSlider.Value = value;
            var position = (float)(this.VideoSlider.Value / this.VideoSlider.Maximum);
            if (position == 1)
            {
                position = 0.99f;
            }
            this.VlcControl.SourceProvider.MediaPlayer.Position = position;
        }
        #endregion
        private void OnStopButtonClick(object sender, RoutedEventArgs e)
        {
            this.VlcControl?.Dispose();
        }
        public float GetCurrentTime()
        {
            float currentTime = this.VlcControl.SourceProvider.MediaPlayer.Time;
            var tick = float.Parse(DateTime.Now.ToString("fff"));
            if (lastPlayTime == currentTime && lastPlayTime != 0)
            {
                currentTime += (tick - lastPlayTimeGlobal);
            }
            else
            {
                lastPlayTime = currentTime;
                lastPlayTimeGlobal = tick;
            }

            return currentTime * 0.001f;
        }
        public void BtnPer_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BtnPause_Click(object sender, RoutedEventArgs e)
        {
            if (VideoPlayState == PlayState.Play)
            {
                this.VlcControl.SourceProvider.MediaPlayer.Pause();
                VideoPlayState = PlayState.Pause;
                timer.Stop();
                position = this.VlcControl.SourceProvider.MediaPlayer.Position;
            }
        }
        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (VideoPlayState != PlayState.Play)
            {
                VideoPlayState = PlayState.Play;
                if (mediaUri == null) // 如果是第一次设置媒体源
                {
                    mediaUri = new Uri(filePath);
                    this.VlcControl.SourceProvider.MediaPlayer.SetMedia(mediaUri);
                    this.VlcControl.SourceProvider.MediaPlayer.LengthChanged += MediaPlayer_LengthChanged;
                    timer = new DispatcherTimer(); // 创建计时器
                    timer.Interval = TimeSpan.FromSeconds(1); // 设置间隔为1秒
                    timer.Tick += Timer_Tick; // 设置计时器事件处理方法
                }
                else
                {
                    this.VlcControl.SourceProvider.MediaPlayer.Position = position;
                }
                this.VlcControl.SourceProvider.MediaPlayer.Play();
                timer.Start();
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            position = this.VlcControl.SourceProvider.MediaPlayer.Position;
            var msValue = position * this.VideoSlider.Maximum;
            this.VideoSlider.Value = msValue;
            TimeSpan ts = TimeSpan.FromMilliseconds(msValue);
            this.StartTextBlock.Text = ts.ToString(@"dd\:hh\:mm\:ss"); ;
        }
        public void BtnNext_Click(object sender, RoutedEventArgs e)
        {

        }
        private void FastRewindClick(object sender, RoutedEventArgs e)
        {

        }
        private void FastForwardClick(object sender, RoutedEventArgs e)
        {

        }

        #region 私有方法
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
