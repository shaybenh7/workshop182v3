using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using WebServices.DAL;
using WebServices.Domain;
using System.Collections.Generic;

namespace UnitTests.DBUnitTests
{
    [TestClass]
    public class CouponDBUnitTests
    {
        string testing = "Testing";
        couponDB couponDB;
        LinkedList<Coupon> li;

        [TestInitialize]
        public void init()
        {
            WebServices.DAL.CleanDB cDB = new WebServices.DAL.CleanDB();
            cDB.emptyDB();
            configuration.DB_MODE = testing;
            couponDB = new couponDB(testing);
            li = new LinkedList<Coupon>();
            couponDB.Add(new Coupon("hey", 1, 50, "02/02/2020"));
        }

        [TestMethod]
        public void AddCoupon()
        {
            try
            {
                Coupon toAdd = new Coupon("blah", 2, 40, "02/12/2030");
                couponDB.Add(toAdd);
                li = couponDB.Get();
                Assert.AreEqual(li.Count, 2);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void RemoveCoupon()
        {
            try
            {
                Coupon toRemove = new Coupon("hey", 1, 50, "02/02/2020");
                couponDB.Remove(toRemove);
                li = couponDB.Get();
                Assert.AreEqual(li.Count, 0);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void GetCoupon()
        {
            try
            {
                Coupon toAdd1 = new Coupon("hey1", 2, 10, "02/02/2020");
                Coupon toAdd2 = new Coupon("hey2", 3, 20, "02/02/2020");
                Coupon toAdd3 = new Coupon("hey3", 4, 30, "02/02/2020");
                Coupon toAdd4 = new Coupon("hey4", 5, 40, "02/02/2020");
                couponDB.Add(toAdd1);
                couponDB.Add(toAdd2);
                couponDB.Add(toAdd3);
                couponDB.Add(toAdd4);
                li = couponDB.Get();
                Assert.AreEqual(li.Count, 5);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
    }
}
