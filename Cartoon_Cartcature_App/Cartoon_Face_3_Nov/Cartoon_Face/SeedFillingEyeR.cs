using System;
using System.Collections.Generic;
using System.Drawing;


namespace Cartoon_Face
{
    class SeedFillingEyeR
    {
       public  Regions regions;
        Bitmap eyeBmp;
       public string path;
        public SeedFillingEyeR(Bitmap bmp, FaceBlobDtetction.IntrestR interestR, string mainPath)
        {
            path=mainPath;
             eyeBmp = new Bitmap(interestR.arr.GetLength(1)+1, interestR.arr.GetLength(0)+1);
            for (int i=interestR.p.X; i<interestR.p.X+eyeBmp.Height;i++)
                for(int j= interestR.p.Y;j< interestR.p.Y + eyeBmp.Width;j++)
                {
                    Color clr = bmp.GetPixel(j, i);
                    eyeBmp.SetPixel(j - interestR.p.Y, i - interestR.p.X,clr);
                }
            eyeBmp.Save(path + "//eyeBmp.jpg");
            regions = new Regions(eyeBmp,interestR,mainPath);
            DetectEyeRegion();       
            DetectEyeSeg();
            FilterRegions();

        }
           public class Region{
           public List<Point> lstPoints = new List<Point>();
           public Color clr=new Color();
           public int regionF;
           public int regionS;
           public int miny= 1000, maxy=0, minx=1000, maxx=0;           
            }

