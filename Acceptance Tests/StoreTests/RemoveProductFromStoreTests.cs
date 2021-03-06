﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;
using WebServices.DAL;

namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class RemoveProductFromStoreTests
    {
        private userServices us;
        private storeServices ss;
        private User zahi;

        [TestInitialize]
        public void init()
        {
            CleanDB cDB = new CleanDB();
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
            us = userServices.getInstance();
            ss = storeServices.getInstance();
            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
        }

        [TestMethod]
        public void SimpleRemoveProduct()
        {
            us.login(zahi, "zahi", "123456");
            int storeId = ss.createStore("abowim", zahi);
            Store s = StoreManagement.getInstance().getStore(storeId);
            int pisId = ss.addProductInStore("cola", 3.2, 10, zahi, storeId, "Drink");
            ProductInStore pis = ProductManager.getInstance().getProductInStore(pisId);
            int result=ss.removeProductFromStore(s.storeId,pis.productInStoreId, zahi);
            Assert.IsTrue(result > -1);
            LinkedList<ProductInStore> LPIS=us.viewProductsInStores();
            Assert.AreEqual(LPIS.Count, 0);
        }

        [TestMethod]
        public void RemoveProductThatNotExist()
        {
            us.login(zahi, "zahi", "123456");
            int storeId = ss.createStore("abowim", zahi);
            Store s = StoreManagement.getInstance().getStore(storeId);
            ss.addProductInStore("cola", 3.2, 10, zahi, s.getStoreId(), "Drink");
            ProductInStore pis = new ProductInStore(2, new Product("cola"), 4, 3, s);
            int result = ss.removeProductFromStore(s.getStoreId(),pis.productInStoreId, zahi);
            Assert.IsFalse(result > -1);
            LinkedList<ProductInStore> LPIS = us.viewProductsInStores();
            Assert.AreEqual(LPIS.Count, 1);
        }

        [TestMethod]
        public void AdminTryToRemoveProduct()
        {
            us.login(zahi, "zahi", "123456");
            int storeId = ss.createStore("abowim", zahi);
            Store s = StoreManagement.getInstance().getStore(storeId);
            int pisId = ss.addProductInStore("cola", 3.2, 10, zahi, s.getStoreId(), "Drink");
            ProductInStore pis = ProductManager.getInstance().getProductInStore(pisId);
            User admin = us.startSession();
            us.login(admin, "admin", "admin");
            int result = ss.removeProductFromStore(s.getStoreId(),pis.productInStoreId, admin);
            Assert.IsFalse(result>-1);
            LinkedList<ProductInStore> LPIS = us.viewProductsInStores();
            Assert.AreEqual(LPIS.Count, 1);
            Assert.IsTrue(LPIS.Contains(pis));
        }

        [TestMethod]
        public void GuestTryToRemoveProduct()
        {
            us.login(zahi, "zahi", "123456");
            int storeId = ss.createStore("abowim", zahi);
            Store s = StoreManagement.getInstance().getStore(storeId);
            int pisId = ss.addProductInStore("cola", 3.2, 10, zahi, s.getStoreId(), "Drinks");
            ProductInStore pis = ProductManager.getInstance().getProductInStore(pisId);
            zahi.logOut();
            int result = ss.removeProductFromStore(s.getStoreId(),pis.productInStoreId, zahi);
            Assert.IsFalse(result > -1);
            LinkedList<ProductInStore> LPIS = us.viewProductsInStores();
            Assert.AreEqual(LPIS.Count, 1);
            Assert.IsTrue(LPIS.Contains(pis));
        }

        [TestMethod]
        public void CostumerTryToRemoveProduct()
        {
            us.login(zahi, "zahi", "123456");
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            int storeId = ss.createStore("abowim", zahi);
            Store s = StoreManagement.getInstance().getStore(storeId);
            int pisId = ss.addProductInStore("cola", 3.2, 10, zahi, s.getStoreId(), "Drinks");
            ProductInStore pis = ProductManager.getInstance().getProductInStore(pisId);
            int result = ss.removeProductFromStore(s.getStoreId(), pis.getProductInStoreId(), aviad);
            Assert.IsFalse(result > -1);
            LinkedList<ProductInStore> LPIS = us.viewProductsInStores();
            Assert.AreEqual(LPIS.Count, 1);
            Assert.IsTrue(LPIS.Contains(pis));
        }

        [TestMethod]
        public void OwnerOfOtherStoreTryToRemoveProduct()
        {
            us.login(zahi, "zahi", "123456");
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            int storeId = ss.createStore("abowim", zahi);
            Store s = StoreManagement.getInstance().getStore(storeId);
            int storeId2 = ss.createStore("Brohim", aviad);
            Store s2 = StoreManagement.getInstance().getStore(storeId2);
            int pisId = ss.addProductInStore("cola", 3.2, 10, zahi, s.getStoreId(), "Drinks");
            ProductInStore pis = ProductManager.getInstance().getProductInStore(pisId);
            int result = ss.removeProductFromStore(s.getStoreId(), pis.getProductInStoreId(), aviad);
            Assert.IsFalse(result > -1);
            LinkedList<ProductInStore> LPIS = us.viewProductsInStores();
            Assert.AreEqual(LPIS.Count, 1);
            Assert.IsTrue(LPIS.Contains(pis));
        }

        [TestMethod]
        public void nullTryToRemoveProduct()
        {
            us.login(zahi, "zahi", "123456");
            int storeId = ss.createStore("abowim", zahi);
            Store s = StoreManagement.getInstance().getStore(storeId);
            int pisId = ss.addProductInStore("cola", 3.2, 10, zahi, s.getStoreId(), "Drinks");
            ProductInStore pis = ProductManager.getInstance().getProductInStore(pisId);
            int result = ss.removeProductFromStore(s.getStoreId(), pis.getProductInStoreId(), null);
            Assert.IsFalse(result > -1);
            LinkedList<ProductInStore> LPIS = us.viewProductsInStores();
            Assert.AreEqual(LPIS.Count, 1);
            Assert.IsTrue(LPIS.Contains(pis));
        }

        [TestMethod]
        public void RemoveNullProduct()
        {
            us.login(zahi, "zahi", "123456");
            int storeId = ss.createStore("abowim", zahi);
            Store s = StoreManagement.getInstance().getStore(storeId);
            int pisId = ss.addProductInStore("cola", 3.2, 10, zahi, s.getStoreId(), "Drinks");
            ProductInStore pis = ProductManager.getInstance().getProductInStore(pisId);
            int result = ss.removeProductFromStore(s.getStoreId(), -31, zahi);
            Assert.IsFalse(result > -1);
            LinkedList<ProductInStore> LPIS = us.viewProductsInStores();
            Assert.AreEqual(LPIS.Count, 1);
            Assert.IsTrue(LPIS.Contains(pis));
        }

        [TestMethod]
        public void RemoveNullProductFromNullStore()
        {
            us.login(zahi, "zahi", "123456");
            int storeId = ss.createStore("abowim", zahi);
            Store s = StoreManagement.getInstance().getStore(storeId);
            int pisId = ss.addProductInStore("cola", 3.2, 10, zahi, s.getStoreId(), "Drinks");
            ProductInStore pis = ProductManager.getInstance().getProductInStore(pisId);
            int result = ss.removeProductFromStore(-31, pis.getProductInStoreId(), zahi);
            Assert.IsFalse(result > -1);
            LinkedList<ProductInStore> LPIS = us.viewProductsInStores();
            Assert.AreEqual(LPIS.Count, 1);
            Assert.IsTrue(LPIS.Contains(pis));
        }


        [TestMethod]
        public void RemoveRemovedProductFromStore()
        {
            us.login(zahi, "zahi", "123456");
            int storeId = ss.createStore("abowim", zahi);
            Store s = StoreManagement.getInstance().getStore(storeId);
            int pisId = ss.addProductInStore("cola", 3.2, 10, zahi, s.getStoreId(), "Drinks");
            ProductInStore pis = ProductManager.getInstance().getProductInStore(pisId);
            int pis2Id = ss.addProductInStore("sprite", 3.2, 10, zahi, s.getStoreId(), "Drinks");
            ProductInStore pis2 = ProductManager.getInstance().getProductInStore(pis2Id);
            ss.removeProductFromStore(s.getStoreId(), pis.getProductInStoreId(), zahi);
            int result = ss.removeProductFromStore(s.getStoreId(), pis.getProductInStoreId(), zahi);
            Assert.IsFalse(result>-1);
            LinkedList<ProductInStore> LPIS = us.viewProductsInStores();
            Assert.AreEqual(LPIS.Count, 1);
            Assert.IsTrue(LPIS.Contains(pis2));
        }

    }
}
