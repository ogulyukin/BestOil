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
            GoodsList list = new();
            fuelList = new();
            list = GoodsLoader.GetGoodsList();
            SetGoodsData(list);
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
            sales.OpenCheque();
            sales.RegisterChequeItem(((TextBlock)SelectFuel.SelectedItem).Text, _FuelPrice, _TotalLitres, _FuelPrice * _TotalLitres);
            if (Check01.IsChecked == true)
            {
                sales.RegisterChequeItem(Goods01.Text, Convert.ToDouble(Price01.Text), 1, Convert.ToDouble(Price01.Text));
                Check01.IsChecked = false;
            }                
            if (Check02.IsChecked == true)
            {
                sales.RegisterChequeItem(Goods02.Text, Convert.ToDouble(Price02.Text), 1, Convert.ToDouble(Price02.Text));
                Check02.IsChecked = false;
            }                
            if (Check03.IsChecked == true)
            {
                sales.RegisterChequeItem(Goods03.Text, Convert.ToDouble(Price03.Text), 1, Convert.ToDouble(Price03.Text));
                Check03.IsChecked = false;
            }                
            if (Check04.IsChecked == true)
            {
                sales.RegisterChequeItem(Goods04.Text, Convert.ToDouble(Price04.Text), 1, Convert.ToDouble(Price04.Text));
                Check04.IsChecked = false;
            }                
            if (Check05.IsChecked == true)
            {
                sales.RegisterChequeItem(Goods05.Text, Convert.ToDouble(Price05.Text), 1, Convert.ToDouble(Price05.Text));
                Check05.IsChecked = false;
            }
            if (Check06.IsChecked == true)
            {
                sales.RegisterChequeItem(Goods06.Text, Convert.ToDouble(Price06.Text), 1, Convert.ToDouble(Price06.Text));
                Check06.IsChecked = false;
            }
            if (Check07.IsChecked == true)
            {
                sales.RegisterChequeItem(Goods07.Text, Convert.ToDouble(Price07.Text), 1, Convert.ToDouble(Price07.Text));
                Check07.IsChecked = false;
            }
            if (Check08.IsChecked == true)
            {
                sales.RegisterChequeItem(Goods08.Text, Convert.ToDouble(Price08.Text), 1, Convert.ToDouble(Price08.Text));
                Check08.IsChecked = false;
            }
            if (Check09.IsChecked == true)
            {
                sales.RegisterChequeItem(Goods09.Text, Convert.ToDouble(Price09.Text), 1, Convert.ToDouble(Price09.Text));
                Check09.IsChecked = false;
            }
            if (Check10.IsChecked == true)
            {
                sales.RegisterChequeItem(Goods10.Text, Convert.ToDouble(Price10.Text), 1, Convert.ToDouble(Price10.Text));
                Check10.IsChecked = false;
            }
            sales.CloseCheque();
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
