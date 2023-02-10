using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public interface IParticipant {
        public string Name { get; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColor TeamColor { get; }
        public int Laps { get; set; }

        public double LapTime { get; set; }
        public DateTime PreviousTime { get; set; }


    }

    public enum TeamColor {
        Red,
        Green,
        Yellow,
        Orange,
        Pink,
        Blue
    }
}
