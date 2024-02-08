using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection;
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
using System.Windows.Controls.Primitives;

namespace WPFTest.NitaCustomControl
{
    /// <summary>
    /// 定制列表框
    /// </summary>
    [TemplatePart(Name = "PART_IndicatorList",Type = typeof(ItemsControl))]
    #region 单词讲解
    /* Indicator是一个英语单词，它有多个含义和用法。以下是一些常见的解释：
    指示器：Indicator通常用来表示一个设备或系统上的指示灯、指针或其他可视化元素，用于显示某种状态或信息。例如，汽车仪表盘上的速度表、温度计等都是指示器。*/
    #endregion
    public class ListBoxWithIndicator : ContentControl
    {
        #region 构造函数
        static ListBoxWithIndicator()
        {
            #region 代码讲解
            // 这段代码是用来定义自定义控件的样式的
            // 自定义控件就是你可以自己设计外观和功能的控件，比如按钮、文本框等
            // 样式就是控件的外观，比如颜色、形状、大小等

            // 这段代码的意思是，这个自定义控件的样式的默认值是 ListBoxWithIndicator 这个类型的样式
            // ListBoxWithIndicator 是一个列表框，它可以显示一些项目，还有一个指示器，可以告诉你有多少项目
            // 这个类型的样式是在一个叫做 Generic.xaml 的文件里定义的

            // 这段代码要放在自定义控件的静态构造函数里
            // 静态构造函数是在你第一次使用这个控件的时候执行的，它可以设置一些初始的值

            // 这段代码的作用是，让你的自定义控件在没有指定其他样式的情况下，使用 ListBoxWithIndicator 的样式
            // 这样你就不用每次都写样式，可以节省时间和精力

            // 你可以把这段代码想象成一个标签，它告诉电脑，这个控件的样式是什么
            // 如果你想改变控件的样式，你可以换一个标签，或者自己写一个新的样式
            #endregion
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListBoxWithIndicator), new FrameworkPropertyMetadata(typeof(ListBoxWithIndicator)));
        }
        public ListBoxWithIndicator()
        {
            this.Loaded += new RoutedEventHandler(ListBoxWithSelectedItemIndicator_Loaded);
        }
        #endregion
        #region 字段
        private ItemsControl _indicatorList;
        private ObservableCollection<Double> _indicatorOffsets;
        private ListBox _listBox;
        #endregion
        #region 依赖属性
        public static readonly DependencyProperty
            IndicatorBrushProperty = DependencyProperty.Register(
              "IndicatorBrush", typeof(Brush),
              typeof(ListBoxWithIndicator), new PropertyMetadata(
              new LinearGradientBrush(Colors.LightBlue, Colors.Blue,
              new Point(0.5, 0), new Point(0.5, 1))));

        public static readonly DependencyProperty
              IndicatorHeightWidthProperty =
                  DependencyProperty.Register("IndicatorHeightWidth",
                  typeof(Double), typeof(ListBoxWithIndicator), new
                  PropertyMetadata(16.0));
        #endregion
        #region 属性
        [Description("用于绘制指示器的笔刷。默认为蓝色渐变笔刷"),Category("Custom")]
        public Brush IndicatorBrush
        {
            get
            {
                return GetValue(IndicatorBrushProperty) as Brush;
            }
            set
            {
                SetValue(IndicatorBrushProperty, value);
            }
        }
        [Description("指示器的大小。指示器呈现为一个方形，因此该值是指示器的高度和宽度。默认值为16。"), Category("Custom")]
        public Double IndicatorHeightWidth
        {
            get
            {
                return (Double)GetValue(IndicatorHeightWidthProperty);
            }
            set
            {
                SetValue(IndicatorHeightWidthProperty, value);
            }
        }
        #endregion
        #region 方法
        protected override void OnContentChanged(object oldContent,
                  object newContent)
        {
            // 这个方法是用来处理控件的内容变化的
            // 它会先调用父类的方法，然后判断新的内容是不是空的，或者是不是一个 ListBox
            // 如果是，它会把新的内容赋值给 _listBox，然后清空 _indicatorOffsets
            // 如果不是，它会抛出一个异常，告诉内容无效
            // 你可以把这段代码想象成一个保姆，它负责照顾这个自定义控件的内容。当内容变化的时候，它会检查内容是不是合适的，如果是，它会收拾一下，如果不是，它会生气，不让你换内容。
            base.OnContentChanged(oldContent, newContent);
            if (newContent == null || newContent is ListBox)
            {
                _listBox = newContent as ListBox;
                if (_indicatorOffsets != null &&
                    _indicatorOffsets.Count > 0)
                {
                    _indicatorOffsets.Clear();
                }
            }
            else
            {
                throw new NotSupportedException("内容无效。 istBoxWithSelectedItemIndicator只接受ListBox控件作为其内容。");
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _indicatorList = GetTemplateChild("PART_IndicatorList") as ItemsControl;
            if (_indicatorList == null)
            {
                throw new Exception("嘿！ PART_IndicatorList从模板中丢失，或者不是ItemsControl 。抱歉，但是需要此ItemsControl 。");
            }
        }

        private void ListBoxWithSelectedItemIndicator_Loaded(object sender,RoutedEventArgs e)
        {
            //  这个方法是在控件第一次显示的时候执行的
            //  它可以做一些需要控件已经加载的工作，比如设置数据源，更新指示器等
            //  这样可以提高性能和保证正确性
            if (_indicatorList != null)
            {
                this._indicatorOffsets = new ObservableCollection<double>();
                this._indicatorList.ItemsSource = this._indicatorOffsets;
                this.AddHandler(ListBox.SelectionChangedEvent, new
                    SelectionChangedEventHandler(ListBox_SelectionChanged));
                this.AddHandler(ScrollViewer.ScrollChangedEvent, new
              ScrollChangedEventHandler(ListBox_ScrollViewer_ScrollChanged));
                UpdateIndicators();
            }
        }
        private void UpdateIndicators()
        {
            if (this._indicatorOffsets == null && this._listBox == null) return;
            if (this._listBox.SelectedItems.Count == 0)
                return;
            if (this._indicatorOffsets.Count > 0)
                this._indicatorOffsets.Clear();

           /* 1.这段代码是用来检查一个列表框的项目是否已经生成好了。
                列表框就是一个可以显示多个项目的控件，比如你的手机上的联系人列表。
                项目就是列表框里面的每一个选项，比如你的每一个联系人。¹
            2. 这段代码的意思是，先创建一个icGen 的变量，它是一个 ItemContainerGenerator 的类型。        ItemContainerGenerator 是一个可以帮助列表框生成项目的工具，
                它可以把你的数据转换成可以显示的项目。² 
            3. 然后，用这个变量去获取列表框的 ItemContainerGenerator，也就是列表框的生成项目的工具。
            4. 判断这个生成项目的工具的状态是不是等于 ContainersGenerated。
                ContainersGenerated 是一个表示项目已经生成好了的值。
                ³ 如果不是，就说明项目还没有生成好，那么就返回，也就是不继续执行后面的代码。
                如果是，就说明项目已经生成好了，那么就继续执行后面的代码。
            5. 你可以把这段代码想象成一个检查员，它负责检查列表框的项目是否已经准备好了。如果没有，它就会说“还没好，等一下”，然后走开。如果有，它就会说“好了，可以用了”，然后继续工作。*/
            ItemContainerGenerator icGen = this._listBox.ItemContainerGenerator;
            if (icGen.Status != GeneratorStatus.ContainersGenerated)
                return;

            foreach (object selectedItem in this._listBox.SelectedItems)
            {
                ListBoxItem lbItem = icGen.ContainerFromItem (selectedItem) as ListBoxItem;
                if (lbItem != null)
                {
                    GeneralTransform gt = lbItem.TransformToAncestor(this._listBox);
                    Point pt = gt.Transform(new Point(0, 0));
                    Double dblOffset = pt.Y + lbItem.ActualHeight / 2
                                  - this.IndicatorHeightWidth / 2;
                    this._indicatorOffsets.Add(dblOffset);
                }
            }
        }
        private void ListBox_ScrollViewer_ScrollChanged(object sender,
                    ScrollChangedEventArgs e)
        {
            if (e.VerticalChange != 0)
            {
                UpdateIndicators();
            }
        }
        private void ListBox_SelectionChanged(object sender,SelectionChangedEventArgs e)
        {
            UpdateIndicators();
        }
        private void ListBoxWithSelectedItemIndicator_Unloaded(object sender, RoutedEventArgs e)
        {
            this.RemoveHandler(ListBox.SelectionChangedEvent, 
                new SelectionChangedEventHandler(ListBox_SelectionChanged));

            this.RemoveHandler(ScrollViewer.ScrollChangedEvent, 
                new ScrollChangedEventHandler(ListBox_ScrollViewer_ScrollChanged));
        }
        #endregion
    }
}