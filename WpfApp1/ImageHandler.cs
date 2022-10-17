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
using System.Drawing.Imaging;

namespace WPF {
    public static class ImageHandler {
        private static Dictionary<String, Bitmap> bitmapData = new Dictionary<string, Bitmap>();
        public static int imageSize = 256;

        public static Bitmap GetBitmap(String path) {
            if (!bitmapData.ContainsKey(path)) {
                bitmapData.Add(path, new Bitmap(path));
            }
            return (Bitmap)bitmapData[path].Clone();
        }

        public static void clearCache() {
            bitmapData.Clear();
        }

        public static Bitmap getNewBitmap(int x, int y,int scale) {
            string key = "empty";
            if (!bitmapData.ContainsKey(key)) {
                Bitmap bitmap = new Bitmap(x, y);
                bitmapData.Add(key, bitmap);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.FillRectangle(new SolidBrush(System.Drawing.Color.DarkGreen), 0, 0, x, y);
            }
            return (Bitmap)bitmapData[key].Clone();

        }

        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap) {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            } finally {
                bitmap.UnlockBits(bitmapData);
            }
        }
    }
}
