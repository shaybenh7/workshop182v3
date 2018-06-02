using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using WebServices.DAL;
using WebServices.Domain;
using System.Collections.Generic;
namespace UnitTests.DBUnitTests
{
    [TestClass]
    public class DiscountDBUnitTests
    {
        string testing = "Testing";
        DiscountDB discountDB;
        LinkedList<Discount> li;

        [TestInitialize]
        public void init()
        {
            WebServices.DAL.CleanDB cDB = new WebServices.DAL.CleanDB();
            cDB.emptyDB();
            configuration.DB_MODE = testing;
            discountDB = new DiscountDB(testing);
            li = new LinkedList<Discount>();
            discountDB.Add(new Discount(1, 1, "", 10, "02/02/2020", ""));
        }

        [TestMethod]
        public void AddDiscount()
        {
            try
            {
                Discount toAdd = new Discount(2, 1, "", 20, "02/02/2030", "COUNTRY=ISRAEL");
                discountDB.Add(toAdd);
                li = discountDB.Get();
                Assert.AreEqual(li.Count, 2);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void RemoveDiscount()
        {
            try
            {
                Discount toRemove = new Discount(1, 1, "", 10, "02/02/2020", "");
                discountDB.Remove(toRemove);
                li = discountDB.Get();
                Assert.AreEqual(li.Count, 0);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void GetDiscount()
        {
            try
            {
                Discount toAdd1 = new Discount(3, 1, "", 60, "02/02/2030", "");
                Discount toAdd2 = new Discount(4, 1, "", 70, "02/02/2030", "");
                Discount toAdd3 = new Discount(5, 1, "", 80, "02/02/2030", "");
                Discount toAdd4 = new Discount(6, 1, "", 90, "02/02/2030", "");
                discountDB.Add(toAdd1);
                discountDB.Add(toAdd2);
                discountDB.Add(toAdd3);
                discountDB.Add(toAdd4);
                li = discountDB.Get();
                Assert.AreEqual(li.Count, 5);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }

        }
    }
}
