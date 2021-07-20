using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe
{
    public class SaleItem
    {
        private Goods _goods;
        private double _quantity;
        private double _summ;
        public SaleItem(String name, double price, double quanntity, double summ)
        {
            _goods = new Goods(name, price);
            _quantity = quanntity;
            _summ = summ;
        }
        public double GetSaleSumm()
        {
            return _summ;
        }
    }
}
