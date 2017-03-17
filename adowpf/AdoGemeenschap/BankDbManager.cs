using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace AdoGemeenschap
{
    public class BankDbManager
    {
        private static ConnectionStringSettings conBankString = ConfigurationManager.ConnectionStrings["Bank"]; // conBankString krijg alle attributen van de connectionstring die 
                                                                                                                // als Name attribuut gelijk is aan Bank

        private static DbProviderFactory factory = DbProviderFactories.GetFactory(conBankString.ProviderName); // factory wordt van het type SqlClientFactory als conBankString.ProviderName
                                                                                                               // gelijk is aan System.Data.SqlClient

        public DbConnection GetConnection()
        {
            DbConnection ConBank = factory.CreateConnection();  // SqlConnection wordt gecreerd met een factory van het type SqlClientFactory
            ConBank.ConnectionString = conBankString.ConnectionString; // ConnectionString van ConBank wordt ingevuld vanuit de conBankString
            return ConBank;
        }
    }
}
