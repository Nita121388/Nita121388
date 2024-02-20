using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WPFTest.Enity;

namespace WPFTest.ViewModel
{
    public class NitaDataModes : ObservableCollection<NitaDataMode>
    {

    }
    public class NitaDataMode
    {
        #region Fields
        private NitaDataType _nitaDataType;
        private object _nitaData;
        private string _header;
        #endregion

        #region Property

        public NitaDataType NitaDataType
        {
            get { return _nitaDataType; }
            set
            {
                if (value != _nitaDataType)
                {
                    _nitaDataType = value;
                }
            }
        }

        public object NitaData
        {
            get { return _nitaData; }
            set
            {
                if (value != _nitaData)
                {
                    _nitaData = value;
                }
            }
        }

        public string Header
        {
            get { return _header; }
            set
            {
                if (value != _header)
                {
                    _header = value;
                }
            }
        }



        #endregion
    }
}
