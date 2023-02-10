using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public class Competition {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks {get; set;}
        public Dictionary<string, int> PointList { get; set; }

        public Competition() {
            Participants = new List<IParticipant> { };
            Tracks = new Queue<Track>();
            PointList = new Dictionary<string, int>();
        }
        public Track? NextTrack() {
            if (Tracks.Count > 0) {
                return Tracks.Dequeue();
            } else {
                return null;
            }
           
        }
    }
}
