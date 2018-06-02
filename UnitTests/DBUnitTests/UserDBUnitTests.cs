using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using WebServices.DAL;
using WebServices.Domain;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class UserDBUnitTests
    {
        string testing = "Testing";
        UserDB userDB;
        LinkedList<User> li;

        [TestInitialize]
        public void init()
        {
            WebServices.DAL.CleanDB cDB = new WebServices.DAL.CleanDB();
            cDB.emptyDB();
            configuration.DB_MODE = testing;
            userDB = new UserDB(testing);
            userDB.Add(new User("aviad", "123"));
            li = new LinkedList<User>();
        }
        [TestMethod]
        public void addUser()
        {
            User toAdd = new User("itamar", "1a2b3c");
            try
            {
                userDB.Add(toAdd);
                li = userDB.Get();
                Assert.AreEqual(li.Count, 2);
                Assert.AreEqual(li.Last.Value.userName, "itamar");
                Assert.AreEqual(li.Last.Value.getPassword(), "1a2b3c");
            }
            catch(Exception e)
            {Assert.AreEqual(true, false,"there was a connection error to the testing db");}
        }
        [TestMethod]
        public void removeUser()
        {
            try
            {
                User aviad = new User("aviad", "123");
                userDB.Remove(aviad);
                li = userDB.Get();
                Assert.AreEqual(li.Count, 0);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }
        public void get()
        {
            try
            {
                User toAdd1 = new User("user1", "1");
                User toAdd2 = new User("user2", "2");
                User toAdd3 = new User("user3", "3");
                User toAdd4 = new User("user4", "4");
                userDB.Add(toAdd1);
                userDB.Add(toAdd2);
                userDB.Add(toAdd3);
                userDB.Add(toAdd4);
                li = userDB.Get();
                Assert.AreEqual(li.Count, 5);
            }
            catch (Exception e)
            { Assert.AreEqual(true, false, "there was a connection error to the testing db"); }
        }

    }
}
