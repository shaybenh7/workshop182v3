using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class User
    {
        private UserState state;
        public String userName;
        private String password;
        private ShoppingCart shoppingCart;
        private Boolean isActive;
        public User(string userName, string password)
        {
            this.password = password;
            this.userName = userName;
            isActive = true;
            state = new Guest();
            shoppingCart = new ShoppingCart();
        }
        
        public String getUserName()
        {
            return userName;
        }
        public String getPassword()
        {
            return password;
        }
        public UserState getState()
        {
            User user = UserArchive.getInstance().getUser(userName);
            if (user == null)
                return new Guest();
            return user.state;
        }
        void setState(UserState s)
        {
            state = s;
        }
        
        public User logOut()
        {
            state = new Guest();
            userName = "guest";
            password = "guest";
            return this;
        }

        public LinkedList<UserCart> getShoppingCart()
        {
            return shoppingCart.getShoppingCartProducts(this);
        }

        /*
         * return:
         *          0 if login success
         *          -1 username not exist
         *          -2 password not exist
         *          -3 user is removed
         *          -4 you are allready logged in
         */
        public int login(String username, String password)
        {
            int user = state.login(username, password);
            if (user == 0 )
            {
                if (username == "admin" || username == "admin1")
                    state = new Admin();
                else
                    state = new LogedIn();
                this.userName = username;
                this.password = password;

                return 0;
            }
            return user;
        }
        /*
         * return:
         *           0 on sucess
         *          -1 if username is empty
         *          -2 if password is empty
         *          -3 if username contains spaces
         *          -4 if username allready exist in the system
         *          -5 if you are allready logged in
         */
        public int register(String username, String password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)
                || username.Equals("") || password.Equals("") || username.Contains(" "))
            {
                if (username==null || username.Equals(""))
                    return -1;
                if (password == null || password.Equals(""))
                    return -2;
                if (username.Contains(" "))
                    return -3;
            }
            if (!(state is Guest))
                return -5;
            
            User u = new User(username, password);
            u.setState(state.register(username, password));
            return UserArchive.getInstance().addUser(u);
        }

        internal LinkedList<StoreRole> getAllStoreRolesOfAUser(string username)
        {
            if (username == this.userName || state is Admin)
            {
                return storeArchive.getInstance().getAllStoreRolesOfAUser(username);
            }
            return null;
        }

        /*
         * return:
         *           0 < on sucess
         *          -1 if user Not Login
         *          -2 if Store Name already exist
         *          -3 if iligale Storename
         *          -4 if 
         */
        public int createStore(String storeName)
        {
            if (storeName == null || storeName.Length==0 || String.IsNullOrWhiteSpace(storeName))
                return -3; //-3 if iligale Storename
            return this.state.createStore(storeName, this);
        }

        public int removeUser(String userName)
        {
            return this.state.removeUser(this,userName);
        }



        public int addToCart(int saleId, int amount)
        {
            if (amount <= 0)
                return -2; // amount can't be zero or lower
            return shoppingCart.addToCart(this, saleId, amount);
        }

        public int editCart(int saleId, int amount)
        {
            return shoppingCart.editCart(this, saleId, amount);
        }
        public Boolean buyProducts(String creditCard, String couponId)
        {
            return shoppingCart.buyProducts(this, creditCard,couponId);
        }

        public int addToCartRaffle(int saleId, double offer)
        {
            if (offer <= 0)
                return -2; // offer can't be zero or lower
            return shoppingCart.addToCartRaffle(this, saleId, offer);
        }

        public static LinkedList<Sale> viewSalesByProductInStoreId(int product)
        {
            LinkedList<Sale> ans = SalesArchive.getInstance().getSalesByProductInStoreId(product);
            if (ans.Count == 0)
                return null;
            return ans;
        }

        public static Sale viewSalesBySaleId(int saleId)
        {
            return SalesArchive.getInstance().getSale(saleId);
        }

        public LinkedList<Purchase> viewStoreHistory(Store store)
        {
            return state.viewStoreHistory(store,this);
        }

        public LinkedList<Purchase> viewUserHistory(String userNameToGetHistory)
        {
            if (userNameToGetHistory == null)
                return null;
            User userToGetHistory = UserArchive.getInstance().getUser(userNameToGetHistory);
            return state.viewUserHistory(userToGetHistory);
        }

        public Boolean getIsActive()
        {
            User user = UserArchive.getInstance().getUser(userName);
            if (user == null)
                return false;
            return user.isActive;
        }

        internal void setIsActive(Boolean state)
        {
            isActive = state;
            this.state = new Guest();
        }

        internal void setPassword(String newPassword)
        {
            password = newPassword;
        }
        public int removeFromCart(int saleId)
        {
            return shoppingCart.removeFromCart(this, saleId);
        }
        public Premissions getPremissions(string manager, int storeId)
        {
            Store s = storeArchive.getInstance().getStore(storeId);
            User managerUser = UserArchive.getInstance().getUser(manager);
            return state.getPremissions(managerUser, s);
        }


    }
}