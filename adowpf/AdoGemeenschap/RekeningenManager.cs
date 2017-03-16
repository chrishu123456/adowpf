using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdoGemeenschap
{
    public class RekeningenManager
    {
        public Int32 SaldoBonus()
        {
            try
            {
                var BankDb = new BankDbManager();

                using (var BankDbCon = BankDb.GetConnection())
                {

                    using (var BankDbcommand = BankDbCon.CreateCommand())
                    {
                        BankDbcommand.CommandType = System.Data.CommandType.Text;
                        BankDbcommand.CommandText = "update Rekeningen set Saldo=Saldo*1.1 ";
                        BankDbCon.Open();
                        return BankDbcommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fout : " + ex.Message, "Fout", MessageBoxButton.OK);
                return 0;
            }
        }

        public Boolean Storten(decimal TeStortenBedrag, string RekeningNrWaaropWordtGestort)
        {

                var BankDb = new BankDbManager();

                using (var BankDbCon = BankDb.GetConnection())
                {

                    using (var BankDbCommand = BankDbCon.CreateCommand())
                    {
                    BankDbCommand.CommandType = System.Data.CommandType.Text;
                    BankDbCommand.CommandText = "update Rekeningen set Saldo=Saldo+@teStorten where RekeningNr=@RekeningNr";

                    var BankDbTeStortenParameter = BankDbCommand.CreateParameter();
                    BankDbTeStortenParameter.ParameterName = "@teStorten";
                    BankDbTeStortenParameter.Value = TeStortenBedrag;
                    BankDbCommand.Parameters.Add(BankDbTeStortenParameter);


                    var BankDbRekeningNrParameter = BankDbCommand.CreateParameter();
                    BankDbRekeningNrParameter.ParameterName = "@RekeningNr";
                    BankDbRekeningNrParameter.Value = RekeningNrWaaropWordtGestort;
                    BankDbCommand.Parameters.Add(BankDbRekeningNrParameter);

                    BankDbCon.Open();
                    return BankDbCommand.ExecuteNonQuery() != 0;
                }
                }

            
        }
    }
}
