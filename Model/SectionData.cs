using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    internal class SectionData {
        public IParticipant Left { get; set; }
        public int DistanceRight { get; set; }
        public IParticipant Right { get; set; }
        public int DistanceLeft { get; set; }

        public SectionData(IParticipant left, int distanceLeft, IParticipant right, int distanceRight) {
            Left = left;
            Right = right;
            DistanceLeft = distanceLeft;
            DistanceRight = distanceRight;
        }
    }
}
