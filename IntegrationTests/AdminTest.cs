using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;

namespace IntegrationTests
{
    [TestClass]
    public class AdminTest
    {
        private User zahi, itamar, niv, admin, admin1;
        private Store store;

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
            admin = new User("admin", "123456");
            admin.register("admin", "123456");
            admin.login("admin", "123456");

            admin1 = new User("admin1", "123456");
            admin1.register("admin1", "123456");

            zahi = new User("zahi", "123456");
            zahi.register("zahi", "123456");

            itamar = new User("itamar", "123456");
            itamar.register("itamar", "123456");
            itamar.login("itamar", "123456");
            int storeId = itamar.createStore("Maria&Netta Inc.");
            store = storeArchive.getInstance().getStore(storeId);
            niv = new User("niv", "123456");
            niv.register("niv", "123456");
        }

        [TestMethod]
        public void SimpleRemoveUser()
        {

            Assert.IsTrue(admin.removeUser("zahi") > -1);
            Assert.IsFalse(zahi.login("zahi", "123456") > -1);
        }
        [TestMethod]
        public void RemoveUserAdminNotLogin()
        {
            Assert.IsFalse(admin1.removeUser("zahi") > -1);
            Assert.IsTrue(zahi.login("zahi", "123456") > -1);
        }
        [TestMethod]
        public void AdminRemoveHimself()
        {
            Assert.IsFalse(admin.removeUser("admin") > -1);
        }
        [TestMethod]
        public void UserRemoveHimself()
        {
            zahi.login("zahi", "123456");
            Assert.IsFalse(zahi.removeUser("zahi") > -1);
        }
        [TestMethod]
        public void AdminRemoveAdmin()
        {
            Assert.IsTrue(admin.removeUser("admin1") > -1);
            Assert.IsFalse(admin1.login("admin1", "123456") > -1);
        }

        [TestMethod]
        public void RemoveUserTwice()
        {
            Assert.IsTrue(admin.removeUser("zahi") > -1);
            Assert.IsFalse(admin.removeUser("zahi") > -1);
            Assert.IsFalse(zahi.login("zahi", "123456") > -1);
        }
        [TestMethod]
        public void RemoveUserThatNotExist()
        {
            Assert.IsFalse(admin.removeUser("shay") > -1);
        }
        [TestMethod]
        public void RemoveCreatoreOwner()
        {
            Assert.IsFalse(admin.removeUser("itamar") > -1);
            Assert.AreEqual(store.getOwners().Count, 1);
        }
        [TestMethod]
        public void BuyHistoryStoreViewByAdmin()
        {
            niv.login("niv", "123456");
            StoreOwner itamarOwner = new StoreOwner(itamar, store);
            int colaId = itamarOwner.addProductInStore(itamar, store, "cola", 3.2, 10, "Drinks");
            ProductInStore cola = ProductArchive.getInstance().getProductInStore(colaId);
            int saleId = itamarOwner.addSaleToStore(itamar, store, cola.getProductInStoreId(), 1, 5, DateTime.Now.AddMonths(1).ToString());
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola.getProductInStoreId());
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            niv.login("niv", "123456");
            Assert.IsTrue(niv.addToCart(sale.SaleId, 1) > -1);
            LinkedList<UserCart> sc = niv.getShoppingCart();
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.First.Value.getSaleId() == saleId);
            Assert.IsTrue(niv.buyProducts("1234", ""));
            Assert.AreEqual(admin.viewStoreHistory(store).Count, 1);
        }
    }
}
