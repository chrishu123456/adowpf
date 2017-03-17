using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
            var Bank2Db = new Bank2DbManager();

            var opties = new TransactionOptions();

            opties.IsolationLevel = IsolationLevel.ReadCommitted;

            using (var traOverschrijven = new TransactionScope(TransactionScopeOption.Required, opties))
            {
                using (var BankDbConnection = BankDb.GetConnection())
                {
                    using (var BankDbAftrekkenCommand = BankDbConnection.CreateCommand())
                    {

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

                        BankDbConnection.Open();

                        if (BankDbAftrekkenCommand.ExecuteNonQuery() == 0)
                        {
                            throw new Exception("Iets misgegaan bij het van de rekening afhalen van het geld.");
                        }
                    }

                }
                using (var Bank2DbConnection = Bank2Db.GetConnection())
                {
                    using (var Bank2DbStortenCommand = Bank2DbConnection.CreateCommand())
                    {

                        Bank2DbStortenCommand.CommandType = System.Data.CommandType.Text;
                        Bank2DbStortenCommand.CommandText = "update Rekeningen set Saldo=Saldo+@Bedrag where RekeningNr=@naarRekeningNr";

                        DbParameter ParStortenBedrag = Bank2DbStortenCommand.CreateParameter();
                        ParStortenBedrag.ParameterName = "@Bedrag";
                        ParStortenBedrag.Value = bedrag;
                        ParStortenBedrag.DbType = System.Data.DbType.Decimal;
                        Bank2DbStortenCommand.Parameters.Add(ParStortenBedrag);

                        DbParameter ParStortennaarRekeningNr = Bank2DbStortenCommand.CreateParameter();
                        ParStortennaarRekeningNr.ParameterName = "@naarRekeningNr";
                        ParStortennaarRekeningNr.Value = naarRekening;
                        ParStortennaarRekeningNr.DbType = System.Data.DbType.String;
                        Bank2DbStortenCommand.Parameters.Add(ParStortennaarRekeningNr);

                        Bank2DbConnection.Open();
                        if (Bank2DbStortenCommand.ExecuteNonQuery() == 0)
                        {
                            throw new Exception("Iets misgegaan bij het naar de rekening schrijven van het geld.");
                        }
                        traOverschrijven.Complete();
                        return true;
                    }
                }

            }
        }

    }
}
