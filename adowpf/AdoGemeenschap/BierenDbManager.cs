using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace AdoGemeenschap
{

    public class BierenDbManager
    {
        private static ConnectionStringSettings conBierenString = ConfigurationManager.ConnectionStrings["Bieren"] ;

        private static DbProviderFactory factory = DbProviderFactories.GetFactory(conBierenString.ProviderName);

        public DbConnection GetConnection()
        {

            var conBieren = factory.CreateConnection();

            conBieren.ConnectionString = conBierenString.ConnectionString;
            return conBieren;

        }
    }


}
