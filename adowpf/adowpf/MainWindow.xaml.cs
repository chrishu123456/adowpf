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
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using AdoGemeenschap;

namespace adowpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBieren_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var manager = new BierenDbManager();

                using (var conBieren = manager.GetConnection())
                {
                    conBieren.Open();
                    labelStatus.Content = "Bieren geopend";
                }
            }
            catch (Exception ex)
            {
                labelStatus.Content = ex.Message;
            }
        }

        private void ButtonBank_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var manager = new BankDbManager();

                using (var conBank = manager.GetConnection())
                {
                    conBank.Open();
                    BankStatus.Content = "Bank geopend";
                }
            }
            catch (Exception ex)
            {
                BankStatus.Content = ex.Message;
            }
        }
    }
}
