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
        private GoodsList fuelList;
        public MainWindow()
        {
            InitializeComponent();
            FuelPrice.Text = Convert.ToString(_FuelPrice);
            FuelSumm.IsEnabled = false;
            Litres.IsEnabled = false;
            sales = new Sales();
            if (sales.GetCashShiftStatus())
            {
                CashStatus.Content = "Закрыть смену";
                CashSviftNumber.Text = Convert.ToString(sales.GetCashShiftNumber());
            }
            GoodsList list = new();
            fuelList = new();
            list = GoodsLoader.GetGoodsList();
            SetGoodsData(list);
            TotalCash.Text = Convert.ToString(sales.GetDayCash());
        }
        private void SetGoodsData(GoodsList list)
        {
            try
            {
                Goods01.Text = list.GList.ElementAt(0).Name;
                Price01.Text = Convert.ToString(list.GList.ElementAt(0).Price);
                Goods02.Text = list.GList.ElementAt(1).Name;
                Price02.Text = Convert.ToString(list.GList.ElementAt(1).Price);
                Goods03.Text = list.GList.ElementAt(2).Name;
                Price03.Text = Convert.ToString(list.GList.ElementAt(2).Price);
                Goods04.Text = list.GList.ElementAt(3).Name;
                Price04.Text = Convert.ToString(list.GList.ElementAt(3).Price);
                Goods05.Text = list.GList.ElementAt(4).Name;
                Price05.Text = Convert.ToString(list.GList.ElementAt(4).Price);
                Goods06.Text = list.GList.ElementAt(5).Name;
                Price06.Text = Convert.ToString(list.GList.ElementAt(5).Price);
                Goods07.Text = list.GList.ElementAt(6).Name;
                Price07.Text = Convert.ToString(list.GList.ElementAt(6).Price);
                Goods08.Text = list.GList.ElementAt(7).Name;
                Price08.Text = Convert.ToString(list.GList.ElementAt(7).Price);
                Goods09.Text = list.GList.ElementAt(8).Name;
                Price09.Text = Convert.ToString(list.GList.ElementAt(8).Price);
                Goods10.Text = list.GList.ElementAt(9).Name;
                Price10.Text = Convert.ToString(list.GList.ElementAt(9).Price);
                fuelList.GList.Add(new(list.GList.ElementAt(10).Name, list.GList.ElementAt(10).Price));
                fuelList.GList.Add(new(list.GList.ElementAt(11).Name, list.GList.ElementAt(11).Price));
                fuelList.GList.Add(new(list.GList.ElementAt(12).Name, list.GList.ElementAt(12).Price));
                fuelList.GList.Add(new(list.GList.ElementAt(13).Name, list.GList.ElementAt(13).Price));
                fuelList.GList.Add(new(list.GList.ElementAt(14).Name, list.GList.ElementAt(14).Price));
                Fuel00.Text = fuelList.GList.ElementAt(0).Name;
                Fuel01.Text = fuelList.GList.ElementAt(1).Name;
                Fuel02.Text = fuelList.GList.ElementAt(2).Name;
                Fuel03.Text = fuelList.GList.ElementAt(3).Name;
                Fuel04.Text = fuelList.GList.ElementAt(4).Name;
                SelectFuel.SelectedIndex = 1;
                ChangeFuelPrice(fuelList.GList.ElementAt(1).Price);
            }
            catch
            {
                return;
            }
        }
        

        private void SelectFuel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string currentFuel = ((TextBlock)SelectFuel.SelectedItem).Text;
            foreach(var i in fuelList.GList)
            {
                if (currentFuel == i.Name)
                    ChangeFuelPrice(i.Price);
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
            if (!sales.GetCashShiftStatus() || TotalSumm.Text == "0")
                return;
            sales.OpenCheque();
            if (_TotalLitres != 0)
                sales.RegisterChequeItem(((TextBlock)SelectFuel.SelectedItem).Text, _FuelPrice, Convert.ToDouble(TotalLitres.Text), _FuelPrice * _TotalLitres);
            if (Check01.IsChecked == true)
            {
                sales.RegisterChequeItem(Goods01.Text, Convert.ToDouble(Price01.Text), Convert.ToDouble(Quant01.Text), Convert.ToDouble(Price01.Text) * Convert.ToDouble(Quant01.Text));
                Check01.IsChecked = false;
            }                
            if (Check02.IsChecked == true)
            {
                sales.RegisterChequeItem(Goods02.Text, Convert.ToDouble(Price02.Text), Convert.ToDouble(Quant02.Text), Convert.ToDouble(Price02.Text) * Convert.ToDouble(Quant02.Text));
                Check02.IsChecked = false;
            }                
            if (Check03.IsChecked == true)
            {
                sales.RegisterChequeItem(Goods03.Text, Convert.ToDouble(Price03.Text), Convert.ToDouble(Quant03.Text), Convert.ToDouble(Price03.Text) * Convert.ToDouble(Quant03.Text));
                Check03.IsChecked = false;
            }                
            if (Check04.IsChecked == true)
            {
                sales.RegisterChequeItem(Goods04.Text, Convert.ToDouble(Price04.Text), Convert.ToDouble(Quant04.Text), Convert.ToDouble(Price04.Text) * Convert.ToDouble(Quant04.Text));
                Check04.IsChecked = false;
            }                
            if (Check05.IsChecked == true)
            {
                sales.RegisterChequeItem(Goods05.Text, Convert.ToDouble(Price05.Text), Convert.ToDouble(Quant05.Text), Convert.ToDouble(Price05.Text) * Convert.ToDouble(Quant05.Text));
                Check05.IsChecked = false;
            }
            if (Check06.IsChecked == true)
            {
                sales.RegisterChequeItem(Goods06.Text, Convert.ToDouble(Price06.Text), Convert.ToDouble(Quant06.Text), Convert.ToDouble(Price06.Text) * Convert.ToDouble(Quant06.Text));
                Check06.IsChecked = false;
            }
            if (Check07.IsChecked == true)
            {
                sales.RegisterChequeItem(Goods07.Text, Convert.ToDouble(Price07.Text), Convert.ToDouble(Quant07.Text), Convert.ToDouble(Price07.Text) * Convert.ToDouble(Quant07.Text));
                Check07.IsChecked = false;
            }
            if (Check08.IsChecked == true)
            {
                sales.RegisterChequeItem(Goods08.Text, Convert.ToDouble(Price08.Text), Convert.ToDouble(Quant08.Text), Convert.ToDouble(Price08.Text) * Convert.ToDouble(Quant08.Text));
                Check08.IsChecked = false;
            }
            if (Check09.IsChecked == true)
            {
                sales.RegisterChequeItem(Goods09.Text, Convert.ToDouble(Price09.Text), Convert.ToDouble(Quant09.Text), Convert.ToDouble(Price09.Text) * Convert.ToDouble(Quant09.Text));
                Check09.IsChecked = false;
            }
            if (Check10.IsChecked == true)
            {
                sales.RegisterChequeItem(Goods10.Text, Convert.ToDouble(Price10.Text), Convert.ToDouble(Quant10.Text), Convert.ToDouble(Price10.Text) * Convert.ToDouble(Quant10.Text));
                Check10.IsChecked = false;
            }
            sales.CloseCheque();
            _TotalCash = sales.GetDayCash();
            ClearAllFuel(0);
            TotalCash.Text = Convert.ToString(_TotalCash);
        }

        private void Goods_Checked(object sender, RoutedEventArgs e)
        {
            GoodsQuantFieldChecker();
            _TotalSumm = CalculateGoods() + _TotalLitres * _FuelPrice;
            TotalSumm.Text = Convert.ToString(_TotalSumm);
        }

        private void Goods_Unchecked(object sender, RoutedEventArgs e)
        {
            GoodsQuantFieldChecker();
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
                result += Convert.ToDouble(Price01.Text) * (Quant01.Text != "0"?Convert.ToDouble(Quant01.Text):1);
            if (Check02.IsChecked == true)
                result += Convert.ToDouble(Price02.Text) * (Quant02.Text != "0" ? Convert.ToDouble(Quant02.Text) : 1);
            if (Check03.IsChecked == true)
                result += Convert.ToDouble(Price03.Text) * (Quant03.Text != "0" ? Convert.ToDouble(Quant03.Text) : 1);
            if (Check04.IsChecked == true)
                result += Convert.ToDouble(Price04.Text) * (Quant04.Text != "0" ? Convert.ToDouble(Quant04.Text) : 1);
            if (Check05.IsChecked == true)
                result += Convert.ToDouble(Price05.Text) * (Quant05.Text != "0" ? Convert.ToDouble(Quant05.Text) : 1);
            if (Check06.IsChecked == true)
                result += Convert.ToDouble(Price06.Text) * (Quant06.Text != "0" ? Convert.ToDouble(Quant06.Text) : 1);
            if (Check07.IsChecked == true)
                result += Convert.ToDouble(Price07.Text) * (Quant07.Text != "0" ? Convert.ToDouble(Quant07.Text) : 1);
            if (Check08.IsChecked == true)
                result += Convert.ToDouble(Price08.Text) * (Quant08.Text != "0" ? Convert.ToDouble(Quant08.Text) : 1);
            if (Check09.IsChecked == true)
                result += Convert.ToDouble(Price09.Text) * (Quant09.Text != "0" ? Convert.ToDouble(Quant09.Text) : 1);
            if (Check10.IsChecked == true)
                result += Convert.ToDouble(Price10.Text) * (Quant10.Text != "0" ? Convert.ToDouble(Quant10.Text) : 1);
            return result;
        }

        private String ReduceDouble(double value)
        {
            String result = Convert.ToString(Convert.ToDouble(Convert.ToInt32(value * 100)) / 100);
            return result;
        }

        private void CashStatus_Click(object sender, RoutedEventArgs e)
        {
            if (!sales.GetCashShiftStatus())
            {
                sales.OpenCashShift(); //add check and error message here in case
                CashStatus.Content = "Закрыть смену";
                CashSviftNumber.Text = Convert.ToString(sales.GetCashShiftNumber());
            }
            else
            {
                sales.CloseCashShift();//add check and error message here in case
                CashStatus.Content = "Открыть смену";
                CashSviftNumber.Text = "---";
            }
        }

        private void Quant_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text == "0")
                return;
            double result = ConvertToDouble(((TextBox)sender).Text);
            if (result == 0)
                ((TextBox)sender).Text = "1";
            CalculateGoods();
            _TotalSumm = CalculateGoods() + _TotalLitres * _FuelPrice;
            TotalSumm.Text = Convert.ToString(_TotalSumm);
        }

        private void GoodsQuantFieldChecker()
        {
            if (Check01.IsChecked == true)
            {
                Quant01.IsEnabled = true;
                if (Quant01.Text == "0")
                    Quant01.Text = "1";
            }else if (Check01.IsChecked == false)
            {
                Quant01.IsEnabled = false;
                Quant01.Text = "0";
            }
            if (Check02.IsChecked == true)
            {
                Quant02.IsEnabled = true;
                if (Quant02.Text == "0")
                    Quant02.Text = "1";
            }else if (Check02.IsChecked == false)
            {
                Quant02.IsEnabled = false;
                Quant02.Text = "0";
            }   
            if (Check03.IsChecked == true)
            {
                Quant03.IsEnabled = true;
                if (Quant03.Text == "0")
                    Quant03.Text = "1";
            }else if (Check03.IsChecked == false)
            {
                Quant03.IsEnabled = false;
                Quant03.Text = "0";
            }    
            if (Check04.IsChecked == true)
            {
                Quant04.IsEnabled = true;
                if (Quant04.Text == "0")
                    Quant04.Text = "1";
            }else if (Check04.IsChecked == false)
            {
                Quant04.IsEnabled = false;
                Quant04.Text = "0";
            }    
            if (Check05.IsChecked == true)
            {
                Quant05.IsEnabled = true;
                if (Quant05.Text == "0")
                    Quant05.Text = "1";
            }else if (Check05.IsChecked == false)
            {
                Quant05.IsEnabled = false;
                Quant05.Text = "0";
            }    
            if (Check06.IsChecked == true)
            {
                Quant06.IsEnabled = true;
                if (Quant06.Text == "0")
                    Quant06.Text = "1";
            }else if (Check06.IsChecked == false)
            {
                Quant06.IsEnabled = false;
                Quant06.Text = "0";
            }    
            if (Check07.IsChecked == true)
            {
                Quant07.IsEnabled = true;
                if (Quant07.Text == "0")
                    Quant07.Text = "1";
            }else if (Check07.IsChecked == false)
            {
                Quant07.IsEnabled = false;
                Quant07.Text = "0";
            }    
            if (Check08.IsChecked == true)
            {
                Quant08.IsEnabled = true;
                if (Quant08.Text == "0")
                    Quant08.Text = "1";
            }else if (Check08.IsChecked == false)
            {
                Quant08.IsEnabled = false;
                Quant08.Text = "0";
            }    
            if (Check09.IsChecked == true)
            {
                Quant09.IsEnabled = true;
                if (Quant09.Text == "0")
                    Quant09.Text = "1";
            }else if (Check09.IsChecked == false)
            {
                Quant09.IsEnabled = false;
                Quant09.Text = "0";
            }
            if (Check10.IsChecked == true)
            {
                Quant01.IsEnabled = true;
                if (Quant10.Text == "0")
                    Quant10.Text = "1";
            }else if (Check10.IsChecked == false)
            {
                Quant10.IsEnabled = false;
                Quant10.Text = "0";
            }
        }
    }
}
