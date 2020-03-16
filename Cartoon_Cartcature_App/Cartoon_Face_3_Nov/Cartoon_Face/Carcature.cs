using Emgu.CV;
using Emgu.CV.CvEnum;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cartoon_Face
{
    public class Carcature
    {
        public static Bitmap bmp,bmpout;
        public static void caricature(bool nose,bool mouth,bool eye, int iNose, int iMouth,int iEye)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Bitmap files (*.bmp)|*.bmp|PNG files (*.png)|*.png|TIFF files (*.tif)|*tif|JPEG files (*.jpg)|*.jpg |All files (*.*)|*.*";
            ofd.FilterIndex = 5;
            ofd.RestoreDirectory = true;
            
            if (ofd.ShowDialog() == true)
                try
                {
                    string filename = ofd.FileName;

                    if (!Directory.Exists(filename.Substring(0, filename.IndexOf('.')) + "\\Results"))
                    {
                        Directory.CreateDirectory(filename.Substring(0, filename.IndexOf('.')) + "\\Results");
                    }
                    // string mainDirectry = filename.Substring(0, filename.IndexOf('.')) + "\\Results";
                    string mainDirectry = filename.Substring(0, filename.IndexOf('.'));
                    string path_original = System.IO.Path.Combine(mainDirectry, "Results");
                    bmp = new Bitmap(filename);
                    bmpout = new Bitmap(filename);
                    // Bitmap bmpLIP = new Bitmap(ImageEnhancement.colorLIPMult(bmp));
                   

                    IImage image;
                    image = new UMat(filename, ImreadModes.Color);
                    long detectionTime;
                    List<System.Drawing.Rectangle> faces = new List<System.Drawing.Rectangle>();
                    List<System.Drawing.Rectangle> eyes = new List<System.Drawing.Rectangle>();
                    List<System.Drawing.Rectangle> noses = new List<System.Drawing.Rectangle>();
                    List<System.Drawing.Rectangle> mouthes = new List<System.Drawing.Rectangle>();

                    DetectFace.Detect(
                      image, "haarcascade_frontalface_default.xml", "haarcascade_eye.xml", "nose.xml", "mouth.xml",
                      faces, eyes, noses, mouthes,
                      out detectionTime);
                    // filteredBmp.Save(mainDirectry + "//filteredBmp.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    //Bitmap ViolaBmp = ImageRectangularCut.GetViolaFace(grayBmp, faces[0]);
                    Bitmap grayBmp = ImageEnhancement.convert2Gray(bmp);
                    Bitmap ViolaOrgBmp = ImageRectangularCut.GetViolaFace(bmp, faces[0]);
                    ViolaOrgBmp.Save(mainDirectry + "//violaImage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                   
                    ///skinDetecttion
                    Bitmap bmpOrgSkin = new Bitmap(SkinDetection.skinColorSegments(ViolaOrgBmp));
                    bmpOrgSkin.Save(mainDirectry + "//Skin.jpg");
                    
                    //Smoothing
                    Bitmap bmpGauSmoothing = new Bitmap(Filters.gaussianFilter(ViolaOrgBmp, 2, 5));
                    bmpGauSmoothing.Save(mainDirectry + "//Gaussian.jpg");
                   
                    //Contrast

                    Bitmap bmpHistStre = PreProc.hisEqua(bmpGauSmoothing);
                    bmpHistStre.Save(mainDirectry + "//histogramStretching.jpg");
                    Bitmap bmpLIPGray = ImageEnhancement.grayLIPMult(bmpHistStre);
                    bmpLIPGray.Save(mainDirectry + "//LIPGray.jpg");
                    
                    //Binarization
                    ///Contrast BinarybmpBinary
                    Bitmap conBinBmp = new Bitmap(PreProc.binary_Bmp(130, bmpLIPGray));
                    conBinBmp.Save(mainDirectry + "//ConBinBmp.jpg");
                   
                    /////Blob detection
                    Bitmap blobBmp = new Bitmap(FaceBlobDtetction.DetectDarkBlobs(bmpOrgSkin, conBinBmp, mainDirectry));
                    blobBmp.Save(mainDirectry + "//blobBmp.jpg");
                    
                    ///Seams
                    SeedFillingEyeR rightEye = new SeedFillingEyeR(bmpLIPGray, FaceBlobDtetction.lstIntRec[0], mainDirectry);
                    SeedFillingEyeR leftEye = new SeedFillingEyeR(bmpLIPGray, FaceBlobDtetction.lstIntRec[1], mainDirectry);
                    SeedFillingMouthNose mouthNose = new SeedFillingMouthNose(bmpLIPGray, FaceBlobDtetction.midY, mainDirectry);

                    Bitmap rightEyeBmp = new Bitmap(leftEye.regions.bmpRegions);
                    
                    ////Segments
                    Bitmap bmpSegments = new Bitmap(rightEyeBmp.Width, rightEyeBmp.Height);
                    Random r = new Random(10);
                    int maxx1 = 0, maxy1 = 0, minx1 = 1000, miny1 = 1000;
                    foreach (SeedFillingEyeR.Region reg in rightEye.regions.lstRegions)
                    {
                        int red = r.Next(0, 255);
                        if (reg.maxx > maxx1)
                            maxx1 = reg.maxx;
                        if (reg.minx < minx1)
                            minx1 = reg.minx;
                        if (reg.maxy > maxy1)
                            maxy1 = reg.maxy;
                        if (reg.miny < miny1)
                            miny1 = reg.miny;
                        Color clr = Color.FromArgb((int)r.Next(0, 255), (int)r.Next(0, 255), (int)r.Next(0, 255));
                        // foreach (System.Drawing.Point p in reg.lstPoints)
                        //   bmpSegments.SetPixel(p.Y, p.X, clr);
                    }
                    int maxx2 = 0, maxy2 = 0, minx2 = 1000, miny2 = 1000;
                    foreach (SeedFillingEyeR.Region reg in leftEye.regions.lstRegions)
                    {
                        int red = r.Next(0, 255);
                        if (reg.maxx > maxx2)
                            maxx2 = reg.maxx;
                        if (reg.minx < minx2)
                            minx2 = reg.minx;
                        if (reg.maxy > maxy2)
                            maxy2 = reg.maxy;
                        if (reg.miny < miny2)
                            miny2 = reg.miny;
                        Color clr = Color.FromArgb((int)r.Next(0, 255), (int)r.Next(0, 255), (int)r.Next(0, 255));
                        foreach (System.Drawing.Point p in reg.lstPoints)
                            bmpSegments.SetPixel(p.Y, p.X, clr);
                    }
                    // path_original = System.IO.Path.Combine(path_original, "Subjects");
                    SeamCurveByFatin ss;
                    
                    if (eye==true)
                    {
                       
                            Bitmap subjectEye1 = new Bitmap(bmpout);
                            ss = new SeamCurveByFatin(FaceBlobDtetction.lstIntRec[0], subjectEye1, grayBmp, 3, new System.Drawing.Point(minx1, miny1), new System.Drawing.Point(maxx1, maxy1), faces[0], (iEye-1), path_original, 0);
                            subjectEye1.Dispose();
                            Bitmap subjectEye2 = new Bitmap(path_original + "//bmpCa_" + (iEye-1) + "_A" + ".jpg");
                            ss = new SeamCurveByFatin(FaceBlobDtetction.lstIntRec[1], subjectEye2, grayBmp, 3, new System.Drawing.Point(minx2, miny2), new System.Drawing.Point(maxx2, maxy2), faces[0], (iEye-1), path_original, 1);
                        bmpout = new Bitmap(path_original + "//bmpCa_" + (iEye-1) + "_B" + ".jpg");
                            subjectEye2.Dispose();
                        }
                    if (nose == true)
                    {
                        //Bitmap subjectNose = new Bitmap(bmp);
                        ss = new SeamCurveByFatin(bmpout, grayBmp, 3, mouthNose.p1Nose, mouthNose.p2Nose, mouthNose.p1NoseROI, mouthNose.p2NoseROI, faces[0], (iNose-1), path_original, 0);
                        bmpout = new Bitmap(path_original + "//bmpCa_" + (iNose-1) + "_N" + ".jpg");
                    }
                    if (mouth == true)
                    {
                        //Bitmap subjectMouth = new Bitmap(bmp);
                        ss = new SeamCurveByFatin(bmpout, grayBmp, 3, mouthNose.p1Mouth, mouthNose.p2Mouth, mouthNose.p1MouthROI, mouthNose.p2MouthROI, faces[0], (iMouth-1), path_original, 1);
                        bmpout = new  Bitmap(path_original + "//bmpCa_" + (iMouth-1) + "_M" + ".jpg");
                    }
                    
                }
                catch (ApplicationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            
        }
    }
}
