﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
using System.Threading;
using Timer = System.Timers.Timer;
using System.Windows.Threading;
using System.Data.SqlClient;

namespace FitLevel_RPG.Pre_Made_Workouts
{
    /// <summary>
    /// Interaction logic for BeginnerCardio.xaml
    /// </summary>
    public partial class BeginnerCardio : Page
    {
        DispatcherTimer t1 = new DispatcherTimer();
        Stopwatch sw;


        int sessionXP = 5;
        string timeEstimate = "37 minutes";
        public BeginnerCardio()
        {
            InitializeComponent();
            xpRewardText.Visibility = Visibility.Hidden;
            ButtonEnd.IsEnabled = false;
            timeEstimateText.Text = "Estimated time to complete: " + timeEstimate;
            sw = new Stopwatch();
            t1 = new DispatcherTimer();
            //t1.Interval = new TimeSpan(0, 0, 1); // This will set the amount of time between timer updates. Disabled for now as not needed.

            t1.Tick += T1_Tick;
            t1.Start();
        }


        private void BeginButton_Click(object sender, RoutedEventArgs e)
        {
            sw.Start();
            ButtonBegin.IsEnabled = false;
            ButtonEnd.IsEnabled = true;

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
                    
                    sqlCon.Open();
                    ButtonEnd.IsEnabled = false;
                    String xpUpdate = "INSERT INTO Experience (experience_points, user_id, timestamp)  VALUES (@xpAmount, @user_id, @timestamp)";
                    SqlCommand cmd2 = new SqlCommand(xpUpdate, sqlCon);
                    cmd2.CommandType = System.Data.CommandType.Text;
                    cmd2.Parameters.AddWithValue("@xpAmount", sessionXP);
                    cmd2.Parameters.AddWithValue("@user_id", LoggedInView.LoggedInUserID);
                    cmd2.Parameters.AddWithValue("@timestamp", DateTime.Now);
                    cmd2.ExecuteScalar();
                    xpRewardText.Text = "For completing the workout, you have been awarded " + sessionXP + " experience!";
                    xpRewardText.Visibility = Visibility.Visible;
                }

                else
                {

                    MessageBox.Show("Unkown error has occured.", "Error");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {

                sqlCon.Close();
            }
        }
    }
}
            
        
   
