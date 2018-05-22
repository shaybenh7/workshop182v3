using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;

namespace IntegrationTests
{
    [TestClass]
    public class storeTest
    {
        private User zahi;  // owner of store
        private User aviad; //manager of store
        private User shay;
        private User itamar; // not a real user
        private User niv; // guest
        private Store store, store2;
        StoreRole zahiOwner;
        StoreRole aviadManeger;
        ProductInStore cola;

        [TestInitialize]
        public void init()
        {
            ProductArchive.restartInstance();
            SalesArchive.restartInstance();
            storeArchive.restartInstance();
            UserArchive.restartInstance();
            UserCartsArchive.restartInstance();
            StorePremissionsArchive.restartInstance();

            BuyHistoryArchive.restartInstance();
            CouponsArchive.restartInstance();
            DiscountsArchive.restartInstance();
            RaffleSalesArchive.restartInstance();
            StorePremissionsArchive.restartInstance();
            zahi = new User("zahi", "123456");
            zahi.register("zahi", "123456");
            zahi.login("zahi", "123456");
            aviad = new User("aviad", "123456");
            aviad.register("aviad", "123456");
            aviad.login("aviad", "123456");
            shay = new User("shay", "123456");
            shay.register("shay", "123456");
            shay.login("shay", "123456");
            itamar = new User("itamar", "123456");
            niv = new User("niv", "123456");
            niv.register("niv", "123456");
            int storeId = zahi.createStore("abowim");
            store = storeArchive.getInstance().getStore(storeId);
            int storeId2 = zahi.createStore("broes");
            store2 = storeArchive.getInstance().getStore(storeId2);
            zahiOwner = new StoreOwner(zahi, store);
            
            aviadManeger = new StoreManager(aviad, store);
            zahiOwner.addStoreManager(zahi, store, "aviad");
            niv.logOut();
            int colaId = zahiOwner.addProductInStore(zahi, store, "cola", 3.2, 10, "Drinks");
            cola = ProductArchive.getInstance().getProductInStore(colaId);
        }

        [TestMethod]
        public void RemoveProductFromStore()
        {
            int result = zahiOwner.removeProductFromStore(zahi, store, cola);
            Assert.IsTrue(result > -1);
            LinkedList<ProductInStore> LPIS = store.getProductsInStore();
            Assert.AreEqual(LPIS.Count, 0);
        }
        [TestMethod]
        public void RemoveProductFromStoreAsMannegerWithoutPremission()
        {
            int result = aviadManeger.removeProductFromStore(zahi, store, cola);
            Assert.IsFalse(result > -1);
            LinkedList<ProductInStore> LPIS = store.getProductsInStore();
            Assert.AreEqual(LPIS.Count, 1);
        }
        [TestMethod]
        public void RemoveProductFromStoreAsMannegerWithPremission()
        {
            zahiOwner.addManagerPermission(zahi, "removeProductFromStore", store, "aviad");
            aviadManeger = new StoreManager(aviad, store);
            int result = aviadManeger.removeProductFromStore(aviad, store, cola);
            Assert.IsTrue(result > -1);
            LinkedList<ProductInStore> LPIS = store.getProductsInStore();
            Assert.AreEqual(LPIS.Count, 0);
        }
        [TestMethod]
        public void RemoveProductFromStoreTwice()
        {
            int result = zahiOwner.removeProductFromStore(zahi, store, cola);
            Assert.IsTrue(result > -1);
            int result2 = zahiOwner.removeProductFromStore(zahi, store, cola);
            Assert.IsFalse(result2 > -1);
            LinkedList<ProductInStore> LPIS = store.getProductsInStore();
            Assert.AreEqual(LPIS.Count, 0);
        }



