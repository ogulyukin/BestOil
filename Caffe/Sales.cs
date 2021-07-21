using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Caffe
{
    public class Sales
    {
        private List<Cheque> _sales;
        private bool _isCheckOpen;
        private int _number;
        private Cheque _currentCheque;
        private CashShift cashShift;
        public Sales()
        {
            _number = 0;
            _isCheckOpen = false;
            _sales = new List<Cheque>();
            cashShift = new();
            cashShift.IsOpen = false;
            //todo loading from database
        }
        public bool OpenCashShift()
        {
            if (cashShift.IsOpen)
                return false;
            cashShift.Number = 1; //last number from database have to be here ++
            cashShift.OpenTime = DateTime.Now;
            //TODO saving to database and get ID
            cashShift.Id = 1;
            cashShift.IsOpen = true;
            return true;
        }
        public bool CloseCashShift()
        {
            if (!cashShift.IsOpen)
                return false;
            cashShift.CloseTime = DateTime.Now;
            //TODO saving info to database
            cashShift.IsOpen = false;
            return true;
        }

        public bool OpenCheque()
        {
            if (_isCheckOpen)
                return false;
            _isCheckOpen = true;
            _number++;
            _currentCheque = new Cheque(_number, cashShift.Id);
            return true;
        }
        public void  RegisterChequeItem(String name, double price, double quantity, double summ)
        {
            if (summ == 0)
                return;
            _currentCheque.RegisterChequeItem(name, price, quantity, summ);
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
            return result;
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
