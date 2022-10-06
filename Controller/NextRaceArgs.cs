using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller {
    public class NextRaceArgs:EventArgs {
        public Race race { get; set; }

        public NextRaceArgs(Race race) {
            this.race = race;
        }
    }
    
}
