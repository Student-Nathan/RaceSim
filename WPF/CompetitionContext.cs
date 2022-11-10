using Model;
using Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace WPF {
    public class CompetitionContext : INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;
        public List<Track>? TrackNameList { get; set; } = new List<Track>();
        public List<IParticipant> LeaderBoard { get; set; } = new List<IParticipant>();

        private void RaiseProperChanged() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));


        public void OnNextRace(object? sender, NextRaceArgs e) {
            UpdateTracks();
            UpdateLeaderboard();
            RaiseProperChanged();
        }

        public void UpdateTracks() {
            if (Data.Competition is not null) {
                TrackNameList = Data.Competition.Tracks.Take(5).ToArray().ToList<Track>();
            }   
        }

        public void UpdateLeaderboard() {
            if (Data.Competition is not null) {
                LeaderBoard = Data.Competition.Participants.OrderByDescending(driver => driver.Points).Take(5).ToArray().ToList();
            }
        }
    }
}
