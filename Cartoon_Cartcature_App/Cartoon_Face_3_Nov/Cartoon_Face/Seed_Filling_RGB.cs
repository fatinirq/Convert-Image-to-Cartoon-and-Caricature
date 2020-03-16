using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;

namespace Cartoon_Face
{
    class Seed_Filling_RGB
    {
        static int h, w;
        public struct index
        {
            public int x;
            public int y;
        }
        public List<Region> lstRegions = new List<Region>();
        public Seed_Filling_RGB(Bitmap bmp)
        {
            h = bmp.Height;
            w = bmp.Width;
            detectR(ConvertBitmap2Buffer(bmp));

        }
        public static Color[,] ConvertBitmap2Buffer(Bitmap bmp)
        {
            Color[,] output = new Color[bmp.Height, bmp.Width];

            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                    output[i, j] = bmp.GetPixel(j, i);
            return output;

        }
        public struct point
        {
            public int x;
            public int y;
            public Color clr;
           

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
            public Color RGB;
        }
        void detectR(Color[,] rgb)
        {

           // MessageBox.Show("h=" + h + " w=" + w);
            Buffer[,] buffer = new Buffer[h, w];

            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {

                    // buffer[i,j].
                    buffer[i, j].temp = 0;
                    buffer[i, j].index = 0;
                    buffer[i, j].RGB = rgb[i, j];
                }

            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {

                    if (buffer[i, j].temp != -1 && buffer[i, j].RGB.R >=10)
                    {
                        //MessageBox.Show("i=" + i + " j= " + j);
                        Region r = new Region();
                        List<index> lstSeeds = new List<index>();
                        index s = new index();
                        s.x = i;
                        s.y = j;

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
            //MessageBox.Show("RegionCount=" + lstRegions.Count);
        }


        public void seedFilling(Region r, Buffer[,] buffer, List<index> lstSeeds)
        {
            List<index> lstSeedsTemp;
            do
            {
                lstSeedsTemp = new List<index>();
                foreach (index i in lstSeeds)
                {
                    if (i.x + 1 < h && buffer[i.x + 1, i.y].temp != -1 
                        && buffer[i.x + 1, i.y].RGB.R>=10
                        && Math.Abs(buffer[i.x, i.y].RGB.R - buffer[i.x + 1, i.y].RGB.R) <=2)
                      
                    {
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
                        && buffer[i.x - 1, i.y].RGB.R>=10
                        && Math.Abs(buffer[i.x, i.y].RGB.R - buffer[i.x - 1, i.y].RGB.R) <= 2 )
                      
                    {
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
                       && buffer[i.x, i.y + 1].RGB.R >=10
                       && Math.Abs(buffer[i.x, i.y].RGB.R - buffer[i.x, i.y + 1].RGB.R) <=2)
                    {
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
                        && buffer[i.x, i.y - 1].RGB.R>=10
                        && Math.Abs(buffer[i.x, i.y].RGB.R - buffer[i.x, i.y - 1].RGB.R) <=2 )
                      
                    {
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

                   // buffer[i.x, i.y].gray = -1;

                }
                lstSeeds = new List<index>(lstSeedsTemp);
            }
            while (lstSeeds.Count > 0);
        }

    }
}

