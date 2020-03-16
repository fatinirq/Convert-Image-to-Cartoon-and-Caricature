using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace Cartoon_KMCG
{
    class Filters
    {
        public static Bitmap meanFilter_Color(Bitmap bmp,int kernalsize)
        {
            Bitmap bmpOut = bmp;
            int r;
            int g;
            int b;
            for (int i = kernalsize / 2; i < bmp.Height - kernalsize / 2; i++)
                for (int j = kernalsize / 2; j < bmp.Width - kernalsize / 2; j++)
                {
                    r = 0;g = 0;b = 0;
                    for (int k = -kernalsize / 2; k <= kernalsize / 2; k++)
                        for (int l = -kernalsize / 2; l <= kernalsize / 2; l++)
                        {
                            int y = j + l;
                            int x = i + k;
                            r = r + bmp.GetPixel(y, x).R;
                            g = g + bmp.GetPixel(y, x).G;
                            b = b + bmp.GetPixel(y, x).B;
                        }
                    r = r / (kernalsize * kernalsize);
                    g = g / (kernalsize * kernalsize);
                    b = b / (kernalsize * kernalsize);
                    bmpOut.SetPixel(j, i, Color.FromArgb(r, g, b));

                }
            return bmpOut;

        }
        public static Bitmap meanFilter(Bitmap bmp, int kernalsize)
        {
            Bitmap bmpOut = bmp;
            
            for (int i = kernalsize/2; i < bmp.Height - kernalsize/2; i++)
                for (int j = kernalsize/2; j < bmp.Width - kernalsize/2; j++)
                {
                    int sum = 0;
                    for (int k = -kernalsize / 2; k <= kernalsize / 2; k++)
                        for (int l = -kernalsize / 2; l <= kernalsize / 2; l++)
                        {
                            int y = j + l;
                            int x = i + k;
                            
                            sum = sum + bmp.GetPixel(y,x).B;
                        }
                          
                    int av = sum / (kernalsize * kernalsize);
                    bmpOut.SetPixel(j, i, Color.FromArgb(av, av, av));
                }
            return bmpOut;

        }
        public static Bitmap meanFilter_LIP(Bitmap bmp, int kernalsize)
        {
            Bitmap bmpOut = bmp;

            for (int i = kernalsize / 2; i < bmp.Height - kernalsize / 2; i++)
                for (int j = kernalsize / 2; j < bmp.Width - kernalsize / 2; j++)
                {
                    double sum = 1;
                    for (int k = -kernalsize / 2; k <= kernalsize / 2; k++)
                        for (int l = -kernalsize / 2; l <= kernalsize / 2; l++)
                        {
                            int y = j + l;
                            int x = i + k;
                            // double mul = 255 - 255 * Math.Pow((1 - bmp.GetPixel(y, x).B / 255), 1 / (kernalsize * kernalsize));
                            // sum = sum + mul - (mul + sum) / 255;
                            sum = sum * bmp.GetPixel(y, x).B;
                        }

                    double av = 256 - 256 * (Math.Pow(sum, 1 / (kernalsize ^ 2)));
                    byte g = (byte)Math.Truncate(av);
                    bmpOut.SetPixel(j, i, Color.FromArgb(g, g, g));
                }
            return bmpOut;

        }
        public static Bitmap gaussianFilter(Bitmap bmp, int sigma)
        {
            Bitmap bmpTemp = bmp;
            double r;
            double g;
            double b;
            ///calculating mask coefficents
            double[,] kernal = new double[5, 5];
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    kernal[i, j] = 1 / (2 * Math.PI * Math.Pow(sigma, 2)) * Math.Exp(-(Math.Pow(Math.Abs(i - 2), 2) + Math.Pow(Math.Abs(j - 2), 2)) / (2 * Math.Pow(sigma, 2)));
            for (int i = 2; i < bmp.Height - 2; i++)
                for (int j = 2; j < bmp.Width - 2; j++)
                {
                    r = 0;
                    g = 0;
                    b = 0;
                    for (int k = -2; k < 3; k++)
                        for (int l = -2; l < 3; l++)

                        {
                            r = r + (double)bmp.GetPixel(j + l, i + k).R * kernal[k + 2, l + 2];
                            g = g + (double)bmp.GetPixel(j + l, i + k).G * kernal[k + 2, l + 2];
                            b = b + (double)bmp.GetPixel(j + l, i + k).B * kernal[k + 2, l + 2];

                        }
                    bmpTemp.SetPixel(j, i, Color.FromArgb((byte)Math.Truncate(r), (byte)Math.Truncate(g), (byte)Math.Truncate(b)));
                }
            BitmapImage bmpout = new BitmapImage();
            MemoryStream strm = new MemoryStream();
            bmpTemp.Save(strm, System.Drawing.Imaging.ImageFormat.Bmp);
            strm.Position = 0;

            bmpout.BeginInit();
            bmpout.StreamSource = strm;
            bmpout.EndInit();
            strm.Close();
            return bmpTemp;

        }
        public static Bitmap gaussianFilter(Bitmap bmp, int sigma, int kSize)
        {
            Bitmap bmpTemp = bmp;
            double r;
            double gray;
            double b;
            ///calculating mask coefficents
            double[,] kernal = new double[kSize, kSize];
            for (int i = 0; i < kSize; i++)
                for (int j = 0; j < kSize; j++)
                    kernal[i, j] = 1 / (2 * Math.PI * Math.Pow(sigma, 2)) * Math.Exp(-(Math.Pow(Math.Abs(i - 2), 2) + Math.Pow(Math.Abs(j - 2), 2)) / (2 * Math.Pow(sigma, 2)));
            for (int i = kSize/2; i < bmp.Height - kSize/2; i++)
                for (int j = kSize/2; j < bmp.Width - kSize/2; j++)
                {
                   
                    gray = 0;
                    
                    for (int k = -kSize/2; k <=kSize/2; k++)
                        for (int l = -kSize/2; l <= kSize/2; l++)

                        {
                            gray = gray + (double)bmp.GetPixel(j + l, i + k).G * kernal[k + kSize/ 2, l + kSize/2];
                        }
                    bmpTemp.SetPixel(j, i, Color.FromArgb((byte)Math.Truncate(gray), (byte)Math.Truncate(gray), (byte)Math.Truncate(gray)));
                }
            BitmapImage bmpout = new BitmapImage();
            MemoryStream strm = new MemoryStream();
            bmpTemp.Save(strm, System.Drawing.Imaging.ImageFormat.Bmp);
            strm.Position = 0;

            bmpout.BeginInit();
            bmpout.StreamSource = strm;
            bmpout.EndInit();
            strm.Close();
            return bmpTemp;

        }
        public static Bitmap Lablacian(Bitmap bmp)
        {
            int m1, m2, m3;
            Bitmap bmpTemp = new Bitmap(bmp);
            for (int i = 2; i < bmp.Height - 2; i++)
                for (int j = 2; j < bmp.Width - 2; j++)
                { }
            return bmpTemp;
        }
        public struct tempImage
        {
          public  Color clr;
           public  int gray;
        }
        public Bitmap Median(Bitmap bmp)
        {
            Bitmap bmpout = new Bitmap(bmp);
          
            for (int i = 2; i < bmp.Height - 2; i++)
                for (int j = 2; j < bmp.Width - 2; j++)
                {
                    tempImage[] n = new tempImage[9];
                    int count = 0;
                    for (int k= -1;k<2;k++)
                        for (int l=-1; l<2; l++)
                        {
                            n[count].clr = bmpout.GetPixel(j + l, i + k);
                            n[count].gray = (n[count].clr.R + n[count].clr.G + n[count].clr.B) / 3;
                            count++;
                            
                        }
                    n.OrderBy(temp=>temp.gray);
                    bmpout.SetPixel(j, i, n[4].clr);

                }
            return bmpout;
        }

    }
}