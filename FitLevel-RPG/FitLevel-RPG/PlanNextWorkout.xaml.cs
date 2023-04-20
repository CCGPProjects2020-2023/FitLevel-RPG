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

                CmdString = "SELECT wset, sreps, sweight FROM WorkoutPlan WHERE username=@username";

                SqlCommand cmd = new SqlCommand(CmdString, sqlCon);
                cmd.Parameters.AddWithValue("@username", LoggedInView.LoggedInUser);
                

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable("WorkoutPlan");

                sda.Fill(dt);

                dataGrid.ItemsSource = dt.DefaultView;

            }
        }
    }
}
