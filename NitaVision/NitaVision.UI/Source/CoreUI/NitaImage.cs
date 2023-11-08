using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NitaVision.UI.Source.CoreUI
{
    public class NitaImage : Image
    {
        // 定义 SourceChanged 事件
        public static readonly RoutedEvent SourceChangedEvent = EventManager.RegisterRoutedEvent(
            "SourceChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NitaImage));

        // 提供对 SourceChanged 事件的访问
        public event RoutedEventHandler SourceChanged
        {
            add { AddHandler(SourceChangedEvent, value); }
            remove { RemoveHandler(SourceChangedEvent, value); }
        }

        // 重写 Source 属性的元数据，以便在 Source 属性改变时引发 SourceChanged 事件
        static NitaImage()
        {
            SourceProperty.OverrideMetadata(typeof(NitaImage), new FrameworkPropertyMetadata(SourcePropertyChanged));
        }

        private static void SourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NitaImage image = (NitaImage)d;
            image.RaiseEvent(new RoutedEventArgs(SourceChangedEvent));
        }
    }
}
