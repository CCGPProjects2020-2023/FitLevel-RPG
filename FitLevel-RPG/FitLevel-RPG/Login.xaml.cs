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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BtnClickLRegister(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Register.xaml", UriKind.Relative));
        }

        private void BtnClickLLogin(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Fake Login Failed. No connections made.", "Placeholder Warning");
        }

        private void ForgotPasswordClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("ResetPassword.xaml", UriKind.Relative));
        }
    }

}
