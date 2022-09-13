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
        public Competition(List<IParticipant> participants, Queue<Track> tracks) {
            Participants = participants;
            Tracks = tracks;
        }
        public Track NextTrack() {
            Tracks.Dequeue();
            return Tracks.Peek();
        }

    }
}