       public  class Regions
        {
           public  Regions(Bitmap eyeBmp, FaceBlobDtetction.IntrestR intrestRTEmp,string mainpath)
            {
                intrestR=new FaceBlobDtetction.IntrestR(intrestRTEmp);
                eyeBmpEnhanced = new Bitmap(ImageEnhancement.grayLIPMult(PreProc.hisEqua(eyeBmp)));
                eyeBmpEnhanced.Save(mainpath + "//eyeBmpEnhanced.jpg");
                eyeBmpBinary = new Bitmap(PreProc.binary_Bmp(120, eyeBmpEnhanced));
                eyeBmpBinary.Save(mainpath + "//eyeBmpBinary.jpg");
            }
           public FaceBlobDtetction.IntrestR intrestR;
           public Bitmap eyeBmpEnhanced;
           public Bitmap eyeBmpBinary;
           public List<Region> lstRegions = new List<Region>();
           public List<Region> lstNonRegion = new List<Region>();
           public Bitmap bmpRegions;         
        }
        public void DetectEyeRegion()
        {
            Random rRan = new Random(50);
            Random gRand = new Random(10);
            Random bRand = new Random(26);
            int red, green, blue;
            int h = regions.eyeBmpBinary.Height;
            int w = regions.eyeBmpBinary.Width;
            byte[,] arr = ImageEnhancement.convert2GrayArr(regions.eyeBmpBinary);
            int[,] arrVisited = new int[h, w];
            int Seq = 1;
           
            Region nonRegion = new Region();
            for (int i = h / 3; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                    if (arrVisited[i, j] != -1 && arr[i, j] == 0)
                    {
                        arrVisited[i, j] = -1;
                        List<Point> lstPoints = new List<Point>();
                        lstPoints.Add(new Point(i, j));
                        List<Point> lstSeeds = new List<Point>();
                        lstSeeds.Add(new Point(i, j));
                        seedFilling(arrVisited, arr, lstSeeds, lstPoints, h, w);
                        Region r = new Region();
                        r.lstPoints = new List<Point>(lstPoints);
                        r.regionF = 0;
                        r.regionS = Seq;
                        Seq++;
                        red = rRan.Next(0, 255);
                        green = rRan.Next(0, 255);
                        blue = rRan.Next(0, 255);
                        r.clr = Color.FromArgb(red, green, blue);
                        if(r.lstPoints.Count/(eyeBmp.Width*eyeBmp.Height* 1.0)>=0.01)
                        regions.lstRegions.Add(r);
                    }
                    else if (arrVisited[i, j] != -1 && arr[i, j] == 255 && j < w && i < h)
                    {
                        arrVisited[i, j] = -1;
                        nonRegion.lstPoints.Add(new Point(j, i));
                    }
                }
            red = rRan.Next(0, 255);
            green = rRan.Next(0, 255);
            blue = rRan.Next(0, 255);
            nonRegion.clr = Color.FromArgb(red, green, blue);
           // regions.lstRegions.Add(nonRegion);
            regions.lstNonRegion.Add(nonRegion);
        }
        public void seedFilling(int[,] arrVisited, byte[,] arrBinary, List<Point> lstSeeds, List<Point> lstPoints, int h, int w)
        {   
            do
            {
                List<Point> lstSeedsTemp = new List<Point>();
                foreach (Point p in lstSeeds)
                {
                    if (p.X+1 < h && arrVisited[p.X+1,p.Y] != -1
                        && arrBinary[p.X+1,p.Y] == 0)

                    {

                        arrVisited[p.X+1, p.Y] = -1;
                        lstSeedsTemp.Add(new Point(p.X+1, p.Y));
                        lstPoints.Add(new Point(p.X + 1, p.Y));
                    }
                    if (p.X - 1 > h/4 && arrVisited[p.X - 1, p.Y] != -1
                        && arrBinary[p.X - 1, p.Y] == 0)

                    {
                        arrVisited[p.X - 1, p.Y] = -1;
                        lstSeedsTemp.Add(new Point(p.X - 1, p.Y));
                        lstPoints.Add(new Point(p.X - 1, p.Y));
                    }
                    if (p.Y + 1 < w && arrVisited[p.X , p.Y+1] != -1
                        && arrBinary[p.X, p.Y+1] == 0)

                    {
                        arrVisited[p.X , p.Y+1] = -1;
                        lstSeedsTemp.Add(new Point(p.X, p.Y+1));
                        lstPoints.Add(new Point(p.X, p.Y + 1));

                    }
                    if (p.Y - 1 > 0 && arrVisited[p.X, p.Y - 1] != -1
                        && arrBinary[p.X, p.Y - 1] == 0)

                    {
                        arrVisited[p.X, p.Y - 1] = -1;
                        lstSeedsTemp.Add(new Point(p.X, p.Y - 1));
                        
                        lstPoints.Add(new Point(p.X, p.Y - 1));
                    }

                    // buffer[i.x, i.y].gray = -1;

                }
                lstSeeds = new List<Point>(lstSeedsTemp);
            }
            while (lstSeeds.Count > 0);
        }
        public void DetectEyeSeg()
        {
            List<Point> lstPoints = new List<Point>();
            foreach (Region r in regions.lstRegions)
            lstPoints.AddRange(r.lstPoints); 
            regions.eyeBmpEnhanced = new Bitmap(PreProc.hisEqua(ImageEnhancement.grayLIPMult(eyeBmp),lstPoints));
            regions.bmpRegions = new Bitmap(regions.eyeBmpEnhanced.Width, regions.eyeBmpEnhanced.Height);
            foreach (Region r in regions.lstRegions)

                foreach (Point p in r.lstPoints)
                {
                    if (p.X > r.maxx)
                        r.maxx = p.X;
                    if (p.X < r.minx)
                        r.minx = p.X;
                    if (p.Y > r.maxy)
                        r.maxy = p.Y;
                    if (p.Y < r.miny)
                        r.miny = p.Y;
                    regions.bmpRegions.SetPixel(p.Y, p.X, r.clr);
                }
            
            regions.bmpRegions.Save(path + "//bmpEyeRegions1.jpg");
         //   foreach (Region r in regions.lstNonRegion)
           //     foreach (Point p in r.lstPoints)
             //       regions.bmpRegions.SetPixel(p.Y, p.X, r.clr);

            lstPoints = new List<Point>();
           // Bitmap BinaryEnhanced = new Bitmap(PreProc.binary_Bmp(40, regions.eyeBmpEnhanced));
            lstPoints = new List<Point>();
           // foreach (Region r in regions.lstRegions)
             //   foreach (Point p in r.lstPoints)
               //     if (BinaryEnhanced.GetPixel(p.Y, p.X).R == 255)
                 //       lstPoints.Add(p);

        }
      public void  FilterRegions()
        {
            
            for (int i = 0; i < regions.lstRegions.Count; i++)
                
                if (regions.lstRegions[i].minx == 0  || regions.lstRegions[i].miny==0)
                {
                    regions.lstRegions.RemoveAt(i);
                    i = -1;
                }
            
            
            regions.bmpRegions = new Bitmap(eyeBmp.Width, eyeBmp.Height);
            foreach (Region r in regions.lstRegions)

                foreach (Point p in r.lstPoints)
                {
                    
                    regions.bmpRegions.SetPixel(p.Y, p.X, r.clr);
                }
            regions.bmpRegions.Save(path + "//bmpEyeRegionsFiltered.jpg");

        }
    }
}
