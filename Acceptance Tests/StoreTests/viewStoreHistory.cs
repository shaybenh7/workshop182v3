using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{

    [TestClass]
    public class viewStoreHistory
    {
        private userServices us;
        private storeServices ss;
        private sellServices ses;
        private User zahi;

        [TestInitialize]
        public void init()
        {
            ProductArchive.restartInstance();
            SalesArchive.restartInstance();
            storeArchive.restartInstance();
            UserArchive.restartInstance();
            UserCartsArchive.restartInstance();
            BuyHistoryArchive.restartInstance();
            CouponsArchive.restartInstance();
            DiscountsArchive.restartInstance();
            RaffleSalesArchive.restartInstance();
            StorePremissionsArchive.restartInstance();
            us = userServices.getInstance();
            ss = storeServices.getInstance();
            ses = sellServices.getInstance();
            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");
        }



        //User is creating a store, adding products
        //another user is buying the products from him
        [TestMethod]
        public void simpleViewHistory()
        {
            User aviad = us.startSession();
            Assert.IsNotNull(aviad);
            int store = ss.createStore("abowim", zahi);
            Assert.IsNotNull(store);
            Assert.IsTrue(us.register(aviad, "aviad", "123456")>-1);
            Assert.IsTrue(us.login(aviad, "aviad", "123456")>-1);
            int pis = ss.addProductInStore("cola", 3.2, 10, zahi, store,"drinks");
            int saleId = ss.addSaleToStore(zahi, store, pis, 1, 8, DateTime.Now.AddDays(10).ToString());
            LinkedList<Sale> sales = ses.viewSalesByProductInStoreId(pis);
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(ses.addProductToCart(aviad, sale.SaleId, 2)>-1);
            LinkedList<UserCart> sc = ses.viewCart(aviad);
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.First.Value.getSaleId() == saleId);
            Assert.IsTrue(ses.buyProducts(aviad, "1234", ""));
            LinkedList<Purchase> historyList = ss.viewStoreHistory(zahi, store);
            Assert.IsTrue(historyList.Count == 1);
            Assert.IsTrue(historyList.First.Value.ProductId == ProductArchive.getInstance().getProductInStore(pis).getProduct().getProductId());
            Assert.IsTrue(historyList.First.Value.Amount == 2);
        }


        [TestMethod]
        public void emptyViewHistory()
        {
            int store = ss.createStore("abowim", zahi);
            LinkedList<Purchase> historyList = ss.viewStoreHistory(zahi, store);
            Assert.IsTrue(historyList.Count == 0);
        }

        //with a guest user
        [TestMethod]
        public void viewHistoryOf2Stores()
        {
            User aviad = us.startSession();
            Assert.IsNotNull(aviad);
            int store = ss.createStore("abowim", zahi);
            Assert.IsNotNull(store);
            Assert.IsTrue(us.register(aviad, "aviad", "123456")>-1);
            Assert.IsTrue(us.login(aviad, "aviad", "123456")>-1);
            int pis = ss.addProductInStore("cola", 3.2, 10, zahi, store,"drinks");
            Assert.IsNotNull(pis);
            int saleId = ss.addSaleToStore(zahi, store, pis, 1, 8, DateTime.Now.AddDays(10).ToString());
            LinkedList<Sale> sales = ses.viewSalesByProductInStoreId(pis);
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(ses.addProductToCart(aviad, sale.SaleId, 2)>-1);
            LinkedList<UserCart> sc = ses.viewCart(aviad);
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.First.Value.getSaleId() == saleId);
            Assert.IsTrue(ses.buyProducts(aviad, "1234", ""));
            LinkedList<Purchase> historyList = ss.viewStoreHistory(zahi, store);
            Assert.IsTrue(historyList.Count == 1);
            Assert.IsTrue(historyList.First.Value.ProductId == ProductArchive.getInstance().getProductInStore(pis).getProduct().getProductId());
            Assert.IsTrue(historyList.First.Value.Amount == 2);



            int store2 = ss.createStore("abowim2", zahi);
            Assert.IsNotNull(store2);
            int pis2 = ss.addProductInStore("cola2", 3.2, 10, zahi, store2,"drinks");
            Assert.IsNotNull(pis2);
            int saleId2 = ss.addSaleToStore(zahi, store2, pis2, 1, 8, DateTime.Now.AddDays(10).ToString());
            LinkedList<Sale> sales2 = ses.viewSalesByProductInStoreId(pis2);
            Assert.IsTrue(sales2.Count == 1);
            Sale sale2 = sales2.First.Value;
            Assert.IsTrue(ses.addProductToCart(aviad, sale2.SaleId, 2)>-1);
            LinkedList<UserCart> sc2 = ses.viewCart(aviad);
            Assert.IsTrue(sc2.Count == 2);
            Assert.IsTrue(ses.buyProducts(aviad, "1234", ""));
            LinkedList<Purchase> historyList2 = ss.viewStoreHistory(zahi, store2);
            Assert.IsTrue(historyList2.Count == 1);
            Assert.IsTrue(historyList2.First.Value.ProductId == ProductArchive.getInstance().getProductInStore(pis2).getProduct().getProductId());
            Assert.IsTrue(historyList2.First.Value.Amount == 2);
        }

        [TestMethod]
        public void viewHistoryOf2Sales()
        {
            User aviad = us.startSession();
            Assert.IsNotNull(aviad);
            int store = ss.createStore("abowim", zahi);
            Assert.IsNotNull(store);
            Assert.IsTrue(us.register(aviad, "aviad", "123456")>-1);
            Assert.IsTrue(us.login(aviad, "aviad", "123456")>-1);
            int pis = ss.addProductInStore("cola", 3.2, 10, zahi, store,"drinks");
            Assert.IsNotNull(pis);
            int saleId = ss.addSaleToStore(zahi, store, pis, 1, 4, DateTime.Now.AddDays(10).ToString());
            int saleId2 = ss.addSaleToStore(zahi, store, pis, 1, 4, DateTime.Now.AddDays(10).ToString());
            LinkedList<Sale> sales = ses.viewSalesByProductInStoreId(pis);
            Assert.AreEqual(1,sales.Count);
            Sale sale = sales.First.Value;

            Assert.IsTrue(ses.addProductToCart(aviad, sale.SaleId, 2)>-1);

            LinkedList<UserCart> sc = ses.viewCart(aviad);
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(ses.buyProducts(aviad, "1234", ""));
            LinkedList<Purchase> historyList = ss.viewStoreHistory(zahi, store);
            Assert.IsTrue(historyList.Count == 1);
        }

        public void viewHistoryOf2SalesWithDifferentUsers()
        {
            User aviad = us.startSession();
            User vadim = us.startSession();
            Assert.IsNotNull(aviad);
            int store = ss.createStore("abowim", zahi);
            Assert.IsNotNull(store);
            int pis = ss.addProductInStore("cola", 3.2, 10, zahi, store,"drinks");
            Assert.IsNotNull(pis);
            int saleId = ss.addSaleToStore(zahi, store, pis, 1, 4, DateTime.Now.AddDays(10).ToString());
            LinkedList<Sale> sales = ses.viewSalesByProductInStoreId(pis);
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;

            Assert.IsTrue(ses.addProductToCart(aviad, sale.SaleId, 2)>-1);
            Assert.IsTrue(ses.addProductToCart(vadim, sale.SaleId, 4)>-1);


            Assert.IsTrue(ses.buyProducts(aviad, "1234", ""));
            Assert.IsTrue(ses.buyProducts(vadim, "1234", ""));
            LinkedList<Purchase> historyList = ss.viewStoreHistory(zahi, store);
            Assert.IsTrue(historyList.Count == 2);
        }


        [TestMethod]
        public void viewHistoryOfAFailedTransaction()
        {
            User aviad = us.startSession();
            Assert.IsNotNull(aviad);
            int store = ss.createStore("abowim", zahi);
            Assert.IsNotNull(store);
            Assert.IsTrue(us.register(aviad, "aviad", "123456")>-1);
            Assert.IsTrue(us.login(aviad, "aviad", "123456")>-1);
            int pis = ss.addProductInStore("cola", 3.2, 10, zahi, store,"drinks");
            Assert.IsNotNull(pis);
            int saleId = ss.addSaleToStore(zahi, store, pis, 1, 8, DateTime.Now.AddDays(10).ToString());
            LinkedList<Sale> sales = ses.viewSalesByProductInStoreId(pis);
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            ses.addProductToCart(aviad, sale.SaleId, 100);
            LinkedList<Purchase> historyList = ss.viewStoreHistory(zahi, store);
            Assert.IsTrue(historyList.Count == 0);
            
        }


    }
}
