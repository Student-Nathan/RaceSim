using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public interface IParticipant {
        public string Name { get; }
        public int Points { get; set; }
        public IEquipment equipment { get; }
        public TeamColor TeamColor { get; }

    }

    public enum TeamColor {
        Red,
        Green,
        Yellow,
        Grey,
        Blue
    }
}
