﻿using System;
using wsep182.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class SaleUnitTests
    {
        Sale sale;
        ProductManager productArchive;
        DiscountsManager discountsArchive;
        Product milk;
        Store store;
        ProductInStore milkInStore;
        [TestInitialize]
        public void init()
        {
            ProductManager.restartInstance();
            DiscountsManager.restartInstance();
            productArchive = ProductManager.getInstance();
            discountsArchive = DiscountsManager.getInstance();
            milk = productArchive.addProduct("milk");
            store = new Store(1, "halavi", new User("itamar", "123456"));
            milkInStore = productArchive.addProductInStore(milk, store, 50, 200);
        }

        [TestMethod]
        public void getSalePriceWithNoDiscounts()
        {
            double price = 200;
            int amount = 5;
            sale = new Sale(1, milkInStore.getProductInStoreId(), 1, 50, "20/5/2020");
            double check = sale.getPriceBeforeDiscount(amount);
            Assert.AreEqual(amount * price, check);

        }
        [TestMethod]
        public void getSalePriceWithDiscount()
        {
            int percentage = 50;
            List<int> lst = new List<int>();
            lst.Add(milkInStore.getProductInStoreId());
            discountsArchive.addNewDiscounts(1,lst,null, percentage, "20/6/2020","");
            double price = 200;
            int amount = 5;
            sale = new Sale(1, milkInStore.getProductInStoreId(), 1, 50, "20/5/2020");
            double check = sale.getPriceAfterDiscount(amount);
            double res = (price * amount) - ((((Double)(price * amount * percentage)) / 100));
            Assert.AreEqual(res, check);
        }
        [TestMethod]
        public void getSalePriceWithInvalidDiscount()
        {
            int percentage = 50;
            List<int> lst = new List<int>();
            lst.Add(milkInStore.getProductInStoreId());
            discountsArchive.addNewDiscounts(1,lst,null, percentage, "20/6/1990","");
            double price = 200;
            int amount = 5;
            sale = new Sale(1, milkInStore.getProductInStoreId(), 1, 50, "20/5/2020");
            double check = sale.getPriceAfterDiscount(amount);
            double res = amount * price;
            Assert.AreEqual(res, check);
        }

    }
}
