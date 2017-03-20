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
    /// Interaction logic for RekeningInfoRaadplegen.xaml
    /// </summary>
    public partial class RekeningInfoRaadplegen : Window
    {
        public RekeningInfoRaadplegen()
        {
            InitializeComponent();
        }

        private void ButtonInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                RekeningenManager mijnrekeningopdebank = new RekeningenManager();

                string RekeningNr = TextBoxRekeningNr.Text;

                var derekening = mijnrekeningopdebank.RekeningInfoRaadplegen(RekeningNr);

                LabelSaldo.Content = "Saldo : " + derekening.Saldo;
                LabelNaam.Content = "Naam : " + derekening.Naam;
            }
            catch (Exception ex)
            {
                LabelMeldingen.Content = "Button info : " + ex.Message;
            }
            
        }
    }
}
