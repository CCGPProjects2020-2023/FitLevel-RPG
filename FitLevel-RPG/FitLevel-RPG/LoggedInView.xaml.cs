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
using System.Windows.Shapes;

namespace FitLevel_RPG
{
    /// <summary>
    /// Interaction logic for UserDashboard.xaml
    /// </summary>
    public partial class LoggedInView : Window
    {
        public static string LoggedInUser { get; set; }
        public static string LoggedInUserID { get; set; }
        
        public LoggedInView()
        {
            InitializeComponent();
            Main.Content = new UserDashboard(); // The dashboard is the first thing the user sees
            welcomeTextBlock.Text = "Welcome, " + LoggedInUser.ToUpper() + "!";
            versionInfo.Text = VersionInfo.getVersionInfo();
        }


        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new UserDashboard();
        }
        

        private void TrackWorkoutButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new TrackWorkout();
        }

        private void WorkoutOverviewButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PlanNextWorkout();
        }

        private void PremadeWorkoutsButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PreMadeWorkouts();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this);
            LoggedInUser = "";
            LoggedInUser = null;
            MainWindow mw = new MainWindow();
            mw.Show();
            parentWindow.Close();
        }
    }
}
