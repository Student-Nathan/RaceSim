using Model;
using Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WPF {
    public class RaceStatsContext : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        public List<IParticipant>? EquipmentList { get; set; }
        public List<IParticipant>? lapTimes { get; set; }
        public double FastestLapTime { get; set; } = 0;



        public void OnNextLap(Object? sender, UpdateRaceStatsArgs e) {
            lapTimes = e.race.Participants.OrderByDescending(x =>x.Laps).ThenBy(x => x.LapTime).Where(x => x.LapTime > 0).ToList<IParticipant>();
            double tempLapTime = lapTimes.Select(x => x.LapTime).Where(x => x > 0).Min();
            if (FastestLapTime == 0 || FastestLapTime > tempLapTime) {
                FastestLapTime = tempLapTime;
            }
                    

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        public void OnUpdatedStats(object? sender, NextRaceArgs e) {
            EquipmentList = e.race.Participants.Take(e.race.CurrentCompetitorNumber).ToList<IParticipant>();
            lapTimes = e.race.Participants.OrderBy(x => x.LapTime).Where(x => x.LapTime > 0).ToList<IParticipant>();
            FastestLapTime = 0;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}
