﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WPFTest.ViewModel;

namespace WPFTest.NitaUserControl.NitaControlSamplesControl
{
    /// <summary>
    /// NitaComboBoxSamples.xaml 的交互逻辑
    /// </summary>
    public partial class NitaComboBoxSamples : UserControl, INotifyPropertyChanged
    {
        public NitaComboBoxSamples()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private NitaItemModels _nitaItemModels = new NitaItemModels();
        public NitaItemModels NitaItemModels
        {
            get
            {
                return _nitaItemModels;
            }
            set
            {
                _nitaItemModels = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NitaItemModels)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
