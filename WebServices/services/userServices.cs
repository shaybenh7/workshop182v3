using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wsep182.Domain;

namespace wsep182.services
{
    public class userServices
    {
        private static userServices instance = null;

        private userServices() {
            init();
        }
        public static userServices getInstance()
        {
            if (instance == null)
            {
                instance = new userServices();
            }
            return instance;
        }
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
            PurchasePolicyArchive.restartInstance();

            storeServices ss;
            User admin;
            Store store;
            sellServices sells;
            ss = storeServices.getInstance();
            sells = sellServices.getInstance();
            admin = startSession();
            register(admin, "admin", "123456");
            login(admin, "admin", "123456");

            User adminTest = startSession();
            register(adminTest, "adminTest", "123456");
            login(adminTest, "adminTest", "123456");

            int storeid = ss.createStore("Maria&Netta Inc.", admin);
            store = storeArchive.getInstance().getStore(storeid);

            int c = ss.addProductInStore("Milk chocolate", 3.2, 30, admin, storeid, "chocolate");
            int s = ss.addProductInStore("Dark chocolate", 5.3, 30, admin, storeid, "chocolate");
            ss.addSaleToStore(admin, storeid, c, 1, 20, "18/11/2018");
            ss.addSaleToStore(admin, storeid, s, 3, 20, "18/11/2018");

        }
        // req 1.1
        public User startSession()
        {
            String s = hashServices.generateID();
            User user = new User(s, s);
            return user;
        }

        // req 1.2
        /*
         * return:
         *           0 on sucess
         *          -1 if username is empty
         *          -2 if password is empty
         *          -3 if username contains spaces
         *          -4 if username allready exist in the system
         *          -5 if you are allready logged in
         */
        public int register(User session, String username, String password)
        {
            return session.register(username, password);
        }

        //req 1.3 a
        public LinkedList<ProductInStore> viewProductsInStore(int storeId)
        {
            Store s = storeArchive.getInstance().getStore(storeId);
            if (s == null)
                return null;
            return s.getProductsInStore();
        }

        //req 1.3 b
        public LinkedList<ProductInStore> viewProductsInStores()
        {
            return ProductInStore.getAllProductsInAllStores();
        }
        //req 1.3 b
        public LinkedList<Sale> viewAllSales()
        {
            return Sale.getAllSales();
        }

        public Sale viewSalesBySaleId(int saleId)
        {
            return User.viewSalesBySaleId(saleId);
        }

            //req 1.3 c
            public LinkedList<Store> viewStores()
        {
            return Store.viewStores();
        }

        /*
         * return:
         *          0 if login success
         *          -1 username not exist
         *          -2 wrong password
         *          -3 user is removed
         *          -4 you are allready logged in
         */

        //req 2.1 
        public int login(User session, String userName, String password)
        {
            return session.login(userName, password);
        }

        /*
         * return :
         *           0 if user removed successfuly
         *          -1 if you are not loggin
         *          -2 user to remove is not exist
         *          -3 user to remove allready removed
         *          -4 user cannot remove himself
         *          -5 user who has raffle sale can not be removed
         *          -6 user who is owner or creator of store can not be removed
         */
        // req 5.2
        public int removeUser(User userMakingDeletion, String userDeleted)
        {
            return userMakingDeletion.removeUser(userDeleted);
        }

        public int setAmountPolicyOnProduct(User session, string productName, int minAmount, int maxAmount)
        {
            if (session == null)
                return -1;
            return session.getState().setAmountPolicyOnProduct(productName, minAmount, maxAmount);
        }

        public int setNoDiscountPolicyOnProduct(User session, string productName)
        {
            if (session == null)
                return -1;
            return session.getState().setNoDiscountPolicyOnProduct(productName);
        }

        public int setNoCouponsPolicyOnProduct(User session, string productName)
        {
            if (session == null)
                return -1;
            return session.getState().setNoCouponsPolicyOnProduct(productName);
        }

        public int removeAmountPolicyOnProduct(User session, string productName)
        {
            if (session == null)
                return -1;
            return session.getState().removeAmountPolicyOnProduct(productName);
        }

        public int removeNoDiscountPolicyOnProduct(User session, string productName)
        {
            if (session == null)
                return -1;
            return session.getState().removeNoDiscountPolicyOnProduct(productName);
        }

        public int removeNoCouponsPolicyOnProduct(User session, string productName)
        {
            if (session == null)
                return -1;
            return session.getState().removeNoCouponsPolicyOnProduct(productName);
        }

        public Premissions getPremissions(User session, string manager, int storeId)
        {
            return session.getPremissions(manager, storeId);
        }


        public LinkedList<StoreRole> getAllStoreRolesOfAUser(User session, String username)
        {
            return session.getAllStoreRolesOfAUser(username);
        }


  }
}
 