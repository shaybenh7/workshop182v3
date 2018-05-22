using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;


namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class addCouponDiscountTests
    {

        private userServices us;
        private storeServices ss;
        private CouponsArchive ca;
        private User zahi;
        private Store store;//itamar owner , niv manneger
        private int cola;
        private Sale colaSale;

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
            ca = CouponsArchive.getInstance();

            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");

            int s = ss.createStore("Abowim", zahi);
            store = storeArchive.getInstance().getStore(s);

            int c = ss.addProductInStore("cola", 10, 100, zahi, store.getStoreId(), "drinks");

            ss.addSaleToStore(zahi, store.getStoreId(), cola, 1, 2, "20/5/2018");

            LinkedList<Sale> SL = ss.viewSalesByStore(store.getStoreId());
            foreach (Sale sale in SL)
            {
                if (sale.ProductInStoreId == cola)
                {
                    colaSale = sale;
                }
            }
        }

        //addNewCoupons
        // type: 1 for discount on productsInStore, 2 for discount on category, 3 for discount on entire product (in product archive)
        [TestMethod]
        public void SimpleAddCouponOnCategory()
        {
            List<int> lst = new List<int>();
            lst.Add(cola);
            List<string> lst2 = new List<string>();
            lst2.Add("drinks");


            Assert.IsTrue(ss.addNewCoupons(zahi, store.getStoreId(), "coupon", 2, lst, lst2, 10, "20/6/2018","")>-1);
            Coupon c = ca.getCoupon("coupon", cola);
            Assert.AreEqual(c.DueDate, "20/6/2018");
            Assert.AreEqual(c.CouponId, "coupon");
            Assert.AreEqual(c.Percentage, 10);
        }

        [TestMethod]
        public void SimpleAddCoupon()
        {
            List<int> lst = new List<int>();
            lst.Add(cola);
            int temp = ss.addNewCoupons(zahi, store.getStoreId(), "coupon", 1, lst, null, 10, "20/6/2018", "");
            Assert.IsTrue(temp > -1);

            Coupon c = ca.getCoupon("coupon", cola);
            Assert.AreEqual(c.DueDate, "20/6/2018");
            Assert.AreEqual(c.CouponId, "coupon");
            Assert.AreEqual(c.Percentage, 10);
        }

        [TestMethod]
        public void AddCouponWithNullId()
        {
            List<int> lst = new List<int>();
            lst.Add(cola);
            int temp = ss.addNewCoupons(zahi, store.getStoreId(), null, 1, lst, null, 10, "20/6/2018", "");
            Assert.IsFalse(temp > -1);
            Assert.IsNull(ca.getCoupon("coupon", cola));
        }

        [TestMethod]
        public void AddCouponWithNullproduct()
        {
            int temp = ss.addNewCoupons(zahi, store.getStoreId(), "coupon", 1, null, null, 10, "20/6/2018", "");
            Assert.IsFalse(temp > -1);
            Assert.IsNull(ca.getCoupon("coupon", cola));
        }

        [TestMethod]
        public void AddCouponWithzeroPrecent()
        {
            List<int> lst = new List<int>();
            lst.Add(cola);
            Assert.IsFalse(ss.addNewCoupons(zahi, store.getStoreId(), "coupon", 1, lst, null, 0, "20/6/2018", "") > -1);
            Assert.IsNull(ca.getCoupon("coupon", cola));
        }

        [TestMethod]
        public void AddCouponWithzNegPrecent()
        {
            List<int> lst = new List<int>();
            lst.Add(cola);
            int temp = ss.addNewCoupons(zahi, store.getStoreId(), "coupon", 1, lst, null, -10, "20/6/2018", "");
            Assert.IsFalse(temp > -1);
            Assert.IsNull(ca.getCoupon("coupon", cola));
        }

        [TestMethod]
        public void AddCouponWithzNullDueDate()
        {
            List<int> lst = new List<int>();
            lst.Add(cola);
            Assert.IsFalse(ss.addNewCoupons(zahi, store.getStoreId(), "coupon", 1, lst, null, 10, null, "") > -1);
            Assert.IsNull(ca.getCoupon("coupon", cola));
        }

        [TestMethod]
        public void AddCouponWithzDueDateAllreadyPassed()
        {
            List<int> lst = new List<int>();
            lst.Add(cola);
            Assert.IsFalse(ss.addNewCoupons(zahi, store.getStoreId(), "coupon", 1, lst, null, 10, DateTime.Now.AddDays(-1).ToString(), "") > -1);
            Assert.IsNull(ca.getCoupon("coupon", cola));
        }
    }
}
