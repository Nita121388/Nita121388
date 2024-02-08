using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFTest.NitaCustomControl
{
    [TemplatePart(Name = "PART_NitaClickCanvas", Type = typeof(NitaClickCanvas))]
    public class TestTabControl : TabControl
    {
        #region 依赖属性

        #region EnumTabControlType
        public EnumTabControlType Type
        {
            get { return (EnumTabControlType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(EnumTabControlType), typeof(TestTabControl), new PropertyMetadata(EnumTabControlType.Card));
        #endregion

        #region HeaderContent
        public object HeaderContent
        {
            get { return (object)GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }
        public static readonly DependencyProperty HeaderContentProperty =
            DependencyProperty.Register("HeaderContent", typeof(object), typeof(TestTabControl));
        #endregion

        #endregion

        #region Constructors
        static TestTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TestTabControl), new FrameworkPropertyMetadata(typeof(TestTabControl)));
        }
        #endregion

        #region Override方法
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TabItem();
        }
        #endregion

        #region Private方法

        #endregion

    }
    public enum EnumTabControlType
    {
        Line,
        Card,
    }

}
