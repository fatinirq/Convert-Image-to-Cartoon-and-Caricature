using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UC_LIPColor uc_LIPColor;
        UC_System1 uc_System1;
        UC_System_2 uc_System2;
        UC_Results_Eval uc_Results_Eval;
        public MainWindow()
        {
            InitializeComponent();
        }

      

        private void btnLIPImage_Click(object sender, RoutedEventArgs e)
        {
            uc_LIPColor = new UC_LIPColor();
            mainArea.Children.Clear();
            mainArea.Children.Add(uc_LIPColor);

        }

        private void btnKMCG_Click(object sender, RoutedEventArgs e)
        {
            uc_System1 = new UC_System1();
            mainArea.Children.Clear();
            mainArea.Children.Add(uc_System1);

        }

        private void btnKMCG_Old_Click(object sender, RoutedEventArgs e)
        {
            uc_System2 = new UC_System_2();
            mainArea.Children.Clear();
            mainArea.Children.Add(uc_System2);

        }

        private void Final_Results_Click(object sender, RoutedEventArgs e)
        {
            uc_Results_Eval = new UC_Results_Eval();
            mainArea.Children.Clear();
            mainArea.Children.Add(uc_Results_Eval);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            uc_Results_Eval = new UC_Results_Eval();
            mainArea.Children.Clear();
            mainArea.Children.Add(uc_Results_Eval);
        }
    }
}
