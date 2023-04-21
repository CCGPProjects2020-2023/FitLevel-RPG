using NPOI.POIFS.Properties;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace FitLevel_RPG
{
    /// <summary>
    /// Interaction logic for ModifyUserInfo.xaml
    /// </summary>
    public partial class ModifyUserInfo : Page
    {
        int levelId = 0;
        
        public ModifyUserInfo()
        {
            InitializeComponent();
            SqlConnection sqlCon = new SqlConnection(@"Data Source=fitlevelrpg1.database.windows.net;Initial Catalog=FitLevelRPG;User ID=rpglogin;Password=HiQ!w2g6SFS;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                    String getUser = "SELECT * FROM [User] WHERE user_id = @user_id";
                    SqlCommand cmd = new SqlCommand(getUser, sqlCon);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@user_id", LoggedInView.LoggedInUserID);

                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        usernameTextBox.Text = reader["username"].ToString();
                        emailTextbox.Text = reader["email"].ToString();
                        fullnameTextBox.Text = reader["full_name"].ToString();
                        dobTextbox.Text = reader["dob"].ToString();
                        passwordTextbox.Password = reader["password"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
        private void ModifyButtonClick(object sender, RoutedEventArgs e)
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
            else if (confirmPasswordTextbox.Password.ToString() == passwordTextbox.Password.ToString() && passwordTextbox.Password.Length >= 5)
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

            if (emailTextbox.Text != "" && usernameTextBox.Text != "" && fullnameTextBox.Text != "" && dobTextbox.Text != "" && passwordTextbox.Password.ToString() != "" && confirmPasswordTextbox.Password.ToString() != "")
            {
                blankFields = false;
            }
            else
            {
                errorTextblock.Visibility = Visibility.Hidden;

                formInfoError.Text = "Missing fields. Please fill out all text fields to modify.";
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

                    if (confirmPasswordTextbox.Password.ToString() == passwordTextbox.Password.ToString() && !passwordCheckFail && !blankFields)
                    {
                        formInfoError.Visibility = Visibility.Hidden;
                        errorTextblock.Visibility = Visibility.Hidden;
                        String updateUser = "UPDATE [User] SET username = @username, email = @email, full_name = @full_name, dob = @dob, password = @password, level_id = @level_id";
                        SqlCommand cmd2 = new SqlCommand(updateUser, sqlCon);
                        cmd2.CommandType = System.Data.CommandType.Text;
                        cmd2.Parameters.AddWithValue("@email", emailTextbox.Text);
                        cmd2.Parameters.AddWithValue("@username", usernameTextBox.Text);
                        cmd2.Parameters.AddWithValue("@full_name", fullnameTextBox.Text);
                        cmd2.Parameters.AddWithValue("@dob", dobTextbox.Text);
                        cmd2.Parameters.AddWithValue("@password", passwordTextbox.Password.ToString());
                        cmd2.Parameters.AddWithValue("@level_id", levelId);
                        cmd2.ExecuteScalar();
                        MessageBox.Show("User Updated!");
                        
                        NavigationService.Navigate(new Uri("ModifyUserInfo.xaml", UriKind.Relative));
                    }
                    else if (passwordTextbox.Password.ToString() == "")
                    {
                        errorTextblock.Text = "Password cannot be blank or null.";
                        errorTextblock.Visibility = Visibility.Visible;
                    }
                    else if (confirmPasswordTextbox.Password.ToString() != passwordTextbox.Password.ToString() && passwordCheckFail)
                    {
                        errorTextblock.Text = "Passwords do not match.";
                        errorTextblock.Visibility = Visibility.Visible;
                    }
                }
            }
            catch (Exception ex)
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
            
            NavigationService.Navigate(new Uri("UserDashboard.xaml", UriKind.Relative));
        }
    }
}
