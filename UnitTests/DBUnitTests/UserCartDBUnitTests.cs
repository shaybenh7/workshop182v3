using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using WebServices.DAL;
using WebServices.Domain;
using System.Collections.Generic;

namespace UnitTests.DBUnitTests
{
    [TestClass]
    public class UserCartDBUnitTests
    {
        string testing = "Testing";
        UserCartDB userCartDB;
        LinkedList<UserCart> li;

        [TestInitialize]
        public void init()
        {
            WebServices.DAL.CleanDB cDB = new WebServices.DAL.CleanDB();
            cDB.emptyDB();
            configuration.DB_MODE = testing;
            userCartDB = new UserCartDB(testing);
            li = new LinkedList<UserCart>();
            UserCart temp = new UserCart("itamar", 1, 10);
            temp.Offer = 200;
            userCartDB.Add(temp);
        }

        [TestMethod]
        public void AddUserCart()
        {
            try
            {
                UserCart toAdd = new UserCart("aviad", 2, 50);
                toAdd.Offer = 10;
                userCartDB.Add(toAdd);
                li = userCartDB.Get();
                Assert.AreEqual(li.Count, 2);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void RemoveUserCart()
        {
            try
            {
                UserCart temp = new UserCart("itamar", 1, 10);
                temp.Offer = 200;
                userCartDB.Remove(temp);
                li = userCartDB.Get();
                Assert.AreEqual(li.Count, 0);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        [TestMethod]
        public void GetUserCart()
        {
            try
            {
                UserCart temp1 = new UserCart("aviad", 1, 10);
                UserCart temp2 = new UserCart("shay", 1, 10);
                UserCart temp3 = new UserCart("niv", 1, 10);
                userCartDB.Add(temp1);
                userCartDB.Add(temp2);
                userCartDB.Add(temp3);
                li = userCartDB.Get();
                Assert.AreEqual(li.Count, 4);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
    }
}
