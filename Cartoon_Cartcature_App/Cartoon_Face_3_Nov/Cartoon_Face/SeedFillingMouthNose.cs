using System;
using System.Collections.Generic;
using System.Drawing;

namespace Cartoon_Face
{
        class SeedFillingMouthNose
        {
            public Regions regions;
            public Point p1Nose, p2Nose, p1NoseROI, p2NoseROI, p1Mouth, p2Mouth, p1MouthROI, p2MouthROI;
            public Bitmap MouthAndNoseBmp;
            public static string path;
            int h,w,y;          
            public SeedFillingMouthNose(Bitmap bmp, int midY, string directory)
            {
            y = midY;
            path = directory;
            MouthAndNoseBmp = new Bitmap(bmp.Width,bmp.Height/2+1);
            MouthAndNoseBmp.Save(path + "//MandN.jpg");
          //  System.Windows.MessageBox.Show("midY=" + midY);
                for (int i = bmp.Width/2; i <bmp.Width; i++)
                    for (int j=0; j < bmp.Height; j++)
                    {
                        Color clr = bmp.GetPixel(j, i);
                        MouthAndNoseBmp.SetPixel(j , i-bmp.Width/2 , clr);
                    }
                regions = new Regions(MouthAndNoseBmp);
                DetectMouthNoseRegion();
                FilterRegions(1000,0,1000,0);
            DetectROI();
            }
            public class Region
            {
                public List<Point> lstPoints = new List<Point>();
                public Color clr = new Color();
                
                public int miny = 1000, maxy = 0, minx = 1000, maxx = 0;
            }
            public class Regions
            {
                public Regions(Bitmap MAndNBmp)
                {
                Bitmap GaussMandN = new Bitmap(Filters.gaussianFilter(MAndNBmp, 2));
                GaussMandN.Save(path + "\\GaussMandN.jpg");
                Bitmap HistogramEqu = new Bitmap(PreProc.hisEqua(GaussMandN));
                HistogramEqu.Save(path + "\\MandN_HistEqu.jpg");
                MouthAndNoseBmpEnhanced = new Bitmap(ImageEnhancement.grayLIPMult(HistogramEqu));
                MouthAndNoseBmpEnhanced.Save(path + "\\MandN_Lip.jpg");
                MouthAndNoseBmpBinary = new Bitmap(PreProc.binary_Bmp(128, MouthAndNoseBmpEnhanced));
                MouthAndNoseBmpBinary.Save(path + "\\MandN_Bin.jpg");
                }
                            
                public Bitmap MouthAndNoseBmpEnhanced;
                public Bitmap MouthAndNoseBmpBinary;
                public List<Region> lstRegions = new List<Region>();
                public List<Region> lstNonRegion = new List<Region>();
                public Bitmap bmpRegions;
            }
   
