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
            bool passwordCheckFail = true;
            bool blankFields = true;
            
            // Checks for matching password
            if (passwordTextbox.Password.Length < 5 && passwordTextbox.Password.ToString() != "")
            {
                passwordInfoText.Text = "(Must be 5+ characters long)";
                errorTextblock.Visibility = Visibility.Hidden;
                passwordCheckFail = true;
                passwordInfoText.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEF5555"));

                //MessageBox.Show("Password must be at least 5 characters long.");
            }
            else if(confirmPasswordTextbox.Password.ToString() == passwordTextbox.Password.ToString() && passwordTextbox.Password.Length >= 5)
            {
                passwordInfoText.Text = "Password Looks Good!";
                passwordCheckFail = false;
                passwordInfoText.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3CDA26"));
            }
            else
            {
                passwordInfoText.Text = "(Must be 5+ characters long)";
                passwordInfoText.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEF5555"));
                passwordCheckFail = false;
            }

            if(emailTextbox.Text != "" && usernameTextBox.Text != "" && fullnameTextBox.Text != "" && dobTextbox.Text != "" && passwordTextbox.Password.ToString() != "" && confirmPasswordTextbox.Password.ToString() != "")
            {
                blankFields = false;
            }
            else
            {
                errorTextblock.Visibility = Visibility.Hidden;

                formInfoError.Text = "Missing fields. Please fill out all text fields to register.";
                formInfoError.Visibility = Visibility.Visible;
                
                //MessageBox.Show("Missing fields. Please fill out all text fields to register.","Missing Fields",MessageBoxButton.OK,MessageBoxImage.Warning);
            }

            SqlConnection sqlCon = new SqlConnection(@"Data Source=fitlevelrpg1.database.windows.net;Initial Catalog=FitLevelRPG;User ID=rpglogin;Password=HiQ!w2g6SFS;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
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


                    if (count == 1)
                    {
                        MessageBox.Show("Username or Email already exists");
                    }
                    if (count == 0 && confirmPasswordTextbox.Password.ToString() == passwordTextbox.Password.ToString() && passwordCheckFail == false && blankFields == false)
                    {
                        formInfoError.Visibility = Visibility.Hidden;
                        errorTextblock.Visibility = Visibility.Hidden;
                        String createAccount = "INSERT INTO Accounts (username, email, name, dateofbirth, password, xp) VALUES (@username, @email, @fullname, @dob, @password, @xp)";
                        SqlCommand cmd2 = new SqlCommand(createAccount, sqlCon);
                        cmd2.CommandType = System.Data.CommandType.Text;
                        cmd2.Parameters.AddWithValue("@email", emailTextbox.Text);
                        cmd2.Parameters.AddWithValue("@username", usernameTextBox.Text);
                        cmd2.Parameters.AddWithValue("@fullname", fullnameTextBox.Text);
                        cmd2.Parameters.AddWithValue("@dob", dobTextbox.Text);
                        cmd2.Parameters.AddWithValue("@password", passwordTextbox.Password.ToString());
                        cmd2.Parameters.AddWithValue("@xp", 0);
                        cmd2.ExecuteScalar();
                        MessageBox.Show("Account created!");
                        NavigationService.Navigate(new Uri("Login.xaml", UriKind.Relative));

                    }
                    else if (passwordTextbox.Password.ToString() == "")
                    {
                        errorTextblock.Text = "Password cannot be blank or null.";
                        errorTextblock.Visibility = Visibility.Visible;
                    }
                    else if(count == 0 && confirmPasswordTextbox.Password.ToString() != passwordTextbox.Password.ToString() && passwordCheckFail==true)
                    {
                        errorTextblock.Text = "Passwords do not match.";
                        errorTextblock.Visibility = Visibility.Visible;
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
