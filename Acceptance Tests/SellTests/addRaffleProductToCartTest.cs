using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.SellTests
{
    [TestClass]
    public class addRaffleProductToCartTest
    {
        private userServices us;
        private storeServices ss;
        private sellServices sellS;
        private User zahi, itamar, niv, admin, admin1; //admin,itamar logedin
        private Store store;//itamar owner , niv manneger
        ProductInStore cola, sprite;

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
            sellS = sellServices.getInstance();

            admin = us.startSession();
            us.register(admin, "admin", "123456");
            us.login(admin, "admin", "123456");

            admin1 = us.startSession();
            us.register(admin1, "admin1", "123456");

            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");

            itamar = us.startSession();
            us.register(itamar, "itamar", "123456");
            us.login(itamar, "itamar", "123456");
            int storeId=ss.createStore("Maria&Netta Inc.", itamar);

            store = storeArchive.getInstance().getStore(storeId);

            niv = us.startSession();
            us.register(niv, "niv", "123456");
            us.login(niv, "niv", "123456");

            ss.addStoreManager(storeId, "niv", itamar);

            int colaId = ss.addProductInStore("cola", 3.2, 10, itamar, storeId,"Drinks");
            cola = ProductArchive.getInstance().getProductInStore(colaId);
            int spriteId = ss.addProductInStore("sprite", 5.2, 100, itamar, storeId, "Drinks");
            sprite = ProductArchive.getInstance().getProductInStore(spriteId);
            ss.addSaleToStore(itamar, storeId, cola.getProductInStoreId(), 3, 1, DateTime.Now.AddMonths(10).ToString());
        }


        [TestMethod]
        public void simpleAddRaffleProductToCart()
        {
            us.login(zahi, "zahi", "123456");
            LinkedList<Sale> saleList = ss.viewSalesByStore(store.getStoreId());
            Assert.IsTrue(sellS.addRaffleProductToCart(zahi, saleList.First.Value.SaleId, 1)>0);
        }
        [TestMethod]
        public void AddProductToCartOfferToBig()
        {
            us.login(zahi, "zahi", "123456");
            LinkedList<Sale> saleList = ss.viewSalesByStore(store.getStoreId());
            Assert.IsFalse(sellS.addRaffleProductToCart(zahi, saleList.First.Value.SaleId, 8)>0);
            Assert.IsFalse(sellS.addRaffleProductToCart(niv, saleList.First.Value.SaleId, 12)>0);
        }
        [TestMethod]
        public void AddProductToCartAfterOffering()
        {
            us.login(zahi, "zahi", "123456");
            LinkedList<Sale> saleList = ss.viewSalesByStore(store.getStoreId());
            int temp2=sellS.addRaffleProductToCart(zahi, saleList.First.Value.SaleId, 8);
            Assert.IsFalse(temp2>0);
            int temp = sellS.addRaffleProductToCart(zahi, saleList.First.Value.SaleId, 1);
            Assert.IsTrue(temp>0);
            Assert.IsFalse(sellS.addRaffleProductToCart(zahi, saleList.First.Value.SaleId, 2.2)>0);
        }
        [TestMethod]
        public void AddProductToCartNull()
        {
            us.login(zahi, "zahi", "123456");
            LinkedList<Sale> saleList = ss.viewSalesByStore(store.getStoreId());
            Assert.IsFalse(sellS.addRaffleProductToCart(null, saleList.First.Value.SaleId, 1)>0);
            Assert.IsFalse(sellS.addRaffleProductToCart(zahi, saleList.First.Value.SaleId, -31)>0);
        }
        [TestMethod]
        public void AddProductToCartZero()
        {
            us.login(zahi, "zahi", "123456");
            LinkedList<Sale> saleList = ss.viewSalesByStore(store.getStoreId());
            Assert.IsFalse(sellS.addRaffleProductToCart(zahi, saleList.First.Value.SaleId, 0)>0);
        }
        [TestMethod]
        public void AddProductToCartNegative()
        {
            us.login(zahi, "zahi", "123456");
            LinkedList<Sale> saleList = ss.viewSalesByStore(store.getStoreId());
            Assert.IsFalse(sellS.addRaffleProductToCart(zahi, saleList.First.Value.SaleId, -1)>0);
        }
        [TestMethod]
        public void AddProductToCartNormalSell()
        {
            us.login(zahi, "zahi", "123456");
            int saleId = ss.addSaleToStore(itamar, store.getStoreId(), sprite.getProductInStoreId(), 1, 1, "20/5/2018");
            LinkedList<Sale> saleList = ss.viewSalesByStore(store.getStoreId());
            foreach (Sale sale in saleList)
            {
                if (sale.SaleId == saleId)
                    Assert.IsFalse(sellS.addRaffleProductToCart(zahi, sale.SaleId, 1)>0);//normal product
                else
                    Assert.IsTrue(sellS.addRaffleProductToCart(zahi, sale.SaleId, 1)>0);
            }
        }
    }
}
