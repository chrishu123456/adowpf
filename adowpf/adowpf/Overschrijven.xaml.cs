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
    /// Interaction logic for Overschrijven.xaml
    /// </summary>
    public partial class Overschrijven : Window
    {
        public Overschrijven()
        {
            InitializeComponent();
        }

        private void ButtonOverschrijven_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RekeningenManager rekening = new RekeningenManager();

                decimal bedrag;

                if (decimal.TryParse(TextBoxBedrag.Text, out bedrag))
                {
                    if (rekening.Overschrijven(bedrag, TextBoxvanRekeningNr.Text, TextBoxnaarRekeningNr.Text))
                    {
                        LabelMeldingen.Content = "Bedrag is gestort.";
                    }
                }
                else
                { LabelMeldingen.Content = "Geen decimaal bedrag"; }
            }
            catch (Exception ex)
            {
                LabelMeldingen.Content = "Overschrijven Button : " + ex.Message;
            }
          


            
        }
    }
}
