using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.interfaces;
using BankApplication.CommonLayer.src.models;
using BankApplication.CommonLayer.src.enums;
using BankApplication.DataAccessLayer.utils;

namespace BankApplication.DataAccessLayer
{
    public class TransactionsDbRepository : ITransactionRepository
    {
        public List<Transaction> GetAll()
        {
            IDbConnection conn = DbHelper.GetConnection();

            string sqlSelect = $"select * from transactions";

            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sqlSelect;
            cmd.Connection = conn;

            conn.Open();
            IDataReader reader = cmd.ExecuteReader();
            List<Transaction> transactionsList = new List<Transaction>();
            while (reader.Read())
            {
                int transId = Convert.ToInt32(reader["transId"]);
                TransactionType transactionType = (TransactionType)Enum.Parse(typeof(TransactionType), reader["transactionType"].ToString());
                string accNo = reader["accNo"].ToString();
                double amount = Convert.ToDouble(reader["amount"]);
                DateTime transDate = Convert.ToDateTime(reader["transDate"]);
                TransactionStatus transStatus = (TransactionStatus)Enum.Parse(typeof(TransactionStatus), reader["transStatus"].ToString());

                Transaction transaction = new Transaction
                {
                    TransID = transId,
                    TransactionType = transactionType,
                    FromAccount = new SavingAccount(),
                    TranDate = transDate,
                    Amount = amount,
                    Status = transStatus
                };
                transactionsList.Add(transaction);
            }
            conn.Close();
            return transactionsList;
        }

        public Transaction GetById(int transId)
        {
            IDbConnection conn = DbHelper.GetConnection();

            string sqlSelect = $"select * from transactions where transId = @transId";

            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sqlSelect;
            cmd.Connection = conn;

            IDbDataParameter p1 = cmd.CreateParameter();
            p1.ParameterName = "@transId";
            p1.Value = transId;
            cmd.Parameters.Add(p1);

            conn.Open();
            IDataReader reader = cmd.ExecuteReader();
            Transaction transaction = null;
            while (reader.Read())
            {
                TransactionType transactionType = (TransactionType)Enum.Parse(typeof(TransactionType), reader["transactionType"].ToString());
                string accNo = reader["accNo"].ToString();
                double amount = Convert.ToDouble(reader["amount"]);
                DateTime transDate = Convert.ToDateTime(reader["transDate"]);
                TransactionStatus transStatus = (TransactionStatus)Enum.Parse(typeof(TransactionStatus), reader["transStatus"].ToString());

                transaction = new Transaction
                {
                    TransID = transId,
                    TransactionType = transactionType,
                    FromAccount = new SavingAccount(),
                    TranDate = transDate,
                    Amount = amount,
                    Status = transStatus
                };
            }
            conn.Close();
            return transaction;
        }

        public List<Transaction> GetByType(TransactionType transactionType)
        {
            IDbConnection conn = DbHelper.GetConnection();

            string sqlSelect = $"select * from transactions where transactionType = @transactionType";

            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sqlSelect;
            cmd.Connection = conn;

            IDbDataParameter p1 = cmd.CreateParameter();
            p1.ParameterName = "@transactionType";
            p1.Value = transactionType;
            cmd.Parameters.Add(p1);

            conn.Open();
            IDataReader reader = cmd.ExecuteReader();
            List<Transaction> transactionsList = new List<Transaction>();
            while (reader.Read())
            {
                Transaction transaction = null;

                int transId = Convert.ToInt32(reader["transId"]);
                string accNo = reader["accNo"].ToString();
                double amount = Convert.ToDouble(reader["amount"]);
                DateTime transDate = Convert.ToDateTime(reader["transDate"]);
                TransactionStatus transStatus = (TransactionStatus)Enum.Parse(typeof(TransactionStatus), reader["transStatus"].ToString());

                transaction = new Transaction
                {
                    TransID = transId,
                    TransactionType = transactionType,
                    FromAccount = new SavingAccount(),
                    TranDate = transDate,
                    Amount = amount,
                    Status = transStatus
                };
                transactionsList.Add(transaction);
            }
            conn.Close();
            return transactionsList;
        }

        public void Save(Transaction transaction)
        {
            IDbConnection conn = DbHelper.GetConnection();

            string sqlInsert = $"insert into transactions values (@transId, @transactionType, @accNo, @transDate, @amount, @transStatus)";

            IDbCommand cmd = conn.CreateCommand();

            IDbDataParameter p1 = cmd.CreateParameter();
            p1.ParameterName = "@transId";
            p1.Value = transaction.TransID;
            cmd.Parameters.Add(p1);

            IDbDataParameter p2 = cmd.CreateParameter();
            p2.ParameterName = "@transactionType";
            p2.Value = transaction.TransactionType.ToString();
            cmd.Parameters.Add(p2);

            IDbDataParameter p3 = cmd.CreateParameter();
            p3.ParameterName = "@accNo";
            p3.Value = transaction.FromAccount.AccNo;
            cmd.Parameters.Add(p3);

            IDbDataParameter p4 = cmd.CreateParameter();
            p4.ParameterName = "@transDate";
            p4.Value = transaction.TranDate;
            cmd.Parameters.Add(p4);

            IDbDataParameter p5 = cmd.CreateParameter();
            p5.ParameterName = "@amount";
            p5.Value = transaction.Amount;
            cmd.Parameters.Add(p5);

            IDbDataParameter p6 = cmd.CreateParameter();
            p6.ParameterName = "@transStatus";
            p6.Value = transaction.Status.ToString();
            cmd.Parameters.Add(p6);

            cmd.CommandText = sqlInsert;
            cmd.Connection = conn;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
