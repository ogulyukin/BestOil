using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe
{
    public class Cheque
    {
        public int ChequeId { get; set; }
        private int _cashShiftId;
        private List<SaleItem> _cheque;
        private double _summ;
        private int _number;
        private DateTime _dateTime;
        public void RegisterChequeItem(String name, double price, double quantity, double summ)
        {
            SaleItem newSale = new(name, price, quantity, summ);
            _cheque.Add(newSale);
        }
        public void RegisterCheque()
        { 
            foreach (var i in _cheque)
                _summ += i.GetSaleSumm();
        }
        public double GetChequeSumm()
        {
            return _summ;
        }
        public Cheque(int number, int cashShiftId, DateTime dateTime)
        {
            _summ = 0;
            _number = number;
            _cheque = new List<SaleItem>();
            _cashShiftId = cashShiftId;
            _dateTime = dateTime;
        }
        public DateTime GetChequeDateTime()
        {
            return _dateTime;
        }
    }
}
