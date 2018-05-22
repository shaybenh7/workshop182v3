using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wsep182.Domain;

namespace wsep182.services
{
    public class sellServices
    {
        private static sellServices instance = null;

        private sellServices()
        {

        }
        public static sellServices getInstance()
        {
            if (instance == null)
            {
                instance = new sellServices();
            }
            return instance;
        }

        //req 1.3 d
        public LinkedList<Sale> viewSalesByProductInStoreId(int productInStore)
        {
            return User.viewSalesByProductInStoreId(productInStore);
        }

        //req 1.5 a
        public int addProductToCart(User user, int saleId, int amount)
        {
            if (user == null)
            {
                return -1; // user is null error
            }
            return user.addToCart(saleId, amount);
        }
        //req 1.5 b
        public int addRaffleProductToCart(User user, int saleId, double offer)
        {
            if (user == null)
            {
                return -1; // user is null error
            }
            return user.addToCartRaffle(saleId, offer);
        }

        // req 1.6 a
        public LinkedList<UserCart> viewCart(User user)
        {
            if (user == null)
                return null;
            return user.getShoppingCart();
        }
        // req 1.6 b
        public int editCart(User user, int saleId, int newAmount)
        {
            if (user == null)
                return -1; // user is null (should not ever happen)
            return user.editCart(saleId, newAmount);
        }

        //req 1.7 a
        public int removeFromCart(User user, int saleId)
        {
            if (user == null)
                return -1;
            return user.removeFromCart(saleId);
        }

        //req 1.7.1 for all the user cart
        public Boolean buyProducts(User session, String creditCard, String couponId)
        {
            if (session == null)
                return false;
            return session.buyProducts(creditCard, couponId);
        }

        public int buyProductsInCart(User session,string country, string address, string creditCard)
        {
            if (session == null|| country==null|| creditCard==null)
                return -1;
            if (country.Equals("") || address.Equals(""))
                return -2;
            if (creditCard.Equals(""))
                return -3;
            return session.buyProductsInCart(country, address, creditCard);
        }

        //VERSION 2 ADDITIONS
        public Tuple<int, LinkedList<UserCart>> checkout(User session, string country, string address, string creditcard)
        {
            LinkedList<UserCart> ans = null;
            if (session == null||country == null || address == null|| creditcard==null)
                return Tuple.Create(-2, ans); // -2 user error
            if(country.Equals("") || address.Equals("")|| creditcard.Equals(""))
                return Tuple.Create(-3, ans); // -3 country or address cannot be empty error
            return session.checkout(country, address);
        }

        public LinkedList<UserCart> getShoppingCartBeforeCheckout(User session)
        {
            if (session == null)
                return null;
            return session.getShoppingCartBeforeCheckout();
        }

        public double getRemainingSumToPayInRaffleSale(User session, int saleId)
        {
            if (session == null)
                return -1;
            return RaffleSalesArchive.getInstance().getRemainingSumToPayInRaffleSale(saleId);
        }

        public LinkedList<UserCart> applyCoupon(User session, string couponId,string country)
        {
            if (session == null)
                return null;
            return session.applyCoupon(couponId,country);
        }

    }
}
