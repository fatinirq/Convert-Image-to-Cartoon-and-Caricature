using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Accord.Vision;
using Accord.Vision.Detection;
using System.Drawing;
using Accord.Vision.Detection.Cascades;
using System.Windows;

namespace Cartoon_Face
{
    class FaceDetection
    {
        public Bitmap bmpOut;
        Rectangle[] objects;

        public FaceDetection(Bitmap bmp)
        {
            HaarObjectDetector detector;
            HaarCascade cascade = new FaceHaarCascade();
            detector = new HaarObjectDetector(cascade, 30);
            detector.SearchMode = ObjectDetectorSearchMode.NoOverlap;
            detector.ScalingMode = ObjectDetectorScalingMode.GreaterToSmaller;
            detector.ScalingFactor = 1.4f;
            detector.UseParallelProcessing = true;
            objects = detector.ProcessFrame(bmp);
            bmpOut = drawRec(objects, bmp);
        }
       
       void getObjects(Bitmap bmp)
        {
            HaarCascade cascade = new FaceHaarCascade();
          //  detector = new HaarObjectDetector(cascade, 30);
          //  detector.SearchMode = ObjectDetectorSearchMode.NoOverlap;
          //  detector.ScalingMode = ObjectDetectorScalingMode.SmallerToGreater;
          //  detector.ScalingFactor = 1.5f;
          //  detector.UseParallelProcessing = true;
          //objects = detector.ProcessFrame(bmp);
            
        }
        public Bitmap drawRec(Rectangle[] objects, Bitmap bmp)
        {
            Bitmap bmpout = new Bitmap(bmp);
          //  MessageBox.Show("BmpHeight= " + bmpout.Height + " Rectangles No.=" + objects.Length);
            using (Graphics g = Graphics.FromImage(bmpout))
            {
                foreach(Rectangle obj in objects)
                {
                    g.DrawRectangle(Pens.Red, obj);

                }


            }
            return bmpout;

        }
    }
}
