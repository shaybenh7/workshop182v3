using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using WebServices.DAL;
using WebServices.Domain;
using System.Collections.Generic;

namespace UnitTests.DBUnitTests
{
    [TestClass]
    public class SaleDBUnitTests
    {
        string testing = "Testing";
        SaleDB saleDB;
        LinkedList<Sale> li;

        [TestInitialize]
        public void init()
        {
            WebServices.DAL.CleanDB cDB = new WebServices.DAL.CleanDB();
            cDB.emptyDB();
            configuration.DB_MODE = testing;
            saleDB = new SaleDB(testing);
            li = new LinkedList<Sale>();
            saleDB.Add(new Sale(1, 1, 1, 10, "20/02/2020"));
        }

        [TestMethod]
        public void AddSale()
        {
            try
            {
                Sale toAdd = new Sale(2, 2, 2, 20, "10/10/2010");
                saleDB.Add(toAdd);
                li = saleDB.Get();
                Assert.AreEqual(li.Count, 2);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void RemoveSale()
        {
            try
            {
                Sale toRemove = new Sale(1, 1, 1, 10, "20/02/2020");
                saleDB.Remove(toRemove);
                li = saleDB.Get();
                Assert.AreEqual(li.Count, 0);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void GetSale()
        {
            try
            {
                Sale toAdd1 = new Sale(2, 2, 1, 200, "01/01/2050");
                Sale toAdd2 = new Sale(3, 2, 1, 200, "01/01/2050");
                Sale toAdd3 = new Sale(4, 2, 1, 200, "01/01/2050");
                saleDB.Add(toAdd1);
                saleDB.Add(toAdd2);
                saleDB.Add(toAdd3);
                li = saleDB.Get();
                Assert.AreEqual(li.Count, 4);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
    }
}
