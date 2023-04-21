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
using System.Diagnostics;
using System.Windows.Threading;
using System.Data.SqlClient;
using System.Data;

namespace FitLevel_RPG
{
    /// <summary>
    /// Interaction logic for TrackWorkout.xaml
    /// </summary>
    /// 
    public partial class TrackWorkout : Page
    {
        DataTable dt = new DataTable("WorkoutPlan");
        DispatcherTimer t1 = new DispatcherTimer();
        Stopwatch sw;
        List<string> list = new List<string>();
        public TrackWorkout()
        {
            InitializeComponent();
            sw = new Stopwatch();
            t1 = new DispatcherTimer();
            //t1.Interval = new TimeSpan(0, 0, 1);

            t1.Tick += T1_Tick;
            t1.Start();
            FillData();
        }

        private void BeginButton_Click(object sender, RoutedEventArgs e)
        {
            sw.Start();
            StartTimerButton.IsEnabled = false;

        }

        private void FillData()
        {
            string CmdString = string.Empty;
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=fitlevelrpg1.database.windows.net;Initial Catalog=FitLevelRPG;User ID=rpglogin;Password=HiQ!w2g6SFS;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                CmdString = "SELECT id AS [ID], wset AS [Set], sreps AS Reps, sweight AS Weight, CONVERT(VARCHAR(10), createdate ,111) AS Date FROM WorkoutPlan WHERE username=@username";

                SqlCommand cmd = new SqlCommand(CmdString, sqlCon);
                cmd.Parameters.AddWithValue("@username", LoggedInView.LoggedInUser);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                dataGrid.ItemsSource = dt.DefaultView;
            }
        }


        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            sw.Stop();

        }

        private void T1_Tick(object sender, EventArgs e)
        {

            TimeSpan ts = sw.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            timerTextBlock.Text = "Elapsed Time: " + elapsedTime;
        }
    }
}
