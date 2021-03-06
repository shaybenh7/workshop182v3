﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;
using WebServices.DAL;

namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class removeCouponTests
    {

        private userServices us;
        private storeServices ss;
        private CouponsManager ca;
        private User zahi;
        private Store store;//itamar owner , niv manneger
        private ProductInStore cola;
        private Sale colaSale;

        [TestInitialize]
        public void init()
        {
            CleanDB cDB = new CleanDB();
            cDB.emptyDB();
            ProductManager.restartInstance();
            SalesManager.restartInstance();
            StoreManagement.restartInstance();
            UserManager.restartInstance();
            UserCartsManager.restartInstance();
            BuyHistoryManager.restartInstance();
            CouponsManager.restartInstance();
            DiscountsManager.restartInstance();
            RaffleSalesManager.restartInstance();
            StorePremissionsArchive.restartInstance();

            us = userServices.getInstance();
            ss = storeServices.getInstance();
            ca = CouponsManager.getInstance();

            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");

            int storeid = ss.createStore("abowim", zahi);
            Store store = StoreManagement.getInstance().getStore(storeid);

            int colaId = ss.addProductInStore("cola", 10, 100, zahi, storeid, "Drinks");
            cola = ProductManager.getInstance().getProductInStore(colaId);

            ss.addSaleToStore(zahi, store.getStoreId(), cola.getProductInStoreId(), 1, 2, "20/8/2018");

            LinkedList<Sale> SL = ss.viewSalesByStore(store.getStoreId());
            foreach (Sale sale in SL)
            {
                if (sale.ProductInStoreId == cola.getProductInStoreId())
                {
                    colaSale = sale;
                }
            }
            ss.addCouponDiscount(zahi, store, "copun", cola, 10, "20/6/2018");
        }

        [TestMethod]
        public void RemoveCouponWithNullStore()
        {
            Assert.IsFalse(ss.removeCoupon(null, zahi, "copun"));
            Coupon c = ca.getCoupon("copun", cola.getProductInStoreId());
            Assert.AreEqual(c.DueDate, "20/6/2018");
            Assert.AreEqual(c.CouponId, "copun");
            Assert.AreEqual(c.Percentage, 10);
        }

        [TestMethod]
        public void RemoveCouponWithNullSession()
        {
            Assert.IsFalse(ss.removeCoupon(store, null, "copun"));
            Coupon c = ca.getCoupon("copun", cola.getProductInStoreId());
            Assert.AreEqual(c.DueDate, "20/6/2018");
            Assert.AreEqual(c.CouponId, "copun");
            Assert.AreEqual(c.Percentage, 10);
        }


        [TestMethod]
        public void RemoveCouponWithNullCopunId()
        {
            Assert.IsFalse(ss.removeCoupon(store, zahi, null));
            Coupon c = ca.getCoupon("copun", cola.getProductInStoreId());
            Assert.AreEqual(c.DueDate, "20/6/2018");
            Assert.AreEqual(c.CouponId, "copun");
            Assert.AreEqual(c.Percentage, 10);
        }

        [TestMethod]
        public void RemoveCouponWithWrongCopunId()
        {
            Assert.IsFalse(ss.removeCoupon(store, zahi, "copun1"));
            Coupon c = ca.getCoupon("copun", cola.getProductInStoreId());
            Assert.AreEqual(c.DueDate, "20/6/2018");
            Assert.AreEqual(c.CouponId, "copun");
            Assert.AreEqual(c.Percentage, 10);
        }

        [TestMethod]
        public void RemoveCouponWithWrongCopunIdWithCapitalLetter()
        {
            Assert.IsFalse(ss.removeCoupon(store, zahi, "copuN"));
            Coupon c = ca.getCoupon("copun", cola.getProductInStoreId());
            Assert.AreEqual(c.DueDate, "20/6/2018");
            Assert.AreEqual(c.CouponId, "copun");
            Assert.AreEqual(c.Percentage, 10);
        }

        [TestMethod]
        public void RemoveCouponWithWrongCopunIdWithCapitalLetter2()
        {
            Assert.IsFalse(ss.removeCoupon(store, zahi, "Copun"));
            Coupon c = ca.getCoupon("copun", cola.getProductInStoreId());
            Assert.AreEqual(c.DueDate, "20/6/2018");
            Assert.AreEqual(c.CouponId, "copun");
            Assert.AreEqual(c.Percentage, 10);
        }
    }
}
