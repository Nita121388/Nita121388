using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTest.ViewModel
{
    public class MyViewModel //: ObservableObject
    {
        public RelayCommand MyCommand { get; }

        private string message;
        public string Message
        {
            get => message;
            //set => SetProperty(ref message, value);
        }

        public MyViewModel()
        {
            MyCommand = new RelayCommand(DoSomething);
        }

        private void DoSomething()
        {
            int q = 0;
            //Message = "Hello, world!";
        }
    }
}
