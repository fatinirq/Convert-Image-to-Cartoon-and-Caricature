using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartoon_Face
{
    class SeedFilling4Seams
    {
 
            public Regions regions;
            public Point p1ROI, p2ROI;
            public Bitmap regionBmp;
            public static string path,name;
            public byte[,] arr;
            int h, w, y;
        public  SeedFilling4Seams(Bitmap bmp, Point P1, Point P2, string directory, string Name)
            {
            path = directory;
            name = Name;

            regionBmp = new Bitmap(bmp);
            p1ROI = P1;
            p2ROI = P2;
                //  System.Windows.MessageBox.Show("midY=" + midY);
                
                regions = new Regions(regionBmp);
                DetectRegion();
                FilterRegions();
               
            }
            public class Region
            {
                public List<Point> lstPoints = new List<Point>();
                public Color clr = new Color();
                public int miny = 1000, maxy = 0, minx = 1000, maxx = 0;
            public int value;
            }
            public class Regions
            {
            public Regions(Bitmap bmp)
                {
                //Bitmap GaussMandN = new Bitmap(Filters.gaussianFilter(MAndNBmp, 2));                 
                //Bitmap HistogramEqu = new Bitmap(PreProc.hisEqua(GaussMandN));                   
                //regionBmpEnhanced = new Bitmap(ImageEnhancement.grayLIPMult(HistogramEqu));                   
                //regionBmpBinary = new Bitmap(PreProc.binary_Bmp(128, regionBmpEnhanced)); 
                regionBmpBinary = new Bitmap(bmp);
            }
                public Bitmap regionBmpEnhanced;
                public Bitmap regionBmpBinary;
                public List<Region> lstRegions = new List<Region>();
                public List<Region> lstNonRegion = new List<Region>();
                public Bitmap bmpRegions;
            }

            public void DetectRegion()
            {
                Random rRan = new Random(50);
                Random gRand = new Random(10);
                Random bRand = new Random(26);
                int red, green, blue;
                h = regions.regionBmpBinary.Height;
                w = regions.regionBmpBinary.Width;
                arr = ImageEnhancement.convert2GrayArr(regions.regionBmpBinary);
                int[,] arrVisited = new int[h, w];
                int Seq = 1;
                Region nonRegion = new Region();
                List<Point> lstPoints;
                for (int i = 0; i < h; i++)
                    for (int j = 0; j < w; j++)
                    {
                        if (arrVisited[i, j] != -1 && arr[i, j] != 0)
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
                            
                                regions.lstRegions.Add(r);
                        }
                        else if (arrVisited[i, j] != -1 && arr[i, j] ==0 && j < w && i < h)
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
                regions.bmpRegions = new Bitmap(regions.regionBmpBinary.Width, regions.regionBmpBinary.Height);
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
            regions.bmpRegions.Save(path + "\\regionsOfDiff" + name + ".jpg");
        }
        public void seedFilling(int[,] arrVisited, byte[,] arrBinary, List<Point> lstSeeds, List<Point> lstPoints, int h, int w)
        {
            do
            {
                List<Point> lstSeedsTemp = new List<Point>();
                foreach (Point p in lstSeeds)
                {
                    if (p.X + 1 < h && arrVisited[p.X + 1, p.Y] != -1 && arrBinary[p.X + 1, p.Y]!=0)
                       // && Math.Abs(arrBinary[p.X + 1, p.Y] - arrBinary[p.X, p.Y])<10)
                    {
                        arrVisited[p.X + 1, p.Y] = -1;
                        lstSeedsTemp.Add(new Point(p.X + 1, p.Y));
                        lstPoints.Add(new Point(p.X + 1, p.Y));
                    }
                    if (p.X - 1 > 0 && arrVisited[p.X - 1, p.Y] != -1 && arrBinary[p.X - 1, p.Y]!=0)
                        //&& Math.Abs(arrBinary[p.X - 1, p.Y] - arrBinary[p.X, p.Y]) < 10)
                    {
                        arrVisited[p.X - 1, p.Y] = -1;
                        lstSeedsTemp.Add(new Point(p.X - 1, p.Y));
                        lstPoints.Add(new Point(p.X - 1, p.Y));
                    }
                    if (p.Y + 1 < w && arrVisited[p.X, p.Y + 1] != -1&& arrBinary[p.X, p.Y+1] != 0)
                        //&& Math.Abs(arrBinary[p.X , p.Y+1] - arrBinary[p.X, p.Y]) < 10)
                    {
                        arrVisited[p.X, p.Y + 1] = -1;
                        lstSeedsTemp.Add(new Point(p.X, p.Y + 1));
                        lstPoints.Add(new Point(p.X, p.Y + 1));
                    }
                    if (p.Y - 1 > 0 && arrVisited[p.X, p.Y - 1] != -1 && arrBinary[p.X, p.Y-1] != 0)
                        //&& Math.Abs(arrBinary[p.X, p.Y-1] - arrBinary[p.X, p.Y]) < 19)

                    {
                        arrVisited[p.X, p.Y - 1] = -1;
                        lstSeedsTemp.Add(new Point(p.X, p.Y - 1));
                        lstPoints.Add(new Point(p.X, p.Y - 1));
                    }

                }
                lstSeeds = new List<Point>(lstSeedsTemp);
            }
            while (lstSeeds.Count > 0);
        }

        public void FilterRegions()
            {
                
                for (int i = 0; i < regions.lstRegions.Count; i++)
                    if (regions.lstRegions[i].miny > p2ROI.Y+(p2ROI.Y-p1ROI.Y)/6 || regions.lstRegions[i].maxy <p1ROI.Y- (p2ROI.Y - p1ROI.Y) / 6 ||
                    regions.lstRegions[i].minx > p2ROI.X + (p2ROI.X - p1ROI.X) /6 || regions.lstRegions[i].maxx < p1ROI.X - (p2ROI.X - p1ROI.X) /6 )
                    {
                    foreach (Point p in regions.lstRegions[i].lstPoints)
                        arr[p.X, p.Y] = 0;
                        
                        regions.lstRegions.RemoveAt(i);

                        i = -1;
                    }
            regions.bmpRegions.Save(path + "\\regionsOfDiffAfter" + name + ".jpg");
            regions.bmpRegions.Dispose();
            }
           
        }
    }

