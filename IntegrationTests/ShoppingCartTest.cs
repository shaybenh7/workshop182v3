using System;
using wsep182.Domain;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{
    [TestClass]
    public class ShoppingCartTest
    {
        int regularSale = 1, raffleSale = 3;
        User admin,storeOwner, itamar, shay;
        Store store;
        Product cola, sprite;
        ProductInStore colaInStore, spriteInStore;
        UserManager userArchive;
        StoreManagement StoreManagement;
        ProductManager productArchive;
        DiscountsManager discountsArchive;
        CouponsManager couponsArchive;
        SalesManager salesArchive;
        PurchasePolicyManager purchasePolicyArchive;
        Sale colaRegularSale,spriteRaffleSale;

        [TestInitialize]
        public void init()
        {
            //              ARCHIVE INIT
            WebServices.DAL.CleanDB cDB = new WebServices.DAL.CleanDB();
            cDB.emptyDB();
            PurchasePolicyManager.restartInstance();
            SalesManager.restartInstance();
            DiscountsManager.restartInstance();
            CouponsManager.restartInstance();
            StoreManagement.restartInstance();
            UserManager.restartInstance();
            ProductManager.restartInstance();
            UserCartsManager.restartInstance();
            purchasePolicyArchive = PurchasePolicyManager.getInstance();
            salesArchive = SalesManager.getInstance();
            discountsArchive = DiscountsManager.getInstance();
            couponsArchive = CouponsManager.getInstance();
            productArchive = ProductManager.getInstance();
            StoreManagement = StoreManagement.getInstance();
            userArchive = UserManager.getInstance();
            //              USERS INIT
            admin = new User("admin", "123456");
            admin.register("admin", "123456");
            admin.login("admin","123456");
            storeOwner = new User("owner", "123456");
            storeOwner.register("owner", "123456");
            storeOwner.login("owner", "123456");
            itamar = new User("itamar", "123456");
            itamar.register("itamar", "123456");
            itamar.login("itamar", "123456");
            shay = new User("shay", "123456");
            shay.register("shay", "123456");
            shay.login("shay", "123456");
            //             PRODUCTS INIT
            cola = productArchive.addProduct("cola");
            sprite = productArchive.addProduct("sprite");
            //             STORES AND PRODUCTS IN STORES
            store = StoreManagement.addStore("samsung", storeOwner);
            colaInStore = productArchive.addProductInStore(cola, store, 200, 500, "cola category");
            spriteInStore =  productArchive.addProductInStore(sprite, store, 100, 200, "sprite category");
            //             SALES INIT
            colaRegularSale = salesArchive.addSale(colaInStore.getProductInStoreId(), regularSale, 50, DateTime.Now.AddDays(10).ToString());
            spriteRaffleSale = salesArchive.addSale(spriteInStore.getProductInStoreId(), raffleSale, 3, DateTime.Now.AddDays(20).ToString());
        }

        [TestMethod]
        public void viewCartWithDiscountOnProductWithNoRestictions()
        {
            // type: 1-productInStore, 2 - category, 3- Product
            discountsArchive.addNewDiscount(colaInStore.getProductInStoreId(), 1, "", 50, DateTime.Now.AddDays(20).ToString(),"");
            itamar.addToCart(colaRegularSale.SaleId, 1);
            LinkedList<UserCart> shoppingcart = itamar.getShoppingCartBeforeCheckout();
            Assert.AreEqual(shoppingcart.First.Value.Price, 500);
            Assert.AreEqual(shoppingcart.First.Value.PriceAfterDiscount, 250);
        }
        [TestMethod]
        public void viewCartWithDiscountOnProductWithTypeOfSaleRestriction1()
        {
            // type: 1-productInStore, 2 - category, 3- Product
            discountsArchive.addNewDiscount(colaInStore.getProductInStoreId(), 1, "", 50, DateTime.Now.AddDays(20).ToString(), "TOS=1");
            itamar.addToCart(colaRegularSale.SaleId, 1);
            LinkedList<UserCart> shoppingcart = itamar.getShoppingCartBeforeCheckout();
            Assert.AreEqual(shoppingcart.First.Value.Price, 500);
            Assert.AreEqual(shoppingcart.First.Value.PriceAfterDiscount, 250);
        }
        [TestMethod]
        public void viewCartWithDiscountOnProductWithTypeOfSaleRestriction2()
        {
            // type: 1-productInStore, 2 - category, 3- Product
            discountsArchive.addNewDiscount(colaInStore.getProductInStoreId(), 1, "", 50, DateTime.Now.AddDays(20).ToString(), "TOS=3");
            itamar.addToCart(colaRegularSale.SaleId, 1);
            LinkedList<UserCart> shoppingcart = itamar.getShoppingCartBeforeCheckout();
            Assert.AreEqual(shoppingcart.First.Value.Price, 500);
            Assert.AreEqual(shoppingcart.First.Value.PriceAfterDiscount, 500);
        }
        [TestMethod]
        public void checkoutWithNoDiscountAndRestrictions()
        {
            itamar.addToCart(colaRegularSale.SaleId, 5);
            itamar.getShoppingCartBeforeCheckout();
            Tuple<int, LinkedList<UserCart>> ans = itamar.checkout("israel", "ben gurion 13");
            Assert.AreEqual(ans.Item1, -1);
            Assert.AreEqual(ans.Item2.First.Value.Price, 2500);
            Assert.AreEqual(ans.Item2.First.Value.PriceAfterDiscount, 2500);
        }
        [TestMethod]
        public void checkoutWithDiscountAndFulfillTypeOfSaleRestrictions()
        {
            discountsArchive.addNewDiscount(colaInStore.getProductInStoreId(), 1, "", 50, DateTime.Now.AddDays(20).ToString(), "TOS=1");
            itamar.addToCart(colaRegularSale.SaleId, 5);
            itamar.getShoppingCartBeforeCheckout();
            Tuple<int, LinkedList<UserCart>> ans = itamar.checkout("israel", "ben gurion 13");
            Assert.AreEqual(ans.Item1, -1);
            Assert.AreEqual(ans.Item2.First.Value.Price, 2500);
            Assert.AreEqual(ans.Item2.First.Value.PriceAfterDiscount, 1250);
        }
        [TestMethod]
        public void checkoutWithDiscountAndFulfillCountryRestrictions()
        {
            discountsArchive.addNewDiscount(colaInStore.getProductInStoreId(), 1, "", 50, DateTime.Now.AddDays(20).ToString(), "COUNTRY=israel");
            itamar.addToCart(colaRegularSale.SaleId, 5);
            itamar.getShoppingCartBeforeCheckout();
            Tuple<int, LinkedList<UserCart>> ans = itamar.checkout("israel", "ben gurion 13");
            Assert.AreEqual(ans.Item1, -1);
            Assert.AreEqual(ans.Item2.First.Value.Price, 2500);
            Assert.AreEqual(ans.Item2.First.Value.PriceAfterDiscount, 1250);
        }
        [TestMethod]
        public void checkoutWithDiscountAndFulfillCountryAndTypeOfSaleRestrictions()
        {
            discountsArchive.addNewDiscount(colaInStore.getProductInStoreId(), 1, "", 50, DateTime.Now.AddDays(20).ToString(), "COUNTRY=israel/TOS=1");
            itamar.addToCart(colaRegularSale.SaleId, 5);
            itamar.getShoppingCartBeforeCheckout();
            Tuple<int, LinkedList<UserCart>> ans = itamar.checkout("israel", "ben gurion 13");
            Assert.AreEqual(ans.Item1, -1);
            Assert.AreEqual(ans.Item2.First.Value.Price, 2500);
            Assert.AreEqual(ans.Item2.First.Value.PriceAfterDiscount, 1250);
        }
        [TestMethod]
        public void checkoutWithDiscountAndNotFulfillCountryAndTypeOfSaleRestrictions()
        {
            discountsArchive.addNewDiscount(colaInStore.getProductInStoreId(), 1, "", 50, DateTime.Now.AddDays(20).ToString(), "COUNTRY=israel/TOS=1");
            itamar.addToCart(colaRegularSale.SaleId, 5);
            itamar.getShoppingCartBeforeCheckout();
            Tuple<int, LinkedList<UserCart>> ans = itamar.checkout("England", "ben gurion 13");
            Assert.AreEqual(ans.Item1, -1);
            Assert.AreEqual(ans.Item2.First.Value.Price, 2500);
            Assert.AreEqual(ans.Item2.First.Value.PriceAfterDiscount, 2500);
        }
        [TestMethod]
        public void checkoutWithFulfillingProductAmountPurchasePolicy()
        {
            purchasePolicyArchive.setAmountPolicyOnProduct("cola", 3, 10);
            int ans = itamar.addToCart(colaRegularSale.SaleId, 5);
            Assert.AreEqual(ans, 1);
            itamar.getShoppingCartBeforeCheckout();
            Tuple<int, LinkedList<UserCart>> cart = itamar.checkout("England", "ben gurion 13");
            Assert.AreEqual(cart.Item1, -1);
            Assert.AreEqual(cart.Item2.First.Value.getSaleId(), colaRegularSale.SaleId);
        }
        [TestMethod]
        public void checkoutWithFulfillingCategoryAmountPurchasePolicy()
        {
            purchasePolicyArchive.setAmountPolicyOnCategory(store.storeId, "cola category", 3, 10);
            int ans = itamar.addToCart(colaRegularSale.SaleId, 5);
            Assert.AreEqual(ans, 1);
            itamar.getShoppingCartBeforeCheckout();
            Tuple<int, LinkedList<UserCart>> cart = itamar.checkout("England", "ben gurion 13");
            Assert.AreEqual(cart.Item1, -1);
            Assert.AreEqual(cart.Item2.First.Value.getSaleId(), colaRegularSale.SaleId);
        }
        [TestMethod]
        public void checkoutWithFulfillingStoreAmountPurchasePolicy()
        {
            purchasePolicyArchive.setAmountPolicyOnStore(store.storeId, 2, 6);
            int ans = itamar.addToCart(colaRegularSale.SaleId, 5);
            Assert.AreEqual(ans, 1);
            itamar.getShoppingCartBeforeCheckout();
            Tuple<int, LinkedList<UserCart>> cart = itamar.checkout("England", "ben gurion 13");
            Assert.AreEqual(cart.Item1, -1);
            Assert.AreEqual(cart.Item2.First.Value.getSaleId(), colaRegularSale.SaleId);
        }
        [TestMethod]
        public void checkoutWithFulfillingCountyAmountPurchasePolicy()
        {
            purchasePolicyArchive.setAmountPolicyOnCountry(store.storeId, "England",1,15);
            int ans = itamar.addToCart(colaRegularSale.SaleId, 5);
            Assert.AreEqual(ans, 1);
            itamar.getShoppingCartBeforeCheckout();
            Tuple<int, LinkedList<UserCart>> cart = itamar.checkout("England", "ben gurion 13");
            Assert.AreEqual(cart.Item1, -1);
            Assert.AreEqual(cart.Item2.First.Value.getSaleId(), colaRegularSale.SaleId);
        }
        [TestMethod]
        public void checkoutWithFulfillingProductInStoreAmountPurchasePolicy()
        {
            purchasePolicyArchive.setAmountPolicyOnProductInStore(colaInStore.productInStoreId,2,7);
            int ans = itamar.addToCart(colaRegularSale.SaleId, 5);
            Assert.AreEqual(ans, 1);
            itamar.getShoppingCartBeforeCheckout();
            Tuple<int, LinkedList<UserCart>> cart = itamar.checkout("England", "ben gurion 13");
            Assert.AreEqual(cart.Item1, -1);
            Assert.AreEqual(cart.Item2.First.Value.getSaleId(), colaRegularSale.SaleId);
        }

        //              TESTS THAT DO NOT FULFILL RESTRICTIONS
        [TestMethod]
        public void checkoutWithNotFulfillingAmountPolicysOnProduct()
        {
            purchasePolicyArchive.setAmountPolicyOnProduct("cola", 3, 10);
            int ans = itamar.addToCart(colaRegularSale.SaleId, 15);
            Assert.AreEqual(ans, 1);
            itamar.getShoppingCartBeforeCheckout();
            Tuple<int, LinkedList<UserCart>> cart = itamar.checkout("England", "ben gurion 13");
            Assert.IsTrue(cart.Item1 >= 0);
        }
        [TestMethod]
        public void checkoutWithNotFulfillingAmountPolicysOnStore()
        {
            purchasePolicyArchive.setAmountPolicyOnStore(store.storeId, 3, 10);
            int ans = itamar.addToCart(colaRegularSale.SaleId, 15);
            Assert.AreEqual(ans, 1);
            itamar.getShoppingCartBeforeCheckout();
            Tuple<int, LinkedList<UserCart>> cart = itamar.checkout("England", "ben gurion 13");
            Assert.IsTrue(cart.Item1 >= 0);
        }
        [TestMethod]
        public void checkoutWithNotFulfillingAmountPolicysOnCategory()
        {
            purchasePolicyArchive.setAmountPolicyOnCategory(store.storeId,"cola category", 3, 10);
            int ans = itamar.addToCart(colaRegularSale.SaleId, 15);
            Assert.AreEqual(ans, 1);
            itamar.getShoppingCartBeforeCheckout();
            Tuple<int, LinkedList<UserCart>> cart = itamar.checkout("England", "ben gurion 13");
            Assert.IsTrue(cart.Item1 >= 0);
        }
        [TestMethod]
        public void checkoutWithNotFulfillingAmountPolicysOnProductInStore()
        {
            purchasePolicyArchive.setAmountPolicyOnProductInStore(colaInStore.productInStoreId,3, 10);
            int ans = itamar.addToCart(colaRegularSale.SaleId, 15);
            Assert.AreEqual(ans, 1);
            itamar.getShoppingCartBeforeCheckout();
            Tuple<int, LinkedList<UserCart>> cart = itamar.checkout("England", "ben gurion 13");
            Assert.IsTrue(cart.Item1 >= 0);
        }
        [TestMethod]
        public void checkoutWithNotFulfillingAmountPolicysOnCountry()
        {
            purchasePolicyArchive.setAmountPolicyOnCountry(store.storeId,"England", 3, 10);
            int ans = itamar.addToCart(colaRegularSale.SaleId, 15);
            Assert.AreEqual(ans, 1);
            itamar.getShoppingCartBeforeCheckout();
            Tuple<int, LinkedList<UserCart>> cart = itamar.checkout("England", "ben gurion 13");
            Assert.IsTrue(cart.Item1 >= 0);
        }

        //======================CHECK NO DISCOUNTS ON ALL TYPES =========================
        [TestMethod]
        public void checkoutWithNoDiscountPurchasePolicyOnProduct()
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            discountsArchive.addNewDiscount(colaInStore.getProductInStoreId(), 1, "", 50, DateTime.Now.AddDays(20).ToString(), "");
            PurchasePolicyManager.getInstance().setNoDiscountPolicyOnProduct("cola");
            itamar.addToCart(colaRegularSale.SaleId, 5);
            itamar.getShoppingCartBeforeCheckout();
            Tuple<int, LinkedList<UserCart>> ans = itamar.checkout("England", "ben gurion 13");
            Assert.AreEqual(ans.Item1, -1);
            Assert.AreEqual(ans.Item2.First.Value.Price, 2500);
            Assert.AreEqual(ans.Item2.First.Value.PriceAfterDiscount, 2500);
        }
        [TestMethod]
        public void checkoutWithNoDiscountPurchasePolicyOnStore()
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            discountsArchive.addNewDiscount(colaInStore.getProductInStoreId(), 1, "", 50, DateTime.Now.AddDays(20).ToString(), "");
            PurchasePolicyManager.getInstance().setNoDiscountPolicyOnStore(store.storeId);
            itamar.addToCart(colaRegularSale.SaleId, 5);
            itamar.getShoppingCartBeforeCheckout();
            Tuple<int, LinkedList<UserCart>> ans = itamar.checkout("England", "ben gurion 13");
            Assert.AreEqual(ans.Item1, -1);
            Assert.AreEqual(ans.Item2.First.Value.Price, 2500);
            Assert.AreEqual(ans.Item2.First.Value.PriceAfterDiscount, 2500);
        }
        [TestMethod]
        public void checkoutWithNoDiscountPurchasePolicyOnCategory()
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            discountsArchive.addNewDiscount(colaInStore.getProductInStoreId(), 1, "", 50, DateTime.Now.AddDays(20).ToString(), "");
            PurchasePolicyManager.getInstance().setNoDiscountPolicyOnCategoty(store.storeId,colaInStore.category);
            itamar.addToCart(colaRegularSale.SaleId, 5);
            itamar.getShoppingCartBeforeCheckout();
            Tuple<int, LinkedList<UserCart>> ans = itamar.checkout("England", "ben gurion 13");
            Assert.AreEqual(ans.Item1, -1);
            Assert.AreEqual(ans.Item2.First.Value.Price, 2500);
            Assert.AreEqual(ans.Item2.First.Value.PriceAfterDiscount, 2500);
        }
        [TestMethod]
        public void checkoutWithNoDiscountPurchasePolicyOnProductInStore()
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            discountsArchive.addNewDiscount(colaInStore.getProductInStoreId(), 1, "", 50, DateTime.Now.AddDays(20).ToString(), "");
            PurchasePolicyManager.getInstance().setNoDiscountPolicyOnProductInStore(colaInStore.getProductInStoreId());
            itamar.addToCart(colaRegularSale.SaleId, 5);
            itamar.getShoppingCartBeforeCheckout();
            Tuple<int, LinkedList<UserCart>> ans = itamar.checkout("England", "ben gurion 13");
            Assert.AreEqual(ans.Item1, -1);
            Assert.AreEqual(ans.Item2.First.Value.Price, 2500);
            Assert.AreEqual(ans.Item2.First.Value.PriceAfterDiscount, 2500);
        }
        [TestMethod]
        public void checkoutWithNoDiscountPurchasePolicyOnProductInCountry()
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            discountsArchive.addNewDiscount(colaInStore.getProductInStoreId(), 1, "", 50, DateTime.Now.AddDays(20).ToString(), "");
            PurchasePolicyManager.getInstance().setNoDiscountPolicyOnCountry(store.storeId, "England");
            itamar.addToCart(colaRegularSale.SaleId, 5);
            itamar.getShoppingCartBeforeCheckout();
            Tuple<int, LinkedList<UserCart>> ans = itamar.checkout("England", "ben gurion 13");
            Assert.AreEqual(ans.Item1, -1);
            Assert.AreEqual(ans.Item2.First.Value.Price, 2500);
            Assert.AreEqual(ans.Item2.First.Value.PriceAfterDiscount, 2500);
        }

    }
}
