using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;
using WebServices.DAL;

namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class PolicyTest
    {

        private userServices us;
        private storeServices ss;
        private sellServices ses;
        private User zahi,itamar;//logedIn
        private Store store;//zahi owner 
        private ProductInStore cola;
        private Sale colaSale;

        [TestInitialize]
        public void init()
        {
            CleanDB cDB = new CleanDB();
            cDB.emptyDB();
            ProductManager.restartInstance();
            SalesManager.restartInstance();
            StoreManagement.restartInstance();
            UserManager.restartInstance();
            UserCartsManager.restartInstance();
            BuyHistoryManager.restartInstance();
            CouponsManager.restartInstance();
            DiscountsManager.restartInstance();
            RaffleSalesManager.restartInstance();
            StorePremissionsArchive.restartInstance();

            us = userServices.getInstance();
            ss = storeServices.getInstance();
            ses = sellServices.getInstance();

            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");
            itamar = us.startSession();
            us.register(itamar, "itamar", "123456");
            us.login(itamar, "itamar", "123456");

            int storeid = ss.createStore("abowim", zahi);
            store = StoreManagement.getInstance().getStore(storeid);

            int colaId = ss.addProductInStore("cola", 10, 100, zahi, storeid, "Drinks");
            cola = ProductManager.getInstance().getProductInStore(colaId);

            ss.addSaleToStore(zahi, store.getStoreId(), cola.getProductInStoreId(), 1, 20, "20/8/2018");

            LinkedList<Sale> SL = ss.viewSalesByStore(store.getStoreId());
            foreach (Sale sale in SL)
            {
                if (sale.ProductInStoreId == cola.getProductInStoreId())
                {
                    colaSale = sale;
                }
            }
        }
        [TestMethod]
        public void simpleSetAmountPolicyOnStore()
        {
            Assert.IsTrue(ss.setAmountPolicyOnStore(zahi, store.getStoreId(), 2, 10) > -1);

            LinkedList<Sale> SL = ss.viewSalesByStore(store.getStoreId());
            Sale sale = SL.First.Value;
            Assert.IsTrue(ses.addProductToCart(itamar, sale.SaleId, 2) > 0);
            LinkedList<UserCart> sc = ses.getShoppingCartBeforeCheckout(itamar);
            Tuple<int, LinkedList<UserCart>> checkOut = ses.checkout(itamar, "israel", "mezada 69", "1234");
            Assert.IsTrue(checkOut.Item1 == -1);
        }
        [TestMethod]
        public void SetAmountPolicyOnStoreLowAmount()
        {
            Assert.IsTrue(ss.setAmountPolicyOnStore(zahi, store.getStoreId(), 2, 10) > -1);

            LinkedList<Sale> SL = ss.viewSalesByStore(store.getStoreId());
            Sale sale = SL.First.Value;
            Assert.IsTrue(ses.addProductToCart(itamar, sale.SaleId, 1) > 0);
            LinkedList<UserCart> sc = ses.getShoppingCartBeforeCheckout(itamar);
            Tuple<int,LinkedList<UserCart>> checkOut= ses.checkout(itamar,"israel","mezada 69","1234");
            Assert.IsFalse(checkOut.Item1 == -1);
        }
        [TestMethod]
        public void SetAmountPolicyOnStoreBigAmount()
        {
            Assert.IsTrue(ss.setAmountPolicyOnStore(zahi, store.getStoreId(), 2, 10)>-1);

            LinkedList<Sale> SL = ss.viewSalesByStore(store.getStoreId());
            Sale sale = SL.First.Value;
            Assert.IsTrue(ses.addProductToCart(itamar, sale.SaleId, 11) > 0);
            LinkedList<UserCart> sc = ses.getShoppingCartBeforeCheckout(itamar);
            Tuple<int, LinkedList<UserCart>> checkOut = ses.checkout(itamar, "israel", "mezada 69", "1234");
            Assert.IsFalse(checkOut.Item1 == -1);
        }
        [TestMethod]
        public void simplesetAmountPolicyOnCategory()
        {
            Assert.IsTrue(ss.setAmountPolicyOnCategory(zahi, store.getStoreId(), "Drinks", 2, 10) > -1);

            LinkedList<Sale> SL = ss.viewSalesByStore(store.getStoreId());
            Sale sale = SL.First.Value;
            Assert.IsTrue(ses.addProductToCart(itamar, sale.SaleId, 2) > 0);
            LinkedList<UserCart> sc = ses.getShoppingCartBeforeCheckout(itamar);
            Tuple<int, LinkedList<UserCart>> checkOut = ses.checkout(itamar, "israel", "mezada 69", "1234");
            Assert.IsTrue(checkOut.Item1 == -1);
        }
        [TestMethod]
        public void setAmountPolicyOnCategoryLowAmount()
        {
            Assert.IsTrue(ss.setAmountPolicyOnCategory(zahi, store.getStoreId(), "Drinks", 2, 10) > -1);

            LinkedList<Sale> SL = ss.viewSalesByStore(store.getStoreId());
            Sale sale = SL.First.Value;
            Assert.IsTrue(ses.addProductToCart(itamar, sale.SaleId, 1) > 0);
            LinkedList<UserCart> sc = ses.getShoppingCartBeforeCheckout(itamar);
            Tuple<int, LinkedList<UserCart>> checkOut = ses.checkout(itamar, "israel", "mezada 69", "1234");
            Assert.IsFalse(checkOut.Item1 == -1);
        }
        [TestMethod]
        public void setAmountPolicyOnCategoryBigAmount()
        {
            Assert.IsTrue(ss.setAmountPolicyOnCategory(zahi, store.getStoreId(), "Drinks", 2, 10)>-1);

            LinkedList<Sale> SL = ss.viewSalesByStore(store.getStoreId());
            Sale sale = SL.First.Value;
            Assert.IsTrue(ses.addProductToCart(itamar, sale.SaleId, 11) > 0);
            LinkedList<UserCart> sc = ses.getShoppingCartBeforeCheckout(itamar);
            Tuple<int, LinkedList<UserCart>> checkOut = ses.checkout(itamar, "israel", "mezada 69", "1234");
            Assert.IsFalse(checkOut.Item1 == -1);
        }
        [TestMethod]
        public void simplesetAmountPolicyOnProductInStore()
        {
            Assert.IsTrue(ss.setAmountPolicyOnProductInStore(zahi, store.getStoreId(),colaSale.ProductInStoreId, 2, 10) > -1);

            LinkedList<Sale> SL = ss.viewSalesByStore(store.getStoreId());
            Sale sale = SL.First.Value;
            Assert.IsTrue(ses.addProductToCart(itamar, sale.SaleId, 2) > 0);
            LinkedList<UserCart> sc = ses.getShoppingCartBeforeCheckout(itamar);
            Tuple<int, LinkedList<UserCart>> checkOut = ses.checkout(itamar, "israel", "mezada 69", "1234");
            Assert.IsTrue(checkOut.Item1 == -1);
        }
        [TestMethod]
        public void setAmountPolicyOnProductInStoreLowAmount()
        {
            Assert.IsTrue(ss.setAmountPolicyOnProductInStore(zahi, store.getStoreId(), colaSale.ProductInStoreId, 2, 10) > -1);

            LinkedList<Sale> SL = ss.viewSalesByStore(store.getStoreId());
            Sale sale = SL.First.Value;
            Assert.IsTrue(ses.addProductToCart(itamar, sale.SaleId, 1) > 0);
            LinkedList<UserCart> sc = ses.getShoppingCartBeforeCheckout(itamar);
            Tuple<int, LinkedList<UserCart>> checkOut = ses.checkout(itamar, "israel", "mezada 69", "1234");
            Assert.IsFalse(checkOut.Item1 == -1);
        }
        [TestMethod]
        public void setAmountPolicyOnProductInStoreBigAmount()
        {
            Assert.IsTrue(ss.setAmountPolicyOnProductInStore(zahi, store.getStoreId(), colaSale.ProductInStoreId, 2, 10) > -1);

            LinkedList<Sale> SL = ss.viewSalesByStore(store.getStoreId());
            Sale sale = SL.First.Value;
            Assert.IsTrue(ses.addProductToCart(itamar, sale.SaleId, 11) > 0);
            LinkedList<UserCart> sc = ses.getShoppingCartBeforeCheckout(itamar);
            Tuple<int, LinkedList<UserCart>> checkOut = ses.checkout(itamar, "israel", "mezada 69", "1234");
            Assert.IsFalse(checkOut.Item1 == -1);
        }
        [TestMethod]
        public void simplesetAmountPolicyOnCountry()
        {
            Assert.IsTrue(ss.setAmountPolicyOnCountry(zahi, store.getStoreId(), "Israel", 2, 10) > -1);

            LinkedList<Sale> SL = ss.viewSalesByStore(store.getStoreId());
            Sale sale = SL.First.Value;
            Assert.IsTrue(ses.addProductToCart(itamar, sale.SaleId, 2) > 0);
            LinkedList<UserCart> sc = ses.getShoppingCartBeforeCheckout(itamar);
            Tuple<int, LinkedList<UserCart>> checkOut = ses.checkout(itamar, "Israel", "mezada 69", "1234");
            Assert.IsTrue(checkOut.Item1 == -1);
        }
        [TestMethod]
        public void setAmountPolicyOnCountryLowAmount()
        {
            Assert.IsTrue(ss.setAmountPolicyOnCountry(zahi, store.getStoreId(), "Israel", 2, 10) > -1);

            LinkedList<Sale> SL = ss.viewSalesByStore(store.getStoreId());
            Sale sale = SL.First.Value;
            Assert.IsTrue(ses.addProductToCart(itamar, sale.SaleId, 1) > 0);
            LinkedList<UserCart> sc = ses.getShoppingCartBeforeCheckout(itamar);
            Tuple<int, LinkedList<UserCart>> checkOut = ses.checkout(itamar, "Israel", "mezada 69", "1234");
            Assert.IsFalse(checkOut.Item1 == -1);
        }
        [TestMethod]
        public void setAmountPolicyOnCountryBigAmount()
        {
            Assert.IsTrue(ss.setAmountPolicyOnCountry(zahi, store.getStoreId(), "Israel", 2, 10) > -1);

            LinkedList<Sale> SL = ss.viewSalesByStore(store.getStoreId());
            Sale sale = SL.First.Value;
            Assert.IsTrue(ses.addProductToCart(itamar, sale.SaleId, 11) > 0);
            LinkedList<UserCart> sc = ses.getShoppingCartBeforeCheckout(itamar);
            Tuple<int, LinkedList<UserCart>> checkOut = ses.checkout(itamar, "Israel", "mezada 69", "1234");
            Assert.IsFalse(checkOut.Item1 == -1);
        }
        [TestMethod]
        public void setAmountPolicyOnCountryNotInTheContry()
        {
            Assert.IsTrue(ss.setAmountPolicyOnCountry(zahi, store.getStoreId(), "Israel", 2, 10) > -1);

            LinkedList<Sale> SL = ss.viewSalesByStore(store.getStoreId());
            Sale sale = SL.First.Value;
            Assert.IsTrue(ses.addProductToCart(itamar, sale.SaleId, 11) > 0);
            LinkedList<UserCart> sc = ses.getShoppingCartBeforeCheckout(itamar);
            Tuple<int, LinkedList<UserCart>> checkOut = ses.checkout(itamar, "USA", "mezada 69", "1234");
            Assert.IsTrue(checkOut.Item1 == -1);
        }




    }
}
