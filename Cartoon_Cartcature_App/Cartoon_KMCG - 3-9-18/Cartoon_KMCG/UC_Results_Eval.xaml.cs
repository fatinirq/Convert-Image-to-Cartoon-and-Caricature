using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace Cartoon_KMCG
{
    /// <summary>
    /// Interaction logic for UC_Results_Eval.xaml
    /// </summary>
    public partial class UC_Results_Eval : UserControl
    {
        private static PerformanceCounter avgCounter64Sample;
        private static PerformanceCounter avgCounter64SampleBase;
        public UC_Results_Eval()
        {
           
            InitializeComponent();
           
        }

        private void btnProceed_Click(object sender, RoutedEventArgs e)
        {

             
        Process.GetCurrentProcess().ProcessorAffinity =
      new IntPtr(2); // Uses the second Core or Processor for the Test
            Process.GetCurrentProcess().PriorityClass =
        ProcessPriorityClass.RealTime;      // Prevents "Normal" processes 
                                            // from interrupting Threads
            Thread.CurrentThread.Priority =
        ThreadPriority.Highest;
            for (int i = 0; i < 100000; i++) ;
            List<string> lstStr = new List<string>();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Bitmap files (*.bmp)|*.bmp|PNG files (*.png)|*.png|TIFF files (*.tif)|*tif|JPEG files (*.jpg)|*.jpg |All files (*.*)|*.*";
            ofd.FilterIndex = 5;
            ofd.RestoreDirectory = true;
            Bitmap bmp;
           
            if (ofd.ShowDialog() == true)
                try
                {
         
                    string filename = ofd.FileName;
                   
                    if (!Directory.Exists(filename.Substring(0, filename.IndexOf('.')) + "/Results"))
                    {
                        Directory.CreateDirectory(filename.Substring(0, filename.IndexOf('.')) + "/Results");
                    }
                    string mainDirectry = filename.Substring(0, filename.IndexOf('.')) + "/Results";
                    string path_original = System.IO.Path.Combine(mainDirectry, "Results");
                    bmp = new Bitmap(ofd.FileName);
                    Original.Source = Convert2WPFBitmap.Win2WPFBitmap(bmp);
                    Bitmap original_his_bmp = new Bitmap(ImageEnhancement.histogram_drawing(ImageEnhancement.convert2Gray(bmp)));
                    Original_His.Source = Convert2WPFBitmap.Win2WPFBitmap(original_his_bmp);
                    original_his_bmp.Save(mainDirectry + "\\original_his_bmp.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    //*************************************************
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    ///////Mean Filter
                    Bitmap filteredBmp = new Bitmap(Filters.meanFilter_Color(bmp, 3));
                    sw.Stop();
                    lstStr.Add("Mean Filter " + sw.ElapsedTicks/Stopwatch.Frequency);
                    sw.Reset();
                    Mean.Source = Convert2WPFBitmap.Win2WPFBitmap(filteredBmp);
                    filteredBmp.Save(mainDirectry + "//filteredBmp.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                 
                    lstStr.Add("");
                    ///////////////////////////////////////////////////////////////////
                    
                    Bitmap kmcg_Old_Bmp_5 = new Bitmap(KMCG_Old.KMCGRGB(filteredBmp, 5, filename,sw));
                    
                    lstStr.Add("kmcg_Old_Bmp with CB Size=5 " + sw.ElapsedTicks/(Stopwatch.Frequency*1.0));
                    lstStr.Add("Kmcg_Old_PSNR " + General_Metrics.PSNR(ImageEnhancement.convert2Gray(bmp), ImageEnhancement.convert2Gray(kmcg_Old_Bmp_5)));
                    lstStr.Add("Entropy_Old= " + General_Metrics.entropy(ImageEnhancement.convert2Gray(bmp), KMCG_Old.rgbMainLst));        
                    sw.Reset();
                    kmcg_Old_Bmp_5.Save(mainDirectry + "//kmcg_Old_Bmp_5.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    Bitmap kmcg_Old_Bmp_5_his = new Bitmap(ImageEnhancement.histogram_drawing(kmcg_Old_Bmp_5));
                    kmcg_Old_Bmp_5_his.Save(mainDirectry + "//kmcg_Old_Bmp_5_his.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    lstStr.Add("");

                    /////////////KMCG Old 6

                    
                    Bitmap kmcg_Old_Bmp_6 = new Bitmap(KMCG_Old.KMCGRGB(filteredBmp, 6, filename,sw));
                    
                    lstStr.Add("kmcg_Old_Bmp with CB Size=6 " + sw.ElapsedTicks/(Stopwatch.Frequency*1.0));
                    lstStr.Add("Kmcg_Old_PSNR " + General_Metrics.PSNR(ImageEnhancement.convert2Gray(bmp), ImageEnhancement.convert2Gray(kmcg_Old_Bmp_6)));
                    lstStr.Add( "Entropy_Old= " + General_Metrics.entropy(ImageEnhancement.convert2Gray(kmcg_Old_Bmp_6), KMCG_Old.rgbMainLst));
                    sw.Reset();
                    kmcg_Old_Bmp_5.Save(mainDirectry + "//kmcg_Old_Bmp_5.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    Bitmap kmcg_Old_Bmp_6_his = new Bitmap(ImageEnhancement.histogram_drawing(kmcg_Old_Bmp_6));
                    kmcg_Old_Bmp_6_his.Save(mainDirectry + "//kmcg_Old_Bmp_6_his.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    lstStr.Add("");
                    /////////////////////////KMCG New///////////////////////////////////////
                    byte th = (byte)siNewTh.Value;
                    
                    Bitmap kmcg_New_Bmp = new Bitmap(KMCGbyFatin.KMCGRGB(bmp, true, true, true, th,th,th, filename,sw));
                    
                    lstStr.Add("kmcg_New_Bmp " + sw.ElapsedTicks/(Stopwatch.Frequency*1.0));
                    lstStr.Add("Kmcg_New_PSNR = " + General_Metrics.PSNR(ImageEnhancement.convert2Gray(bmp), ImageEnhancement.convert2Gray(kmcg_New_Bmp)));
                    lstStr.Add("Entropy_New= " + General_Metrics.entropy(ImageEnhancement.convert2Gray(kmcg_New_Bmp), KMCGbyFatin.rgbMainLst));
                    int CBSize = KMCGbyFatin.codeBookCount[0].Count + KMCGbyFatin.codeBookCount[1].Count + KMCGbyFatin.codeBookCount[2].Count;
                    lstStr.Add("Cluster No. " + KMCGbyFatin.ClusterNo + " CB Size= " + CBSize);
                    sw.Reset();
                    kmcg_New_Bmp.Save(mainDirectry + "//kmcg_New_Bmp_withOutEdge.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    kmcg_New_Bmp.SetResolution(96, 96);
                    Bitmap kmcg_New_Bmp_his = new Bitmap(ImageEnhancement.histogram_drawing(kmcg_New_Bmp));
                    kmcg_New_Bmp_his.Save(mainDirectry + "//kmcg_New_Bmp_his.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    lstStr.Add("");
                    ///////////////KMCG Enhancement and Hist

                    sw.Start();
                    Bitmap kmcg_New_Bmp_Enhanced = new Bitmap(ImageEnhancement.colorLIPMult(kmcg_New_Bmp));
                    kmcg_New_Bmp_Enhanced.SetResolution(96, 96);
                    sw.Stop();
                   
                    lstStr.Add("kmcg_New_Bmp_Enhanced " + sw.ElapsedTicks/(Stopwatch.Frequency*1.0));
                    sw.Reset();
                    kmcg_New_Bmp_Enhanced.Save(mainDirectry + "//kmcg_New_Bmp_Enhanced.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    Bitmap kmcg_New_Bmp_Enhanced_His = new Bitmap(ImageEnhancement.histogram_drawing(kmcg_New_Bmp_Enhanced));
                    kmcg_New_Bmp_Enhanced_His.Save(mainDirectry + "//kmcg_New_Bmp_Enhanced_His.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    //////////////////Canny
                    sw.Start();
                    Canny CannyData1 = new Canny(filteredBmp, (float)siHiTh.Value, (float)siMinTh.Value, 5, 1, mainDirectry);
                    sw.Stop();
                 
                    lstStr.Add( "Canny Filtered Image " + sw.ElapsedTicks/(Stopwatch.Frequency*1.0));
                    sw.Reset();
                    Bitmap edge1Bmp = new Bitmap(CannyData1.DisplayImage(CannyData1.EdgeMap));
                    edge1Bmp.Save(mainDirectry + "//edge1Bmp.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    Canny_filtered.Source = Convert2WPFBitmap.Win2WPFBitmap(edge1Bmp);
                    //////////////////////////////////////////////////////////////////////////
                    sw.Reset();
                    ///////////////////////////////////////////////////////////////////////
                    sw.Start();
                     for (int i = 2; i < bmp.Height-2; i++)
                        for (int j = 2; j < bmp.Width-2; j++)
                        {
                            if (edge1Bmp.GetPixel(j, i).R == 255)
                            {
                                kmcg_Old_Bmp_5.SetPixel(j, i, System.Drawing.Color.FromArgb(0, 0, 0));
                                kmcg_Old_Bmp_6.SetPixel(j, i, System.Drawing.Color.FromArgb(0,0,0));
                            }      
                        }
                    sw.Stop();
                    kmcg_Old_Bmp_5.SetResolution(96, 96);
                    kmcg_Old_Bmp_6.SetResolution(96, 96);
                    //if (Stopwatch.IsHighResolution == true)
                      //  MessageBox.Show("Not High reso");
                    kmcg_Old_Bmp_5.Save(mainDirectry + "//kmcg_Old_Bmp_5.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    kmcg_Old_Bmp_6.Save(mainDirectry + "//kmcg_Old_Bmp_6.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    KMCG_Old_Image.Source = Convert2WPFBitmap.Win2WPFBitmap(kmcg_Old_Bmp_6);
                    lstStr.Add("Canny Edge Addition Old= " + sw.ElapsedTicks/Stopwatch.Frequency);
                    sw.Reset();
                    Bitmap kmcg_New_Bmp_Enhanced_Edge = new Bitmap(kmcg_New_Bmp_Enhanced);
                 
                    sw.Start();
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
                                    avB = avB / count-20 ;
                                    avG = avG / count-20 ;
                                    avR = avR / count -20;
                                    if (avB < 0)
                                        avB = 0;
                                    if (avR < 0)
                                        avR = 0;
                                    if (avG < 0)
                                        avG = 0;
                                //    for (int k = -1; k <= 1; k++)
                                 //       for (int l = -1; l <= 1; l++)
                                   //     {

                                            kmcg_New_Bmp_Enhanced_Edge.SetPixel(j , i, Color.FromArgb(avR, avG, avB));

                                     //   }
                                }
                            }
                        }
                    sw.Stop();
                    kmcg_New_Bmp_Enhanced_Edge.Save(mainDirectry + "//kmcg_New_Bmp_Enhanced_Edge.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    lstStr.Add("Canny Edge Addition New= " + sw.ElapsedTicks/(Stopwatch.Frequency*1.0)); 
                    Bitmap kmcg_New_Bmp_Enhanced_his = new Bitmap(ImageEnhancement.histogram_drawing(kmcg_New_Bmp_Enhanced));
                    kmcg_New_Bmp_Enhanced_his.Save(mainDirectry + "//kmcg_New_Bmp_Enhanced_his.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    KMCG_New_Image.Source = Convert2WPFBitmap.Win2WPFBitmap(kmcg_New_Bmp_Enhanced_Edge);
                 
                    File.AppendAllLines(path_original, lstStr);
                }
                catch (ApplicationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
        }
    }
}
