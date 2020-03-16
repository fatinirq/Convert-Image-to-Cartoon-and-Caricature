using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;

namespace Cartoon_Face
{
    public class ColorSpaces
    {
        public struct RGB
        {
            public byte R;
            public byte G;
            public byte B;
        }

        public struct HSV
        {
            public float H;
            public float S;
            public float V;
        }
        public struct HSI
        {
            public float H;
            public float S;
            public float I;
        }
        public struct YUV
        {
            public int Y;
            public int U;
            public int V;
        }
        public static List<YUV> ConvertRGBToYUV(List<Color> rgbColors)
        {
            List<YUV> yuv = new List<YUV>();
           
            foreach (Color rgb in rgbColors)
            {
                YUV yuvitem = new YUV();
                yuvitem.Y = (int)Math.Truncate((rgb.R * 0.299 + rgb.G * 0.587 + rgb.B * 0.144));
                yuvitem.U = (int)Math.Truncate((-0.148 * rgb.R - 0.288 * rgb.G + 0.436 * rgb.B));
                yuvitem.V = (int)Math.Truncate((0.615 * rgb.R - 0.515 * rgb.G - 0.144 * rgb.B));
                yuv.Add(yuvitem);
            }
            return yuv;
        }

        public static RGB ConvertYUVToRGB(YUV yuv)
        {
            RGB rgb;

            rgb.R = (byte)Math.Truncate(yuv.Y + 1.140 * yuv.V);
            rgb.G = (byte)Math.Truncate(yuv.Y - 0.395 * yuv.U - 0.581 * yuv.V);
            rgb.B = (byte)Math.Truncate(yuv.Y + 2.032 * yuv.U);

            return rgb;
        }
        public static List<HSV> ConvertRGBToHSV(List<Color> rgbColors)
        {
            List<HSV> hsv = new List<HSV>();

            foreach (Color rgb in rgbColors)
            {
                HSV hsvItem = new HSV();

                float M, m,r,g,b;
                M = (float)Math.Max(rgb.R, Math.Max(rgb.R, rgb.G));
                m = (float)Math.Min(rgb.R, Math.Min(rgb.G, rgb.B));
                hsvItem.V = M;
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
                
                hsv.Add(hsvItem);
            }
          
            
            
            return hsv;
        }
        public static HSV[,] ConvertRGBToHSV_BMP(Bitmap bmp)
        {
            float maxh = 0, minh = 100, maxI = 0, minI = 100;
            HSV[,] output = new HSV[bmp.Height, bmp.Width];
            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                    HSV hsvItem = new HSV();
                    Color rgb = bmp.GetPixel(j, i);
                    float M, m, r, g, b;
                    M = (float)Math.Max(rgb.R, Math.Max(rgb.G, rgb.B));
                    m = (float)Math.Min(rgb.R, Math.Min(rgb.G, rgb.B));
                    hsvItem.V = M;
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
                    if (hsvItem.V < minI)
                        minI = hsvItem.V;
                    if (hsvItem.V > maxI)
                        maxI=hsvItem.V;
                    if (hsvItem.H < minh)
                        minh = hsvItem.H;
                    if (hsvItem.H > maxh)
                        maxh = hsvItem.H;
                    output[i, j] = hsvItem;
                }
            //MessageBox.Show("Min V=" + minI + ", Min H= " + minh + " ,max H= " + maxh + " , max V=" + maxI);
            return output;

        }
        public static RGB ConvertHSVToRGB(HSV hsv)
        {
            RGB rgb = new RGB();
            float HEX, primaryC, secondaryC, a, b, c;
            HEX = hsv.H / 60f;
            primaryC = (float)Math.Truncate(HEX);
            secondaryC = HEX - primaryC;
            a = (1f - hsv.S) * hsv.V;
            b = (1f - (hsv.S * secondaryC)) * hsv.V;
            c = (1f - (hsv.S * (1 - secondaryC))) * hsv.V;
            if (primaryC == 0) { rgb.R = (byte)hsv.V; rgb.G = (byte)c; rgb.B = (byte)a; }
            if (primaryC == 1) { rgb.R = (byte)b; rgb.G = (byte)hsv.V; rgb.B = (byte)a; }
            if (primaryC == 2) { rgb.R = (byte)a; rgb.G = (byte)hsv.V; rgb.B = (byte)c; }
            if (primaryC == 3) { rgb.R = (byte)a; rgb.G = (byte)b; rgb.B = (byte)hsv.V; }
            if (primaryC == 4) { rgb.R = (byte)c; rgb.G = (byte)a; rgb.B = (byte)hsv.V; }
            if (primaryC == 5) { rgb.R = (byte)hsv.V; rgb.G = (byte)a; rgb.B = (byte)b; }
            //int hi = Convert.ToInt32(Math.Floor(hsv.H / 60)) % 6;
            //double f = hsv.H / 60 - Math.Floor(hsv.H / 60);

            //// hsv.V = hsv.V * 255;
            //int v = Convert.ToInt32(hsv.V);
            //int p = Convert.ToInt32(hsv.V * (1 - hsv.S));
            //int q = Convert.ToInt32(hsv.V * (1 - f * hsv.S));
            //int t = Convert.ToInt32(hsv.V * (1 - (1 - f) * hsv.S));

            //if (hi == 0)

            //    clr = Color.FromArgb(255, v, t, p);
            //else if (hi == 1)
            //    clr = Color.FromArgb(255, q, v, p);
            //else if (hi == 2)
            //    clr = Color.FromArgb(255, p, v, t);
            //else if (hi == 3)
            //    clr = Color.FromArgb(255, p, q, v);
            //else if (hi == 4)
            //    clr = Color.FromArgb(255, t, p, v);
            //else
            //    clr = Color.FromArgb(255, v, p, q);

            //rgb.R = clr.R;
            //rgb.G = clr.G;
            //rgb.B = clr.B;

            return rgb;

        }
        public static List<HSI> ConvertRGBToHSI(List<Color> rgbColors)
        {
            List<HSI> hsiList = new List<HSI> ();
            foreach (Color rgb in rgbColors)
            {
                HSI hsi = new HSI();
                float theta = (float)Math.Truncate(Math.Acos((1 / 2 * ((rgb.R - rgb.G) + (rgb.R - rgb.B))) /
                Math.Sqrt(Math.Pow(rgb.R - rgb.G, 2) + (rgb.R - rgb.G) * (rgb.G - rgb.B))));
                if (Math.Max(rgb.B, rgb.G) == rgb.B) hsi.H = 360 - theta;
                else hsi.H = theta;
                hsi.I = (rgb.R + rgb.G + rgb.B) / 3f;
                hsi.S = 1 - (3 / (float)(rgb.R + rgb.G + rgb.B) * (float)Math.Min(rgb.R, Math.Min(rgb.G, rgb.B)));
                if (hsi.I == 0) hsi.S = 0;
                hsiList.Add(hsi);
            }
            return hsiList;
        }
        public static HSI[,] ConvertRGBToHSI_BMP(Bitmap bmp)
        {
            float maxh=0, minh=100, maxI=0, minI=100;
            HSI[,] output = new HSI[bmp.Height, bmp.Width];
            for (int i=0; i<bmp.Height;i++)
                for(int j=0;j<bmp.Width; j++)
                {
                    Color rgb = bmp.GetPixel(j, i);
                HSI hsi = new HSI();
                    float theta = (float)Math.Truncate(Math.Acos((1 / 2 * ((rgb.R - rgb.G) + (rgb.R - rgb.B))/
                    Math.Round((Math.Sqrt(Math.Pow(rgb.R - rgb.G, 2) + (rgb.R - rgb.B) * (rgb.G - rgb.B))), 2))));
                if (Math.Max(rgb.B, rgb.G) == rgb.B) hsi.H = 360 - theta;
                else hsi.H = theta;
                hsi.I = (rgb.R + rgb.G + rgb.B) / 3f;
                hsi.S = 1 - (3 / (float)(rgb.R + rgb.G + rgb.B) * (float)Math.Min(rgb.R, Math.Min(rgb.G, rgb.B)));
                if (hsi.I == 0) hsi.S = 0;
                    output[i, j] = hsi;
                    if (maxh < hsi.H)
                        maxh = hsi.H;
                    if (maxI < hsi.I)
                        maxI = hsi.I;
                    if (minI > hsi.I)
                        minI = hsi.I;
                    if (minh > hsi.H)
                        minh = hsi.H;


                        
            }
           // MessageBox.Show("Min I=" + minI + ", Min H= " + minh + " ,max H= " + maxh + " , max I=" + maxI);
            return output;
           
        }
        public static RGB ConvertHSIToRGB(HSI hsi)
        {
            RGB rgb = new RGB();
            if (hsi.H >= 0 && hsi.H < 120)
            {
                rgb.R = (byte)Math.Truncate(hsi.I * (1 + (hsi.S * Math.Cos(hsi.H)) / Math.Cos(60 - hsi.H)));
                rgb.B = (byte)Math.Truncate(hsi.I * (1 - hsi.S));
                rgb.G = (byte)Math.Truncate(3 * hsi.I - (rgb.R + rgb.B));

            }
            if (hsi.H >= 120 && hsi.H < 240)
            {
                hsi.H = hsi.H - 120;
                rgb.G = (byte)Math.Truncate(hsi.I * (1 + (hsi.S * Math.Cos(hsi.H)) / Math.Cos(60 - hsi.H)));
                rgb.R = (byte)Math.Truncate(hsi.I * (1 - hsi.S));
                rgb.B = (byte)Math.Truncate(3 * hsi.I - (rgb.G + rgb.R));

            }
            if (hsi.H >= 240 && hsi.H < 360)
            {
                hsi.H = hsi.H - 240;
                rgb.B = (byte)Math.Truncate(hsi.I * (1 + (hsi.S * Math.Cos(hsi.H)) / Math.Cos(60 - hsi.H)));
                rgb.R = (byte)Math.Truncate(hsi.I * (1 - hsi.S));
                rgb.G = (byte)Math.Truncate(3 * hsi.I - (rgb.B + rgb.R));

            }


            return rgb;
        }


    }
}