        [TestMethod]
        public void RemoveMangerFromStore()
        {
            Assert.IsTrue(zahiOwner.removeStoreManager(zahi, store, "aviad") > -1);
            Assert.AreEqual(store.getManagers().Count, 0);
        }
        [TestMethod]
        public void RemoveMangerFromStoreTwice()
        {
            Assert.IsTrue(zahiOwner.removeStoreManager(zahi, store, "aviad") > -1);
            Assert.AreEqual(store.getManagers().Count, 0);
            Assert.IsFalse(zahiOwner.removeStoreManager(zahi, store, "aviad") > -1);
            Assert.AreEqual(store.getManagers().Count, 0);
        }
        [TestMethod]
        public void RemoveMangerFromStoreAsMannegerWithoutPremition()
        {
            zahiOwner.addStoreManager(zahi, store, "niv");
            Assert.IsFalse(aviadManeger.removeStoreManager(aviad, store, "niv") > -1);
            Assert.AreEqual(store.getManagers().Count, 2);
        }
        [TestMethod]
        public void RemoveMangerFromStoreAsMannegerWithPremition()
        {
            zahiOwner.addStoreManager(zahi, store, "niv");
            zahiOwner.addManagerPermission(zahi, "removeStoreManager", store, "aviad");
            aviadManeger = new StoreManager(aviad, store);           
            int temp = aviadManeger.removeStoreManager(aviad, store, "niv");
            Assert.IsTrue(temp > -1);
            Assert.AreEqual(store.getManagers().Count, 1);
        }
        [TestMethod]
        public void RemoveMangerFromStoreHimself()
        {
            zahiOwner.addStoreManager(zahi, store, "niv");
            zahiOwner.addManagerPermission(zahi, "removeManagerPermission", store, "aviad");
            Assert.IsFalse(aviadManeger.removeStoreManager(aviad, store, "aviad") > -1);
            Assert.AreEqual(store.getManagers().Count, 2);
        }

        [TestMethod]
        public void RemoveStoreOwner()
        {
            zahiOwner.addStoreOwner(zahi, store, "shay");
            Assert.IsTrue(zahiOwner.removeStoreOwner(zahi, store, "shay") > -1);
            Assert.AreEqual(store.getOwners().Count, 1);
        }
        [TestMethod]
        public void RemoveStoreOwnerCreitore()
        {
            zahiOwner.addStoreOwner(zahi, store, "shay");
            StoreRole shayOwner = new StoreOwner(shay, store);
            Assert.IsFalse(shayOwner.removeStoreOwner(shay, store, "zahi") > -1);
            Assert.AreEqual(store.getOwners().Count, 2);
        }
        [TestMethod]
        public void RemoveStoreOwnerNotOwner()
        {
            zahiOwner.addStoreOwner(zahi, store, "shay");
            Assert.IsFalse(zahiOwner.removeStoreOwner(zahi, store, "aviad") > -1);
            Assert.AreEqual(store.getOwners().Count, 2);
        }
        [TestMethod]
        public void RemoveStoreOwnerTwice()
        {
            zahiOwner.addStoreOwner(zahi, store, "shay");
            StoreRole shayOwner = new StoreOwner(shay, store);
            zahiOwner.addStoreOwner(zahi, store, "aviad");
            Assert.AreEqual(store.getOwners().Count, 3);
            Assert.IsTrue(shayOwner.removeStoreOwner(zahi, store, "aviad") > -1);
            Assert.AreEqual(store.getOwners().Count, 2);
        }
        [TestMethod]
        public void RemoveStoreOwnerNotCretore()
        {
            zahiOwner.addStoreOwner(zahi, store, "shay");
            Assert.IsFalse(zahiOwner.removeStoreOwner(zahi, store, "aviad") > -1);
            Assert.AreEqual(store.getOwners().Count, 2);
        }
        [TestMethod]
        public void RemoveStoreOwnerHimself()
        {
            zahiOwner.addStoreOwner(zahi, store, "shay");
            StoreRole shayOwner = new StoreOwner(shay, store);
            Assert.IsFalse(shayOwner.removeStoreOwner(shay, store, "shay") > -1);
            Assert.AreEqual(store.getOwners().Count, 2);
        }



