using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitLevel_RPG
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TextBlockHeader.Text = "Welcome to FitLevel RPG!\n" +
                "Please login or register.";
           
        }

        private void BtnClickLRegister(object sender, RoutedEventArgs e)
        {
            
            Main.Content = new Register();
        }

        private void BtnClickLLogin(object sender, RoutedEventArgs e)
        {
            Main.Content = new Login();
        }

        private void Load()
        {
            
        }
    }
}
