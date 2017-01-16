﻿using System;
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
    }

}