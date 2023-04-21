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
using System.Diagnostics;
using System.Windows.Threading;

namespace FitLevel_RPG
{
    /// <summary>
    /// Interaction logic for TrackWorkout.xaml
    /// </summary>
    /// 
    public partial class TrackWorkout : Page
    {
        DispatcherTimer t1 = new DispatcherTimer();
        Stopwatch sw;
        List<string> list = new List<string>();
        public TrackWorkout()
        {
            InitializeComponent();
            sw = new Stopwatch();
            t1 = new DispatcherTimer();
            //t1.Interval = new TimeSpan(0, 0, 1);

            t1.Tick += T1_Tick;
            t1.Start();
        }

        private void BeginButton_Click(object sender, RoutedEventArgs e)
        {
            sw.Start();
            StartTimerButton.IsEnabled = false;

        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            sw.Stop();

        }

        private void T1_Tick(object sender, EventArgs e)
        {

            TimeSpan ts = sw.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            timerTextBlock.Text = "Elapsed Time: " + elapsedTime;
        }
    }
}
