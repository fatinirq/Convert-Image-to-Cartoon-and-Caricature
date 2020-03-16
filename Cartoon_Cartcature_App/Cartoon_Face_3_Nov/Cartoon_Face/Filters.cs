using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Cartoon_Face
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
        public static void  meanFilter_Color(Color[,] bmp, int kernalsize)
        {
            Color[,] bmpOut = bmp;
            int h = bmp.GetLength(0);
            int w = bmp.GetLength(1);
            int r;
            int g;
            int b;
            for (int i = kernalsize / 2; i < h - kernalsize / 2; i++)
                for (int j = kernalsize / 2; j < w - kernalsize / 2; j++)
                {
                    r = 0; g = 0; b = 0;
                    for (int k = -kernalsize / 2; k <= kernalsize / 2; k++)
                        for (int l = -kernalsize / 2; l <= kernalsize / 2; l++)
                        {
                            int y = j + l;
                            int x = i + k;
                            r = r + bmp[x,y].R;
                            g = g + bmp[x, y].G;
                            b = b + bmp[x, y].B;
                        }
                    r = r / (kernalsize * kernalsize);
                    g = g / (kernalsize * kernalsize);
                    b = b / (kernalsize * kernalsize);
                    bmpOut[i,j]= Color.FromArgb(r, g, b);

                }
            bmp = bmpOut;

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
        public static double[,] meanFilter_arr(Bitmap bmp, int kernalsize)
        {
            double[,] bmpOut =new double[bmp.Height,bmp.Width];
            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                    bmpOut[i, j] = bmp.GetPixel(j, i).R;
            for (int i = kernalsize / 2; i < bmp.Height - kernalsize / 2; i++)
                for (int j = kernalsize / 2; j < bmp.Width - kernalsize / 2; j++)
                {
                    int sum = 0;
                    for (int k = -kernalsize / 2; k <= kernalsize / 2; k++)
                        for (int l = -kernalsize / 2; l <= kernalsize / 2; l++)
                        {
                            int y = j + l;
                            int x = i + k;

                            sum = sum + bmp.GetPixel(y, x).B;
                        }

                    double av = sum / (kernalsize * kernalsize);
                    bmpOut[i,j]= av;
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
        public static Bitmap gaussianFilter_List_Gray(Bitmap bmp, int sigma, List<System.Drawing.Point> lstPoints, int count, int kSize)
        {
            Bitmap bmpTemp = new Bitmap(bmp);
            Bitmap bmpOut = new Bitmap(bmp);
           
            double g;
            int c = 0;
            ///calculating mask coefficents
            double[,] kernal = new double[kSize, kSize];
            for (int i = 0; i < kSize; i++)
                for (int j = 0; j < kSize; j++)
                    kernal[i, j] = 1 / (2 * Math.PI * Math.Pow(sigma, 2)) * Math.Exp(-(Math.Pow(Math.Abs(i - 2), 2) + Math.Pow(Math.Abs(j - 2), 2)) / (2 * Math.Pow(sigma, 2)));
            while (c <= count)
            {
                foreach (System.Drawing.Point p in lstPoints)
                    if (p.X > kSize/2 && p.X < bmp.Height - kSize/2 && p.Y > kSize/2 && p.Y < bmp.Width - kSize/2)
                    {


                        int val = bmpTemp.GetPixel(p.Y , p.X).R;
                        g = 0;

                        for (int k = -kSize/2; k <=kSize/2; k++)
                            for (int l = -kSize/2; l <=kSize/2; l++)

                            {

                                g = g + (double)bmpTemp.GetPixel(p.Y + l, p.X + k).R * kernal[k + kSize/2, l + kSize/2];


                            }
                        bmpOut.SetPixel(p.Y, p.X, Color.FromArgb((byte)Math.Truncate(g), (byte)Math.Truncate(g), (byte)Math.Truncate(g)));
                    }
                bmpTemp = new Bitmap(bmpOut);
                c++;
            }
        
            return bmpOut;

        }
        public static Bitmap meanFilter_List_Gray(Bitmap bmp,  List<System.Drawing.Point> lstPoints, int count, int kSize)
        {
            Bitmap bmpTemp = new Bitmap(bmp);
            Bitmap bmpOut = new Bitmap(bmp);

            double g;
            int c = 0;
            ///calculating mask coefficents
            
            while (c <= count)
            {
                foreach (System.Drawing.Point p in lstPoints)
                    if (p.X > kSize / 2 && p.X < bmp.Height - kSize / 2 && p.Y > kSize / 2 && p.Y < bmp.Width - kSize / 2)
                    {


                        int val = bmpTemp.GetPixel(p.Y, p.X).R;
                        g = 0;

                        for (int k = -kSize / 2; k <= kSize / 2; k++)
                            for (int l = -kSize / 2; l <= kSize / 2; l++)

                            {

                                g = g + (double)bmpTemp.GetPixel(p.Y + l, p.X + k).R;


                            }
                        g = g / kSize;
                        bmpOut.SetPixel(p.Y, p.X, Color.FromArgb((byte)Math.Truncate(g), (byte)Math.Truncate(g), (byte)Math.Truncate(g)));
                    }
                bmpTemp = new Bitmap(bmpOut);
                c++;
            }

            return bmpOut;

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
        public static  void Median(Bitmap bmp, System.Drawing.Point P1, System.Drawing.Point P2)
        {
            Bitmap bmpout = new Bitmap(bmp);
            for (int z = 0; z < 3; z++)
            {
                

                for (int i = P1.X; i < P2.X; i++)
                    for (int j = P1.Y; j < P2.Y; j++)
                    {
                        tempImage[] n = new tempImage[25];
                        int count = 0;
                        for (int k = -2; k <= 2; k++)
                            for (int l = -2; l <= 2; l++)
                            {
                                n[count].clr = bmpout.GetPixel(j + l, i + k);
                                n[count].gray = (n[count].clr.R + n[count].clr.G + n[count].clr.B) / 3;
                                count++;

                            }
                        int gray = n[12].gray;
                        n.OrderBy(temp => temp.gray);
                       // if(n[12].gray>=gray)
                        bmpout.SetPixel(j, i, n[12].clr);

                    }
                bmp = new Bitmap(bmpout);
            }
            bmpout.Dispose();
        }
        public static void Median(Color[,] arr, byte[,] changedP)
        {
            
            int h = arr.GetLength(0);
            int w = arr.GetLength(1);
            for (int x = 0; x < 15; x++)
            {
                Color[,] output = arr;
                for (int i = 2; i < h - 2; i++)
                    for (int j = 2; j < w - 2; j++)
                    {
                        if (changedP[i, j] != 0)
                        {
                            tempImage[] n = new tempImage[9];
                            int count = 0;
                            for (int k = -1; k < 2; k++)
                                for (int l = -1; l < 2; l++)
                                {
                                    n[count].clr = arr[i + l, j + k]; ;
                                    n[count].gray = (n[count].clr.R + n[count].clr.G + n[count].clr.B) / 3;
                                    count++;

                                }
                            n.OrderBy(temp => temp.gray);
                            output[i, j] = n[4].clr;
                        }
                    }
                arr = output;
            }
            for (int x = 0; x < 15; x++)
            {
                Color[,] output = new Color[arr.GetLength(0),arr.GetLength(1)];
                output = arr;
                for (int i = 2; i < h - 2; i++)
                    for (int j = 2; j < w - 2; j++)
                    {
                       
                            tempImage[] n = new tempImage[9];
                            int count = 0;
                            for (int k = -1; k < 2; k++)
                                for (int l = -1; l < 2; l++)
                                {
                                    n[count].clr = arr[i + l, j + k]; ;
                                    n[count].gray = (n[count].clr.R + n[count].clr.G + n[count].clr.B) / 3;
                                    count++;

                                }
                            n.OrderBy(temp => temp.gray);
                            output[i, j] = n[4].clr;
                        
                    }
                arr = output;
            }
        }
        public static Bitmap Sobel_Hor(Bitmap bmp,  List<System.Drawing.Point> lstPoint)
        {
            byte[,] bmpIn = ImageEnhancement.convertBmpGray2Arr(bmp);
            float[,] bmpOut = new float[bmp.Height, bmp.Width];
            int[,] Dx = {{-1,0,1},
                         {-2,0,2},
                         {-1,0,1}};

            int[,] Dy = {{1,2,1},
                         {0,0,0},
                         {-1,-2,-1}};
            bmpOut = Differentiate(bmpIn, Dx, bmp.Width, bmp.Height,  lstPoint);



            return ImageEnhancement.convertArr2Gray(bmpOut,bmp.Height,bmp.Width);

        }
        public static Bitmap Sobel_Ver(Bitmap bmp, List<System.Drawing.Point> lstPoints)
        {
            byte[,] bmpIn = ImageEnhancement.convertBmpGray2Arr(bmp);
            float[,] bmpOut = new float[bmp.Height, bmp.Width];
            int[,] Dx = {{-1,0,1},
                         {-2,0,2},
                         {-1,0,1}};

            int[,] Dy = {{1,2,1},
                         {0,0,0},
                         {-1,-2,-1}};
            bmpOut = Differentiate(bmpIn, Dy, bmp.Width, bmp.Height, lstPoints);

          //  MessageBox.Show("lstCount in Sobel=" + lstPoints.Count);

            return ImageEnhancement.convertArr2Gray(bmpOut, bmp.Height, bmp.Width);

        }
        private static float[,] Differentiate(byte[,] Data, int[,] Filter,int Width, int Height, List<System.Drawing.Point> lstPoint)
        {
           
            int i, j, k, l, Fh, Fw;

            Fw = Filter.GetLength(0);
            Fh = Filter.GetLength(1);
            float sum = 0;
            float[,] Output = new float[Height, Width];

            for (i = Fh / 2; i <= (Height - Fh / 2) - 1; i++)
            {
                for (j = Fw / 2; j <= (Width - Fw / 2) - 1; j++)
                {
                    sum = 0;
                    for (k = -Fh / 2; k <= Fh / 2; k++)
                    {
                        for (l = -Fw / 2; l <= Fw / 2; l++)
                        {
                            sum = sum + Data[i + k, j + l] * Filter[Fh / 2 + k, Fw / 2 + l];


                        }
                    }
                    Output[i, j] = sum;
                    float oldV = Data[i, j];
                    float v = Math.Abs(Data[i, j] - Output[i, j]);
                    if (Math.Abs(sum) >= 10)
                    {
                        Output[i, j] = 255;
                        System.Drawing.Point p = new System.Drawing.Point();
                        p.X = i;
                        p.Y = j;
                        lstPoint.Add(p);

                    }
                    else
                    {
                        Output[i, j] = 0;
                       
                            }




                }

            }
            return Output;

        }
        //////////////

        public static Bitmap medianFilter_List_Gray(Bitmap bmp, List<System.Drawing.Point> lstPoints, int count, int kSize)
        {
            Bitmap bmpTemp = new Bitmap(bmp);
            Bitmap bmpOut = new Bitmap(bmp);
            List<byte> lstKernal;
            byte g;
            int c = 0;
            ///calculating mask coefficents

            while (c <= count)
            {
                foreach (System.Drawing.Point p in lstPoints)
                    if (p.X > kSize / 2 && p.X < bmp.Height - kSize / 2 && p.Y > kSize / 2 && p.Y < bmp.Width - kSize / 2)
                    {

                        lstKernal= new List<byte>();
                        int val = bmpTemp.GetPixel(p.Y, p.X).R;
                       

                        for (int k = -kSize / 2; k <= kSize / 2; k++)
                            for (int l = -kSize / 2; l <= kSize / 2; l++)

                            {

                                lstKernal.Add(bmpTemp.GetPixel(p.Y + l, p.X + k).R);


                            }
                        lstKernal.Sort();
                        g = lstKernal[lstKernal.Count / 2];
                        bmpOut.SetPixel(p.Y, p.X, Color.FromArgb(g, g, g));
                    }
                bmpTemp = new Bitmap(bmpOut);
                lstPoints = new List<System.Drawing.Point>();
                Filters.Sobel_Ver(bmpTemp, lstPoints);
                c++;
            }

            return bmpOut;

        }
    }
}