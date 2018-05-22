﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class viewSaleInStoreTest
    {

        private userServices us;
        private storeServices ss;
        private User zahi, itamar, niv, admin, admin1; //admin,itamar logedin
        private Store store;//itamar owner , niv manneger
        ProductInStore cola, sprite;

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

            itamar = us.startSession();
            us.register(itamar, "itamar", "123456");
            us.login(itamar, "itamar", "123456");
            store = storeArchive.getInstance().getStore(ss.createStore("Maria&Netta Inc.", itamar));

            niv = us.startSession();
            us.register(niv, "niv", "123456");
            us.login(niv, "niv", "123456");

            ss.addStoreManager(store.getStoreId(), "niv", itamar);

            cola = ProductArchive.getInstance().getProductInStore(ss.addProductInStore("cola", 3.2, 10, itamar, store.getStoreId(), "Drinks"));
            sprite = ProductArchive.getInstance().getProductInStore(ss.addProductInStore("sprite", 5.3, 20, itamar, store.getStoreId(), "Drinks"));

        }

        [TestMethod]
        public void simpleViewSlaeInStore()
        {
            int saleId=ss.addSaleToStore(itamar, store.getStoreId(), cola.getProductInStoreId(), 1, 1, "20/5/2018");
            LinkedList <Sale>saleList=ss.viewSalesByStore(store.getStoreId());
            Assert.AreEqual(saleList.Count, 1);
            Assert.AreEqual(saleId, saleList.First.Value.SaleId);
        }
        [TestMethod]
        public void ViewSlaeInStoreEmptySale()
        {
            LinkedList<Sale> saleList = ss.viewSalesByStore(store.getStoreId());
            Assert.AreEqual(saleList.Count, 0);

        }
        [TestMethod]
        public void ViewSlaeInStoreNull()
        {
            LinkedList<Sale> saleList = ss.viewSalesByStore(-7);
            Assert.IsNull(saleList);
        }
        [TestMethod]
        public void ViewSlaeInStoreFewStors()
        {
            int saleId1 = ss.addSaleToStore(itamar, store.getStoreId(), cola.getProductInStoreId(), 1, 1, "20/5/2018");
            LinkedList<Sale> saleList1 = ss.viewSalesByStore(store.getStoreId());
            int storeId = ss.createStore("admin store", admin);
            Store store2 = storeArchive.getInstance().getStore(storeId);
            int milkId = ss.addProductInStore("milk", 3.2, 10, admin, store2.getStoreId(), "Drinks");
            ProductInStore milk = ProductArchive.getInstance().getProductInStore(milkId);
            int saleId2 = ss.addSaleToStore(admin, store2.getStoreId(), milk.getProductInStoreId(), 1, 1, "20/5/2018");
            LinkedList<Sale> saleList2 = ss.viewSalesByStore(store2.getStoreId());

            Assert.AreEqual(saleList1.Count, 1);
            Assert.AreEqual(saleId1, saleList1.First.Value.SaleId);
            Assert.AreEqual(saleList2.Count, 1);
            Assert.AreEqual(saleId2, saleList2.First.Value.SaleId);
        }
        [TestMethod]
        public void ViewSlaeInStoreFewProducts()
        {
            int saleId = ss.addSaleToStore(itamar, store.getStoreId(), cola.getProductInStoreId(), 1, 1, "20/5/2018");
            
            int saleId2 = ss.addSaleToStore(itamar, store.getStoreId(), sprite.getProductInStoreId(), 1, 1, "20/5/2018");
            LinkedList<Sale> saleList = ss.viewSalesByStore(store.getStoreId());

            Assert.AreEqual(saleList.Count, 2);
            foreach(Sale s in saleList)
            {
                if(s.ProductInStoreId== cola.getProductInStoreId())
                {
                    Assert.AreEqual(s.SaleId, saleId);
                }
                if (s.ProductInStoreId == sprite.getProductInStoreId())
                {
                    Assert.AreEqual(s.SaleId, saleId2);
                }
            }
        }

    }
}
