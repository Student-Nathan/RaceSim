using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Controller {
    public class CompetitionContext : INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;
        //private String _trackName1;
        //public String trackName1 {
        //    get => _trackName1;
        //    set {
        //        _trackName1 = value;
        //        RaiseProperChanged();
        //    }
        //}
        public String trackName1 { get; set; } = "test1";
        public String trackName2 { get; set; } = "test2";
        public String trackName3 { get; set; } = "test3";
        public String trackName4 { get; set; } = "test4";
        public String trackName5 { get; set; } = "test5";
        private String[] trackNameList = new string[5];

        private void RaiseProperChanged() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));


        public void OnNextRace(object sender, NextRaceArgs e) {
            
            Track[] tempArray = Data.Competition.Tracks.ToArray();
            for (int i = 0; i < tempArray.Length && i < 5; i++) {
                trackNameList[i] = tempArray[i].Name;
            }

            for(int i = tempArray.Length; i < 5; i++) {
                trackNameList[i] = "";
            }

            trackName1 = trackNameList[0];
            trackName2 = trackNameList[1];
            trackName3 = trackNameList[2];
            trackName4 = trackNameList[3];
            trackName5 = trackNameList[4];
            //Array.Clear(trackNameList);
            RaiseProperChanged();
        }
    }
}
