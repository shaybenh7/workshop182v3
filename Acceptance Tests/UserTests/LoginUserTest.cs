using System;
using wsep182.Domain;
using wsep182.services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Acceptance_Tests
{
    [TestClass]
    public class LoginUserTest
    {

        [TestInitialize]
        public void init()
        {
            ProductArchive.restartInstance();
            SalesArchive.restartInstance();
            storeArchive.restartInstance();
            UserArchive.restartInstance();
            UserCartsArchive.restartInstance();
            BuyHistoryArchive.restartInstance();
            CouponsArchive.restartInstance();
            DiscountsArchive.restartInstance();
            RaffleSalesArchive.restartInstance();
            StorePremissionsArchive.restartInstance();
        }

        [TestMethod]
        public void simpleLogin()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            us.register(session, "zahi", "123456");
            us.login(session, "zahi", "123456");
            Assert.AreEqual(session.getUserName(), "zahi");
            Assert.IsTrue(session.getState() is LogedIn);

        }

        [TestMethod]
        public void LoginToNoneExisitingUser()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(us.login(session,"zahi", "123456") >= 0);
        }

        [TestMethod]
        public void LoginWithBadUserName()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            us.register(session,"zahi", "123456");
            Assert.IsFalse(us.login(session, "gabi", "123456") >= 0);
            Assert.IsFalse(us.login(session, "Zahi", "123456") >= 0);
            Assert.IsFalse(us.login(session, "zahi ", "123456") >= 0);
            Assert.IsFalse(us.login(session, "zaHi", "123456") >= 0);
            Assert.IsFalse(us.login(session, "zahi1", "123456") >= 0);
            Assert.IsFalse(us.login(session, "zahI", "123456") >= 0);
            Assert.IsFalse(us.login(session, " zahi", "123456") >= 0);
        }

        [TestMethod]
        public void LoginWithBadPassword()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            us.register(session, "zahi", "abow");
            Assert.IsFalse(us.login(session, "zahi", "Abow") >= 0);
            Assert.IsFalse(us.login(session, "zahi", "aboW") >= 0);
            Assert.IsFalse(us.login(session, "zahi", "aBow") >= 0);
            Assert.IsFalse(us.login(session, "zahi", "abow1") >= 0);
            Assert.IsFalse(us.login(session, "zahi", "1abow") >= 0);
            Assert.IsFalse(us.login(session, "zahi", " abow") >= 0);
            Assert.IsFalse(us.login(session, "zahi", "abow ") >= 0);
        }

        [TestMethod]
        public void LoginWithEmptyPasswordOrUsername()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(us.login(session,"", "abow") >= 0);
            Assert.IsFalse(us.login(session, "zahi", "") >= 0);
        }

        [TestMethod]
        public void LoginWhenThereIsNoUsers()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(us.login(session, "zahi", "abow") >= 0);
        }

        [TestMethod]
        public void LoginWithUserWithSpaces()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(us.login(session, "zahi abow", "abow") >= 0);
            Assert.IsFalse(us.login(session, "zahi", "zahi abow") >= 0);
        }

        [TestMethod]
        public void LoginUserWithNullPassword()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(us.login(session, "zahi", null) >= 0);
        }

        [TestMethod]
        public void LoginUserWithNullUsername()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(us.login(session, null, "abow") >= 0);
        }
    }
}
