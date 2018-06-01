using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServices.DAL;
using WebServices.Domain;

namespace wsep182.Domain
{
    public class UserCartsManager
    {
        private static UserCartsManager instance;
        private LinkedList<UserCart> carts;
        private UserCartDB UCDB;

        private UserCartsManager()
        {
            UCDB = new UserCartDB(configuration.DB_MODE);
            carts = UCDB.Get();
        }
        public static UserCartsManager getInstance()
        {
            if (instance == null)
                instance = new UserCartsManager();
            return instance;
        }

        public static void restartInstance()
        {
            instance = new UserCartsManager();
        }
        public Boolean updateUserCarts(String userName, int saleId, int amount)
        {
            foreach (UserCart cart in carts)
            {
                if(cart.getUserName().Equals(userName) && cart.getSaleId() == saleId)
                {
                    if (UCDB.Remove(cart))
                    {
                        cart.setAmount(cart.getAmount() + amount);
                        if (UCDB.Add(cart))
                        {
                            return true;
                        }
                    }
                    return false;
                }

            }

            UserCart toAdd = new UserCart(userName, saleId, amount);
            if (UCDB.Add(toAdd))
            {
                carts.AddLast(toAdd);
                return true;
            }
            return false;
        }
        public Boolean updateUserCarts(String userName, int saleId, int amount,double offer)
        {
            foreach (UserCart cart in carts)
            {
                if (cart.getUserName().Equals(userName) && cart.getSaleId() == saleId)
                {
                    if (UCDB.Remove(cart))
                    {
                        cart.setOffer(offer);
                        if (UCDB.Add(cart))
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            UserCart toAdd = new UserCart(userName, saleId, amount);
            toAdd.setOffer(offer);
            if (UCDB.Add(toAdd))
            {
                carts.AddLast(toAdd);
                return true;
            }
            return false;
        }

        public Boolean editUserCarts(String userName, int saleId, int amount)
        {
            foreach (UserCart cart in carts)
            {
                if (cart.getUserName().Equals(userName) && cart.getSaleId() == saleId)
                {
                    if (UCDB.Remove(cart))
                    {
                        cart.setAmount(amount);
                        if (UCDB.Add(cart))
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            return false ;
        }       


        public UserCart getUserCart(String userName, int saleId)
        {
            foreach (UserCart cart in carts)
            {
                if (cart.getUserName().Equals(userName) && cart.getSaleId() == saleId)
                {
                    return cart;
                }
            }
            return null;
        }

        public LinkedList<UserCart> getUserShoppingCart(String userName)
        {
            LinkedList<UserCart> ans = new LinkedList<UserCart>();
            foreach(UserCart cart in carts)
            {
                if (cart.getUserName().Equals(userName))
                {
                    ans.AddLast(cart);
                }
            }
            return ans;
        }

        public Boolean removeUserCart(String userName, int saleId)
        {
            foreach(UserCart c in carts)
            {
                if(c.getUserName().Equals(userName) && c.getSaleId() == saleId)
                {
                    if (UCDB.Remove(c))
                    {
                        carts.Remove(c);
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }

    }
}
