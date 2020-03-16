using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Microsoft.Win32;

namespace Cartoon_Face
{
    /// <summary>
    /// Interaction logic for UC_FaceDrawingModel.xaml
    /// </summary>
    public partial class UC_FaceDrawingModel : UserControl
    {
        public UC_FaceDrawingModel()
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
                    Enhanced.Source = Convert2WPFBitmap.Win2WPFBitmap(enhancedImage);
                 
                    Bitmap grayBmp = ImageEnhancement.convert2Gray(enhancedImage);
                    Gray.Source = Convert2WPFBitmap.Win2WPFBitmap(grayBmp);
                  
                    IImage image, blankImage;

                    //Read the files as an 8-bit Bgr image  

                    image = new UMat(filename, ImreadModes.Color); //UMat version
                    blankImage = new UMat();                                             //image = new Mat("lena.jpg", ImreadModes.Color); //CPU version

                    long detectionTime;
                    List<System.Drawing.Rectangle> faces = new List<System.Drawing.Rectangle>();
                    List<System.Drawing.Rectangle> eyes = new List<System.Drawing.Rectangle>();
                    List<System.Drawing.Rectangle> noses = new List<System.Drawing.Rectangle>();
                    List<System.Drawing.Rectangle> mouthes = new List<System.Drawing.Rectangle>();

                    DetectFace.Detect(
                      image, "haarcascade_frontalface_default.xml", "haarcascade_eye.xml", "nose.xml", "mouth.xml",
                      faces, eyes, noses, mouthes,
                      out detectionTime);
                    Bitmap ViolaBmp = ImageRectangularCut.GetViolaFace(grayBmp, faces[0]);
                    Bitmap ViolaOrgBmp = ImageRectangularCut.GetViolaFace(bmp, faces[0]);
                    Bitmap ViolaColoredBmp = ImageRectangularCut.GetViolaFace(enhancedImage, faces[0]);
                    Viola.Source = Convert2WPFBitmap.Win2WPFBitmap(ViolaBmp);

                    //   Bitmap LeeViolaBmp = ImageEnhancement.modifiedLee(ViolaBmp, siAlpha.Value, siBeta.Value, 3);
                    // Enhanced.Source = Convert2WPFBitmap.Win2WPFBitmap(LeeViolaBmp);
                    // MessageBox.Show("Modified Lee is Done");
                    //////////////
                    //Smoothing
                    Bitmap bmpGauSmoothing = new Bitmap(Filters.gaussianFilter(ViolaBmp, 2, 5));
                    gauSmooth.Source = Convert2WPFBitmap.Win2WPFBitmap(bmpGauSmoothing);
                    Bitmap bmpBinary = new Bitmap(PreProc.binary_Bmp(20, bmpGauSmoothing));
                    Binary.Source = Convert2WPFBitmap.Win2WPFBitmap(bmpBinary);
                    //Contrast
                    Bitmap bmpContrast = ImageEnhancement.grayLIPMult(bmpGauSmoothing);
                    Bitmap bmpHistStre = PreProc.hisEqua(ImageEnhancement.grayLIPMult(bmpGauSmoothing));
                    Contrast_Viola.Source = Convert2WPFBitmap.Win2WPFBitmap(bmpContrast);
                    HistEq_Viola.Source = Convert2WPFBitmap.Win2WPFBitmap(bmpHistStre);
                    ///LogMul

                   ///skinDetecttion
                    Bitmap bmpOrgSkin = new Bitmap(SkinDetection.skinColorSegments_3rd(ViolaOrgBmp));
                    SkinOriginal.Source = Convert2WPFBitmap.Win2WPFBitmap(bmpOrgSkin);
                    Bitmap bmpEnhancedSkin = new Bitmap(SkinDetection.skinColorSegments_3rd(ViolaColoredBmp));
                    SkinEnhanced.Source = Convert2WPFBitmap.Win2WPFBitmap(bmpEnhancedSkin);

