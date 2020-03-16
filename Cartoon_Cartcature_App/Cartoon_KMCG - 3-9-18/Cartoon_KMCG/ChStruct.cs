using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Drawing;

namespace Cartoon_KMCG
{
    class ChStruct
    {
        public class RGBWin
        {
            public RGBWin(List<Color> rgb, int i, int j)
            {
                lstColor = new List<Color>(rgb);
              
                avR = (byte)lstColor.Average(temp => temp.R);
                avG = (byte)lstColor.Average(temp => temp.G);
                avB = (byte)lstColor.Average(temp => temp.B);               
                x = i;
                y = j;
               
            }

           public List<Color> lstColor;
           
            public byte avR;
            public byte avG;
            public byte avB;
            public byte YCb;
            public byte YCr;
            public byte Y;
            public int x;
            public int y;
        }
        public class RGBAv
        {
            public RGBAv(List<Color> rgb, int i, int j)
            {
              
                avR = (byte)rgb.Average(temp => temp.R);
                avG = (byte)rgb.Average(temp => temp.G);
                avB = (byte)rgb.Average(temp => temp.B);
                
                x = i;
                y = j;
              
            }

            
            public byte avR;
            public byte avG;
            public byte avB;
            
            public int x;
            public int y;
        }

        public class YUVWin
        {
            public YUVWin(List<Color> rgb, int i, int j)
            {
                lstColor = new List<Color>(rgb);
                lstYUVColor = new List<ColorSpaces.YUV>(ColorSpaces.ConvertRGBToYUV(rgb));
                avR = (byte)lstColor.Average(temp => temp.R);
                avG = (byte)lstColor.Average(temp => temp.G);
                avB = (byte)lstColor.Average(temp => temp.B);
                avY = (int)lstYUVColor.Average(temp => temp.Y);
                avU = (int)lstYUVColor.Average(temp => temp.U);
                avV = (int)lstYUVColor.Average(temp => temp.V);
                defR = (byte)(lstColor.Max(temp => temp.R) - lstColor.Min(temp => temp.R));
                defG = (byte)(lstColor.Max(temp => temp.G) - lstColor.Min(temp => temp.G));
                defB = (byte)(lstColor.Max(temp => temp.B) - lstColor.Min(temp => temp.B));
                defY = (int)(lstYUVColor.Max(temp => temp.Y) - lstYUVColor.Min(temp => temp.Y));
                defU = (int)Math.Abs((lstYUVColor.Max(temp => temp.U) - lstYUVColor.Min(temp => temp.U)));
                defV = (int)Math.Abs((lstYUVColor.Max(temp => temp.V) - lstYUVColor.Min(temp => temp.V)));
                x = i;
                y = j;

            }
            List<Color> lstColor;
            List<ColorSpaces.YUV> lstYUVColor;
            public byte avR;
            public byte avG;
            public byte avB;
            public int avY;
            public int avU;
            public int avV;
            public byte defR;
            public byte defG;
            public byte defB;
            public int defY;
            public int defU;
            public int defV;
            public bool edge;
            public int x;
            public int y;
        }


        public class HSVWin
        {
            public HSVWin(List<Color> rgb, int i, int j)
            {
                lstColor = new List<Color>(rgb);
                lstHSVColor = new List<ColorSpaces.HSV>(ColorSpaces.ConvertRGBToHSV(rgb));
                avR = (byte)lstColor.Average(temp => temp.R);
                avG = (byte)lstColor.Average(temp => temp.G);
                avB = (byte)lstColor.Average(temp => temp.B);
                avH = (int)lstHSVColor.Average(temp => temp.H);
                avS = (int)lstHSVColor.Average(temp => temp.S);
                avV = (int)lstHSVColor.Average(temp => temp.V);
                defR = (byte)(lstColor.Max(temp => temp.R) - lstColor.Min(temp => temp.R));
                defG = (byte)(lstColor.Max(temp => temp.G) - lstColor.Min(temp => temp.G));
                defB = (byte)(lstColor.Max(temp => temp.B) - lstColor.Min(temp => temp.B));
                defH = (int)(lstHSVColor.Max(temp => temp.H) - lstHSVColor.Min(temp => temp.H));
                defS = (int)Math.Abs((lstHSVColor.Max(temp => temp.S) - lstHSVColor.Min(temp => temp.S)));
                defV = (int)Math.Abs((lstHSVColor.Max(temp => temp.V) - lstHSVColor.Min(temp => temp.V)));
                edge = false;
                x = i;
                y = j;

            }
            List<Color> lstColor;
            List<ColorSpaces.HSV> lstHSVColor;
            public byte avR;
            public byte avG;
            public byte avB;
            public float avH;
            public float avS;
            public float avV;
            public byte defR;
            public byte defG;
            public byte defB;
            public float defH;
            public float defS;
            public float defV;
            public bool edge;
            public int x;
            public int y;
            public byte lstGray;
        }

        public class HSIWin
        {
            public HSIWin(List<Color> rgb, int i, int j)
            {
                lstColor = new List<Color>(rgb);
                lstHSIColor = new List<ColorSpaces.HSI>(ColorSpaces.ConvertRGBToHSI(rgb));
                avR = (byte)lstColor.Average(temp => temp.R);
                avG = (byte)lstColor.Average(temp => temp.G);
                avB = (byte)lstColor.Average(temp => temp.B);
                avH = (int)lstHSIColor.Average(temp => temp.H);
                avS = (int)lstHSIColor.Average(temp => temp.S);
                avI = (int)lstHSIColor.Average(temp => temp.I);
                defR = (byte)(lstColor.Max(temp => temp.R) - lstColor.Min(temp => temp.R));
                defG = (byte)(lstColor.Max(temp => temp.G) - lstColor.Min(temp => temp.G));
                defB = (byte)(lstColor.Max(temp => temp.B) - lstColor.Min(temp => temp.B));
                defH = (int)(lstHSIColor.Max(temp => temp.H) - lstHSIColor.Min(temp => temp.H));
                defS = (int)Math.Abs((lstHSIColor.Max(temp => temp.S) - lstHSIColor.Min(temp => temp.S)));
                defI = (int)Math.Abs((lstHSIColor.Max(temp => temp.I) - lstHSIColor.Min(temp => temp.I)));
                edge = false;
                x = i;
                y = j;

            }
            List<Color> lstColor;
            List<ColorSpaces.HSI> lstHSIColor;
            public byte avR;
            public byte avG;
            public byte avB;
            public float avH;
            public float avS;
            public float avI;
            public byte defR;
            public byte defG;
            public byte defB;
            public float defH;
            public float defS;
            public float defI;
            public bool edge;
            public int x;
            public int y;
        }
      public  class RGBWinGroup
        {
            public RGBWinGroup(Color rgb, int i, int j)
            {
                clr = new Color();
                clr = rgb;
                x = i;
                y = j;
            }
            public Color clr;
            public int x;
            public int y;
        
        }
        public class LstRGBWinGroup
        {
           
           public List <RGBWinGroup> lstClr=new List<RGBWinGroup>();
            public byte limitRed;
            public byte limitGreen;
            public byte limitBlue;
            public int noCheck;

        }

    }
}



