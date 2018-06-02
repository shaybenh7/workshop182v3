using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using WebServices.DAL;
using WebServices.Domain;
using System.Collections.Generic;

namespace UnitTests.DBUnitTests
{
    [TestClass]
    public class BuyHistoryDBUnitTests
    {
        string testing = "Testing";
        BuyHistoryDB buyHistoryDB;
        LinkedList<Purchase> li;

        [TestInitialize]
        public void init()
        {
            WebServices.DAL.CleanDB cDB = new WebServices.DAL.CleanDB();
            cDB.emptyDB();
            configuration.DB_MODE = testing;
            buyHistoryDB = new BuyHistoryDB(testing);
            buyHistoryDB.Add(new Purchase(1,1,1,"itamar",0.5,"02/02/2020",5,1));
            li = new LinkedList<Purchase>();
        }

        [TestMethod]
        public void AddBuyHistory()
        {
            try
            {
                Purchase toAdd = new Purchase(2, 2, 2, "aviad", 50, "05/05/2050", 6, 2);
                buyHistoryDB.Add(toAdd);
                li = buyHistoryDB.Get();
                Assert.AreEqual(li.Count, 2);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void RemoveBuyHistory()
        {
            try
            {
                Purchase toRemove = new Purchase(1, 1, 1, "itamar", 0.5, "02/02/2020", 5, 1);
                buyHistoryDB.Remove(toRemove);
                li = buyHistoryDB.Get();
                Assert.AreEqual(li.Count, 0);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void GetBuyHistory()
        {
            try
            {
                Purchase toAdd1 = new Purchase(2, 2, 2, "itamar", 500, "02/02/2020", 4, 3);
                Purchase toAdd2 = new Purchase(3, 3, 2, "itamar", 500, "02/02/2020", 4, 3);
                Purchase toAdd3 = new Purchase(4, 4, 2, "itamar", 500, "02/02/2020", 4, 3);
                Purchase toAdd4 = new Purchase(5, 5, 2, "itamar", 500, "02/02/2020", 4, 3);
                buyHistoryDB.Add(toAdd1);
                buyHistoryDB.Add(toAdd2);
                buyHistoryDB.Add(toAdd3);
                buyHistoryDB.Add(toAdd4);
                li = buyHistoryDB.Get();
                Assert.AreEqual(li.Count, 5);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }

    }
}
