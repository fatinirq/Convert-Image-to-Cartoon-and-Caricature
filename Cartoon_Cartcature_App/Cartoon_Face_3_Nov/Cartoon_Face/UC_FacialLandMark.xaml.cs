using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
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
    /// Interaction logic for UC_FacialLandMark.xaml
    /// </summary>
    public partial class UC_FacialLandMark : UserControl
    {
        public UC_FacialLandMark()
        {
            InitializeComponent();
        }

        private void btnProceed_Click(object sender, RoutedEventArgs e)
        {
            Bitmap bmp;
            BitmapImage bmImage = new BitmapImage();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Bitmap files (*.bmp)|*.bmp|PNG files (*.png)|*.png|TIFF files (*.tif)|*tif|JPEG files (*.jpg)|*.jpg |All files (*.*)|*.*";
            ofd.FilterIndex = 5;
            ofd.RestoreDirectory = true;
            Seed_Filling_HSV regions_HSV;
            Seed_Filling_RGB regions_Red;
            if (ofd.ShowDialog() == true)
                try
                {
                    string filename = ofd.FileName;
                    bmp = new Bitmap(ofd.FileName);
                    IImage image;

                    //Read the files as an 8-bit Bgr image  

                    image = new UMat(filename, ImreadModes.Color); //UMat version
                                                                   //image = new Mat("lena.jpg", ImreadModes.Color); //CPU version

                    long detectionTime;
                    List<System.Drawing.Rectangle> faces = new List<System.Drawing.Rectangle>();
                    List<System.Drawing.Rectangle> eyes = new List<System.Drawing.Rectangle>();
                   

                    DetectFaceEyes.Detect(
                      image, "haarcascade_frontalface_default.xml", "haarcascade_eye.xml", 
                      faces, eyes, 
                      out detectionTime);
                    Head_Seg seg = new Head_Seg(faces[0],eyes,bmp,filename);
                    seg._Double_Rec();
                    doubleRec.Source=Convert2WPFBitmap.Win2WPFBitmap( seg._DrawBmp_Rec());
                    //display the image 
                    
                }

                catch (ApplicationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

        }
    }
}
