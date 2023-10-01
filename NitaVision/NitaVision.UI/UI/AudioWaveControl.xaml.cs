using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
using NAudio.Wave;

namespace NitaVision.UI.UI
{
    /// <summary>
    /// AudioWaveControl.xaml 的交互逻辑
    /// </summary>
    public partial class AudioWaveControl : UserControl, INotifyPropertyChanged
    {
        #region 变量
        private string audioFilePath;
        private double Duration;
        private double CurrentTime;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public AudioWaveControl()
        {
            InitializeComponent();
            DataContext = this;
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            if (currentDirectory == null)
            {
                //todo
                return;
            }
            audioFilePath = System.IO.Path.Combine(currentDirectory, "Test2.wav");
            if (string.IsNullOrEmpty(audioFilePath) || !File.Exists(audioFilePath))
            {
                return;
            }
            LoadAudioFile(audioFilePath);
        }
        public ISeries[] Series { get; set; } =
        {
            new StackedColumnSeries<float>
            {
                Values = new ObservableCollection<float>
                    {
                    },
                Stroke = null,
                DataLabelsPaint = new SolidColorPaint(new SKColor(45, 45, 45)),
                DataLabelsSize = 14,
                DataLabelsPosition = DataLabelsPosition.Middle,
                MaxBarWidth = 1
            },
            new StackedColumnSeries<float>
            {
                Values = new float[]
                    {
                    },
                Stroke = null,
                DataLabelsPaint = new SolidColorPaint(new SKColor(45, 45, 45)),
                DataLabelsSize = 14,
                DataLabelsPosition = DataLabelsPosition.Middle,
                MaxBarWidth = 1
            },
        };
        public Axis[] XAxes { get; set; } =
        {
            new Axis
            {
               IsVisible = false,
               UnitWidth = 1,
               CustomSeparators = null,
               MinStep = 1,
            }
        };
        public Axis[] YAxes { get; set; } =
        {
            new Axis
            {
               IsVisible = false,
            }
        };
        public void LoadAudioFile(string fileName, double multiplier = 16_000)
        {
            var startTime = CurrentTime;
            XAxes[0].MinLimit = startTime;
            var values = new ObservableCollection<ObservablePoint>();
            var minvalues = new List<float>();
            var maxvalues = new List<float>();
            using (var audioFile = new AudioFileReader(fileName))
            {
                Duration = audioFile.TotalTime.TotalSeconds;
                var sampleRate = audioFile.WaveFormat.SampleRate;
                var sampleCount = (int)audioFile.Length / audioFile.WaveFormat.BitsPerSample / 8;
                var samplesPerPoint = sampleRate / 400.0;
                var secondsPerPoint = samplesPerPoint / sampleRate;
                var currentTime = 0.0;
                var samples = new float[sampleCount];
                var audio = new List<double>(sampleCount);
                int samplesRead = 0;
                int channelCount = audioFile.WaveFormat.Channels;
                var buffer = new float[sampleRate * channelCount];
                while ((samplesRead = audioFile.Read(buffer, 0, buffer.Length)) > 0)
                    audio.AddRange(buffer.Take(samplesRead).Select(x => x * multiplier));
                samples = audio.Select(d => (float)d).ToArray();
                for (int i = 0; i < sampleCount; i += (int)samplesPerPoint)
                {
                    var max = 0f;
                    var min = 0f;
                    for (int j = 0; j < samplesPerPoint && i + j < sampleCount; j++)
                    {
                        var sample = samples[i + j];
                        if (sample > max) max = sample;
                        if (sample < min) min = sample;
                    }
                    var point = new ObservablePoint(currentTime, (max + min) / 2);
                    values.Add(point);
                    minvalues.Add(min);
                    maxvalues.Add(max);
                    currentTime += secondsPerPoint;
                }
                CurrentTime = currentTime;
            }
            this.Series[0] = new ColumnSeries<float>
            {
                Values = minvalues,
            };
            this.Series[1] = new ColumnSeries<float>
            {
                Values = maxvalues,
            };
            XAxes[0].MaxLimit = CurrentTime;
            OnPropertyChanged(nameof(Series));
        }
    }
}
