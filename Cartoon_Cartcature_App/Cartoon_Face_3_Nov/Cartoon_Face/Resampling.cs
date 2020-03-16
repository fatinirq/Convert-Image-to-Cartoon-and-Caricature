using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cartoon_Face
{
    class Resampling
    {

        public double NearstNeighborKernel(double vv)
        {

            if (Math.Abs(vv) < 0.5)
                return 1;
            else
                return 0;
        }

        public double BilinearKernel(double vv)
        {

            if (Math.Abs(vv) < 1)
                return (1 - Math.Abs(vv));
            else
                return 0;
        }
        public void BilinerReSampling(byte[,] OrignalImage, ref byte[,] ResizedImage, int Ow, int Oh, ref int Nwid, ref int Nhgt, float Sf)
        {

            Nwid = (int)(Ow * Sf);
            Nhgt = (int)(Oh * Sf);
            ResizedImage = new byte[Nwid, Nhgt];

            float kx = (float)Ow / (float)Nwid;
            float ky = (float)Oh / (float)Nhgt;

            int kernel_size = 1;

            double sum = 0;
            for (int x = 0; x <= Nwid - 1; x++)
            {
                float fx = x * kx;
                int x1 = (int)(fx);
                fx = fx - x1;
                for (int y = 0; y <= Nhgt - 1; y++)
                {
                    float fy = y * ky;
                    int y1 = (int)(fy);
                    fy = fy - y1;
                    sum = 0;

                    for (int M = 0; M <= kernel_size; M++)
                    {
                        double R1 = BilinearKernel(M - fx);

                        for (int N = 0; N <= kernel_size; N++)
                        {
                            double R2 = BilinearKernel(N - fy);

                            sum = sum + (OrignalImage[x1 + M, y1 + N] * R1 * R2);

                        }

                    }
                    ResizedImage[x, y] = (byte)sum;

                }
            }
        }

        public void Nearst_NeighborResampling(byte[,] OrignalImage, ref byte[,] ResizedImage, int Ow, int Oh, ref int Nwid, ref int Nhgt, float Sf)
        {

            Nwid = (int)(Ow * Sf);
            Nhgt = (int)(Oh * Sf);
            ResizedImage = new byte[Nwid, Nhgt];

            float kx = (float)Ow / (float)Nwid;
            float ky = (float)Oh / (float)Nhgt;

            int kernel_size = 1;

            double sum = 0;
            for (int x = 0; x < Nwid; x++)
            {
                float fx = x * kx;
                int x1 = (int)(fx);
                fx = fx - x1;
                for (int y = 0; y < Nhgt; y++)
                {
                    float fy = y * ky;
                    int y1 = (int)(fy);
                    fy = fy - y1;
                    sum = 0;

                    for (int M = -kernel_size + 1; M < kernel_size + 1; M++)
                    {
                        double R1 = NearstNeighborKernel(M - fx);

                        for (int N = -kernel_size + 1; N < kernel_size + 1; N++)
                        {
                            double R2 = NearstNeighborKernel(N - fy);

                            sum = sum + (OrignalImage[x1 + M, y1 + N] * R1 * R2);

                        }

                    }
                    ResizedImage[x, y] = (byte)sum;

                }
            }

        }
    }
}
