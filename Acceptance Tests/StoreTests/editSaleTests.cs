using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;
namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class editSaleTests
    {

        private userServices us;
        private storeServices ss;
        private User zahi; 
        private Store store;//itamar owner , niv manneger
        ProductInStore cola;
        Sale colaSale;

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
           
            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");

            int storeid = ss.createStore("abowim", zahi);
            store = storeArchive.getInstance().getStore(storeid);

            int c = ss.addProductInStore("cola", 3.2, 10, zahi, storeid, "Drinks");
            cola = ProductArchive.getInstance().getProductInStore(c);
            ss.addSaleToStore(zahi, store.getStoreId(), cola.getProductInStoreId(), 1, 2, "20/5/2018");

            LinkedList<Sale> SL = ss.viewSalesByStore(store.getStoreId());
            foreach(Sale sale in SL)
            {
                if(sale.ProductInStoreId == cola.getProductInStoreId())
                {
                    colaSale = sale;
                }
            }
        }

        [TestMethod]
        public void simpleEditSale()
        {
            Assert.AreEqual(ss.editSale(zahi, store.getStoreId(), colaSale.SaleId, 5, "20/6/2018"),0);
            Assert.AreEqual(colaSale.Amount, 5);
            Assert.AreEqual(colaSale.DueDate, "20/6/2018");
        }

        [TestMethod]
        public void EditSaleToNegAmount()
        {
            Assert.AreEqual(ss.editSale(zahi, store.getStoreId(), colaSale.SaleId,-1, "20/6/2018"),-12);//-12 if illegal amount
            Assert.AreEqual(colaSale.Amount, 2);
            Assert.AreEqual(colaSale.DueDate, "20/5/2018");
        }

        [TestMethod]
        public void EditSaleToAmountBiggerThanTheAmountOfProduct()
        {
            Assert.AreEqual(ss.editSale(zahi, store.getStoreId(), colaSale.SaleId, 11, "20/6/2018"),-5);//-5 if illegal amount bigger then amount in stock
            Assert.AreEqual(colaSale.Amount, 2);
            Assert.AreEqual(colaSale.DueDate, "20/5/2018");
        }
  

        [TestMethod]
        public void EditSaleToDueDateInThePastInYears()
        {
            Assert.AreEqual(-10,ss.editSale(zahi, store.getStoreId(), colaSale.SaleId, 1, "20/6/2017"));//-10 due date not good
            Assert.AreEqual(colaSale.Amount, 2);
            Assert.AreEqual(colaSale.DueDate, "20/5/2018");
        }

        [TestMethod]
        public void EditSaleToDueDateInThePastInMonth()
        {
            Assert.AreEqual(-10,ss.editSale(zahi, store.getStoreId(), colaSale.SaleId, 1, "20/3/2018"));//-10 due date not good
            Assert.AreEqual(colaSale.Amount, 2);
            Assert.AreEqual(colaSale.DueDate, "20/5/2018");
        }

        [TestMethod]
        public void EditSaleToDueDateNull()
        {
            Assert.AreEqual(-10,ss.editSale(zahi, store.getStoreId(), colaSale.SaleId, 1, null));////-10 due date not good
            Assert.AreEqual(colaSale.Amount, 2);
            Assert.AreEqual(colaSale.DueDate, "20/5/2018");
        }
    }
}
