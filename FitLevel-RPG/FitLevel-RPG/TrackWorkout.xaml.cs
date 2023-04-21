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

        SqlConnection sqlCon = new SqlConnection(@"Data Source=fitlevelrpg1.database.windows.net;Initial Catalog=FitLevelRPG;User ID=rpglogin;Password=HiQ!w2g6SFS;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
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

        // ID Containers
        static public int workoutID;
        static public int exerciseID;

        private void BeginButton_Click(object sender, RoutedEventArgs e)
        {
            sw.Start();
            StartTimerButton.IsEnabled = false;
           
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    // NEEDS FIXED. SQL IS IN WRONG ORDER I THINK
                    sqlCon.Open();
                    String startWorkoutQuery = "INSERT INTO Workout (user_id, start_time) VALUES (@user_id, @start_time); SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(startWorkoutQuery, sqlCon);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@user_id", LoggedInView.LoggedInUserID);
                    cmd.Parameters.AddWithValue("@start_time", DateTime.Now);
                    workoutID = Convert.ToInt32(cmd.ExecuteScalar());                 
                }

                else
                {

                    MessageBox.Show("Unkown error has occured.", "Error");
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {

                sqlCon.Close();
            }

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

            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    // FIX
                    sqlCon.Open();
                    String startWorkoutQuery = "UPDATE Workout SET end_time=@end_time WHERE workout_id=@workout_id AND user_id=@user_id";
                    SqlCommand cmd = new SqlCommand(startWorkoutQuery, sqlCon);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@workout_id", workoutID);
                    cmd.Parameters.AddWithValue("@user_id", LoggedInView.LoggedInUserID);
                    cmd.Parameters.AddWithValue("@end_time", DateTime.Now);
                    cmd.ExecuteScalar();
                }

                else
                {

                    MessageBox.Show("Unkown error has occured.", "Error");
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {

                sqlCon.Close();
            }

        }

        private void T1_Tick(object sender, EventArgs e)
        {

            TimeSpan ts = sw.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            timerTextBlock.Text = "Elapsed Time: " + elapsedTime;
        }

        private void AddExercise_Click(object sender, RoutedEventArgs e)
        {
            AddExercise ae = new AddExercise();
            ae.Show();
        }

        private void AddSetButton_Click(object sender, RoutedEventArgs e)
        {
            AddSet addset = new AddSet();
            addset.Show();
        }
    }
}
