using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Cartoon_KMCG
{
    class KMCG_Old
    {
      
        public static List<List<ChStruct.RGBWin>> rgbMainLst = new List<List<ChStruct.RGBWin>>();
        public static List<ChStruct.RGBWin> codeBookLst;
       
        #region KMCGRGB
        public static Bitmap KMCGRGB(Bitmap bmp,int count,  string filename,Stopwatch sw)
        {
            sw.Start();
            codeBookLst = new List<ChStruct.RGBWin>();         
            Bitmap bmpOut = new Bitmap(bmp);
            List<ChStruct.RGBWin> lstRGB = new List<ChStruct.RGBWin>();
            Color clr = new Color();
            ChStruct.RGBWin tempRGBWin;
           
            for (int i = 0; i < bmp.Height - 1; i += 2)
                for (int j = 0; j < bmp.Width - 1; j += 2)
                {
                    List<Color> lstColor = new List<Color>();
                    clr = bmp.GetPixel(j, i);
                    lstColor.Add(clr);
                    clr = bmp.GetPixel(j + 1, i);
                    lstColor.Add(clr);
                    clr = bmp.GetPixel(j, i + 1);
                    lstColor.Add(clr);
                    clr = bmp.GetPixel(j + 1, i + 1);
                    lstColor.Add(clr);
                    tempRGBWin = new ChStruct.RGBWin(lstColor, i, j);
                    lstRGB.Add(tempRGBWin);
                }
            List<List<ChStruct.RGBWin>> mainLst = new List<List<ChStruct.RGBWin>>();
            List<List<ChStruct.RGBWin>> tempMainLst = new List<List<ChStruct.RGBWin>>();
            mainLst.Add(lstRGB);
            int round = 0;
            for (int i = 0; i < 4 && round < count; i++)
            {

                if (round < count)
                {
                    tempMainLst = new List<List<ChStruct.RGBWin>>();
                    for (int j=0;j<mainLst.Count;j++)
                    {

                        var lstTemp2 = mainLst[j].OrderBy(e => e.lstColor[i].R).ToList<ChStruct.RGBWin>();

                        codeBookLst.Add(lstTemp2[lstTemp2.Count / 2]);

                        var listA = (lstTemp2.GetRange(0, lstTemp2.Count / 2)).ToList<ChStruct.RGBWin>();

                        var listB = lstTemp2.Except(listA).ToList<ChStruct.RGBWin>();
                        tempMainLst.Add(listA);
                        tempMainLst.Add(listB);


                    }
                }
                mainLst = new List<List<ChStruct.RGBWin>>(tempMainLst);
                round++;

                if (round < count)
                {
                    tempMainLst = new List<List<ChStruct.RGBWin>>();
                    for (int j = 0; j < mainLst.Count; j++)
                    {

                        var lstTemp2 = mainLst[j].OrderBy(e => e.lstColor[i].B).ToList<ChStruct.RGBWin>();

                        codeBookLst.Add(lstTemp2[lstTemp2.Count / 2]);

                        var listA = (lstTemp2.GetRange(0, lstTemp2.Count / 2)).ToList<ChStruct.RGBWin>();

                        var listB = lstTemp2.Except(listA).ToList<ChStruct.RGBWin>();
                        tempMainLst.Add(listA);
                        tempMainLst.Add(listB);


                    }
                }
                mainLst = new List<List<ChStruct.RGBWin>>(tempMainLst);


                round++;
                if (round < count)
                {
                    tempMainLst = new List<List<ChStruct.RGBWin>>();
                    for (int j = 0; j < mainLst.Count; j++)
                    {

                        var lstTemp2 = mainLst[j].OrderBy(e => e.lstColor[i].G).ToList<ChStruct.RGBWin>();
                        codeBookLst.Add(lstTemp2[lstTemp2.Count / 2]);
                        var listA = (lstTemp2.GetRange(0, lstTemp2.Count / 2)).ToList<ChStruct.RGBWin>();

                        var listB = lstTemp2.Except(listA).ToList<ChStruct.RGBWin>();
                        tempMainLst.Add(listA);
                        tempMainLst.Add(listB);

                    }
            }
                mainLst = new List<List<ChStruct.RGBWin>>(tempMainLst);
                round++;
                
            }
            sw.Stop();
            rgbMainLst = new List<List<ChStruct.RGBWin>>(mainLst);
            
                foreach (List<ChStruct.RGBWin> tempLst in mainLst)
            {
                double avR = tempLst.Average(temp => temp.avR);
                double avG = tempLst.Average(temp => temp.avG);
                double avB = tempLst.Average(temp => temp.avB);
                foreach (ChStruct.RGBWin tempItem in tempLst)
                {
                   
                    
                        bmpOut.SetPixel(tempItem.y, tempItem.x, Color.FromArgb(255, (byte)avR, (byte)avG, (byte)avB));
                        bmpOut.SetPixel(tempItem.y + 1, tempItem.x, Color.FromArgb(255, (byte)avR, (byte)avG, (byte)avB));
                        bmpOut.SetPixel(tempItem.y, tempItem.x + 1, Color.FromArgb(255, (byte)avR, (byte)avG, (byte)avB));
                        bmpOut.SetPixel(tempItem.y + 1, tempItem.x + 1, Color.FromArgb(255, (byte)avR, (byte)avG, (byte)avB));
                    
                }


            }
           
            return bmpOut;

        }
        #endregion
    }
}