                    //Canny
                    Canny cn = new Canny(bmpContrast,  20, 10, filename);
                    Bitmap bmpCanny = new Bitmap(cn.DisplayImage(cn.EdgeMap));
                    Canny_Viola.Source = Convert2WPFBitmap.Win2WPFBitmap(bmpCanny);
                    ///Contrast BinarybmpBinary
                    Bitmap conBinBmp = new Bitmap(PreProc.binary_Bmp(150, bmpContrast));
                    Binary_Contrast.Source=Convert2WPFBitmap.Win2WPFBitmap(conBinBmp);
                    /////Blob detection
                    Bitmap blobBmp = new Bitmap(FaceBlobDtetction.DetectDarkBlobs(bmpEnhancedSkin, conBinBmp,"D:/"));
                    Blob.Source = Convert2WPFBitmap.Win2WPFBitmap(blobBmp);
                    ////Right Eye
                    SeedFillingEyeR rightEye = new SeedFillingEyeR(bmpContrast, FaceBlobDtetction.lstIntRec[0],"D:/");
                    SeedFillingEyeR leftEye = new SeedFillingEyeR(bmpContrast, FaceBlobDtetction.lstIntRec[1],"D:/");

                    Bitmap rightEyeBmp = new Bitmap(leftEye.regions.bmpRegions);
                    RightEye.Source = Convert2WPFBitmap.Win2WPFBitmap(rightEyeBmp);
                    ////Segments
                    Bitmap bmpSegments = new Bitmap(rightEyeBmp.Width, rightEyeBmp.Height);
                    Random r = new Random(10);
                    int maxx = 0, maxy = 0, minx = 1000, miny = 1000;
                    foreach(SeedFillingEyeR.Region reg in rightEye.regions.lstRegions)
                    {     
                       int red= r.Next(0, 255);
                        if (reg.maxx > maxx)
                            maxx = reg.maxx;
                        if (reg.minx < minx)
                            minx = reg.minx;
                        if (reg.maxy > maxy)
                            maxy = reg.maxy;
                        if (reg.miny < miny)
                            miny = reg.miny;
                        Color clr = Color.FromArgb((int) r.Next(0,255), (int) r.Next(0, 255), (int)r.Next(0, 255));

                        foreach (System.Drawing.Point p in reg.lstPoints)
                            bmpSegments.SetPixel(p.Y, p.X, clr);
                    }

                   // SeamCurveByFatin ss = new SeamCurveByFatin(FaceBlobDtetction.lstIntRec[0],bmp,grayBmp, 3, new System.Drawing.Point(minx,miny),new System.Drawing.Point(maxx,maxy),faces[0]);
                    maxx = 0; maxy = 0; minx = 1000; miny = 1000;
                    foreach (SeedFillingEyeR.Region reg in leftEye.regions.lstRegions)
                    {

                        int red = r.Next(0, 255);
                        if (reg.maxx > maxx)
                            maxx = reg.maxx;
                        if (reg.minx < minx)
                            minx = reg.minx;
                        if (reg.maxy > maxy)
                            maxy = reg.maxy;
                        if (reg.miny < miny)
                            miny = reg.miny;
                        Color clr = Color.FromArgb((int)r.Next(0, 255), (int)r.Next(0, 255), (int)r.Next(0, 255));

                        foreach (System.Drawing.Point p in reg.lstPoints)
                            bmpSegments.SetPixel(p.Y, p.X, clr);
                    }
                    //ss = new SeamCurveByFatin(FaceBlobDtetction.lstIntRec[1], bmp, grayBmp, 5, new System.Drawing.Point(minx, miny), new System.Drawing.Point(maxx, maxy), faces[0]);
                    EyeSegments.Source = Convert2WPFBitmap.Win2WPFBitmap(bmpSegments);
                    Seams.Source = Convert2WPFBitmap.Win2WPFBitmap(bmp);
                    bmp.Save("D:\\seam.jpg");
                }
               


                catch (ApplicationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }


        }
    }
}
