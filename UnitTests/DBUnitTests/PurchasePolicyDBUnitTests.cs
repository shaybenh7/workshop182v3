using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using WebServices.DAL;
using WebServices.Domain;
using System.Collections.Generic;

namespace UnitTests.DBUnitTests
{
    [TestClass]
    public class PurchasePolicyDBUnitTests
    {
        string testing = "Testing";
        PurchasePolicyDB PPDB;
        LinkedList<PurchasePolicy> li;

        [TestInitialize]
        public void init()
        {
            WebServices.DAL.CleanDB cDB = new WebServices.DAL.CleanDB();
            cDB.emptyDB();
            configuration.DB_MODE = testing;
            PPDB = new PurchasePolicyDB(testing);
            li = new LinkedList<PurchasePolicy>();
            PurchasePolicy temp = new PurchasePolicy();
            temp.TypeOfPolicy = 1;
            temp.ProductName = "milk";
            temp.StoreId = 1;
            temp.Category = "";
            temp.ProductInStoreId = 1;
            temp.Country = "";
            PPDB.Add(temp);
    }

        [TestMethod]
        public void AddPurchasePolicy()
        {
            try
            {
                PurchasePolicy toAdd = new PurchasePolicy();
                toAdd.TypeOfPolicy = 1;
                toAdd.ProductName = "meat";
                toAdd.StoreId = 1;
                toAdd.Category = "";
                toAdd.ProductInStoreId = 2;
                toAdd.Country = "ISRAEL";
                PPDB.Add(toAdd);
                li = PPDB.Get();
                Assert.AreEqual(li.Count, 2);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void RemovePurchasePolicy()
        {
            try
            {
                PurchasePolicy temp = new PurchasePolicy();
                temp.TypeOfPolicy = 1;
                temp.ProductName = "milk";
                temp.StoreId = 1;
                temp.Category = "";
                temp.ProductInStoreId = 1;
                temp.Country = "";
                PPDB.Remove(temp);
                li = PPDB.Get();
                Assert.AreEqual(li.Count, 0);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void GetPurchasePolicy()
        {
            try
            {
                li = PPDB.Get();
                Assert.AreEqual(li.Count, 1);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
    }
}
