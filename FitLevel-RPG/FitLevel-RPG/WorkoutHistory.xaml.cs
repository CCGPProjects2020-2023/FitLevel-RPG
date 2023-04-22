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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitLevel_RPG
{
    /// <summary>
    /// Interaction logic for WorkoutHistory.xaml
    /// </summary>
    public partial class WorkoutHistory : Page
    {
        public WorkoutHistory()
        {
            InitializeComponent();

            //SqlConnection sqlCon = new(@"Data Source=fitlevelrpg1.database.windows.net;Initial Catalog=FitLevelRPG;User ID=rpglogin;Password=HiQ!w2g6SFS;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //try
            //{
            //    if (sqlCon.State == System.Data.ConnectionState.Closed)
            //    {
            //        sqlCon.Open();
            //        String getUser = "SELECT * FROM [User] WHERE user_id = @user_id";
            //        SqlCommand cmd = new(getUser, sqlCon)
            //        {
            //            CommandType = System.Data.CommandType.Text
            //        };
            //        cmd.Parameters.AddWithValue("@user_id", LoggedInView.LoggedInUserID);

            //        using SqlDataReader reader = cmd.ExecuteReader();
            //        if (reader.Read())
            //        {
            //            usernameTextBox.Text = reader["username"].ToString();
            //            emailTextbox.Text = reader["email"].ToString();
            //            fullnameTextBox.Text = reader["full_name"].ToString();
            //            dobTextbox.Text = reader["dob"].ToString();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    sqlCon.Close();
            //}
        }

        private void BtnClickPlanNextWorkout(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("PlanNextWorkout.xaml", UriKind.Relative));
        }
    }
}
