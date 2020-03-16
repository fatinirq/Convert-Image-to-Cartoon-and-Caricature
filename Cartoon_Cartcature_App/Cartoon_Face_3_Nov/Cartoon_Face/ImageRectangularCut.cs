using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartoon_Face
{
    class ImageRectangularCut
    {
        public static Bitmap GetViolaFace(Bitmap bmp, Rectangle rec)
        {
            Bitmap bmpFace = new Bitmap(rec.Height, rec.Height);
            Point p = new Point(rec.Top, rec.Left);
            int k = 0, l = 0;
            for (int i = p.X; i < rec.Height + p.X; i++)
            {
                for (int j = p.Y; j < rec.Height + p.Y; j++)
                {
                    Color clr = bmp.GetPixel(j, i);

                    bmpFace.SetPixel(k, l, clr);
                    k++;
                }
                l++;
                k = 0;
                    }
            return bmpFace;

        }
        public static List<Bitmap> GetEyes(Bitmap bmp, List<Rectangle> Eyes)
        {
            List<Bitmap> lstEyes = new List<Bitmap>();
            foreach (Rectangle rec in Eyes)
            {
                Bitmap bmpEye = new Bitmap(rec.Height, rec.Height);
                Point p = new Point(rec.Top, rec.Left);
                int k = 0, l = 0;
                for (int i = p.X; i < rec.Height + p.X; i++)
                {
                    for (int j = p.Y; j < rec.Height + p.Y; j++)
                    {
                        Color clr = bmp.GetPixel(j, i);

                        bmpEye.SetPixel(k, l, clr);
                        k++;
                    }
                    l++;
                    k = 0;
                }
                lstEyes.Add(bmp);
            }
            return lstEyes;

        }
        public static List<Bitmap> GetViolaEyes(Bitmap bmp, List<Rectangle> Eyes)
        {
            List<Bitmap> lstEyes = new List<Bitmap>();
            foreach (Rectangle rec in Eyes)
            {
                Bitmap bmpEye = new Bitmap(rec.Height, rec.Height);
                Point p = new Point(rec.Top, rec.Left);
                int k = 0, l = 0;
                for (int i = p.X; i < rec.Height + p.X; i++)
                {
                    for (int j = p.Y; j < rec.Height + p.Y; j++)
                    {
                        Color clr = bmp.GetPixel(j, i);

                        bmpEye.SetPixel(k, l, clr);
                        k++;
                    }
                    l++;
                    k = 0;
                }
                lstEyes.Add(bmp);
            }
            return lstEyes;

        }
    }
}
