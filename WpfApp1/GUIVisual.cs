using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing

namespace WPF {
    public enum Rotation {
        WestEast, //links naar rechts
        NorthSouth, //boven naar beneden
        EastWest, //rechts naar links
        SouthNorth, //beneden naar boven

    }
    public static class GUIVisual {

        private static Rotation rotation;
        private static int posX;
        private static int posY;
        private static int graphicLength = 4;
        public static void initialize() {
            Data.currentRace.DriversChanged += OnDriversChanged;
        }

        public static void OnNextRaceEvent(object sender, NextRaceArgs e) {
            Console.WriteLine("test");
            initialize();
            drawTrack(e.race.Track);
        }


        public static BitmapSource drawTrack(Track track) {

            switch (track.rotationINT) {
                case 0: rotation = Rotation.WestEast; break;
                case 1: rotation = Rotation.NorthSouth; break;
                case 2: rotation = Rotation.EastWest; break;
                case 3: rotation = Rotation.SouthNorth; break;
            }
            resetTrack();
            foreach (Section section in track.Sections) {
                
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
                }
            }
            return ImageHandler.CreateBitmapSourceFromGdiBitmap(getGraphics(new Section(SectionTypes.Empty), rotation));
        }

        public static void resetTrack() {
            Console.Clear();
            posX = 0;
            posY = 1;
        }

        public static Bitmap getGraphics(Section section, Rotation rotation) {
            switch (section.SectionType) {
                case SectionTypes.Empty: return ImageHandler.GetBitmap("empty");
                case SectionTypes.Straight:
                    switch (rotation) {
                        case Rotation.WestEast: return ImageHandler.GetBitmap("..\\Graphics\\StraightHorizontal.png");
                        case Rotation.NorthSouth: return ImageHandler.GetBitmap("..\\Graphics\\StraightVertical.png");
                        case Rotation.EastWest: return ImageHandler.GetBitmap("..\\Graphics\\StraightHorizontal.png"); 
                        case Rotation.SouthNorth: return ImageHandler.GetBitmap("..\\Graphics\\StraightVertical.png"); 
                    }
                    break;
                case (SectionTypes.LeftCorner):
                    switch (rotation) {
                        case Rotation.WestEast: return ImageHandler.GetBitmap("..\\Graphics\\CornerSW.png"); 
                        case Rotation.NorthSouth: return ImageHandler.GetBitmap("..\\Graphics\\CornerSE.png");
                        case Rotation.EastWest: return ImageHandler.GetBitmap("..\\Graphics\\CornerNE.png");
                        case Rotation.SouthNorth: return ImageHandler.GetBitmap("..\\Graphics\\CornerNW.png");
                    }
                    break;
                case (SectionTypes.RightCorner):
                    switch (rotation) {
                        case Rotation.WestEast: return ImageHandler.GetBitmap("..\\Graphics\\CornerNW.png");
                        case Rotation.NorthSouth: return ImageHandler.GetBitmap("..\\Graphics\\CornerSW.png");
                        case Rotation.EastWest: return ImageHandler.GetBitmap("..\\Graphics\\CornerSE.png");
                        case Rotation.SouthNorth: return ImageHandler.GetBitmap("..\\Graphics\\CornerNE.png");
                    }
                    break;
                case (SectionTypes.StartGrid):
                    switch (rotation) {
                        case Rotation.WestEast: return ImageHandler.GetBitmap("..\\Graphics\\StartGridWE.png");
                        case Rotation.NorthSouth: return ImageHandler.GetBitmap("..\\Graphics\\StartGridNS.png");
                        case Rotation.EastWest: return ImageHandler.GetBitmap("..\\Graphics\\StartGridEW.png");
                        case Rotation.SouthNorth: return ImageHandler.GetBitmap("..\\Graphics\\StartGridSN.png");
                    }
                    break;
                case (SectionTypes.Finish):
                    switch (rotation) {
                        case Rotation.WestEast: return ImageHandler.GetBitmap("..\\Graphics\\FinishWE.png");
                        case Rotation.NorthSouth: return ImageHandler.GetBitmap("..\\Graphics\\FinishNS.png");
                        case Rotation.EastWest: return ImageHandler.GetBitmap("..\\Graphics\\FinishEW.png");
                        case Rotation.SouthNorth: return ImageHandler.GetBitmap("..\\Graphics\\FinishSN.png");
                    }
                    break;
            }
            throw new Exception("No graphic found");
            

        }
        //still old copied code from visual in racesim
        public static String replacePlaceholders(String sectionPart, IParticipant left, IParticipant right) {
            if (left != null) {
                if (left.Equipment.IsBroken) {
                    sectionPart = sectionPart.Replace("1", "X");
                } else {
                    sectionPart = sectionPart.Replace("1", left.Name[0].ToString());
                }
            } else {
                sectionPart = sectionPart.Replace("1", " ");
            }
            if (right != null) {
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

        private static void OnDriversChanged(object sender, DriversChangedEventArgs e) {
            drawTrack(e.Track);
        }
    }
}
