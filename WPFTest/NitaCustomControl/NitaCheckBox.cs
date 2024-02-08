using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;
using WPFTest.Util;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using WPFTest.Enity;

namespace WPFTest.NitaCustomControl
{
    public class NitaCheckBox : CheckBox
    {
        //public ObservableCollection<ImageSource> Icons { get; set; }
        static NitaCheckBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaCheckBox), 
                new FrameworkPropertyMetadata(typeof(NitaCheckBox)));
        }

        public NitaCheckBox()
        {
            Icons = new ObservableCollection<ImageSource>();
            GetImages();
            ///this.Loaded += new RoutedEventHandler(NitaCheckBox_Loaded);
        }

        #region 属性

        #region SizeType
        public SizeType SizeType
        {
            get { return (SizeType)GetValue(SizeTypeProperty); }
            set { SetValue(SizeTypeProperty, value); }
        }
        public static readonly DependencyProperty SizeTypeProperty =
            DependencyProperty.Register("SizeType", typeof(SizeType), typeof(NitaCheckBox), new PropertyMetadata(SizeType.Medium));
        #endregion



        #region SizeType
        public ObservableCollection<ImageSource> Icons
        {
            get { return (ObservableCollection<ImageSource>)GetValue(IconsProperty); }
            set { SetValue(IconsProperty, value); }
        }
        public static readonly DependencyProperty IconsProperty =
            DependencyProperty.Register("Icons", typeof(ObservableCollection<ImageSource>), typeof(NitaCheckBox), new PropertyMetadata());
        #endregion

        #endregion

        private void NitaCheckBox_Loaded(object sender, RoutedEventArgs e)
        {
            Icons = new ObservableCollection<ImageSource>();
            GetImages();
        }

        private void GetImages()
        {
            Icons.Add(ColorHelper.GetResourceByKey("UnCheck"));
            Icons.Add(ColorHelper.GetResourceByKey("Check"));
        }
    }
}
