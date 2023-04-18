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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {

        public Register()
        {
            InitializeComponent();
            
        }

        private void RegisterButtonClick(object sender, RoutedEventArgs e)
        {
            bool passwordCheckFail = false;
            SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FitLevelDB;Integrated Security=True");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    // Login check
                    sqlCon.Open();
                    String query = "SELECT COUNT(1) FROM Accounts WHERE email=@email OR username=@username";

                    SqlCommand cmd = new SqlCommand(query, sqlCon);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@email", emailTextbox.Text);
                    cmd.Parameters.AddWithValue("@username", usernameTextBox.Text);
                    cmd.Parameters.AddWithValue("@fullname", fullnameTextBox.Text);
                    cmd.Parameters.AddWithValue("@dob", dobTextbox.Text);
                    cmd.Parameters.AddWithValue("@password", passwordTextbox.Password.ToString());

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if(passwordTextbox.Password.Length < 5)
                    {
                        passwordCheckFail = true;
                        MessageBox.Show("Password must be at least 5 characters long.");
                    }
                    else
                    {
                        passwordCheckFail = false;
                    }
                    if (count == 0 && confirmPasswordTextbox.Password.ToString() == passwordTextbox.Password.ToString() && passwordCheckFail == false)
                    {
                        
                        String createAccount = "INSERT INTO Accounts (username, email, name, dateofbirth, password) VALUES (@username, @email, @fullname, @dob, @password)";
                        SqlCommand cmd2 = new SqlCommand(createAccount, sqlCon);
                        cmd2.CommandType = System.Data.CommandType.Text;
                        cmd2.Parameters.AddWithValue("@email", emailTextbox.Text);
                        cmd2.Parameters.AddWithValue("@username", usernameTextBox.Text);
                        cmd2.Parameters.AddWithValue("@fullname", fullnameTextBox.Text);
                        cmd2.Parameters.AddWithValue("@dob", dobTextbox.Text);
                        cmd2.Parameters.AddWithValue("@password", passwordTextbox.Password.ToString());
                        cmd2.ExecuteScalar();
                        MessageBox.Show("Account created!");
                        NavigationService.Navigate(new Uri("Login.xaml", UriKind.Relative));

                    }
                    else if(count == 0 && confirmPasswordTextbox.Password.ToString() != passwordTextbox.Password.ToString() && passwordCheckFail==false)
                    {
                        MessageBox.Show("Passwords do not match.");
                    }
                    else
                    {
                        MessageBox.Show("Username or Email already exists");
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

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {           
            NavigationService.Navigate(new Uri("Login.xaml", UriKind.Relative));
        }
    }
}
