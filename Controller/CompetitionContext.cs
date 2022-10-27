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
        public String? trackName1 { get; set; }
        public String? trackName2 { get; set; } 
        public String? trackName3 { get; set; }
        public String? trackName4 { get; set; }
        public String? trackName5 { get; set; }
        public List<Track>? trackNameList { get; set; } = new List<Track>();
        public List<IParticipant> leaderBoard { get; set; } = new List<IParticipant>();

        private void RaiseProperChanged() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));


        public void OnNextRace(object sender, NextRaceArgs e) {
            UpdateTracks();
            UpdateLeaderboard();
            RaiseProperChanged();
        }

        public void UpdateTracks() {
            //Track[] tempArray = Data.Competition.Tracks.ToArray();
            //for (int i = 0; i < tempArray.Length && i < 5; i++) {
            //    trackNameList[i] = tempArray[i].Name;
            //}

            //for (int i = tempArray.Length; i < 5; i++) {
            //    trackNameList[i] = "";
            //}
            //trackName1 = trackNameList[0];
            //trackName2 = trackNameList[1];
            //trackName3 = trackNameList[2];
            //trackName4 = trackNameList[3];
            //trackName5 = trackNameList[4];
            trackNameList = Data.Competition.Tracks.Take(5).ToArray().ToList<Track>();
            
        }

        public void UpdateLeaderboard() {
            leaderBoard = Data.Competition.Participants.OrderByDescending(driver => driver.Points).Take(5).ToArray().ToList();
            
        }
    }
}
