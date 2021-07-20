using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Caffe
{
    class GoodsLoader
    {
        public static GoodsList GetGoodsList()
        {
            GoodsList goodsList = new();
            try
            {
                using (FileStream fileStream = new("Goods.json", FileMode.Open))
                {
                    goodsList = JsonSerializer.DeserializeAsync<GoodsList>(fileStream).Result;
                }
            }
            catch
            {
                return null;
            }
            return goodsList;
        }
        public static void WriteJson()
        {
            GoodsList goodsList = new();
            Goods goods = new Goods("Сигареты в ассортименте", 150);
            goodsList.GList.Add(goods);
            goods.Name = "Жевательная Резинка в асортименте";
            goods.Price = 50;
            goodsList.GList.Add(goods);
            goods.Name = "Шоколад плитка в асортименте";
            goods.Price = 100;
            goodsList.GList.Add(goods);
            goods.Name = "Хотдог в асортименте";
            goods.Price = 1200;
            goodsList.GList.Add(goods);
            using (var file = new FileStream("Goods.json", FileMode.Truncate))
            {
                JsonSerializer.SerializeAsync(file, goodsList);
            }

        }
    }
}
