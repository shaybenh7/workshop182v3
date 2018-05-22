using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class AddStoreManagerTests
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
        public void SimpleAddStoreManager()
        {
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");

            int storeid = ss.createStore("abowim", zahi);
            Store store = storeArchive.getInstance().getStore(storeid);
            LinkedList<StoreManager> managers = store.getManagers();
            Assert.AreEqual(managers.Count, 0);

            ss.addStoreManager(store.getStoreId(), "aviad", zahi);
            managers = store.getManagers();
            Assert.AreEqual(managers.Count, 1);
            StoreManager SM = managers.First.Value;
            Assert.AreEqual(SM.getUser().getUserName(), aviad.getUserName());
            Assert.AreEqual(SM.getStore(), store);

            Premissions SP= SM.getPremissions(aviad,store);
            Dictionary<string, Boolean> Dict = SP.getPrivileges();
            foreach (KeyValuePair<string, Boolean> entry in Dict)
            {
                Assert.IsFalse(entry.Value);
            }
        }

        [TestMethod]
        public void OwnerTryAddHimselfToBeStoreManager()
        {
            int storeid = ss.createStore("abowim", zahi);
            Store store = storeArchive.getInstance().getStore(storeid);
            ss.addStoreManager(store.getStoreId(), "zahi", zahi);
            LinkedList<StoreManager>  managers = store.getManagers();
            Assert.AreEqual(managers.Count, 0);
        }

        [TestMethod]
        public void OwnerTryAddOtherOwnerToBeStoreManager()
        {
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            int storeid = ss.createStore("abowim", zahi);
            Store store = storeArchive.getInstance().getStore(storeid);
            ss.addStoreOwner(store.getStoreId(), "aviad", zahi);
            ss.addStoreManager(store.getStoreId(), "aviad", zahi);
            LinkedList<StoreManager>  managers = store.getManagers();
            Assert.AreEqual(managers.Count, 0);
        }

        [TestMethod]
        public void AddStoreManagerWhoIsNull()
        {

            int storeid = ss.createStore("abowim", zahi);
            Store store = storeArchive.getInstance().getStore(storeid);
            ss.addStoreManager(store.getStoreId(), null, zahi);
            LinkedList<StoreManager> managers = store.getManagers();
            Assert.AreEqual(managers.Count, 0);
        }

        [TestMethod]
        public void NullTryToAddStoreManager()
        {

            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            int storeid = ss.createStore("abowim", zahi);
            Store store = storeArchive.getInstance().getStore(storeid);
            ss.addStoreManager(store.getStoreId(), "aviad", null);
            LinkedList<StoreManager> managers = store.getManagers();
            Assert.AreEqual(managers.Count, 0);
        }

        [TestMethod]
        public void TryToAddStoreManagerToNullStore()
        {
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            storeServices ss = storeServices.getInstance();
            int storeid = ss.createStore("abowim", zahi);
            Store store = storeArchive.getInstance().getStore(storeid);
            ss.addStoreManager(-7, "aviad", zahi);
            LinkedList<StoreManager> managers = store.getManagers();
            Assert.AreEqual(managers.Count, 0);
        }



        [TestMethod]
        public void AddStoreManagerWhoAllreadyManager()
        {
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            int storeid = ss.createStore("abowim", zahi);
            Store store = storeArchive.getInstance().getStore(storeid);
            LinkedList<StoreManager> managers = store.getManagers();
            Assert.AreEqual(managers.Count, 0);
            ss.addStoreManager(store.getStoreId(), "aviad", zahi);
            ss.addStoreManager(store.getStoreId(), "aviad", zahi);
            managers = store.getManagers();
            Assert.AreEqual(managers.Count, 1);
            StoreManager SM = managers.First.Value;
            Assert.AreEqual(SM.getUser().getUserName(), aviad.getUserName());
            Assert.AreEqual(SM.getStore(), store);
            Premissions SP = SM.getPremissions(aviad,store);
            Dictionary<string, Boolean> Dict = SP.getPrivileges();
            foreach (KeyValuePair<string, Boolean> entry in Dict)
            {
                Assert.IsFalse(entry.Value);
            }
        }

    }
}
