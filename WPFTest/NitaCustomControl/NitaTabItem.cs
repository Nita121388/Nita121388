using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFTest.Enity;
using WPFTest.Util;

namespace WPFTest.NitaCustomControl
{
    public class NitaTabItem :TabItem
    {
        #region Constructors
        static NitaTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaTabItem), new FrameworkPropertyMetadata(typeof(NitaTabItem)));
        }
        #endregion

        #region DependencyProperty

        #region Icon
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(NitaTabItem), new PropertyMetadata(ColorHelper.GetResourceByKey("Check")));
        #endregion

        #region HeaderText
        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register("HeaderText", typeof(string), typeof(NitaTabItem), new PropertyMetadata());
        #endregion

        #region HeaderSizeType
        public SizeType HeaderSizeType
        {
            get { return (SizeType)GetValue(HeaderSizeTypeProperty); }
            set { SetValue(HeaderSizeTypeProperty, value); }
        }

        public static readonly DependencyProperty HeaderSizeTypeProperty =
            DependencyProperty.Register("HeaderSizeType", typeof(SizeType), typeof(NitaTabItem), new PropertyMetadata(SizeType.Medium));

        #endregion

        #region HeaderContentMode
        public ContentMode HeaderContentMode
        {
            get { return (ContentMode)GetValue(HeaderContentModeProperty); }
            set { SetValue(HeaderContentModeProperty, value); }
        }
        public static readonly DependencyProperty HeaderContentModeProperty =
            DependencyProperty.Register("HeaderContentMode", typeof(ContentMode), typeof(NitaTabItem), new PropertyMetadata(ContentMode.IconAndText));
        #endregion

        #endregion
    }
}
