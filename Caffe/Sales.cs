using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.Sqlite;

namespace Caffe
{
    public class Sales
    {
        private List<Cheque> _sales;
        private bool _isCheckOpen;
        private int _chequeNumber;
        private Cheque _currentCheque;
        private CashShift cashShift;
        private double _cashShiftLast;
        private const String connectionString= "Data Source=cash.sqlite;Mode=ReadWrite;";
        public Sales()
        {
            _chequeNumber = 0;
            _cashShiftLast = 0;
            _isCheckOpen = false;
            _sales = new List<Cheque>();
            cashShift = new();
            using var db = new SqliteConnection(connectionString);
            db.Open();
            var sql = "SELECT * FROM \"dbo.CashSviftTab\" ORDER BY Id DESC LIMIT 1";
            var command = new SqliteCommand(sql, db);
            using var result = command.ExecuteReader();
            if (result.HasRows)
            {
                while(result.Read())
                {
                    cashShift.Id = result.GetInt32("Id");
                    cashShift.IsOpen = result.GetBoolean("sOpen");
                    cashShift.Number = result.GetInt32("Number");
                }
                if(cashShift.IsOpen)
                {
                    sql = $"SELECT Number FROM \"dbo.ChequesTab\" WHERE CashShiftId = {cashShift.Id} ORDER BY Number DESC LIMIT 1";
                    var command2 = new SqliteCommand(sql, db);
                    using var resultNumber = command2.ExecuteReader();
                    if(resultNumber.HasRows)
                    {
                        resultNumber.Read();
                        _chequeNumber = resultNumber.GetInt32("Number");
                        sql = $"select  t1.Id, sum(t1.Summ) as TotalSumm, t2.CashShiftId as SwiftId from  'dbo.ChequeItemTab' t1 join 'dbo.ChequesTab' t2 on t1.ChequeId = t2.Id where t2.CashShiftId = '{cashShift.Id}'";
                        var command3 = new SqliteCommand(sql, db);
                        using var resultSumm = command3.ExecuteReader();
                        if (resultSumm.HasRows)
                        {
                            resultSumm.Read();
                            _cashShiftLast = resultSumm.GetDouble("TotalSumm"); 
                        }
                    }
                    else
                    {
                        _chequeNumber = 0;
                    }
                }
            }
            else
            {
                cashShift.Number = 0;
                cashShift.IsOpen = false;
            }
        }
        public bool OpenCashShift()
        {
            if (cashShift.IsOpen)
                return false;
            cashShift.Number++;
            cashShift.OpenTime = DateTime.Now;
            //TODO saving to database and get ID
            using var db = new SqliteConnection(connectionString);
            db.Open();
            var sql = $"INSERT INTO \"dbo.CashSviftTab\" (Number, OpenTime, sOpen) VALUES ({cashShift.Number},'{cashShift.OpenTime}', 1);";
            var command = new SqliteCommand(sql, db);
            command.ExecuteNonQuery();
            sql = $"SELECT Id FROM \"dbo.CashSviftTab\" where Number={cashShift.Number}";
            var command2 = new SqliteCommand(sql, db);
            using var result = command2.ExecuteReader();
            if (result.HasRows)
            {
                result.Read();
                cashShift.Id = result.GetInt32("Id");
            }
            else
            {
                cashShift.Id = 0;
            }
            db.Close();
            cashShift.IsOpen = true;
            return true;
        }
        public bool CloseCashShift()
        {
            if (!cashShift.IsOpen)
                return false;
            cashShift.CloseTime = DateTime.Now;
            //saving info to database
            using var db = new SqliteConnection(connectionString);
            db.Open();
            var sql = $" UPDATE \"dbo.CashSviftTab\" SET CloseTime = '{cashShift.CloseTime}', sOpen = 0 WHERE Id = {cashShift.Id}";
            var command = new SqliteCommand(sql, db);
            command.ExecuteNonQuery();
            db.Close();
            cashShift.IsOpen = false;
            return true;
        }

        public bool OpenCheque()
        {
            if (_isCheckOpen)
                return false;
            _isCheckOpen = true;
            _chequeNumber++;
            _currentCheque = new Cheque(_chequeNumber, cashShift.Id, DateTime.Now);
            using var db = new SqliteConnection(connectionString);
            db.Open();
            var sql = $"INSERT INTO \"dbo.ChequesTab\" (CashShiftId, DateTime, Number) VALUES ({cashShift.Id}, '{_currentCheque.GetChequeDateTime()}', {_chequeNumber});";
            var command = new SqliteCommand(sql, db);
            command.ExecuteNonQuery();
            sql = $"SELECT Id FROM \"dbo.ChequesTab\" WHERE Number = {_chequeNumber}";
            var command2 = new SqliteCommand(sql, db);
            using var result = command2.ExecuteReader();
            result.Read();
            _currentCheque.ChequeId = result.GetInt32("Id");
            db.Close();
            return true;
        }
        public void  RegisterChequeItem(String name, double price, double quantity, double summ)
        {
            if (summ == 0)
                return;
            _currentCheque.RegisterChequeItem(name, price, quantity, summ);
            using var db = new SqliteConnection(connectionString);
            db.Open();
            var sql = $"INSERT INTO \"dbo.ChequeItemTab\" (ChequeId, Name, Price, Quantity, Summ) VALUES ('{_currentCheque.ChequeId}', \"{name}\", '{price}', '{quantity}', '{summ}');";
            var command = new SqliteCommand(sql, db);
            command.ExecuteNonQuery();
            db.Close();
        }
       
        public bool CloseCheque()
        {
            if (!_isCheckOpen)
                return false;
            _currentCheque.RegisterCheque();
            _sales.Add(_currentCheque);
            _isCheckOpen = false;
            _currentCheque = null;
            return true;
        }
        public double GetDayCash()
        {
            double result = 0;
            foreach (var i in _sales)
            {
                result += i.GetChequeSumm();
            }
            return result + _cashShiftLast;
        }
        public bool GetCashShiftStatus()
        {
            if (!cashShift.IsOpen)
                return false;
            return true;
        }
        public int GetCashShiftNumber()
        {
            return cashShift.Number;
        }
    }
}
