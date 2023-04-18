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
    /// Interaction logic for VerifyCode.xaml
    /// </summary>
    public partial class VerifyCode : Page
    {
        public VerifyCode()
        {
            InitializeComponent();
            TextblockResetInfo.Text = "Please enter a valid account email below and valid verification code.\n Then click Verify Code button";
        }

        private void VerifyCodeButtonClick(object sender, RoutedEventArgs e)
        {
            // DB CONNECTION
            SqlConnection sqlCon = new SqlConnection(@"Data Source=fitlevelrpg1.database.windows.net;Initial Catalog=FitLevelRPG;User ID=rpglogin;Password=HiQ!w2g6SFS;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    // Login check
                    sqlCon.Open();
                    String query = "SELECT COUNT(1) FROM Accounts WHERE email=@email AND verifycode=@code";
                    String resetCode = "UPDATE Accounts SET verifycode=NULL WHERE email=@email";

                    SqlCommand cmd = new SqlCommand(query, sqlCon);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@email", TextboxEmail.Text);
                    cmd.Parameters.AddWithValue("@code", TextboxvCode.Text);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 1 && TextboxvCode.Text != null && TextboxvCode.Text != "" && TextboxEmail.Text != "")
                    {
                        MessageBox.Show("Code and Email validated!");
                        ChangePassword.Email = TextboxEmail.Text;
                        NavigationService.Navigate(new Uri("./PasswordReset/ChangePassword.xaml", UriKind.Relative));                        
                    }
                    else
                    {
                        SqlCommand cmd2 = new SqlCommand(resetCode, sqlCon);
                        cmd2.Parameters.AddWithValue("@email", TextboxEmail.Text);
                        cmd2.ExecuteScalar();
                        MessageBox.Show("Invalid Code and/or Email.\nFor security, code has been reset if the account exists.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        NavigationService.Navigate(new Uri("./Login.xaml", UriKind.Relative));
                    }
                }
            } catch (Exception ex)
            {
                String resetCode = "UPDATE Accounts SET verifycode=NULL WHERE email=@email";
                SqlCommand cmd2 = new SqlCommand(resetCode, sqlCon);
                cmd2.Parameters.AddWithValue("@email", TextboxEmail.Text);
                cmd2.ExecuteScalar();
                MessageBox.Show("Invalid Code and/or Email.\nFor security, code has been reset if the account exists.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                NavigationService.Navigate(new Uri("./Login.xaml", UriKind.Relative));
            }
            finally
            {
                String resetCode = "UPDATE Accounts SET verifycode=NULL WHERE email=@email";
                SqlCommand cmd2 = new SqlCommand(resetCode, sqlCon);
                cmd2.Parameters.AddWithValue("@email", TextboxEmail.Text);
                cmd2.ExecuteScalar();
                sqlCon.Close();
            }


        }
    }

}
