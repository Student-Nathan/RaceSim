using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Controller{
    public static class Data {
        public static Competition Competition;
        public static Race currentRace;

        public static void Initialize() {
            Competition = new Competition();
        }
        public static void addParticipants() {
            Driver driver1 = new Driver();
            Competition.Participants.Add(driver1);
            Driver driver2 = new Driver();
            Competition.Participants.Add(driver2);
        }

        public static void nextRace() {
            Track next = Competition.NextTrack();
            if (next != null) {
               currentRace = new Race(next, Competition.Participants);
            } else {
                currentRace = null;
            }
        }
        

        public static void addTracks() {
            SectionTypes[] sections = new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.RightCorner };
            Competition.Tracks.Enqueue(new Track("Test", sections));
        }
    }
}
