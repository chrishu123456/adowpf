using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoGemeenschap
{
    public class Bank2DbManager
    {
        private static ConnectionStringSettings conBankString = ConfigurationManager.ConnectionStrings["Bank2"]; 
                                                                                                                

        private static DbProviderFactory factory = DbProviderFactories.GetFactory(conBankString.ProviderName); 

        public DbConnection GetConnection()
        {
            DbConnection ConBank = factory.CreateConnection(); 
            ConBank.ConnectionString = conBankString.ConnectionString; 
            return ConBank;
        }
    }
}
