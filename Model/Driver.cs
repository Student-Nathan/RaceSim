﻿using System;
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

        public Driver() {
            Equipment = new Car();
            TeamColor = new TeamColor();
        }
    }
}
