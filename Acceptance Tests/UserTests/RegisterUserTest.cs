using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests
{
    [TestClass]
    public class RegisterUserTest
    {
        [TestInitialize]
        public void init()
        {
            ProductManager.restartInstance();
            SalesManager.restartInstance();
            storeArchive.restartInstance();
            UserManager.restartInstance();
            UserCartsManager.restartInstance();
            BuyHistoryManager.restartInstance();
            CouponsManager.restartInstance();
            DiscountsManager.restartInstance();
            RaffleSalesManager.restartInstance();
            StorePremissionsArchive.restartInstance();
        }

        [TestMethod]
        public void SimpleRegister()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            us.register(session,"zahi", "123456");
            us.login(session, "zahi", "123456");
            Assert.AreEqual(session.getUserName(),"zahi");
            Assert.IsTrue(session.getState() is LogedIn);
        }

        [TestMethod]
        public void registerUserAlreadyExist()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            session.register("zahi", "123456");
            Assert.IsTrue(session.getState() is Guest);
            Assert.IsFalse(us.register(session, "zahi", "123456") >= 0);
            Assert.IsFalse(us.register(session, "zahi", "12345678") >= 0);

        }

        [TestMethod]
        public void registerUserWithEmptyUsername()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(session.register("", "12345678") >= 0);
        }

        [TestMethod]
        public void registerUserWithEmptyPassword()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(session.register("zahi", "") >= 0);
        }

        [TestMethod]
        public void registerUserWithspacesUsername()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(session.register("zahi abow", "123456") >= 0);
        }


        [TestMethod]
        public void registerUserWithNullPassword()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(session.register("zahi", null) >= 0);
        }

        [TestMethod]
        public void registerUserWithNullUsername()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(session.register(null, "abow") >= 0);
        }
    }
}
