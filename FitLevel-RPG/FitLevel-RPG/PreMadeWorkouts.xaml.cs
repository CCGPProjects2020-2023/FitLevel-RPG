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
    /// Interaction logic for PreMadeWorkouts.xaml
    /// </summary>
    public partial class PreMadeWorkouts : Page
    {

        List<ItemList> BeginnerList = new List<ItemList>();
        List<ItemList> IntermediateList = new List<ItemList>();
        List<ItemList> AdvancedList = new List<ItemList>();

        public PreMadeWorkouts()
        {
            InitializeComponent();
            
            difficultyComboBox.Items.Add("Beginner");
            difficultyComboBox.Items.Add("Intermediate");
            difficultyComboBox.Items.Add("Advanced");
            difficultyComboBox.SelectedIndex = 0;
            fillList();

            this.listView.ItemsSource = BeginnerList;

        }

        public class ItemList
        {
            public string Workout { get; set; }
            public string Description { get; set; }

            public override string ToString()
            {
                return Workout + " - " + Description;
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // Fills the pre-made workout list form DB
        private void fillList()
        {
            string CmdString = string.Empty;
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=fitlevelrpg1.database.windows.net;Initial Catalog=FitLevelRPG;User ID=rpglogin;Password=HiQ!w2g6SFS;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                CmdString = "SELECT premade_name, premade_description FROM PreMade WHERE difficulty='Beginner'";
                DataTable dt = new DataTable();
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand(CmdString, sqlCon);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ItemList beginneril = new ItemList();
                    beginneril.Workout = (string)dr["premade_name"];
                    beginneril.Description = (string)dr["premade_description"];
                    BeginnerList.Add(beginneril);
                }

                CmdString = "SELECT premade_name, premade_description FROM PreMade WHERE difficulty='Intermediate'";
                sqlCon.Close();

                sqlCon.Open();
                SqlCommand cmd2 = new SqlCommand(CmdString, sqlCon);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    ItemList interil = new ItemList();
                    interil.Workout = (string)dr2["premade_name"];
                    interil.Description = (string)dr2["premade_description"];
                    IntermediateList.Add(interil);
                }
                CmdString = "SELECT premade_name, premade_description FROM PreMade WHERE difficulty='Advanced'";
                sqlCon.Close();

                sqlCon.Open();
                SqlCommand cmd3 = new SqlCommand(CmdString, sqlCon);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                while (dr3.Read())
                {
                    ItemList advancedil = new ItemList();
                    advancedil.Workout = (string)dr3["premade_name"];
                    advancedil.Description = (string)dr3["premade_description"];
                    AdvancedList.Add(advancedil);
                }
                sqlCon.Close();
            }
        }

        private void OpenPreMadeWorkout(object sender, RoutedEventArgs e)
        {
            if(difficultyComboBox.Text == "Beginner")
            {
                if (listView.SelectedIndex == 0)
                {
                    NavigationService.Navigate(new Uri("./Pre-Made Workouts/BeginnerCardio.xaml", UriKind.Relative));
                }
            }
            if(difficultyComboBox.Text == "Intermediate")
            {
                if(listView.SelectedIndex == 0)
                {
                    NavigationService.Navigate(new Uri("./Pre-Made Workouts/IntermediateStarterWarmup.xaml", UriKind.Relative));
                }
            }
            
        }

        private void difficultyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (difficultyComboBox.SelectedIndex == 0)
            {
                this.listView.ItemsSource = BeginnerList;
            }
            else if(difficultyComboBox.SelectedIndex == 1)
            {
                this.listView.ItemsSource = IntermediateList;
            }
            else if(difficultyComboBox.SelectedIndex == 2)
            {
                this.listView.ItemsSource = AdvancedList;
            }
        }

        
    }
}
