using AdoGemeenschap;
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
using System.Windows.Shapes;

namespace adowpf
{
    /// <summary>
    /// Interaction logic for SaldoRekeningRaadplegen.xaml
    /// </summary>
    public partial class SaldoRekeningRaadplegen : Window
    {
        public SaldoRekeningRaadplegen()
        {
            InitializeComponent();
        }

        private void ButtonSaldoRekeningRaadplegen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var mijnrekening = new RekeningenManager();

                    decimal saldo = mijnrekening.RekeningSaldo(TextBoxRekeningNr.Text);

                LabelSaldoWeergeven.Content = "Saldo : " + saldo;
            }

            catch (Exception ex)
            {
                LabelMeldingen.Content = "Button Saldo : " + ex.Message;
            }
            
        }
    }
}
