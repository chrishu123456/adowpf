using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoGemeenschap
{
    public class Brouwer
    {
        private Int32 brouwernrValue;

        public Int32 BrouwerNr
        {
            get { return brouwernrValue; }
        }

        private string brnaamValue;

        public string BrNaam
        {
            get { return brnaamValue; }
            set { brnaamValue = value; }
        }

        private string adresValue;

        public string Adres
        {
            get { return adresValue; }
            set { adresValue = value; }
        }

        private Int16 postcodeValue;

        public Int16 PostCode
        {
            get { return postcodeValue; }
            set { postcodeValue = value; }
        }

        private string gemeenteValue;

        public string Gemeente
        {
            get { return gemeenteValue; }
            set { gemeenteValue = value; }
        }

        private Int32? omzetValue;

        public Int32? Omzet
        {
            get { return omzetValue; }
            set { omzetValue = value; }
        }

        public Brouwer():this(0, null, null, 0, null, 0)
        {

        }

        public Brouwer(Int32 brouwernr, string brnaam, string adres, Int16 postcode, string gemeente, Int32? omzet)
        {
            this.brouwernrValue = brouwernr;
            this.BrNaam = brnaam;
            this.Adres = adres;
            this.PostCode = postcode;
            this.Gemeente = gemeente;
            this.Omzet = omzet;
        }
    }
}
