using System;
using System.Collections.Generic;

namespace PotterShoppingCart
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
        }

        public int CalcAmt(List<Order> orders)
        {
            throw new NotImplementedException();
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