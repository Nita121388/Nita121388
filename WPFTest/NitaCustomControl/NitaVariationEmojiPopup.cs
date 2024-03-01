using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFTest.NitaCustomControl
{
    public class NitaVariationEmojiPopup : ContentControl
    {
        static NitaVariationEmojiPopup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaVariationEmojiPopup),
                new FrameworkPropertyMetadata(typeof(NitaVariationEmojiPopup)));
        }
    }
}
