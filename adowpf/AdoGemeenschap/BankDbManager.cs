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
        private static ConnectionStringSettings conBankString = ConfigurationManager.ConnectionStrings["Bank"];

        private static DbProviderFactory factory = DbProviderFactories.GetFactory(conBankString.ProviderName);

        public DbConnection GetConnection()
        {
            DbConnection ConBank = factory.CreateConnection();
            ConBank.ConnectionString = conBankString.ConnectionString;
            return ConBank;
        }
    }
}
