using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Cuda;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace Cartoon_Face
{
    /// <summary>
    /// Interaction logic for UC_Viola.xaml
    /// </summary>
    public partial class UC_Viola : UserControl
    {
        public UC_Viola()
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
                    IImage image,blankImage;
                   
                    //Read the files as an 8-bit Bgr image  
                   
                    image = new UMat(filename, ImreadModes.Color); //UMat version
                    blankImage = new UMat();                                             //image = new Mat("lena.jpg", ImreadModes.Color); //CPU version

                    long detectionTime;
                    List<System.Drawing.Rectangle> faces = new List<Rectangle>();
                    List<System.Drawing.Rectangle> eyes = new List<Rectangle>();
                    List<System.Drawing.Rectangle> noses = new List<Rectangle>();
                    List<System.Drawing.Rectangle> mouthes = new List<Rectangle>();

                    DetectFace.Detect(
                      image, "haarcascade_frontalface_default.xml", "haarcascade_eye.xml","nose.xml","mouth.xml",
                      faces, eyes,noses,mouthes,
                      out detectionTime);

                    foreach (System.Drawing.Rectangle face in faces)
                    CvInvoke.Rectangle(image, face, new Bgr(Color.Red).MCvScalar, 2);
                   
                   
                    foreach (System.Drawing.Rectangle eye in eyes)
                        CvInvoke.Rectangle(image, eye, new Bgr(System.Drawing.Color.Blue).MCvScalar, 2);
                    foreach (System.Drawing.Rectangle mouth in mouthes)
                        CvInvoke.Rectangle(image, mouth, new Bgr(System.Drawing.Color.Yellow).MCvScalar, 2);
                    ///////////////////
                    //Bitmap bmpRec = new Bitmap(image.Bitmap);
                    //List<Rectangle> lstRec = new List<Rectangle>();
                    //Bitmap bmpViola = image.Bitmap;
                    //Color clr = Color.Red;
                    //System.Drawing.Point P1 = new System.Drawing.Point();
                    //int len = 0;
                    //for (int i = 0; i < image.Bitmap.Height; i++)
                    //    for (int j = 0; j < image.Bitmap.Width; j++)
                    //    {
                    //        Color clr2 = image.Bitmap.GetPixel(j, i);
                    //        if (clr2.R == clr.R && clr2.G == clr.G && clr2.B == clr.B)
                    //        {
                    //            P1.X = i;
                    //            P1.Y = j;
                    //            Color clr3 = image.Bitmap.GetPixel(j + 1, i);
                    //            Color clr4 = image.Bitmap.GetPixel(j, i + 1);
                    //            bool flag = true;
                    //            if (clr3.R == clr.R && clr3.G == clr.G && clr3.B == clr.B
                    //                && clr4.R == clr.R && clr4.G == clr.G && clr4.B == clr.B)
                    //            {
                    //                while (flag == true)
                    //                {
                    //                    len++;
                    //                    i++;
                    //                    clr3 = image.Bitmap.GetPixel(j, i);
                    //                    if (clr3.R == clr.R && clr3.G == clr.G && clr3.B == clr.B)
                    //                        flag = true;
                    //                    else
                    //                        flag = false;
                    //                }
                    //                bmpRec.SetPixel(P1.Y, P1.X, Color.Orange);
                    //                bmpRec.SetPixel(P1.Y - 1, P1.X - 1, Color.Orange);
                    //                bmpRec.SetPixel(P1.Y + 1, P1.X + 1, Color.Orange);
                    //                bmpRec.SetPixel(P1.Y + 2, P1.X + 2, Color.Orange);
                    //                bmpRec.SetPixel(P1.Y + 3, P1.X + 3, Color.Orange);

                    //            }
                    //            else
                    //                P1 = new System.Drawing.Point();
                    //        }
                    //        if (len != 0)
                    //        {
                    //            Rectangle rec = new Rectangle(P1, new System.Drawing.Size(len, len));
                    //            lstRec.Add(rec);
                    //            len = 0;
                    //        }
                    //        //bmpRec.SetPixel(j, i, clr);

                    //    }
                    //foreach (Rectangle rec in lstRec)
                    //    using (Graphics g = Graphics.FromImage(bmpRec))
                    //    {
                    //        MessageBox.Show("Rec.Height= " + Rec.Height + " Rec.P1");
                    //        g.DrawRectangle(Pens.Aquamarine, rec);
                    //    }
                    Enhanced.Source = Convert2WPFBitmap.Win2WPFBitmap(image.Bitmap);
                    if (faces.Count>0)
                    Rec.Source = Convert2WPFBitmap.Win2WPFBitmap(ImageRectangularCut.GetViolaFace(bmp,faces[0]));

                }
         
                catch (ApplicationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

        }
    }
}
