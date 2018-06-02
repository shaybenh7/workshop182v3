using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using WebServices.DAL;
using WebServices.Domain;
using System.Collections.Generic;

namespace UnitTests.DBUnitTests
{
    [TestClass]
    public class RaffleSaleDBUnitTests
    {
        string testing = "Testing";
        RaffleSaleDB raffDB;
        LinkedList<RaffleSale> li;

        [TestInitialize]
        public void init()
        {
            WebServices.DAL.CleanDB cDB = new WebServices.DAL.CleanDB();
            cDB.emptyDB();
            configuration.DB_MODE = testing;
            raffDB = new RaffleSaleDB(testing);
            li = new LinkedList<RaffleSale>();
            raffDB.Add(new RaffleSale(1, "itamar", 500, "02/02/2020"));
        }
        [TestMethod]
        public void AddRaffleSale()
        {
            try
            {
                RaffleSale toAdd = new RaffleSale(2, "aviad", 100, "03/03/3000");
                raffDB.Add(toAdd);
                li = raffDB.Get();
                Assert.AreEqual(li.Count, 2);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void RemoveRaffleSale()
        {
            try
            {
                RaffleSale toRemove = new RaffleSale(1, "itamar", 500, "02/02/2020");
                raffDB.Remove(toRemove);
                li = raffDB.Get();
                Assert.AreEqual(li.Count, 0);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void GetRaffleSale()
        {
            try
            {
                li = raffDB.Get();
                Assert.AreEqual(li.Count, 1);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
    }
}
