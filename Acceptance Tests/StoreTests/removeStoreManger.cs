using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class removeStoreManger
    {

        private userServices us;
        private storeServices ss;
        private User zahi, itamar, niv, admin, admin1; //admin,itamar logedin
        private Store store;//itamar owner , niv manneger

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
            admin = us.startSession();
            us.register(admin, "admin", "123456");
            us.login(admin, "admin", "123456");

            admin1 = us.startSession();
            us.register(admin1, "admin1", "123456");

            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");

            itamar = us.startSession();
            us.register(itamar, "itamar", "123456");
            us.login(itamar,"itamar", "123456");
            store = storeArchive.getInstance().getStore(ss.createStore("Maria&Netta Inc.", itamar));

            niv = us.startSession();
            us.register(niv, "niv", "123456");
            us.login(niv, "niv", "123456");

            ss.addStoreManager(store.getStoreId(), "niv", itamar);

        }

        [TestMethod]
        public void simpleRemoveManger()
        {
           Assert.AreEqual(ss.removeStoreManager(store.getStoreId(), "niv", itamar),0);
            Assert.AreEqual(store.getManagers().Count , 0);
        }
        [TestMethod]
        public void RemoveMangerByAdmin()
        {
            Assert.AreEqual(ss.removeStoreManager(store.getStoreId(), "niv", admin),-4);//-4 if don't have premition
            Assert.AreEqual(store.getManagers().Count, 1);
        }
        [TestMethod]
        public void RemoveMangerByUser()
        {
            zahi.login("zahi", "123456");
            Assert.AreEqual(ss.removeStoreManager(store.getStoreId(), "niv", zahi),-4);//-4 if don't have premition
            Assert.AreEqual(store.getManagers().Count, 1);
        }
        [TestMethod]
        public void RemoveMangerByNotExistUser()
        {
            Assert.AreEqual(-4,ss.removeStoreManager(store.getStoreId(), "niv", zahi));//-1 if user Not Login could be -4
            Assert.AreEqual(store.getManagers().Count, 1);
        }
        [TestMethod]
        public void RemoveMangerByManegerWithPremition()
        {
            Assert.AreEqual(ss.addManagerPermission("removeStoreManager", store.getStoreId(), "niv", itamar),0);
            us.login(zahi, "zahi", "123456");
            ss.addStoreManager(store.getStoreId(), "zahi", itamar);
            Assert.AreEqual(ss.removeStoreManager(store.getStoreId(), "zahi", niv),0);
            Assert.AreEqual(store.getManagers().Count, 1);
        }

        [TestMethod]
        public void RemoveMangerByHimselfWithPremition()
        {
            ss.addManagerPermission("removeStoreManager", store.getStoreId(), "niv", itamar);
            Assert.AreEqual(ss.removeStoreManager(store.getStoreId(), "niv", niv),-10);//-10 can't remove himself
            Assert.AreEqual(store.getManagers().Count, 1);
        }
        [TestMethod]
        public void RemoveMangerByManegerThatRemoved()
        {
            ss.addManagerPermission("removeStoreManager", store.getStoreId(), "niv", itamar);
            us.login(zahi, "zahi", "123456");
            ss.addStoreManager(store.getStoreId(), "zahi", itamar);
            ss.addStoreManager(store.getStoreId(), "admin", itamar);
            ss.addManagerPermission("removeStoreManager", store.getStoreId(), "zahi", itamar);
            ss.addManagerPermission("removeStoreManager", store.getStoreId(), "admin", itamar);
            Assert.AreEqual(ss.removeStoreManager(store.getStoreId(), "zahi", niv),0);
            Assert.AreEqual(ss.removeStoreManager(store.getStoreId(), "niv", zahi),-4);//-4 if don't have premition
            Assert.AreEqual(ss.removeStoreManager(store.getStoreId(), "admin", niv),0);
            Assert.AreEqual(store.getManagers().Count, 1);
        }
    }
}
