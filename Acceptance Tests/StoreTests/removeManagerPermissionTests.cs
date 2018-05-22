using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using wsep182.Domain;
using wsep182.services;
namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class removeManagerPermissionTests
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
                store = storeArchive.getInstance().getStore(ss.createStore("abowim", zahi));
                ss.addStoreManager(store.getStoreId(), "aviad", zahi);
                niv.logOut();
                //ADD ALL PERMISSIONS
                ss.addManagerPermission("addProductInStore", store.getStoreId(), "aviad", zahi);
                ss.addManagerPermission("editProductInStore", store.getStoreId(), "aviad", zahi);
                ss.addManagerPermission("removeProductFromStore", store.getStoreId(), "aviad", zahi);
                ss.addManagerPermission("addStoreManager", store.getStoreId(), aviad.getUserName(), zahi);
                ss.addManagerPermission("removeStoreManager", store.getStoreId(), aviad.getUserName(), zahi);
                ss.addManagerPermission("addManagerPermission", store.getStoreId(), aviad.getUserName(), zahi);
                ss.addManagerPermission("removeManagerPermission", store.getStoreId(), aviad.getUserName(), zahi);
                ss.addManagerPermission("addSaleToStore", store.getStoreId(), aviad.getUserName(), zahi);
                ss.addManagerPermission("removeSaleFromStore", store.getStoreId(), aviad.getUserName(), zahi);
                ss.addManagerPermission("editSale", store.getStoreId(), aviad.getUserName(), zahi);
                ss.addManagerPermission("addDiscount", store.getStoreId(), aviad.getUserName(), zahi);
                ss.addManagerPermission("removeDiscount", store.getStoreId(), aviad.getUserName(), zahi);
                ss.addManagerPermission("addNewCoupon", store.getStoreId(), aviad.getUserName(), zahi);
                ss.addManagerPermission("removeCoupon", store.getStoreId(), aviad.getUserName(), zahi);
                
        }


        [TestMethod]
            public void addProductInStore()
            {
                ss.removeManagerPermission("addProductInStore", store.getStoreId(), "aviad", zahi);
                ss.addProductInStore("cola", 10, 4, aviad, store.getStoreId(), "Drink");
                Assert.AreEqual(0, store.getProductsInStore().Count);
            }

            [TestMethod]
            public void editProductInStoreWithManagerPermission()
            {
            
                ProductInStore pis = ProductArchive.getInstance().getProductInStore(ss.addProductInStore("cola", 10, 4, zahi, store.getStoreId(), "Drink"));
                Assert.AreEqual(1, store.getProductsInStore().Count);
                ss.removeManagerPermission("editProductInStore", store.getStoreId(), "aviad", zahi);
                ss.editProductInStore(aviad, store.getStoreId(), pis.getProductInStoreId(), 13, 4.5);
                Assert.AreEqual(10, pis.getPrice());
                Assert.AreEqual(4, pis.getAmount());
            }
            [TestMethod]
            public void removeProductFromStoreWithManagerPermission()
            {
                ss.removeManagerPermission("removeProductFromStore", store.getStoreId(), "aviad", zahi);
                ProductInStore pis = ProductArchive.getInstance().getProductInStore(ss.addProductInStore("cola", 10, 4, zahi, store.getStoreId(), "Drink"));
                ss.removeProductFromStore(store.getStoreId(), pis.getProductInStoreId(), aviad);
                Assert.AreEqual(1, store.getProductsInStore().Count);
            }

            [TestMethod]
            public void addStoreManagerWithManagerPermission()
            {
                User newManager;
                newManager = us.startSession();
                us.register(newManager, "newManager", "123456");
                us.login(newManager, "newManager", "123456");
                ss.removeManagerPermission("addStoreManager", store.getStoreId(), aviad.getUserName(), zahi);
                ss.addStoreManager(store.getStoreId(), newManager.getUserName(), aviad);
                Assert.AreEqual(1, store.getManagers().Count);
            }
            [TestMethod]
            public void removeStoreManagerWithManagerPermission()
            {
                User newManager;
                newManager = us.startSession();
                us.register(newManager, "newManager", "123456");
                us.login(newManager, "newManager", "123456");
                ss.addStoreManager(store.getStoreId(), newManager.getUserName(), aviad);
                Assert.AreEqual(2, store.getManagers().Count);
                ss.removeManagerPermission("removeStoreManager", store.getStoreId(), aviad.getUserName(), zahi);
                ss.removeStoreManager(store.getStoreId(), newManager.getUserName(), aviad);
                Assert.AreEqual(2, store.getManagers().Count);
            }
            [TestMethod]
            public void addManagerPermissionWithManagerPermission()
            {
                User newManager;
                newManager = us.startSession();
                us.register(newManager, "newManager", "123456");
                us.login(newManager, "newManager", "123456");
                ss.removeManagerPermission("addManagerPermission", store.getStoreId(), aviad.getUserName(), zahi);
                ss.addStoreManager(store.getStoreId(), newManager.getUserName(), zahi);
                ss.addProductInStore("cola", 10, 4, newManager, store.getStoreId(), "Drink");
                Assert.AreEqual(0, store.getProductsInStore().Count);
                ss.addManagerPermission("addProductInStore", store.getStoreId(), newManager.getUserName(), aviad);
                ss.addProductInStore("cola", 10, 4, newManager, store.getStoreId(), "Drink");
                Assert.AreEqual(0, store.getProductsInStore().Count);
            }
            [TestMethod]
            public void removeManagerPermissionWithManagerPermission()
            {
                User newManager;
                newManager = us.startSession();
                us.register(newManager, "newManager", "123456");
                us.login(newManager, "newManager", "123456");
                ss.addStoreManager(store.getStoreId(), newManager.getUserName(), zahi);
                ss.addManagerPermission("addProductInStore", store.getStoreId(), newManager.getUserName(), aviad);
                ss.addProductInStore("cola", 10, 4, newManager, store.getStoreId(), "Drink");
                Assert.AreEqual(1, store.getProductsInStore().Count);
                ss.removeManagerPermission("removeManagerPermission", store.getStoreId(), aviad.getUserName(), zahi);
                ss.removeManagerPermission("addProductInStore", store.getStoreId(), newManager.getUserName(), aviad);
                ss.addProductInStore("cola2", 10, 4, newManager, store.getStoreId(), "Drink");
                Assert.AreEqual(2, store.getProductsInStore().Count);
            }

            [TestMethod]
            public void addSaleToStoreWithManagerPermission()
            {
                ProductInStore pis = ProductArchive.getInstance().getProductInStore(ss.addProductInStore("cola", 10, 4, zahi, store.getStoreId(), "Drink"));
                ss.removeManagerPermission("addSaleToStore", store.getStoreId(), aviad.getUserName(), zahi);
                int saleId = ss.addSaleToStore(aviad, store.getStoreId(), pis.getProductInStoreId(), 1, 100, DateTime.Now.AddYears(1).ToString());
                Assert.AreEqual(-4, saleId);//-4 dont have premition
            }

            [TestMethod]
            public void removeSaleFromStoreWithManagerPermission()
            {
                ProductInStore pis = ProductArchive.getInstance().getProductInStore(ss.addProductInStore("cola", 10, 4, zahi, store.getStoreId(), "Drink"));
                int saleId = ss.addSaleToStore(aviad, store.getStoreId(), pis.getProductInStoreId(), 1, 3, DateTime.Now.AddYears(1).ToString());
                Assert.AreEqual(saleId, SalesArchive.getInstance().getSalesByProductInStoreId(pis.getProductInStoreId()).First.Value.SaleId);
                ss.removeManagerPermission("removeSaleFromStore", store.getStoreId(), aviad.getUserName(), zahi);
                int deleted = ss.removeSaleFromStore(aviad, store.getStoreId(), saleId);
                Assert.AreEqual(deleted, -4);//-4 dont have premition

        }

            [TestMethod]
            public void editSaleWithManagerPermission()
            {
                ProductInStore pis = ProductArchive.getInstance().getProductInStore( ss.addProductInStore("cola", 100, 100, zahi, store.getStoreId(), "Drink"));
                int saleId = ss.addSaleToStore(zahi, store.getStoreId(), pis.getProductInStoreId(), 1, 40, DateTime.Now.AddYears(1).ToString());
                Assert.AreEqual(saleId, SalesArchive.getInstance().getSalesByProductInStoreId(pis.getProductInStoreId()).First.Value.SaleId);
                ss.removeManagerPermission("editSale", store.getStoreId(), aviad.getUserName(), zahi);
                int edited = ss.editSale(aviad, store.getStoreId(), saleId, 30, DateTime.Now.AddYears(1).ToString());
                Assert.AreEqual(40, SalesArchive.getInstance().getSale(saleId).Amount);
                Assert.AreEqual(edited, -4);// -4 dont have premition
        }

        [TestMethod]
            public void addDiscountWithManagerPermission()
            {
                ProductInStore pis = ProductArchive.getInstance().getProductInStore(ss.addProductInStore("cola", 150, 100, zahi, store.getStoreId(), "Drink"));
                int saleId = ss.addSaleToStore(zahi, store.getStoreId(), pis.getProductInStoreId(), 1, 40, DateTime.Now.AddYears(1).ToString());
                ss.removeManagerPermission("addDiscount", store.getStoreId(), aviad.getUserName(), zahi);
                Assert.AreEqual(0, DiscountsArchive.getInstance().getAllDiscounts().Count);
                
            }

            [TestMethod]
            public void removeDiscountWithManagerPermission()
            {
                ProductInStore pis = ProductArchive.getInstance().getProductInStore(ss.addProductInStore("cola", 150, 100, zahi, store.getStoreId(), "Drink"));
                int saleId = ss.addSaleToStore(zahi, store.getStoreId(), pis.getProductInStoreId(), 1, 40, DateTime.Now.AddYears(1).ToString());
                ss.removeManagerPermission("removeDiscount", store.getStoreId(), aviad.getUserName(), zahi);
                Assert.IsTrue(ss.addDiscount(pis, 10, DateTime.Now.AddDays(1).ToString(), aviad, store));
                Boolean removed = ss.removeDiscount(pis, store, aviad);
                Assert.AreEqual(1, DiscountsArchive.getInstance().getAllDiscounts().Count);
                Assert.AreEqual(false, removed);
            }

            [TestMethod]
            public void addCouponWithManagerPermission()
            {
                ProductInStore pis = ProductArchive.getInstance().getProductInStore(ss.addProductInStore("cola", 150, 100, zahi, store.getStoreId(), "Drink"));
                ss.removeManagerPermission("addNewCoupon", store.getStoreId(), aviad.getUserName(), zahi);
                Boolean added = ss.addCouponDiscount(aviad, store, "coupon", pis, 10, DateTime.Now.AddYears(1).ToString());
                Assert.AreEqual(false, added);
            }

            [TestMethod]
            public void removeCouponWithManagerPermission()
            {
                ProductInStore pis = ProductArchive.getInstance().getProductInStore(ss.addProductInStore("cola", 150, 100, zahi, store.getStoreId(), "Drink"));
                Boolean added = ss.addCouponDiscount(aviad, store, "coupon", pis, 10, DateTime.Now.AddYears(1).ToString());
                Assert.AreEqual(true, added);
                ss.removeManagerPermission("removeCoupon", store.getStoreId(), aviad.getUserName(), zahi);
                Assert.AreEqual(false, ss.removeCoupon(store, aviad, "coupon"));

            }

        }
    }
