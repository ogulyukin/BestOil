using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe
{
    public class Goods
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public Goods(string name, double price)
        {
            this.Name = name;
            this.Price = price;
        }
    }
}
