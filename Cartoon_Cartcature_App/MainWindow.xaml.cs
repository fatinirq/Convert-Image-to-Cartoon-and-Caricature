
using System.Windows;
using Cartoon_Face;
using System.Drawing;
using Cartoon_KMCG;

namespace Cartoon_Cartcature_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int i=0;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void tglEye_Click(object sender, RoutedEventArgs e)
        {
            if (tglEye.IsChecked == true)
                siEye.Visibility = Visibility.Visible;
            else
                siEye.Visibility = Visibility.Collapsed;
        }

        private void siEye_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {if (siEye.IsLoaded == true)
            {
                if (siEye.Value == 1)
                    txtEye.Text = "Shrink Eye Vertically";
                if (siEye.Value == 2)
                    txtEye.Text = "Shrink Eye Horizontally";
                if (siEye.Value == 3)
                    txtEye.Text = "Shrink Eye Vertically and Horizontally";
                if (siEye.Value == 4)
                    txtEye.Text = "Stretch Eye Vertically";
                if (siEye.Value == 5)
                    txtEye.Text = "Stretch Eye Horizontally";
                if (siEye.Value == 6)
                    txtEye.Text = "Stretch Eye Vertically and Horizontally";
            }

        }

        private void siEye_Loaded(object sender, RoutedEventArgs e)
        {
            if (siEye.Value == 1)
                txtEye.Text = "Shrink Eye Vertically";

        }

        private void siNose_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (siNose.IsLoaded == true)
            {
                if (siNose.Value == 1)
                    txtNose.Text = "Shrink Nose Vertically";
                if (siNose.Value == 2)
                    txtNose.Text = "Shrink Nose Horizontally";
                if (siNose.Value == 3)
                    txtNose.Text = "Shrink Nose Vertically and Horizontally";
                if (siNose.Value == 4)
                    txtNose.Text = "Stretch Nose Vertically";
                if (siNose.Value == 5)
                    txtNose.Text = "Stretch Nose Horizontally";
                if (siNose.Value == 6)
                    txtNose.Text = "Stretch Nose Vertically and Horizontally";
            }

        }

        private void siNose_Loaded(object sender, RoutedEventArgs e)
        {
            if (siNose.Value == 1)
                txtNose.Text = "Shrink Nose Vertically";
        }

        private void tglNose_Loaded(object sender, RoutedEventArgs e)
        {
            if (tglNose.IsChecked == true)
                siNose.Visibility = Visibility.Visible;
            else
                siNose.Visibility = Visibility.Collapsed;
        }

        private void tglNose_Click(object sender, RoutedEventArgs e)
        {
            if (tglNose.IsChecked == true)
                siNose.Visibility = Visibility.Visible;
            else
                siNose.Visibility = Visibility.Collapsed;
        }

        private void siMouth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (siMouth.IsLoaded == true)
            {
                if (siMouth.Value == 1)
                    txtMouth.Text = "Shrink Mouth Vertically";
                if (siMouth.Value == 2)
                    txtMouth.Text = "Shrink Mouth Horizontally";
                if (siMouth.Value == 3)
                    txtMouth.Text = "Shrink Mouth Vertically and Horizontally";
                if (siMouth.Value == 4)
                    txtMouth.Text = "Stretch Mouth Vertically";
                if (siMouth.Value == 5)
                    txtMouth.Text = "Stretch Mouth Horizontally";
                if (siMouth.Value == 6)
                    txtMouth.Text = "Stretch Mouth Vertically and Horizontally";
            }
        }

        private void siMouth_Loaded(object sender, RoutedEventArgs e)
        {
            if (siMouth.Value == 1)
                txtMouth.Text = "Shrink Mouth Vertically";
        }

        private void tglMouth_Loaded(object sender, RoutedEventArgs e)
        {
            if (tglMouth.IsChecked == true)
                siMouth.Visibility = Visibility.Visible;
            else
                siMouth.Visibility = Visibility.Collapsed;
        }

        private void tglMouth_Click(object sender, RoutedEventArgs e)
        {
            if (tglMouth.IsChecked == true)
                siMouth.Visibility = Visibility.Visible;
            else
                siMouth.Visibility = Visibility.Collapsed;
        }

        private void btnProceed_Click(object sender, RoutedEventArgs e)
        {
            Bitmap bmp;
            Cartoon_Face.Carcature.caricature((bool)tglNose.IsChecked,(bool) tglMouth.IsChecked,(bool) tglEye.IsChecked,
                (int)siNose.Value, (int)siMouth.Value,(int) siEye.Value);
            original.Source = Cartoon_Face.Convert2WPFBitmap.Win2WPFBitmap(Cartoon_Face.Carcature.bmp);
            if (tglCart.IsChecked == true)
            {
                Bitmap bmpOut =new Bitmap( Cartoon_KMCG.Cartoon.Convert2Cartoon(Cartoon_Face.Carcature.bmpout));
                bmpOut.Save("Frame" + i.ToString() + ".jpg");
                i++;
                art.Source = Cartoon_Face.Convert2WPFBitmap.Win2WPFBitmap(bmpOut);
                bmpOut.Dispose();
            }
            else
            {
                Bitmap bmpOut = Cartoon_Face.Carcature.bmpout;
                bmpOut.Save("Frame" + i.ToString() + ".jpg");
                i++;
                art.Source = Cartoon_Face.Convert2WPFBitmap.Win2WPFBitmap(Cartoon_Face.Carcature.bmpout);
                bmpOut.Dispose();
            }
                Cartoon_Face.Carcature.bmpout.Dispose();
        }
    }
}
