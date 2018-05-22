using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.SellTests
{

    [TestClass]
    public class buyProductsTest
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
        public void simpleTranscation()
        {
            User aviad = us.startSession();
            Assert.IsNotNull(aviad);
            int storeId= ss.createStore("abowim", zahi);
            Store store = storeArchive.getInstance().getStore(storeId);
            Assert.IsNotNull(store);
            Assert.IsTrue(us.register(aviad, "aviad", "123456")>-1);
            Assert.IsTrue(us.login(aviad,"aviad", "123456")>-1);
            int colaId= ss.addProductInStore("cola", 3.2, 10, zahi, storeId, "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(colaId);
            Assert.IsNotNull(pis);
            int saleId = ss.addSaleToStore(zahi, storeId, pis.getProductInStoreId(), 1, 8, DateTime.Now.AddDays(10).ToString());
            LinkedList<Sale> sales = ses.viewSalesByProductInStoreId(pis.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(ses.addProductToCart(aviad, sale.SaleId, 2)>0);
            LinkedList<UserCart> sc = ses.viewCart(aviad);
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.First.Value.getSaleId() == saleId);
            Assert.IsTrue(ses.buyProducts(aviad,"1234",""));
        }


        [TestMethod]
        public void TransactionWithAnEmptyCart()
        {
            User aviad = us.startSession();
            Assert.IsTrue(ses.buyProducts(aviad, "1234", ""));
        }

        //with a guest user
        [TestMethod]
        public void TransactionContaining2DifferentStores()
        {
            User aviad = us.startSession();
            User vadim = us.startSession();
            Assert.IsTrue(us.register(vadim, "vadim", "123456")>-1);
            Assert.IsTrue(us.login(vadim,"vadim", "123456")>-1);

            Assert.IsNotNull(aviad);
            int storeId1= ss.createStore("abowim", zahi);
            int storeId2 = ss.createStore("Russian liquor", vadim);
            int pis = ss.addProductInStore("cola", 3.2, 10, zahi, storeId1,"Drinks");
            int pis2 = ss.addProductInStore("vodka", 3.2, 10, vadim, storeId2,"Liquor");
            Assert.IsNotNull(pis);
            Assert.IsNotNull(pis2);
            int saleId = ss.addSaleToStore(zahi, storeId1, pis, 1, 8, DateTime.Now.AddDays(10).ToString());
            int saleId2 = ss.addSaleToStore(vadim, storeId2, pis2 ,1, 8, DateTime.Now.AddDays(10).ToString());

            LinkedList<Sale> sales = ses.viewSalesByProductInStoreId(pis);
            LinkedList<Sale> sales2 = ses.viewSalesByProductInStoreId(pis2);
            Assert.IsTrue(sales.Count == 1);
            Assert.IsTrue(sales2.Count == 1);

            Sale sale = sales.First.Value;
            Sale sale2 = sales2.First.Value;

            Assert.IsTrue(ses.addProductToCart(aviad, sale.SaleId, 1)>0);
            Assert.IsTrue(ses.addProductToCart(aviad, sale2.SaleId, 2)>0);
            LinkedList<UserCart> sc = ses.viewCart(aviad);
            Assert.IsTrue(sc.Count == 2);
            Assert.IsTrue(ses.buyProducts(aviad, "1234", ""));
        }

        [TestMethod]
        public void TransactionWith2Sales()
        {
            User aviad = us.startSession();
            Assert.IsNotNull(aviad);
            int store = ss.createStore("abowim", zahi);
            Assert.IsNotNull(store);
            Assert.IsTrue(us.register(aviad, "aviad", "123456")>-1);
            Assert.IsTrue(us.login(aviad, "aviad", "123456")>-1);
            int pis = ss.addProductInStore("cola", 3.2, 10, zahi, store,"Drinks");
            Assert.IsNotNull(pis);
            int saleId = ss.addSaleToStore(zahi, store, pis, 1, 6, DateTime.Now.AddDays(10).ToString());
            int saleId2 = ss.addSaleToStore(zahi, store, pis, 1, 3, DateTime.Now.AddDays(9).ToString());
            LinkedList<Sale> sales = ses.viewSalesByProductInStoreId(pis);
            Assert.IsTrue(sales.Count == 2);
            Sale sale = sales.First.Value;
            Sale sale2 = sales.First.Next.Value;
            Assert.IsTrue(ses.addProductToCart(aviad, sale.SaleId, 2)>0);
            Assert.IsTrue(ses.addProductToCart(aviad, sale2.SaleId, 1)>0);

            LinkedList<UserCart> sc = ses.viewCart(aviad);
            Assert.IsTrue(sc.Count == 2);
            Assert.IsTrue(ses.buyProducts(aviad, "1234", ""));
        }


        [TestMethod]
        public void TransactionWithExpiredDate()
        {
            User aviad = us.startSession();
            Assert.IsNotNull(aviad);
            int store = ss.createStore("abowim", zahi);
            Assert.IsNotNull(store);
            Assert.IsTrue(us.register(aviad, "aviad", "123456")>-1);
            Assert.IsTrue(us.login(aviad, "aviad", "123456")>-1);
            int pis = ss.addProductInStore("cola", 3.2, 10, zahi, store,"Drinks");
            Assert.IsNotNull(pis);
            int saleId = ss.addSaleToStore(zahi, store, pis, 1, 8, DateTime.Now.AddDays(-1).ToString());
            LinkedList<Sale> sales = ses.viewSalesByProductInStoreId(pis);
            Assert.IsNull(sales);
            
        }

        [TestMethod]
        public void TransactionWithExceededAmountOfStock()
        {
            //in order to bypass the cart's defenses we use dual user
            User aviad = us.startSession();
            User vadim = us.startSession();

            Assert.IsNotNull(aviad);
            int store = ss.createStore("abowim", zahi);
            Assert.IsNotNull(store);
            int pis = ss.addProductInStore("cola", 3.2, 10, zahi, store,"Drinks");
            Assert.IsNotNull(pis);
            int saleId = ss.addSaleToStore(zahi, store, pis, 1, 6, DateTime.Now.AddDays(10).ToString());
            int saleId2 = ss.addSaleToStore(zahi, store, pis, 1, 4, DateTime.Now.AddDays(8).ToString());

            LinkedList<Sale> sales = ses.viewSalesByProductInStoreId(pis);
            Assert.IsTrue(sales.Count == 2);

            Sale sale = sales.First.Value;
            Sale sale2 = sales.First.Next.Value;

            Assert.IsTrue(ses.addProductToCart(aviad, sale.SaleId, 5)>0);
            LinkedList<UserCart> sc = ses.viewCart(aviad);
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.Count == 1);

            Assert.IsTrue(ses.buyProducts(aviad, "1234", ""));
        }

        [TestMethod]
        public void TransactionWithExceededAmountOfSale()
        {
            //in order to bypass the cart's defenses we use dual user
            User aviad = us.startSession();
            User vadim = us.startSession();

            Assert.IsNotNull(aviad);
            int store = ss.createStore("abowim", zahi);
            Assert.IsNotNull(store);
            int pis = ss.addProductInStore("cola", 3.2, 10, zahi, store,"Drinks");
            Assert.IsNotNull(pis);
            int saleId = ss.addSaleToStore(zahi, store, pis, 1, 6, DateTime.Now.AddDays(10).ToString());

            LinkedList<Sale> sales = ses.viewSalesByProductInStoreId(pis);
            Assert.IsTrue(sales.Count == 1);

            Sale sale = sales.First.Value;

            Assert.IsTrue(ses.addProductToCart(aviad, sale.SaleId, 4)>0);
            Assert.IsTrue(ses.addProductToCart(vadim, sale.SaleId, 3)>0);
            LinkedList<UserCart> sc = ses.viewCart(aviad);
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.Count == 1);

            Assert.IsTrue(ses.buyProducts(aviad, "1234", ""));
        }

        [TestMethod]
        public void TransactionWithChangedSale()
        {
            //in order to bypass the cart's defenses we use dual user
            User aviad = us.startSession();
            User vadim = us.startSession();

            Assert.IsNotNull(aviad);
            int store = ss.createStore("abowim", zahi);
            int pis = ss.addProductInStore("cola", 3.2, 10, zahi, store,"Drinks");
            int saleId = ss.addSaleToStore(zahi, store, pis, 1, 8, DateTime.Now.AddDays(10).ToString());

            LinkedList<Sale> sales = ses.viewSalesByProductInStoreId(pis);
            Assert.IsTrue(sales.Count == 1);
            
            Sale sale = sales.First.Value;
            Assert.IsTrue(ss.editSale(zahi,store,saleId,6, DateTime.Now.AddDays(10).ToString())>-1);
            Assert.IsTrue(ses.addProductToCart(aviad, sale.SaleId, 4)>0);
            Assert.IsTrue(ses.addProductToCart(vadim, sale.SaleId, 3)>0);
            LinkedList<UserCart> sc = ses.viewCart(aviad);
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.Count == 1);

            Assert.IsTrue(ses.buyProducts(aviad, "1234", ""));
        }

        [TestMethod]
        public void TransactionWithChangedCart()
        {

            User aviad = us.startSession();
            Assert.IsNotNull(aviad);
            int store = ss.createStore("abowim", zahi);
            Assert.IsNotNull(store);
            Assert.IsTrue(us.register(aviad, "aviad", "123456")>-1);
            Assert.IsTrue(us.login(aviad, "aviad", "123456")>-1);
            int pis = ss.addProductInStore("cola", 3.2, 10, zahi, store,"Drinks");
            Assert.IsNotNull(pis);
            int saleId = ss.addSaleToStore(zahi, store, pis, 1, 8, DateTime.Now.AddDays(10).ToString());
            LinkedList<Sale> sales = ses.viewSalesByProductInStoreId(pis);
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(ses.addProductToCart(aviad, sale.SaleId, 2)>-1);
            Assert.IsTrue(ses.removeFromCart(aviad, sale.SaleId)>-1);
        }

    }
}
