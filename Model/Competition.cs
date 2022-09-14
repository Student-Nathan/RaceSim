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

        public Competition() {
            Participants = new List<IParticipant> { };
            Tracks = new Queue<Track>();
        }
        public Track NextTrack() {
            if (Tracks.Count > 0) {
                return Tracks.Dequeue();
            } else {
                return null;
            }
           
        }
    }
}
