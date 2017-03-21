using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoGemeenschap
{
    public class KlantenManager
    {
        public Int64 NieuweKlant (String naam)
        {
            var BankDb = new BankDbManager();

            using (var BankDbConnection = BankDb.GetConnection())
            {
                using (var BankDbNieuweKlantCommand = BankDbConnection.CreateCommand())
                {
                    BankDbNieuweKlantCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    BankDbNieuweKlantCommand.CommandText = "NieuweKlant";

                    DbParameter ParNieuweKlantNaam = BankDbNieuweKlantCommand.CreateParameter();
                    ParNieuweKlantNaam.ParameterName = "@naam";
                    ParNieuweKlantNaam.Value = naam;
                    ParNieuweKlantNaam.DbType = System.Data.DbType.String;

                    BankDbNieuweKlantCommand.Parameters.Add(ParNieuweKlantNaam);

                    BankDbConnection.Open();
                    Int64 KlantNr = Convert.ToInt64(BankDbNieuweKlantCommand.ExecuteScalar());
                    return KlantNr;
                }
            }
        }
    }
}
