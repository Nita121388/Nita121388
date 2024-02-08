using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WPFTest.Enity;

namespace WPFTest.ViewModel
{
    public class NitaItemModels : ObservableCollection<NitaItemModel>
    { 
        
    }
    public class NitaItemModel
    {
        #region Fields

        private string _color = (String)Application.Current.FindResource("DefaultClickColorStr");
        private string _text = "默认状态";
        private ImageSource _icon = (ImageSource)Application.Current.FindResource("DownTriangle");
        public ContentMode _mode = ContentMode.ColorAndText;
        #endregion

        #region Property

        public string Text
        {
            get { return _text; }
            set
            {
                if (value != _text)
                {
                    _text = value;
                }
            }
        }
        public string Color
        {
            get { return _color; }
            set
            {
                if (value != _color)
                {
                    _color = value;
                }
            }
        }
        public ImageSource Icon
        {
            get { return _icon; }
            set
            {
                if (value != _icon)
                {
                    _icon = value;
                }
            }
        }
        public ContentMode Mode
        {
            get { return _mode; }
            set
            {
                if (value != _mode)
                {
                    _mode = value;
                }
            }
        }
        #endregion
    }
}
