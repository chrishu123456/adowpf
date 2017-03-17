using System;
using System.Collections.Generic;
using System.Data.Common;
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
                    BankDbCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    BankDbCommand.CommandText = "Storten";

                    var BankDbTeStortenParameter = BankDbCommand.CreateParameter();
                    BankDbTeStortenParameter.ParameterName = "@teStorten";
                    BankDbTeStortenParameter.Value = TeStortenBedrag;
                    BankDbTeStortenParameter.DbType = System.Data.DbType.Currency;
                    BankDbCommand.Parameters.Add(BankDbTeStortenParameter);


                    var BankDbRekeningNrParameter = BankDbCommand.CreateParameter();
                    BankDbRekeningNrParameter.ParameterName = "@RekeningNr";
                    BankDbRekeningNrParameter.Value = RekeningNrWaaropWordtGestort;
                    BankDbRekeningNrParameter.DbType = System.Data.DbType.String;
                    BankDbCommand.Parameters.Add(BankDbRekeningNrParameter);

                    BankDbCon.Open();
                    return BankDbCommand.ExecuteNonQuery() != 0;
                }
                }

            
        }

        public Boolean Overschrijven(decimal bedrag, string vanRekening, string naarRekening)
        {
            var BankDb = new BankDbManager();

            using (var BankDbConnection = BankDb.GetConnection())
            {
                BankDbConnection.Open();

                using (var BankDbTransaction = BankDbConnection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    using (var BankDbAftrekkenCommand = BankDbConnection.CreateCommand())
                    {
                        BankDbAftrekkenCommand.Transaction = BankDbTransaction;

                        BankDbAftrekkenCommand.CommandType = System.Data.CommandType.Text;
                        BankDbAftrekkenCommand.CommandText = "update Rekeningen set Saldo=Saldo - @Bedrag where RekeningNr=@vanRekeningNr";

                        DbParameter ParAftrekkenBedrag = BankDbAftrekkenCommand.CreateParameter();
                        ParAftrekkenBedrag.ParameterName = "@Bedrag";
                        ParAftrekkenBedrag.Value = bedrag;
                        ParAftrekkenBedrag.DbType = System.Data.DbType.Decimal;
                        BankDbAftrekkenCommand.Parameters.Add(ParAftrekkenBedrag);

                        DbParameter ParAftrekkenvanRekeningNr = BankDbAftrekkenCommand.CreateParameter();
                        ParAftrekkenvanRekeningNr.ParameterName = "@vanRekeningNr";
                        ParAftrekkenvanRekeningNr.Value = vanRekening;
                        ParAftrekkenvanRekeningNr.DbType = System.Data.DbType.String;
                        BankDbAftrekkenCommand.Parameters.Add(ParAftrekkenvanRekeningNr);

                        
                        if (BankDbAftrekkenCommand.ExecuteNonQuery() == 0)
                        {
                            //iets misgegaan
                            BankDbTransaction.Rollback();
                            throw new Exception("Iets misgegaan bij het van de rekening afhalen van het geld.");
                        }
                    }

                    using (var BankDbStortenCommand = BankDbConnection.CreateCommand())
                    {
                        BankDbStortenCommand.Transaction = BankDbTransaction;

                        BankDbStortenCommand.CommandType = System.Data.CommandType.Text;
                        BankDbStortenCommand.CommandText = "update Rekeningen set Saldo=Saldo+@Bedrag where RekeningNr=@naarRekeningNr";

                        DbParameter ParStortenBedrag = BankDbStortenCommand.CreateParameter();
                        ParStortenBedrag.ParameterName = "@Bedrag";
                        ParStortenBedrag.Value = bedrag;
                        ParStortenBedrag.DbType = System.Data.DbType.Decimal;
                        BankDbStortenCommand.Parameters.Add(ParStortenBedrag);

                        DbParameter ParStortennaarRekeningNr = BankDbStortenCommand.CreateParameter();
                        ParStortennaarRekeningNr.ParameterName = "@naarRekeningNr";
                        ParStortennaarRekeningNr.Value = naarRekening;
                        ParStortennaarRekeningNr.DbType = System.Data.DbType.String;
                        BankDbStortenCommand.Parameters.Add(ParStortennaarRekeningNr);

                        if (BankDbStortenCommand.ExecuteNonQuery() == 0)
                        {
                            BankDbTransaction.Rollback();
                            throw new Exception("Iets misgegaan bij het naar de rekening schrijven van het geld.");
                        }
                    }

                    

                    BankDbTransaction.Commit();
                    return true;
                }
            }
        }
    }
}
