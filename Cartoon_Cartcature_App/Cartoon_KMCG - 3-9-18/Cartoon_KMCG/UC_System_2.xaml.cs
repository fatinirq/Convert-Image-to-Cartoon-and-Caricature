using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Cartoon_KMCG
{
    /// <summary>
    /// Interaction logic for UC_System_2.xaml
    /// </summary>
    public partial class UC_System_2 : UserControl
    {
        public UC_System_2()
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
                    Original.Source = Convert2WPFBitmap.Win2WPFBitmap(bmp);
                    Bitmap enhancedBmp = new Bitmap(Filters.meanFilter_Color(bmp,3));
                    Enhanced.Source = Convert2WPFBitmap.Win2WPFBitmap(enhancedBmp);
                    Stopwatch sw = new Stopwatch();
                    Bitmap kmcgBmp = new Bitmap(KMCG_Old.KMCGRGB(enhancedBmp,6,filename,sw));
                    KMCG.Source = Convert2WPFBitmap.Win2WPFBitmap(kmcgBmp);
                }
                catch (ApplicationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
        }
    }
}
