using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public class Competition {
        public List<IParticipant> Participants;
        public Queue<Track> Tracks;
        public Dictionary<string, int> pointList;

        public Competition() {
            Participants = new List<IParticipant> { };
            Tracks = new Queue<Track>();
            pointList = new Dictionary<string, int>();
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
