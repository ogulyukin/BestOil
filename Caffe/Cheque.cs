using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe
{
    public class Cheque
    {
        private List<SaleItem> _cheque;
        private double _summ;
        private int _number;
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
        public Cheque(int number)
        {
            _summ = 0;
            _number = number;
            _cheque = new List<SaleItem>();
        }
    }
}
