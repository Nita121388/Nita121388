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
using WPFTest.Enity;

namespace WPFTest.NitaCustomControl
{
    public class NitaBorder : Border
    {
        static NitaBorder()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaBorder), new FrameworkPropertyMetadata(typeof(NitaBorder))); 
        }
        #region DependencyProperty

        #region 鼠标悬浮背景颜色  MouseOverBackground
        /// <summary>
        /// 鼠标悬浮背景颜色
        /// </summary>
        public static readonly DependencyProperty MouseOverBackgroundProperty =
            DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(NitaBorder), new PropertyMetadata((Brush)Application.Current.Resources["DefaultMouseOverBackground"]));

        public Brush MouseOverBackground
        {
            get { return (Brush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }
        #endregion

        #region 鼠标悬浮样式 MouseOverStyle
        /// <summary>
        /// 鼠标悬浮样式
        /// </summary>
        public static readonly DependencyProperty MouseOverStyleProperty =
            DependencyProperty.Register("MouseOverStyle", typeof(MouseOverStyle), typeof(NitaBorder), new PropertyMetadata(MouseOverStyle.ZoomAndChangeColor));
        public MouseOverStyle MouseOverStyle
        {
            get { return (MouseOverStyle)GetValue(MouseOverStyleProperty); }
            set { SetValue(MouseOverStyleProperty, value); }
        }
        #endregion

        #region 尺寸类型 SizeType
        public SizeType SizeType
        {
            get { return (SizeType)GetValue(SizeTypeProperty); }
            set { SetValue(SizeTypeProperty, value); }
        }

        public static readonly DependencyProperty SizeTypeProperty =
            DependencyProperty.Register("SizeType", typeof(SizeType), typeof(NitaBorder), new PropertyMetadata(SizeType.Medium));
        #endregion
        #endregion
    }
}
                                                                                                                                                                                                                                    