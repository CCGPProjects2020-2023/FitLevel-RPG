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
        public static int LoggedInUserID { get; set; }
        
        public LoggedInView()
        {
            InitializeComponent();
            SetMainContent(new UserDashboard()); // The dashboard is the first thing the user sees
            welcomeTextBlock.Text = "Welcome, " + LoggedInUser.ToUpper() + "!";
            versionInfo.Text = VersionInfo.getVersionInfo();
        }

        public void SetMainContent(object content)
        {
            Main.Content = content;
        }
        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            SetMainContent(new UserDashboard());
        }
        

        private void TrackWorkoutButton_Click(object sender, RoutedEventArgs e)
        {
            SetMainContent(new TrackWorkout());
        }

        private void WorkoutOverviewButton_Click(object sender, RoutedEventArgs e)
        {
            SetMainContent(new PlanNextWorkout());
        }

        private void PremadeWorkoutsButton_Click(object sender, RoutedEventArgs e)
        {
            SetMainContent(new PreMadeWorkouts());
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this);
            LoggedInUser = "";
            LoggedInUser = null;
            MainWindow mw = new();
            mw.Show();
            parentWindow.Close();
        }

        private void ModifyUserInfo_Click(object sender, RoutedEventArgs e)
        {
            SetMainContent(new ModifyUserInfo());
        }
    }
}
