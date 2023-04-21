using System;
using System.Collections.Generic;
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
        
        List<ItemList> BeginnerList = new List<ItemList>()
            {
                new ItemList(){ Workout="Beginner Cardio", Description="Easy cardio to help get you started."}
            };
        List<ItemList> IntermediateList = new List<ItemList>()
            {
                new ItemList(){ Workout="Intermediate Starter Warmup", Description="Simple intermediate warmup."}
            };
        List<ItemList> AdvancedList = new List<ItemList>()
            {
                new ItemList(){ Workout="(Not Active Yet)Advanced Starter Warmup", Description="In depth advanced warmup."}
            };
        public PreMadeWorkouts()
        {
            InitializeComponent();
            
            difficultyComboBox.Items.Add("Beginner");
            difficultyComboBox.Items.Add("Intermediate");
            difficultyComboBox.Items.Add("Advanced");
            difficultyComboBox.SelectedIndex = 0;

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
