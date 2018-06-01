using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsep182.Domain;
using MySql.Data.MySqlClient;

namespace WebServices.DAL
{
    public class UserCartDB : BaseDBConnector<UserCart>
    {
        public UserCartDB(String mode) : base(mode) { }

        public override bool Add(UserCart uc)
        {
            try
            {
                con.Open();

                string sql = "INSERT INTO UserCart (userName, saleId, amount, offer, couponActivated, price, priceAfterDiscount)" +
                             " VALUES ('" + uc.UserName + "', " + uc.SaleId + ", " + uc.Amount + ", " + uc.Offer + ", " + uc.CouponActivated +
                             ", " + uc.Price + ", " + uc.PriceAfterDiscount + ")";
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

        public override LinkedList<UserCart> Get()
        {
            try
            {
                string sql = " SELECT * FROM UserCart";
                LinkedList<UserCart> userCart = new LinkedList<UserCart>();

                MySqlCommand cmd = new MySqlCommand(sql, con);

                con.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //userName, saleId, amount, offer, couponActivated, price, priceAfterDiscount
                    string userName = reader.GetString("userName");
                    int saleId = reader.GetInt32("saleId");
                    int amount = reader.GetInt32("amount");
                    double offer = reader.GetDouble("offer");
                    Boolean couponA = reader.GetBoolean("couponActivated");
                    double price = reader.GetDouble("price");
                    double priceAfterDiscount = reader.GetDouble("priceAfterDiscount");

                    UserCart toAdd = new UserCart(userName, saleId, amount);
                    toAdd.Offer = offer;
                    toAdd.CouponActivated = couponA;
                    toAdd.Price = price;
                    toAdd.PriceAfterDiscount = priceAfterDiscount;

                    userCart.AddLast(toAdd);
                }
                con.Close();
                return userCart;
            }
            catch (Exception e)
            {
                con.Close();
                throw new Exception("DB ERROR");
            }
        }

        public override bool Remove(UserCart uc)
        {
            try
            {
                con.Open();
                string sql = "DELETE FROM UserCart WHERE userName is '" + uc.UserName + "' AND saleId = " + uc.SaleId + ";";
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