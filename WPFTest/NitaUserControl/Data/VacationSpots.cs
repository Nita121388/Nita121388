using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTest.NitaUserControl.Data
{
    internal class VacationSpots : ObservableCollection<string>
    {
        public VacationSpots()
        {

            Add("Spain");
            Add("France");
            Add("Peru");
            Add("Mexico");
            Add("Italy");
        }
    }
}
