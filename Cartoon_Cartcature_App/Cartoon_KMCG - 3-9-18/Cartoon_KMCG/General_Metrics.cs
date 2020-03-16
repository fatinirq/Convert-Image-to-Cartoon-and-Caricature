using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartoon_KMCG
{
    class General_Metrics
    {
        public static double PSNR(Bitmap bmpOriginal,Bitmap bmpSegmented)
        {
            double MSEValue = Math.Round(MSE(bmpOriginal, bmpSegmented),5);
            double PSNRValue=Math.Round( 10 * Math.Log10(Math.Pow(255,2)/MSEValue),2);
            return PSNRValue;

        }
       public static double MSE(Bitmap bmpOriginal, Bitmap bmpSegmented)
        {
            double sum = 0.0;
           
            for (int i = 0; i < bmpOriginal.Height; i++)
                for (int j = 0; j < bmpOriginal.Width; j++)
                    sum = sum + Math.Round((double)Math.Abs(bmpOriginal.GetPixel(j, i).R - bmpSegmented.GetPixel(j, i).R),5);
            double av = sum / (bmpOriginal.Height * bmpOriginal.Width);
            return av;

        }
        public static string entropy(Bitmap bmpOriginal, List<List<ChStruct.RGBWin>> lstClusters)
        {
            int[] histR = new int[256];
            double Hr = 0.0;
            double Hl = 0.0;
            double total = (bmpOriginal.Width - 2) * (bmpOriginal.Height - 2);
            double[] Ev = new double[lstClusters.Count];
            for(int k=0;k<lstClusters.Count;k++)
            {


                foreach (ChStruct.RGBWin tempItem in lstClusters[k])
                {
                    histR[ bmpOriginal.GetPixel(tempItem.y,tempItem.x).R]++;
                    histR[bmpOriginal.GetPixel(tempItem.y, tempItem.x+1).R]++;
                    histR[bmpOriginal.GetPixel(tempItem.y+1, tempItem.x).R]++;
                    histR[bmpOriginal.GetPixel(tempItem.y+1, tempItem.x+1).R]++;
                }
                double countR = lstClusters[k].Count * 4;
                
                for (int i = 0; i < 256 ; i++)
            if(histR[i]!=0)
                    Ev[k] = Ev[k] + (histR[i] / countR * Math.Log10((histR[i]/countR)+0.0001));
                Ev[k] = -1.0 * Ev[k];
                Hr = Hr + countR / total * Ev[k];
                Hl = Hl + countR / total * Math.Log10(countR / total);
                histR = new int[256];
            }
            Hl = -1 * Math.Round(Hl, 3);
            double entropy = 0.0;
            entropy = Hr + Hl;
            return entropy.ToString()+" Hr= "+Hr.ToString()+" Hl= "+Hl.ToString() ;
        }
    }
}
