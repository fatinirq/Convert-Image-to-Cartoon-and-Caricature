using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows;

namespace Cartoon_Face
{
    class Head_Seg
    {
        public static Bitmap  bmp;
        static Rectangle Face;
        static List<Rectangle> Eyes;
       public Head_Seg(Rectangle face, List<Rectangle> eyes, Bitmap bmpTemp, string filename)
        {
            bmp = new Bitmap(bmpTemp);
            Face = face;
            Eyes = new List<Rectangle>(eyes);
         //   _Double_Rec();
        }
        public void _Double_Rec()
        {

            int faceHeight_X = Face.Top-Face.Height/2;
            int faceWidth_Y = Face.Left - Face.Width / 2;
            int faceHeight =(int)( Face.Height * 1.2);
            int faceWidth = (int)(Face.Width * 1.2);
            if (faceHeight_X < 0)
                faceHeight_X = 0;
            if (faceWidth_Y < 0)
                faceWidth_Y = 0;

            Rectangle doubleFace = new Rectangle(faceHeight_X, faceWidth_Y, faceWidth, faceHeight);
            ///////////////////////
            List<Rectangle> lstEyesRec = new List<Rectangle>();
            if (Eyes.Count >=1)
            {
                int steyeHeight_X = Eyes[0].X - Eyes[0].Height / 5;
                int steyeWidth_Y = Eyes[0].Y - Eyes[0].Width / 6;
                int steyeHeight =(int) (Eyes[0].Height * 1.3);
                int steyeWidth =(int)( Eyes[0].Width * 1.3);
                if (steyeHeight_X < 0)
                    steyeHeight_X = 0;
                if (steyeWidth_Y < 0)
                    steyeWidth_Y = 0;

                Rectangle doublesteye = new Rectangle(steyeHeight_X, steyeWidth_Y, steyeWidth, steyeHeight);
                lstEyesRec.Add(doublesteye);
            }
            if (Eyes.Count == 2)
            {
                int ndeyeHeight_X = Eyes[1].X - Eyes[1].Height / 5;
                int ndeyeWidth_Y = Eyes[1].Y - Eyes[1].Width / 5;
                int ndeyeHeight = (int)(Eyes[1].Height * 1.2);
                int ndeyeWidth = (int)(Eyes[1].Width * 1.2);
                if (ndeyeHeight_X < 0)
                    ndeyeHeight_X = 0;
                if (ndeyeWidth_Y < 0)
                    ndeyeWidth_Y = 0;
                Rectangle double2ndeye = new Rectangle(ndeyeHeight_X, ndeyeWidth_Y, ndeyeWidth, ndeyeHeight);
                lstEyesRec.Add(double2ndeye);

            }
            Face = doubleFace;
            Eyes = new List<Rectangle>(lstEyesRec); 
        }
        public   Bitmap _DrawBmp_Rec()
        {
           // Face.Offset(new System.Drawing.Point(20, 20));
            Bitmap bmpOut = new Bitmap(bmp);
            MessageBox.Show("Face TOP = " + Face.Top + " X= " + Face.X + " Face Left=" + Face.Left+ " Face.Y= "+Face.Y);
              
            using (Graphics g = Graphics.FromImage(bmpOut))
            {
                //Face.Offset(Eyes[1].Left-Face.Width/10, Face.Top);
                //Face.Width = Eyes[0].Right - Eyes[1].Left + Face.Width / 10 * 2;
                // g.RotateTransform(270);
              //  Face.X = Math.Min(Eyes[0].X, Eyes[1].X);
                //Face.Y = Math.Min(Eyes[0].Y, Eyes[1].Y);
                // Translate to desired position. Be sure to append
                // the rotation so it occurs after the rotation.
               // g.TranslateTransform(0, Face.Height, MatrixOrder.Append);
                g.DrawRectangle(Pens.Red, Face.X, Face.Y,(int) (Face.Height),(int) (Face.Width));
                 foreach(Rectangle eye in Eyes)
                {
                    
                    g.DrawRectangle(Pens.Green, eye.X, eye.Y, eye.Width, eye.Height);
                }      
                }
            return bmpOut;
            }
        }
    }

