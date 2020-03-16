using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows;

namespace Cartoon_KMCG
{
    class ImageEnhancement
    {
       public static Bitmap colorLIPMult(Bitmap bmp)
        {
            Bitmap bmpOut = new Bitmap(bmp.Width, bmp.Height);
            Color[,] clr = new Color[bmp.Height, bmp.Width];
            int min = 100,max = 0;
            for(int i=0; i<bmp.Height;i++)
                for(int j=0; j<bmp.Width;j++)
                {
                    clr[i, j] = bmp.GetPixel(j, i);
                    int minTemp = Math.Min(clr[i, j].R, Math.Min(clr[i, j].B, clr[i, j].G));
                    if (minTemp < min)
                        min = minTemp;
                    int maxTemp = Math.Max(clr[i, j].R, Math.Max(clr[i, j].B, clr[i, j].G));
                    if (maxTemp >max)
                        max = maxTemp;
                }
            //MessageBox.Show("Min= " + min + " Max= " + max);
            double per, dom;
            per = Math.Log(1 - (min+1) / 256.0) / Math.Log(1 - max / 256.0);
            dom = ((1 - max / 256.0) / (1 - min / 256.0));
            double  lamda =Math.Round(Math.Log( Math.Round(per,5)) /Math.Log(dom),3);
         //  MessageBox.Show("Per= " + per + " Dom= " + dom + " Lamda= " + lamda+ "Math.Log(1 - max / 256)= "+ Math.Log(1 - max / 256.0)+ " Math.Log(1 - min / 256)= "+ Math.Log(1.1 - (min+1) / 256.0));
            //lamda = 2;
            for (int i=0;i<bmp.Height;i++)
                for(int j=0; j<bmp.Width; j++)
                {
                    int r, g, b;
                    
                    r=(int)(255 - 255 * Math.Pow((1 - clr[i, j].R / 255.0) , lamda));
                    g = (int)(255 - 255 * Math.Pow((1 - clr[i, j].G / 255.0), lamda));
                    b = (int)(255 - 255 * Math.Pow((1 - clr[i, j].B / 255.0), lamda));
                    bmpOut.SetPixel(j, i, Color.FromArgb(r, g, b));
                }
            return bmpOut;
        }
        public static Bitmap colorLIPMult(Bitmap bmp,byte th)
        {
            Bitmap bmpOut = new Bitmap(bmp.Width, bmp.Height);
            Color[,] clr = new Color[bmp.Height, bmp.Width];
            int min = 100, max = 0;
            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                    clr[i, j] = bmp.GetPixel(j, i);
                    min = th;
                    int maxTemp = Math.Max(clr[i, j].R, Math.Max(clr[i, j].B, clr[i, j].G));
                    if (maxTemp > max)
                        max = maxTemp;
                    if (min > max)
                        min = 0;
                }
            //MessageBox.Show("Min= " + min + " Max= " + max);
            double per, dom;
            per = Math.Log(1 - (min + 1) / 256.0) / Math.Log(1 - max / 256.0);
            dom = ((1 - max / 256.0) / (1 - min / 256.0));
            double lamda = Math.Round(Math.Log(Math.Round(per, 5)) / Math.Log(dom), 3);
            if (max - min > 200)
                lamda = lamda + 2;
            else if (max - min > 150)
                lamda = lamda + 1;
                //  MessageBox.Show("Per= " + per + " Dom= " + dom + " Lamda= " + lamda+ "Math.Log(1 - max / 256)= "+ Math.Log(1 - max / 256.0)+ " Math.Log(1 - min / 256)= "+ Math.Log(1.1 - (min+1) / 256.0));
                //lamda = 2;
                for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                    int r, g, b;
                    if (clr[i, j].R > th && clr[i, j].B > th)
                    {
                        r = (int)(255 - 255 * Math.Pow((1 - clr[i, j].R / 255.0), lamda));
                        g = (int)(255 - 255 * Math.Pow((1 - clr[i, j].G / 255.0), lamda));
                        b = (int)(255 - 255 * Math.Pow((1 - clr[i, j].B / 255.0), lamda));
                    }
                    else 
                            {
                        r = clr[i,j].R;
                        g= clr[i, j].G;
                        b = clr[i, j].B;


                    }
                    bmpOut.SetPixel(j, i, Color.FromArgb(r, g, b));
                }
            return bmpOut;
        }
        public static Bitmap colorLIPMult(Bitmap bmp, double lamda)
        {
            Bitmap bmpOut = new Bitmap(bmp.Width, bmp.Height);
            Color[,] clr = new Color[bmp.Height, bmp.Width];
            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                    clr[i,j]= bmp.GetPixel(j, i);
            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                   
                    int r, g, b;
                    r = (int)(256 - 256 * Math.Pow((1 - clr[i, j].R / 256.0), lamda));
                    g = (int)(256 - 256 * Math.Pow((1 - clr[i, j].G / 256.0), lamda));
                    b = (int)(256 - 256 * Math.Pow((1 - clr[i, j].B / 256.0), lamda));
                    bmpOut.SetPixel(j, i, Color.FromArgb(r, g, b));
                }
            return bmpOut;
        }
        public static Bitmap colorLIPMult_Comp(Bitmap bmp)
        {
            Bitmap bmpOut = new Bitmap(bmp.Width, bmp.Height);
            Color[,] clr = new Color[bmp.Height, bmp.Width];
            int min = 100, max = 0;
            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                    clr[i, j] = bmp.GetPixel(j, i);
                    int minTemp = Math.Min(256-clr[i, j].R-1, 256-Math.Min(clr[i, j].B-1, 256-clr[i, j].G-1));
                    if (minTemp < min)
                        min = minTemp;
                    int maxTemp = Math.Max(256-clr[i, j].R-1,256- Math.Max(clr[i, j].B-1,256- clr[i, j].G-1));
                    if (maxTemp > max)
                        max = maxTemp;
                }
            //MessageBox.Show("Min= " + min + " Max= " + max);
            double per, dom;
            per = Math.Log(1 - (min + 1) / 256.0) / Math.Log(1 - max / 256.0);
            dom = ((1 - max / 256.0) / (1 - min / 256.0));
            double lamda = Math.Round(Math.Log(Math.Round(per, 5)) / Math.Log(dom), 3);
             //   MessageBox.Show("Per= " + per + " Dom= " + dom + " Lamda= " + lamda+ "Math.Log(1 - max / 256)= "+ Math.Log(1 - max / 256.0)+ " Math.Log(1 - min / 256)= "+ Math.Log(1.1 - (min+1) / 256.0));
            //if (lamda > 0.3)  lamda = lamda-0.2;
            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                    int r, g, b;
                    r = (int)(256-(256 - 256 * Math.Pow((1 - (256-clr[i, j].R-1) / 256.0), lamda))-1);
                    g = (int)(256-(256 - 256 * Math.Pow((1 - (256-clr[i, j].G-1) / 256.0), lamda))-1);
                    b = (int)(256-(256 - 256 * Math.Pow((1 - (256-clr[i, j].B-1) / 256.0), lamda))-1);
                   
                    bmpOut.SetPixel(j, i, Color.FromArgb(r, g, b));
                }
            return bmpOut;
        }
        public static Bitmap histogram_drawing(Bitmap bmpIn)
        {
            int[] histogram_r = new int[256];
            int max = 0, min=500;

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
            per =Math.Round( Math.Log(Math.Round((double)(1 - (min%100 + 1) / total)),4) / Math.Log(Math.Round((double)(1 - max%1000 / total),4)),6);
            dom = ((1 - max / total) / (1 - min / total));
            double lamda = Math.Round(Math.Log(Math.Round(per, 5)) / Math.Log(dom), 3);
           
            int histHeight = 128;
            for (int i = 0; i < 256; i++)
            {
               // histogram_r[i]  = (int)(128 - 128 * Math.Pow((1 - histogram_r[i] / max), 9));
                //Console.WriteLine(histogram_r[i]);
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
                      new System.Drawing.Point(img.Width, img.Height - 30 ) // Use that percentage of the height
                       );
                g.DrawLine(Pens.OliveDrab,
                       new System.Drawing.Point(15, img.Height - 30),
                      new System.Drawing.Point(15, 15 ) // Use that percentage of the height
                       );
                g.DrawString("Intensity" ,new Font("Arial", 3, System.Drawing.FontStyle.Bold), Brushes.DarkBlue, 100, img.Height - 15, format);
               // g.DrawString(((int)(i - 10)).ToString(), new Font("Arial", 3, System.Drawing.FontStyle.Bold), Brushes.OliveDrab, i, img.Height - 25, format);
                for (int i = 15; i < histogram_r.Length+10; i++)
                {
                    float pct = (float)(histogram_r[i-15]) / (float)(max);   // What percentage of the max is this value?
                    g.DrawLine(Pens.LightSkyBlue,
                        new System.Drawing.Point(i, img.Height - 30),
                       new System.Drawing.Point(i, img.Height - 30 - (int)(pct * histHeight)) // Use that percentage of the height
                        );
                    if ((i-15) % 50 == 0)
                    {
                       
                       
                        g.DrawLine(Pens.OliveDrab,
                        new System.Drawing.Point((i), img.Height - 30),
                       new System.Drawing.Point((i), img.Height - 28 ) // Use that percentage of the height
                        );
                        g.DrawString(((int)(i-15)).ToString(), new Font("Arial", 3,System.Drawing.FontStyle.Bold), Brushes.OliveDrab,i,img.Height-25,format);
                    }
                }
                GraphicsState state = g.Save();
                g.ResetTransform();

                // Rotate.
                g.RotateTransform(270);

                // Translate to desired position. Be sure to append
                // the rotation so it occurs after the rotation.
                g.TranslateTransform(0, histHeight-20, MatrixOrder.Append);

                // Draw the text at the origin.
                // g.DrawString(txt, the_font, the_brush, 0, 0);
                rectF = new RectangleF(20, 90, 50, 90);
                g.DrawString("Frequency", new Font("Arial", 3, System.Drawing.FontStyle.Bold), Brushes.DarkBlue, 0,0, format);
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
    }
}
