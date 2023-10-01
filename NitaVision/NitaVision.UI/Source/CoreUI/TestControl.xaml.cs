using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using UserControl = System.Windows.Controls.UserControl;

namespace NitaVision.UI.Source.CoreUI
{
    /// <summary>
    /// TestControl.xaml 的交互逻辑
    /// </summary>
    public partial class TestControl : UserControl
    {
        public TestControl()
        {
            InitializeComponent();
            NitaControls = new ObservableCollection<UserControl>();
            this.DataContext = this;

            var control1 = new NitaStatusControl();
            control1.NitaColorStatus = new ColorStatus()
            {
                Color = "#F0E68C",
                Status = "解析中"
            };;
            control1.NitaIconStatusMode = IconStatusMode.EllipseText;

            var defaultColorStatus2 = new ColorStatus()
            {
                Color = "#F0E68C",
                Status = "解析完成"
            };
            var control2 = new NitaStatusControl();
            control2.NitaColorStatus = defaultColorStatus2;
            control2.NitaIconStatusMode = IconStatusMode.EllipseText;

            this.DataContext = this;

            var temp = new ObservableCollection<UserControl>();
            temp.Add(control1);
            temp.Add(control2);
            //NitaControls = temp;
        }

        public static readonly DependencyProperty NitaControlsProperty =
            DependencyProperty.Register("NitaControls", typeof(ObservableCollection<UserControl>), typeof(TestControl), new PropertyMetadata(null));

        public ObservableCollection<UserControl> NitaControls
        {
            get 
            { 
                return (ObservableCollection<UserControl>)GetValue(NitaControlsProperty); 
            }
            set 
            {
                SetValue(NitaControlsProperty, value);
                //update(NitaControls);
            }
        }
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }
        public static readonly DependencyProperty SelectedIndexProperty =
          DependencyProperty.Register("SelectedIndex", typeof(int), typeof(TestControl), new PropertyMetadata(0, OnSelectedIndexChanged));
        private static void OnSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TestControl customUserControl = d as TestControl;
            int newIndex = (int)e.NewValue;
            if (newIndex >= 0 && newIndex < customUserControl.NitaControls.Count)
            {
                UserControl userControl = customUserControl.NitaControls[newIndex];
                customUserControl.Content = null;
                customUserControl.Content = userControl;
            }
        }
        private void update(ObservableCollection<UserControl> controls)
        { 
            foreach (UserControl control in controls)
            {
                grid.Children.Add(control);
            }
        }
    }
}
