using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public class RaceContext : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        private String currentTrackName;
        public string publicTrackName {
            get => currentTrackName;
            set {
                currentTrackName = value;
                RaiseProperChanged();
            }
        }

        private void RaiseProperChanged() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));




        public void OnDriversChanged(object sender, DriversChangedEventArgs e) {
            publicTrackName = e.Track.Name;
            //currentTrackName = "Test";
            //RaiseProperChanged();
        }
    }
}
