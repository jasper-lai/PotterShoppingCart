using System;
using System.Collections.Generic;
using System.Linq;

namespace PotterShoppingCart
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
        }

        public int CalcAmt(List<Order> orders)
        {
            int result = 0;
            int booksCnt = 0;   //總共買了幾本書

            booksCnt = orders.Sum(x => x.Qty);

            if (2 == booksCnt)
            {
                //2本不同系列, 打9.5折
                result = (int) Math.Floor(orders.Sum(x => x.Book.Price * x.Qty) * 0.95m) ;
            }
            else
            {
                //不打折的狀況下, 直接用 Linq 處理
                result = orders.Sum(x => x.Book.Price * x.Qty);
            }
            return result;
        }
    }

    public class Book
    {
        public string Name => "Potter";

        public int VolNo { get; set; }   //VolNo 第幾集

        public int Price => 100;
    }

    public class Order
    {
        public Book Book { get; set; }
        public int Qty { get; set; }
    }

}