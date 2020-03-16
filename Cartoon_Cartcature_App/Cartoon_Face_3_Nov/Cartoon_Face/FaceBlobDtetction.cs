
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;

namespace Cartoon_Face
{
    class FaceBlobDtetction
    {
        public struct Rec
        {
           public int maxX, minX, maxY, minY;
           public Color clr;
            public int flag;
        }
        public class IntrestR
        {
            public IntrestR(int w,int h, System.Drawing.Point point)
            {
                arr = new int[h, w];
                p = new System.Drawing.Point(point.X, point.Y);
            }
            public IntrestR(IntrestR intrestR)
            {
                arr = intrestR.arr;
                p = new System.Drawing.Point(intrestR.p.X, intrestR.p.Y);
            }
            public int [,] arr;
            public System.Drawing.Point p;
            
        } 
    
        public static List<Rec> lstRec;
        public static List<IntrestR> lstIntRec;
        static byte[,] arrValues;
        public static int midY;
    
        public  static void MergeBinary2Skin(Bitmap bmpBin,Bitmap skinBmp)
        {
            for(int i=0;i<bmpBin.Height/2;i++)
                for(int j=0;j<bmpBin.Width;j++)
                {if (skinBmp.GetPixel(j, i).R == 255)
                        arrValues[i, j] = 2;
                }
            for (int i = 0; i < bmpBin.Height/2; i++)
                for (int j = 0; j < bmpBin.Width; j++)
                {
                    if (arrValues[i, j] != 2)
                        if (bmpBin.GetPixel(j, i).R == 255)
                            arrValues[i, j] = 0;
                        else
                            arrValues[i, j] = 255;
                }           
        }
       public  static Bitmap DetectDarkBlobs(Bitmap binaryBmp,Bitmap skinBmp, string maindirectory)
        {
            lstRec = new List<Rec>();
            lstIntRec = new List<IntrestR>();
            Bitmap bmpOut = new Bitmap(skinBmp);
            arrValues = new byte[skinBmp.Height / 2, skinBmp.Width];
            MergeBinary2Skin(binaryBmp, skinBmp);
            SeedFiilingBlob s = new SeedFiilingBlob(arrValues, skinBmp.Height / 2, skinBmp.Width);
            Color clr = new Color();
            clr = Color.OrangeRed;
            Color clr2 = new Color();
             clr2 = Color.Green;
            Color clr3 = new Color();
            clr3 = Color.Blue;
           
            Rec rightEye = new Rec();
            Rec leftEye = new Rec();
            rightEye.maxX = skinBmp.Width ;
            rightEye.minX = skinBmp.Width ;
            rightEye.minY = skinBmp.Height / 5;
            rightEye.maxY = skinBmp.Height / 5;
            leftEye.maxX = skinBmp.Width/5;
            leftEye.minX = skinBmp.Width/5;
            leftEye.minY = skinBmp.Height / 5;
            leftEye.maxY = skinBmp.Height / 5;
            rightEye.clr = clr2;
            leftEye.clr = clr3;
            foreach (SeedFiilingBlob.Region r in s.lstRegions)
            {
                Rec rec = new Rec();
                int maxX = r.lstPoints.Max(temp => temp.x);
                rec.maxX = maxX;
                int minX = r.lstPoints.Min(temp => temp.x);
                rec.minX = minX;
                int minY = r.lstPoints.Min(temp => temp.y);
                rec.minY = minY;
                int maxY = r.lstPoints.Max(temp => temp.y);
                rec.maxY = maxY;
                
                Random rnd = new Random();
                Color clr4 = new Color();
                clr4 = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                foreach (SeedFiilingBlob.point p in r.lstPoints)
                {
                    bmpOut.SetPixel(p.y, p.x, clr4);
                }
                if (maxX <= skinBmp.Height / 2 && minX >= skinBmp.Height / 5)
                    if (minY < skinBmp.Width / 2 && maxY < skinBmp.Width / 2 && minY > skinBmp.Width / 5)
                    {    
                        rec.clr = clr2;
                        rec.flag = 0;
                        if (Math.Abs(skinBmp.Width / 2 - leftEye.maxY) + Math.Abs(skinBmp.Height / 2 - leftEye.maxX) >
                                Math.Abs(skinBmp.Width / 2 - rec.maxY) + Math.Abs(skinBmp.Height / 2) - rec.maxX)
                        {     
                            leftEye.maxX = rec.maxX;
                            leftEye.minY = rec.minX;
                            leftEye.maxY = rec.maxY;
                            leftEye.minY = rec.minY;

                        }
                    }
                    else if (minY > skinBmp.Width / 2 && maxY < skinBmp.Width - skinBmp.Width / 5)
                    {
                        rec.clr = clr3;
                        rec.flag = 1;
                        if (Math.Abs(skinBmp.Width / 2 - rightEye.minY) + Math.Abs(skinBmp.Height / 2 - rightEye.maxX) >
                                Math.Abs(skinBmp.Width / 2 - rec.minY) + Math.Abs(skinBmp.Height / 2) - rec.maxX)
                        {
                            rightEye.maxX = rec.maxX;
                            rightEye.minY = rec.minX;
                            rightEye.maxY = rec.maxY;
                            rightEye.minY = rec.minY;
                        }    
                    }
            }
            bmpOut.Save(maindirectory + "//segBlob.jbg");
                List<Rec> lstTemp = new List<Rec>();                 
                    lstTemp = new List<Rec>();
                 
                            int yGabLenghth;
                            yGabLenghth = rightEye.minY-leftEye.maxY;
                            rightEye.minY = rightEye.minY - yGabLenghth / 2;
            midY = rightEye.minY;
                            leftEye.maxY = leftEye.maxY + yGabLenghth / 2;
                            rightEye.maxY = skinBmp.Width - skinBmp.Width / 10;
                            leftEye.minY = skinBmp.Width / 10;
                            int minMinX = Math.Min(leftEye.minX, rightEye.minX);
           
                            int maxMaxX = Math.Max(leftEye.maxX, rightEye.maxX);
                            rightEye.minX = minMinX - (maxMaxX - minMinX) / 3;
            if (rightEye.minX < 0)
                rightEye.minX = skinBmp.Width/2;
                            leftEye.minX= minMinX - (maxMaxX - minMinX) / 3;
            if (leftEye.minX < 0)
                leftEye.minX = 0;
                            rightEye.maxX= maxMaxX + (maxMaxX - minMinX) / 2;
            if (rightEye.maxX >= skinBmp.Height / 2)
                rightEye.maxX = skinBmp.Height / 2;
                leftEye.maxX = maxMaxX + (maxMaxX - minMinX) / 2;
            if (leftEye.maxX >= skinBmp.Height / 2)
                leftEye.maxX = skinBmp.Height / 2;
                     
            IntrestR rIntR = new IntrestR(rightEye.maxY-rightEye.minY,rightEye.maxX-rightEye.minX,new System.Drawing.Point(rightEye.minX,rightEye.minY));
            IntrestR lIntR = new IntrestR(leftEye.maxY - leftEye.minY,  leftEye.maxX - leftEye.minX, new System.Drawing.Point(leftEye.minX, leftEye.minY));
            lstRec =new List<Rec>();
            lstRec.Add(rightEye);
            lstRec.Add(leftEye);
            for (int i = lstRec[0].minX; i < lstRec[0].maxX; i++)
                for (int j = lstRec[0].minY; j < lstRec[0].maxY; j++)
                {
                    bmpOut.SetPixel(j, i, lstRec[0].clr);
                    rIntR.arr[i - rIntR.p.X, j - rIntR.p.Y]=1;
                }
            for (int i = lstRec[1].minX; i < lstRec[1].maxX; i++)
                for (int j = lstRec[1].minY; j < lstRec[1].maxY; j++)
                {
                    bmpOut.SetPixel(j, i, lstRec[1].clr);
                    lIntR.arr[i - lIntR.p.X, j - lIntR.p.Y] = 1;
                }
            lstIntRec.Add(rIntR);
            lstIntRec.Add(lIntR);
            return bmpOut;
        }
      
        }

    }

