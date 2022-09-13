using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Controller{
    public static class Data {
        public static Competition Competition;

        public static void Initialize(Competition competition) {
            Competition = competition;
        }
        public static void addParticipants() {
            Car car1 = new Car(3, 4, 5, false);
            Driver driver1 = new Driver("test1", 0,car1,TeamColor.Red);
            Competition.Participants.Add(driver1);
            Car car2 = new Car(4, 3, 5, false);
            Driver driver2 = new Driver("test1", 0, car1, TeamColor.Red);
            Competition.Participants.Add(driver1);
        }

        public static void addTracks() {
            SectionTypes[] sections = new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.RightCorner };
            Competition.Tracks.Enqueue(new Track("Test", sections));
        }
    }
}
