using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Controller{
    public static class Data {
        public static Competition? Competition;
        public static Race ?currentRace;

        public static void Initialize() {
            Competition = new Competition();
            
            addTracks();
            addParticipants();
        }
        public static void addParticipants() {
            Driver driver1 = new Driver("TestDriver",TeamColor.Red);
            Competition.Participants.Add(driver1);
            Driver driver2 = new Driver("DestDriver2",TeamColor.Green);
            Competition.Participants.Add(driver2);
            //Competition.Participants.Add(driver1);
        }

        public static void nextRace() {
            Track next = Competition.NextTrack();
            if (next != null) {
                Race race1 = new Race(next, Competition.Participants);
                race1.RandomizeEquipment();
               currentRace = race1;
            } else {
                currentRace = null;
            }
        }
        

        public static void addTracks() {
            Track track1 = new Track("test 4", new SectionTypes[] { SectionTypes.RightCorner, SectionTypes.Finish, SectionTypes.RightCorner, SectionTypes.Finish, SectionTypes.RightCorner, SectionTypes.Finish, SectionTypes.RightCorner, SectionTypes.Finish }, 3);
            //Track track1 = new Track("test 4", new SectionTypes[] { SectionTypes.RightCorner, SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight },3);
            Competition.Tracks.Enqueue(track1);
        }
    }
}
