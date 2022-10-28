using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Controller {
    public class RaceStatsContext : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        public List<IParticipant>? EquipmentList { get; set; } = new List<IParticipant>();
        public List<IParticipant>? lapTimes { get; set; } = new List<IParticipant>();
        public double fastestLapTime { get; set; } = 0;



        public void OnNextLap(Object? sender, UpdateRaceStatsArgs e) {
            lapTimes = e.race.Participants.OrderBy(x => x.lapTime).Where(x => x.lapTime > 0).ToList<IParticipant>();
            double tempLapTime = lapTimes.Select(x => x.lapTime).Where(x => x > 0).Min();
            if (fastestLapTime == 0 || fastestLapTime > tempLapTime) {
                fastestLapTime = tempLapTime;
            }
                    

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        public void OnUpdatedStats(object? sender, NextRaceArgs e) {
            EquipmentList = Data.Competition.Participants;
            lapTimes = e.race.Participants.OrderBy(x => x.lapTime).Where(x => x.lapTime > 0).ToList<IParticipant>();
            fastestLapTime = 0;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}
