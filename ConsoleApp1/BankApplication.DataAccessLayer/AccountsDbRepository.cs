using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.interfaces;
using System.Configuration;
using System.Data.SqlClient;
using BankApplication.CommonLayer.src.enums;
using BankApplication.CommonLayer.src.models;
using BankApplication.DataAccessLayer.utils;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace BankApplication.DataAccessLayer
{
    public class AccountsDbRepository : IAccountsRepository
    {
        public Dictionary<string, long> GetAccountCountByType()
        {
            IDbConnection conn = DbHelper.GetConnection();

            string sqlSelect = $"select * from accounts";

            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sqlSelect;
            cmd.Connection = conn;

            conn.Open();
            IDataReader reader = cmd.ExecuteReader();
            Dictionary<string, long> accountCountByType = new Dictionary<string, long>();
            while (reader.Read())
            {
                string accType = reader["accType"].ToString();
                long count = Convert.ToInt64(reader[1]);

                if (accountCountByType.ContainsKey(accType))
                {
                    accountCountByType[accType] += count;
                }
                else
                {
                    accountCountByType.Add(accType, count);
                }
            }
            conn.Close();
            return accountCountByType;
        }

        public List<IAccount> GetAll()
        {
            IDbConnection conn = DbHelper.GetConnection();

            string sqlSelect = $"select * from accounts";

            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sqlSelect;
            cmd.Connection = conn;

            conn.Open();
            IDataReader reader = cmd.ExecuteReader();
            List<IAccount> accountsList = new List<IAccount>();
            while (reader.Read())
            {
                IAccount account = null;
                AccountType accountType = (AccountType)Enum.Parse(typeof(AccountType), reader["accType"].ToString());

                if (accountType == AccountType.SAVING)
                {
                    account = new SavingAccount();
                }
                else
                {
                    account = new CurrentAccount();
                }
                account.AccNo = Convert.ToString(reader["accNo"]);
                account.Name = Convert.ToString(reader["name"]);
                account.Pin = Convert.ToString(reader["pin"]);
                account.Active = Convert.ToBoolean(reader["active"]);
                account.DateOfOpening = Convert.ToDateTime(reader["dateOfOpening"]);
                account.Balance = Convert.ToDouble(reader["balance"]);
                account.PrivilegeType = (PrivilegeType)Enum.Parse(typeof(PrivilegeType), reader["privilegeType"].ToString());

                accountsList.Add(account);
            }
            conn.Close();
            return accountsList;
        }

        public IAccount GetById(string accNo)
        {
            IDbConnection conn = DbHelper.GetConnection();

            string sqlSelect = $"select * from accounts where accNo = @accNo";

            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sqlSelect;
            cmd.Connection = conn;

            IDbDataParameter p1 = cmd.CreateParameter();
            p1.ParameterName = "@accNO";
            p1.Value = accNo;
            cmd.Parameters.Add(p1);

            conn.Open();
            IAccount account = null;
            IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                AccountType accountType = (AccountType)Enum.Parse(typeof(AccountType), reader["accType"].ToString());

                if (accountType == AccountType.SAVING)
                {
                    account = new SavingAccount();
                }
                else
                {
                    account = new CurrentAccount();
                }
                account.AccNo = accNo;
                account.Name = Convert.ToString(reader["name"]);
                account.Pin = Convert.ToString(reader["pin"]);
                account.Active = Convert.ToBoolean(reader["active"]);
                account.DateOfOpening = Convert.ToDateTime(reader["dateOfOpening"]);
                account.Balance = Convert.ToDouble(reader["balance"]);
                account.PrivilegeType = (PrivilegeType)Enum.Parse(typeof(PrivilegeType), reader["privilegeType"].ToString());
            }
            conn.Close();
            return account;
        }

        public long GetTotalNoOfAccounts()
        {
            IDbConnection conn = DbHelper.GetConnection();

            string sqlSelect = "select count(*) from accounts";

            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sqlSelect;
            cmd.Connection = conn;

            conn.Open();
            long totalNoOfAccounts = 0;
            IDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) 
            {
                totalNoOfAccounts = Convert.ToInt64(reader[0]);
            }
            conn.Close();
            return totalNoOfAccounts;
        }

        public double GetTotalWorth()
        {
            IDbConnection conn = DbHelper.GetConnection();

            string sqlSelect = "select sum(balance) from accounts";

            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sqlSelect;
            cmd.Connection = conn;

            conn.Open();
            double totalWorth = 0.0;
            IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                totalWorth = Convert.ToDouble(reader[0]);
            }
            conn.Close();
            return totalWorth;
        }

        public void Save(IAccount account)
        {
            IDbConnection conn = DbHelper.GetConnection();

            string sqlInsert = $"insert into accounts values (@accNo, @name, @pin, @active, @dateOfOpening, @balance, @privilegeType, @accType)";

            IDbCommand cmd = conn.CreateCommand();

            IDbDataParameter p1 = cmd.CreateParameter();
            p1.ParameterName = "@accNO";
            p1.Value = account.AccNo;
            cmd.Parameters.Add(p1);

            IDbDataParameter p2 = cmd.CreateParameter();
            p2.ParameterName = "@name";
            p2.Value = account.Name;
            cmd.Parameters.Add(p2);

            IDbDataParameter p3 = cmd.CreateParameter();
            p3.ParameterName = "@pin";
            p3.Value = account.Pin;
            cmd.Parameters.Add(p3);

            IDbDataParameter p4 = cmd.CreateParameter();
            p4.ParameterName = "@active";
            p4.Value = account.Active;
            cmd.Parameters.Add(p4);

            IDbDataParameter p5 = cmd.CreateParameter();
            p5.ParameterName = "@dateOfOpening";
            p5.Value = account.DateOfOpening;
            cmd.Parameters.Add(p5);
            
            IDbDataParameter p6 = cmd.CreateParameter();
            p6.ParameterName = "@balance";
            p6.Value = account.Balance;
            cmd.Parameters.Add(p6);
            
            IDbDataParameter p7 = cmd.CreateParameter();
            p7.ParameterName = "@privilegeType";
            p7.Value = account.PrivilegeType.ToString();
            cmd.Parameters.Add(p7);
            
            IDbDataParameter p8 = cmd.CreateParameter();
            p8.ParameterName = "@accType";
            p8.Value = account.GetAccType();
            cmd.Parameters.Add(p8);

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

        public void Update(IAccount account)
        {
            IDbConnection conn = DbHelper.GetConnection();

            string sqlUpdate = $"update accounts set name=@name, pin=@pin, active=@active, dateOfOpening=@dateOfOpening, " +
                                $"balance=@balance, privilegeType=@privilegeType, accType=@accType where accNo = @accNo";

            IDbCommand cmd = conn.CreateCommand();

            IDbDataParameter p1 = cmd.CreateParameter();
            p1.ParameterName = "@accNO";
            p1.Value = account.AccNo;
            cmd.Parameters.Add(p1);

            IDbDataParameter p2 = cmd.CreateParameter();
            p2.ParameterName = "@name";
            p2.Value = account.Name;
            cmd.Parameters.Add(p2);

            IDbDataParameter p3 = cmd.CreateParameter();
            p3.ParameterName = "@pin";
            p3.Value = account.Pin;
            cmd.Parameters.Add(p3);

            IDbDataParameter p4 = cmd.CreateParameter();
            p4.ParameterName = "@active";
            p4.Value = account.Active;
            cmd.Parameters.Add(p4);

            IDbDataParameter p5 = cmd.CreateParameter();
            p5.ParameterName = "@dateOfOpening";
            p5.Value = account.DateOfOpening;
            cmd.Parameters.Add(p5);

            IDbDataParameter p6 = cmd.CreateParameter();
            p6.ParameterName = "@balance";
            p6.Value = account.Balance;
            cmd.Parameters.Add(p6);

            IDbDataParameter p7 = cmd.CreateParameter();
            p7.ParameterName = "@privilegeType";
            p7.Value = account.PrivilegeType.ToString();
            cmd.Parameters.Add(p7);

            IDbDataParameter p8 = cmd.CreateParameter();
            p8.ParameterName = "@accType";
            p8.Value = account.GetAccType().ToString();
            cmd.Parameters.Add(p8);

            cmd.CommandText = sqlUpdate;
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
