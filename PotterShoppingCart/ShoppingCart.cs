using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;

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
            ICalculator calculator = ShoppingFactory.GetCalculator(booksCnt);

            result = calculator.Calc(orders);
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

    public interface ICalculator
    {
        int Calc(List<Order> orders);
    }

    public class CalculatorWithNoDiscount : ICalculator
    {
        public int Calc(List<Order> orders)
        {
            return orders.Sum(x => x.Book.Price * x.Qty);
        }
    }

    public class CalculatorWithDiscount95 : ICalculator
    {
        public int Calc(List<Order> orders)
        {
            return (int) Math.Floor(orders.Sum(x => x.Book.Price * x.Qty) * 0.95m);
        }
    }

    public class CalculatorWithDiscount90 : ICalculator
    {
        public int Calc(List<Order> orders)
        {
            return (int)Math.Floor(orders.Sum(x => x.Book.Price * x.Qty) * 0.9m);
        }
    }

    public class CalculatorWithDiscount80 : ICalculator
    {
        public int Calc(List<Order> orders)
        {
            return (int)Math.Floor(orders.Sum(x => x.Book.Price * x.Qty) * 0.8m);
        }
    }

    public class CalculatorWithDiscount75 : ICalculator
    {
        public int Calc(List<Order> orders)
        {
            return (int)Math.Floor(orders.Sum(x => x.Book.Price * x.Qty) * 0.75m);
        }
    }

    public static class ShoppingFactory
    {
        public static ICalculator GetCalculator(int booksCnt)
        {
            ICalculator result = null;
            switch (booksCnt)
            {
                case 5:
                    result = new CalculatorWithDiscount75();
                    break;
                case 4:
                    result = new CalculatorWithDiscount80();
                    break;
                case 3:
                    result = new CalculatorWithDiscount90();
                    break;
                case 2 :
                    result = new CalculatorWithDiscount95();
                    break;
                default :
                    result = new CalculatorWithNoDiscount();
                    break;
            }
            return result;
        }
    }

}