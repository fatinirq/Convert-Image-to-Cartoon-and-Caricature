using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
#if !(__IOS__ || NETFX_CORE)
using Emgu.CV.Cuda;
#endif

namespace Cartoon_Face
{
    class DetectFaceEyes
    {
        public static void Detect(
            IInputArray image, String faceFileName, String eyeFileName, 
            List<Rectangle> faces, List<Rectangle> eyes, 
            out long detectionTime)
        {
            Stopwatch watch;
            CascadeClassifier hh = new CascadeClassifier();
            using (InputArray iaImage = image.GetInputArray())
            {
                //Read the HaarCascade objects
                using (CascadeClassifier face = new CascadeClassifier(faceFileName))
                using (CascadeClassifier eye = new CascadeClassifier(eyeFileName))
               
                {
                    watch = Stopwatch.StartNew();

                    using (UMat ugray = new UMat())
                    {
                        CvInvoke.CvtColor(image, ugray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

                        //normalizes brightness and increases contrast of the image
                        CvInvoke.EqualizeHist(ugray, ugray);

                        //Detect the faces  from the gray scale image and store the locations as rectangle
                        //The first dimensional is the channel
                        //The second dimension is the index of the rectangle in the specific channel                     
                        Rectangle[] facesDetected = face.DetectMultiScale(
                           ugray,
                           1.1,
                           10
                           );

                        faces.AddRange(facesDetected);

                        foreach (Rectangle f in facesDetected)
                        {
                            //Get the region of interest on the faces
                            using (UMat faceRegion = new UMat(ugray, f))
                            {
                                Rectangle[] eyesDetected = eye.DetectMultiScale(
                                   faceRegion,
                                   1.1,
                                   10);
                               

                                foreach (Rectangle e in eyesDetected)
                                {
                                    Rectangle eyeRect = e;
                                    eyeRect.Offset(f.X, f.Y);
                                    eyes.Add(eyeRect);
                                }
                               
                            }

                        }
                    }
                    watch.Stop();
                }
            }
            detectionTime = watch.ElapsedMilliseconds;
        }
    }
}



