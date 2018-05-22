using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class addProductInStoreTest
    {
        private userServices us;
        private storeServices ss;
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
            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");
        }

        [TestMethod]
        public void SimpleAddProduct()
        {
            int storeid = ss.createStore("abowim", zahi);
            Store s = storeArchive.getInstance().getStore(storeid);
            int p = ss.addProductInStore("cola", 3.2, 10, zahi, storeid, "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);
            Assert.AreEqual(pis.getPrice(), 3.2);
            Assert.AreEqual(pis.getAmount(), 10);
            Assert.AreEqual(pis.getStore().getStoreId(), s.getStoreId());
            LinkedList<ProductInStore> pList=s.getProductsInStore();
            Assert.IsTrue(pList.Contains(pis));
            Assert.AreEqual(pList.Count, 1);

        }

        [TestMethod]
        public void AddProductByCostumer()
        {
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            int storeid = ss.createStore("abowim", zahi);
            Store s = storeArchive.getInstance().getStore(storeid);
            int p = ss.addProductInStore("cola", 3.2, 10, aviad, storeid, "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);
            Assert.IsNull(pis);
            Assert.AreEqual(s.getProductsInStore().Count, 0);
        }


        [TestMethod]
        public void AddProductTwice()
        {
            int storeid = ss.createStore("abowim", zahi);
            Store s = storeArchive.getInstance().getStore(storeid);
            int p = ss.addProductInStore("cola", 3.2, 10, zahi, storeid, "Drinks");
            int p2 = ss.addProductInStore("cola", 3.2, 10, zahi, storeid, "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);
            ProductInStore pis2 = ProductArchive.getInstance().getProductInStore(p2);
            Assert.AreEqual(pis.getPrice(), 3.2);
            Assert.AreEqual(pis.getAmount(), 10);
            Assert.AreEqual(pis.getStore().getStoreId(), s.getStoreId());
            LinkedList<ProductInStore> pList = s.getProductsInStore();
            Assert.IsTrue(pList.Contains(pis));
            Assert.AreEqual(pList.Count, 1);
            Assert.IsNull(pis2);
        }


        [TestMethod]
        public void AddProductWithNegativeAmount()
        {
            int storeid = ss.createStore("abowim", zahi);
            Store s = storeArchive.getInstance().getStore(storeid);
            int p = ss.addProductInStore("cola", 3.2, -31, zahi, storeid, "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);
            Assert.IsNull(pis);
            Assert.AreEqual(s.getProductsInStore().Count, 0);
        }

        [TestMethod]
        public void AddProductWithNegativePrice()
        {
            int storeid = ss.createStore("abowim", zahi);
            Store s = storeArchive.getInstance().getStore(storeid);
            int p = ss.addProductInStore("cola", -3, 31, zahi, storeid, "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);
            Assert.IsNull(pis);
            Assert.AreEqual(s.getProductsInStore().Count, 0);
        }

        [TestMethod]
        public void AddProductWithZeroPrice()
        {
            int storeid = ss.createStore("abowim", zahi);
            Store s = storeArchive.getInstance().getStore(storeid);
            int p = ss.addProductInStore("cola", 0, 31, zahi, storeid, "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);
            Assert.IsNull(pis);
            Assert.AreEqual(s.getProductsInStore().Count, 0);
        }

        [TestMethod]
        public void AddProductWithZeroAmount()
        {
            int storeid = ss.createStore("abowim", zahi);
            Store s = storeArchive.getInstance().getStore(storeid);
            int p = ss.addProductInStore("cola", 3.2, 0, zahi, storeid, "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);
            Assert.AreEqual(s.getProductsInStore().Count, 0);
            Assert.IsNull(pis);
            Assert.AreEqual(s.getProductsInStore().Count, 0);
        }


        [TestMethod]
        public void AddProductWithEmptyName()
        {
            int storeid = ss.createStore("abowim", zahi);
            Store s = storeArchive.getInstance().getStore(storeid);
            int p = ss.addProductInStore("", 3.2, 31, zahi, storeid, "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);
            Assert.IsNull(pis);
            Assert.AreEqual(s.getProductsInStore().Count, 0);
        }

        [TestMethod]
        public void AddProductWithOnlySpacesName()
        {
            int storeid = ss.createStore("abowim", zahi);
            Store s = storeArchive.getInstance().getStore(storeid);
            int p = ss.addProductInStore("     ", 3.2, 31, zahi, storeid, "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);
            Assert.IsNull(pis);
            Assert.AreEqual(s.getProductsInStore().Count, 0);
        }


        [TestMethod]
        public void AddProductInStoreWithNullProduct()
        {
            int storeid = ss.createStore("abowim", zahi);
            Store s = storeArchive.getInstance().getStore(storeid);
            int p = ss.addProductInStore(null, 3.2, 31, null, storeid, "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);
            Assert.IsNull(pis);
            Assert.AreEqual(s.getProductsInStore().Count, 0);
        }

        [TestMethod]
        public void AddProductWithSpacesInName()
        {
            int storeid = ss.createStore("abowim", zahi);
            Store s = storeArchive.getInstance().getStore(storeid);
            int p = ss.addProductInStore("coca cola", 3.2, 10, zahi, storeid, "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);
            Assert.AreEqual(pis.getPrice(), 3.2);
            Assert.AreEqual(pis.getAmount(), 10);
            Assert.AreEqual(pis.getStore().getStoreId(), s.getStoreId());
            LinkedList<ProductInStore> pList = s.getProductsInStore();
            Assert.IsTrue(pList.Contains(pis));
            Assert.AreEqual(pList.Count, 1);
        }

        [TestMethod]
        public void AddProductToNoneExistingStore()
        {
            Store s = new Store(3,"coca", zahi);
            int p = ss.addProductInStore("cola", 3.2, 10, zahi, 3, "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);
            Assert.IsNull(pis);
            LinkedList<ProductInStore> pList = s.getProductsInStore();
            Assert.IsTrue(pList.Count == 0); //store is not exist in archive
        }

        [TestMethod]
        public void AddProductToStoreByGuest()
        {
            int storeid = ss.createStore("abowim", zahi);
            Store s = storeArchive.getInstance().getStore(storeid);
            zahi.logOut();
            int p = ss.addProductInStore("cola", 3.2, 10, zahi, storeid, "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);
            Assert.IsNull(pis);
            LinkedList<ProductInStore> pList = s.getProductsInStore();
            Assert.AreEqual(pList.Count,0); 
        }

    }
}
