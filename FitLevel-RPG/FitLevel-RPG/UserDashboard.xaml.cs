using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using Plotly.NET;

namespace FitLevel_RPG
{
    /// <summary>
    /// Interaction logic for UserDashboard.xaml
    /// </summary>
    public partial class UserDashboard : Page
    {
        float currentXP = 80f;
        float nextLevelXP = 100f;
        int currentLevel = 1;

        int totalExperience = 0;
        int levelNumber = 0;

        List<DateTime> dates = new();
        List<int> volumes = new();

        public UserDashboard()
        {
            InitializeComponent();
            SqlConnection sqlCon = new(@"Data Source=fitlevelrpg1.database.windows.net;Initial Catalog=FitLevelRPG;User ID=rpglogin;Password=HiQ!w2g6SFS;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                    // Calculate total experience and retrieve level information
                    String query = @"
                        SELECT SUM(E.experience_points), L.level_number
                        FROM Experience E
                        JOIN [User] U ON E.user_id = U.user_id
                        JOIN Level L ON U.level_id = L.level_id
                        WHERE E.user_id = @user_id
                        GROUP BY L.level_number";

                    SqlCommand cmd = new(query, sqlCon)
                    {
                        CommandType = System.Data.CommandType.Text
                    };
                    cmd.Parameters.AddWithValue("@user_id", LoggedInView.LoggedInUserID);

                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        totalExperience = reader.GetInt32(0);
                        levelNumber = reader.GetInt32(1);
                    }

                    // Calculate total training volume per workout
                    String query2 = @"
                        SELECT W.date, SUM(S.repetitions * S.weight) AS volume
                        FROM Workout W
                        JOIN Exercise E ON W.workout_id = E.workout_id
                        JOIN Sets S ON E.exercise_id = S.exercise_id
                        GROUP BY W.date
                        ORDER BY W.date";

                    SqlCommand cmd2 = new(query2, sqlCon);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using SqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        DateTime date = reader2.GetDateTime(0);
                        int volume = reader2.GetInt32(1);

                        dates.Add(date);
                        volumes.Add(volume);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }

            // Create a trace
            var trace = new Trace("scatter");
            trace.SetValue("x", dates);
            trace.SetValue("y", volumes);

            // Create a layout
            var layout = new Layout();
            layout.SetValue("title", "Workout Volume Over Time");

            // Create a chart
            var chart = Chart.Plot(trace);
            chart.WithLayout(layout);

            // Display the chart
            chart.Show();

            currentUserLevel.Content = "Current Lvl: " + currentLevel;
            currentXpLabel.Content = "Current XP: " + currentXP;
            requiredXpLabel.Content = "Next Level at: " + nextLevelXP + " XP";
            xpPercentLabel.Content = "Level Progress - " + (currentXP / nextLevelXP * 100) + "%";
            xpBar.Value = currentXP;
        }
    }
}
