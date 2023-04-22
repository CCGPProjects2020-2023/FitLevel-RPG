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
        readonly DataTable dt = new("WorkoutPlan");
        readonly DispatcherTimer t1;
        readonly Stopwatch sw;

        readonly SqlConnection sqlCon = new(@"Data Source=fitlevelrpg1.database.windows.net;Initial Catalog=FitLevelRPG;User ID=rpglogin;Password=HiQ!w2g6SFS;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public TrackWorkout()
        {
            InitializeComponent();
            sw = new Stopwatch();
            t1 = new DispatcherTimer();

            t1.Tick += T1_Tick;
            t1.Start();
            FillData();
            //try
            //{
            //    if (sqlCon.State == ConnectionState.Closed)
            //    {   
            //        // check for planned workout.
            //        sqlCon.Open();
            //        String getPlannedWorkoutQuery = "SELECT * FROM Workout";
            //        SqlCommand cmd = new(startWorkoutQuery, sqlCon)
            //        {
            //            CommandType = CommandType.Text
            //        };
            //        cmd.Parameters.AddWithValue("@user_id", LoggedInView.LoggedInUserID);
            //        cmd.Parameters.AddWithValue("@start_time", DateTime.Now);
            //        workoutID = Convert.ToInt32(cmd.ExecuteScalar());
            //    }
            //    else
            //    {
            //        MessageBox.Show("Unkown error has occured.", "Error");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error");
            //}
            //finally
            //{
            //    sqlCon.Close();
            //}
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
                if (sqlCon.State == ConnectionState.Closed)
                {
                    // NEEDS FIXED. SQL IS IN WRONG ORDER I THINK
                    sqlCon.Open();
                    String startWorkoutQuery = "INSERT INTO Workout (user_id, start_time) VALUES (@user_id, @start_time); SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new(startWorkoutQuery, sqlCon)
                    {
                        CommandType = CommandType.Text
                    };
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
            using SqlConnection sqlCon = new(@"Data Source=fitlevelrpg1.database.windows.net;Initial Catalog=FitLevelRPG;User ID=rpglogin;Password=HiQ!w2g6SFS;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string CmdString = "SELECT workout_id AS [ID], start_time AS start_time, end_time AS end_time FROM Workout WHERE user_id=@user_id";

            SqlCommand cmd = new(CmdString, sqlCon);
            cmd.Parameters.AddWithValue("@user_id", LoggedInView.LoggedInUserID);
            SqlDataAdapter sda = new(cmd);
            sda.Fill(dt);

            dataGrid.ItemsSource = dt.DefaultView;
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
                    SqlCommand cmd = new(startWorkoutQuery, sqlCon);
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

            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            timerTextBlock.Text = "Elapsed Time: " + elapsedTime;
        }

        private void AddExercise_Click(object sender, RoutedEventArgs e)
        {
            AddExercise ae = new();
            ae.Show();
        }

        private void AddSetButton_Click(object sender, RoutedEventArgs e)
        {
            AddSet addset = new();
            addset.Show();
        }
    }
}
