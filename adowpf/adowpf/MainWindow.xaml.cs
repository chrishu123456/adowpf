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


        private void ButtonSaldoVerhogen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var rekeningen = new RekeningenManager();
                RekeningenVerhoogdSaldo.Content = "Aantal : " + rekeningen.SaldoBonus();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Fout" + ex.Message, "Fout", MessageBoxButton.OK);
            }
        }

        private void ButtonStorten_Click(object sender, RoutedEventArgs e)
        {
            decimal TeStorten;

            if (decimal.TryParse(TextBoxTeStorten.Text, out TeStorten))
            {
                try
                {
                    var manager = new RekeningenManager();
                    if (manager.Storten(TeStorten, TextBoxRekeningNr.Text))
                    {
                        labelStatus.Content = "OK";
                    }
                    else
                    {
                        labelStatus.Content = "Rekening niet gevonden.";
                    }
                }
                catch (Exception ex)
                {
                    labelStatus.Content = ex.Message;

                }
            }
            else
            {
                labelStatus.Content = "Geen getal ingetikt.";
            }
        }
    }
}
