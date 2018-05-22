using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;


namespace IntegrationTests
{
    [TestClass]
    public class StoreRoleTest
    {
        private User zahi;  // owner of store
        private User aviad; //manager of store
        private User shay;
        private User itamar; // not a real user
        private User niv; // guest
        private Store store;
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
            zahiOwner = new StoreOwner(zahi, store);
            aviadManeger = new StoreManager(aviad, store);
            zahiOwner.addStoreManager(zahi, store, "aviad");
            niv.logOut();
            int colaId = zahiOwner.addProductInStore(zahi, store, "cola", 3.2, 10, "Drinks");
            cola = ProductArchive.getInstance().getProductInStore(colaId);
        }

        [TestMethod]
        public void editProductInStoreWithManagerPermission()
        {
            int pisId = zahiOwner.addProductInStore(zahi, store, "cola2", 10, 4, "Drinks");
            ProductInStore pis= ProductArchive.getInstance().getProductInStore(pisId);
            Assert.AreEqual(2, store.getProductsInStore().Count);
            aviadManeger.editProductInStore(aviad, pis, 13, 4.5);
            Assert.AreEqual(10, pis.getPrice());
            Assert.AreEqual(4, pis.getAmount());
            zahiOwner.addManagerPermission(zahi, "editProductInStore", store, "aviad");
            aviadManeger.editProductInStore(aviad, pis, 13, 4.5);
            Assert.AreEqual(4.5, pis.getPrice());
            Assert.AreEqual(13, pis.getAmount());
        }
        [TestMethod]
        public void editProductInStoreWithoutManagerPermission()
        {
            int pisId = zahiOwner.addProductInStore(zahi, store, "cola2", 10, 4, "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(pisId);
            Assert.AreEqual(2, store.getProductsInStore().Count);
            aviadManeger.editProductInStore(aviad, pis, 13, 4.5);
            Assert.AreEqual(10, pis.getPrice());
            Assert.AreEqual(4, pis.getAmount());
            zahiOwner.addManagerPermission(zahi, "addSaleToStore", store, "aviad");
            aviadManeger.editProductInStore(aviad, pis, 13, 4.5);
            Assert.AreEqual(10, pis.getPrice());
            Assert.AreEqual(4, pis.getAmount());
        }
        [TestMethod]
        public void SimpleAddSaleeWithManagerPermission()
        {
            zahiOwner.addManagerPermission(zahi, "addSaleToStore", store, "aviad");
            Assert.IsTrue(aviadManeger.addSaleToStore(aviad, store, cola.getProductInStoreId(), 1, 1, DateTime.Now.AddMonths(1).ToString()) > -1);
            Assert.AreEqual(store.getAllSales().Count, 1);
        }
        [TestMethod]
        public void SimpleAddSaleeWithoutManagerPermission()
        {
            zahiOwner.addManagerPermission(zahi, "editProductInStore", store, "aviad");
            Assert.IsFalse(aviadManeger.addSaleToStore(aviad, store, cola.getProductInStoreId(), 1, 1, DateTime.Now.AddMonths(1).ToString()) > -1);
            Assert.AreEqual(store.getAllSales().Count, 0);
        }
        [TestMethod]
        public void SimpleAddSaleeWithManagerPermissionNotGoodProduct()
        {
            zahiOwner.addManagerPermission(zahi, "addSaleToStore", store, "aviad");
            Assert.IsFalse(aviadManeger.addSaleToStore(aviad, store, cola.getProductInStoreId(), -3, 1, DateTime.Now.AddMonths(1).ToString()) > -1);
            Assert.AreEqual(store.getAllSales().Count, 0);
        }
        [TestMethod]
        public void SimpleAddSaleeWithOwner()
        {
            Assert.IsTrue(zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 1, DateTime.Now.AddMonths(1).ToString()) > -1);
            Assert.AreEqual(store.getAllSales().Count, 1);
        }
        [TestMethod]
        public void SimpleAddSaleeWithOwnerTwice()
        {
            Assert.IsTrue(zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 1, DateTime.Now.AddMonths(1).ToString()) > -1);
            Assert.IsTrue(zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 1, DateTime.Now.AddMonths(1).ToString()) > -1);
            Assert.AreEqual(store.getAllSales().Count, 1);
        }
        [TestMethod]
        public void SimpleAddRaffleSaleWithOwner()
        {
            Assert.IsTrue(zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 3, 1, DateTime.Now.AddMonths(1).ToString()) > -1);
        }
        [TestMethod]
        public void AddRaffleSaleWithOwnerWrongAmount()
        {
            Assert.IsTrue(zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 3, 2, DateTime.Now.AddMonths(1).ToString()) > -1);
            Assert.IsFalse(zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 3, -2, DateTime.Now.AddMonths(1).ToString()) > -1);
        }
        [TestMethod]
        public void RaffleSaleWithMannegerwithoutPremition()
        {
            Assert.IsFalse(aviadManeger.addSaleToStore(aviad, store, cola.getProductInStoreId(), 3, 1, DateTime.Now.AddMonths(1).ToString()) > -1);
        }
        [TestMethod]
        public void SimpleAddRaffleSaleWithManagerPermission()
        {
            zahiOwner.addManagerPermission(zahi, "addSaleToStore", store, "aviad");
            Assert.IsTrue(aviadManeger.addSaleToStore(aviad, store, cola.getProductInStoreId(), 3, 1, DateTime.Now.AddMonths(1).ToString()) > -1);
            Assert.AreEqual(store.getAllSales().Count, 1);
        }

        [TestMethod]
        public void removePISWithManagerPremition()
        {
            zahiOwner.addManagerPermission(zahi, "removeProductFromStore", store, "aviad");
            ProductInStore colaId = ProductArchive.getInstance().getProductInStore(cola.productInStoreId);
            Assert.IsTrue(aviadManeger.removeProductFromStore(aviad,store, colaId) > -1);
            Assert.AreEqual(store.getAllSales().Count, 0);
        }
        [TestMethod]
        public void removePISWithManagerPremitionTwice()
        {
            zahiOwner.addManagerPermission(zahi, "removeProductFromStore", store, "aviad");
            ProductInStore colaId = ProductArchive.getInstance().getProductInStore(cola.productInStoreId);
            Assert.IsTrue(aviadManeger.removeProductFromStore(aviad, store, colaId) > -1);
            Assert.AreEqual(store.getAllSales().Count, 0);
            Assert.IsFalse(aviadManeger.removeProductFromStore(aviad, store, colaId) > -1);
            Assert.AreEqual(store.getAllSales().Count, 0);
        }
        [TestMethod]
        public void addStoreMannegerWithManagerPermission()
        {
            zahiOwner.addManagerPermission(zahi, "addStoreManager", store, "aviad");
            int ans = aviadManeger.addStoreManager(aviad, store, "shay");
            Assert.IsTrue(ans > -1);
            Assert.AreEqual(store.getManagers().Count, 2);
        }
        [TestMethod]
        public void removeStoreMannegerWithManagerPermission()
        {
            zahiOwner.addStoreManager(zahi,store,"shay");
            zahiOwner.addManagerPermission(zahi, "removeStoreManager", store, "shay");
            StoreManager shaymanneger = new StoreManager(shay, store);
            Assert.IsTrue(shaymanneger.removeStoreManager(shay, store, "aviad") > -1);
            Assert.AreEqual(store.getManagers().Count, 1);
        }
        [TestMethod]
        public void addStoreMannegerWithManagerPermissionAndRemoveTheOneWhoAddHim()
        {
            zahiOwner.addManagerPermission(zahi, "addStoreManager", store, "aviad");
            Assert.IsTrue(aviadManeger.addStoreManager(aviad, store, "shay") > -1);
            Assert.AreEqual(store.getManagers().Count, 2);
            zahiOwner.addManagerPermission(zahi, "removeStoreManager", store, "shay");
            StoreManager shaymanneger = new StoreManager(shay, store);
            Assert.IsTrue(shaymanneger.removeStoreManager(shay, store, "aviad") > -1);
            Assert.AreEqual(store.getManagers().Count, 1);
        }
        [TestMethod]
        public void removeStoreMannegerWithManagerPermissionUserNotExist()
        {
            zahiOwner.addStoreManager(zahi, store, "shay");
            zahiOwner.addManagerPermission(zahi, "removeStoreManager", store, "shay");
            StoreManager shaymanneger = new StoreManager(shay, store);
            Assert.IsFalse(shaymanneger.removeStoreManager(shay, store, "niv") > -1);
            Assert.AreEqual(store.getManagers().Count, 2);
        }
        [TestMethod]
        public void addManagerPermission()
        {
            zahiOwner.addManagerPermission(zahi, "addManagerPermission", store, "aviad");
            Assert.IsTrue(zahiOwner.addStoreManager(zahi, store, "shay") > -1);
            Assert.AreEqual(store.getManagers().Count, 2);
            aviadManeger.addManagerPermission(aviad, "removeStoreManager", store, "shay");
            StoreManager shaymanneger = new StoreManager(shay, store);
            Assert.IsTrue(shaymanneger.removeStoreManager(shay, store, "aviad") > -1);
            Assert.AreEqual(store.getManagers().Count, 1);
        }
        [TestMethod]
        public void addManagerPermissionToHimself()
        {
            zahiOwner.addManagerPermission(zahi, "addManagerPermission", store, "aviad");
            Assert.IsTrue(zahiOwner.addStoreManager(zahi, store, "shay") > -1);
            Assert.AreEqual(store.getManagers().Count, 2);
            Assert.IsTrue(aviadManeger.addManagerPermission(aviad, "removeStoreManager", store, "aviad") > -1);
            Assert.IsTrue(aviadManeger.removeStoreManager(aviad, store, "shay") > -1);
            Assert.AreEqual(store.getManagers().Count, 1);
        }
        [TestMethod]
        public void removeManagerPermission()
        {
            zahiOwner.addManagerPermission(zahi, "removeManagerPermission", store, "aviad");
            Assert.IsTrue(zahiOwner.addStoreManager(zahi, store, "shay") > -1);
            Assert.AreEqual(store.getManagers().Count, 2);
            zahiOwner.addManagerPermission(zahi, "addStoreManager", store, "shay");
            StoreManager shaymanneger = new StoreManager(shay, store);
            shaymanneger.addStoreManager(shay, store, "niv");
            Assert.AreEqual(store.getManagers().Count, 3);
            zahiOwner.removeStoreManager(zahi,  store, "niv");
            Assert.AreEqual(store.getManagers().Count, 2);
            Assert.IsTrue(aviadManeger.removeManagerPermission(aviad, "addStoreManager", store, "shay") > -1);
            Assert.IsFalse(shaymanneger.addStoreManager(shay, store, "niv") > -1);
            Assert.AreEqual(store.getManagers().Count, 2);
        }
        [TestMethod]
        public void removeManagerPermissionToHimself()
        {
            zahiOwner.addManagerPermission(zahi, "removeManagerPermission", store, "aviad");
            Assert.IsTrue(zahiOwner.addStoreManager(zahi, store, "shay") > -1);
            Assert.AreEqual(store.getManagers().Count, 2);
            zahiOwner.addManagerPermission(zahi, "addStoreManager", store, "shay");
            StoreManager shaymanneger = new StoreManager(shay, store);
            shaymanneger.addStoreManager(shay, store, "niv");
            Assert.AreEqual(store.getManagers().Count, 3);
            zahiOwner.removeStoreManager(zahi, store, "niv");
            Assert.AreEqual(store.getManagers().Count, 2);
            Assert.IsTrue(aviadManeger.removeManagerPermission(aviad, "removeManagerPermission", store, "aviad") > -1);
            Assert.IsFalse(aviadManeger.removeManagerPermission(aviad, "addStoreManager", store, "shay") > -1);
            Assert.IsTrue(shaymanneger.addStoreManager(shay, store, "niv") > -1);
            Assert.AreEqual(store.getManagers().Count, 3);
        }
        [TestMethod]
        public void removeManagerPermissionTwice()
        {
            zahiOwner.addManagerPermission(zahi, "removeManagerPermission", store, "aviad");
            Assert.IsTrue(zahiOwner.addStoreManager(zahi, store, "shay") > -1);
            Assert.AreEqual(store.getManagers().Count, 2);
            zahiOwner.addManagerPermission(zahi, "addStoreManager", store, "shay");
            StoreManager shaymanneger = new StoreManager(shay, store);
            shaymanneger.addStoreManager(shay, store, "niv");
            Assert.AreEqual(store.getManagers().Count, 3);
            zahiOwner.removeStoreManager(zahi, store, "niv");
            Assert.AreEqual(store.getManagers().Count, 2);
            Assert.IsTrue(aviadManeger.removeManagerPermission(aviad, "addStoreManager", store, "shay") > -1);
            Assert.IsTrue(aviadManeger.removeManagerPermission(aviad, "addStoreManager", store, "shay") > -1);
            Assert.IsFalse(shaymanneger.addStoreManager(shay, store, "niv") > -1);
            Assert.AreEqual(store.getManagers().Count, 2);
        }
        [TestMethod]
        public void viewPurchasesHistoryPremition()
        {
            zahiOwner.addManagerPermission(zahi, "viewPurchasesHistory", store, "aviad");
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 1) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCart();
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.First.Value.getSaleId() == saleId);
            Assert.IsTrue(aviad.buyProducts("1234", ""));
            Assert.AreEqual(aviad.viewStoreHistory(store).Count, 1);    
        }
        [TestMethod]
        public void viewPurchasesHistoryPremitionwithoutpremition()
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
            Assert.IsNull(aviad.viewStoreHistory(store));
        }
        [TestMethod]
        public void removeSaleeWithManagerPermission()
        {
            zahiOwner.addManagerPermission(zahi, "removeSaleFromStore", store, "aviad");
            int saleId=zahiOwner.addSaleToStore(aviad, store, cola.getProductInStoreId(), 1, 1, DateTime.Now.AddMonths(1).ToString());
            Assert.AreEqual(store.getAllSales().Count, 1);
            Assert.IsTrue(aviadManeger.removeSaleFromStore(aviad, store, saleId) > -1);
            Assert.AreEqual(store.getAllSales().Count, 0);
        }
        [TestMethod]
        public void removeSaleeWithManagerPermissionNotExistSale()
        {
            zahiOwner.addManagerPermission(zahi, "removeSaleFromStore", store, "aviad");
            int saleId = zahiOwner.addSaleToStore(aviad, store, cola.getProductInStoreId(), 1, 1, DateTime.Now.AddMonths(1).ToString());
            Assert.AreEqual(store.getAllSales().Count, 1);
            Assert.IsFalse(aviadManeger.removeSaleFromStore(aviad, store, saleId+1) > -1);
            Assert.AreEqual(store.getAllSales().Count, 1);
        }
        [TestMethod]
        public void removeSaleeWithManagerPermissionTwice()
        {
            zahiOwner.addManagerPermission(zahi, "removeSaleFromStore", store, "aviad");
            int saleId = zahiOwner.addSaleToStore(aviad, store, cola.getProductInStoreId(), 1, 1, DateTime.Now.AddMonths(1).ToString());
            Assert.AreEqual(store.getAllSales().Count, 1);
            Assert.IsTrue(aviadManeger.removeSaleFromStore(aviad, store, saleId) > -1);
            Assert.AreEqual(store.getAllSales().Count, 0);
            Assert.IsFalse(aviadManeger.removeSaleFromStore(aviad, store, saleId + 1) > -1);
            Assert.AreEqual(store.getAllSales().Count, 0);
        }
        [TestMethod]
        public void EditeSaleeWithManagerPermission()
        {
            zahiOwner.addManagerPermission(zahi, "editSale", store, "aviad");
            int saleId = zahiOwner.addSaleToStore(aviad, store, cola.getProductInStoreId(), 1, 1, DateTime.Now.AddMonths(1).ToString());
            Assert.AreEqual(store.getAllSales().Count, 1);
            Assert.IsTrue(aviadManeger.editSale(aviad, store, saleId, 2, DateTime.Now.AddMonths(1).ToString()) > -1);
            Assert.AreEqual(store.getAllSales().Count, 1);
            Assert.AreEqual(store.getAllSales().First.Value.Amount,2);
        }
        [TestMethod]
        public void EditeSaleeWithManagerPermissionTwice()
        {
            zahiOwner.addManagerPermission(zahi, "editSale", store, "aviad");
            int saleId = zahiOwner.addSaleToStore(aviad, store, cola.getProductInStoreId(), 1, 1, DateTime.Now.AddMonths(1).ToString());
            Assert.AreEqual(store.getAllSales().Count, 1);
            Assert.IsTrue(aviadManeger.editSale(aviad, store, saleId, 2, DateTime.Now.AddMonths(1).ToString()) > -1);
            Assert.AreEqual(store.getAllSales().First.Value.Amount, 2);
            Assert.IsTrue(aviadManeger.editSale(aviad, store, saleId, 3, DateTime.Now.AddMonths(1).ToString()) > -1);
            Assert.AreEqual(store.getAllSales().Count, 1);
            Assert.AreEqual(store.getAllSales().First.Value.Amount, 3);
        }
        [TestMethod]
        public void EditeSaleeWithManagerPermissionWrongeValue()
        {
            zahiOwner.addManagerPermission(zahi, "editSale", store, "aviad");
            int saleId = zahiOwner.addSaleToStore(aviad, store, cola.getProductInStoreId(), 1, 1, DateTime.Now.AddMonths(1).ToString());
            Assert.AreEqual(store.getAllSales().Count, 1);
            Assert.IsTrue(aviadManeger.editSale(aviad, store, saleId, 2, DateTime.Now.AddMonths(1).ToString()) > -1);
            Assert.AreEqual(store.getAllSales().First.Value.Amount, 2);
            Assert.IsFalse(aviadManeger.editSale(aviad, store, saleId, -4, DateTime.Now.AddMonths(1).ToString()) > -1);
            Assert.AreEqual(store.getAllSales().Count, 1);
            Assert.AreEqual(store.getAllSales().First.Value.Amount, 2);
        }



    }
}
