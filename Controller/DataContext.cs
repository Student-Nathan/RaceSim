using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller {
    public class DataContext : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        public String currentTrackName { get => Data.currentRace.Track.Name; } 

        public DataContext() {
            Data.currentRace.DriversChanged += OnDriversChanged;
        }

        private void OnDriversChanged(Object sender, DriversChangedEventArgs e) {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(""));
         }
    }
}
