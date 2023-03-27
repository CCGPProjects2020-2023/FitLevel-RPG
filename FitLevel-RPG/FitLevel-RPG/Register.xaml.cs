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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {

        public Register()
        {
            InitializeComponent();
        }

        private void RegisterButtonClick(object sender, RoutedEventArgs e)
        {
          
            MessageBox.Show("Fake Register Success.");
            NavigationService.Navigate(new Uri("Login.xaml", UriKind.Relative));

        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {           
            NavigationService.Navigate(new Uri("Login.xaml", UriKind.Relative));
        }
    }
}
