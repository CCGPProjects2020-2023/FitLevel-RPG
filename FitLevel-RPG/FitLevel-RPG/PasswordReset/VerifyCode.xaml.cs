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
            SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FitLevelDB;Integrated Security=True");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    // Login check
                    sqlCon.Open();
                    String query = "SELECT COUNT(1) FROM Accounts WHERE email=@email AND verifycode=@code";
                    
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
                        MessageBox.Show("Invalid Code and/or Email.");
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
