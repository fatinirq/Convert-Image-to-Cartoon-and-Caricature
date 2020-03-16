using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;

namespace Cartoon_Face
{
    class Seed_Filling_HSV
    {
        static int h, w;
        public struct index
        {
            public int x;
            public int y;
        }
        public List<Region> lstRegions = new List<Region>();
        public Seed_Filling_HSV(Bitmap bmp)
        {
            h = bmp.Height;
            w = bmp.Width;
            detectR(ColorSpaces.ConvertRGBToHSV_BMP(bmp));
        }
        public struct point
        {
            public int x;
            public int y;
            public Color clr;
            public ColorSpaces.HSV hsv;

        }
        public class Region
        {
            public List<point> lstPoints = new List<point>();
            public Bitmap bmp = new Bitmap(w, h);
        }
        public struct Buffer
        {
            public int index;
            public int gray;
            public int temp;
            public ColorSpaces.HSV hsv;
        }
        void detectR(ColorSpaces.HSV[,] hsv)
        {

         //   MessageBox.Show("h=" + h + " w=" + w);
            Buffer[,] buffer = new Buffer[h, w];

            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                    // buffer[i,j].
                    buffer[i, j].temp = 0;
                    buffer[i, j].index = 0;
                    buffer[i, j].hsv = hsv[i, j];
                }

            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {

                    if (buffer[i, j].temp != -1)
                    {
                        //MessageBox.Show("i=" + i + " j= " + j);
                        Region r = new Region();
                        List<index> lstSeeds = new List<index>();
                        index s = new index();
                        s.x = i;
                        s.y = j;
                        point p0 = new point();
                        p0.x = i;
                        p0.y = j;
                        r.lstPoints.Add(p0);

                        lstSeeds.Add(s);
                        buffer[i, j].temp = -1;
                        seedFilling(r, buffer, lstSeeds);
                        if (r.lstPoints.Count > 3)
                        {
                            int g = (int)r.lstPoints.Average(temp => temp.clr.R);
                            foreach (point p in r.lstPoints)
                            {
                                r.bmp.SetPixel(p.y, p.x, Color.FromArgb(g, g, g));
                            }
                            lstRegions.Add(r);
                        }

                    }
                }
           // MessageBox.Show("RegionCount=" + lstRegions.Count);
        }


        public void seedFilling(Region r, Buffer[,] buffer, List<index> lstSeeds)
        {
            double avS = buffer[lstSeeds[0].x,lstSeeds[0].y].hsv.S,avH= buffer[lstSeeds[0].x, lstSeeds[0].y].hsv.H, avV= buffer[lstSeeds[0].x, lstSeeds[0].y].hsv.V;

            List<index> lstSeedsTemp;
            do
            {
                lstSeedsTemp = new List<index>();
                foreach (index i in lstSeeds)
                {
                    if (i.x + 1 < h && buffer[i.x + 1, i.y].temp != -1 
                        && Math.Abs(buffer[i.x, i.y].hsv.H - buffer[i.x + 1, i.y].hsv.H) <= 1
                       
                        )
                    {
                        avH = (avH + buffer[i.x + 1, i.y].hsv.H) / 2;
                        avV = (avS + buffer[i.x + 1, i.y].hsv.V) / 2;
                        avS = (avS + buffer[i.x + 1, i.y].hsv.S) / 2;
                        index s = new index();
                        s.x = i.x + 1;
                        s.y = i.y;
                        lstSeedsTemp.Add(s);
                        buffer[s.x, s.y].temp = -1;
                        point p = new point();
                        p.x = i.x + 1;
                        p.y = i.y;
                        p.clr = Color.FromArgb(buffer[i.x + 1, i.y].gray, buffer[i.x + 1, i.y].gray, buffer[i.x + 1, i.y].gray);
                        r.lstPoints.Add(p);

                    }
                    if (i.x - 1 > 0 && buffer[i.x - 1, i.y].temp != -1 
                        && Math.Abs(buffer[i.x, i.y].hsv.H - buffer[i.x - 1, i.y].hsv.H) <= 1
                       
                       )
                    {
                        avH = (avH + buffer[i.x - 1, i.y].hsv.H) / 2;
                        avV = (avV + buffer[i.x - 1, i.y].hsv.V) / 2;
                        avS = (avS + buffer[i.x - 1, i.y].hsv.S) / 2;
                        index s = new index();
                        s.x = i.x - 1;
                        s.y = i.y;
                        lstSeedsTemp.Add(s);
                        buffer[s.x, s.y].temp = -1;
                        point p = new point();
                        p.x = i.x - 1;
                        p.y = i.y;
                        p.clr = Color.FromArgb(buffer[i.x - 1, i.y].gray, buffer[i.x - 1, i.y].gray, buffer[i.x - 1, i.y].gray);
                        r.lstPoints.Add(p);
                    }
                    if (i.y + 1 < w && buffer[i.x, i.y + 1].temp != -1 
                        && Math.Abs(buffer[i.x, i.y].hsv.H - buffer[i.x, i.y + 1].hsv.H) <= 1
                       
                       )
                    {
                        avH = (avH + buffer[i.x, i.y + 1].hsv.H) / 2;
                        avS = (avS + buffer[i.x, i.y + 1].hsv.S) / 2;
                        avV = (avV + buffer[i.x, i.y + 1].hsv.V) / 2;
                        index s = new index();
                        s.x = i.x;
                        s.y = i.y + 1;
                        lstSeedsTemp.Add(s);
                        buffer[s.x, s.y].temp = -1;
                        point p = new point();
                        p.x = i.x;
                        p.y = i.y + 1;
                        p.clr = Color.FromArgb(buffer[i.x, i.y + 1].gray, buffer[i.x, i.y + 1].gray, buffer[i.x, i.y + 1].gray);
                        r.lstPoints.Add(p);
                    }
                    if (i.y - 1 > 0 && buffer[i.x, i.y - 1].temp != -1 
                        && Math.Abs(buffer[i.x, i.y].hsv.H - buffer[i.x, i.y - 1].hsv.H) <= 1
                       
                       )
                    {
                        avH = (avH + buffer[i.x, i.y - 1].hsv.H) / 2;
                        avV = (avV + buffer[i.x, i.y - 1].hsv.V) / 2;
                        avS = (avS + buffer[i.x, i.y - 1].hsv.S) / 2;
                        index s = new index();
                        s.x = i.x;
                        s.y = i.y - 1;
                        lstSeedsTemp.Add(s);
                        buffer[s.x, s.y].temp = -1;
                        point p = new point();
                        p.x = i.x;
                        p.y = i.y - 1;
                        p.clr = Color.FromArgb(buffer[i.x, i.y - 1].gray, buffer[i.x, i.y - 1].gray, buffer[i.x, i.y - 1].gray);
                        r.lstPoints.Add(p);
                    }
                    
                }
                lstSeeds = new List<index>(lstSeedsTemp);
            }
            while (lstSeeds.Count > 0);
        }

    }
}

