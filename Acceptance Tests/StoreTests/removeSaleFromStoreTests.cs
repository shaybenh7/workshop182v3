﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;
using WebServices.DAL;

namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class removeSaleFromStoreTests
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

            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");

            store = StoreManagement.getInstance().getStore(ss.createStore("Abowim", zahi));

            cola = ProductManager.getInstance().getProductInStore(ss.addProductInStore("cola", 10, 100, zahi, store.getStoreId(), "Drinks"));

            ss.addSaleToStore(zahi, store.getStoreId(), cola.getProductInStoreId(), 1, 2, "20/5/2018");

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
        public void simpleRemoveSale()
        {
            Assert.AreEqual(ss.removeSaleFromStore(zahi, store.getStoreId(), colaSale.SaleId),0);
            Assert.AreEqual(ss.viewSalesByStore(store.getStoreId()).Count,0);
        }

        [TestMethod]
        public void RemoveSaleWithNullSession()
        {
            Assert.AreEqual(-4,ss.removeSaleFromStore(null, store.getStoreId(), colaSale.SaleId));//-1 not login || -4 could be 
            Assert.AreEqual(ss.viewSalesByStore(store.getStoreId()).Count, 1);
        }
        [TestMethod]
        public void RemoveSaleWithNullStore()
        {
            Assert.AreEqual(-4,ss.removeSaleFromStore(zahi, -7, colaSale.SaleId));//-6 if illegal store id
            Assert.AreEqual(ss.viewSalesByStore(store.getStoreId()).Count, 1);
        }

        [TestMethod]
        public void RemoveSaleWithNoneExistingSaleId()
        {
            Assert.AreEqual(ss.removeSaleFromStore(zahi, store.getStoreId(), 1000),-8);//-8 if illegal sale id
            Assert.AreEqual(ss.viewSalesByStore(store.getStoreId()).Count, 1);
        }
    }
}
