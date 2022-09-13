using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public interface IEquipment {
        public int Quality { get; set; }
        public int Performance { get; set; }
        public int Speed { get; set; }
        public bool IsBroken { get; set; }
    }
}
