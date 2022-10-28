using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public interface IParticipant {
        public string Name { get; }
        public int Points { get; set; }
        public IEquipment Equipment { get; }
        public TeamColor TeamColor { get; }
        public double lapTime { get; set; }
        public DateTime previousTime { get; set; }


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
