using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.UserTests
{
    [TestClass]
    public class RemoveUser
    {
        private userServices us;
        private storeServices ss;
        private User zahi, itamar, niv, admin, admin1;
        private Store store;

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
            itamar.login("itamar", "123456");
            int id=itamar.createStore("Maria&Netta Inc.");
            store = storeArchive.getInstance().getStore(id);
            niv = us.startSession();
            us.register(niv, "niv", "123456");
            

            


        }

        [TestMethod]
        public void SimpleRemoveUser()
        {
            
            Assert.IsTrue(us.removeUser(admin,"zahi") >= 0);
            Assert.IsFalse(us.login(zahi, "zahi", "123456") >= 0);
        }
        [TestMethod]
        public void RemoveUserAdminNotLogin()
        {
            Assert.IsFalse(us.removeUser(admin1,"zahi") >= 0 );
            Assert.IsTrue(us.login(zahi, "zahi", "123456") >= 0 );
        }
        [TestMethod]
        public void AdminRemoveHimself()
        {
            int temp = us.removeUser(admin, "admin");
            Assert.IsFalse( temp>= 0);
            int temp2 = us.login(admin, "admin", "123456");
            Assert.IsTrue(temp2== -4);
        }
        [TestMethod]
        public void UserRemoveHimself()
        {
            us.login(zahi, "zahi", "123456");
            Assert.IsFalse(us.removeUser(zahi, "zahi") >= 0);
            Assert.IsTrue(us.login(zahi,"zahi", "123456") == -4);
        }
        [TestMethod]
        public void UserRemoveUser()
        {
            us.login(zahi, "zahi", "123456");
            User shay = us.startSession();
            us.register(shay, "shay", "123456");
            Assert.IsFalse(us.removeUser(zahi,"shay") >= 0);
            Assert.IsTrue(us.login(shay,"shay", "123456") >= 0);
        }
        [TestMethod]
        public void AdminRemoveAdmin()
        {
            Assert.IsTrue(admin.removeUser("admin1") >= 0);
            Assert.IsFalse(admin1.login("admin1", "123456") >= 0);
        }
        [TestMethod]
        public void AdminRemoveAdminThatTryToRemoveUser()
        {
            us.login(admin1, "admin1", "123456");
            Assert.IsTrue(us.removeUser(admin,"admin1") >= 0);
            Assert.IsFalse(us.removeUser(admin1, "zahi") >= 0);
            Assert.IsTrue(us.login(zahi,"zahi", "123456") >= 0);
            User setion = us.startSession();
            Assert.IsFalse(us.login(setion,"admin1", "123456") >= 0);
        }
        [TestMethod]
        public void RemoveUserTwice()
        {
            Assert.IsTrue(us.removeUser(admin,"zahi") >= 0);
            Assert.IsFalse(us.removeUser(admin,"zahi") >= 0);
            Assert.IsFalse(us.login(zahi,"zahi", "123456") >= 0);
        }
        [TestMethod]
        public void RemoveUserThatNotExist()
        {
            Assert.IsFalse(us.removeUser(admin,"shay") >= 0);
        }
        [TestMethod]
        public void RemoveManager()
        {
            us.login(niv, "niv", "123456");
            ss.addStoreManager(store.getStoreId(), "niv", itamar);
            Assert.IsTrue(us.removeUser(admin,"niv") >= 0);
            Assert.IsFalse(us.login(niv,"niv", "123456") >= 0);
            Assert.AreEqual(store.getManagers().Count, 0);
        }
        [TestMethod]
        public void RemoveCreatoreOwner()
        {
            Assert.IsFalse(us.removeUser(admin,"itamar") >= 0);
            Assert.AreEqual(store.getOwners().Count, 1);
        }
        [TestMethod]
        public void RemoveNotCreatoreOwner()
        {
            ss.addStoreOwner(store.getStoreId(), "zahi", itamar);
            Assert.IsTrue(us.removeUser(admin,"zahi") >= 0);
            Assert.IsFalse(us.login(zahi,"zahi", "123456") >= 0);
            Assert.AreEqual(store.getOwners().Count, 1);
        }
        [TestMethod]
        public void RemoveCreatoreOwnerWithAnotherOwner()
        {
            ss.addStoreOwner(store.getStoreId(), "zahi", itamar);
            Assert.AreEqual(store.getOwners().Count, 2);
            Assert.IsFalse(us.removeUser(admin,"itamar") >= 0);
            Assert.AreEqual(store.getOwners().Count, 2);
        }
        [TestMethod]
        public void RemoveCreatorOwnerWithAnotherManager()
        {
            ss.addStoreOwner(store.getStoreId(), "niv", itamar);
            Assert.IsFalse(us.removeUser(admin,"itamar") >= 0);
            //Assert.IsTrue(us.login(itamar,"itamar", "123456"));
            Assert.AreEqual(store.getOwners().Count, 2);
        }
        [TestMethod]
        public void RemoveMannegerInFewStores()
        {
            int s2 = ss.createStore("admin store", admin);
            Store store2 = storeArchive.getInstance().getStore(s2);
            ss.addStoreManager(store2.getStoreId(), "niv", admin);
            Assert.IsTrue(us.removeUser(admin,"niv") >= 0);
            Assert.IsFalse(us.login(niv,"niv", "123456") >= 0);
            Assert.AreEqual(store.getManagers().Count, 0);
            Assert.AreEqual(store2.getManagers().Count, 0);
        }

    }
}
