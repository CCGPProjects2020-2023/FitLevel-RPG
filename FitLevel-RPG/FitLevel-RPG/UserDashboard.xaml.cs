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
    /// Interaction logic for UserDashboard.xaml
    /// </summary>
    public partial class UserDashboard : Page
    {
        float currentXP = 80f;
        float nextLevelXP = 100f;
        int currentLevel = 1;
        public UserDashboard()
        {
            InitializeComponent();
            currentUserLevel.Content = "Current Lvl: " + currentLevel;
            currentXpLabel.Content = "Current XP: " + currentXP;
            requiredXpLabel.Content = "Next Level at: " + nextLevelXP + " XP";
            xpPercentLabel.Content = "Level Progress - " + (currentXP / nextLevelXP * 100) + "%";
            xpBar.Value = currentXP;
        }
    }
}
