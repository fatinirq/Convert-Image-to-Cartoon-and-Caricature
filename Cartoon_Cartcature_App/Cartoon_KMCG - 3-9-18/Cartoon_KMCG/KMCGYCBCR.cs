using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartoon_KMCG
{
    class KMCGYCBCR
    {

        public static List<List<ChStruct.RGBWin>> rgbMainLst = new List<List<ChStruct.RGBWin>>();

        public static List<ChStruct.RGBWin>[] codeBookCount = new List<ChStruct.RGBWin>[3];
        public static int ClusterNo = 0;
        #region KMCGRGB
        public static Bitmap KMCGRGB(Bitmap bmp, bool rF, bool gF, bool bF, byte rV, byte gV, byte bV, string filename)
        {
            List<Color> lstClrDef = new List<Color>();
            lstClrDef.Add(Color.FromArgb(0, 0, 0));
            ChStruct.RGBWin defCB = new ChStruct.RGBWin(lstClrDef, 0, 0);
            for (int i = 0; i < 3; i++)
                codeBookCount[i] = new List<ChStruct.RGBWin>();



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
            bool flag = false;
            //string pathColor = System.IO.Path.Combine(filename.Substring(0, filename.IndexOf('.')) + "/cartoon2Data", "Red_Av_Min_Max");
            foreach (List<ChStruct.RGBWin> lstTemp in mainLst)
            {

                var lstTemp2 = lstTemp.OrderBy(e => e.Y).ToList<ChStruct.RGBWin>();
                tempMainLst.Add(lstTemp2);
            }
            mainLst = new List<List<ChStruct.RGBWin>>(tempMainLst);
            tempMainLst = new List<List<ChStruct.RGBWin>>();

            while (gF == true)
            {
                flag = false;
                foreach (List<ChStruct.RGBWin> lstTemp2 in mainLst)
                {

                    if (lstTemp2[lstTemp2.Count - 1].Y - lstTemp2[0].Y > gV)
                    {
                        flag = true;
                        codeBookCount[1].Add(lstTemp2[lstTemp2.Count / 2]);
                        var listA = (lstTemp2.GetRange(0, lstTemp2.Count / 2)).ToList<ChStruct.RGBWin>();

                        var listB = lstTemp2.Except(listA).ToList<ChStruct.RGBWin>();
                        tempMainLst.Add(listA);
                        tempMainLst.Add(listB);
                    }
                    else
                    {
                        tempMainLst.Add(lstTemp2);
                        codeBookCount[1].Add(defCB);
                    }

                }
                if (flag == false)
                {

                    flag = false;
                    gF = false;
                }
                else
                    mainLst = new List<List<ChStruct.RGBWin>>(tempMainLst);
                tempMainLst = new List<List<ChStruct.RGBWin>>();
            }
            foreach (List<ChStruct.RGBWin> lstTemp in mainLst)
            {

                var lstTemp2 = lstTemp.OrderBy(e => e.YCr).ToList<ChStruct.RGBWin>();
                tempMainLst.Add(lstTemp2);
            }
            mainLst = new List<List<ChStruct.RGBWin>>(tempMainLst);
            tempMainLst = new List<List<ChStruct.RGBWin>>();
           
            while (rF == true)
            {
                flag = false;
                foreach (List<ChStruct.RGBWin> lstTemp2 in mainLst)
                {

                    if (lstTemp2[lstTemp2.Count - 1].YCr - lstTemp2[0].YCr > rV)
                    {
                        flag = true;
                        codeBookCount[0].Add(lstTemp2[lstTemp2.Count / 2]);
                        var listA = (lstTemp2.GetRange(0, lstTemp2.Count / 2)).ToList<ChStruct.RGBWin>();

                        var listB = lstTemp2.Except(listA).ToList<ChStruct.RGBWin>();
                        tempMainLst.Add(listA);
                        tempMainLst.Add(listB);
                    }
                    else
                    {
                        tempMainLst.Add(lstTemp2);
                        codeBookCount[0].Add(defCB);
                    }

                }
                if (flag == false)
                {

                    flag = false;
                    rF = false;
                }
                else
                    mainLst = new List<List<ChStruct.RGBWin>>(tempMainLst);
                tempMainLst = new List<List<ChStruct.RGBWin>>();
            }
            ///////////////////////////
            
            
            //////////////////////////////////////////
            foreach (List<ChStruct.RGBWin> lstTemp in mainLst)
            {

                var lstTemp2 = lstTemp.OrderBy(e => e.YCb).ToList<ChStruct.RGBWin>();
                tempMainLst.Add(lstTemp2);
            }
            mainLst = new List<List<ChStruct.RGBWin>>(tempMainLst);
            tempMainLst = new List<List<ChStruct.RGBWin>>();

            while (bF == true)
            {
                flag = false;
                foreach (List<ChStruct.RGBWin> lstTemp2 in mainLst)
                {

                    if (lstTemp2[lstTemp2.Count - 1].YCb - lstTemp2[0].YCb > bV)
                    {
                        flag = true;
                        codeBookCount[2].Add(lstTemp2[lstTemp2.Count / 2]);
                        var listA = (lstTemp2.GetRange(0, lstTemp2.Count / 2)).ToList<ChStruct.RGBWin>();

                        var listB = lstTemp2.Except(listA).ToList<ChStruct.RGBWin>();
                        tempMainLst.Add(listA);
                        tempMainLst.Add(listB);
                    }
                    else
                    {
                        tempMainLst.Add(lstTemp2);
                        codeBookCount[2].Add(defCB);
                    }

                }
                if (flag == false)
                {

                    flag = false;
                    bF = false;
                }
                else
                    mainLst = new List<List<ChStruct.RGBWin>>(tempMainLst);
                tempMainLst = new List<List<ChStruct.RGBWin>>();
            }
            //////////////////////////////////////
            foreach (List<ChStruct.RGBWin> lstTemp in mainLst)
            {

                var lstTemp2 = lstTemp.OrderBy(e => e.Y).ToList<ChStruct.RGBWin>();
                tempMainLst.Add(lstTemp2);
            }
            mainLst = new List<List<ChStruct.RGBWin>>(tempMainLst);
            tempMainLst = new List<List<ChStruct.RGBWin>>();

            while (gF == true)
            {
                flag = false;
                foreach (List<ChStruct.RGBWin> lstTemp2 in mainLst)
                {

                    if (lstTemp2[lstTemp2.Count - 1].Y - lstTemp2[0].Y > gV)
                    {
                        flag = true;
                        codeBookCount[1].Add(lstTemp2[lstTemp2.Count / 2]);
                        var listA = (lstTemp2.GetRange(0, lstTemp2.Count / 2)).ToList<ChStruct.RGBWin>();

                        var listB = lstTemp2.Except(listA).ToList<ChStruct.RGBWin>();
                        tempMainLst.Add(listA);
                        tempMainLst.Add(listB);
                    }
                    else
                    {
                        tempMainLst.Add(lstTemp2);
                        codeBookCount[1].Add(defCB);
                    }

                }
                if (flag == false)
                {

                    flag = false;
                    gF = false;
                }
                else
                    mainLst = new List<List<ChStruct.RGBWin>>(tempMainLst);
                tempMainLst = new List<List<ChStruct.RGBWin>>();
            }
            ////////////////////////////
            rgbMainLst = new List<List<ChStruct.RGBWin>>(mainLst);
            foreach (List<ChStruct.RGBWin> tempLst in mainLst)
            {
                ClusterNo++;
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

            //MessageBox.Show("Stop Watch KMCGbyFatin =" + sw.Elapsed);

            return bmpOut;
        }
        #endregion
    }
}
