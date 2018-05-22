using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class editProductInStore
    {
        private userServices us;
        private storeServices ss;
        private User zahi;
        private Store store;
        private ProductInStore cola;

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
            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");
            int storeid = ss.createStore("abowim", zahi);
            store = storeArchive.getInstance().getStore(storeid);
            int c = ss.addProductInStore("cola", 3.2, 10, zahi, storeid, "Drinks");
            cola = ProductArchive.getInstance().getProductInStore(c);

        }
  
        [TestMethod]
        public void simpleEditProductInStore()
        {
            Assert.AreEqual(ss.editProductInStore(zahi, store.getStoreId(), cola.getProductInStoreId(), 100, 5.2),0);
            Assert.AreEqual(cola.getAmount(),100);
            Assert.AreEqual(cola.getPrice(), 5.2);
        }

        [TestMethod]
        public void EditProductInStoreWithNullSession()
        {
            Assert.AreEqual(-4,ss.editProductInStore(null, store.getStoreId(), cola.getProductInStoreId(), 100, 5.2));//-1 not log in can be -4 no premition
            Assert.AreEqual(cola.getAmount(), 10);
            Assert.AreEqual(cola.getPrice(), 3.2);
        }

        [TestMethod]
        public void EditProductInStoreWithNullStore()
        {
            int temp = ss.editProductInStore(zahi, -7, cola.getProductInStoreId(), 100, 5.2);
            Assert.AreEqual(-6,temp);//-6 if illegal store id
            Assert.AreEqual(cola.getAmount(), 10);
            Assert.AreEqual(cola.getPrice(), 3.2);
        }

        [TestMethod]
        public void EditProductInStoreWithNullProductInStore()
        {
            Assert.AreEqual(ss.editProductInStore(zahi, store.getStoreId(), -7, 100, 5.2),-8);// -8 if illegal product in store Id
            Assert.AreEqual(cola.getAmount(), 10);
            Assert.AreEqual(cola.getPrice(), 3.2);
        }

        [TestMethod]
        public void EditProductInStoreWithNegativeAmount()
        {
            Assert.AreEqual(ss.editProductInStore(zahi, store.getStoreId(), cola.getProductInStoreId(), -1, 5.2),-5);//-5 if illegal amount
            Assert.AreEqual(cola.getAmount(), 10);
            Assert.AreEqual(cola.getPrice(), 3.2);
        }
        [TestMethod]
        public void EditProductInStoreWithZeroAmount()
        {
            Assert.AreEqual(ss.editProductInStore(zahi, store.getStoreId(), cola.getProductInStoreId(), 0, 5.2),0);
            Assert.AreEqual(cola.getAmount(), 0);
            Assert.AreEqual(cola.getPrice(), 5.2);
        }

        [TestMethod]
        public void EditProductInStoreWithNegativePrice()
        {
            Assert.AreEqual(ss.editProductInStore(zahi, store.getStoreId(), cola.getProductInStoreId(), 100, -4),-7);//-7 if illegal price
            Assert.AreEqual(cola.getAmount(), 10);
            Assert.AreEqual(cola.getPrice(), 3.2);
        }

    }
}
