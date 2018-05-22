using System;
using wsep182.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace IntegrationTests
{
    [TestClass]
    public class UserTest
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
        public void LoginAndRegister()
        {
            User aviad = new User("aviad", "123456");
            Assert.IsTrue(aviad.getState() is Guest);
            aviad.register("aviad", "123456");
            aviad.login("aviad", "123456");
            Assert.AreEqual(aviad.getUserName(), "aviad");
            Assert.IsTrue(aviad.getState() is LogedIn);
        }
        [TestMethod]
        public void LoginAndRegisterNotExist()
        {
            User aviad = new User("aviad", "123456");
            Assert.IsTrue(aviad.getState() is Guest);
            Assert.IsFalse(aviad.login("aviad", "123456") > -1);

        }
        [TestMethod]
        public void LoginAndRegisterNotWork()
        {
            User aviad = new User("aviad", "123456");
            Assert.IsTrue(aviad.getState() is Guest);
            aviad.register("aviad", "");
            Assert.IsFalse(aviad.login("aviad", "")>-1);
        }


        [TestMethod]
        public void createStoreAndOwnerManneger()
        {
            User aviad = new User("aviad", "123456");
            aviad.register("aviad", "123456");
            User zahi = new User("zahi", "123456");
            zahi.register("zahi", "123456");
            User niv = new User("niv", "123456");
            niv.register("niv", "123456");
            aviad.login("aviad", "123456");
            int storeId = aviad.createStore("bro burger");
            Store s = storeArchive.getInstance().getStore(storeId);
            Assert.AreEqual(s.getStoreName(), "bro burger");
            Assert.AreEqual(s.getOwners().Count, 1);
            StoreRole sr = new StoreOwner(aviad, s);
            sr.addStoreOwner(aviad, s, "zahi");
            Assert.AreEqual(s.getOwners().Count, 2);
            sr.addStoreManager(aviad, s, "niv");
            Assert.AreEqual(s.getManagers().Count, 1);
        }
        [TestMethod]
        public void createStoreAndOwnerMannegerNotExistUser()
        {
            User aviad = new User("aviad", "123456");
            aviad.register("aviad", "123456");
            User zahi = new User("zahi", "123456");
            zahi.register("zahi", "123456");
            User niv = new User("niv", "123456");
            niv.register("niv", "123456");
            aviad.login("aviad", "123456");
            int storeId = aviad.createStore("bro burger");
            Store s = storeArchive.getInstance().getStore(storeId);
            Assert.AreEqual(s.getStoreName(), "bro burger");
            Assert.AreEqual(s.getOwners().Count, 1);
            StoreRole sr = new StoreOwner(aviad, s);
            sr.addStoreOwner(aviad, s, "zahi2");
            Assert.AreEqual(s.getOwners().Count, 1);
            sr.addStoreManager(aviad, s, "niv2");
            Assert.AreEqual(s.getManagers().Count, 0);
        }
        [TestMethod]
        public void createStoreAndOwnerMannegerAlredyMannege()
        {
            User aviad = new User("aviad", "123456");
            aviad.register("aviad", "123456");
            User zahi = new User("zahi", "123456");
            zahi.register("zahi", "123456");
            User niv = new User("niv", "123456");
            niv.register("niv", "123456");
            aviad.login("aviad", "123456");
            int storeId = aviad.createStore("bro burger");
            Store s = storeArchive.getInstance().getStore(storeId);
            Assert.AreEqual(s.getStoreName(), "bro burger");
            Assert.AreEqual(s.getOwners().Count, 1);
            StoreRole sr = new StoreOwner(aviad, s);
            sr.addStoreOwner(aviad, s, "zahi");
            Assert.AreEqual(s.getOwners().Count, 2);
            sr.addStoreManager(aviad, s, "niv");
            Assert.AreEqual(s.getManagers().Count, 1);
            sr.addStoreOwner(aviad, s, "zahi");
            Assert.AreEqual(s.getOwners().Count, 2);
            sr.addStoreManager(aviad, s, "niv");
            Assert.AreEqual(s.getManagers().Count, 1);
        }
        [TestMethod]
        public void createStoreAndOwnerMannegerNotOwner()
        {
            User aviad = new User("aviad", "123456");
            aviad.register("aviad", "123456");
            User zahi = new User("zahi", "123456");
            zahi.register("zahi", "123456");
            User niv = new User("niv", "123456");
            niv.register("niv", "123456");
            aviad.login("aviad", "123456");
            zahi.login("zahi", "123456");
            int storeId = aviad.createStore("bro burger");
            Store s = storeArchive.getInstance().getStore(storeId);
            Assert.AreEqual(s.getStoreName(), "bro burger");
            Assert.AreEqual(s.getOwners().Count, 1);
            StoreRole sr = new StoreOwner(aviad, s);
            sr.addStoreOwner(zahi, s, "niv");
            Assert.AreEqual(s.getOwners().Count, 2);
            sr.addStoreManager(zahi, s, "niv");
            Assert.AreEqual(s.getManagers().Count, 0);
        }
        [TestMethod]
        public void createStoreAndOwnerMannegerFromeManeger()
        {
            User aviad = new User("aviad", "123456");
            aviad.register("aviad", "123456");
            User zahi = new User("zahi", "123456");
            zahi.register("zahi", "123456");
            User niv = new User("niv", "123456");
            niv.register("niv", "123456");
            aviad.login("aviad", "123456");
            int storeId = aviad.createStore("bro burger");
            Store s = storeArchive.getInstance().getStore(storeId);
            Assert.AreEqual(s.getStoreName(), "bro burger");
            Assert.AreEqual(s.getOwners().Count, 1);
            StoreRole sr = new StoreOwner(aviad, s);
            sr.addStoreManager(aviad, s, "niv");
            Assert.AreEqual(s.getManagers().Count, 1);
            sr.addManagerPermission(aviad, "addManagerPermission", s, "niv");
            niv.login("niv", "123456");
            sr.addStoreManager(niv, s, "zahi");
            Assert.AreEqual(s.getManagers().Count, 2);
        }
        [TestMethod]
        public void createStoreAndOwnerMannegerFromeManegerWioutPremition()
        {
            User aviad = new User("aviad", "123456");
            aviad.register("aviad", "123456");
            User zahi = new User("zahi", "123456");
            zahi.register("zahi", "123456");
            User niv = new User("niv", "123456");
            niv.register("niv", "123456");
            aviad.login("aviad", "123456");
            int storeId = aviad.createStore("bro burger");
            Store s = storeArchive.getInstance().getStore(storeId);
            Assert.AreEqual(s.getStoreName(), "bro burger");
            Assert.AreEqual(s.getOwners().Count, 1);
            StoreRole sr = new StoreOwner(aviad, s);
            sr.addStoreManager(aviad, s, "niv");
            Assert.AreEqual(s.getManagers().Count, 1);
            sr.addManagerPermission(aviad, "removeStoreManager", s, "niv");
            niv.login("niv", "123456");
            sr.addStoreManager(niv, s, "zahi");
            Assert.AreEqual(s.getManagers().Count, 2);
        }


        [TestMethod]
        public void UserAddProductAndViewIt()
        {
            User aviad = new User("aviad", "123456");
            aviad.register("aviad", "123456");
            aviad.login("aviad", "123456");
            int storeId = aviad.createStore("bro burger");
            Store s = storeArchive.getInstance().getStore(storeId);
            StoreRole sr = new StoreOwner(aviad, s);
            int pisId = sr.addProductInStore(aviad, s, "cola", 3.2, 10, "Driks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(pisId);
            LinkedList<ProductInStore> pisList = s.getProductsInStore();
            Assert.IsTrue(pisList.Contains(pis));
            Assert.AreEqual(pisList.Count, 1);
        }
        [TestMethod]
        public void UserAddProductAndViewItNotExist()
        {
            User aviad = new User("aviad", "123456");
            aviad.register("aviad", "123456");
            aviad.login("aviad", "123456");
            int storeId = aviad.createStore("bro burger");
            Store s = storeArchive.getInstance().getStore(storeId);
            StoreRole sr = new StoreOwner(aviad, s);
            int pisId = sr.addProductInStore(aviad, s, "cola", -5, 10, "Driks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(pisId);
            LinkedList<ProductInStore> pisList = s.getProductsInStore();
            Assert.IsFalse(pisList.Contains(pis));
            Assert.AreEqual(pisList.Count, 0);
        }
        [TestMethod]
        public void UserAddProductAndViewItFewProducts()
        {
            User aviad = new User("aviad", "123456");
            aviad.register("aviad", "123456");
            aviad.login("aviad", "123456");
            int storeId = aviad.createStore("bro burger");
            Store s = storeArchive.getInstance().getStore(storeId);
            StoreRole sr = new StoreOwner(aviad, s);
            int pisId = sr.addProductInStore(aviad, s, "cola", 3.2, 10, "Driks");
            sr.addProductInStore(aviad, s, "sprite", 3.2, 10, "Driks");
            LinkedList<ProductInStore> pisList = s.getProductsInStore();
            Assert.AreEqual(pisList.Count, 2);
        }
        [TestMethod]
        public void UserAddProductAndViewItNotOwnerAddes()
        {
            User aviad = new User("aviad", "123456");
            aviad.register("aviad", "123456");
            aviad.login("aviad", "123456");
            User zahi = new User("zahi", "123456");
            zahi.register("zahi", "123456");
            zahi.login("zahi", "123456");
            int storeId = aviad.createStore("bro burger");
            Store s = storeArchive.getInstance().getStore(storeId);
            StoreRole sr = new StoreOwner(aviad, s);
            int pisId = sr.addProductInStore(aviad, s, "cola", 3.2, 10, "Driks");
            sr.addProductInStore(zahi, s, "sprite", 3.2, 10, "Driks");
            LinkedList<ProductInStore> pisList = s.getProductsInStore();
            Assert.AreEqual(pisList.Count, 2);
        }
        [TestMethod]
        public void UserAddProductAndViewItNoLoggedInTryToAdd()
        {
            User aviad = new User("aviad", "123456");
            aviad.register("aviad", "123456");
            aviad.login("aviad", "123456");
            User zahi = new User("zahi", "123456");
            int storeId = aviad.createStore("bro burger");
            Store s = storeArchive.getInstance().getStore(storeId);
            StoreRole sr = new StoreOwner(aviad, s);
            int pisId = sr.addProductInStore(aviad, s, "cola", 3.2, 10, "Driks");
            sr.addProductInStore(zahi, s, "sprite", 3.2, 10, "Driks");
            LinkedList<ProductInStore> pisList = s.getProductsInStore();
            Assert.AreEqual(pisList.Count, 2);
        }
        [TestMethod]
        public void UserAddProductAndViewItWioutProducts()
        {
            User aviad = new User("aviad", "123456");
            aviad.register("aviad", "123456");
            aviad.login("aviad", "123456");
            User zahi = new User("zahi", "123456");
            int storeId = aviad.createStore("bro burger");
            Store s = storeArchive.getInstance().getStore(storeId);
            LinkedList<ProductInStore> pisList = s.getProductsInStore();
            Assert.AreEqual(pisList.Count, 0);
        }

    }
}
