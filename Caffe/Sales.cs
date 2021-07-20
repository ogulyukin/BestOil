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
        public Sales()
        {
            number = 0;
            isCheckOpen = false;
            _sales = new List<Cheque>();
        }

        public int OpenCheque()
        {
            if (isCheckOpen)
                return 0;
            isCheckOpen = true;
            number++;
            return number;
        }
       
        public bool CloseCheque(Cheque newcheque)
        {
            if (!isCheckOpen)
                return false;
            newcheque.RegisterCheque();
            _sales.Add(newcheque);
            isCheckOpen = false;
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

    }
}
