using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartoon_Face
{
     class SkinDetection
    {
     
            static int h, w;
            public struct YCrCb
            {
                public int y;
                public int Cr;
                public int Cb;
            }
            public struct HSV
            {
                public double H;
                public double S;
                public double V;
            }
              SkinDetection(Bitmap bmp)
            {
                h = bmp.Height;
                w = bmp.Width;
            }
            // public HSV[,] ConvertRGB2HSV(Bitmap bmp)
            // {

            // }
             YCrCb[,] ConvertRGB2YCrCb(Bitmap bmp)
            {
                YCrCb[,] output = new YCrCb[bmp.Height, bmp.Width];
                for (int i = 0; i < bmp.Height; i++)
                    for (int j = 0; j < bmp.Width; j++)
                    {
                        Color clr = new Color();
                        clr = bmp.GetPixel(j, i);

                        output[i, j].y = (int)(0.299 * clr.R + 0.587 * clr.G + 0.114 * clr.B);
                        output[i, j].Cb = (int)(-0.169 * clr.R - 0.331 * clr.G + 0.500 * clr.B + 128);
                        output[i, j].Cr = (int)(0.500 * clr.R - 0.419 * clr.G - 0.082 * clr.B + 128);
                    }
                return output;
            }
        public HSV[,] ConvertRGBToHSV(Bitmap bmp)
        {
            HSV[,] output = new HSV[bmp.Height, bmp.Width];

            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color rgb = bmp.GetPixel(j, i);
                    HSV hsvItem = new HSV();

                float M, m, r, g, b;
                M = (float)Math.Max(rgb.R, Math.Max(rgb.R, rgb.G));
                m = (float)Math.Min(rgb.R, Math.Min(rgb.G, rgb.B));
                hsvItem.V = M/255;
                r = rgb.R / 255f;
                g = rgb.G / 255f;
                b = rgb.B / 255f;


                if (M == 0)
                {
                    hsvItem.S = 0;
                    hsvItem.H = 180;
                }
                else
                {
                    hsvItem.S = (M - m) / M;
                    if (rgb.R == M) hsvItem.H = (int)(60 * (b - g));
                    if (rgb.G == M) hsvItem.H = (int)(60 * (2 + r - b));
                    if (rgb.B == M) hsvItem.H = (int)(60 * (4 + g - r));
                    if (hsvItem.H >= 360) hsvItem.H = 360 - hsvItem.H;
                    if (hsvItem.H < 0) hsvItem.H = hsvItem.H + 360;
                }
                   output[i, j] = hsvItem;
            }
            return output;
        }

            public static  Bitmap skinColorSegments(Bitmap bmp)
            {
            SkinDetection s = new SkinDetection(bmp);
            YCrCb[,] data = s.ConvertRGB2YCrCb(bmp);
                Bitmap output = new Bitmap(w, h);
                for (int i = 0; i < h; i++)
                    for (int j = 0; j < w; j++)
                    {
                    if (//data[i, j].y < 180 && data[i, j].y > 90
                         data[i, j].Cr > 150 && data[i, j].Cr < 200
                        && data[i, j].Cb > 100 && data[i, j].Cb < 150)
                        output.SetPixel(j, i, Color.White);
                    else
                        output.SetPixel(j, i, Color.Black);
                    }


                return output;

            }
        public static Bitmap skinColorSegments_2nd(Bitmap bmp)
        {
            SkinDetection s = new SkinDetection(bmp);
            YCrCb[,] data = s.ConvertRGB2YCrCb(bmp);
            HSV[,] data2 = s.ConvertRGBToHSV(bmp);
            Color[,] data3 = new Color[bmp.Height, bmp.Width];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                    data3[i, j] = bmp.GetPixel(j, i);
                }
                    Bitmap output = new Bitmap(w, h);
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                    if(data3[i, j].R > 95 && data3[i, j].G > 40
                        && data3[i, j].B > 20 && data3[i, j].R > data3[i, j].G
                        && data3[i, j].R > data3[i, j].B)
                        output.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                    else if (data2[i, j].H >= 0 && data2[i, j].H <= 50.0
                        && data2[i, j].S >= 0.23 && data2[i, j].S <= 0.68)
                       
                    output.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                    else if (data[i,j].Cr > 135 && data[i, j].Cb>85
                            && data[i, j].y>80 && data[i, j].Cr<=(1.5862* data[i, j].Cb+20)
                            && data[i, j].Cr>=(0.3448* data[i, j].Cb)+76.1069
                            && data[i, j].Cr>=(-4.5652 * data[i, j].Cr)+234.5652
                            && data[i,j].Cr<=(-1.15 * data[i,j].Cb+301.75)
                            && data[i, j].Cr<=(-2.2857* data[i, j].Cb)+423.85)
                        output.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                }
            return output;
        }
        public static Bitmap skinColorSegments_3rd(Bitmap bmp)
        {
            SkinDetection s = new SkinDetection(bmp);
           // YCrCb[,] data = s.ConvertRGB2YCrCb(bmp);
            HSV[,] data2 = s.ConvertRGBToHSV(bmp);
          //  Color[,] data3 = new Color[bmp.Height, bmp.Width];
         //   for (int i = 0; i < h; i++)
            //    for (int j = 0; j < w; j++)
            //    {
            //        data3[i, j] = bmp.GetPixel(j, i);
             //   }
            Bitmap output = new Bitmap(w, h);
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                  
                        if (((data2[i, j].H >= 0 && data2[i, j].H <= 50.0)
                        ||(data2[i, j].H >= 240 && data2[i, j].H <= 360))
                            && data2[i, j].S >= 0.23 && data2[i, j].S <= 1
                        && data2[i,j].V>=0.3 && data2[i,j].V<=1)
  
                            output.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                       
                }


            return output;

        }
        public static Bitmap skinColorSegments_4th(Bitmap bmp)
        {
            SkinDetection s = new SkinDetection(bmp);
            YCrCb[,] data = s.ConvertRGB2YCrCb(bmp);
            HSV[,] data2 = s.ConvertRGBToHSV(bmp);
           
            Bitmap output = new Bitmap(w, h);
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                    if (((data2[i, j].H >= 0 && data2[i, j].H <= 50.0)
                        || (data2[i, j].H >= 240 && data2[i, j].H <= 360))
                            && data2[i, j].S >= 0.23 && data2[i, j].S <= 1
                        && data2[i, j].V >= 0.3 && data2[i, j].V <= 1)

                        output.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                 else if (//data[i, j].y < 180 && data[i, j].y > 90
                             data[i, j].Cr > 150 && data[i, j].Cr < 200
                            && data[i, j].Cb > 100 && data[i, j].Cb < 150)
                        output.SetPixel(j, i, Color.FromArgb(255, 255, 255));

                }


            return output;

        }
    }
    }

