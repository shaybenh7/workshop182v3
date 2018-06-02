using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using WebServices.DAL;
using WebServices.Domain;
using System.Collections.Generic;

namespace UnitTests.DBUnitTests
{
    [TestClass]
    public class ProductInStoreDBUnitTests
    {
        string testing = "Testing";
        ProductInStoreDB pisDB;
        LinkedList<ProductInStore> li;
        User itamar;
        Store apple;
        Product milk, meat;

        [TestInitialize]
        public void init()
        {
            WebServices.DAL.CleanDB cDB = new WebServices.DAL.CleanDB();
            cDB.emptyDB();
            configuration.DB_MODE = testing;
            pisDB = new ProductInStoreDB(testing);
            li = new LinkedList<ProductInStore>();
            itamar = new User("itamar", "123");
            apple = new Store(1, "apple", itamar);
            milk = new Product("milk");
            meat = new Product("meat");
            ProductInStore meatInStore = new ProductInStore(1, meat, 30, 30, apple);
            pisDB.Add(meatInStore);
        }
        [TestMethod]
        public void AddProductInStore()
        {
            try
            {
                ProductInStore milkInStore = new ProductInStore(2, milk, 200, 200, apple);
                pisDB.Add(milkInStore);
                li = pisDB.Get();
                Assert.AreEqual(li.Count, 2);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void RemoveProductInStore()
        {
            try
            {
                ProductInStore toRemove = new ProductInStore(1, meat, 30, 30, apple);
                pisDB.Remove(toRemove);
                li = pisDB.Get();
                Assert.AreEqual(li.Count, 0);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void GetProductInStore()
        {
            try
            {
                li = pisDB.Get();
                Assert.AreEqual(li.Count, 1);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
    }
}
