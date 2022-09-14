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
            SectionTypes[] sectionTypes = new SectionTypes[] {SectionTypes.StartGrid,SectionTypes.LeftCorner};
            Track track1 = new Track("testTrack", sectionTypes);
            sectionTypes = new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.LeftCorner };
            Track track2 = new Track ("test2",sectionTypes);
            Tracks.Enqueue(null);
            Tracks.Enqueue(track1);
            Tracks.Enqueue(track2);
        }
        public Track NextTrack() { 
            if (Tracks.Count == 1) {
                Tracks.Dequeue();
                return null;
            }else if(Tracks.Count >= 2) {
                Tracks.Dequeue();
                return Tracks.Peek();
            }
            return null;
        }
    }
}
