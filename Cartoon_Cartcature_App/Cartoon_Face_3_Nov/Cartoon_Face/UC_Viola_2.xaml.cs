using Microsoft.Win32;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace Cartoon_Face
{
    /// <summary>
    /// Interaction logic for UC_Viola_2.xaml
    /// </summary>
    public partial class UC_Viola_2 : UserControl
    {
        public UC_Viola_2()
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
                    bmp = new Bitmap(ofd.FileName);
                   // MessageBox.Show(bmp.Height + " " + bmp.Width);
                    FaceDetection face = new FaceDetection(bmp);
                    Rec.Source = Convert2WPFBitmap.Win2WPFBitmap(face.bmpOut);
                }

                catch (ApplicationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
        }
    }
}