﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoGemeenschap
{
    public class Brouwers
    {
        private int brouwernrValue;

        public int BrouwerNr
        {
            get { return brouwernrValue; }
            set { brouwernrValue = value; }
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

        private int postcodeValue;

        public int PostCode
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

        private decimal omzetValue;

        public decimal Omzet
        {
            get { return omzetValue; }
            set { omzetValue = value; }
        }

        public Brouwers():this(0, null, null, 0, null, 0)
        {

        }

        public Brouwers(int brouwernr, string brnaam, string adres, int postcode, string gemeente, decimal omzet)
        {
            this.BrouwerNr = brouwernr;
            this.BrNaam = brnaam;
            this.Adres = adres;
            this.PostCode = postcode;
            this.Gemeente = gemeente;
            this.Omzet = omzet;
        }
    }
}
