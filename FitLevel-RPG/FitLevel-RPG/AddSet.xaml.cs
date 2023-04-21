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
    public partial class AddSet : Window
    {
        public AddSet()
        {
            InitializeComponent();
            exerciseIdTextbox.Text = TrackWorkout.exerciseID.ToString();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                    
                    if (repsTextbox.Text != "" && weightTextbox.Text != "")
                    {
                        
                        String addPlan = "INSERT INTO [SET](exercise_id, repetitions, [weight]) VALUES (@exercise_id, @reps, @weight)";
                        SqlCommand cmd2 = new SqlCommand(addPlan, sqlCon);
                        cmd2.CommandType = System.Data.CommandType.Text;
                        cmd2.Parameters.AddWithValue("@reps", repsTextbox.Text);
                        cmd2.Parameters.AddWithValue("@weight", weightTextbox.Text);
                        cmd2.Parameters.AddWithValue("@exercise_id", TrackWorkout.exerciseID);
                        cmd2.ExecuteScalar();
                        MessageBox.Show("Set Added! Click OK to close this window.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Make sure the values are filled out.", "Error");
                    }
                }
            }catch(InvalidCastException)
            {
                MessageBox.Show("Invalid input type.");
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
