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
using System.Windows.Controls.Primitives;
using Track = Model.Track;

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
        private const int imageSize = 256/2-25;//calculation to compensate for the white spaces in the png's
        private const string folder = "C:\\Users\\School\\source\\repos\\RaceSim\\RaceSim\\WPF";
        private static Dictionary<Section, int[]> sectionCoords = new Dictionary<Section, int[]>();

        public static void Initialize() {
        }


        public static BitmapSource DrawTrack(Track track) {
            Bitmap plaatje = ImageHandler.GetBitmap("Empty");
            Graphics g = Graphics.FromImage(plaatje);


            switch (track.rotationINT) {
                case 0: rotation = Rotation.WestEast; break;
                case 1: rotation = Rotation.NorthSouth; break;
                case 2: rotation = Rotation.EastWest; break;
                case 3: rotation = Rotation.SouthNorth; break;
            }
            ResetTrack();
            foreach (Section section in track.Sections) {
                if (!sectionCoords.ContainsKey(section)) {
                    sectionCoords[section] = new int[2] {posX,posY};
                }
                SectionData sectionData;
                if (Data.CurrentRace is not null) {
                    sectionData = Data.CurrentRace.getSectionData(section);
                } else {
                    throw new NullReferenceException("Error: currentRace is null");
                }
                g.DrawImage(GetGraphics(section, rotation), new Point(posX, posY));
                DrawParticipants(plaatje, section, sectionData.Left, sectionData.Right, rotation);
                

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

        public static void ResetTrack() {
            posX = 0;
            posY = 0;
        }

        public static void OnNextRace(object? sender, NextRaceArgs e) {
            ImageHandler.clearCache();
            int[] bounds = CalculateBounds(e.race.Track);
            ImageHandler.getNewBitmap(bounds[0], bounds[1]);
            DrawTrack(e.race.Track);
           
            //drawingReady.Invoke(null, new NextRaceArgs(e.race));
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

        //drivers

        //Blue
        private const string blueN = folder + "\\Graphics\\Cars\\N\\TeamBlue.png";
        private const string blueE = folder + "\\Graphics\\Cars\\E\\TeamBlue.png";
        private const string blueS = folder + "\\Graphics\\Cars\\S\\TeamBlue.png";
        private const string blueW = folder + "\\Graphics\\Cars\\W\\TeamBlue.png";

        //Green
        private const string greenN = folder + "\\Graphics\\Cars\\N\\TeamGreen.png";
        private const string greenE = folder + "\\Graphics\\Cars\\E\\TeamGreen.png";
        private const string greenS = folder + "\\Graphics\\Cars\\S\\TeamGreen.png";
        private const string greenW = folder + "\\Graphics\\Cars\\W\\TeamGreen.png";

        //Orange
        private const string orangeN = folder + "\\Graphics\\Cars\\N\\TeamOrange.png";
        private const string orangeE = folder + "\\Graphics\\Cars\\E\\TeamOrange.png";
        private const string orangeS = folder + "\\Graphics\\Cars\\S\\TeamOrange.png";
        private const string orangeW = folder + "\\Graphics\\Cars\\W\\TeamOrange.png";

        //Pink
        private const string pinkN = folder + "\\Graphics\\Cars\\N\\TeamPink.png";
        private const string pinkE = folder + "\\Graphics\\Cars\\E\\TeamPink.png";
        private const string pinkS = folder + "\\Graphics\\Cars\\S\\TeamPink.png";
        private const string pinkW = folder + "\\Graphics\\Cars\\W\\TeamPink.png";

        //Red
        private const string redN = folder + "\\Graphics\\Cars\\N\\TeamRed.png";
        private const string redE = folder + "\\Graphics\\Cars\\E\\TeamRed.png";
        private const string redS = folder + "\\Graphics\\Cars\\S\\TeamRed.png";
        private const string redW = folder + "\\Graphics\\Cars\\W\\TeamRed.png";

        //Yellow
        private const string yellowN = folder + "\\Graphics\\Cars\\N\\TeamYellow.png";
        private const string yellowE = folder + "\\Graphics\\Cars\\E\\TeamYellow.png";
        private const string yellowS = folder + "\\Graphics\\Cars\\S\\TeamYellow.png";
        private const string yellowW = folder + "\\Graphics\\Cars\\W\\TeamYellow.png";

        //broken
        private const string broken = folder + "\\Graphics\\X.png";


        #endregion

        private static Bitmap GetGraphics(Section section, Rotation rotation) {
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

        private static Bitmap DrawParticipants(Bitmap plaatje, Section section, IParticipant? left, IParticipant? right, Rotation rotation){
            Graphics g = Graphics.FromImage(plaatje);
            int[] sectionCoord = sectionCoords[section];
            if(left is not null) {
                switch (rotation) {
                    case Rotation.WestEast: g.DrawImage(GetDriverGraphics(left, rotation), new Point(sectionCoord[0]+20, sectionCoord[1]+20));break;
                    case Rotation.NorthSouth: g.DrawImage(GetDriverGraphics(left, rotation), new Point(sectionCoord[0]+55, sectionCoord[1]+40)); break;
                    case Rotation.EastWest: g.DrawImage(GetDriverGraphics(left, rotation), new Point(sectionCoord[0]+30, sectionCoord[1]+50)); break;
                    case Rotation.SouthNorth: g.DrawImage(GetDriverGraphics(left, rotation), new Point(sectionCoord[0]+20, sectionCoord[1]+40)); break;
                }
            }
            if (right is not null) {
                switch (rotation) {
                    case Rotation.WestEast: g.DrawImage(GetDriverGraphics(right, rotation), new Point(sectionCoord[0]+20, sectionCoord[1]+55)); break;
                    case Rotation.NorthSouth: g.DrawImage(GetDriverGraphics(right, rotation), new Point(sectionCoord[0]+30, sectionCoord[1]+40)); break;
                    case Rotation.EastWest: g.DrawImage(GetDriverGraphics(right, rotation), new Point(sectionCoord[0]+30, sectionCoord[1]+20)); break;
                    case Rotation.SouthNorth: g.DrawImage(GetDriverGraphics(right, rotation), new Point(sectionCoord[0]+50, sectionCoord[1]+40)); break;
                }
            }

            return plaatje;
        }

        //returns string with path to relevant driver graphic
        private static Bitmap GetDriverGraphics(IParticipant driver, Rotation rotation) {
            if (driver.Equipment.IsBroken) {
                return ImageHandler.GetBitmap(broken);
            }
            switch (driver.TeamColor) {
                case TeamColor.Red:
                    switch (rotation) {
                        case Rotation.WestEast: return ImageHandler.GetBitmap(redE);
                        case Rotation.NorthSouth: return ImageHandler.GetBitmap(redS);
                        case Rotation.EastWest: return ImageHandler.GetBitmap(redW);
                        case Rotation.SouthNorth: return ImageHandler.GetBitmap(redN);
                    }
                    break;
                case TeamColor.Green:
                    switch (rotation) {
                        case Rotation.WestEast: return ImageHandler.GetBitmap(greenE);
                        case Rotation.NorthSouth: return ImageHandler.GetBitmap(greenS);
                        case Rotation.EastWest: return ImageHandler.GetBitmap(greenW);
                        case Rotation.SouthNorth: return ImageHandler.GetBitmap(greenN);
                    }
                    break;
                case TeamColor.Yellow:
                    switch (rotation) {
                        case Rotation.WestEast: return ImageHandler.GetBitmap(yellowE);
                        case Rotation.NorthSouth: return ImageHandler.GetBitmap(yellowS);
                        case Rotation.EastWest: return ImageHandler.GetBitmap(yellowW);
                        case Rotation.SouthNorth: return ImageHandler.GetBitmap(yellowN);
                    }
                    break;

                case TeamColor.Orange:
                    switch (rotation) {
                        case Rotation.WestEast: return ImageHandler.GetBitmap(orangeE);
                        case Rotation.NorthSouth: return ImageHandler.GetBitmap(orangeS);
                        case Rotation.EastWest: return ImageHandler.GetBitmap(orangeW);
                        case Rotation.SouthNorth: return ImageHandler.GetBitmap(orangeN);
                    }
                    break;
                case TeamColor.Pink:
                    switch (rotation) {
                        case Rotation.WestEast: return ImageHandler.GetBitmap(pinkE);
                        case Rotation.NorthSouth: return ImageHandler.GetBitmap(pinkS);
                        case Rotation.EastWest: return ImageHandler.GetBitmap(pinkW);
                        case Rotation.SouthNorth: return ImageHandler.GetBitmap(pinkN);
                    }
                    break;
                case TeamColor.Blue:
                    switch (rotation) {
                        case Rotation.WestEast: return ImageHandler.GetBitmap(blueE);
                        case Rotation.NorthSouth: return ImageHandler.GetBitmap(blueS);
                        case Rotation.EastWest: return ImageHandler.GetBitmap(blueW);
                        case Rotation.SouthNorth: return ImageHandler.GetBitmap(blueN);
                    }
                    break;
            }
            throw new Exception("GUI: Driver graphic not found");
        }

        private static int[] CalculateBounds(Track track) {
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
