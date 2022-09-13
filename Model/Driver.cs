using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public class Driver : IParticipant {
        public string Name { get; set; }

        public int Points { get; set; }

        public IEquipment equipment { get; set; }

        public TeamColor TeamColor { get; set; }

        public Driver(String name, int points, IEquipment equipment, TeamColor teamColor) {
            Name = name;
            Points = points;
            this.equipment = equipment;
            TeamColor = teamColor;
        }
    }
}
