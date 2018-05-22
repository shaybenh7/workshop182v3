﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{

    [TestClass]
    public class addStoreOwnerTests
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
        public void simpleAddStoreOwner()
        {
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            aviad.login("aviad", "123456");
            int storeid = ss.createStore("abowim", zahi);
            Store store = storeArchive.getInstance().getStore(storeid);
            ss.addStoreOwner(store.getStoreId(), "aviad", zahi);
            LinkedList<StoreOwner> Userowners = store.getOwners();
            LinkedList<String> owners = new LinkedList<String>();
            foreach (StoreOwner o in Userowners)
            {
                owners.AddFirst(o.getUser().getUserName());
            }
            Assert.AreEqual(owners.Count, 2);
            Assert.IsTrue(owners.Contains("zahi"));
            Assert.IsTrue(owners.Contains("aviad"));
        }

        [TestMethod]
        public void AddStoreOwnerWhichIsMyself()
        {
            int storeid = ss.createStore("abowim", zahi);
            Store store = storeArchive.getInstance().getStore(storeid);
            ss.addStoreOwner(store.getStoreId(), "zahi", zahi);
            LinkedList<StoreOwner> Userowners = store.getOwners();
            LinkedList<User> owners = new LinkedList<User>();
            foreach (StoreOwner o in Userowners)
            {
                owners.AddFirst(o.getUser());
            }
            Assert.AreEqual(owners.Count, 1);
            Assert.IsTrue(owners.Contains(zahi));
        }

        [TestMethod]
        public void AddStoreOwnerWhichAllreadyOwner()
        {
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            int storeid = ss.createStore("abowim", zahi);
            Store store = storeArchive.getInstance().getStore(storeid);
            us.login(aviad, "aviad", "123456");
            ss.addStoreOwner(store.getStoreId(), "aviad", zahi);
            Assert.IsFalse(ss.addStoreOwner(store.getStoreId(), "zahi", aviad));
            LinkedList<StoreOwner> Userowners = store.getOwners();
            LinkedList<String> owners = new LinkedList<String>();
            foreach (StoreOwner o in Userowners)
            {
                owners.AddFirst(o.getUser().getUserName());
            }
            Assert.AreEqual(owners.Count, 2);
            Assert.IsTrue(owners.Contains("zahi"));
            Assert.IsTrue(owners.Contains("aviad"));
        }

        [TestMethod]
        public void AddMyselfAsStoreOwnerWithoutBeingOwner()
        {
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            storeServices ss = storeServices.getInstance();
            int storeid = ss.createStore("abowim", zahi);
            Store store = storeArchive.getInstance().getStore(storeid);
            ss.addStoreOwner(store.getStoreId(), "aviad", aviad);
            LinkedList<StoreOwner> Userowners = store.getOwners();
            LinkedList<User> owners = new LinkedList<User>();
            foreach (StoreOwner o in Userowners)
            {
                owners.AddFirst(o.getUser());
            }
            Assert.AreEqual(owners.Count, 1);
            Assert.IsTrue(owners.Contains(zahi));

        }

        [TestMethod]
        public void AddOtherUserAsStoreOwnerWithoutBeingOwner()
        {
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            User itamar = us.startSession();
            us.register(itamar, "itamar", "123456");
            us.login(itamar, "itamar", "123456");
            User niv = new User("niv", "123456");
            storeServices ss = storeServices.getInstance();
            int storeid = ss.createStore("abowim", zahi);
            Store store = storeArchive.getInstance().getStore(storeid);
            ss.addStoreOwner(store.getStoreId(), "itamar", aviad);
            LinkedList<StoreOwner> Userowners = store.getOwners();
            LinkedList<User> owners = new LinkedList<User>();
            foreach (StoreOwner o in Userowners)
            {
                owners.AddFirst(o.getUser());
            }
            Assert.AreEqual(owners.Count, 1);
            Assert.IsTrue(owners.Contains(zahi));
            ss.addStoreOwner(store.getStoreId(), "niv", aviad);
            LinkedList<StoreOwner> Userowners2 = store.getOwners();
            LinkedList<User> owners2 = new LinkedList<User>();
            foreach (StoreOwner o in Userowners2)
            {
                owners2.AddFirst(o.getUser());
            }
            Assert.AreEqual(owners2.Count, 1);
            Assert.IsTrue(owners2.Contains(zahi));
        }

        [TestMethod]
        public void StoreOwnerAddUserWhichNotExistAsOwner()
        {
            User aviad = new User("aviad", "123456");
            int storeid = ss.createStore("abowim", zahi);
            Store store = storeArchive.getInstance().getStore(storeid);
            ss.addStoreOwner(store.getStoreId(), "aviad", zahi);
            LinkedList<StoreOwner> Userowners = store.getOwners();
            LinkedList<User> owners = new LinkedList<User>();
            foreach (StoreOwner o in Userowners)
            {
                owners.AddFirst(o.getUser());
            }
            Assert.AreEqual(owners.Count, 1);
            Assert.IsTrue(owners.Contains(zahi));
        }

        [TestMethod]
        public void StoreOwnerAddUserWhichIsNullAsOwner()
        {
            int storeid = ss.createStore("abowim", zahi);
            Store store = storeArchive.getInstance().getStore(storeid);
            ss.addStoreOwner(store.getStoreId(), null, zahi);
            LinkedList<StoreOwner> Userowners = store.getOwners();
            LinkedList<User> owners = new LinkedList<User>();
            foreach (StoreOwner o in Userowners)
            {
                owners.AddFirst(o.getUser());
            }
            Assert.AreEqual(owners.Count, 1);
            Assert.IsTrue(owners.Contains(zahi));
        }

        [TestMethod]
        public void NullAddUserAsOwner()
        {
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            int storeid = ss.createStore("abowim", zahi);
            Store store = storeArchive.getInstance().getStore(storeid);
            ss.addStoreOwner(store.getStoreId(), "zahi", null);
            LinkedList<StoreOwner> Userowners = store.getOwners();
            LinkedList<User> owners = new LinkedList<User>();
            foreach (StoreOwner o in Userowners)
            {
                owners.AddFirst(o.getUser());
            }
            Assert.AreEqual(owners.Count, 1);
            Assert.IsTrue(owners.Contains(zahi));
        }

        [TestMethod]
        public void AddUserAsOwnerToNullStore()
        {
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            int storeid = ss.createStore("abowim", zahi);
            Store store = storeArchive.getInstance().getStore(storeid);
            ss.addStoreOwner(-31, "aviad", zahi);
            LinkedList<StoreOwner> Userowners = store.getOwners();
            LinkedList<User> owners = new LinkedList<User>();
            foreach (StoreOwner o in Userowners)
            {
                owners.AddFirst(o.getUser());
            }
            Assert.AreEqual(owners.Count, 1);
            Assert.IsTrue(owners.Contains(zahi));
            Assert.IsFalse(owners.Contains(aviad));
        }

    }
}