        public void DetectMouthNoseRegion()
        {
            Random rRan = new Random(50);
            Random gRand = new Random(10);
            Random bRand = new Random(26);
            int red, green, blue;
            h = regions.MouthAndNoseBmpBinary.Height;
            w = regions.MouthAndNoseBmpBinary.Width;  
            byte[,] arr = ImageEnhancement.convert2GrayArr(regions.MouthAndNoseBmpBinary);
            int[,] arrVisited = new int[h, w];
            int Seq = 1;
            Region nonRegion = new Region();
            List<Point> lstPoints;
            for (int i =0; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                    if (arrVisited[i, j] != -1 && arr[i, j] == 0)
                    {
                        arrVisited[i, j] = -1;
                        lstPoints = new List<Point>();
                        lstPoints.Add(new Point(i, j));
                        List<Point> lstSeeds = new List<Point>();
                        lstSeeds.Add(new Point(i, j));
                        seedFilling(arrVisited, arr, lstSeeds, lstPoints, h, w);
                        Region r = new Region();
                        r.lstPoints = new List<Point>(lstPoints);
                     
                        
                        red = rRan.Next(0, 255);
                        green = rRan.Next(0, 255);
                        blue = rRan.Next(0, 255);
                        r.clr = Color.FromArgb(red, green, blue);
                        if (r.lstPoints.Count / (MouthAndNoseBmp.Width * MouthAndNoseBmp.Height * 1.0) >= 0.01)
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
            regions.lstNonRegion.Add(nonRegion);
            regions.bmpRegions = new Bitmap(regions.MouthAndNoseBmpEnhanced.Width, regions.MouthAndNoseBmpEnhanced.Height);
            lstPoints = new List<Point>();
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
            lstPoints = new List<Point>();
            regions.bmpRegions.Save(path + "\\regions.jpg");
        }
        public void DetectMouthNoseRegion2nd(Point p1,Point p2, Bitmap MouthAndNoseBmpBinary2)
        {
            Random rRan = new Random(50);
            Random gRand = new Random(10);
            Random bRand = new Random(26);
            int red, green, blue;
            h = MouthAndNoseBmpBinary2.Height;
            w = MouthAndNoseBmpBinary2.Width;
            byte[,] arr = ImageEnhancement.convert2GrayArr(MouthAndNoseBmpBinary2);
            int[,] arrVisited = new int[h, w];
            int Seq = 1;
            Region nonRegion = new Region();
            List<Point> lstPoints;
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                    if (arrVisited[i, j] != -1 && arr[i, j] == 0)
                    {
                        arrVisited[i, j] = -1;
                        lstPoints = new List<Point>();
                        lstPoints.Add(new Point(i+p1.X, j+p1.Y));
                        List<Point> lstSeeds = new List<Point>();
                        lstSeeds.Add(new Point(i, j));
                        seedFilling(arrVisited, arr, lstSeeds, lstPoints,p1,p2,h,w);
                        Region r = new Region();
                        r.lstPoints = new List<Point>(lstPoints);
                                              
                        red = rRan.Next(0, 255);
                        green = rRan.Next(0, 255);
                        blue = rRan.Next(0, 255);
                        r.clr = Color.FromArgb(red, green, blue);
                        if (r.lstPoints.Count / (MouthAndNoseBmp.Width * MouthAndNoseBmp.Height * 1.0) >= 0.01)
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
            regions.lstNonRegion.Add(nonRegion);
            regions.bmpRegions = new Bitmap(regions.MouthAndNoseBmpEnhanced.Width, regions.MouthAndNoseBmpEnhanced.Height);
            lstPoints = new List<Point>();
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
            lstPoints = new List<Point>();
            regions.bmpRegions.Save(path + "\\regions.jpg");
        }
        public void seedFilling(int[,] arrVisited, byte[,] arrBinary, List<Point> lstSeeds, List<Point> lstPoints, int h, int w)
            {
                do
                {
                    List<Point> lstSeedsTemp = new List<Point>();
                    foreach (Point p in lstSeeds)
                    {
                        if (p.X + 1 < h && arrVisited[p.X + 1, p.Y] != -1
                            && arrBinary[p.X + 1, p.Y] == 0)
                        {
                            arrVisited[p.X + 1, p.Y] = -1;
                            lstSeedsTemp.Add(new Point(p.X + 1, p.Y));
                            lstPoints.Add(new Point(p.X + 1, p.Y));
                        }
                        if (p.X - 1 >0 && arrVisited[p.X - 1, p.Y] != -1
                            && arrBinary[p.X - 1, p.Y] == 0)
                        {
                            arrVisited[p.X - 1, p.Y] = -1;
                            lstSeedsTemp.Add(new Point(p.X - 1, p.Y));
                            lstPoints.Add(new Point(p.X - 1, p.Y));
                        }
                        if (p.Y + 1 < w && arrVisited[p.X, p.Y + 1] != -1
                            && arrBinary[p.X, p.Y + 1] == 0)
                        {
                            arrVisited[p.X, p.Y + 1] = -1;
                            lstSeedsTemp.Add(new Point(p.X, p.Y + 1));
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
        public void seedFilling(int[,] arrVisited, byte[,] arrBinary, List<Point> lstSeeds, List<Point> lstPoints, Point p1, Point p2, int h, int w)
        {
            do
            {
                List<Point> lstSeedsTemp = new List<Point>();
                foreach (Point p in lstSeeds)
                {
                    if (p.X + 1 < h-1 && arrVisited[p.X + 1, p.Y] != -1
                        && arrBinary[p.X + 1, p.Y] == 0)
                    {
                        arrVisited[p.X + 1, p.Y] = -1;
                        lstSeedsTemp.Add(new Point(p.X + 1, p.Y));
                        lstPoints.Add(new Point(p.X + 1+p1.X, p.Y+p1.Y));
                    }
                    if (p.X - 1 > p1.X && arrVisited[p.X - 1, p.Y] != -1
                        && arrBinary[p.X - 1, p.Y] == 0)
                    {
                        arrVisited[p.X - 1, p.Y] = -1;
                        lstSeedsTemp.Add(new Point(p.X - 1, p.Y));
                        lstPoints.Add(new Point(p.X - 1+p1.X, p.Y+p1.Y));
                    }
                    if (p.Y + 1 < w && arrVisited[p.X, p.Y + 1] != -1
                        && arrBinary[p.X, p.Y + 1] == 0)
                    {
                        arrVisited[p.X, p.Y + 1] = -1;
                        lstSeedsTemp.Add(new Point(p.X, p.Y + 1));
                        lstPoints.Add(new Point(p.X+p1.X, p.Y + 1+p1.Y));

                    }
                    if (p.Y - 1 >=0 && arrVisited[p.X, p.Y - 1] != -1
                        && arrBinary[p.X, p.Y - 1] == 0)

                    {
                        arrVisited[p.X, p.Y - 1] = -1;
                        lstSeedsTemp.Add(new Point(p.X, p.Y - 1));
                        lstPoints.Add(new Point(p.X+p1.X, p.Y - 1+p1.Y));
                    }

                    // buffer[i.x, i.y].gray = -1;

                }
                lstSeeds = new List<Point>(lstSeedsTemp);
            }
            while (lstSeeds.Count > 0);
        }

       
            public void FilterRegions(int minX, int maxX, int minY,int maxY)
            {
            if(minX!=1000)
            {
                int temp = minX;
                minX = maxX;
                maxX = temp;
            }
            
            
            Point p1, p2;
                for (int i = 0; i < regions.lstRegions.Count; i++)
                    if ( regions.lstRegions[i].maxy < y
                     ||regions.lstRegions[i].miny>y|| regions.lstRegions[i].maxy>w-w/5 || regions.lstRegions[i].miny < w / 5)
                    {
                    if ( regions.lstRegions[i].maxy<y)
                        minY = regions.lstRegions[i].maxy;
                    if (regions.lstRegions[i].miny>y)
                        maxY = regions.lstRegions[i].miny;
                        regions.lstRegions.RemoveAt(i);
                        i = -1;
                    }
                regions.bmpRegions=new Bitmap (MouthAndNoseBmp.Width, MouthAndNoseBmp.Height);
            foreach (Region r in regions.lstRegions)
                    foreach (Point p in r.lstPoints)
                    {if (p.X < minX)
                        minX = p.X;
                    if (p.X > maxX)
                        maxX = p.X;
                        regions.bmpRegions.SetPixel(p.Y, p.X, r.clr);
                    }
            regions.bmpRegions.Save(path + "\\MandNFiltered.jpg");
            if (regions.lstRegions.Count == 1)
            {
                if (minX > maxX - minX)
                {
                    if (minY == 1000)
                        minY = 0;
                    if (maxY == 0)
                        maxY = w;
                    p1 = new Point(0, minY);
                    p2 = new Point(minX, maxY);
                    for (int i = 0; i < minX; i++)
                        for (int j = minY; j < maxY; j++)
                            regions.bmpRegions.SetPixel(j, i, Color.Cyan);
                }
                else if(maxX-minX>h/4)
                {
                    p1 = new Point(0, minY);
                    p2 = new Point(h, maxY);
                    for (int i = maxX; i < h; i++)
                        for (int j = minY; j < maxY; j++)
                            regions.bmpRegions.SetPixel(j, i, Color.Cyan);
                }
                else 
                {
                    p1 = new Point(maxX, minY);
                    p2 = new Point(h, maxY);
                    for (int i = maxX; i < h; i++)
                        for (int j = minY; j < maxY; j++)
                            regions.bmpRegions.SetPixel(j, i, Color.Cyan);
                }

                regions.bmpRegions.Save(path + "\\subRegionAnalysis.jpg");
                GetAnotherSegment(p1,p2);
                foreach (Region r in regions.lstRegions)
                    foreach (Point p in r.lstPoints)
                    {
                       
                        regions.bmpRegions.SetPixel(p.Y, p.X, r.clr);
                    }

            }
            if (regions.lstRegions.Count == 0)
            {
                minX = 0;
                maxX = h-1;
                    p1 = new Point(minX, minY);
                    p2 = new Point(maxX, maxY);
                    for (int i = maxX; i < maxX; i++)
                        for (int j = minY; j < maxY; j++)
                            regions.bmpRegions.SetPixel(j, i, Color.Cyan);
               
                regions.bmpRegions.Save(path + "\\subRegionAnalysisCount_0.jpg");
                GetAnotherSegment(p1, p2);
                foreach (Region r in regions.lstRegions)
                    foreach (Point p in r.lstPoints)
                    {

                        regions.bmpRegions.SetPixel(p.Y, p.X, r.clr);
                    }
                regions.bmpRegions.Save(path + "//subRegionAnalysisCount_0.jpg");
            }
        }
      public void  GetAnotherSegment(Point p1,Point p2)
        {
            Bitmap bmpROI = new Bitmap(p2.Y - p1.Y, p2.X - p1.X);
            for(int i=p1.X;i<p2.X;i++)
                for(int j=p1.Y;j<p2.Y;j++)
                {
                    bmpROI.SetPixel(j - p1.Y, i -p1.X, MouthAndNoseBmp.GetPixel(j, i));
                }
            Bitmap GaussMandN = new Bitmap(Filters.gaussianFilter(bmpROI, 2));
            GaussMandN.Save(path + "\\GaussMandN2.jpg");
            Bitmap HistogramEqu = new Bitmap(PreProc.hisEqua(GaussMandN));
            HistogramEqu.Save(path + "\\MandN_HistEqu2.jpg");
            Bitmap MouthAndNoseBmpEnhanced = new Bitmap(ImageEnhancement.grayLIPMult(HistogramEqu));
            MouthAndNoseBmpEnhanced.Save(path + "\\MandN_Lip2.jpg");
            Bitmap MouthAndNoseBmpBinary = new Bitmap(PreProc.binary_Bmp(128, MouthAndNoseBmpEnhanced));
            MouthAndNoseBmpBinary.Save(path + "\\MandN_Bin2.jpg");
            DetectMouthNoseRegion2nd(p1, p2, MouthAndNoseBmpBinary);
            h = MouthAndNoseBmp.Height;
            w = MouthAndNoseBmp.Width;
            FilterRegions(p1.X, p2.X, p1.Y, p2.Y);
            
            
        }
       public void DetectROI()
        {
            int difX;
            int maxY,minY;
            if (regions.lstRegions[0].minx > regions.lstRegions[1].minx)
            {
                if (regions.lstRegions[0].maxy - y > y - regions.lstRegions[0].miny)
                {
                    maxY = regions.lstRegions[0].maxy;
                    minY = regions.lstRegions[0].maxy - 2 * (regions.lstRegions[0].maxy - y);
                }
                else
                {
                    minY = regions.lstRegions[0].miny;
                    maxY  = regions.lstRegions[0].miny + 2 * (y-regions.lstRegions[0].miny );
                }
                difX = regions.lstRegions[0].maxx - regions.lstRegions[0].minx;
                p1Mouth = new Point(regions.lstRegions[0].minx+h,minY);
                p2Mouth = new Point(regions.lstRegions[0].maxx+h, maxY);
                p1MouthROI = new Point(regions.lstRegions[1].maxx+h, p1Mouth.Y- (int)(0.3*(p2Mouth.Y - p1Mouth.Y)));
                p2MouthROI = new Point(h+h, p2Mouth.Y +(int)( 0.3*(p2Mouth.Y - p1Mouth.Y)));
             //   p1Mouth = new Point(regions.lstRegions[0].minx + h-(p1Mouth.X-p1MouthROI.X)/2, minY);
              //  p2Mouth = new Point(regions.lstRegions[0].maxx + h+ (p2MouthROI.X - p2Mouth.X) / 2, maxY);
                //////////////////
                if (regions.lstRegions[1].maxy - y > y - regions.lstRegions[1].miny )
                {
                    if (regions.lstRegions[1].maxy - y > regions.lstRegions[1].maxy - regions.lstRegions[1].miny)
                    {
                        maxY = regions.lstRegions[1].maxy;
                        minY = regions.lstRegions[1].miny;

                    }
                    else
                    {
                        maxY = regions.lstRegions[1].maxy;
                        minY = regions.lstRegions[1].maxy - 2 * (regions.lstRegions[1].maxy - y);
                    }
                }
                else if(regions.lstRegions[1].maxy - y < y - regions.lstRegions[1].miny)
                {
                    if (y-regions.lstRegions[1].miny > regions.lstRegions[1].maxy - regions.lstRegions[1].miny)
                    {
                        maxY = regions.lstRegions[1].maxy;
                        minY = regions.lstRegions[1].miny;

                    }
                    else
                    {
                        minY = regions.lstRegions[1].miny;
                        maxY = regions.lstRegions[1].miny + 2 * (y - regions.lstRegions[1].miny);
                    }
                    

                }

                difX = regions.lstRegions[1].maxx - regions.lstRegions[1].minx;
                p1Nose = new Point(regions.lstRegions[1].minx+h, minY);
                p2Nose = new Point(regions.lstRegions[1].maxx+h, maxY);
                p1NoseROI = new Point(h,p1Nose.Y -(int)(0.2* (p2Nose.Y - p1Nose.Y)));
                p2NoseROI = new Point(regions.lstRegions[0].minx+h,p2Nose.Y +(int)(0.2* (p2Nose.Y - p1Nose.Y)));
            //    p1Nose = new Point(regions.lstRegions[1].minx + h - (p1Nose.X - p1NoseROI.X) / 2, minY);
           //     p2Nose = new Point(regions.lstRegions[1].maxx + h + (p2NoseROI.X - p2Nose.X) / 2, maxY);
            }
            else
            {
                if (regions.lstRegions[1].maxy-y>y- regions.lstRegions[1].miny)
                {
                    maxY = regions.lstRegions[1].maxy;
                    minY = regions.lstRegions[1].maxy - 2 * (regions.lstRegions[1].maxy - y);
                }
                else
                {
                    minY = regions.lstRegions[1].miny;
                    maxY=  regions.lstRegions[1].miny + 2 * (y-regions.lstRegions[1].miny );

                }
                   

                difX = regions.lstRegions[1].maxx - regions.lstRegions[1].minx;
                p1Mouth = new Point(regions.lstRegions[1].minx+h, minY);
                p2Mouth = new Point(regions.lstRegions[1].maxx+h, maxY);
                p1MouthROI = new Point(regions.lstRegions[0].maxx+h, p1Mouth.Y - (int)(0.3*(p2Mouth.Y - p1Mouth.Y)));
                p2MouthROI = new Point(h+h, p2Mouth.Y + (int)(0.3*(p2Mouth.Y - p1Mouth.Y)));
                //   p1Mouth = new Point(regions.lstRegions[1].minx + h - (p1Mouth.X - p1MouthROI.X) / 2, minY);
                //    p2Mouth = new Point(regions.lstRegions[1].maxx + h + (p2MouthROI.X - p2Mouth.X) / 2, maxY);
                //////////////////
                if (regions.lstRegions[0].maxy - y > y - regions.lstRegions[0].miny)
                {
                    if (regions.lstRegions[0].maxy - y > regions.lstRegions[0].maxy - regions.lstRegions[0].miny)
                    {
                        maxY = regions.lstRegions[0].maxy;
                        minY = regions.lstRegions[0].miny;

                    }
                    else
                    {
                        maxY = regions.lstRegions[0].maxy;
                        minY = regions.lstRegions[0].maxy - 2 * (regions.lstRegions[0].maxy - y);
                    }
                }
                else if (regions.lstRegions[0].maxy - y < y - regions.lstRegions[0].miny)
                {
                    if (y - regions.lstRegions[0].miny > regions.lstRegions[0].maxy - regions.lstRegions[0].miny)
                    {
                        maxY = regions.lstRegions[0].maxy;
                        minY = regions.lstRegions[0].miny;

                    }
                    else
                    {
                        minY = regions.lstRegions[0].miny;
                        maxY = regions.lstRegions[0].miny + 2 * (y - regions.lstRegions[0].miny);
                    }


                }
                difX = regions.lstRegions[0].maxx - regions.lstRegions[0].minx;
                p1Nose = new Point(regions.lstRegions[0].minx+h, minY);
                p2Nose = new Point(regions.lstRegions[0].maxx+h, maxY);
                p1NoseROI = new Point(h, p1Nose.Y - (int)(0.2*(p2Nose.Y-p1Nose.Y)));
                p2NoseROI = new Point(regions.lstRegions[1].minx+h, p2Nose.Y + (int)(0.2*(p2Nose.Y - p1Nose.Y)));
               // p1Nose = new Point(regions.lstRegions[0].minx + h - (p1Nose.X - p1NoseROI.X) / 2, minY);
              //  p2Nose = new Point(regions.lstRegions[0].maxx + h + (p2NoseROI.X - p2Nose.X) / 2, maxY);

            }
            Bitmap bmpTemp = new Bitmap(path + "//MandNFiltered.jpg");
            for (int i = p1NoseROI.X; i < p2NoseROI.X; i++)
                for (int j = p1NoseROI.Y; j < p2NoseROI.Y; j++)
                    bmpTemp.SetPixel(j, i-h, Color.Red);
            for (int i = p1Nose.X; i < p2Nose.X; i++)
                for (int j = p1Nose.Y; j < p2Nose.Y; j++)
                    bmpTemp.SetPixel(j , i-h, Color.Black);
            foreach (Region r in regions.lstRegions)
                foreach (Point p in r.lstPoints)
                    bmpTemp.SetPixel(p.Y, p.X, r.clr);
            bmpTemp.Save(path+ "//bmpTemp.jpg");
        }
        }
    }
  

