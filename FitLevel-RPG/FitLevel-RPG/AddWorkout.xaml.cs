using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace FitLevel_RPG
{
    /// <summary>
    /// Interaction logic for AddWorkout.xaml
    /// </summary>
    public partial class AddWorkout : Window
    {
        public AddWorkout()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=fitlevelrpg1.database.windows.net;Initial Catalog=FitLevelRPG;User ID=rpglogin;Password=HiQ!w2g6SFS;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    // Login check
                    sqlCon.Open();
                    
                    if (setTextbox.Text != "" && repsTextbox.Text != "" && weightTextbox.Text != "")
                    {
                        
                        String addPlan = "INSERT INTO WorkoutPlan(wset, sreps, sweight, username, createdate) VALUES(@set, @reps, @weight, @username, @date)";
                        SqlCommand cmd2 = new SqlCommand(addPlan, sqlCon);
                        cmd2.CommandType = System.Data.CommandType.Text;
                        cmd2.Parameters.AddWithValue("@set", setTextbox.Text);
                        cmd2.Parameters.AddWithValue("@reps", repsTextbox.Text);
                        cmd2.Parameters.AddWithValue("@weight", weightTextbox.Text);
                        cmd2.Parameters.AddWithValue("@date", addDatePicker.SelectedDate);
                        cmd2.Parameters.AddWithValue("@username", LoggedInView.LoggedInUser);
                        cmd2.ExecuteScalar();
                        MessageBox.Show("Plan Added! Click OK to close this window.");
                        PlanNextWorkout pnw = new PlanNextWorkout();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Make sure the values are filled out.", "Error");
                    }
                }
            }catch(InvalidCastException)
            {
                MessageBox.Show("All fields must be a numeric value.");
            }
            catch (Exception ex)
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
