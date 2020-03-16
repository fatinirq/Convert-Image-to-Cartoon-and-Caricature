using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;


namespace Cartoon_Face
{
    class SeamCurveByFatin
    {
        public class ColorDataPostion
        {
            public Color clr = new Color();
            public Point p = new Point();
            public bool flag = false;
        }
        int Width, Height;
        byte[,] Data;
        Color[,] DataColorShift;
        Color[,] DataColor;


        public List<Seam> lstHor = new List<Seam>();
        public List<Seam> lstVer = new List<Seam>();
       
        
        public SeamCurveByFatin( Bitmap bmp, Bitmap gray, int BZ, Point P1, Point P2,Point p1ROI,Point p2ROI,Rectangle rec, int ca, string filename, int flag)
        {
            string subjectsDir=filename, changeDetectDir=filename+"/changeDetectDir";
            if (!Directory.Exists(subjectsDir))
            {
                Directory.CreateDirectory(subjectsDir);
            }
            if (!Directory.Exists(changeDetectDir))
            {
                Directory.CreateDirectory(changeDetectDir);
            }
            Bitmap bmpOrg = new Bitmap(bmp);
            Width = p2ROI.Y-p1ROI.Y;
            Height = p2ROI.X-p1ROI.X;
            Data = new byte[Height, Width];
            DataColor = new Color[Height, Width];
            DataColorShift = new Color[Height, Width];
            DetectROIChange dd;
           // DataColorShiftOrg
            Point pViola = new Point(rec.Top, rec.Left);
            for (int i = p1ROI.X + pViola.X; i < p2ROI.X + pViola.X; i++)
                for (int j = p1ROI.Y + pViola.Y; j < p2ROI.Y + pViola.Y; j++)
                {
                    Data[i - p1ROI.X - pViola.X, j - p1ROI.Y - pViola.Y] = gray.GetPixel(j, i).R;
                    DataColor[i - p1ROI.X - pViola.X, j - p1ROI.Y - pViola.Y] = bmp.GetPixel(j, i);
                    DataColorShift[i - p1ROI.X - pViola.X, j - p1ROI.Y - pViola.Y] = bmp.GetPixel(j, i);
                }
            energy = new float[Height, Width];
            P1.X = P1.X - p1ROI.X;
            P1.Y = P1.Y - p1ROI.Y;
            P2.X = P2.X - p1ROI.X;
            P2.Y = P2.Y - p1ROI.Y;
            findEnergy();
            findHorSeams(BZ);
            findVerSeams(BZ);
            Bitmap verSeamsBmp = new Bitmap(bmp);
            Bitmap horSeamsBmp = new Bitmap(bmp);
            
            if (ca == 0)
            {
                
                shiftSeamsVerticalCase1(100, P1, P2);

                if (flag == 0)
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_0_N",P1,P2);
                   // Filters.Median(DataColorShift, dd.arrRegions);
                   // Filters.meanFilter_Color(DataColorShift, 3);
                   // Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpOrg.SetPixel(j + p1ROI.Y + pViola.Y, i + p1ROI.X + pViola.X, DataColorShift[i, j]);
                        }
                    Filters.Median(bmpOrg, p1ROI, p2ROI);
                    bmpOrg.Save(subjectsDir + "//bmpCa_0_N.jpg");
                    
                    bmpOrg.Dispose();
                }
                else

                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_0_M",P1,P2);
                   // Filters.Median(DataColorShift, dd.arrRegions);
                   // Filters.meanFilter_Color(DataColorShift, 3);
                  //   Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpOrg.SetPixel(j + p1ROI.Y + pViola.Y, i + p1ROI.X + pViola.X, DataColorShift[i, j]);
                        }
                    Filters.Median(bmpOrg, p1ROI, p2ROI);
                    bmpOrg.Save(subjectsDir + "//bmpCa_0_M.jpg");
                    
                   
                    bmpOrg.Dispose();
                }
            }
            else if (ca == 1)
            {
                
                shiftSeamsHorizontalCase1(100, P1, P2);
                if (flag == 0)
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_1_N", P1, P2);
                    //Filters.Median(DataColorShift, dd.arrRegions);
                    //Filters.meanFilter_Color(DataColorShift, 3);
                    //Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpOrg.SetPixel(j + p1ROI.Y + pViola.Y, i + p1ROI.X + pViola.X, DataColorShift[i, j]);
                        }
                    Filters.Median(bmpOrg, p1ROI, p2ROI);
                    bmpOrg.Save(subjectsDir + "//bmpCa_1_N.jpg");
                    bmpOrg.Dispose();
                    

                }
                else
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_1_M", P1, P2);
                    //Filters.Median(DataColorShift, dd.arrRegions);
                    //Filters.meanFilter_Color(DataColorShift, 3);
                   // Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpOrg.SetPixel(j + p1ROI.Y + pViola.Y, i + p1ROI.X + pViola.X, DataColorShift[i, j]);
                        }
                    Filters.Median(bmpOrg, p1ROI, p2ROI);
                    bmpOrg.Save(subjectsDir + "//bmpCa_1_M.jpg");
                    bmpOrg.Dispose();
                    
                }
            }
            else if (ca == 2)
            {
                
                shiftSeamsVerticalCase1(100, P1, P2);
                shiftSeamsHorizontalCase1(100, P1, P2);
                if (flag == 0)
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_2_N", P1, P2);
                    //Filters.Median(DataColorShift, dd.arrRegions);
                    //Filters.meanFilter_Color(DataColorShift, 3);
                    //Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpOrg.SetPixel(j + p1ROI.Y + pViola.Y, i + p1ROI.X + pViola.X, DataColorShift[i, j]);
                        }
                    Filters.Median(bmpOrg, p1ROI, p2ROI);
                    bmpOrg.Save(subjectsDir + "//bmpCa_2_N.jpg");
                    bmpOrg.Dispose();
                    
                }
                else
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_2_M", P1, P2);
                    //Filters.Median(DataColorShift, dd.arrRegions);
                    //Filters.meanFilter_Color(DataColorShift, 3);
                    //Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpOrg.SetPixel(j + p1ROI.Y + pViola.Y, i + p1ROI.X + pViola.X, DataColorShift[i, j]);
                        }
                    Filters.Median(bmpOrg, p1ROI, p2ROI);
                    bmpOrg.Save(subjectsDir + "//bmpCa_2_M.jpg");
                    bmpOrg.Dispose();
                   
                }
            }
            else if (ca == 3)
            {
                
                shiftSeamsVerticalCase2(100, P1, P2);
                
                if (flag == 0)
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_3_N", P1, P2);
                    //Filters.Median(DataColorShift, dd.arrRegions);
                    //Filters.meanFilter_Color(DataColorShift, 3);
                    //Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpOrg.SetPixel(j + p1ROI.Y + pViola.Y, i + p1ROI.X + pViola.X, DataColorShift[i, j]);
                        }
                    Filters.Median(bmpOrg, p1ROI, p2ROI);
                    bmpOrg.Save(subjectsDir + "//bmpCa_3_N.jpg");
                    bmpOrg.Dispose();
                }
                else
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_3_M", P1, P2);
                    //Filters.Median(DataColorShift, dd.arrRegions);
                    //Filters.meanFilter_Color(DataColorShift, 3);
                    //Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpOrg.SetPixel(j + p1ROI.Y + pViola.Y, i + p1ROI.X + pViola.X, DataColorShift[i, j]);
                        }
                    Filters.Median(bmpOrg, p1ROI, p2ROI);
                    bmpOrg.Save(subjectsDir + "//bmpCa_3_M.jpg");
                    bmpOrg.Dispose();

                }
            }
            else if (ca == 4)
            {
                
                shiftSeamsHorizontalCase2(100, P1, P2);
                if (flag == 0)
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_4_N", P1, P2);
                    //Filters.Median(DataColorShift, dd.arrRegions);
                    //Filters.meanFilter_Color(DataColorShift, 3);
                  //  Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpOrg.SetPixel(j + p1ROI.Y + pViola.Y, i + p1ROI.X + pViola.X, DataColorShift[i, j]);
                        }
                    Filters.Median(bmpOrg, p1ROI, p2ROI);
                    bmpOrg.Save(subjectsDir + "//bmpCa_4_N.jpg");
                    bmpOrg.Dispose();

                }
                else
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_4_M", P1, P2);
                    //Filters.Median(DataColorShift, dd.arrRegions);
                    //Filters.meanFilter_Color(DataColorShift, 3);
                    //Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpOrg.SetPixel(j + p1ROI.Y + pViola.Y, i + p1ROI.X + pViola.X, DataColorShift[i, j]);
                        }
                    Filters.Median(bmpOrg, p1ROI, p2ROI);
                    bmpOrg.Save(subjectsDir + "//bmpCa_4_M.jpg");
                    bmpOrg.Dispose();
                }
            }
            else if (ca == 5)
            {             
                shiftSeamsVerticalCase2(100, P1, P2);
                shiftSeamsHorizontalCase2(100, P1, P2);
                if (flag == 0)
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_5_N", P1, P2);
                    //Filters.Median(DataColorShift, dd.arrRegions);
                    //Filters.meanFilter_Color(DataColorShift, 3);
                    //Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpOrg.SetPixel(j + p1ROI.Y + pViola.Y, i +p1ROI.X + pViola.X, DataColorShift[i, j]);
                        }
                    Filters.Median(bmpOrg, p1ROI, p2ROI);
                    bmpOrg.Save(subjectsDir + "//bmpCa_5_N.jpg");
                    bmpOrg.Dispose();
                }
                else
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_5_M", P1, P2);
                  //  Filters.Median(DataColorShift, dd.arrRegions);
                   // Filters.meanFilter_Color(DataColorShift, 3);
                   // Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpOrg.SetPixel(j + p1ROI.Y + pViola.Y, i + p1ROI.X + pViola.X, DataColorShift[i, j]);
                        }
                    Filters.Median(bmpOrg, p1ROI, p2ROI);
                       
                    bmpOrg.Save(subjectsDir + "//bmpCa_5_M.jpg");
                    bmpOrg.Dispose();

                }
            }
            if (flag == 1)
            {
                foreach (Seam s in lstHor)
                    foreach (Point p in s.lstPoint)
                        horSeamsBmp.SetPixel(p.Y + p1ROI.Y + pViola.Y, p.X + p1ROI.X + pViola.X, Color.Red);
                horSeamsBmp.Save(filename + "//horSeamsN.JPG");
                foreach (Seam s in lstVer)
                    foreach (Point p in s.lstPoint)
                        verSeamsBmp.SetPixel(p.Y + p1ROI.Y + pViola.Y, p.X + p1ROI.X + pViola.X, Color.Green);
                verSeamsBmp.Save(filename + "//verSeamsN.JPG");
            }
        }
        public SeamCurveByFatin(FaceBlobDtetction.IntrestR intRegion, Bitmap bmp, Bitmap gray, int BZ, Point P1, Point P2, Rectangle rec, int ca, string filename,int flag)
        {
            string subjectsDir = filename , changeDetectDir = filename + "\\changeDetectDir";
            if (!Directory.Exists(subjectsDir))
            {
                Directory.CreateDirectory(subjectsDir);
            }
            if (!Directory.Exists(changeDetectDir))
            {
                Directory.CreateDirectory(changeDetectDir);
            }
            Width = intRegion.arr.GetLength(1);
            Height = intRegion.arr.GetLength(0);
            Data = new byte[Height, Width];
            DataColorShift = new Color[Height, Width];
            Point pViola = new Point(rec.Top, rec.Left);
            for (int i = intRegion.p.X + pViola.X; i < pViola.X+Height+intRegion.p.X; i++)
                for (int j = intRegion.p.Y + pViola.Y; j < pViola.Y + Width + intRegion.p.Y; j++)
                {
                    Data[i - intRegion.p.X - pViola.X, j - intRegion.p.Y - pViola.Y] = gray.GetPixel(j, i).R;
                    DataColorShift[i - intRegion.p.X - pViola.X, j - intRegion.p.Y - pViola.Y] = bmp.GetPixel(j, i);
                }
           energy = new float[Height, Width];
           findEnergy();
           findHorSeams(BZ);
           findVerSeams(BZ);
           Bitmap verSeamsBmp=new Bitmap(bmp);
           Bitmap horSeamsBmp = new Bitmap(bmp);
            DetectROIChange dd;
           // int 0 = (int)(P1.X - BZ / 20.0 * (P2.X - P1.X)), Height = (int)(P2.X + BZ / 20.0 * (P2.X - P1.X)), 0 = (int)(P1.Y - BZ / 20.0 * (P2.Y - P1.Y)), Width = (int)(P2.Y + (BZ / 20.0 * (P2.Y - P1.Y)));
             
           
            if (ca == 0)
            {
                

                Bitmap bmpca0_A = new Bitmap(bmp);
                Bitmap bmpca0_B = new Bitmap(bmp);
                shiftSeamsVerticalCase1(100, P1, P2);
                if (flag == 0)
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_0_A", P1, P2);
                   // Filters.Median(DataColorShift, dd.arrRegions);
                   // Filters.meanFilter_Color(DataColorShift, 3);
                  //  Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpca0_A.SetPixel(j + intRegion.p.Y + pViola.Y, i + intRegion.p.X + pViola.X, DataColorShift[i, j]);
                        }
                    bmpca0_A.Save(subjectsDir + "//bmpCa_0_A.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    bmpca0_A.Dispose();
                }
                else
                   
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_0_B", P1, P2);
                   // Filters.Median(DataColorShift, dd.arrRegions);
                   // Filters.meanFilter_Color(DataColorShift, 3);
                   // Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpca0_B.SetPixel(j + intRegion.p.Y + pViola.Y, i + intRegion.p.X + pViola.X, DataColorShift[i, j]);
                        }
                    Filters.Median(bmpca0_B, new Point(intRegion.p.X + pViola.X, intRegion.p.Y + pViola.Y),
                        new Point(intRegion.p.X + pViola.X + Height, intRegion.p.Y + pViola.Y + Width));
                    bmpca0_B.Save(subjectsDir + "//bmpCa_0_B.jpg");
                    bmpca0_B.Dispose();
                }
            }
            else if (ca == 1)
            {
                
                Bitmap bmpca2_A = new Bitmap(bmp);
                Bitmap bmpca2_B = new Bitmap(bmp);
                shiftSeamsHorizontalCase1(100, P1, P2);
                if (flag == 0)
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_1_A", P1, P2);
                  //  Filters.Median(DataColorShift, dd.arrRegions);
                  //  Filters.meanFilter_Color(DataColorShift, 3);
                  //  Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpca2_A.SetPixel(j + intRegion.p.Y + pViola.Y, i + intRegion.p.X + pViola.X, DataColorShift[i, j]);
                        }
                    bmpca2_A.Save(filename + "//bmpCa_1_A.jpg");
                    bmpca2_A.Dispose();
                }
                else
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_1_B", P1, P2);
                   // Filters.Median(DataColorShift, dd.arrRegions);
                   // Filters.meanFilter_Color(DataColorShift, 3);
                  //  Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)

                                bmpca2_B.SetPixel(j + intRegion.p.Y + pViola.Y, i + intRegion.p.X + pViola.X, DataColorShift[i, j]);
                        }
                    Filters.Median(bmpca2_B, new Point(intRegion.p.X + pViola.X, intRegion.p.Y + pViola.Y),
                        new Point(intRegion.p.X + pViola.X + Height, intRegion.p.Y + pViola.Y + Width));
                    bmpca2_B.Save(subjectsDir + "//bmpCa_1_B.jpg");
                    bmpca2_B.Dispose();
                }
            }
            else if(ca==2)
            {

                Bitmap bmpca3_A = new Bitmap(bmp);
                Bitmap bmpca3_B = new Bitmap(bmp);
                shiftSeamsVerticalCase1(100, P1, P2);
                shiftSeamsHorizontalCase1(100, P1, P2);
                if (flag == 0)
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_2_A", P1, P2);
                 //   Filters.Median(DataColorShift, dd.arrRegions);
                //    Filters.meanFilter_Color(DataColorShift, 3);
               //     Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpca3_A.SetPixel(j + intRegion.p.Y + pViola.Y, i + intRegion.p.X + pViola.X, DataColorShift[i, j]);
                        }
                    bmpca3_A.Save(subjectsDir + "//bmpCa_2_A.jpg");
                    bmpca3_A.Dispose();
                }
                else
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_2_B", P1, P2);
               //     Filters.Median(DataColorShift, dd.arrRegions);
                //    Filters.meanFilter_Color(DataColorShift, 3);
                //    Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpca3_B.SetPixel(j + intRegion.p.Y + pViola.Y, i + intRegion.p.X + pViola.X, DataColorShift[i, j]);
                        }
                    Filters.Median(bmpca3_B, new Point(intRegion.p.X + pViola.X, intRegion.p.Y + pViola.Y),
                        new Point(intRegion.p.X + pViola.X + Height, intRegion.p.Y + pViola.Y + Width));
                    bmpca3_B.Save(subjectsDir + "//bmpCa_2_B.jpg");
                    bmpca3_B.Dispose();
                }
            }
            else if (ca == 3)
            {
                Bitmap bmpca4_A = new Bitmap(bmp);
                Bitmap bmpca4_B = new Bitmap(bmp);
                shiftSeamsVerticalCase2(100, P1, P2);
                if (flag == 0)
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_3_A", P1, P2);
                 //   Filters.Median(DataColorShift, dd.arrRegions);
                //    Filters.meanFilter_Color(DataColorShift, 3);
               //     Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpca4_A.SetPixel(j + intRegion.p.Y + pViola.Y, i + intRegion.p.X + pViola.X, DataColorShift[i, j]);
                        }
                    bmpca4_A.Save(subjectsDir + "//bmpCa_3_A.jpg");
                    bmpca4_A.Dispose();
                }
                else
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_3_B", P1, P2);
                //    Filters.Median(DataColorShift, dd.arrRegions);
                 //   Filters.meanFilter_Color(DataColorShift, 3);
                 //   Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpca4_B.SetPixel(j + intRegion.p.Y + pViola.Y, i + intRegion.p.X + pViola.X, DataColorShift[i, j]);
                        }
                    Filters.Median(bmpca4_B, new Point(intRegion.p.X + pViola.X, intRegion.p.Y + pViola.Y),
                        new Point(intRegion.p.X + pViola.X + Height, intRegion.p.Y + pViola.Y + Width));
                    bmpca4_B.Save(subjectsDir + "//bmpCa_3_B.jpg");
                    bmpca4_B.Dispose();

                }
            }
            else if (ca == 4)
            {
                Bitmap bmpca5_A = new Bitmap(bmp);
                Bitmap bmpca5_B = new Bitmap(bmp);
                shiftSeamsHorizontalCase2(100, P1, P2);
                if (flag == 0)
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_4_A", P1, P2);
             //       Filters.Median(DataColorShift, dd.arrRegions);
             //       Filters.meanFilter_Color(DataColorShift, 3);
              //      Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpca5_A.SetPixel(j + intRegion.p.Y + pViola.Y, i + intRegion.p.X + pViola.X, DataColorShift[i, j]);
                        }

                    bmpca5_A.Save(subjectsDir + "//bmpCa_4_A.jpg");
                    bmpca5_A.Dispose();
                }
                else
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_4_B", P1, P2);
               //     Filters.Median(DataColorShift, dd.arrRegions);
                //    Filters.meanFilter_Color(DataColorShift, 3);
                 //   Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpca5_B.SetPixel(j + intRegion.p.Y + pViola.Y, i + intRegion.p.X + pViola.X, DataColorShift[i, j]);
                        }
                    Filters.Median(bmpca5_B, new Point(intRegion.p.X + pViola.X, intRegion.p.Y + pViola.Y),
                        new Point(intRegion.p.X + pViola.X + Height, intRegion.p.Y + pViola.Y + Width));
                    bmpca5_B.Save(subjectsDir + "//bmpCa_4_B.jpg");
                    bmpca5_B.Dispose();
                }
            }
            else if (ca == 5)
            {
                Bitmap bmpca6_A = new Bitmap(bmp);
                Bitmap bmpca6_B = new Bitmap(bmp);
                shiftSeamsVerticalCase2(100, P1, P2);
                shiftSeamsHorizontalCase2(100, P1, P2);
                if (flag == 0)
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_5_A", P1, P2);
               //     Filters.Median(DataColorShift, dd.arrRegions);
               //     Filters.meanFilter_Color(DataColorShift, 3);
               //     Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i, j] != 0)
                                bmpca6_A.SetPixel(j + intRegion.p.Y + pViola.Y, i + intRegion.p.X + pViola.X, DataColorShift[i, j]);
                        }
                    bmpca6_A.Save(subjectsDir + "//bmpCa_5_A.jpg");
                    bmpca6_A.Dispose();
                }
                else
                {
                    dd = new DetectROIChange(DataColorShift, Data, changeDetectDir, "_5_B", P1, P2);
                   // Filters.Median(DataColorShift, dd.arrRegions);
                   // Filters.meanFilter_Color(DataColorShift, 3);
                   // Filters.meanFilter_Color(DataColorShift, 3);
                    for (int i = 0; i <Height; i++)
                        for (int j = 0; j <Width; j++)
                        {
                            if (dd.arrRegions[i,j]!=0)
                                bmpca6_B.SetPixel(j + intRegion.p.Y + pViola.Y, i + intRegion.p.X + pViola.X, DataColorShift[i, j]);
                        }
                    Filters.Median(bmpca6_B, new Point(intRegion.p.X + pViola.X, intRegion.p.Y + pViola.Y),
                        new Point(intRegion.p.X + pViola.X + Height, intRegion.p.Y + pViola.Y + Width));
                    bmpca6_B.Save(subjectsDir + "//bmpCa_5_B.jpg");
                    bmpca6_B.Dispose();

                }
            }

            foreach (Seam s in lstHor)
                foreach(Point p in s.lstPoint)
                horSeamsBmp.SetPixel(p.Y + intRegion.p.Y + pViola.Y, p.X + intRegion.p.X + pViola.X,Color.Red);
            horSeamsBmp.Save(filename + "//horSeams.JPG");
            foreach (Seam s in lstVer)
                foreach (Point p in s.lstPoint)
                    verSeamsBmp.SetPixel(p.Y + intRegion.p.Y + pViola.Y, p.X + intRegion.p.X + pViola.X, Color.Green);
            verSeamsBmp.Save(filename + "//verSeams.JPG");
        }
        public class Seam
        {
            public List<Point> lstPoint = new List<Point>();
            public List<float> lstEnergy = new List<float>();
            public float avEnergy = new float();
            int count = 1;
            int areaInd;
        }
        public class PointCount
        {
            Point p = new Point();
            int count;
        }
        class Diff
        {
            List<Point> lstPoints = new List<Point>();
            List<float> lstValues = new List<float>();
            public Diff(int x, int y, float[,] energy)
            {
                for (int i = -1; i <= 1; i++)
                    for (int j = -1; j <= 1; j++)
                    {
                        Point p = new Point();
                        int v = new int();
                        p.X = x + i;
                        p.Y = y + j;
                        v = (int)(energy[x, y] - energy[p.X, p.Y]);
                        lstPoints.Add(p);
                        lstValues.Add(v);
                    }
            }
        }
        float[,] energy;
        Diff[,] diff;
        public void findEnergy()
        {
            int[,] Dx = {{-1,0,1},
                         {-2,0,2},
                         {-1,0,1}};

            int[,] Dy = {{1,2,1},
                         {0,0,0},
                         {-1,-2,-1}};
            float[,] DerivativeX = new float[Height, Width];
            float[,] DerivativeY = new float[Height, Width];
            DerivativeX = Differentiate(Data, Dx);
            DerivativeY = Differentiate(Data, Dy);
            int i, j;
            Bitmap bmpEnergy = new Bitmap(Width, Height);
            //Compute the gradient magnitude based on derivatives in x and y:
            for (i = 0; i <= (Height - 1); i++)
            {
                for (j = 0; j <= (Width - 1); j++)
                {
                    energy[i, j] = (float)Math.Sqrt((DerivativeX[i, j] * DerivativeX[i, j]) + (DerivativeY[i, j] * DerivativeY[i, j]));
                    int en=0;
                    if (energy[i, j] > 255)
                        en = 255;
                    else if (energy[i, j] < 0)
                        en = 0;
                    else
                        en = (int)energy[i, j];
                    bmpEnergy.SetPixel(j, i, Color.FromArgb(255, en,en,en));
                    
                }

            }
            bmpEnergy.Save( "energy.JPG");
            bmpEnergy.Dispose();
        }
        public void findEnergyDiff()
        {
            for (int i = 1; i < Width - 1; i++)
                for (int j = 1; j < Height - 1; j++)
                    diff[i, j] = new Diff(i, j, energy);

        }
        public void findHorSeams(int blockSize)
        {
            
            for (int i = 1; i < Height - 1; i = i + blockSize)
            {
                Seam seam = new Seam();
                traverseHor(i, 1, blockSize, seam);
                seam.avEnergy = seam.lstEnergy.Average();
                lstHor.Add(seam);

            }
        }
        public void findVerSeams(int blockSize)
        { 
            for (int i = 1; i < Width - 1; i = i + blockSize)
            {
                Seam seam = new Seam();
                traverseVer(1, i, blockSize, seam);
                seam.avEnergy = seam.lstEnergy.Average();
                lstVer.Add(seam);
           }
        }

        public void shiftSeamsHorizontalCase1(int ratio, Point p1, Point p2)
        {
            int count = 0;
            List<Seam> lstShrink = new List<Seam>();
            List<Seam> lstStretch = new List<Seam>();
            int[] arr = new int[lstHor.Count];
            for (int i = 0; i < lstHor.Count; i++)
                if (lstHor[i].lstPoint[0].X <= p2.X && lstHor[i].lstPoint[0].X >= p1.X)
                {
                    arr[i] = 1;
                    count++;
                    lstShrink.Add(lstHor[i]);
                }
                else
                {
                    arr[i] = 0;
                    lstStretch.Add(lstHor[i]);
                }
            int countShrink = lstShrink.Count;
            int countStretch = lstStretch.Count;
            int flag = 0;

            List<List<PointCount>> dataPoints = new List<List<PointCount>>();
            int[,] dataPointsCount = new int[Height, Width];
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                {
                    dataPointsCount[i, j] = 1;
                }
            while (lstShrink.Count / (100 / ratio) > 0 && lstStretch.Count / (100 / ratio) > 0)
            {
                if (flag == 0)
                {
                    foreach (Point p in lstStretch[0].lstPoint)
                        dataPointsCount[p.X, p.Y] = 2;
                    foreach (Point p in lstShrink[0].lstPoint)
                        dataPointsCount[p.X, p.Y] = 0;
                    lstShrink.RemoveAt(0);
                    lstStretch.RemoveAt(0);

                }

                else if (flag == 1)
                {
                    foreach (Point p in lstStretch[lstStretch.Count / 2].lstPoint)
                        dataPointsCount[p.X, p.Y] = 2;
                    foreach (Point p in lstShrink[lstShrink.Count / 2].lstPoint)
                        dataPointsCount[p.X, p.Y] = 0;
                    lstShrink.RemoveAt(lstShrink.Count / 2);
                    lstStretch.RemoveAt(lstStretch.Count / 2);

                }
                else if (flag == 2)
                {
                    foreach (Point p in lstStretch[lstStretch.Count - 1].lstPoint)
                        dataPointsCount[p.X, p.Y] = 2;
                    foreach (Point p in lstShrink[lstShrink.Count - 1].lstPoint)
                        dataPointsCount[p.X, p.Y] = 0;
                    lstShrink.RemoveAt(lstShrink.Count - 1);
                    lstStretch.RemoveAt(lstStretch.Count - 1);

                }
                
                flag = (flag + 1) % 3;
            }
            for (int j = 0; j < Width; j++)
            {
                List<Color> lstColor = new List<Color>();
                for (int i = 0; i < Height; i++)
                {
                    if (dataPointsCount[i, j] == 1)
                        lstColor.Add(DataColorShift[i, j]);
                    else if (dataPointsCount[i, j] == 2)
                    {
                        lstColor.Add(DataColorShift[i, j]);
                        lstColor.Add(DataColorShift[i, j]);
                    }
                }
                for (int i = 0; i < Height; i++)
                    DataColorShift[i, j] = lstColor[i];
            }
        }
        public void shiftSeamsHorizontalCase2(int ratio, Point p1, Point p2)
        {
            int count = 0;
            List<Seam> lstShrink = new List<Seam>();
            List<Seam> lstStretch = new List<Seam>();
            int[] arr = new int[lstHor.Count];
            for (int i = 0; i < lstHor.Count; i++)
                if (lstHor[i].lstPoint[0].X <= p2.X && lstHor[i].lstPoint[0].X >= p1.X)
                {
                    arr[i] = 1;
                    count++;
                    lstStretch.Add(lstHor[i]);
                }
                else
                {
                    arr[i] = 0;
                    lstShrink.Add(lstHor[i]);
                }
            int countShrink = lstShrink.Count;
            int countStretch = lstStretch.Count;
            int flag = 0;

            List<List<PointCount>> dataPoints = new List<List<PointCount>>();
            int[,] dataPointsCount = new int[Height, Width];
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                {
                    dataPointsCount[i, j] = 1;
                }
            while (lstShrink.Count / (100 / ratio) > 0 && lstStretch.Count / (100 / ratio) > 0)
            {
                if (flag == 0)
                {
                    foreach (Point p in lstStretch[0].lstPoint)
                        dataPointsCount[p.X, p.Y] = 2;
                    foreach (Point p in lstShrink[0].lstPoint)
                        dataPointsCount[p.X, p.Y] = 0;
                    lstShrink.RemoveAt(0);
                    lstStretch.RemoveAt(0);

                }

                else if (flag == 1)
                {
                    foreach (Point p in lstStretch[lstStretch.Count / 2].lstPoint)
                        dataPointsCount[p.X, p.Y] = 2;
                    foreach (Point p in lstShrink[lstShrink.Count / 2].lstPoint)
                        dataPointsCount[p.X, p.Y] = 0;
                    lstShrink.RemoveAt(lstShrink.Count / 2);
                    lstStretch.RemoveAt(lstStretch.Count / 2);

                }
                else if (flag == 2)
                {
                    foreach (Point p in lstStretch[lstStretch.Count - 1].lstPoint)
                        dataPointsCount[p.X, p.Y] = 2;
                    foreach (Point p in lstShrink[lstShrink.Count - 1].lstPoint)
                        dataPointsCount[p.X, p.Y] = 0;
                    lstShrink.RemoveAt(lstShrink.Count - 1);
                    lstStretch.RemoveAt(lstStretch.Count - 1);

                }
                flag = (flag + 1) % 3;
            }
            for (int j = 0; j < Width; j++)
            {
                List<Color> lstColor = new List<Color>();
                for (int i = 0; i < Height; i++)
                {

                    if (dataPointsCount[i, j] == 1)
                        lstColor.Add(DataColorShift[i, j]);
                    else if (dataPointsCount[i, j] == 2)
                    {
                        lstColor.Add(DataColorShift[i, j]);
                        lstColor.Add(DataColorShift[i, j]);
                    }
                }
                for (int i = 0; i < Height; i++)
                    DataColorShift[i, j] = lstColor[i];

            }
        }
        public void shiftSeamsVerticalCase1(int ratio, Point p1, Point p2)
        {
            int count = 0;
            List<Seam> lstShrink = new List<Seam>();
            List<Seam> lstStretch = new List<Seam>();
            int[] arr = new int[lstVer.Count];
            for (int i = 0; i < lstVer.Count; i++)
                if (lstVer[i].lstPoint[0].Y <= p2.Y && lstVer[i].lstPoint[0].Y >= p1.Y)
                {
                    arr[i] = 1;
                    count++;
                    lstShrink.Add(lstVer[i]);
                }
                else
                {
                    arr[i] = 0;
                    lstStretch.Add(lstVer[i]);
                }
            int countShrink = lstShrink.Count;
            int countStretch = lstStretch.Count;
            int flag = 0;

            List<List<PointCount>> dataPoints = new List<List<PointCount>>();
            int[,] dataPointsCount = new int[Height, Width];
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                {
                    dataPointsCount[i, j] = 1;
                }
            while (lstShrink.Count / (100 / ratio) > 0 && lstStretch.Count / (100 / ratio) > 0)
            {
                if (flag == 0)
                {
                    foreach (Point p in lstStretch[0].lstPoint)
                        dataPointsCount[p.X, p.Y] = 2;
                    foreach (Point p in lstShrink[0].lstPoint)
                        dataPointsCount[p.X, p.Y] = 0;
                    lstShrink.RemoveAt(0);
                    lstStretch.RemoveAt(0);

                }

                else if (flag == 1)
                {
                    foreach (Point p in lstStretch[lstStretch.Count / 2].lstPoint)
                        dataPointsCount[p.X, p.Y] = 2;
                    foreach (Point p in lstShrink[lstShrink.Count / 2].lstPoint)
                        dataPointsCount[p.X, p.Y] = 0;
                    lstShrink.RemoveAt(lstShrink.Count / 2);
                    lstStretch.RemoveAt(lstStretch.Count / 2);

                }
                else if (flag == 2)
                {
                    foreach (Point p in lstStretch[lstStretch.Count - 1].lstPoint)
                        dataPointsCount[p.X, p.Y] = 2;
                    foreach (Point p in lstShrink[lstShrink.Count - 1].lstPoint)
                        dataPointsCount[p.X, p.Y] = 0;
                    lstShrink.RemoveAt(lstShrink.Count - 1);
                    lstStretch.RemoveAt(lstStretch.Count - 1);

                }
               
                
                flag = (flag + 1) % 3;
            }
            for (int i = 0; i < Height; i++)
            {
                List<Color> lstColor = new List<Color>();
                for (int j = 0; j < Width; j++)
                {

                    if (dataPointsCount[i, j] == 1)
                        lstColor.Add(DataColorShift[i, j]);
                    else if (dataPointsCount[i, j] == 2)
                    {
                        lstColor.Add(DataColorShift[i, j]);
                        lstColor.Add(DataColorShift[i, j]);
                    }
                }
                for (int j = 0; j < Width; j++)
                    DataColorShift[i, j] = lstColor[j];
            }

        }
        public void shiftSeamsVerticalCase2(int ratio, Point p1, Point p2)
        {
            int count = 0;
            List<Seam> lstShrink = new List<Seam>();
            List<Seam> lstStretch = new List<Seam>();
            int[] arr = new int[lstVer.Count];
            for (int i = 0; i < lstVer.Count; i++)
                if (lstVer[i].lstPoint[0].Y <= p2.Y && lstVer[i].lstPoint[0].Y >= p1.Y)
                {
                    arr[i] = 1;
                    count++;
                    lstStretch.Add(lstVer[i]);
                }
                else
                {
                    arr[i] = 0;
                    lstShrink.Add(lstVer[i]);
                }
            int countShrink = lstShrink.Count;
            int countStretch = lstStretch.Count;
            int flag = 0;

            List<List<PointCount>> dataPoints = new List<List<PointCount>>();
            int[,] dataPointsCount = new int[Height, Width];
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                {
                    dataPointsCount[i, j] = 1;
                }
            while (lstShrink.Count / (100 / ratio) > 0 && lstStretch.Count / (100 / ratio) > 0)
            {
                if (flag == 0)
                {
                    foreach (Point p in lstStretch[0].lstPoint)
                        dataPointsCount[p.X, p.Y] = 2;
                    foreach (Point p in lstShrink[0].lstPoint)
                        dataPointsCount[p.X, p.Y] = 0;
                    lstShrink.RemoveAt(0);
                    lstStretch.RemoveAt(0);

                }

                else if (flag == 1)
                {
                    foreach (Point p in lstStretch[lstStretch.Count / 2].lstPoint)
                        dataPointsCount[p.X, p.Y] = 2;
                    foreach (Point p in lstShrink[lstShrink.Count / 2].lstPoint)
                        dataPointsCount[p.X, p.Y] = 0;
                    lstShrink.RemoveAt(lstShrink.Count / 2);
                    lstStretch.RemoveAt(lstStretch.Count / 2);

                }
                else if (flag == 2)
                {
                    foreach (Point p in lstStretch[lstStretch.Count - 1].lstPoint)
                        dataPointsCount[p.X, p.Y] = 2;
                    foreach (Point p in lstShrink[lstShrink.Count - 1].lstPoint)
                        dataPointsCount[p.X, p.Y] = 0;
                    lstShrink.RemoveAt(lstShrink.Count - 1);
                    lstStretch.RemoveAt(lstStretch.Count - 1);

                }
                flag = (flag + 1) % 3;
            }
            for (int i = 0; i < Height; i++)
            {
                List<Color> lstColor = new List<Color>();
                for (int j = 0; j < Width; j++)
                {

                    if (dataPointsCount[i, j] == 1)
                        lstColor.Add(DataColorShift[i, j]);
                    else if (dataPointsCount[i, j] == 2)
                    {
                        lstColor.Add(DataColorShift[i, j]);
                        lstColor.Add(DataColorShift[i, j]);
                    }
                }
                for (int j = 0; j < Width; j++)
                    DataColorShift[i, j] = lstColor[j];
            }

        }
        private float[,] Differentiate(byte[,] Data, int[,] Filter)
        {
            int i, j, k, l, Fh, Fw;

            Fw = Filter.GetLength(0);
            Fh = Filter.GetLength(1);
            float sum = 0;
            float[,] Output = new float[Height, Width];

            for (i = Fh / 2; i <= (Height - Fh / 2) - 1; i++)
            {
                for (j = Fw / 2; j <= (Width - Fw / 2) - 1; j++)
                {
                    sum = 0;
                    for (k = -Fh / 2; k <= Fh / 2; k++)
                    {
                        for (l = -Fw / 2; l <= Fw / 2; l++)
                        {
                            sum = sum + Data[i + k, j + l] * Filter[Fh / 2 + k, Fw / 2 + l];
                        }
                    }
                    Output[i, j] = sum;
                }

            }
            return Output;

        }
        void traverseHor(int x, int y, int BZ, Seam seam)
        {
            Point prev = new Point();
            prev.X = x;
            prev.Y = y;

            while (y <= Width - 2)
            {
                float min = 1000;
                List<Point> lstPointChk = new List<Point>();
                
    
                    if (prev.X - 1 >= x)
                        lstPointChk.Add(new Point(prev.X - 1, y + 1));
                    if (prev.X + 1 < x + BZ && prev.X + 1 < Height)
                        lstPointChk.Add(new Point(prev.X + 1, y + 1));
                    lstPointChk.Add(new Point(prev.X, y + 1));
           
                foreach (Point p in lstPointChk)
                {
                    if (energy[p.X, p.Y] < min)
                    {
                        prev.X = p.X;
                        prev.Y = p.Y;
                        min = energy[p.X, p.Y];
                    }
                }
                y++;
                seam.lstPoint.Add(prev);
                seam.lstEnergy.Add(energy[prev.X, prev.Y]);

            }

        }
        void traverseVer(int x, int y, int BZ, Seam seam)
        {
            Point prev = new Point();
            prev.X = x;
            prev.Y = y;

            while (x <= Height - 2)
            {
                float min = 1000;
                List<Point> lstPointChk = new List<Point>();
                if (prev.Y - 1 >= y)
                    lstPointChk.Add(new Point(x+1, prev.Y-1));
                if (prev.Y + 1 < y + BZ && prev.Y + 1 < Width)
                    lstPointChk.Add(new Point(x + 1, prev.Y + 1));
                lstPointChk.Add(new Point(x+1, prev.Y));
                foreach (Point p in lstPointChk)
                {
                    if (energy[p.X, p.Y] < min)
                    {
                        prev.X = p.X;
                        prev.Y = p.Y;
                        min = energy[p.X, p.Y];
                    }
                }
                x++;
                seam.lstPoint.Add(prev);
                seam.lstEnergy.Add(energy[prev.X, prev.Y]);
            }
        }
    }
}