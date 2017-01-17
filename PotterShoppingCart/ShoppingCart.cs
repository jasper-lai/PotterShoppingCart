﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Channels;

namespace PotterShoppingCart
{
    public class ShoppingCart
    {

        //利用 Dictionary 儲存各種不同集數購買數量的折扣,
        private readonly Dictionary<int, Discount> _discounts = new Dictionary<int, Discount>()
        {
            //Amt = 售價 * 數量 * 折扣率
            {1, new Discount() {Qty = 1, DiscountRate = 1m, Amt = 100 * 1} },
            {2, new Discount() {Qty = 2, DiscountRate = 0.95m, Amt = (int) Math.Floor(100 * 2 * 0.95m)} },
            {3, new Discount() {Qty = 3, DiscountRate = 0.90m, Amt = (int) Math.Floor(100 * 3 * 0.90m)} },
            {4, new Discount() {Qty = 4, DiscountRate = 0.80m, Amt = (int) Math.Floor(100 * 4 * 0.80m)} },
            {5, new Discount() {Qty = 5, DiscountRate = 0.75m, Amt = (int) Math.Floor(100 * 5 * 0.75m)} },
        };

        public ShoppingCart()
        {
        }

        /// <summary>
        /// 進行資料配對
        /// 例如1: 第1集買1本, 應回傳 [1]
        /// 例如2: 第1, 2集各買1本, 應回傳 [2]
        /// 例如3: 第1, 2, 3集各買1本, 應回傳 [3]
        /// 例如4: 第1, 2, 3, 4集各買1本, 應回傳 [4]
        /// 例如5: 第1, 2, 3, 4, 5集各買1本, 應回傳 [5]
        /// 例如6: 第1集買1本, 第2集買1本, 第3集買2本, 應回傳 [3, 1]
        /// 例如7: 第1集買1本, 第2集買2本, 第3集買2本, 應回傳 [3, 2]
        /// </summary>
        /// <param name="orders"></param>
        /// <returns>配對結果</returns>
        /// <remarks>
        /// 將不同集數的數量作統整, 以取得一系列(不同集數) 的數量配對 (折扣方案是以系列為主, 而每個系列裡的集數必須不同).
        /// 例如: 
        /// 方案1: 數量=2, 折扣=0.95
        /// 方案2: 數量=3, 折扣=0.90
        /// 方案3: 數量=4, 折扣=0.80
        /// 方案4: 數量=5, 折扣=0.75
        /// </remarks>
        private List<int> MakePairs(List<Order> orders)
        {
            List<int> results = new List<int>();

            //Clone一份原來的訂單資料, 以避免改到原來的訂單資料
            //[1,1,2]  [1,2,2]
            //var bookCnts = orders.Select(x => new {x.Book.VolNo, x.Qty});
            List<int> bookCnts = new List<int>();
            foreach (var order in orders)
            {
                bookCnts.Add(order.Qty);
            }

            while (true)
            {
                int i = 0;
                int currentCnt = 0;
                int remainCnt = 0;
                foreach (var order in orders)
                {
                    //因為經過一輪彙整後, bookCnts 的元素內容, 有可能成為 0
                    if (bookCnts[i] > 0)
                    {
                        currentCnt = currentCnt + 1;
                        remainCnt = remainCnt + bookCnts[i] - 1;
                        bookCnts[i] = bookCnts[i] - 1;
                    }
                    i++;
                }

                results.Add(currentCnt);

                //檢查剩下只有 1 個元素是大於 0 的狀況, 視為結束
                if (bookCnts.Count(x => x > 0) == 1)
                {
                    results.Add(bookCnts.Single(x => x > 0));
                    break;
                }

                //如果沒有書了, 也視為結束
                if (remainCnt == 0)
                {
                    break;
                }
            }

            Debug.WriteLine("配對結果: [" + string.Join(", ", results) + "]");
            return results;
        }

        private bool CheckFinalIsSameVolumn(List<Order> orders)
        {
            bool result = false;

            //檢查配對結果的最後一個數字, 是否為 "同一集"
            var tmpOrders = orders.OrderByDescending(x => x.Qty).ToList();

            //最大值 - 次大值 大於或等於 2, 檢查配對結果的最後一個數字, 是 "同一集" --> 不打折
            if ((tmpOrders[0].Qty - tmpOrders[1].Qty) >= 2)
            {
                result = true;
            }

            return result;
        }

        public int CalcAmt(List<Order> orders)
        {
            int result = 0;

            //如果只有1筆訂單項目, 代表沒有打折的可能, 直接回傳結果
            if (orders.Count == 1)
            {
                result = orders.Sum(x => x.Book.Price * x.Qty);
                return result;
            }

            //如果大於1筆訂單項目, 才有打折配對的必要
            List<int> pairs = MakePairs(orders);
            bool isSameVolumn = CheckFinalIsSameVolumn(orders);
            int i = 0;
            int pairsCnt = pairs.Count;
            foreach (var pair in pairs)
            {
                i++;
                //處理最後一個配對值的狀況
                if (i == pairsCnt && isSameVolumn)
                {
                    result += 100 * pair;
                }
                else
                {
                    Discount discount = null;
                    //查表取值 (_discounts)
                    if (!_discounts.TryGetValue(pair, out discount))
                    {
                        throw new ArgumentException("購買數量不在設定的範圍 (1 .. 5)");
                    }
                    result += discount.Amt;
                }
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

    public interface ICalculator
    {
        int Calc(List<Order> orders);
    }

    public class Discount
    {
        //數量
        public int Qty { get; set; }
        //折扣率
        public decimal DiscountRate { get; set; }
        //計算後的金額
        public int Amt { get; set; }
    }

}