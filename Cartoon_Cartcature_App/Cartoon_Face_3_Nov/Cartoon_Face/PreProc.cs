using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;

namespace Cartoon_Face
{
    class PreProc
    {
        static byte[,] bmpArr2,binArr;
        static int h, w;
        public static Bitmap shadowReduction(Bitmap bmp)
        {
            Bitmap subBmp = new Bitmap(bmp.Width, bmp.Height);
            double[,] arr = Filters.meanFilter_arr(bmp, 11);
            double min=200, max=0;
            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                    double newVal =( arr[i,j] - arr[i,j] + 128);
                    if (min > newVal)
                        min = newVal;
                    if (max < newVal)
                        max = newVal;
                   
                }
            MessageBox.Show("Min=" + min + " Max=" + max);
            Bitmap stretchImage = new Bitmap(bmp.Width, bmp.Height);
            return subBmp;
        }
        public static Bitmap contrastStretching(Bitmap Bmp)
        {
            w = Bmp.Width;
            h = Bmp.Height;
            ImageEnhancement e = new ImageEnhancement();
            byte[,] bmpArr = ImageEnhancement.convert2GrayArr(Bmp);
            bmpArr2=new byte[Bmp.Height,Bmp.Width];
            e.LinearStreching(Bmp.Width,Bmp.Height,bmpArr,ref bmpArr2,1.1f);
            Bitmap bmpOut = ImageEnhancement.convertArr2Gray(bmpArr2, Bmp.Height, Bmp.Width);
            return bmpOut;
        }
        public static Bitmap hisEqua(Bitmap Bmp)
        {
            w = Bmp.Width;
            h = Bmp.Height;
            ImageEnhancement e = new ImageEnhancement();
            byte[,] bmpArr = ImageEnhancement.convert2GrayArr(Bmp);
            bmpArr2 = new byte[Bmp.Height, Bmp.Width];
            e.Histogram_Equalization(Bmp.Width, Bmp.Height, bmpArr, ref bmpArr2,8,7);
            Bitmap bmpOut = ImageEnhancement.convertArr2Gray(bmpArr2, Bmp.Height, Bmp.Width);
            return bmpOut;
        }
        public static Bitmap hisEqua(Bitmap Bmp,List<System.Drawing.Point> lstPoint)
        {
            w = Bmp.Width;
            h = Bmp.Height;
            ImageEnhancement e = new ImageEnhancement();
            byte[,] bmpArr = ImageEnhancement.convert2GrayArr(Bmp);
            bmpArr2 = new byte[Bmp.Height, Bmp.Width];
            e.Histogram_Equalization(Bmp.Width, Bmp.Height, bmpArr, ref bmpArr2, 8, 7, lstPoint);
            Bitmap bmpOut = ImageEnhancement.convertArr2Gray(bmpArr2, Bmp.Height, Bmp.Width);
            return bmpOut;
        }
        public static Bitmap localcontrastStretching(Bitmap Bmp)
        {
            w = Bmp.Width;
            h = Bmp.Height;
            ImageEnhancement e = new ImageEnhancement();
            byte[,] bmpArr = ImageEnhancement.convert2GrayArr(Bmp);
            bmpArr2 = new byte[Bmp.Height, Bmp.Width];
            e.Local_ContrastStreching(Bmp.Width, Bmp.Height, bmpArr, ref bmpArr2, 0.8f,1.2f,20);
            Bitmap bmpOut = ImageEnhancement.convertArr2Gray(bmpArr2, Bmp.Height, Bmp.Width);
            return bmpOut;
        }
        public  static Bitmap binary_Bmp(int th)
        {
            Bitmap bmpOut=new Bitmap(w, h);
            binArr = new byte[h, w];
            for (int i=0;i<h;i++)
                for(int j=0;j< w;j++)
                {
                    if (bmpArr2[i, j] < th)
                        binArr[i, j] = 0;
                    else binArr[i, j] = 255;

                }
            bmpOut=ImageEnhancement.convertArr2Gray(binArr,h,w);
            return bmpOut;

        }
        public static Bitmap binary_Bmp(int th,Bitmap bmpIn)
        {
            Bitmap bmpOut = new Bitmap(bmpIn.Width, bmpIn.Height);
            binArr = new byte[bmpIn.Height, bmpIn.Width];
            for (int i = 0; i < bmpIn.Height; i++)
                for (int j = 0; j < bmpIn.Width; j++)
                {
                    if (bmpIn.GetPixel(j,i).R< th)
                        binArr[i, j] = 0;
                    else binArr[i, j] = 255;

                }
            bmpOut = ImageEnhancement.convertArr2Gray(binArr,bmpIn.Height,bmpIn.Width);
            return bmpOut;
        }
        

    }
}
