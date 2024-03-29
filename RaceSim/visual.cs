﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Controller;

namespace RaceSim {
    public enum Rotation {
        WestEast, //links naar rechts
        NorthSouth, //boven naar beneden
        EastWest, //rechts naar links
        SouthNorth, //beneden naar boven

    }
    public static class Visual {
        private static Rotation rotation;
        private static int posX;
        private static int posY;
        private static readonly int graphicLength=4;
        public static void Initialize() {
            if (Data.CurrentRace is not null) {
                Data.CurrentRace.DriversChanged += OnDriversChanged;
            } else {
                throw new Exception("Error: There is no current race");
            }
        }

        public static void OnNextRaceEvent(object? sender, NextRaceArgs e) {
            Initialize();
            DrawTrack(e.race.Track);
        }

        #region graphics
        //let op: als graphics aangepast worden, verander dan ook graphicLength
        private static readonly string[] _finishWE = { "----", "|1# ", "|2# ", "----" };
        private static readonly string[] _finishEW = { "----", " #1|", " #2|", "----" };
        private static readonly string[] _finishSN = {"|--|","|21|","|##|","|  |" };
        private static readonly string[] _finishNS = {"|--|","|##|","|12|","|  |" };


        private static readonly string[] _straightHorizontal = {"----","  1 ","  2 ","----" };
        private static readonly string[] _straightVertical = {"|  |","|12|","|  |","|  |" };

        
        private static readonly string[] _cornerNe = { @" /--", @"/1  ", @"| 2 ", @"|  /" };//boven links
        private static readonly string[] _cornerNw = { @"--\", @" 1 \", @"2  |", @"\  |" };//boven rechts
        private static readonly string[] _cornerSe = { @"|  \", @"| 1 ", @"\2  ", @" \--" };//onder rechts
        private static readonly string[] _cornerSw = { @"/  |", @" 1 |", @"  2/", @"--/ " };//onder links

        private static readonly string[] _startGridWE = { "----", " 1] ", " 2]  ", "----" };
        private static readonly string[] _startGridEW = { "----", " [1 ", " [2  ", "----" };
        private static readonly string[] _startGridNS = { "|  |","|21|", "|__|","|  |" };
        private static readonly string[] _startGridSN = { "|  |", "|‾‾|", "|12|", "|  |" };


        private static readonly string[] _empty = {"    ", "    ", "    ", "    "};


        #endregion



 

        private static void DrawTrack(Track track) {
            
            switch (track.RotationINT) {
                case 0: rotation = Rotation.WestEast; break;
                case 1: rotation = Rotation.NorthSouth; break;
                case 2: rotation = Rotation.EastWest; break;
                case 3: rotation = Rotation.SouthNorth; break;
            }
            ResetTrack();
            Console.WriteLine(track.Name);
            foreach (Section section in track.Sections) {
                foreach (String sectionPart in GetGraphics(section, rotation)) {//draws every part of a sectionType
                    Console.SetCursorPosition(posX, posY);
                    posY += 1;
                    Console.WriteLine(ReplacePlaceholders(sectionPart,Data.CurrentRace.GetSectionData(section).Left, Data.CurrentRace.GetSectionData(section).Right));
                }
                posY -= graphicLength;
                switch (section.SectionType) {//switch to rotate every section properly and compensate for the empty sectiontype
                    case SectionTypes.LeftCorner:
                        switch (rotation) {
                            case Rotation.WestEast: rotation = Rotation.SouthNorth; break;
                            case Rotation.SouthNorth: rotation = Rotation.EastWest; break;
                            case Rotation.EastWest: rotation = Rotation.NorthSouth; break;
                            case Rotation.NorthSouth: rotation = Rotation.WestEast; break;
                        }
                        break;
                    case SectionTypes.RightCorner:
                        switch (rotation) {
                            case Rotation.WestEast: rotation = Rotation.NorthSouth; break;
                            case Rotation.SouthNorth: rotation = Rotation.WestEast; break;
                            case Rotation.EastWest: rotation = Rotation.SouthNorth; break;
                            case Rotation.NorthSouth: rotation = Rotation.EastWest; break;
                        }
                        break;
                    case SectionTypes.Empty: posX += graphicLength; break;

                }
                switch (rotation) {
                    case Rotation.WestEast: posX += graphicLength; break;
                    case Rotation.NorthSouth: posY += graphicLength; break;
                    case Rotation.EastWest: posX -= graphicLength; break;
                    case Rotation.SouthNorth: posY -= graphicLength; break;
                }            
            }
        }

        private static void ResetTrack() {
            Console.Clear();
            posX = 0;
            posY = 1;
        }

        public static String[] GetGraphics(Section section, Rotation rotation) {
            switch (section.SectionType) {
                case SectionTypes.Empty: return _empty;
                case SectionTypes.Straight:
                    switch (rotation) {
                        case Rotation.WestEast: return _straightHorizontal;
                        case Rotation.NorthSouth: return _straightVertical;
                        case Rotation.EastWest: return _straightHorizontal;
                        case Rotation.SouthNorth: return _straightVertical;
                    }
                    break;
                case (SectionTypes.LeftCorner):
                    switch (rotation) {
                        case Rotation.WestEast: return _cornerSw;
                        case Rotation.NorthSouth: return _cornerSe;
                        case Rotation.EastWest: return _cornerNe;
                        case Rotation.SouthNorth: return _cornerNw;
                    }
                    break;
                case (SectionTypes.RightCorner):
                    switch (rotation) {
                        case Rotation.WestEast: return _cornerNw;
                        case Rotation.NorthSouth: return _cornerSw;
                        case Rotation.EastWest: return _cornerSe;
                        case Rotation.SouthNorth: return _cornerNe;
                    }
                    break;
                case (SectionTypes.StartGrid):
                    switch (rotation) {
                        case Rotation.WestEast: return _startGridWE;
                        case Rotation.NorthSouth: return _startGridNS;
                        case Rotation.EastWest: return _startGridEW;
                        case Rotation.SouthNorth: return _startGridSN;
                    }
                    break;
                case (SectionTypes.Finish):
                    switch (rotation) {
                        case Rotation.WestEast: return _finishWE;
                        case Rotation.NorthSouth: return _finishNS;
                        case Rotation.EastWest: return _finishEW;
                        case Rotation.SouthNorth: return _finishSN;
                    }
                    break;
            }
            throw new Exception("No graphic found");
        }

        public static String ReplacePlaceholders(String sectionPart, IParticipant? left, IParticipant? right) {
            if (left is not null) {
                if (left.Equipment.IsBroken) {
                    sectionPart = sectionPart.Replace("1", "X");
                } else {
                    sectionPart = sectionPart.Replace("1", left.Name[0].ToString());
                }
            } else {
                sectionPart = sectionPart.Replace("1", " ");
            }
            if (right is not null) {
                if (right.Equipment.IsBroken) {
                    sectionPart = sectionPart.Replace("2", "X");
                } else {
                    sectionPart = sectionPart.Replace("2", right.Name[0].ToString());
                }
            } else {
                sectionPart = sectionPart.Replace("2", " ");
            }
            return sectionPart;
        }

        private static void OnDriversChanged(object? sender, DriversChangedEventArgs e) {
            DrawTrack(e.Track);
        }
    }
    //plan van aanpak:
    //1. foreach met alle sectiontypes van track
    //2. voor elke sectiontype wordt de tekening in nieuw array gegooit
    //3. foreach voor tekenen. Denk om rotatie (Console.SetCursorPosition(x,y))

}
