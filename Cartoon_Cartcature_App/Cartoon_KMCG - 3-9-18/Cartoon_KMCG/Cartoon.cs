using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartoon_KMCG
{
    public class Cartoon
    {
        public static Bitmap Convert2Cartoon(Bitmap bmp)
        {

            /////////////////////////KMCG New///////////////////////////////////////
            byte th = 60;
            Stopwatch sw = new Stopwatch();
            Bitmap kmcg_New_Bmp = new Bitmap(KMCGbyFatin.KMCGRGB(bmp, true, true, true, th, th, th));

            int CBSize = KMCGbyFatin.codeBookCount[0].Count + KMCGbyFatin.codeBookCount[1].Count + KMCGbyFatin.codeBookCount[2].Count;

            kmcg_New_Bmp.SetResolution(96, 96);
            Bitmap kmcg_New_Bmp_his = new Bitmap(ImageEnhancement.histogram_drawing(kmcg_New_Bmp));
            ///////////////KMCG Enhancement and Hist
            Bitmap kmcg_New_Bmp_Enhanced = new Bitmap(ImageEnhancement.colorLIPMult(kmcg_New_Bmp));
            kmcg_New_Bmp_Enhanced.SetResolution(96, 96);
            //////////////////Canny                 
            Canny CannyData1 = new Canny(bmp, 70, 100, 5, 1, "D:\\");
            Bitmap edge1Bmp = new Bitmap(CannyData1.DisplayImage(CannyData1.EdgeMap));
            Bitmap kmcg_New_Bmp_Enhanced_Edge = new Bitmap(kmcg_New_Bmp_Enhanced);

            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                    if (edge1Bmp.GetPixel(j, i).R == 255)
                    {
                        int avR = 0;
                        int avG = 0;
                        int avB = 0;
                        int count = 0;
                        for (int k = -1; k <= 1; k++)
                            for (int l = -1; l <= 1; l++)
                            {
                                Color clr2 = kmcg_New_Bmp_Enhanced.GetPixel(j, i);
                                int gray = (clr2.R + clr2.G + clr2.B) / 3;
                                if (l != 0 || k != 0)
                                {
                                    System.Drawing.Color clr = kmcg_New_Bmp_Enhanced.GetPixel(j + l, i + k);
                                    if ((clr.R + clr.G + clr.B) / 3 <= gray)
                                    {
                                        avR = avR + clr.R;
                                        avG = avG + clr.G;
                                        avB = avB + clr.B;
                                        count++;
                                    }
                                }
                            }
                        if (count != 0)
                        {
                            avB = avB / count - 20;
                            avG = avG / count - 20;
                            avR = avR / count - 20;
                            if (avB < 0)
                                avB = 0;
                            if (avR < 0)
                                avR = 0;
                            if (avG < 0)
                                avG = 0;
                            //    for (int k = -1; k <= 1; k++)
                            //       for (int l = -1; l <= 1; l++)
                            //     {

                            kmcg_New_Bmp_Enhanced_Edge.SetPixel(j, i, Color.FromArgb(avR, avG, avB));

                            //   }
                        }
                    }
                }

            return kmcg_New_Bmp_Enhanced_Edge;

        }

    }
}

