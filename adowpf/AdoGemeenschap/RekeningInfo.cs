using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoGemeenschap
{
    public class RekeningInfo
    {
        private decimal saldoValue;

        public decimal Saldo
        {
            get { return saldoValue; }
            set { saldoValue = value; }
        }

        private string naamValue;

        public string Naam
        {
            get { return naamValue; }
            set { naamValue = value; }
        }


        public RekeningInfo(decimal saldo, string naam)
        {
            this.Saldo = saldo;
            this.Naam = naam;
           
        }
    }
}
