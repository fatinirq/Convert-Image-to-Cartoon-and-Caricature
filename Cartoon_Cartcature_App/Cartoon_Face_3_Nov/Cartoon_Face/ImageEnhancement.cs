using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows;

namespace Cartoon_Face
{
    class ImageEnhancement
    {
        public static Bitmap colorLIPMult(Bitmap bmp)
        {
            Bitmap bmpOut = new Bitmap(bmp.Width, bmp.Height);
            Color[,] clr = new Color[bmp.Height, bmp.Width];
            int min = 100, max = 0;
            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                    clr[i, j] = bmp.GetPixel(j, i);
                    int minTemp = Math.Min(clr[i, j].R, Math.Min(clr[i, j].B, clr[i, j].G));
                    if (minTemp < min)
                        min = minTemp;
                    int maxTemp = Math.Max(clr[i, j].R, Math.Max(clr[i, j].B, clr[i, j].G));
                    if (maxTemp > max)
                        max = maxTemp;
                }
            //MessageBox.Show("Min= " + min + " Max= " + max);
            double per, dom;
            per = Math.Log(1 - (min + 1) / 256.0) / Math.Log(1 - max / 256.0);
            dom = ((1 - max / 256.0) / (1 - min / 256.0));
            double lamda = Math.Round(Math.Log(Math.Round(per, 5)) / Math.Log(dom), 3);
            //    MessageBox.Show("Per= " + per + " Dom= " + dom + " Lamda= " + lamda+ "Math.Log(1 - max / 256)= "+ Math.Log(1 - max / 256.0)+ " Math.Log(1 - min / 256)= "+ Math.Log(1.1 - (min+1) / 256.0));
            //lamda = 2;
            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                    int r, g, b;
                    r = (int)(255 - 255 * Math.Pow((1 - clr[i, j].R / 255.0), lamda));
                    g = (int)(255 - 255 * Math.Pow((1 - clr[i, j].G / 255.0), lamda));
                    b = (int)(255 - 255 * Math.Pow((1 - clr[i, j].B / 255.0), lamda));
                    bmpOut.SetPixel(j, i, Color.FromArgb(r, g, b));
                }
            return bmpOut;
        }
        public static Bitmap grayLIPMult(Bitmap bmp)
        {
            Bitmap bmpOut = new Bitmap(bmp.Width, bmp.Height);
            Color[,] clr = new Color[bmp.Height, bmp.Width];
            int min = 100, max = 0;
            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                    clr[i, j] = bmp.GetPixel(j, i);
                    int minTemp = clr[i, j].R;
                    if (minTemp < min)
                        min = minTemp;
                    int maxTemp = clr[i, j].R;
                    if (maxTemp > max)
                        max = maxTemp;
                }
            //MessageBox.Show("Min= " + min + " Max= " + max);
            double per, dom;
            per = Math.Log(1 - (min + 1) / 256.0) / Math.Log(1 - max / 256.0);
            dom = ((1 - max / 256.0) / (1 - min / 256.0));
            double lamda = Math.Round(Math.Log(Math.Round(per, 5)) / Math.Log(dom), 3);
            //    MessageBox.Show("Per= " + per + " Dom= " + dom + " Lamda= " + lamda+ "Math.Log(1 - max / 256)= "+ Math.Log(1 - max / 256.0)+ " Math.Log(1 - min / 256)= "+ Math.Log(1.1 - (min+1) / 256.0));
            //lamda = 2;
            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                    int r, g, b; 
                    g = (int)(255 - 255 * Math.Pow((1 - clr[i, j].G / 255.0), lamda));                
                    bmpOut.SetPixel(j, i, Color.FromArgb(g, g, g));
                }
            return bmpOut;
        }
        public static Bitmap grayLIPMult(Bitmap bmp,float lamda)
        {
            Bitmap bmpOut = new Bitmap(bmp.Width, bmp.Height);
            //    MessageBox.Show("Per= " + per + " Dom= " + dom + " Lamda= " + lamda+ "Math.Log(1 - max / 256)= "+ Math.Log(1 - max / 256.0)+ " Math.Log(1 - min / 256)= "+ Math.Log(1.1 - (min+1) / 256.0));
            //lamda = 2;
            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                    int r;
                    byte value = bmp.GetPixel(j, i).R;
                    r = (int)(255 - 255 * Math.Pow((1 - value / 255.0), lamda));
                   
                    bmpOut.SetPixel(j, i, Color.FromArgb(r, r, r));
                }
            return bmpOut;
        }
        public static Bitmap histogram_drawing(Bitmap bmpIn)
        {
            int[] histogram_r = new int[256];
            int max = 0, min = 500;

            for (int i = 0; i < bmpIn.Height; i++)
            {
                for (int j = 0; j < bmpIn.Width; j++)
                {

                    Color clr = bmpIn.GetPixel(j, i);
                    byte value = clr.R;

                    histogram_r[value]++;
                    if (max < histogram_r[value])
                        max = histogram_r[value];
                    if (min > histogram_r[value])
                        min = histogram_r[value];
                }
            }
            int total = bmpIn.Height * bmpIn.Width;
            double per, dom;
            per = Math.Round(Math.Log(Math.Round((double)(1 - (min % 100 + 1) / total)), 4) / Math.Log(Math.Round((double)(1 - max % 1000 / total), 4)), 6);
            dom = ((1 - max / total) / (1 - min / total));
            double lamda = Math.Round(Math.Log(Math.Round(per, 5)) / Math.Log(dom), 3);

            int histHeight = 128;
            for (int i = 0; i < 256; i++)
            {
                // histogram_r[i]  = (int)(128 - 128 * Math.Pow((1 - histogram_r[i] / max), 9));
                Console.WriteLine(histogram_r[i]);
            }
            Bitmap img = new Bitmap(300, histHeight + 30);
            for (int i = 0; i < img.Height; i++)
                for (int j = 0; j < img.Width; j++)
                    img.SetPixel(j, i, Color.White);

            using (Graphics g = Graphics.FromImage(img))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // The interpolation mode determines how intermediate values between two endpoints are calculated.
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Use this property to specify either higher quality, slower rendering, or lower quality, faster rendering of the contents of this Graphics object.
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // This one is important
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                RectangleF rectF = new RectangleF(0, img.Height - 1, 40, 10);//Create string formatting options (used for alignment)
                StringFormat format = new StringFormat()
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near

                };
                g.DrawLine(Pens.OliveDrab,
                       new System.Drawing.Point(15, img.Height - 30),
                      new System.Drawing.Point(img.Width, img.Height - 30) // Use that percentage of the height
                       );
                g.DrawLine(Pens.OliveDrab,
                       new System.Drawing.Point(15, img.Height - 30),
                      new System.Drawing.Point(15, 15) // Use that percentage of the height
                       );
                g.DrawString("Intencity", new Font("Arial", 3, System.Drawing.FontStyle.Bold), Brushes.DarkBlue, 100, img.Height - 15, format);
                // g.DrawString(((int)(i - 10)).ToString(), new Font("Arial", 3, System.Drawing.FontStyle.Bold), Brushes.OliveDrab, i, img.Height - 25, format);
                for (int i = 15; i < histogram_r.Length + 10; i++)
                {
                    float pct = (float)(histogram_r[i - 15]) / (float)(max);   // What percentage of the max is this value?
                    g.DrawLine(Pens.LightSkyBlue,
                        new System.Drawing.Point(i, img.Height - 30),
                       new System.Drawing.Point(i, img.Height - 30 - (int)(pct * histHeight)) // Use that percentage of the height
                        );
                    if ((i - 15) % 50 == 0)
                    {


                        g.DrawLine(Pens.OliveDrab,
                        new System.Drawing.Point((i), img.Height - 30),
                       new System.Drawing.Point((i), img.Height - 28) // Use that percentage of the height
                        );
                        g.DrawString(((int)(i - 15)).ToString(), new Font("Arial", 3, System.Drawing.FontStyle.Bold), Brushes.OliveDrab, i, img.Height - 25, format);
                    }
                }
                GraphicsState state = g.Save();
                g.ResetTransform();

                // Rotate.
                g.RotateTransform(270);

                // Translate to desired position. Be sure to append
                // the rotation so it occurs after the rotation.
                g.TranslateTransform(0, histHeight - 20, MatrixOrder.Append);

                // Draw the text at the origin.
                // g.DrawString(txt, the_font, the_brush, 0, 0);
                rectF = new RectangleF(20, 90, 50, 90);
                g.DrawString("Frequency", new Font("Arial", 3, System.Drawing.FontStyle.Bold), Brushes.DarkBlue, 0, 0, format);
                // Restore the graphics state.
                g.Restore(state);

            }


            return img;
        }
        public static Bitmap convert2Gray(Bitmap bmp)
        {
            Bitmap bmpout = new Bitmap(bmp.Width, bmp.Height);
            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color clr = new Color();
                    clr = bmp.GetPixel(j, i);
                    byte g = Convert.ToByte((clr.R + clr.G + clr.B) / 3);
                    bmpout.SetPixel(j, i, Color.FromArgb(g, g, g));
                }
            return bmpout;
        }
        public static byte[,] convert2GrayArr(Bitmap bmp)
        {
            byte[,] bmpout = new byte[bmp.Height, bmp.Width];
            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color clr = new Color();
                    clr = bmp.GetPixel(j, i);
                    byte g = Convert.ToByte((clr.R + clr.G + clr.B) / 3);
                    bmpout[i, j] = g;
                }
            return bmpout;
        }
        public static byte[,] convertBmpGray2Arr(Bitmap bmp)
        {
            byte[,] bmpout = new byte[bmp.Height, bmp.Width];
            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color clr = new Color();
                    clr = bmp.GetPixel(j, i);
                    bmpout[i, j] = clr.R;
                }
            return bmpout;
        }
        public static Bitmap convertArr2Gray(byte[,] arr, int h, int w)
        {
            Bitmap bmpout = new Bitmap(w, h);
            for (int i = 0; i < bmpout.Height; i++)
                for (int j = 0; j < bmpout.Width; j++)
                {
                    byte g = arr[i, j];
                    bmpout.SetPixel(j, i, Color.FromArgb(g, g, g));
                }
            return bmpout;
        }
        public static Bitmap convertArr2Gray(float[,] arr, int h, int w)
        {
            Bitmap bmpout = new Bitmap(w, h);
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                    float g = arr[i, j];
                    bmpout.SetPixel(j, i, Color.FromArgb((int)g,(int) g,(int) g));
                }
            return bmpout;
        }

        public float Max, Min, Mean, STD;
        public void ImageStreching1(int wid, int hgt, byte[,] I, byte[,] SI)
        {
            try
            {
                int X, Y;
                //float Sum = 0.0f, Diff = 0.0f;

                Min = I[0, 0];
                Max = I[0, 0];
                for (X = 0; X < wid; X++)
                {
                    for (Y = 0; Y < hgt; Y++)
                    {

                        if (Max < I[X, Y])
                            Max = I[X, Y];
                        if (Min > I[X, Y])
                            Min = I[X, Y];

                    }
                }

                for (X = 0; X < wid; X++)
                {
                    for (Y = 0; Y < hgt; Y++)
                    {

                        SI[X, Y] = (byte)(255.0 * ((I[X, Y] - Min) / (float)(Max - Min)));

                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Can not Stretch the Histogram of the Image \n" + err.Message);
            }

        }
        public void ImageStreching(int wid, int hgt, byte[,] NI, ref byte[,] SI, float Alpha, float Gamma)
        {
            try
            {
                int X, Y;
                float Sum = 0.0f, Diff1 = 0.0f;


                for (X = 0; X < wid; X++)
                {
                    for (Y = 0; Y < hgt; Y++)
                    {

                        Sum += NI[X, Y];
                    }
                }
                Mean = Sum / (float)(wid * hgt);

                for (X = 0; X < wid; X++)
                {
                    for (Y = 0; Y < hgt; Y++)
                    {
                        Diff1 = Diff1 + (float)(Math.Pow((NI[X, Y] - Mean), 2));

                    }
                }
                STD = (float)(Math.Sqrt(Diff1 / (float)(wid * hgt)));


                Min = Mean - (Alpha * STD);
                Max = Mean + (Alpha * STD);

                if (Min < 0)
                    Min = 0;
                if (Max > 255)
                    Max = 255;
                byte[] lookup = new byte[256];
                for (int i = 0; i < 256; i++)
                {
                    if (i <= Min)
                        lookup[i] = 0;
                    else if (i >= Max)
                        lookup[i] = 255;
                    else
                        lookup[i] = (byte)(255.0 * Math.Pow(((i - Min) / (float)(Max - Min)), Gamma));
                }
                for (X = 0; X < wid; X++)
                {
                    for (Y = 0; Y < hgt; Y++)
                    {
                        SI[X, Y] = lookup[NI[X, Y]];
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Can not Stretch the Histogram of the Image \n" + err.Message);
            }
        }

        public void LinearStreching(int wid, int hgt, byte[,] NI, ref byte[,] SI, float Alpha)
        {
            try
            {
                int X, Y;
                float Sum = 0.0f, Diff1 = 0.0f;


                for (X = 0; X < hgt; X++)
                {
                    for (Y = 0; Y < wid; Y++)
                    {

                        Sum += NI[X, Y];
                    }
                }
                Mean = Sum / (float)(wid * hgt);

                for (X = 0; X < hgt; X++)
                {
                    for (Y = 0; Y < wid; Y++)
                    {
                        Diff1 = Diff1 + (float)(Math.Pow((NI[X, Y] - Mean), 2));

                    }
                }
                STD = (float)(Math.Sqrt(Diff1 / (float)(wid * hgt)));


                Min = 0;

                Max = Mean + (Alpha * STD);


                if (Min < 0)
                    Min = 0;
                if (Max > 255)
                    Max = 255;
                byte[] lookup = new byte[256];
                for (int i = 0; i < 256; i++)
                {
                    if (i <= Min)
                        lookup[i] = 0;
                    else if (i >= Max)
                        lookup[i] = 255;
                    else
                        lookup[i] = (byte)(255.0 * ((i - Min) / (float)(Max - Min)));
                }
                for (X = 0; X < hgt; X++)
                {
                    for (Y = 0; Y < wid; Y++)
                    {
                        SI[X, Y] = lookup[NI[X, Y]];
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Can not Stretch the Histogram of the Image \n" + err.Message);
            }
        }
        public void Local_ContrastStreching(int wid, int hgt, byte[,] NI, ref byte[,] SI, float Alpha, float Gamma, int bs)
        {
            try
            {
                // int bs = 40;
                int nbw = wid / bs;
                int nbh = hgt / bs;
                float Sum = 0.0f, Diff = 0.0f;


                for (int i = 0; i < nbh; i++)
                {
                    for (int j = 0; j < nbw; j++)
                    {
                        Sum = 0;
                        for (int ii = 0; ii < bs; ii++)
                        {
                            for (int jj = 0; jj < bs; jj++)
                            {
                                int x = ii + bs * i;
                                int y = jj + bs * j;
                                Sum += NI[x, y];


                            }
                        }
                        Mean = Sum / (float)(bs * bs);
                        Diff = 0;
                        for (int ii = 0; ii < bs; ii++)
                        {
                            for (int jj = 0; jj < bs; jj++)
                            {
                                int x = ii + bs * i;
                                int y = jj + bs * j;
                                Diff = Diff + ((NI[x, y] - Mean) * (NI[x, y] - Mean));


                            }
                        }

                        STD = (float)(Math.Sqrt(Diff / (float)(bs * bs)));
                        Min = Mean - (Alpha * STD);
                        Max = Mean + (Alpha * STD);

                        if (Min < 0)
                            Min = 0;
                        if (Max > 255)
                            Max = 255;
                        for (int ii = 0; ii < bs; ii++)
                        {
                            for (int jj = 0; jj < bs; jj++)
                            {
                                int X = ii + bs * i;
                                int Y = jj + bs * j;
                                if (NI[X, Y] <= Min)
                                    SI[X, Y] = 0;
                                else if (NI[X, Y] >= Max)
                                    SI[X, Y] = 255;
                                else
                                    SI[X, Y] = (byte)(255.0 * Math.Pow(((NI[X, Y] - Min) / (float)(Max - Min)), Gamma));
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Can not Stretch the Histogram of the Image \n" + err.Message);
            }
        }
        struct strLogPixel
        {
            public int i;
            public int j;
            public double log;
            public double av;
            public double Value;
        }
        public static Bitmap modifiedLee(Bitmap bmp, double alpha, double beta, int n)
        {
            Bitmap bmpOut = new Bitmap(bmp);
            List<strLogPixel> lstLog = new List<strLogPixel>();
            for (int i = n / 2; i < bmp.Height - n / 2; i++)
                for (int j = n / 2; j < bmp.Width - n / 2; j++)
                {
                    byte clr = bmp.GetPixel(j, i).R;
                    strLogPixel strLog = new strLogPixel();
                    strLog.i = i;
                    strLog.j = j;
                    // double log = -256 * Math.Log(1 - ((clr.R + clr.B + clr.G) / 3 + 1));
                    // strLog.log=  -255*Math.Log(1-(double)((clr.R+clr.G+clr.B)/3)/255);
                    strLog.log = 1 - clr / 256.0;
                    double sum = 0;

                    for (int k = -n / 2; k <= n / 2; k++)
                        for (int l = -n / 2; l <= n / 2; l++)
                        {
                            byte clrTemp = bmp.GetPixel(j + k, i + l).R;
                            sum = sum + (1 - clrTemp / 256.0);

                        }
                    //double av = sum / (n * n);
                    strLog.av = sum / (n * n*1.0);
                    //strLog.log = Math.Round( 256 - 256 * Math.Pow((1 - (double)((clr.R + clr.G + clr.B) / 3)/256),2),3);
                   
                    lstLog.Add(strLog);
                }
            foreach (strLogPixel temp in lstLog)
            {

                double v = -256 * Math.Round(alpha * Math.Log(temp.av) + beta * Math.Log(temp.log - temp.av),5);

                byte vInv = (byte)Math.Truncate((256 * (1 - Math.Exp(-v / 256.0))));


                bmpOut.SetPixel(temp.j, temp.i, Color.FromArgb(255, vInv, vInv, vInv));
            }
            return bmpOut;
        }
        public static Bitmap Lee(Bitmap bmp, double alpha, double beta, int n)
        {
            Bitmap bmpOut = new Bitmap(bmp);
            List<strLogPixel> lstLog = new List<strLogPixel>();
            for (int i = n / 2; i < bmp.Height - n / 2; i++)
                for (int j = n / 2; j < bmp.Width - n / 2; j++)
                {
                    byte clr = bmp.GetPixel(j, i).R;
                    strLogPixel strLog = new strLogPixel();
                    strLog.i = i;
                    strLog.j = j;
                    // double log = -256 * Math.Log(1 - ((clr.R + clr.B + clr.G) / 3 + 1));
                    // strLog.log=  -255*Math.Log(1-(double)((clr.R+clr.G+clr.B)/3)/255);
                    strLog.log = clr ;
                    double sum = 0;

                    for (int k = -n / 2; k <= n / 2; k++)
                        for (int l = -n / 2; l <= n / 2; l++)
                        {
                            byte clrTemp = bmp.GetPixel(j + k, i + l).R;
                            sum = sum + clrTemp-(sum* clrTemp / 256.0);
                         

                        }
                    //double av = sum / (n * n);
                    strLog.av = sum / (n * n * 1.0);
                    //strLog.log = Math.Round( 256 - 256 * Math.Pow((1 - (double)((clr.R + clr.G + clr.B) / 3)/256),2),3);
                    double mult = Math.Round(256 - 256 * Math.Pow(1 - strLog.av / 256, alpha));
                    double sub = Math.Round(256 * ((strLog.log - strLog.av) / (256 - strLog.av)));
                    double multSub = Math.Round(256 - 256 * Math.Pow(1 - sub / 256, beta));
                    double result = Math.Truncate(mult + multSub - (multSub * mult / 256));
                    strLog.Value = result;
                    lstLog.Add(strLog);
                }
            
            foreach (strLogPixel temp in lstLog)
            {
               
                byte val = (byte)temp.Value;

                bmpOut.SetPixel(temp.j, temp.i, Color.FromArgb(255, val, val, val));
            }
            return bmpOut;
        }
        public static Bitmap imgLog2(Bitmap bmp, double alpha, double beta, int n)
        {
            Bitmap bmpOut = new Bitmap(bmp);
            List<strLogPixel> lstLog = new List<strLogPixel>();
            for (int i = n/2; i < bmp.Height - n/2 ; i++)
                for (int j = n/2; j < bmp.Width - n/2; j++)
                {
                    byte clr = bmp.GetPixel(j, i).R;
                    strLogPixel strLog = new strLogPixel();
                    strLog.i = i;
                    strLog.j = j;
                    // double log = -256 * Math.Log(1 - ((clr.R + clr.B + clr.G) / 3 + 1));
                    // strLog.log=  -255*Math.Log(1-(double)((clr.R+clr.G+clr.B)/3)/255);
                    strLog.log = clr;
                    int sum = 0;

                    for (int k = -n/2; k <= n/2 ; k++)
                        for (int l = -n/2; l <=n/2; l++)
                        {
                            byte clrTemp = bmp.GetPixel(j+k, i+l).R;
                            sum = sum + clrTemp;

                        }
                    strLog.av = sum / (n * n);
                    //strLog.log = Math.Round( 256 - 256 * Math.Pow((1 - (double)((clr.R + clr.G + clr.B) / 3)/256),2),3);
                    lstLog.Add(strLog);
                }
                    foreach (strLogPixel temp in lstLog)
                    {
                        double av = Math.Round(-256 * Math.Log(1 - Math.Truncate(temp.av / 256)), 3);
                        double vLn = Math.Round(-256 * Math.Log(1 - temp.log / 256), 3);

                        double v = Math.Round(alpha * +beta * (vLn - av), 3);

                        byte vInv = (byte)Math.Truncate((256 * (1 - Math.Exp(-v / 256))));


                        bmpOut.SetPixel(temp.j, temp.i, Color.FromArgb(255, vInv, vInv, vInv));
                    }
                    return bmpOut;
                }
        public static Bitmap imgLog2(Bitmap bmp,List<System.Drawing.Point> lstPoints, double alpha, double beta, int n)
        {
            Bitmap bmpOut = new Bitmap(bmp);
            List<strLogPixel> lstLog = new List<strLogPixel>();
            foreach(System.Drawing.Point p in lstPoints)
                if(p.X>n/2 && p.Y>n/2&& p.X<bmp.Height-n/2 && p.Y<bmp.Width-n/2)
                {
                   int i = p.X;int j = p.Y;
                    byte clr = bmp.GetPixel(j, i).R;
                    strLogPixel strLog = new strLogPixel();
                    strLog.i = i;
                    strLog.j = j;
                    // double log = -256 * Math.Log(1 - ((clr.R + clr.B + clr.G) / 3 + 1));
                    // strLog.log=  -255*Math.Log(1-(double)((clr.R+clr.G+clr.B)/3)/255);
                    strLog.log = clr;
                    int sum = 0;

                    for (int k = -n / 2; k <= n / 2; k++)
                        for (int l = -n / 2; l <= n / 2; l++)
                        {
                            byte clrTemp = bmp.GetPixel(j + k, i + l).R;
                            sum = sum + clrTemp;

                        }
                    strLog.av = sum / (n * n);
                    //strLog.log = Math.Round( 256 - 256 * Math.Pow((1 - (double)((clr.R + clr.G + clr.B) / 3)/256),2),3);
                    lstLog.Add(strLog);
                }
            foreach (strLogPixel temp in lstLog)
            {
                double av = Math.Round(-256 * Math.Log(1 - Math.Truncate(temp.av / 256)), 3);
                double vLn = Math.Round(-256 * Math.Log(1 - temp.log / 256), 3);

                double v = Math.Round(alpha * +beta * (vLn - av), 3);

                byte vInv = (byte)Math.Truncate((256 * (1 - Math.Exp(-v / 256))));


                bmpOut.SetPixel(temp.j, temp.i, Color.FromArgb(255, vInv, vInv, vInv));
            }

            Bitmap bmpTemp=Filters.meanFilter_List_Gray(bmpOut, lstPoints, 1, 3);
            return bmpTemp;
        }
        public void Histogram_Equalization(int wid, int hgt, byte[,] I, ref byte[,] EI, int OPI, int NPI)
        {
            //Compute discrete PDF (histogram) of the image 
            int OIR = (int)Math.Pow(2, OPI);
            int[] His = new int[OIR];
            Histogram_Cal(wid, hgt, I, ref His);

            //Compute CDF (cumulative histogram) of the image
            float[] CDF = new float[OIR];
            CDF[0] = His[0];
            for (int i = 1; i < OIR; i++)
            {
                CDF[i] = CDF[i - 1] + His[i];
            }
            //Normalize CDF 

            for (int i = 0; i < OIR; i++)
            {
                CDF[i] = CDF[i] / (float)(wid * hgt);
            }
            int NIR = (int)Math.Pow(2, NPI);
            //Remapping pixels using histogram equalization
            for (int i = 0; i < hgt; i++)
            {
                for (int j = 0; j < wid; j++)
                {
                    EI[i, j] = (byte)Math.Round((NIR - 1) * CDF[I[i, j]], 0);

                }
            }
        }
        public void Histogram_Equalization(int wid, int hgt, byte[,] I, ref byte[,] EI, int OPI, int NPI,List<System.Drawing.Point> lstPoints)
        {
            //Compute discrete PDF (histogram) of the image 
            int OIR = (int)Math.Pow(2, OPI);
            int[] His = new int[OIR];
            Histogram_Cal( I, ref His,lstPoints);
            List<System.Drawing.Point> lstTempPoints = new List<System.Drawing.Point>();
            //Compute CDF (cumulative histogram) of the image
            float[] CDF = new float[OIR];
            CDF[0] = His[0];
            for (int i = 1; i < OIR; i++)
            {
                CDF[i] = CDF[i - 1] + His[i];
            }
            //Normalize CDF 

            for (int i = 0; i < OIR; i++)
            {
                CDF[i] = CDF[i] / (float)(wid * hgt);
            }
            int NIR = (int)Math.Pow(2, NPI);
            //Remapping pixels using histogram equalization
            foreach (System.Drawing.Point p in lstPoints)
            {
                EI[p.X, p.Y] = (byte)Math.Round((NIR - 1) * CDF[I[p.X, p.Y]], 0);
            }    
            }
        
        public void Gamma_Correction(int wid, int hgt, byte[,] I, ref byte[,] CI, float Gamma)
        {
            byte[] lookup = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                lookup[i] = (byte)(Math.Pow((i / 255f), Gamma) * 255);
            }
            for (int i = 0; i < wid; i++)
            {
                for (int j = 0; j < hgt; j++)
                {
                    CI[i, j] = lookup[I[i, j]];

                }
            }

        }
        public void Histogram_Cal(int wid, int hgt, byte[,] I, ref int[] Histogram)
        {
            for (int i = 0; i < hgt; i++)
            {
                for (int j = 0; j < wid; j++)
                {
                    Histogram[I[i, j]]++;

                }
            }
        }
        public void Histogram_Cal(byte[,] I, ref int[] Histogram, List<System.Drawing.Point> lstPoints)
        {
            foreach (System.Drawing.Point p in lstPoints)
                Histogram[I[p.X, p.Y]]++;
        }

   
public static byte[,] IlluminationCompens(byte[,] inBmp, int w, int h, int th)
        {
            int i, j;
            byte[,] bmpOut=new byte[h,w];
            float Sum = 0.0f, Diff1 = 0.0f;


            for ( i = 0; i < h; i++)
            {
                for (j = 0; j < w; j++)
                {

                    Sum += inBmp[i, j];
                }
            }
            float Mean = Sum / (float)(w * h);

            for (i = 0; i < h; i++)
            {
                for (j = 0; j < w; j++)
                {
                    Diff1 = Diff1 + (float)(Math.Pow((inBmp[i, j] - Mean), 2));

                }
            }
            float k;
            float STD = (float)(Math.Sqrt(Diff1 / (float)(w * h)));
            for (i = 0; i < h; i++)
                for ( j = 0; j < w; j++)
                {
                    k = (th / STD) * (inBmp[i, j] - Mean) + 128;
                    if (k < 0)
                        k = 0;
                    else if (k > 255)
                        k = 255;
                    bmpOut[i, j] = (byte)k;
                }
            return bmpOut;

        }
    }
}
