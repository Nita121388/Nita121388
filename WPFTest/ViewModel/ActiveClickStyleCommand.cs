using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTest.ViewModel
{
    public class ActiveClickStyleCommand //: ObservableObject
    {
        public RelayCommand MyCommand { get; }

        private string message;
        public string Message
        {
            get => message;
            //set => SetProperty(ref message, value);
        }

        public ActiveClickStyleCommand()
        {
            MyCommand = new RelayCommand(DoSomething);
        }

        private void DoSomething()
        {
            int a = 100;
            //Message = "Hello, world!";
        }
    }
}
