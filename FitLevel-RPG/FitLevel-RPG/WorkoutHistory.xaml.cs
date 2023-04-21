﻿using System;
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
    /// Interaction logic for WorkoutHistory.xaml
    /// </summary>
    public partial class WorkoutHistory : Page
    {
        public WorkoutHistory()
        {
            InitializeComponent();
        }

        private void BtnClickPlanNextWorkout(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("PlanNextWorkout.xaml", UriKind.Relative));
        }
    }
}