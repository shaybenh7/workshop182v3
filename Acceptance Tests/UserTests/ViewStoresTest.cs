using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServices.DAL;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.UserTests
{
    [TestClass]
    public class ViewStoresTest
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
        public void SimpleViewStore()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            us.register(session, "zahi", "123456");
            us.login(session, "zahi", "123456");
            storeServices ss = storeServices.getInstance();
            ss.createStore("abowim", session);
            LinkedList<Store> Lstore=us.viewStores();
            Assert.AreEqual(Lstore.Count, 1);
        }

        [TestMethod]
        public void ViewMultipuleStores()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            us.register(session, "zahi", "123456");
            us.login(session, "zahi", "123456");
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            storeServices ss = storeServices.getInstance();
            ss.createStore("abowim", session);
            ss.createStore("abowim2", session);
            ss.createStore("bros", aviad);
            LinkedList<Store> Lstore = us.viewStores();
            Assert.AreEqual(Lstore.Count, 3);
        }

        [TestMethod]
        public void ViewStoreWhenThrerIsNoStores()
        {
            userServices us = userServices.getInstance();
            LinkedList<Store> Lstore = us.viewStores();
            Assert.AreEqual(Lstore.Count, 0);
        }
    }
}
