using System;
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

                CmdString = "SELECT id AS [ID], wset AS [Set], sreps AS Reps, sweight AS Weight, CONVERT(VARCHAR(10), createdate ,111) AS Date FROM WorkoutPlan WHERE username=@username";

                SqlCommand cmd = new SqlCommand(CmdString, sqlCon);
                cmd.Parameters.AddWithValue("@username", LoggedInView.LoggedInUser);              
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                dataGrid.ItemsSource = dt.DefaultView;

            }
        }

        private void addExercise_Click(object sender, RoutedEventArgs e)
        {
            AddWorkout aw = new AddWorkout();
            aw.Show();
        }

        private void deletePlanButton_Click(object sender, RoutedEventArgs e)
        {
         

            SqlConnection sqlCon = new SqlConnection(@"Data Source=fitlevelrpg1.database.windows.net;Initial Catalog=FitLevelRPG;User ID=rpglogin;Password=HiQ!w2g6SFS;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    // Login check

                    sqlCon.Open();
                    String query = "SELECT COUNT(1) FROM WorkoutPlan WHERE id=@id";
                    SqlCommand cmd = new SqlCommand(query, sqlCon);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", deleteIdTextbox.Text);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count == 1)
                    {

                        String deletePlan = "DELETE FROM WorkoutPlan WHERE id=@id AND username=@username";
                        SqlCommand cmd2 = new SqlCommand(deletePlan, sqlCon);
                        cmd2.CommandType = System.Data.CommandType.Text;
                        cmd2.Parameters.AddWithValue("@id", deleteIdTextbox.Text);
                        cmd2.Parameters.AddWithValue("@username", LoggedInView.LoggedInUser);
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

        }
    }
}
