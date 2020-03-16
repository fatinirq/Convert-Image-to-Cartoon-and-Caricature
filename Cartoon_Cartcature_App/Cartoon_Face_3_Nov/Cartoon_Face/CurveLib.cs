using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Cartoon_Face
{
    public class CurveLib
    {
       public struct CubicBezier
        {
            public System.Windows.Point P0;
            public Point P1;
            public Point P2;
            public Point P3;
            public int noPoints;
        }
        public class Cordinates
        {
            public Cordinates(int i, int j,float grad, float dir)
            {
                x = i;
                y = j;
                gradiant = grad;
                direction = dir;
            }
            public int x;
            public int y;
            public float gradiant;
            public float direction;
        }

        public class stEdgeMap
        {
            public List<Cordinates> lstMap = new List<Cordinates>();
        }
        public class CurveCalculations
        {
           public  CurveData[] data;
            public CubicBezier cb;
            public CurveCalculations(stEdgeMap st)
            {
                init(st);
                curvatureCalculation();
                estimateBezier();
                
            }
            public void init(stEdgeMap st)
            {
                
                data = new CurveData[st.lstMap.Count];

                int count = 0;
                for (int i = 0; i < st.lstMap.Count & st.lstMap.Count>=3; i++)
                {

                    data[count].x = st.lstMap[i].x;
                    data[count].y = st.lstMap[i].y;
                    
                    data[count].seq = i+1;
                    count++;
                }
            }
            public void curvatureCalculation()
            {
                if (data.Length >= 3)
                {
                    for (int i = 1; i < data.Length; i++)
                    {
                        data[i].dx = (data[i].x - data[i - 1].x);
                        data[i].dy = (data[i].y - data[i - 1].y);

                    }
                    for (int i = 2; i < data.Length; i++)
                    {
                        data[i].ddx = (data[i].dx - data[i - 1].dx);
                        data[i].ddy = (data[i].dy - data[i - 1].dy);

                    }
                    
                    for (int i = 2; i < data.Length; i++)
                    {
                        data[i].curvature =(data[i].dx * data[i].ddy - data[i].dy * data[i].ddx)/ Math.Sqrt(Math.Pow(Math.Pow(data[i].dx, 2) + Math.Pow(data[i].dy, 2), 3));
                    }
                }

            }
            public void estimateBezier()
            {
                double[] tempcurvature;
                List<CubicBezier> lstCB = new List<CubicBezier>();
                if (data.Length >= 10)
                {
                    double min = 0, max = 0;
                    double intCurvature = Math.Abs(data[2].curvature);
                    double tempCurvature = 0;
                    int firstPoint = 1;
                    cb = new CubicBezier();
                    cb.P0.X = data[0].x;
                    cb.P0.Y = data[0].y;
                    int firstP = 1;
                    cb.P1.X = (int)(data[1].dx / 3 + data[1].x);
                    cb.P1.Y = (int)(data[1].dy / 3 + data[1].y);
                    cb.P2.X = (int)(data[data.Length - 2].x - data[data.Length - 2].dx / 3);
                    cb.P2.Y = (int)(data[data.Length - 2].y - data[data.Length - 2].dy / 3);
                    cb.P3.X = data[data.Length-1].x;
                    cb.P3.Y = data[data.Length-1].y;
                    cb.noPoints = data.Length;
                   
                    //for (int i = 2; i < data.Length && data[i].curvature != 0; i++)
                    //{
                    //   if (Math.Abs(intCurvature - data[i].curvature) >= 0.1 || i == data.Length - 1)
                    //    {

                    //        cb.P1.X = (int)(data[1].dx / 3 + data[1].x);
                    //        cb.P1.Y = (int)(data[1].dy / 3 + data[1].y);
                    //        cb.P2.X = (int)(data[i - 1].x - data[i - 1].dx / 3);
                    //        cb.P2.Y = (int)(data[i - 1].y - data[i - 1].dy / 3);
                    //        cb.P3.X = data[i].x;
                    //        cb.P3.Y = data[i].y;
                    //        cb.noPoints = i - firstP;
                    //        firstP = i;

                    //    }

                    //}

                }


            }
        }
        public class CurveDrawing
        {
            public Path BezierDraw(CubicBezier cb)
            {
                
                PathGeometry myGeo = new PathGeometry();
                PathFigure myFig = new PathFigure();
                Path myPath = new Path();
                System.Windows.Point[] points = new System.Windows.Point[cb.noPoints];
                Double t = 0.0;
                int i = 0;
                double ratio = 1/cb.noPoints ;
                List<LineSegment> segments = new List<LineSegment>();
             
                while (i < cb.noPoints)
                {
                    points[i].X = (double)(Math.Pow(1 - t, 3) * cb.P0.X + 3 * Math.Pow(1 - t, 2) * t * cb.P1.X + 3 * (1 - t) * Math.Pow(t, 2) * cb.P2.X + Math.Pow(t, 3) * cb.P3.X);
                    points[i].Y = (double)(Math.Pow(1 - t, 3) * cb.P0.Y + 3 * Math.Pow(1 - t, 2) * t * cb.P1.Y + 3 * (1 - t) * Math.Pow(t, 2) * cb.P2.Y + Math.Pow(t, 3) * cb.P3.Y);
                    t = t + ratio;
                    Console.WriteLine(points[i].ToString());

                    segments.Add(new LineSegment(points[i], true));
                   
                    i = i + 1;

                }
               
                myFig = new PathFigure(cb.P0, segments, false);
                myGeo.Figures.Add(myFig);
                GeometryGroup gg = new GeometryGroup();

                
                myPath.Stroke = Brushes.DarkOrange;
                myPath.StrokeThickness = 1;
                myPath.Data = gg;
                gg.Children.Add(myGeo);
                return myPath;
            }

        }
       public struct CurveData
        {
            public int x;
            public int y;
            public double curvature;
            public double dx;
            public double dy;
            public double ddx;
            public double ddy;
            public int seq;
           
        }
       
    }
}
