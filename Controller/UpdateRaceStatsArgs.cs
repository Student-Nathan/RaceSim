using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller {
    public class UpdateRaceStatsArgs:EventArgs {
        public Race race;

        public UpdateRaceStatsArgs(Race race) {
            this.race = race;
        }
    }
}
