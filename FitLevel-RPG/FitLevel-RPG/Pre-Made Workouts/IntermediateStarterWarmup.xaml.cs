using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace FitLevel_RPG.Pre_Made_Workouts
{
    /// <summary>
    /// Interaction logic for IntermediateStarterWarmup.xaml
    /// </summary>
    public partial class IntermediateStarterWarmup : Page
    {
        DispatcherTimer t1 = new DispatcherTimer();
        Stopwatch sw;


        int sessionXP = 5;
        string timeEstimate = "37 minutes";
        public IntermediateStarterWarmup()
        {
            InitializeComponent();
            xpRewardText.Visibility = Visibility.Hidden;
            ButtonEnd.IsEnabled = true;
            timeEstimateText.Text = "Estimated time to complete: " + timeEstimate;
            sw = new Stopwatch();
            t1 = new DispatcherTimer();
            //t1.Interval = new TimeSpan(0, 0, 1);

            t1.Tick += T1_Tick;
            t1.Start();
        }


        private void BeginButton_Click(object sender, RoutedEventArgs e)
        {
            sw.Start();
            ButtonBegin.IsEnabled = false;

        }
        private void T1_Tick(object sender, EventArgs e)
        {

            TimeSpan ts = sw.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            timerTextBlock.Text = "Elapsed Time: " + elapsedTime;
        }

        private void EndWorkout_Click(object sender, RoutedEventArgs e)
        {
            sw.Stop();

            // DB CONNECTION
            SqlConnection sqlCon = new SqlConnection(@"Data Source=fitlevelrpg1.database.windows.net;Initial Catalog=FitLevelRPG;User ID=rpglogin;Password=HiQ!w2g6SFS;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    // Login check
                    sqlCon.Open();
                    String query = "SELECT COUNT(1) FROM [User] WHERE username=@username";
                    SqlCommand cmd = new SqlCommand(query, sqlCon);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@username", LoggedInView.LoggedInUser);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 1)
                    {
                        // NEEDS TO BE UPDATED WITH NEW SQL QUERIES

                        ButtonEnd.IsEnabled = false;
                        /*String xpUpdate = "UPDATE Experience SET experience_points + @xpAmount WHERE user_id=@user_id";
                        SqlCommand cmd2 = new SqlCommand(xpUpdate, sqlCon);
                        cmd2.CommandType = System.Data.CommandType.Text;
                        cmd2.Parameters.AddWithValue("@xpAmount", sessionXP);
                        cmd2.Parameters.AddWithValue("@user_id", LoggedInView.LoggedInUserID);
                        cmd2.ExecuteScalar();*/
                        xpRewardText.Text = "For completing the workout, you have been awarded " + sessionXP + " experience!";
                        xpRewardText.Visibility = Visibility.Visible;

                    }
                    else
                    {
                        MessageBox.Show("Unkown error has occured.", "Error");
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                sqlCon.Close();
            }
        }
    }
}
