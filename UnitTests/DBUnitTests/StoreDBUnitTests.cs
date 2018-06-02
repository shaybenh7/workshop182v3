using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using WebServices.DAL;
using WebServices.Domain;
using System.Collections.Generic;

namespace UnitTests.DBUnitTests
{
    [TestClass]
    public class StoreDBUnitTests
    {

        string testing = "Testing";
        StoreDB storeDB;
        LinkedList<Store> li;
        User itamar;
        [TestInitialize]
        public void init()
        {
            WebServices.DAL.CleanDB cDB = new WebServices.DAL.CleanDB();
            cDB.emptyDB();
            configuration.DB_MODE = testing;
            storeDB = new StoreDB(testing);
            li = new LinkedList<Store>();
            itamar = new User("itamar", "123");
            storeDB.Add(new Store(1, "halavi", itamar));
        }

        [TestMethod]
        public void AddStore()
        {
            try
            {
                Store toAdd = new Store(2, "apply", itamar);
                storeDB.Add(toAdd);
                li = storeDB.Get();
                Assert.AreEqual(li.Count, 2);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void RemoveStore()
        {
            try
            {
                Store toRemove = new Store(1, "halavi", itamar);
                storeDB.Remove(toRemove);
                li = storeDB.Get();
                Assert.AreEqual(li.Count, 0);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void GetStore()
        {
            try
            {
                Store toAdd1 = new Store(2, "hey1", itamar);
                Store toAdd2 = new Store(3, "hey2", itamar);
                Store toAdd3 = new Store(4, "hey3", itamar);
                storeDB.Add(toAdd1);
                storeDB.Add(toAdd2);
                storeDB.Add(toAdd3);
                li = storeDB.Get();
                Assert.AreEqual(li.Count, 4);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
    }
}
