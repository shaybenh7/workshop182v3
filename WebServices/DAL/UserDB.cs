using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsep182.Domain;

namespace WebServices.DAL
{
    public class UserDB : BaseDBConnector<User>
    {

        public UserDB(String mode) : base(mode) { }

        public override bool Add(User u)
        {
            if (!addShoppingCart(u.shoppingCart.products))
            {
                return false;
            }
            try
            {
                con.Open();

                int state = 2;
                if (u.getState() is LogedIn)
                    state = 2;
                else if (u.getState() is Admin)
                    state = 3;
                string sql = "INSERT INTO User (state, userName, password, isActive)" +
                             " VALUES (" + state + ", '" + u.getUserName() + "', '" + u.getPassword() + "', " + u.getIsActive() + ")";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e)
            {
                con.Close();
                throw new Exception("DB ERROR");
            }

        }

        public override LinkedList<User> Get()
        {
            try
            {
                string sql = " SELECT * FROM User";
                LinkedList<User> users = new LinkedList<User>();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int state = reader.GetInt32("state");
                    string userName = reader.GetString("userName");
                    string password = reader.GetString("password");
                    Boolean isActive = reader.GetBoolean("isActive");
                    User u = new User(userName, password);
                    u.setIsActive(isActive);
                    if (state == 2)
                        u.setState(new LogedIn());
                    else if (state == 3)
                        u.setState(new Admin());
                    users.AddLast(u);
                }
                con.Close();
                foreach (User u in users)
                    u.shoppingCart = GetShoppingCart(u.userName);
                return users;
            }
            catch(Exception e)
            {
                con.Close();
                throw new Exception("DB ERROR");
            }

        }

        public override bool Remove(User u)
        {
            if(!removeShoppingCart(u.getUserName()))
                return false;
            try
            {
                con.Open();

                string sql = "DELETE FROM User WHERE userName = '" + u.getUserName() + "'; ";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e)
            {
                con.Close();
                throw new Exception("DB ERROR");
            }
        }


        public ShoppingCart GetShoppingCart(string userName)
        {
            try {
            ShoppingCart ans = new ShoppingCart();
            string sql = " SELECT * FROM UserCart where userName = '"+ userName +"';";
            LinkedList<UserCart> ucs = new LinkedList<UserCart>();

            MySqlCommand cmd = new MySqlCommand(sql, con);

            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int saleId = reader.GetInt32("saleId");
                int amount = reader.GetInt32("amount");
                double offer = reader.GetDouble("offer");
                Boolean couponActivated = reader.GetBoolean("couponActivated");
                double price = reader.GetDouble("price");
                double priceAfterDiscount = reader.GetDouble("priceAfterDiscount");

                UserCart uc = new UserCart(userName, saleId, amount);
                uc.Offer = offer;
                uc.CouponActivated = couponActivated;
                uc.Price = price;
                uc.PriceAfterDiscount = priceAfterDiscount;

                ucs.AddLast(uc);
            }
            con.Close();
            ans.products = ucs;

            return ans;
            }
            catch (Exception e)
            {
                con.Close();
                throw new Exception("DB ERROR");
            }
        }

        public Boolean addShoppingCart(LinkedList<UserCart> ucs)
        {
            if (ucs.Count == 0)
                return true;
            string sql = "";
            foreach(UserCart uc in ucs)
            {
                sql += "INSERT INTO UserCart (userName, saleId, amount, offer, couponActivated, price, priceAfterDiscount)" +
                             " VALUES ('" +uc.UserName + "', " + uc.SaleId + ", " + uc.Amount + ", " + uc.Offer + ", " + uc.CouponActivated +
                             ", " + uc.Price + ", " + uc.PriceAfterDiscount + ");";
            }
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e)
            {
                con.Close();
                throw new Exception("DB ERROR");
            }
        }

        public Boolean removeShoppingCart(string username)
        {
            try
            {
                con.Open();

                string sql = "DELETE FROM UserCart WHERE userName = '" + username + "'; ";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e)
            {
                con.Close();
                throw new Exception("DB ERROR");
            }
        }


    }
}