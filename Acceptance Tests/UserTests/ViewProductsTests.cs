﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServices.DAL;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.UserTests
{
    [TestClass]
    public class ViewProductsTests
    {
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
        }

        [TestMethod]
        public void SimpleViewProductWithOneProduct()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            us.register(session, "zahi", "123456");
            us.login(session, "zahi", "123456");
            storeServices ss = storeServices.getInstance();
            int id=ss.createStore("abowim", session);
            Store s = StoreManagement.getInstance().getStore(id);
            int pis=ss.addProductInStore("cola", 3.2, 10, session, id, "drinks");
            LinkedList<ProductInStore> pisList = us.viewProductsInStore(id);
            LinkedList<ProductInStore> piStorsList = us.viewProductsInStores();
            Assert.AreEqual(pisList.Count, 1);
            Assert.AreEqual(piStorsList.Count, 1);
        }


        [TestMethod]
        public void ViewProductWhenThereIsNoProducts()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            us.register(session, "zahi", "123456");
            us.login(session, "zahi", "123456");
            storeServices ss = storeServices.getInstance();
            int id=ss.createStore("abowim", session);
            Product p = new Product("cola");
            LinkedList<ProductInStore> pisList = us.viewProductsInStore(id);
            LinkedList<ProductInStore> piStorsList = us.viewProductsInStores();
            Assert.AreEqual(pisList.Count, 0);
            Assert.AreEqual(piStorsList.Count, 0);

        }

        [TestMethod]
        public void SimpleViewProductWithTwoProducts()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            us.register(session, "zahi", "123456");
            us.login(session, "zahi", "123456");
            storeServices ss = storeServices.getInstance();
            int id1=ss.createStore("abowim", session);
            ss.addProductInStore("cola", 3.2, 10, session, id1, "drinks");
            ss.addProductInStore("sprite", 3.2, 10, session, id1, "drinks");
            LinkedList<ProductInStore> pisList = us.viewProductsInStore(id1);
            LinkedList<ProductInStore> piStorsList = us.viewProductsInStores();
            Assert.AreEqual(pisList.Count, 2);
            Assert.AreEqual(piStorsList.Count, 2);
        }

        [TestMethod]
        public void ViewProductInMultipleStores()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            us.register(session, "zahi", "123456");
            us.login(session, "zahi", "123456");
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            storeServices ss = storeServices.getInstance();
            int id1=ss.createStore("abowim", session);
            int id2= ss.createStore("bros", aviad);
            ss.addProductInStore("cola", 3.2, 10, session, id1, "drinks");
            ss.addProductInStore("sprite", 3.2, 10, session, id1, "drinks");
            ss.addProductInStore("milk", 3.2, 10, aviad, id2, "milk");
            LinkedList<ProductInStore> pisList = us.viewProductsInStore(id1);
            LinkedList<ProductInStore> pisList2 = us.viewProductsInStore(id2);
            LinkedList<ProductInStore> piStorsList = us.viewProductsInStores();

            Assert.AreEqual(pisList.Count, 2);
            Assert.AreEqual(pisList2.Count, 1);
            Assert.AreEqual(piStorsList.Count, 3);
        }

    }
}
