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
    /// Interaction logic for TrackWorkout.xaml
    /// </summary>
    public partial class TrackWorkout : Page
    {
        public TrackWorkout()
        {
            InitializeComponent();
        }
        private void LogoutButtonClick(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this);
            MainWindow mw = new MainWindow();
            mw.Show();
            parentWindow.Close();
        }
    }
}
