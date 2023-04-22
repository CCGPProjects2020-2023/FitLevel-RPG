using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace FitLevel_RPG
{
    /// <summary>
    /// Interaction logic for WorkoutHistory.xaml
    /// </summary>
    public partial class WorkoutHistory : Page
    {
        DataTable dt = new DataTable("WorkoutPlan");
        public WorkoutHistory()
        {
            InitializeComponent();
            FillData();
        }

        private void BtnClickPlanNextWorkout(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("PlanNextWorkout.xaml", UriKind.Relative));
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
                CmdString = "SELECT Exercise.name, Workout.start_time FROM Exercise, Workout WHERE Exercise.workout_id = Workout.workout_id AND Workout.user_id = @user_id";

                SqlCommand cmd = new SqlCommand(CmdString, sqlCon);
                cmd.Parameters.AddWithValue("@user_id", LoggedInView.LoggedInUserID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                dataGrid.ItemsSource = dt.DefaultView;

            }
        }
    }
}
