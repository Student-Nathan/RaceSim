using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller {
    internal class RaceStatsContext : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        public String number1 = "testString top 1";
        public String number2 = "testString top 2";
        public String number3 = "testString top 3";
        public String number4 = "testString top 4";
        public String number5 = "testString top 5";


        public void OnNextLap(Object sender, UpdateRaceStatsArgs e) {
            List<String>top5 = new List<String>();
            //top5= e.competition.

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

    }
}
