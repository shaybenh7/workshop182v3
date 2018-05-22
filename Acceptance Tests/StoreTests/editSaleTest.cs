using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using wsep182.Domain;
using wsep182.services;
namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class editSaleTest
    {

        private userServices us;
        private storeServices ss;
        private User zahi, itamar, niv, admin, admin1; //admin,itamar logedin
        private Store store;//itamar owner , niv manneger
        ProductInStore cola, sprite;
        int saleId;
        int raffleSale;

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

            us = userServices.getInstance();
            ss = storeServices.getInstance();
            admin = us.startSession();
            us.register(admin, "admin", "123456");
            us.login(admin, "admin", "123456");

            admin1 = us.startSession();
            us.register(admin1, "admin1", "123456");

            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");

            itamar = us.startSession();
            us.register(itamar, "itamar", "123456");
            us.login(itamar, "itamar", "123456");



            int storeid = ss.createStore("MariaNettaInc", itamar);
            store = storeArchive.getInstance().getStore(storeid);

            niv = us.startSession();
            us.register(niv, "niv", "123456");
            us.login(niv, "niv", "123456");

            ss.addStoreManager(store.getStoreId(), "niv", itamar);

            int c = ss.addProductInStore("cola", 3.2, 10, itamar, storeid, "Drinks");
            int s = ss.addProductInStore("sprite", 5.3, 20, itamar, storeid, "Drinks");
            cola = ProductArchive.getInstance().getProductInStore(c);
            sprite = ProductArchive.getInstance().getProductInStore(s);
            saleId = ss.addSaleToStore(itamar, store.getStoreId(), cola.getProductInStoreId(), 1, 1, "20.5.2018");
            raffleSale=ss.addSaleToStore(itamar, store.getStoreId(), cola.getProductInStoreId(), 3, 1, "20.5.2018");

        }

        [TestMethod]
        public void SimpleEditSale()
        {
            ss.editSale(itamar, store.getStoreId(), saleId, 10, "15.2.2019");
            Assert.AreEqual(10, SalesArchive.getInstance().getSale(saleId).Amount);
            Assert.AreEqual("15.2.2019", SalesArchive.getInstance().getSale(saleId).DueDate);
        }
        [TestMethod]
        public void SimpleEditRaffleSale()
        {
            ss.editSale(itamar, store.getStoreId(), raffleSale, 10, "15.2.2019");
            Assert.AreEqual(10, SalesArchive.getInstance().getSale(raffleSale).Amount);
            Assert.AreEqual("15.2.2019", SalesArchive.getInstance().getSale(raffleSale).DueDate);
        }
        [TestMethod]
        public void EditSaleNegativeAmount()
        {
            Assert.AreEqual(ss.editSale(itamar, store.getStoreId(), saleId, -1, "20/5/2018"),-12);//-12 if illegal amount
        }
       
        [TestMethod]
        public void EditSaleZeroAmount()
        {
            Assert.AreEqual(ss.editSale(itamar, store.getStoreId(), saleId, 0, "20/5/2018"),0);//OK
        }

        [TestMethod]
        public void EditSaleBiggerThenOneAmount()
        {
            Assert.AreEqual(ss.editSale(itamar, store.getStoreId(), saleId, 25, "15.2.2019"),-5);//-5 if illegal amount bigger then amount in stock
        }
        [TestMethod]
        public void EditSaleWithOwnerOfAnotherStore()
        {
            Assert.AreEqual(ss.editSale(admin, store.getStoreId(), saleId, 1, "20/5/2018"),-4);// -4 if don't have premition
        }
        [TestMethod]
        public void EditSaleWithNullParameters()
        {
            Assert.AreEqual(-4,ss.editSale(null, store.getStoreId(), saleId, 1, "20/5/2018"));//-1 not login || -4 don't have premition
            Assert.AreEqual(-4,ss.editSale(itamar, -7, saleId, 1, "20/5/2018"));//-6 if illegal store id
            Assert.AreEqual(-10,ss.editSale(itamar, store.getStoreId(), saleId, 1, null));//-10 due date not good
        }
        [TestMethod]
        public void EditSaleWithDoesExistsSaleId()
        {
            Assert.AreEqual(ss.editSale(itamar, store.getStoreId(), 9, 1, DateTime.Now.AddDays(20).ToString()),-8);// -8 if illegal sale id
        }
        [TestMethod]
        public void AddSaleWithDateNotGood()
        {
            int temp = ss.editSale(itamar, store.getStoreId(), saleId, 1, "HEY");
            Assert.IsFalse(temp>0);//-10 due date not good
        }
    }
    }