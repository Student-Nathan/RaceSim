﻿using System;
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
        public static event EventHandler<NextRaceArgs> nextRaceEvent;
        private static Boolean GUI = false;

        public static void Initialize(Boolean gui) {
            GUI = true;
            Initialize();
        }

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
            Driver driver3 = new Driver("EestDriver3", TeamColor.Blue);
            Competition.Participants.Add(driver3);
            Driver driver4 = new Driver("test", TeamColor.Pink);
            Driver driver5 = new Driver("test", TeamColor.Yellow);
            Driver driver6 = new Driver("test", TeamColor.Orange);
            Competition.Participants.Add(driver4);
            Competition.Participants.Add(driver5);
            Competition.Participants.Add(driver6);

        }

        public static void nextRace() {
            if (!GUI) {
                Console.Clear();
            }
            currentRace?.Cleanup();
            Track next = Competition.NextTrack();
            if (next != null) {
                Race race1 = new Race(next, Competition.Participants);
                race1.RandomizeEquipment();
                currentRace = race1;
                nextRaceEvent.Invoke(null, new NextRaceArgs(currentRace));
            } else {
                currentRace = null;
            }
            
        }
        

        public static void addTracks() {
            //Track track1 = new Track("test 4", new SectionTypes[] { SectionTypes.RightCorner, SectionTypes.Finish, SectionTypes.RightCorner, SectionTypes.Finish, SectionTypes.RightCorner, SectionTypes.Finish, SectionTypes.RightCorner, SectionTypes.Finish }, 3);
            Track track1 = new Track("test 4", new SectionTypes[] { SectionTypes.RightCorner, SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Finish, SectionTypes.RightCorner, SectionTypes.Straight },3);
            //Competition.Tracks.Enqueue(track1);
            Track track2 = new Track("test 5", new SectionTypes[] {SectionTypes.RightCorner, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight,SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Finish, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight,SectionTypes.Straight },3);
            //Track track2 = new Track("test 5", new SectionTypes[] { SectionTypes.RightCorner, SectionTypes.StartGrid, SectionTypes.Straight , SectionTypes.Straight , SectionTypes.Straight , SectionTypes.Straight , SectionTypes.Straight , SectionTypes.Straight , SectionTypes.Straight , SectionTypes.Straight , SectionTypes.Straight , SectionTypes.Straight , SectionTypes.Straight , SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight }, 3);
            //Track track2 = new Track("test 5", new SectionTypes[] { SectionTypes.RightCorner, SectionTypes.StartGrid,SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Finish,SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight }, 3);
            Competition.Tracks.Enqueue(track2);

        }
    }
}
