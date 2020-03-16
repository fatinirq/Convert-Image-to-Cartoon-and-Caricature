using System;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartoon_Face
{
    class DetectROIChange
    {
        int[,] subPixels;
        int h, w;
        string path;
        string subName;
        Bitmap bmpDiff;
        public byte[,] arrRegions;
       public  DetectROIChange(Color[,] colorPixels,byte[,] grayPixels,string directory, string name, Point p1,Point p2 )
        {
             h = colorPixels.GetLength(0);
             w = colorPixels.GetLength(1);
            path = directory;
            subName = name;
            getDiff(colorPixels, grayPixels);
            SeedFilling4Seams ss = new SeedFilling4Seams(bmpDiff, p1, p2,  path,name);
            arrRegions = ss.arr;
            bmpDiff.Dispose();
        }
        public void getDiff(Color[,] colorPixels, Byte[,] grayPixels)
        {
            subPixels = new int[h, w];
            for(int i=0;i< h;i++)
                for(int j=0;j< w;j++)
                {
                    subPixels[i, j] = Math.Abs((int)((colorPixels[i, j].R + colorPixels[i, j].G + colorPixels[i, j].B) / 3) - grayPixels[i, j]);
                    if (subPixels[i, j] > 5)
                        subPixels[i, j] = subPixels[i, j] + 20;
                    else
                        subPixels[i, j] = 0;
                    if (subPixels[i, j] > 255)
                        subPixels[i, j] = 255;
                }
             bmpDiff = new Bitmap(w, h);
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    bmpDiff.SetPixel(j, i, Color.FromArgb(subPixels[i, j], subPixels[i, j], subPixels[i, j]));
            bmpDiff.Save(path + "\\bmpDiff"+subName+".jpg");
          //  Bitmap bmpDiffHis = new Bitmap(PreProc.hisEqua(bmpDiff));
          //  bmpDiffHis.Save(path + "\\bmpDiffHisEq" + subName + ".jpg");
           // Bitmap bmpDiffHisLIP = new Bitmap(ImageEnhancement.grayLIPMult(bmpDiffHis));
           // bmpDiffHisLIP.Save(path + "\\bmpDiffHisEqLIP" + subName + ".jpg");
          //  Bitmap bmpDiffBin = new Bitmap(PreProc.binary_Bmp(40, bmpDiff));
          //  bmpDiffBin.Save(path + "\\bmpDiffBin" + subName + ".jpg");
           // bmpDiff.Dispose();
          //  bmpDiffHis.Dispose();
          //  bmpDiffHisLIP.Dispose();
        //    bmpDiffBin.Dispose();

        }

    }
}
