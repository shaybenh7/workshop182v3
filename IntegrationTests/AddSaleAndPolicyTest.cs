using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;

namespace IntegrationTests
{
    [TestClass]
    public class AddSaleAndPolicyTest
    {
        private User zahi;  // owner of store
        private User aviad; //manager of store
        private User shay;
        private User itamar; // not a real user
        private User niv; // guest
        private Store store, store2;
        StoreRole zahiOwner;
        StoreRole aviadManeger;
        ProductInStore cola;

        [TestInitialize]
        public void init()
        {
            ProductArchive.restartInstance();
            SalesArchive.restartInstance();
            storeArchive.restartInstance();
            UserArchive.restartInstance();
            UserCartsArchive.restartInstance();
            StorePremissionsArchive.restartInstance();

            BuyHistoryArchive.restartInstance();
            CouponsArchive.restartInstance();
            DiscountsArchive.restartInstance();
            RaffleSalesArchive.restartInstance();
            StorePremissionsArchive.restartInstance();
            zahi = new User("zahi", "123456");
            zahi.register("zahi", "123456");
            zahi.login("zahi", "123456");
            aviad = new User("aviad", "123456");
            aviad.register("aviad", "123456");
            aviad.login("aviad", "123456");
            shay = new User("shay", "123456");
            shay.register("shay", "123456");
            shay.login("shay", "123456");
            itamar = new User("itamar", "123456");
            niv = new User("niv", "123456");
            niv.register("niv", "123456");
            int storeId = zahi.createStore("abowim");
            store = storeArchive.getInstance().getStore(storeId);
            int storeId2 = zahi.createStore("broes");
            store2 = storeArchive.getInstance().getStore(storeId2);
            zahiOwner = new StoreOwner(zahi, store);

            aviadManeger = new StoreManager(aviad, store);
            zahiOwner.addStoreManager(zahi, store, "aviad");
            niv.logOut();
            int colaId = zahiOwner.addProductInStore(zahi, store, "cola", 3.2, 40, "Drinks");
            cola = ProductArchive.getInstance().getProductInStore(colaId);
        }
        [TestMethod]
        public void simpleAddSalePolicyToStore()
        {
            Assert.IsTrue(zahiOwner.setAmountPolicyOnStore(zahi, store.getStoreId(), 2, 10) > -1);
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 20, DateTime.Now.AddMonths(1).ToString());
            Assert.IsTrue(saleId > -1);
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 2) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCartBeforeCheckout();
            Assert.IsTrue(aviad.checkout("Israel", "mazada 69").Item1==-1);
        }
        [TestMethod]
        public void AddSalePolicyLowAmountToStore()
        {
            Assert.IsTrue(zahiOwner.setAmountPolicyOnStore(zahi, store.getStoreId(), 2, 10) > -1);
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 20, DateTime.Now.AddMonths(1).ToString());
            Assert.IsTrue(saleId > -1);
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 1) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCartBeforeCheckout();
            Assert.IsFalse(aviad.checkout("Israel", "mazada 69").Item1 == -1);
        }
        [TestMethod]
        public void AddSalePolicyHightAmountToStore()
        {
            Assert.IsTrue(zahiOwner.setAmountPolicyOnStore(zahi, store.getStoreId(), 2, 10) > -1);
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 20, DateTime.Now.AddMonths(1).ToString());
            Assert.IsTrue(saleId > -1);
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 11) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCartBeforeCheckout();
            Assert.IsFalse(aviad.checkout("Israel", "mazada 69").Item1 == -1);
        }

        [TestMethod]
        public void simpleAddSalePolicyOnCategory()
        {
            Assert.IsTrue(zahiOwner.setAmountPolicyOnCategory(zahi, store.getStoreId(),"Drinks", 2, 10) > -1);
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 20, DateTime.Now.AddMonths(1).ToString());
            Assert.IsTrue(saleId > -1);
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 2) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCartBeforeCheckout();
            Assert.IsTrue(aviad.checkout("Israel", "mazada 69").Item1 == -1);
        }
        [TestMethod]
        public void AddSalePolicyLowAmountOnCategory()
        {
            Assert.IsTrue(zahiOwner.setAmountPolicyOnCategory(zahi, store.getStoreId(), "Drinks", 2, 10) > -1);
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 20, DateTime.Now.AddMonths(1).ToString());
            Assert.IsTrue(saleId > -1);
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 1) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCartBeforeCheckout();
            Assert.IsFalse(aviad.checkout("Israel", "mazada 69").Item1 == -1);
        }
        [TestMethod]
        public void AddSalePolicyHightAmountOnCategory()
        {
            Assert.IsTrue(zahiOwner.setAmountPolicyOnCategory(zahi, store.getStoreId(), "Drinks", 2, 10) > -1);
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 20, DateTime.Now.AddMonths(1).ToString());
            Assert.IsTrue(saleId > -1);
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 11) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCartBeforeCheckout();
            Assert.IsFalse(aviad.checkout("Israel", "mazada 69").Item1 == -1);//no iteam in cart
        }

        [TestMethod]
        public void simpleAddSalePolicyOnProductInStore()
        {
            Assert.IsTrue(zahiOwner.setAmountPolicyOnProductInStore(zahi, store.getStoreId(),cola.getProductInStoreId(), 2, 10) > -1);
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 20, DateTime.Now.AddMonths(1).ToString());
            Assert.IsTrue(saleId > -1);
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 2) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCartBeforeCheckout();
            Assert.IsTrue(aviad.checkout("Israel", "mazada 69").Item1 == -1);
        }
        [TestMethod]
        public void AddSalePolicyLowAmountOnProductInStore()
        {
            Assert.IsTrue(zahiOwner.setAmountPolicyOnProductInStore(zahi, store.getStoreId(), cola.getProductInStoreId(), 2, 10) > -1);
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 20, DateTime.Now.AddMonths(1).ToString());
            Assert.IsTrue(saleId > -1);
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 1) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCartBeforeCheckout();
            Assert.IsFalse(aviad.checkout("Israel", "mazada 69").Item1 == -1);
        }
        [TestMethod]
        public void AddSalePolicyHightAmountOnProductInStore()
        {
            Assert.IsTrue(zahiOwner.setAmountPolicyOnProductInStore(zahi, store.getStoreId(), cola.getProductInStoreId(), 2, 10) > -1);
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 20, DateTime.Now.AddMonths(1).ToString());
            Assert.IsTrue(saleId > -1);
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 11) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCartBeforeCheckout();
            Assert.IsFalse(aviad.checkout("Israel", "mazada 69").Item1 == -1);
        }

        [TestMethod]
        public void simpleAddSalePolicyOnCountry()
        {
            Assert.IsTrue(zahiOwner.setAmountPolicyOnCountry(zahi, store.getStoreId(),"Israel", 2, 10) > -1);
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 20, DateTime.Now.AddMonths(1).ToString());
            Assert.IsTrue(saleId > -1);
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 2) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCartBeforeCheckout();
            Assert.IsTrue(aviad.checkout("Israel", "mazada 69").Item1 == -1);
        }
        [TestMethod]
        public void AddSalePolicyLowAmountOnCountry()
        {
            Assert.IsTrue(zahiOwner.setAmountPolicyOnCountry(zahi, store.getStoreId(), "Israel", 2, 10) > -1);
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 20, DateTime.Now.AddMonths(1).ToString());
            Assert.IsTrue(saleId > -1);
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 1) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCartBeforeCheckout();
            Assert.IsFalse(aviad.checkout("Israel", "mazada 69").Item1 == -1);
        }
        [TestMethod]
        public void AddSalePolicyHightAmountOnCountry()
        {
            Assert.IsTrue(zahiOwner.setAmountPolicyOnCountry(zahi, store.getStoreId(), "Israel", 2, 10) > -1);
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 20, DateTime.Now.AddMonths(1).ToString());
            Assert.IsTrue(saleId > -1);
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 11) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCartBeforeCheckout();
            Assert.IsFalse(aviad.checkout("Israel", "mazada 69").Item1 == -1);
        }
        [TestMethod]
        public void AddSalePolicyOnCountryNotSame()
        {
            Assert.IsTrue(zahiOwner.setAmountPolicyOnCountry(zahi, store.getStoreId(), "Israel", 2, 10) > -1);
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 20, DateTime.Now.AddMonths(1).ToString());
            Assert.IsTrue(saleId > -1);
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 11) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCartBeforeCheckout();
            Assert.IsTrue(aviad.checkout("USA", "mazada 69").Item1 == -1);
        }
    }
}
