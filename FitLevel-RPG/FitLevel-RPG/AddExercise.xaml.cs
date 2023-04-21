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
    /// Interaction logic for AddExercise.xaml
    /// </summary>
    public partial class AddExercise : Window
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=fitlevelrpg1.database.windows.net;Initial Catalog=FitLevelRPG;User ID=rpglogin;Password=HiQ!w2g6SFS;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        
        public AddExercise()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    // Login check
                    sqlCon.Open();

                    if (typeTextbox.Text != "" && nameTextbox.Text != "" && descTextbox.Text != "")
                    {

                        String addExercise = "INSERT INTO Exercise(workout_id, [type], [name], [description]) VALUES(@workout_id, @type, @name, @description); SELECT SCOPE_IDENTITY();";
                        SqlCommand cmd = new SqlCommand(addExercise, sqlCon);
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@workout_id", TrackWorkout.workoutID);
                        cmd.Parameters.AddWithValue("@type", typeTextbox.Text);
                        cmd.Parameters.AddWithValue("@name", nameTextbox.Text);
                        cmd.Parameters.AddWithValue("@description", descTextbox.Text);
                        TrackWorkout.exerciseID = Convert.ToInt32(cmd.ExecuteScalar());
                        MessageBox.Show("Exercise Added! Click OK to close this window.");
                        
                        PlanNextWorkout pnw = new PlanNextWorkout();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Make sure the values are filled out.", "Error");
                    }
                }
            } catch (InvalidCastException)
            {
                MessageBox.Show("Fields are not the correct data type.");
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                sqlCon.Close();
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
