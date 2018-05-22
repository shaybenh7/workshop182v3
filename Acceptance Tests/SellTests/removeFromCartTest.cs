using System;
using wsep182.Domain;
using wsep182.services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Acceptance_Tests.SellTests
{
    [TestClass]
    public class removeFromCartTest
    {

        private userServices us;
        private storeServices ss;
        private sellServices sellS;
        private User zahi, itamar, niv, admin, admin1; //admin,itamar , niv logedin
        private Store store, store2; //itamar owner , niv manneger
        ProductInStore cola, sprite, chicken, cow;
        int saleId1, saleId2;
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
            sellS = sellServices.getInstance();

            admin = us.startSession();
            us.register(admin, "admin", "123456");
            us.login(admin, "admin", "123456");

            admin1 = us.startSession();
            us.register(admin1, "admin1", "123456");

            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");
            int storeId2 = ss.createStore("Darkness Inc.", zahi);
            store2 = storeArchive.getInstance().getStore(storeId2);

            itamar = us.startSession();
            us.register(itamar, "itamar", "123456");
            us.login(itamar, "itamar", "123456");
            int storeId = ss.createStore("Maria&Netta Inc.", itamar);
            store = storeArchive.getInstance().getStore(storeId);

            niv = us.startSession();
            us.register(niv, "niv", "123456");
            us.login(niv, "niv", "123456");

            ss.addStoreManager(storeId, "niv", itamar);


            int colaId = ss.addProductInStore("cola", 3.2, 10, itamar, storeId, "Drinks");
            cola = ProductArchive.getInstance().getProductInStore(colaId);
            int spriteId = ss.addProductInStore("sprite", 5.2, 100, itamar, storeId, "Drinks");
            sprite = ProductArchive.getInstance().getProductInStore(spriteId);

            int chickenId = ss.addProductInStore("chicken", 50, 20, zahi, storeId2, "Food");
            chicken = ProductArchive.getInstance().getProductInStore(chickenId);
            int cowId = ss.addProductInStore("cow", 80, 40, zahi, storeId2, "Food");
            cow = ProductArchive.getInstance().getProductInStore(cowId);
            saleId1 = ss.addSaleToStore(itamar, store.getStoreId(), cola.getProductInStoreId(), 1, 5, "20/5/2018");
            saleId2 = ss.addSaleToStore(itamar, store.getStoreId(), sprite.getProductInStoreId(), 1, 20, "20/7/2019");
        }

        [TestMethod]
        public void removeExistingFromCart()
        {
            LinkedList<Sale> saleList = ss.viewSalesByStore(store.getStoreId());
            sellS.addProductToCart(niv, saleList.First.Value.SaleId, 2);

            LinkedList<UserCart> uc = sellS.viewCart(niv);
            int beforeDeletion = uc.Count;
            int check = sellS.removeFromCart(niv, saleList.First.Value.SaleId);
            Assert.IsTrue(check > -1);
            uc = sellS.viewCart(niv);
            int afterDeletion = uc.Count;
            Assert.AreEqual(beforeDeletion, afterDeletion + 1);
        }

        [TestMethod]
        public void removeNonExistingFromCart()
        {
            LinkedList<Sale> saleList = ss.viewSalesByStore(store.getStoreId());
            sellS.addProductToCart(niv, saleList.First.Value.SaleId, 2);
            LinkedList<UserCart> uc = sellS.viewCart(niv);
            int beforeDeletion = uc.Count;
            Sale nada = new Sale(4, 4, 1, 2, "");
            int check = sellS.removeFromCart(niv, nada.SaleId);
            Assert.IsFalse(check > -1);
            uc = sellS.viewCart(niv);
            int afterDeletion = uc.Count;
            Assert.AreEqual(beforeDeletion, afterDeletion);
        }

        [TestMethod]
        public void badUserInput1()
        {
            LinkedList<Sale> saleList = ss.viewSalesByStore(store.getStoreId());
            sellS.addProductToCart(niv, saleList.First.Value.SaleId, 2);
            LinkedList<UserCart> uc = sellS.viewCart(niv);
            int beforeDeletion = uc.Count;
            int check = sellS.removeFromCart(null, saleList.First.Value.SaleId);
            Assert.IsFalse(check > -1);
            uc = sellS.viewCart(niv);
            int afterDeletion = uc.Count;
            Assert.AreEqual(beforeDeletion, afterDeletion);
        }
        [TestMethod]
        public void badUserInput2()
        {
            LinkedList<Sale> saleList = ss.viewSalesByStore(store.getStoreId());
            sellS.addProductToCart(niv, saleList.First.Value.SaleId, 2);
            LinkedList<UserCart> uc = sellS.viewCart(niv);
            int beforeDeletion = uc.Count;
            int check = sellS.removeFromCart(zahi, saleList.First.Value.SaleId);
            Assert.IsFalse(check > -1);
            uc = sellS.viewCart(niv);
            int afterDeletion = uc.Count;
            Assert.AreEqual(beforeDeletion, afterDeletion);
        }
        [TestMethod]
        public void badSaleInput1()
        {
            LinkedList<Sale> saleList = ss.viewSalesByStore(store.getStoreId());
            sellS.addProductToCart(niv, saleList.First.Value.SaleId, 2);
            LinkedList<UserCart> uc = sellS.viewCart(niv);
            int beforeDeletion = uc.Count;
            int check = sellS.removeFromCart(niv, -31);
            Assert.IsFalse(check > -1);
            uc = sellS.viewCart(niv);
            int afterDeletion = uc.Count;
            Assert.AreEqual(beforeDeletion, afterDeletion);
        }

    }
}
