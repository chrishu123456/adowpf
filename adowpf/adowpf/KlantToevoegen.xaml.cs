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
    /// Interaction logic for KlantToevoegen.xaml
    /// </summary>
    public partial class KlantToevoegen : Window
    {
        public KlantToevoegen()
        {
            InitializeComponent();
        }

        private void ButtonToevoegen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                KlantenManager klant = new KlantenManager();

                if (TextBoxnaam.Text != null)
                {
                    Int64 KlantNr = klant.NieuweKlant(TextBoxnaam.Text);
                    LabelKlantNr.Content = "Klantnr : " + KlantNr;
                }
                else
                {
                    throw new Exception("Geen naam ingevuld.");
                }
            }
            catch (Exception ex)
            {
                LabelMeldingen.Content = "Button Toevoegen : " + ex.Message;
            }
           
            
        }
    }
}
