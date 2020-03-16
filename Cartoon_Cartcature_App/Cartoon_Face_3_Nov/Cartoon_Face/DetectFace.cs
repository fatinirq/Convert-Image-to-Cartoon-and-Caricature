using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
#if !(__IOS__ || NETFX_CORE)
using Emgu.CV.Cuda;
#endif

namespace Cartoon_Face
{

    public static  class DetectFace
    {
       
        public static void Detect(
           IInputArray image, String faceFileName, String eyeFileName, String noseFileName, String mouthFileName,
           List<Rectangle> faces, List<Rectangle> eyes, List<Rectangle> noses, List<Rectangle> mouthes,
           out long detectionTime)
        {
            Stopwatch watch;

            using (InputArray iaImage = image.GetInputArray())
            {
                    //Read the HaarCascade objects
                    using (CascadeClassifier face = new CascadeClassifier(faceFileName))
                using (CascadeClassifier eye = new CascadeClassifier(eyeFileName))
                    using(CascadeClassifier mouth=new CascadeClassifier(mouthFileName))
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
                           10,
                           new Size(20, 20));

                        faces.AddRange(facesDetected);

                        foreach (Rectangle f in facesDetected)
                        {
                            //Get the region of interest on the faces
                            using (UMat faceRegion = new UMat(ugray, f))
                            {
                                Rectangle[] eyesDetected = eye.DetectMultiScale(
                                   faceRegion,
                                   1.1,
                                   10,
                                   new Size(20, 20));
                                Rectangle[] mouthesDetected = mouth.DetectMultiScale(
                                   faceRegion,
                                   1.1,
                                   10,
                                   new Size(20, 20));

                                foreach (Rectangle e in eyesDetected)
                                {
                                    Rectangle eyeRect = e;
                                    eyeRect.Offset(f.X, f.Y);
                                    eyes.Add(eyeRect);
                                }
                                foreach (Rectangle m in mouthesDetected)
                                {
                                    Rectangle mouthRect = m;
                                    mouthRect.Offset(f.X, f.Y);
                                    mouthes.Add(mouthRect);
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


