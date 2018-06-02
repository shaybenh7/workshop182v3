using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using WebServices.DAL;
using WebServices.Domain;
using System.Collections.Generic;

namespace UnitTests.DBUnitTests
{
    [TestClass]
    public class StoreRoleDictionaryDBUnitTests
    {
        string testing = "Testing";
        StoreRoleDictionaryDB strldDB;
        LinkedList<Tuple<int, String, String, String, String>> li;

        [TestInitialize]
        public void init()
        {
            WebServices.DAL.CleanDB cDB = new WebServices.DAL.CleanDB();
            cDB.emptyDB();
            configuration.DB_MODE = testing;
            strldDB = new StoreRoleDictionaryDB(testing);
            li = new LinkedList<Tuple<int, String, String, String, String>>();//storeId,username,storeRole,addedy,timeadded
            strldDB.Add(Tuple.Create(1, "itamar", "Owner", "yossi", "03/03/2030"));
        }

        [TestMethod]
        public void AddStoreRoleDictionary()
        {
            try
            {
                var toAdd = Tuple.Create(1, "itamar", "Owner", "yossi", "03/03/2030");
                strldDB.Add(toAdd);
                li = strldDB.Get();
                Assert.AreEqual(li.Count, 2);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void RemoveStoreRoleDictionary()
        {
            try
            {
                var toRemove = Tuple.Create(1, "itamar", "Owner", "yossi", "03/03/2030");
                strldDB.Remove(toRemove);
                li = strldDB.Get();
                Assert.AreEqual(li.Count, 0);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void GetStoreRoleDictionary()
        {
            try
            {
                var toAdd1 = Tuple.Create(2, "itamar", "Owner", "yossi", "03/03/2030");
                var toAdd2 = Tuple.Create(3, "itamar", "Owner", "yossi", "03/03/2030");
                var toAdd3 = Tuple.Create(4, "itamar", "Owner", "yossi", "03/03/2030");
                strldDB.Add(toAdd1);
                strldDB.Add(toAdd2);
                strldDB.Add(toAdd3);
                li = strldDB.Get();
                Assert.AreEqual(li.Count, 4);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
    }
}
