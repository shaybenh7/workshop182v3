using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using WebServices.DAL;
using WebServices.Domain;
using System.Collections.Generic;

namespace UnitTests.DBUnitTests
{
    [TestClass]
    public class UsersNotificationPreferencesDBUnitTests
    {

        string testing = "Testing";
        UsersNotificationPreferencesDB usDB;
        LinkedList<Tuple<int, String, int>> li;

        [TestInitialize]
        public void init()
        {
            WebServices.DAL.CleanDB cDB = new WebServices.DAL.CleanDB();
            cDB.emptyDB();
            configuration.DB_MODE = testing;
            usDB = new UsersNotificationPreferencesDB(testing);
            li = new LinkedList<Tuple<int, String, int>>();//category(enum),username,storeId
            usDB.Add(Tuple.Create(1, "itamar", 1));
        }

        [TestMethod]
        public void Add()
        {
            try
            {
                var toAdd = Tuple.Create(2, "aviad", 2);
                usDB.Add(toAdd);
                li = usDB.Get();
                Assert.AreEqual(li.Count, 2);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void Remove()
        {
            try
            {
                usDB.Remove(Tuple.Create(1, "itamar", 1));
                li = usDB.Get();
                Assert.AreEqual(li.Count, 0);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void Get()
        {
            try
            {
                var toAdd1 = Tuple.Create(2, "itamar2", 2);
                var toAdd2 = Tuple.Create(3, "itamar3", 3);
                var toAdd3 = Tuple.Create(4, "itamar4", 4);
                usDB.Add(toAdd1);
                usDB.Add(toAdd2);
                usDB.Add(toAdd3);
                li = usDB.Get();
                Assert.AreEqual(li.Count, 4);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
    }
}
