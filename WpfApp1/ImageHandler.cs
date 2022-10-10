using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;
using Controller;
using Model;
using System.Windows.Media.Imaging;

namespace WPF {
    public static class ImageHandler {
        private static Dictionary<String, Bitmap> bitmapData;
        private static int imageSize = 256;

        private static Bitmap GetBitmap(String path) {
            if (!bitmapData.ContainsKey(path)) {
                bitmapData[path] = new Bitmap(path);
            }
            return bitmapData[path];
        }

        private static void clearCache() {
            bitmapData.Clear();
        }

        private static int[] calculateBounds(Track track) {
            int rotationINT = track.rotationINT;
            int[] tempBounds = new int[4] {0,0,0,0};
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

        public static Bitmap getNewBitmap(int width, int height) {
            return new Bitmap(width, height);
        }

        public static void OnNextRaceEvent(Object sender, NextRaceArgs e) {
            clearCache();
            int[] bounds = calculateBounds(e.race.Track);
            Bitmap bitmap = getNewBitmap(bounds[0], bounds[1]);
        }
    }
}
