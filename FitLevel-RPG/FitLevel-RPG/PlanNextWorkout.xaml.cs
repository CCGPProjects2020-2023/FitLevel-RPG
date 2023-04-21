﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for PlanNextWorkout.xaml
    /// </summary>
    public partial class PlanNextWorkout : Page
    {
        DataTable dt = new DataTable("WorkoutPlan");
        public PlanNextWorkout()
        {
            InitializeComponent();
            FillData();

        }


        private void BtnClickWorkoutHistory(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("WorkoutHistory.xaml", UriKind.Relative));
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FillData()
        {
            string CmdString = string.Empty;
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=fitlevelrpg1.database.windows.net;Initial Catalog=FitLevelRPG;User ID=rpglogin;Password=HiQ!w2g6SFS;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))

            {
                //FIX
                CmdString = "SELECT workout_id AS [ID], start_time AS start_time, end_time AS end_time FROM Workout WHERE user_id=@user_id";

                SqlCommand cmd = new SqlCommand(CmdString, sqlCon);
                cmd.Parameters.AddWithValue("@user_id", LoggedInView.LoggedInUserID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                dataGrid.ItemsSource = dt.DefaultView;

            }
        }

        private void addExercise_Click(object sender, RoutedEventArgs e)
        {
            AddExercise ae = new AddExercise();
            ae.Show();
        }

        // FUTURE FEATURE - Delete
        private void deletePlanButton_Click(object sender, RoutedEventArgs e)
        {
         
            //Needs adjustments
            SqlConnection sqlCon = new SqlConnection(@"Data Source=fitlevelrpg1.database.windows.net;Initial Catalog=FitLevelRPG;User ID=rpglogin;Password=HiQ!w2g6SFS;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    

                    sqlCon.Open();
                    String query = "SELECT COUNT(1) FROM Workout WHERE workout_id=@workout_id";
                    SqlCommand cmd = new SqlCommand(query, sqlCon);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@workout_id", Convert.ToInt32(deleteIdTextbox.Text));
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count == 1)
                    {

                        String deletePlan = "DELETE FROM Workout WHERE user_id=@user_id AND workout_id=@wid";
                        SqlCommand cmd2 = new SqlCommand(deletePlan, sqlCon);
                        cmd2.CommandType = System.Data.CommandType.Text;
                        cmd2.Parameters.AddWithValue("@user_id", LoggedInView.LoggedInUserID);
                        cmd2.Parameters.AddWithValue("@wid", Convert.ToInt32(deleteIdTextblock.Text));
                        cmd2.ExecuteScalar();
                        dt.Rows.Clear();
                        FillData();
                        MessageBox.Show("Plan with ID " + deleteIdTextbox.Text + " has been successfully deleted.");
                    }
                    else
                    {
                        MessageBox.Show("Invalid ID. ID does not exist for your account.", "Error");
                    }
                }
            } catch (InvalidCastException)
            {
                MessageBox.Show("Invalid cast.");
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nMake sure you entered a number.", "Error Has Occurred", MessageBoxButton.OK,MessageBoxImage.Warning);
            }
            finally
            {

                sqlCon.Close();
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            dt.Rows.Clear();           
            FillData();
        }

        private void setButton_Click(object sender, RoutedEventArgs e)
        {
            AddSet aw = new AddSet();
            aw.Show();
        }
    }
}
