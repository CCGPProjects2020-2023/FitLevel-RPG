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
        Hyperlink hl = new Hyperlink();
       List<ItemList> BeginnerList = new List<ItemList>()
            {
                new ItemList(){ Workout="Introduction to Workouts", Description="Easy & simple workouts to help you get started."}
            };
        List<ItemList> IntermediateList = new List<ItemList>()
            {
                new ItemList(){ Workout="Intermediate Starter Warmup", Description="Simple intermediate warmup."}
            };
        public PreMadeWorkouts()
        {
            InitializeComponent();
            
            difficultyComboBox.Items.Add("Beginner");
            difficultyComboBox.Items.Add("Intermediate");
            difficultyComboBox.Items.Add("Advanced");

            this.dataGrid.ItemsSource = BeginnerList;


        }

        public class ItemList
        {
            public string Workout { get; set; }
            public string Description { get; set; }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void difficultyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (difficultyComboBox.SelectedIndex == 0)
            {
                this.dataGrid.ItemsSource = BeginnerList;
            }
            else if(difficultyComboBox.SelectedIndex == 1)
            {
                this.dataGrid.ItemsSource = IntermediateList;
            }
        }
    }
}
