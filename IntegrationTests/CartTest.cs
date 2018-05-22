using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;

namespace IntegrationTests
{
    [TestClass]
    public class CartTest
    {
        private User zahi;  // owner of store
        private User aviad; //manager of store
        private User shay;
        private User itamar; // not a real user
        private User niv; // guest
        private Store store;
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
            zahiOwner = new StoreOwner(zahi, store);
            aviadManeger = new StoreManager(aviad, store);
            zahiOwner.addStoreManager(zahi, store, "aviad");
            niv.logOut();
            int colaId = zahiOwner.addProductInStore(zahi, store, "cola", 3.2, 10, "Drinks");
            cola = ProductArchive.getInstance().getProductInStore(colaId);
        }
        [TestMethod]
        public void AddProductToCart()
        {
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> saleList = store.getAllSales();
            Assert.IsTrue(aviad.addToCart(saleList.First.Value.SaleId, 1) > -1);
            Assert.AreEqual(aviad.getShoppingCart().First.Value.getSaleId(), saleId);
        }
        [TestMethod]
        public void AddProductToCartByGuest()
        {
            
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> saleList = store.getAllSales();
            Assert.IsTrue(niv.addToCart(saleList.First.Value.SaleId, 1) > -1);
            Assert.AreEqual(niv.getShoppingCart().First.Value.getSaleId(), saleId);
        }
        [TestMethod]
        public void AddProductToCartByGuestNotEnoghInStock()
        {

            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> saleList = store.getAllSales();
            Assert.IsFalse(niv.addToCart(saleList.First.Value.SaleId, 6) > -1);
            Assert.AreEqual(niv.getShoppingCart().Count, 0);
        }
        [TestMethod]
        public void AddProductToCartTwice()
        {
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> saleList = store.getAllSales();
            Assert.IsTrue(aviad.addToCart(saleList.First.Value.SaleId, 1) > -1);
            Assert.IsTrue(aviad.addToCart(saleList.First.Value.SaleId, 1) > -1);
            Assert.AreEqual(aviad.getShoppingCart().First.Value.getSaleId(), saleId);
        }
        [TestMethod]
        public void AddProductToCartWrongAmount()
        {
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> saleList = store.getAllSales();
            Assert.IsFalse(aviad.addToCart(saleList.First.Value.SaleId, 6) > -1);
            Assert.IsFalse(aviad.addToCart(saleList.First.Value.SaleId, -1) > -1);
        }


        [TestMethod]
        public void AddRaffleProductToCart()
        {
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 3, 1, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> saleList = store.getAllSales(); ;
            Assert.IsTrue(aviad.addToCartRaffle(saleList.First.Value.SaleId, 1) > -1);
        }

        [TestMethod]
        public void EditAmount()
        {
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> saleList = store.getAllSales();
            aviad.addToCart(saleList.First.Value.SaleId, 1);
            int check = aviad.editCart(saleList.First.Value.SaleId, 4);
            Assert.IsTrue(check > -1);
            LinkedList<UserCart> aviadCart = aviad.getShoppingCart();
            UserCart uc = aviadCart.First.Value;
            Assert.AreEqual(uc.getAmount(), 4);
        }
        [TestMethod]
        public void EditAmountBadAmount()
        {
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> saleList = store.getAllSales();
            aviad.addToCart(saleList.First.Value.SaleId, 1);
            int check = aviad.editCart(saleList.First.Value.SaleId, -2);
            Assert.IsFalse(check > -1);
            LinkedList<UserCart> aviadCart = aviad.getShoppingCart();
            UserCart uc = aviadCart.First.Value;
            Assert.AreEqual(uc.getAmount(), 1);
        }

        [TestMethod]
        public void viewSaleByProductInStoreId()
        {
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            Assert.AreEqual(User.viewSalesByProductInStoreId(cola.getProductInStoreId()).Count, 1);
        }
        [TestMethod]
        public void viewSaleByProductInStoreIdTwiceAddSale()
        {
            zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 3, DateTime.Now.AddMonths(1).ToString());

            Assert.AreEqual(User.viewSalesByProductInStoreId(cola.getProductInStoreId()).Count, 1);
        }

        [TestMethod]
        public void removeProductFromCart()
        {

            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> saleList = store.getAllSales();
            aviad.addToCart(saleList.First.Value.SaleId, 1);
            Assert.IsTrue(aviad.removeFromCart(saleId) > -1);
            Assert.AreEqual(aviad.getShoppingCart().Count, 0);
        }
        [TestMethod]
        public void removeProductFromCartByGuest()
        {

            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> saleList = store.getAllSales();
            niv.addToCart(saleList.First.Value.SaleId, 1);
            Assert.IsTrue(niv.removeFromCart(saleId)>-1);
            Assert.AreEqual(niv.getShoppingCart().Count,0);
        }
        [TestMethod]
        public void removeProductFromCartNoSuchSale()
        {

            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> saleList = store.getAllSales();
            niv.addToCart(saleList.First.Value.SaleId, 1);
            Assert.IsFalse( niv.removeFromCart(saleId+1) > -1);
            Assert.AreEqual(niv.getShoppingCart().Count, 1);
        }

        //User is creating a store, adding products
        //another user is buying the products from him
        [TestMethod]
        public void TranscationImidiat()
        {
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 1) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCart();
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.First.Value.getSaleId() == saleId);
            Assert.IsTrue(aviad.buyProducts("1234", ""));
        }
        [TestMethod]
        public void TranscationRaffle()
        {
            int saleId = zahiOwner.addSaleToStore(zahi, store, cola.getProductInStoreId(), 3, 1, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCartRaffle(sale.SaleId, 1) > -1);
            LinkedList<UserCart> sc = aviad.getShoppingCart();
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.First.Value.getSaleId() == saleId);
            Assert.IsTrue(aviad.buyProducts("1234", ""));
        }
    }
}
