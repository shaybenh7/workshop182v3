using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using WebServices.DAL;
using WebServices.Domain;
using System.Collections.Generic;

namespace UnitTests.DBUnitTests
{
    [TestClass]
    public class PendingMessagesDBUnitTests
    {
        string testing = "Testing";
        PendingMessagesDB pendingMessagesDB;
        LinkedList<Tuple<String,String>> li;

        [TestInitialize]
        public void init()
        {
            WebServices.DAL.CleanDB cDB = new WebServices.DAL.CleanDB();
            cDB.emptyDB();
            configuration.DB_MODE = testing;
            pendingMessagesDB = new PendingMessagesDB(testing);
            li = new LinkedList<Tuple<String, String>>();
            pendingMessagesDB.Add(Tuple.Create("itamar", "hey"));
        }


        [TestMethod]
        public void AddMessage()
        {
            try
            {
                var toAdd = Tuple.Create("aviad", "9000");
                pendingMessagesDB.Add(toAdd);
                li = pendingMessagesDB.Get();
                Assert.AreEqual(li.Count, 2);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }

        }
        [TestMethod]
        public void RemoveMessage()
        {
            try
            {
                var toRemove = Tuple.Create("itamar", "hey");
                pendingMessagesDB.Remove(toRemove);
                li = pendingMessagesDB.Get();
                Assert.AreEqual(li.Count, 0);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void GetMessage()
        {
            try
            {
                var toAdd1 = Tuple.Create("shay", "1");
                var toAdd2 = Tuple.Create("niv", "2");
                var toAdd3 = Tuple.Create("zahi", "3");
                var toAdd4 = Tuple.Create("avicii", "4");
                pendingMessagesDB.Add(toAdd1);
                pendingMessagesDB.Add(toAdd2);
                pendingMessagesDB.Add(toAdd3);
                pendingMessagesDB.Add(toAdd4);
                li = pendingMessagesDB.Get();
                Assert.AreEqual(li.Count, 5);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
    }
}
