using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public class Driver : IParticipant {
        public string Name { get; set; }

        public int Points { get; set; }

        public IEquipment Equipment { get; set; }

        public TeamColor TeamColor { get; set; }
        public int Laps { get; set; }
        public double LapTime { get; set; }
        public DateTime PreviousTime { get; set; }

        public Driver(String name, TeamColor teamColor) {
            Name = name;
            Points = 0;
            LapTime = 0;
            Equipment = new Car();
            TeamColor = teamColor;
        }
    }
}
