using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Cartoon_KMCG
{
    /// <summary>
    /// Interaction logic for UC_LIPColor.xaml
    /// </summary>
    public partial class UC_LIPColor : UserControl
    {
        public UC_LIPColor()
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
                    if (!Directory.Exists(filename.Substring(0, filename.IndexOf('.')) + "/Results"))
                    {
                        Directory.CreateDirectory(filename.Substring(0, filename.IndexOf('.')) + "/Results");
                    }
                    string mainDirectry = filename.Substring(0, filename.IndexOf('.')) + "/Results";
                    string path_original = System.IO.Path.Combine(mainDirectry, "Results");
                    bmp = new Bitmap(ofd.FileName);
                    bmp.Save(mainDirectry + "//originalBmp.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    Original.Source = Convert2WPFBitmap.Win2WPFBitmap(bmp);
                    Bitmap ndEnhancement = new Bitmap(ImageEnhancement.colorLIPMult_Comp(bmp));
                    Bitmap bothEnhancement = new Bitmap(ImageEnhancement.colorLIPMult(ndEnhancement));
                    Bitmap stEnhancement = new Bitmap(ImageEnhancement.colorLIPMult(bmp,10));
                    stEnhancement.Save(mainDirectry + "//stEnhancement.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    Enhanced.Source = Convert2WPFBitmap.Win2WPFBitmap(stEnhancement);
                    Enhanced_Both.Source = Convert2WPFBitmap.Win2WPFBitmap(bothEnhancement);
                    Enhanced_Comp.Source = Convert2WPFBitmap.Win2WPFBitmap(ndEnhancement);
                }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
