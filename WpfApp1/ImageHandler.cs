using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;

namespace WPF {
    public static class ImageHandler {
        private static Dictionary<String, Bitmap> bitmapData;


        private static Bitmap GetBitmap(String path) {
            if (!bitmapData.ContainsKey(path)) {
                bitmapData[path] = new Bitmap(path);
            }
            return bitmapData[path];
        }

        private static void clearCache() {
            bitmapData.Clear();
        }
    }
}
