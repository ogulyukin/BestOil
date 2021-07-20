using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffe
{
    public class Sales
    {
        private List<Cheque> _sales;
        private bool isCheckOpen;
        int number;
        Cheque currentCheque;
        public Sales()
        {
            number = 0;
            isCheckOpen = false;
            _sales = new List<Cheque>();
        }

        public bool OpenCheque()
        {
            if (isCheckOpen)
                return false;
            isCheckOpen = true;
            number++;
            currentCheque = new Cheque(number);
            return true;
        }
        public void  RegisterChequeItem(String name, double price, double quantity, double summ)
        {
            if (summ == 0)
                return;
            currentCheque.RegisterChequeItem(name, price, quantity, summ);
        }
       
        public bool CloseCheque()
        {
            if (!isCheckOpen)
                return false;
            currentCheque.RegisterCheque();
            _sales.Add(currentCheque);
            isCheckOpen = false;
            return true;
            currentCheque = null;
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

    }
}