        [TestMethod]
        public void ViewSlaeInStore()
        {
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 1, "20/5/2018");
            LinkedList<Sale> saleList = store.getAllSales();
            Assert.AreEqual(saleList.Count, 1);
            Assert.AreEqual(saleId, saleList.First.Value.SaleId);
        }
        [TestMethod]
        public void ViewSlaeInStoreEmptySale()
        {
            LinkedList<Sale> saleList = store.getAllSales();
            Assert.AreEqual(saleList.Count, 0);
        }
        [TestMethod]
        public void ViewSlaeInStoreWithRaffleSell()
        {
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 1, "20/5/2018");
            int saleId2 = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 3, 1, "20/5/2018");
            LinkedList <Sale> saleList = store.getAllSales();
            Assert.AreEqual(saleList.Count, 2);
        }
        [TestMethod]
        public void ViewSlaeInStoreTwiceSameProduct()
        {
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 1, "20/5/2018");
            int saleId2 = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 1, "20/5/2018");
            LinkedList<Sale> saleList = store.getAllSales();
            Assert.AreEqual(saleList.Count, 1);
        }
        [TestMethod]
        public void ViewSlaeInStoreAsMannegerWithoutPremition()
        {
            int saleId = aviadManeger.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 1, "20/5/2018");
            LinkedList<Sale> saleList = store.getAllSales();
            Assert.AreEqual(saleList.Count, 0);
        }
        [TestMethod]
        public void ViewSlaeInStoreAsMannegerWithPremition()
        {
            zahiOwner.addManagerPermission(zahi, "addSaleToStore", store, "aviad");
            aviadManeger = new StoreManager(aviad, store);
            int saleId = aviadManeger.addSaleToStore(aviad, store, cola.getProductInStoreId(), 1, 1, "20/5/2018");
            LinkedList<Sale> saleList = store.getAllSales();
            Assert.AreEqual(saleList.Count, 1);
        }

        [TestMethod]
        public void BuyHistoryStore()
        {
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 1) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCart();
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.First.Value.getSaleId() == saleId);
            Assert.IsTrue(aviad.buyProducts("1234", ""));
            Assert.AreEqual(zahi.viewStoreHistory(store).Count, 1);
        }
        [TestMethod]
        public void BuyHistoryStoreWithoutAnyBuies()
        {
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 1) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCart();
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.First.Value.getSaleId() == saleId);
            Assert.AreEqual(zahi.viewStoreHistory(store).Count, 0);
        }
        [TestMethod]
        public void BuyHistoryStoreWithoutPremition()
        {
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 1) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCart();
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.First.Value.getSaleId() == saleId);
            Assert.IsNull(aviad.viewStoreHistory(store));
        }
        [TestMethod]
        public void BuyHistoryUser()
        {
            User admin = new User("admin", "123456");
            admin.register("admin", "123456");
            admin.login("admin", "123456");
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 1) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCart();
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.First.Value.getSaleId() == saleId);
            Assert.IsTrue(aviad.buyProducts("1234", ""));
            Assert.AreEqual(admin.viewUserHistory("aviad").Count, 1);
        }
        [TestMethod]
        public void BuyHistoryUserWithoutAnyBuies()
        {
            User admin = new User("admin", "123456");
            admin.register("admin", "123456");
            admin.login("admin", "123456");
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 1) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCart();
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.First.Value.getSaleId() == saleId);
            Assert.AreEqual(admin.viewUserHistory("aviad").Count, 0);
        }
        [TestMethod]
        public void BuyHistoryUserWithoutPremition()
        {
            User admin = new User("admin", "123456");
            admin.register("admin", "123456");
            admin.login("admin", "123456");
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 1) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCart();
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.First.Value.getSaleId() == saleId);
            Assert.IsNull(zahi.viewUserHistory("aviad"));
        }
    }
}
