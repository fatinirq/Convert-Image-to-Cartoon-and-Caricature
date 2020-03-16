using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace Cartoon_KMCG
{
    class Convert2WPFBitmap
    {
        public static BitmapImage Win2WPFBitmap(Bitmap bmp)
        {
            BitmapImage wpfBmp = new BitmapImage();
            MemoryStream strm = new MemoryStream();
            bmp.Save(strm, System.Drawing.Imaging.ImageFormat.Bmp);
            strm.Position = 0;
            wpfBmp.BeginInit();
            wpfBmp.StreamSource = strm;
            wpfBmp.EndInit();
            return wpfBmp;

        }
    }
}
