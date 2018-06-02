using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;
using WebServices.Domain;

namespace IntegrationTests
{
    [TestClass]
    public class PayingSystemTest
    {
        private userServices us;
        private storeServices ss;
        private sellServices sellS;
        private User zahi, itamar, niv, admin, admin1; //admin,itamar logedin
        private Store store;//itamar owner , niv manneger
        ProductInStore cola, sprite;
        PaymentInterface paymentProxy;


        [TestInitialize]
        public void init()
        {
            WebServices.DAL.CleanDB cDB = new WebServices.DAL.CleanDB();
            cDB.emptyDB();
            ProductManager.restartInstance();
            SalesManager.restartInstance();
            StoreManagement.restartInstance();
            UserManager.restartInstance();
            UserCartsManager.restartInstance();
            BuyHistoryManager.restartInstance();
            CouponsManager.restartInstance();
            DiscountsManager.restartInstance();
            RaffleSalesManager.restartInstance();
            StorePremissionsArchive.restartInstance();

            paymentProxy = new PaymentProxy();

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
            int storeId = ss.createStore("Maria&Netta Inc.", itamar);

            store = StoreManagement.getInstance().getStore(storeId);

            niv = us.startSession();
            us.register(niv, "niv", "123456");
            us.login(niv, "niv", "123456");

            ss.addStoreManager(storeId, "niv", itamar);

            int colaId = ss.addProductInStore("cola", 3.2, 10, itamar, storeId, "Drinks");
            cola = ProductManager.getInstance().getProductInStore(colaId);
            int spriteId = ss.addProductInStore("sprite", 5.2, 100, itamar, storeId, "Drinks");
            sprite = ProductManager.getInstance().getProductInStore(spriteId);
            ss.addSaleToStore(itamar, storeId, cola.getProductInStoreId(), 1, 10, DateTime.Now.AddMonths(10).ToString());
        }

        [TestMethod]
        public void simplePayment()
        {
            us.login(zahi, "zahi", "123456");
            LinkedList<Sale> saleList = ss.viewSalesByStore(store.getStoreId());
            Assert.IsTrue(sellS.addProductToCart(zahi, saleList.First.Value.SaleId, 1) > 0);
            sellS.getShoppingCartBeforeCheckout(zahi);
            Tuple<int, LinkedList<UserCart>> ans=sellS.checkout(zahi,"Rager 214 Bash" ,"Israel", "123456");
            Assert.IsTrue(paymentProxy.payForProduct("123", zahi, ans.Item2.First.Value));
        }
        [TestMethod]
        public void nullCreditCard()
        {
            us.login(zahi, "zahi", "123456");
            LinkedList<Sale> saleList = ss.viewSalesByStore(store.getStoreId());
            Assert.IsTrue(sellS.addProductToCart(zahi, saleList.First.Value.SaleId, 1) > 0);
            sellS.getShoppingCartBeforeCheckout(zahi);
            Tuple<int, LinkedList<UserCart>> ans = sellS.checkout(zahi, "Rager 214 Bash", "Israel", "123456");
            Assert.IsFalse(paymentProxy.payForProduct(null, zahi, ans.Item2.First.Value));
        }
        [TestMethod]
        public void emptyCreditCard()
        {
            us.login(zahi, "zahi", "123456");
            LinkedList<Sale> saleList = ss.viewSalesByStore(store.getStoreId());
            Assert.IsTrue(sellS.addProductToCart(zahi, saleList.First.Value.SaleId, 1) > 0);
            sellS.getShoppingCartBeforeCheckout(zahi);
            Tuple<int, LinkedList<UserCart>> ans = sellS.checkout(zahi, "Rager 214 Bash", "Israel", "123456");
            Assert.IsFalse(paymentProxy.payForProduct("", zahi, ans.Item2.First.Value));
        }
        [TestMethod]
        public void nullUser()
        {
            us.login(zahi, "zahi", "123456");
            LinkedList<Sale> saleList = ss.viewSalesByStore(store.getStoreId());
            Assert.IsTrue(sellS.addProductToCart(zahi, saleList.First.Value.SaleId, 1) > 0);
            sellS.getShoppingCartBeforeCheckout(zahi);
            Tuple<int, LinkedList<UserCart>> ans = sellS.checkout(zahi, "Rager 214 Bash", "Israel", "123456");
            Assert.IsFalse(paymentProxy.payForProduct("123", null, ans.Item2.First.Value));
        }
        [TestMethod]
        public void nullProduct()
        {
            us.login(zahi, "zahi", "123456");
            LinkedList<Sale> saleList = ss.viewSalesByStore(store.getStoreId());
            Assert.IsTrue(sellS.addProductToCart(zahi, saleList.First.Value.SaleId, 1) > 0);
            sellS.getShoppingCartBeforeCheckout(zahi);
            Tuple<int, LinkedList<UserCart>> ans = sellS.checkout(zahi, "Rager 214 Bash", "Israel", "123456");
            Assert.IsFalse(paymentProxy.payForProduct("123", zahi, null));
        }

    }
}
