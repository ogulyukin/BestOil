using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Caffe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _FuelPrice = 0;
        private double _TotalSumm = 0;
        private double _TotalLitres = 0;
        private double _TotalCash = 0;
        private Sales sales;
        public MainWindow()
        {
            InitializeComponent();
            SelectFuel.SelectedIndex = 1;
            _FuelPrice = 47.5;
            FuelPrice.Text = Convert.ToString(_FuelPrice);
            FuelSumm.IsEnabled = false;
            Litres.IsEnabled = false;
            sales = new Sales();
        }
        

        private void SelectFuel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string currentFuel = ((TextBlock)SelectFuel.SelectedItem).Text;
            switch (currentFuel)
            {
                case "АИ95":
                    ChangeFuelPrice(58.3);
                    break;
                case "АИ92":
                    ChangeFuelPrice(47.5);
                    break;
                case "ДТ":
                    ChangeFuelPrice(51.9);
                    break;
            }
        }

        private void ChangeFuelPrice(double newPrice)
        {
            if(chooseSumm.IsChecked == true)
            {
                _FuelPrice = newPrice;
                FuelPrice.Text = Convert.ToString(newPrice);
                _TotalLitres = ConvertToDouble(FuelSumm.Text) / _FuelPrice;
                Litres.Text = ReduceDouble(_TotalLitres);
            }
            else if (chooseLitres.IsChecked == true) 
            {
                _TotalSumm -= _FuelPrice * ConvertToDouble(Litres.Text);
                _FuelPrice = newPrice;
                FuelPrice.Text = Convert.ToString(newPrice);
                _TotalSumm += _FuelPrice * ConvertToDouble(Litres.Text);
                TotalSumm.Text = ReduceDouble(_TotalSumm);
            }
            else
            {
                _FuelPrice = newPrice;
                FuelPrice.Text = Convert.ToString(newPrice);
            }                                        
                    
        }

        private void ChooseLitres_Checked(object sender, RoutedEventArgs e)
        {
            chooseSumm.IsChecked = false;
            FuelSumm.IsEnabled = false;
            Litres.IsEnabled = true;
            ClearAllFuel(0);            
        }

        private void ChooseSumm_Checked(object sender, RoutedEventArgs e)
        {
            chooseLitres.IsChecked = false;
            FuelSumm.IsEnabled = true;
            Litres.IsEnabled = false;
            ClearAllFuel(0);
        }

        private void Calculation_Click(object sender, RoutedEventArgs e)
        {
            Cheque newcheque = new(sales.OpenCheque());
            newcheque.RegisterChequeItem(((TextBlock)SelectFuel.SelectedItem).Text, _FuelPrice, _TotalLitres, _FuelPrice * _TotalLitres);
            if (Check01.IsChecked == true)
            {
                newcheque.RegisterChequeItem(Goods01.Text, Convert.ToDouble(Price01.Text), 1, Convert.ToDouble(Price01.Text));
                Check01.IsChecked = false;
            }                
            if (Check02.IsChecked == true)
            {
                newcheque.RegisterChequeItem(Goods02.Text, Convert.ToDouble(Price02.Text), 1, Convert.ToDouble(Price02.Text));
                Check02.IsChecked = false;
            }                
            if (Check03.IsChecked == true)
            {
                newcheque.RegisterChequeItem(Goods03.Text, Convert.ToDouble(Price03.Text), 1, Convert.ToDouble(Price03.Text));
                Check03.IsChecked = false;
            }                
            if (Check04.IsChecked == true)
            {
                newcheque.RegisterChequeItem(Goods04.Text, Convert.ToDouble(Price04.Text), 1, Convert.ToDouble(Price04.Text));
                Check04.IsChecked = false;
            }                
            if (Check05.IsChecked == true)
            {
                newcheque.RegisterChequeItem(Goods05.Text, Convert.ToDouble(Price05.Text), 1, Convert.ToDouble(Price05.Text));
                Check05.IsChecked = false;
            }
            if (Check06.IsChecked == true)
            {
                newcheque.RegisterChequeItem(Goods06.Text, Convert.ToDouble(Price06.Text), 1, Convert.ToDouble(Price06.Text));
                Check06.IsChecked = false;
            }
            if (Check07.IsChecked == true)
            {
                newcheque.RegisterChequeItem(Goods07.Text, Convert.ToDouble(Price07.Text), 1, Convert.ToDouble(Price07.Text));
                Check07.IsChecked = false;
            }
            if (Check08.IsChecked == true)
            {
                newcheque.RegisterChequeItem(Goods08.Text, Convert.ToDouble(Price08.Text), 1, Convert.ToDouble(Price08.Text));
                Check08.IsChecked = false;
            }
            if (Check09.IsChecked == true)
            {
                newcheque.RegisterChequeItem(Goods09.Text, Convert.ToDouble(Price09.Text), 1, Convert.ToDouble(Price09.Text));
                Check09.IsChecked = false;
            }
            if (Check10.IsChecked == true)
            {
                newcheque.RegisterChequeItem(Goods10.Text, Convert.ToDouble(Price10.Text), 1, Convert.ToDouble(Price10.Text));
                Check10.IsChecked = false;
            }
            sales.CloseCheque(newcheque);
            _TotalCash = sales.GetDayCash();
            ClearAllFuel(0);
            TotalCash.Text = Convert.ToString(_TotalCash);
        }

        private void Goods_Checked(object sender, RoutedEventArgs e)
        {
            _TotalSumm = CalculateGoods() + _TotalLitres * _FuelPrice;
            TotalSumm.Text = Convert.ToString(_TotalSumm);
        }

        private void FuelSumm_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            double temp = ConvertToDouble(FuelSumm.Text);
            ClearAllFuel(1);
            _TotalSumm += temp;
            _TotalLitres = temp / _FuelPrice;
            TotalSumm.Text = ReduceDouble(_TotalSumm);
            TotalLitres.Text = ReduceDouble(_TotalLitres);
        }

        private void Litres_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            double temp = ConvertToDouble(Litres.Text); 
            _TotalSumm = temp * _FuelPrice;
            _TotalSumm += CalculateGoods();
            _TotalLitres = temp;
            TotalSumm.Text = ReduceDouble(_TotalSumm);
            TotalLitres.Text = ReduceDouble(_TotalLitres);
        }
        private static double ConvertToDouble(String value)
        {
            double temp;
            try
            {
                temp = Convert.ToDouble(value);
            }
            catch
            {
                return 0;
            }
            return temp;
        }
        private void ClearAllFuel(int flag)
        {
            Litres.Text = "0";
            if(flag != 1)
                FuelSumm.Text = "0";
            _TotalSumm = CalculateGoods();
            _TotalLitres = 0;
            TotalSumm.Text = ReduceDouble(_TotalSumm);
            TotalLitres.Text = "0";
        }
        private double CalculateGoods()
        {
            double result = 0;
            if (Check01.IsChecked == true)
                result += Convert.ToDouble(Price01.Text);
            if (Check02.IsChecked == true)
                result += Convert.ToDouble(Price02.Text);
            if (Check03.IsChecked == true)
                result += Convert.ToDouble(Price03.Text);
            if (Check04.IsChecked == true)
                result += Convert.ToDouble(Price04.Text);
            if (Check05.IsChecked == true)
                result += Convert.ToDouble(Price05.Text);
            if (Check06.IsChecked == true)
                result += Convert.ToDouble(Price06.Text);
            if (Check07.IsChecked == true)
                result += Convert.ToDouble(Price07.Text);
            if (Check08.IsChecked == true)
                result += Convert.ToDouble(Price08.Text);
            if (Check09.IsChecked == true)
                result += Convert.ToDouble(Price09.Text);
            if (Check10.IsChecked == true)
                result += Convert.ToDouble(Price10.Text);
            return result;
        }
        private String ReduceDouble(double value)
        {
            String result = Convert.ToString(Convert.ToDouble(Convert.ToInt32(value * 100)) / 100);
            return result;
        }

        private void Goods_Unchecked(object sender, RoutedEventArgs e)
        {
            _TotalSumm = CalculateGoods() + _TotalLitres * _FuelPrice;
            TotalSumm.Text = Convert.ToString(_TotalSumm);
        }
    }
}
