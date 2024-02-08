using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFTest.NitaCustomControl
{
    public class NitaButtonGroup : ContentControl
    {
        static NitaButtonGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaButtonGroup),
                new FrameworkPropertyMetadata(typeof(NitaButtonGroup)));
        }
    }
}
