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
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Page
    {
        public static string? Email { get; set; }

        public ChangePassword()
        {
            InitializeComponent();
            TextblockInfo.Text = "Please enter your new password.";
            textBlockRules.Text = "- Password cannot be blank\n- Password must contain at least 8 characters";
        }

        private void ChangePasswordButtonClick(object sender, RoutedEventArgs e)
        {

            SqlConnection sqlCon = new SqlConnection(@"Data Source=fitlevelrpg1.database.windows.net;Initial Catalog=FitLevelRPG;User ID=rpglogin;Password=HiQ!w2g6SFS;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    // Login check
                    sqlCon.Open();
                    String query = "SELECT COUNT(1) FROM [User] WHERE email=@email";
                    SqlCommand cmd = new SqlCommand(query, sqlCon);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@email", Email);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if(count == 1 && (TextboxConfirmPassword.Password.ToString() == "" || TextboxConfirmPassword.Password.ToString() == null))
                    {
                       // MessageBox.Show("Password cannot be null or empty.");
                        TextblockError.Visibility = Visibility.Visible;
                        TextblockError.Text = "Password cannot be null or empty.";
                    }
                    else if(count == 1 && TextboxConfirmPassword.Password.ToString().Length < 8 && TextboxPassword.Password.ToString().Length < 8)
                    {
                       // MessageBox.Show("Password does not meet requirements.\nPassword MUST contain at least 8 characters.", "Invalid Character Count");
                        TextblockError.Visibility = Visibility.Visible;
                        TextblockError.Text = "Password MUST contain at least 8 characters.";
                    }
                    else
                    {
                        if (count == 1 && TextboxConfirmPassword.Password.ToString() == TextboxPassword.Password.ToString())
                        {

                            String passwordUpdate = "UPDATE [User] SET password=@password, verifycode=NULL WHERE email=@email";

                            SqlCommand cmd2 = new SqlCommand(passwordUpdate, sqlCon);
                            cmd2.CommandType = System.Data.CommandType.Text;
                            cmd2.Parameters.AddWithValue("@password", TextboxConfirmPassword.Password.ToString());
                            cmd2.Parameters.AddWithValue("@email", Email);

                            cmd2.ExecuteScalar();
                            TextblockError.Visibility = Visibility.Hidden;

                            MessageBox.Show("Password has been updated!");
                            NavigationService.Navigate(new Uri("Login.xaml", UriKind.Relative));

                        }
                        else
                        {
                            //MessageBox.Show("Passwords do not match. No changes made.");
                            TextblockError.Visibility = Visibility.Visible;
                            TextblockError.Text = "Passwords do not match. No changes made.";
                        }
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
    }
}
