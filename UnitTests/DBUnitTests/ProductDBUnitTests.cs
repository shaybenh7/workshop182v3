using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using WebServices.DAL;
using WebServices.Domain;
using System.Collections.Generic;

namespace UnitTests.DBUnitTests
{
    [TestClass]
    public class ProductDBUnitTests
    {
        string testing = "Testing";
        ProductDB productDB;
        LinkedList<Product> li;

        [TestInitialize]
        public void init()
        {
            WebServices.DAL.CleanDB cDB = new WebServices.DAL.CleanDB();
            cDB.emptyDB();
            configuration.DB_MODE = testing;
            productDB = new ProductDB(testing);
            li = new LinkedList<Product>();
            productDB.Add(new Product("milk"));
        }
        [TestMethod]
        public void AddProduct()
        {
            try
            {
                Product toAdd = new Product("meat");
                productDB.Add(toAdd);
                li = productDB.Get();
                Assert.AreEqual(li.Count, 2);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }

        }
        [TestMethod]
        public void RemoveProduct()
        {
            try
            {
                Product toRemove = new Product("milk");
                productDB.Remove(toRemove);
                li = productDB.Get();
                Assert.AreEqual(li.Count, 0);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void GetProduct()
        {
            li = productDB.Get();
            Assert.AreEqual(li.Count, 1);
        }
    }
}
