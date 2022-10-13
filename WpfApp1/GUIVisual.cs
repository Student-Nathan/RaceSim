using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Threading;

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
        private static int imageSize = 256/2-35;
        private const string folder = "C:\\Users\\School\\source\\repos\\RaceSim\\RaceSim\\WpfApp1";

        public static void initialize() {
        }


        public static BitmapSource drawTrack(Track track) {
            int[] bounds = calculateBounds(track);
            Bitmap plaatje = ImageHandler.getNewBitmap(bounds[0], bounds[1],5);
            //Bitmap plaatje = ImageHandler.getNewBitmap(900, 512);
            Graphics g = Graphics.FromImage(plaatje);


            switch (track.rotationINT) {
                case 0: rotation = Rotation.WestEast; break;
                case 1: rotation = Rotation.NorthSouth; break;
                case 2: rotation = Rotation.EastWest; break;
                case 3: rotation = Rotation.SouthNorth; break;
            }
            resetTrack();
            foreach (Section section in track.Sections) {
                g.DrawImage(getGraphics(section, rotation), new Point(posX, posY));


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
                switch (rotation) {
                    case Rotation.WestEast: posX += imageSize; break;
                    case Rotation.SouthNorth: posY -= imageSize; break;
                    case Rotation.EastWest: posX -= imageSize;break;
                    case Rotation.NorthSouth: posY+=imageSize; break;
                }
            }
            return ImageHandler.CreateBitmapSourceFromGdiBitmap(plaatje);
        }

        public static void resetTrack() {
            posX = 0;
            posY = 0;
        }

        public static void OnNextRace(object sender, NextRaceArgs e) {
            drawTrack(e.race.Track);
        }

        #region graphics
        //straight
        private const string straightHorizontal = folder+"\\Graphics\\TrackParts\\StraightHorizontal.png";
        private const string straightVertical = folder+"\\Graphics\\TrackParts\\StraightVertical.png";

        //turns
        private const string cornerSW = folder+"\\Graphics\\TrackParts\\CornerSW.png";
        private const string cornerSE = folder+"\\Graphics\\TrackParts\\CornerSE.png";
        private const string cornerNE = folder+"\\Graphics\\TrackParts\\CornerNE.png";
        private const string cornerNW = folder+"\\Graphics\\TrackParts\\CornerNW.png";

        //starts

        private const string startWE = folder + "\\Graphics\\TrackParts\\StartGridWE.png";
        private const string startNS = folder + "\\Graphics\\TrackParts\\StartGridNS.png";
        private const string startEW = folder + "\\Graphics\\TrackParts\\StartGridEW.png";
        private const string startSN = folder + "\\Graphics\\TrackParts\\StartGridSN.png";


        //finish lines
        private const string finishWE = folder + "\\Graphics\\TrackParts\\finishWE.png";
        private const string finishNS = folder + "\\Graphics\\TrackParts\\finishNS.png";
        private const string finishEW = folder + "\\Graphics\\TrackParts\\finishEW.png";
        private const string finishSN = folder + "\\Graphics\\TrackParts\\finishSN.png";

        #endregion

        public static Bitmap getGraphics(Section section, Rotation rotation) {
            switch (section.SectionType) {
                case SectionTypes.Empty: return ImageHandler.GetBitmap("empty");
                case SectionTypes.Straight:
                    switch (rotation) {
                        case Rotation.WestEast: return ImageHandler.GetBitmap(straightHorizontal);
                        case Rotation.NorthSouth: return ImageHandler.GetBitmap(straightVertical);
                        case Rotation.EastWest: return ImageHandler.GetBitmap(straightHorizontal); 
                        case Rotation.SouthNorth: return ImageHandler.GetBitmap(straightVertical); 
                    }
                    break;
                case (SectionTypes.LeftCorner):
                    switch (rotation) {
                        case Rotation.WestEast: return ImageHandler.GetBitmap(cornerSW); 
                        case Rotation.NorthSouth: return ImageHandler.GetBitmap(cornerSE);
                        case Rotation.EastWest: return ImageHandler.GetBitmap(cornerNE);
                        case Rotation.SouthNorth: return ImageHandler.GetBitmap(cornerNW);
                    }
                    break;
                case (SectionTypes.RightCorner):
                    switch (rotation) {
                        case Rotation.WestEast: return ImageHandler.GetBitmap(cornerNW);
                        case Rotation.NorthSouth: return ImageHandler.GetBitmap(cornerSW);
                        case Rotation.EastWest: return ImageHandler.GetBitmap(cornerSE);
                        case Rotation.SouthNorth: return ImageHandler.GetBitmap(cornerNE);
                    }
                    break;
                case (SectionTypes.StartGrid):
                    switch (rotation) {
                        case Rotation.WestEast: return ImageHandler.GetBitmap(startWE);
                        case Rotation.NorthSouth: return ImageHandler.GetBitmap(startNS);
                        case Rotation.EastWest: return ImageHandler.GetBitmap(startEW);
                        case Rotation.SouthNorth: return ImageHandler.GetBitmap(startSN);
                    }
                    break;
                case (SectionTypes.Finish):
                    switch (rotation) {
                        case Rotation.WestEast: return ImageHandler.GetBitmap(finishWE);
                        case Rotation.NorthSouth: return ImageHandler.GetBitmap(finishNS);
                        case Rotation.EastWest: return ImageHandler.GetBitmap(finishEW);
                        case Rotation.SouthNorth: return ImageHandler.GetBitmap(finishSN);
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

        private static int[] calculateBounds(Track track) {
            int rotationINT = track.rotationINT;
            int[] tempBounds = new int[4] { 1, 1, 1, 1 };
            int[] result = new int[2];
            foreach (Section section in track.Sections) {
                tempBounds[rotationINT] += 1;

                //copied from visual.cs
                switch (section.SectionType) {//switch to rotate every section properly and compensate for the empty sectiontype
                    case SectionTypes.LeftCorner:
                        switch (rotationINT) {
                            case 0: rotationINT = 1; break;
                            case 1: rotationINT = 2; break;
                            case 2: rotationINT = 3; break;
                            case 3: rotationINT = 0; break;
                        }
                        break;
                    case SectionTypes.RightCorner:
                        switch (rotationINT) {
                            case 0: rotationINT = 3; break;
                            case 1: rotationINT = 0; break;
                            case 2: rotationINT = 1; break;
                            case 3: rotationINT = 2; break;
                        }
                        break;
                }

            }

            if (tempBounds[0] > tempBounds[2]) {//value voor de breedte
                result[0] = tempBounds[0];
            } else {
                result[0] = tempBounds[2];
            }

            if (tempBounds[1] > tempBounds[3]) {//value voor de hoogte
                result[1] = tempBounds[1];
            } else {
                result[1] = tempBounds[3];
            }

            result[0] *= imageSize;//de images zijn 256 pixels hoog en breed
            result[1] *= imageSize;

            return result;
        }
    }
}
