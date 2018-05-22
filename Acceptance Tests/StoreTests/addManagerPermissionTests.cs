using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{

    /*
      "addProductInStore":
       "removeProductFromStore":
       "addStoreManager":
       "removeStoreManager":
       "addManagerPermission":
       "removeManagerPermission":
    */

    [TestClass]
    public class addManagerPermissionTests
    {
        private userServices us;
        private storeServices ss;
        private User zahi;  // owner of store
        private User aviad; //manager of store
        private User shay;
        private User itamar; // not a real user
        private User niv; // guest
        private Store store;

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
            us = userServices.getInstance();
            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");
            aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            shay = us.startSession();
            us.register(shay, "shay", "123456");
            us.login(shay, "shay", "123456");
            itamar = new User("itamar", "123456");
            niv = us.startSession();
            us.register(niv, "niv", "123456");
            ss = storeServices.getInstance();
            int s = ss.createStore("abowim", zahi);
            store = storeArchive.getInstance().getStore(s);
            ss.addStoreManager(store.getStoreId(), "aviad", zahi);
            niv.logOut();
        }


        [TestMethod]
        public void addProductInStore()
        {
            ss.addProductInStore("cola", 10, 4, aviad, store.getStoreId(), "Drinks");
            Assert.AreEqual(0, store.getProductsInStore().Count);
            ss.addManagerPermission("addProductInStore", store.getStoreId(), "aviad", zahi);
            ss.addProductInStore("cola", 10, 4, aviad, store.getStoreId(), "Drinks");
            Assert.AreEqual(1, store.getProductsInStore().Count);
        }

        [TestMethod]
        public void editProductInStoreWithManagerPermission()
        {
            int p=ss.addProductInStore("cola", 10, 4, zahi, store.getStoreId(), "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);
            Assert.AreEqual(1, store.getProductsInStore().Count);
            ss.editProductInStore(aviad, store.getStoreId(), pis.getProductInStoreId(), 13, 4.5);
            Assert.AreEqual(10, pis.getPrice());
            Assert.AreEqual(4, pis.getAmount());
            ss.addManagerPermission("editProductInStore", store.getStoreId(), "aviad", zahi);
            ss.editProductInStore(aviad, store.getStoreId(), pis.getProductInStoreId(), 13, 4.5);
            Assert.AreEqual(4.5, pis.getPrice());
            Assert.AreEqual(13, pis.getAmount());
        }
        [TestMethod]
        public void removeProductFromStoreWithManagerPermission()
        {
            int p = ss.addProductInStore("cola", 10, 4, zahi, store.getStoreId(), "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);
            ss.removeProductFromStore(store.getStoreId(), pis.getProductInStoreId(), aviad);
            Assert.AreEqual(1, store.getProductsInStore().Count);
            ss.addManagerPermission("removeProductFromStore", store.getStoreId(), "aviad", zahi);
            ss.removeProductFromStore(store.getStoreId(), pis.getProductInStoreId(), aviad);
            Assert.AreEqual(0, store.getProductsInStore().Count);
        }
        [TestMethod]
        public void tryoToAddStoreOwnerWithManagerPermission()
        {
            User newOwner;
            newOwner = us.startSession();
            us.register(newOwner, "newOwner", "123456");
            us.login(newOwner, "newOwner", "123456");
            Assert.AreEqual(1, store.getOwners().Count);
            ss.addManagerPermission("addStoreOwner", store.getStoreId(), aviad.getUserName(), zahi);
            ss.addStoreOwner(store.getStoreId(), newOwner.getUserName(), aviad);
            Assert.AreEqual(1, store.getOwners().Count);
        }
        [TestMethod]
        public void tryoToRemoveStoreOwnerWithManagerPermission()
        {
            User newOwner;
            newOwner = us.startSession();
            us.register(newOwner, "newOwner", "123456");
            us.login(newOwner, "newOwner", "123456");
            ss.addStoreOwner(store.getStoreId(), newOwner.getUserName(), zahi);
            Assert.AreEqual(2, store.getOwners().Count);
            ss.addManagerPermission("removeStoreOwner", store.getStoreId(), aviad.getUserName(), zahi);
            ss.removeStoreOwner(store.getStoreId(), newOwner.getUserName(), aviad);
            Assert.AreEqual(2, store.getOwners().Count);
        }
        [TestMethod]
        public void addStoreManagerWithManagerPermission()
        {
            User newManager;
            newManager = us.startSession();
            us.register(newManager, "newManager", "123456");
            us.login(newManager, "newManager", "123456");
            ss.addStoreManager(store.getStoreId(), newManager.getUserName(), aviad);
            Assert.AreEqual(1, store.getManagers().Count);
            ss.addManagerPermission("addStoreManager", store.getStoreId(), aviad.getUserName(), zahi);
            ss.addStoreManager(store.getStoreId(), newManager.getUserName(), aviad);
            Assert.AreEqual(2, store.getManagers().Count);
        }
        [TestMethod]
        public void removeStoreManagerWithManagerPermission()
        {
            User newManager;
            newManager = us.startSession();
            us.register(newManager, "newManager", "123456");
            us.login(newManager, "newManager", "123456");
            ss.addManagerPermission("addStoreManager", store.getStoreId(), aviad.getUserName(), zahi);
            ss.addStoreManager(store.getStoreId(), newManager.getUserName(), aviad);
            Assert.AreEqual(2, store.getManagers().Count);
            ss.removeStoreManager(store.getStoreId(), newManager.getUserName(), aviad);
            Assert.AreEqual(2, store.getManagers().Count);
            ss.addManagerPermission("removeStoreManager", store.getStoreId(), aviad.getUserName(), zahi);
            ss.removeStoreManager(store.getStoreId(), newManager.getUserName(), aviad);
            Assert.AreEqual(1, store.getManagers().Count);
        }
        [TestMethod]
        public void addManagerPermissionWithManagerPermission()
        {
            User newManager;
            newManager = us.startSession();
            us.register(newManager, "newManager", "123456");
            us.login(newManager, "newManager", "123456");
            ss.addManagerPermission("addManagerPermission", store.getStoreId(), aviad.getUserName(), zahi);
            ss.addStoreManager(store.getStoreId(), newManager.getUserName(), zahi);
            ss.addProductInStore("cola", 10, 4, newManager, store.getStoreId(), "Drinks");
            Assert.AreEqual(0, store.getProductsInStore().Count);
            ss.addManagerPermission("addProductInStore", store.getStoreId(), newManager.getUserName(), aviad);
            ss.addProductInStore("cola", 10, 4, newManager, store.getStoreId(), "Drinks");
            Assert.AreEqual(1, store.getProductsInStore().Count);
        }
        [TestMethod]
        public void removeManagerPermissionWithManagerPermission()
        {
            User newManager;
            newManager = us.startSession();
            us.register(newManager, "newManager", "123456");
            us.login(newManager, "newManager", "123456");
            ss.addManagerPermission("addManagerPermission", store.getStoreId(), aviad.getUserName(), zahi);
            ss.addManagerPermission("removeManagerPermission", store.getStoreId(), aviad.getUserName(), zahi);
            ss.addStoreManager(store.getStoreId(), newManager.getUserName(), zahi);
            ss.addManagerPermission("addProductInStore", store.getStoreId(), newManager.getUserName(), aviad);
            ss.addProductInStore("cola", 10, 4, newManager, store.getStoreId(), "Drinks");
            Assert.AreEqual(1, store.getProductsInStore().Count);
            ss.removeManagerPermission("addProductInStore", store.getStoreId(), newManager.getUserName(), aviad);
            ss.addProductInStore("cola2", 10, 4, newManager, store.getStoreId(), "Drinks");
            Assert.AreEqual(1, store.getProductsInStore().Count);
        }

        [TestMethod]
        public void addSaleToStoreWithManagerPermission()
        {
            int p = ss.addProductInStore("cola", 10, 4, zahi, store.getStoreId(), "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);
            int saleId = ss.addSaleToStore(aviad, store.getStoreId(), pis.getProductInStoreId(), 1, 100, DateTime.Now.AddYears(1).ToString());
            Assert.IsFalse(saleId>0);
            ss.addManagerPermission("addSaleToStore", store.getStoreId(), aviad.getUserName(), zahi);
            saleId = ss.addSaleToStore(aviad, store.getStoreId(), pis.getProductInStoreId(), 1, 3, DateTime.Now.AddYears(1).ToString());
            Sale sale = SalesArchive.getInstance().getSalesByProductInStoreId(pis.getProductInStoreId()).First.Value;
            Assert.AreEqual(saleId, sale.SaleId);
        }

        [TestMethod]
        public void removeSaleFromStoreWithManagerPermission()
        {
            int p = ss.addProductInStore("cola", 10, 4, zahi, store.getStoreId(), "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);
            ss.addManagerPermission("addSaleToStore", store.getStoreId(), aviad.getUserName(), zahi);
            int saleId = ss.addSaleToStore(aviad, store.getStoreId(), pis.getProductInStoreId(), 1, 3, DateTime.Now.AddYears(1).ToString());
            Assert.AreEqual(saleId, SalesArchive.getInstance().getSalesByProductInStoreId(pis.getProductInStoreId()).First.Value.SaleId);
            int deleted= ss.removeSaleFromStore(aviad, store.getStoreId(), saleId);
            Assert.AreEqual(deleted,-4);//-4 no premition
            ss.addManagerPermission("removeSaleFromStore", store.getStoreId(), aviad.getUserName(), zahi);
            deleted = ss.removeSaleFromStore(aviad, store.getStoreId(), saleId);
            Assert.AreEqual(deleted, 0);
            Assert.AreEqual(0, SalesArchive.getInstance().getAllSales().Count);
        }

        [TestMethod]
        public void editSaleWithManagerPermission()
        {
            int p = ss.addProductInStore("cola", 100, 100, zahi, store.getStoreId(), "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);
            int saleId = ss.addSaleToStore(zahi, store.getStoreId(), pis.getProductInStoreId(), 1, 40, DateTime.Now.AddYears(1).ToString());
            Assert.AreEqual(saleId, SalesArchive.getInstance().getSalesByProductInStoreId(pis.getProductInStoreId()).First.Value.SaleId);
            int edited = ss.editSale(aviad, store.getStoreId(), saleId,30, DateTime.Now.AddYears(1).ToString());
            Assert.AreEqual(40,SalesArchive.getInstance().getSale(saleId).Amount);
            Assert.AreEqual(edited, -4);//-4 if don't have premition
            ss.addManagerPermission("editSale", store.getStoreId(), aviad.getUserName(), zahi);
            edited = ss.editSale(aviad, store.getStoreId(), saleId, 30, DateTime.Now.AddYears(1).ToString());
            Assert.AreEqual(30,SalesArchive.getInstance().getSale(saleId).Amount);
            Assert.AreEqual(edited, 0);
        }

        [TestMethod]
        public void addDiscountWithManagerPermission()
        {
            int p = ss.addProductInStore("cola", 150, 100, zahi, store.getStoreId(), "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);
            int saleId = ss.addSaleToStore(zahi, store.getStoreId(), pis.getProductInStoreId(), 1, 40, "10.1.2019");
            Boolean added=ss.addDiscount(pis, 11, "20.1.2019", aviad, store);
            Assert.AreEqual(false, added);
            Assert.AreEqual(0, DiscountsArchive.getInstance().getAllDiscounts().Count);
            ss.addManagerPermission("addDiscount", store.getStoreId(), aviad.getUserName(), zahi);
            added = ss.addDiscount(pis, 11, "20.1.2019", aviad, store);
            Assert.AreEqual(true, added);
            Assert.AreEqual(1, DiscountsArchive.getInstance().getAllDiscounts().Count);
            Assert.AreEqual(133.5, SalesArchive.getInstance().getSale(saleId).getPriceAfterDiscount(1));
        }

        [TestMethod]
        public void removeDiscountWithManagerPermission()
        {
            int p = ss.addProductInStore("cola", 150, 100, zahi, store.getStoreId(), "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);

            int saleId = ss.addSaleToStore(zahi, store.getStoreId(), pis.getProductInStoreId(), 1, 40, "10.1.2019");
            ss.addManagerPermission("addDiscount", store.getStoreId(), aviad.getUserName(), zahi);
            Boolean added = ss.addDiscount(pis, 11, "20.1.2019", aviad, store);
            Assert.AreEqual(true, added);
            ss.removeDiscount(pis, store, aviad);
            Assert.AreEqual(1, DiscountsArchive.getInstance().getAllDiscounts().Count);
            ss.addManagerPermission("removeDiscount", store.getStoreId(), aviad.getUserName(), zahi);
            Boolean removed = ss.removeDiscount(pis, store, aviad);
            Assert.AreEqual(true, removed);
            Assert.AreEqual(0, DiscountsArchive.getInstance().getAllDiscounts().Count);
        }

        [TestMethod]
        public void addCouponWithManagerPermission()
        {
            int p = ss.addProductInStore("cola", 150, 100, zahi, store.getStoreId(), "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);
            Boolean added = ss.addCouponDiscount(aviad, store,"coupon" ,pis, 10, "10.1.2019");
            Assert.AreEqual(false, added);
            ss.addManagerPermission("addNewCoupon", store.getStoreId(), aviad.getUserName(), zahi);
            added = ss.addCouponDiscount(aviad, store, "coupon", pis, 10, "10.1.2019");
            Assert.AreEqual(true, added);
        }

        [TestMethod]
        public void removeCouponWithManagerPermission()
        {
            int p = ss.addProductInStore("cola", 150, 100, zahi, store.getStoreId(), "Drinks");
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(p);

            ss.addManagerPermission("addNewCoupon", store.getStoreId(), aviad.getUserName(), zahi);
            Boolean added = ss.addCouponDiscount(aviad, store, "coupon", pis, 10, "10.1.2019");
            Assert.AreEqual(true, added);
            //Coupon c = CouponsArchive.getInstance().getCoupon("coupon", pis.getProductInStoreId());
            //Assert.AreEqual(null, c);
            Assert.AreEqual(false, ss.removeCoupon(store, aviad, "coupon"));
            ss.addManagerPermission("removeCoupon", store.getStoreId(), aviad.getUserName(), zahi);
            Assert.AreEqual(true, ss.removeCoupon(store, aviad, "coupon"));

        }

    }
}
