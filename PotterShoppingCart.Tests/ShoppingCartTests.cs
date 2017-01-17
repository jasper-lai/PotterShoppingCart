using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PotterShoppingCart;

namespace PotterShoppingCart.Tests
{

    [TestClass]
    public class ShoppingCartTests
    {

        [TestMethod]
        public void Scenario_1_第一集買了一本_價格應為100元()
        {
            //arrange
            List<Order> orders = new List<Order>()
            {
                new Order { Book = new Book() {VolNo = 1}, Qty = 1}
            };

            //act
            var target = new ShoppingCart();
            int actual = target.CalcAmt(orders);

            //assert
            int expected = 100;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void Scenario_2_第一集買了一本_第二集也買了一本_價格應為190()
        {
            //arrange
            List<Order> orders = new List<Order>()
            {
                new Order { Book = new Book() {VolNo = 1}, Qty = 1},
                new Order { Book = new Book() {VolNo = 2}, Qty = 1}
            };

            //act
            var target = new ShoppingCart();
            int actual = target.CalcAmt(orders);

            //assert
            int expected = 190;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void Scenario_3_一二三集各買了一本_價格應為270()
        {
            //arrange
            List<Order> orders = new List<Order>()
            {
                new Order { Book = new Book() {VolNo = 1}, Qty = 1},
                new Order { Book = new Book() {VolNo = 2}, Qty = 1},
                new Order { Book = new Book() {VolNo = 3}, Qty = 1},
            };

            //act
            var target = new ShoppingCart();
            int actual = target.CalcAmt(orders);

            //assert
            int expected = 270;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void Scenario_4_一二三四集各買了一本_價格應為320()
        {
            //arrange
            List<Order> orders = new List<Order>()
            {
                new Order { Book = new Book() {VolNo = 1}, Qty = 1},
                new Order { Book = new Book() {VolNo = 2}, Qty = 1},
                new Order { Book = new Book() {VolNo = 3}, Qty = 1},
                new Order { Book = new Book() {VolNo = 4}, Qty = 1},
            };

            //act
            var target = new ShoppingCart();
            int actual = target.CalcAmt(orders);

            //assert
            int expected = 320;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void Scenario_5_一二三四五集各買了一本_價格應為375()
        {
            //arrange
            List<Order> orders = new List<Order>()
            {
                new Order { Book = new Book() {VolNo = 1}, Qty = 1},
                new Order { Book = new Book() {VolNo = 2}, Qty = 1},
                new Order { Book = new Book() {VolNo = 3}, Qty = 1},
                new Order { Book = new Book() {VolNo = 4}, Qty = 1},
                new Order { Book = new Book() {VolNo = 5}, Qty = 1},
            };

            //act
            var target = new ShoppingCart();
            int actual = target.CalcAmt(orders);

            //assert
            int expected = 375;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void Scenario_6_一二集各買了一本_第三集買了兩本_價格應為_370()
        {
            //arrange
            List<Order> orders = new List<Order>()
            {
                new Order { Book = new Book() {VolNo = 1}, Qty = 1},
                new Order { Book = new Book() {VolNo = 2}, Qty = 1},
                new Order { Book = new Book() {VolNo = 3}, Qty = 2},
            };

            //act
            var target = new ShoppingCart();
            int actual = target.CalcAmt(orders);

            //assert
            int expected = 370;     // 100*3*0.9 + 100
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void Scenario_7_第一集買了一本_第二三集各買了兩本_價格應為460()
        {
            //arrange
            List<Order> orders = new List<Order>()
            {
                new Order { Book = new Book() {VolNo = 1}, Qty = 1},
                new Order { Book = new Book() {VolNo = 2}, Qty = 2},
                new Order { Book = new Book() {VolNo = 3}, Qty = 2},
            };

            //act
            var target = new ShoppingCart();
            int actual = target.CalcAmt(orders);

            //assert
            int expected = 460;     // 100*3*0.9 + 100*2*0.95
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// Scenario_8_第一集買了一本_第二集買了兩本_第三集買了四本_價格應為660
        /// </summary>
        /// <remarks>
        /// 自行新增的測試案例: 假設最後剩下的數量, 是同一集, 那就不打折
        /// 例如: 第1集買了2本 ==> 100*2 = 200
        /// 例如: 第1集買了1本, 第2集買了3本 ==> 100*2*.95 + 100*2 = 390
        /// 例如: 第1集買了1本, 第2集買了2本, 第3集買了4本 ==> 100*3*.9 + 100*2*.95 + 100*2 = 
        /// </remarks>
        [TestMethod]
        public void Scenario_8_第一集買了一本_第二集買了兩本_第三集買了四本_價格應為660()
        {
            //arrange
            List<Order> orders = new List<Order>()
            {
                new Order { Book = new Book() {VolNo = 1}, Qty = 1},
                new Order { Book = new Book() {VolNo = 2}, Qty = 2},
                new Order { Book = new Book() {VolNo = 3}, Qty = 4},
            };

            //act
            var target = new ShoppingCart();
            int actual = target.CalcAmt(orders);

            //assert
            int expected = 660;     // 100*3*0.9 + 100*2*0.95 + 100*2*1 = 660
            Assert.AreEqual(expected, actual);

        }


    }

}
