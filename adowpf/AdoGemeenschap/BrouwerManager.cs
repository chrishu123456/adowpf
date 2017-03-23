using AdoGemeenschap;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoGemeenschap
{
    public class BrouwerManager
    {

        public List<Brouwer> BrouwersMetBeginLetters(String beginletters)
        {
            List<Brouwer> brouwers = new List<Brouwer>();

            BierenDbManager BierDb = new BierenDbManager();

            using (var BierDbConnection = BierDb.GetConnection())
            {
                using (var BierDbCommand = BierDbConnection.CreateCommand())
                {

                    BierDbCommand.CommandType = System.Data.CommandType.Text;

                    if (beginletters != string.Empty )
                    {
                        BierDbCommand.CommandText = "select * from Brouwers where BrNaam like @beginletters";

                        DbParameter ParBierDbBeginletters = BierDbCommand.CreateParameter();
                        ParBierDbBeginletters.ParameterName = "@beginletters";
                        ParBierDbBeginletters.Value = beginletters + "%";
                        ParBierDbBeginletters.DbType = System.Data.DbType.String;

                        BierDbCommand.Parameters.Add(ParBierDbBeginletters);

                    }
                    else
                    {

                       BierDbCommand.CommandText = "select * from Brouwers"; //omdat er geen parameter in deze select staat , 
                                                                            // is er geen DbParameter nodig, en dus ook geen BierDbCommand.Parameters.Add van die parameter
                    }
                    

                   

                    BierDbConnection.Open();

                    using (var reader = BierDbCommand.ExecuteReader())
                    {
                        int brouwernrpos = reader.GetOrdinal("BrouwerNr");
                        int brnaampos = reader.GetOrdinal("BrNaam");
                        int adrespos = reader.GetOrdinal("Adres");
                        int postcodepos = reader.GetOrdinal("PostCode");
                        int gemeentepos = reader.GetOrdinal("Gemeente");
                        int omzetpos = reader.GetOrdinal("Omzet");

                        Int32? omzet;

                        while (reader.Read())
                        {
                            if (reader.IsDBNull(omzetpos))
                            {
                                omzet = null;
                            }
                            else
                            {
                                omzet = reader.GetInt32(omzetpos); // om het ongedefinieerde van omzet te verwijderen , wanneer omzet in deze lus geraakt.
                                
                            }
                            brouwers.Add(new Brouwer(reader.GetInt32(brouwernrpos), reader.GetString(brnaampos),
                                  reader.GetString(adrespos), reader.GetInt16(postcodepos), reader.GetString(gemeentepos), omzet));

                        }


                    }




                    

                }
            }

            return brouwers;
        }


    }
}
