using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Controller;

namespace RaceSim {
    public static class visual {
        private static Rotation rotation;
        public static void initialize() {
            
        }

        #region graphics
        //todo: add own graphics
        private static string[] _finishHorizontal = { "----", "  1# ", "  2# ", "----" };
        private static string[] _finishVertical = { };

        private static string[] _straightHorizontal = {"----"," 1 "," 2 ","----" };
        private static string[] _straightVertical = {" |  |"," 1 "," 2 ","|  |" };

        
        private static string[] _cornerNe = { @" /--", @"/1  ", @"| 2 ", @"|  /" };//boven links
        private static string[] _cornerNw = { @"--\ ", @"  1\", @" 2 |", @"\  |" };//boven rechts
        private static string[] _cornerSe = { @"|  \", @"| 1 ", @"\2  ", @" \--" };//onder rechts
        private static string[] _cornerSw = { @"/  |", @" 1 |", @"  2/", @"--/ " };//onder links

        private static string[] _startGridHorizontal = { "----", " 1] ", "2]  ", "----" };
        private static string[] _startGridVertical = { };

        #endregion

        public enum Rotation {
            EastWest, //links naar rechts
            NorthSouth, //boven naar beneden
            WestEast, //rechts naar links
            SouthNorth, //beneden naar boven

        }

        public static void drawTrack(Track track) {
            resetTrack();
            foreach (Section section in track.Sections) {
                foreach(String sectionPart in getGraphics(section, rotation)) {
                    Console.WriteLine(sectionPart);
                }
                switch (section.SectionType) {
                    case SectionTypes.LeftCorner:
                        switch (rotation) {
                            case Rotation.EastWest: rotation = Rotation.SouthNorth; break;
                            case Rotation.SouthNorth: rotation = Rotation.WestEast; break;
                            case Rotation.WestEast: rotation = Rotation.SouthNorth; break;
                            case Rotation.NorthSouth: rotation = Rotation.EastWest; break;
                        }
                        break;
                    case SectionTypes.RightCorner:
                        switch (rotation) {//nog goed doen
                            case Rotation.EastWest: rotation = Rotation.SouthNorth; break;
                            case Rotation.SouthNorth: rotation = Rotation.WestEast; break;
                            case Rotation.WestEast: rotation = Rotation.SouthNorth; break;
                            case Rotation.NorthSouth: rotation = Rotation.EastWest; break;
                        }
                        break;
                }
            }
        }

        public static void resetTrack() {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            rotation = Rotation.EastWest;
        }

//delete this, not used
        //private static String[] rotateGrapic(String[]graphic, int times) {//draait een graphic met de klok mee
        //    String[] result = new string[graphic.Length];
        //    for(int i = 1; i<times; i++) {//rotates it multiple times if neccesary 
        //        for(int j = 0; j < graphic.Length; j++) {//itterates the indexes for the original grapic
        //            for( int k = 0; k < graphic[j].Length; k++) {//itterates the placement of the characters
        //                result[k]+=graphic[j][k];
        //            }
        //        }
        //        if (times > 1) {
        //            Array.Copy(result, graphic, result.Length);
        //            Array.Clear(result);
        //        }
        //    }
        //    foreach (String s in result) {
        //        Console.WriteLine(s);
        //    }
        //    return result;
        //}

        public static String[] getGraphics(Section section, Rotation rotation) {
            switch (section.SectionType) {
                case SectionTypes.Straight:
                    switch (rotation) {
                        case Rotation.EastWest: return _straightHorizontal;
                        case Rotation.NorthSouth: return _straightVertical;
                        case Rotation.WestEast: return _straightHorizontal;
                        case Rotation.SouthNorth: return _straightVertical;
                    }
                    break;
                case (SectionTypes.LeftCorner):
                    switch (rotation) {
                        case Rotation.EastWest: return _cornerSw;
                        case Rotation.NorthSouth: return _cornerSe;
                        case Rotation.WestEast: return _cornerNe;
                        case Rotation.SouthNorth: return _straightHorizontal;
                    }
                    break;
                case (SectionTypes.RightCorner):
                    switch (rotation) {
                        case Rotation.EastWest: return _cornerNw;
                        case Rotation.NorthSouth: return _cornerSw;
                        case Rotation.WestEast: return _cornerSe;
                        case Rotation.SouthNorth: return _cornerNe;
                    }
                    break;
                case (SectionTypes.StartGrid):
                    switch (rotation) {
                        case Rotation.EastWest: return _startGridHorizontal;
                        case Rotation.NorthSouth: return _startGridVertical;
                        case Rotation.WestEast: return _startGridHorizontal;
                        case Rotation.SouthNorth: return _startGridVertical;
                    }
                    break;
                case (SectionTypes.Finish):
                    switch (rotation) {
                        case Rotation.EastWest: return _finishHorizontal;
                        case Rotation.NorthSouth: return _finishVertical;
                        case Rotation.WestEast: return _finishHorizontal;
                        case Rotation.SouthNorth: return _finishVertical;
                    }
                    break;
            }
            throw new Exception("Invalid gridType");
        }

        
    }
    //plan van aanpak:
    //1. foreach met alle sectiontypes van track
    //2. voor elke sectiontype wordt de tekening in nieuw array gegooit
    //3. foreach voor tekenen. Denk om rotatie (Console.SetCursorPosition(x,y))

}
