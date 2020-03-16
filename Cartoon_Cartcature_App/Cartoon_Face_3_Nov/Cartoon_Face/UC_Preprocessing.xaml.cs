using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cartoon_Face
{
    /// <summary>
    /// Interaction logic for Preprocessing.xaml
    /// </summary>
    public partial class UC_Preprocessing : UserControl
    {
        public UC_Preprocessing()
        {
            InitializeComponent();
        }

        private void btnProceed_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Bitmap files (*.bmp)|*.bmp|PNG files (*.png)|*.png|TIFF files (*.tif)|*tif|JPEG files (*.jpg)|*.jpg |All files (*.*)|*.*";
            ofd.FilterIndex = 5;
            ofd.RestoreDirectory = true;
            Bitmap bmp;
            if (ofd.ShowDialog() == true)
                try
                {
                    string filename = ofd.FileName;
                    bmp = new Bitmap(filename);
                    Original.Source = Convert2WPFBitmap.Win2WPFBitmap(bmp);
                    Bitmap enhancedImage = ImageEnhancement.colorLIPMult(bmp);
                    Bitmap grayLIPBmp = ImageEnhancement.convert2Gray(enhancedImage);
                    Bitmap grayBmp = ImageEnhancement.convert2Gray(bmp);
                    Gray.Source = Convert2WPFBitmap.Win2WPFBitmap(grayBmp);
                    Gray_LIP.Source = Convert2WPFBitmap.Win2WPFBitmap(grayLIPBmp);
                    Bitmap bmpIllumin_LIP = new Bitmap(ImageEnhancement.convertArr2Gray(ImageEnhancement.IlluminationCompens(
                    ImageEnhancement.convert2GrayArr(grayLIPBmp), bmp.Width, bmp.Height, 40), bmp.Height, bmp.Width));
                    Gray_LIP_Illumination.Source = Convert2WPFBitmap.Win2WPFBitmap(bmpIllumin_LIP);
                    Original_LIP.Source = Convert2WPFBitmap.Win2WPFBitmap(enhancedImage);
                  //  Bitmap without_Shadow_Bmp = PreProc.shadowReduction(grayBmp);
                 //   Shadow.Source = Convert2WPFBitmap.Win2WPFBitmap(without_Shadow_Bmp);
                   
                   
                   // Bitmap binBmp = PreProc.binary_Bmp(150);
                   // Binary.Source = Convert2WPFBitmap.Win2WPFBitmap(binBmp);
                   
                    
                    List<System.Drawing.Point> lstPoints=new List<System.Drawing.Point>();
                    
                  //  MessageBox.Show("Count="+ lstPoints.Count);
                    
                   // Bitmap binSobel = Filters.Sobel_Ver(gauBmp,lstPoints);
                   // Bitmap contrastLIPBmp = PreProc.localcontrastStretching(gauBmp); 
                  
                    //Bitmap binBmpLIP = PreProc.binary_Bmp(150);
                   // BinaryLIP.Source = Convert2WPFBitmap.Win2WPFBitmap(binBmpLIP);
                    
                  //  Bin_Sobel.Source = Convert2WPFBitmap.Win2WPFBitmap(binSobel);
                   
                    
                   
                    Bitmap leeBmp = ImageEnhancement.imgLog2(grayLIPBmp, siAlpha.Value, siBeta.Value, 3);
                    Gray_Lee.Source = Convert2WPFBitmap.Win2WPFBitmap(leeBmp);
                   
                    Bitmap sobelBmp = Filters.Sobel_Ver(leeBmp, lstPoints);
                   // Bitmap leeContrastBmp = new Bitmap(PreProc.hisEqua(leeBmp));
                    //Contrast_LIP.Source = Convert2WPFBitmap.Win2WPFBitmap(leeContrastBmp);
                    Sobel.Source = Convert2WPFBitmap.Win2WPFBitmap(sobelBmp);
                    Bitmap gauBmp = Filters.medianFilter_List_Gray(leeBmp, lstPoints, 6, 5);
                    Gaussian.Source = Convert2WPFBitmap.Win2WPFBitmap(gauBmp);
                    Bitmap binBmpGauss = new Bitmap(PreProc.binary_Bmp(250, leeBmp));
                    BinaryGaussian.Source = Convert2WPFBitmap.Win2WPFBitmap(binBmpGauss);
                    Bitmap sobelEnhancedBmp = Filters.Sobel_Ver(leeBmp, lstPoints);
                    Bin_Sobel.Source = Convert2WPFBitmap.Win2WPFBitmap(sobelEnhancedBmp);
                    Bitmap enhancedLee = ImageEnhancement.grayLIPMult(leeBmp,0.8f);
                    Gray_EnhancedLee.Source = Convert2WPFBitmap.Win2WPFBitmap(enhancedLee);
                    //lstPoints = new List<System.Drawing.Point>();
                    //Filters.Sobel_Ver(grayLIPBmp, lstPoints);
                    //Bitmap leeLocalBmp = new Bitmap(ImageEnhancement.imgLog2(grayLIPBmp,lstPoints, siAlpha.Value, siBeta.Value, 3));
                    //Gray_LocalLee.Source = Convert2WPFBitmap.Win2WPFBitmap(leeLocalBmp);
                }

                catch (ApplicationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

        }
    }
}
