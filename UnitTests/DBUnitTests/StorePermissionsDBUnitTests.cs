using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using WebServices.DAL;
using WebServices.Domain;
using System.Collections.Generic;


namespace UnitTests.DBUnitTests
{
    [TestClass]
    public class StorePermissionsDBUnitTests
    {

        string testing = "Testing";
        StorePremissionsDB SPDB;
        LinkedList<Tuple<int,String,String>> li;

        [TestInitialize]
        public void init()
        {
            WebServices.DAL.CleanDB cDB = new WebServices.DAL.CleanDB();
            cDB.emptyDB();
            configuration.DB_MODE = testing;
            SPDB = new StorePremissionsDB(testing);
            li = new LinkedList<Tuple<int, String, String>>(); // storeId, username, permission
            SPDB.Add(Tuple.Create(1, "itamar", "AddSales"));
        }

        [TestMethod]
        public void AddStorePermission()
        {
            try
            {
                var toAdd = Tuple.Create(1, "aviad", "DeleteSales");
                SPDB.Add(toAdd);
                li = SPDB.Get();
                Assert.AreEqual(li.Count, 2);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }

        }
        [TestMethod]
        public void RemoveStorePermission()
        {
            try
            {
                var toRemove = Tuple.Create(1, "itamar", "AddSales");
                SPDB.Remove(toRemove);
                li = SPDB.Get();
                Assert.AreEqual(li.Count, 0);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void GetStorePermission()
        {
            try
            {
                var toAdd1 = Tuple.Create(1, "aviad", "AddDiscount");
                var toAdd2 = Tuple.Create(1, "shay", "AddCoupon");
                var toAdd3 = Tuple.Create(1, "itamar", "RemoveCoupon");
                SPDB.Add(toAdd1);
                SPDB.Add(toAdd2);
                SPDB.Add(toAdd3);
                li = SPDB.Get();
                Assert.AreEqual(li.Count, 4);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
    }
}
