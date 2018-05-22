using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            ss.createStore("bro's", aviad);
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
