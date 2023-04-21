using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

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
            SqlConnection sqlCon2 = new(@"Data Source=fitlevelrpg1.database.windows.net;Initial Catalog=FitLevelRPG;User ID=rpglogin;Password=HiQ!w2g6SFS;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                    sqlCon2.Open();
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
                        JOIN [Set] S ON E.exercise_id = S.exercise_id
                        GROUP BY W.user_id
                        ORDER BY W.user_id";
                   
                    
                    SqlCommand cmd2 = new(query2, sqlCon2);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using SqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        DateTime date = reader2.GetDateTime(0);
                        int volume = reader2.GetInt32(1);

                        dates.Add(date);
                        volumes.Add(volume);
                    }
                    sqlCon2.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
                sqlCon2.Close();


            }

            var lastDatesValues = dates.Skip(Math.Max(0, dates.Count - 10)).Take(10);
            var lastVolumesValues = volumes.Skip(Math.Max(0, volumes.Count - 10)).Take(10);
            var plotModel = new PlotModel
            {
                Title = "Example Plot",             // Reduce the padding and margin around the plot area
                Padding = new OxyThickness(50),
                PlotMargins = new OxyThickness(0),
                PlotAreaBorderThickness = new OxyThickness(0)
            };

            var lineSeries = new LineSeries();

            for (int i = 0; i < lastVolumesValues.Count(); i++)
            {
                lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(lastDatesValues.ElementAt(i)), lastVolumesValues.ElementAt(i)));
            }

            plotModel.Series.Add(lineSeries);

            MyPlot.Model = plotModel;

            currentUserLevel.Content = "Current Lvl: " + levelNumber;
            currentXpLabel.Content = "Current XP: " + currentXP;
            requiredXpLabel.Content = "Next Level at: " + nextLevelXP + " XP";
            xpPercentLabel.Content = "Level Progress - " + (currentXP / nextLevelXP * 100) + "%";
            xpBar.Value = currentXP;

            
        }
        private void ModifyUserInfoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Uri("ModifyUserInfo.xaml", UriKind.Relative));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
