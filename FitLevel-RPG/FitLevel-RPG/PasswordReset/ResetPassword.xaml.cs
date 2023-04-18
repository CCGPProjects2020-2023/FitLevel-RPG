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
    /// Interaction logic for ResetPassword.xaml
    /// </summary>
    public partial class ResetPassword : Page
    {
        bool numValid = false;
        public ResetPassword()
        {
            InitializeComponent();
            TextblockResetInfo.Text = "Please enter a valid account email below\n and click the 'Get Code' button";
        }

        private void ResetButtonClick(object sender, RoutedEventArgs e)
        {
            // Generates a random verification code
            Random randomNum = new Random();
            var x = 0;
            string s = "";

            do
            {
                x = randomNum.Next(000000, 999999);
                s = x.ToString();
            } while (x.ToString().Length < 6);


            // DB CONNECTION
            SqlConnection sqlCon = new SqlConnection(@"Data Source=fitlevelrpg1.database.windows.net;Initial Catalog=FitLevelRPG;User ID=rpglogin;Password=HiQ!w2g6SFS;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    // Login check
                    sqlCon.Open();
                    String query = "SELECT COUNT(1) FROM Accounts WHERE email=@email";
                    SqlCommand cmd = new SqlCommand(query, sqlCon);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@email", TextboxEmail.Text);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (TextboxEmail.Text == "" || TextboxEmail.Text == null)
                    {                      
                        TextblockError.Text = "Email cannot be blank or null.";
                        TextblockError.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        if (count == 1)
                        {

                            TextblockError.Visibility = Visibility.Hidden;
                            
                            String vCodeUpdate = "UPDATE Accounts SET verifycode=@x WHERE email=@email";
                            SqlCommand cmd2 = new SqlCommand(vCodeUpdate, sqlCon);
                            cmd2.CommandType = System.Data.CommandType.Text;
                            cmd2.Parameters.AddWithValue("@x", x);
                            cmd2.Parameters.AddWithValue("@email", TextboxEmail.Text);
                            cmd2.ExecuteScalar();
                            MessageBox.Show("If the email exists in our system, a code will be emailed to it.");
                            MessageBox.Show("The code is: " + s, "Code Box for Testing Only");
                            NavigationService.Navigate(new Uri("./PasswordReset/VerifyCode.xaml", UriKind.Relative));

                        }
                        else
                        {
                            TextblockError.Visibility = Visibility.Hidden;
                            
                            MessageBox.Show("If the email exists in our system, a code will be emailed to it.");
                            MessageBox.Show("The code is: " + s, "Code Box for Testing Only");
                            NavigationService.Navigate(new Uri("./PasswordReset/VerifyCode.xaml", UriKind.Relative));
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
