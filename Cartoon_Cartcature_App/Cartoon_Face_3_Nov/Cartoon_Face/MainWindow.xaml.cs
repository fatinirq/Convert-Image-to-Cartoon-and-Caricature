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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UC_Viola uc_viola ;
        UC_FaceDrawingModel uc_FaceDrawing;
        UC_FacialLandMark uc_FacialLandMark;
        //UC_Viola_2 uc_viola_2;
        UC_Preprocessing uc_prep;
        UC_FinalResultsCaricature uc_finalCaricature;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLIPImage_Click(object sender, RoutedEventArgs e)
        {
           

        }

        private void btnViola_Click(object sender, RoutedEventArgs e)
        {
            uc_viola = new UC_Viola();
            mainArea.Children.Clear();
            mainArea.Children.Add(uc_viola);

        }

        private void btnLandMark_Click(object sender, RoutedEventArgs e)
        {
            uc_FacialLandMark = new UC_FacialLandMark();
            mainArea.Children.Clear();
            mainArea.Children.Add(uc_FacialLandMark);
        }

        private void btnViola2_Click(object sender, RoutedEventArgs e)
        {
            //uc_viola_2 = new UC_Viola_2();
            //mainArea.Children.Clear();
           // mainArea.Children.Add(uc_viola_2);
        }

        private void preProc_Click(object sender, RoutedEventArgs e)
        {
            uc_prep = new UC_Preprocessing();
            mainArea.Children.Clear();
            mainArea.Children.Add(uc_prep);
        }

        private void btnFaceDrawing_Click(object sender, RoutedEventArgs e)
        {
            uc_FaceDrawing = new UC_FaceDrawingModel();
            mainArea.Children.Clear();
            mainArea.Children.Add(uc_FaceDrawing);
        }

        private void btnFinalResults_Click(object sender, RoutedEventArgs e)
        {
            uc_finalCaricature = new UC_FinalResultsCaricature();
            mainArea.Children.Clear();
            mainArea.Children.Add(uc_finalCaricature);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            uc_finalCaricature = new UC_FinalResultsCaricature();
            mainArea.Children.Clear();
            mainArea.Children.Add(uc_finalCaricature);
        }
    }
}
