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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BtnClickLRegister(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Register.xaml", UriKind.Relative));
        }

        private void BtnClickLLogin(object sender, RoutedEventArgs e)
        {
            // SQL Connection
            SqlConnection sqlCon = new SqlConnection(@"Data Source=fitlevelrpg1.database.windows.net;Initial Catalog=FitLevelRPG;User ID=rpglogin;Password=HiQ!w2g6SFS;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    // Login check
                    sqlCon.Open();
                    String query = "SELECT COUNT(1) FROM [User] WHERE username=@username AND password=@password";
                    SqlCommand cmd = new SqlCommand(query, sqlCon);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@username", TextboxUsername.Text);
                    cmd.Parameters.AddWithValue("@password", TextboxPassword.Password.ToString());
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if(count == 1)
                    {
                        MessageBox.Show("Logged in successfully!\nClick OK to continue.","Login Success",MessageBoxButton.OK,MessageBoxImage.Information);
                        LoggedInView.LoggedInUser = TextboxUsername.Text;
                        LoggedInView dashboard = new LoggedInView();
                        var parentWindow = Window.GetWindow(this);
                        dashboard.Show();
                        parentWindow.Close();
                        
                    }
                    else
                    {
                        MessageBox.Show("Invalid username and/or password.","Login Error",MessageBoxButton.OK,MessageBoxImage.Warning);
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
            
        }
        
        

        private void ForgotPasswordClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("./PasswordReset/ResetPassword.xaml", UriKind.Relative));
        }
    }

}
