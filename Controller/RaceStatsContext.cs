using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Controller {
    public class RaceStatsContext : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        private int i = 0;
        public List<IParticipant>? EquipmentList { get; set; }
        public List<IParticipant>? lapTimes { get; set; }
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
            EquipmentList = e.race.Participants.Take(e.race.competitors).ToList<IParticipant>();
            lapTimes = e.race.Participants.OrderBy(x => x.lapTime).Where(x => x.lapTime > 0).ToList<IParticipant>();
            fastestLapTime = 0;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}
