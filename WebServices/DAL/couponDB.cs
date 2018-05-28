using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsep182.Domain;

namespace WebServices.DAL
{
    public class couponDB : BaseDBConnector<Coupon>
    {

        public couponDB(string mode) : base(mode) { }

        public override LinkedList<Coupon> Get()
        {
            try {
            string sql = " SELECT * FROM Coupons";
            LinkedList<Coupon> Coupons = new LinkedList<Coupon>();

            MySqlCommand cmd = new MySqlCommand(sql, con);

            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string couponId = reader.GetString("couponId");
                int productInStoreId = reader.GetInt32("productInStoreId");
                double percentage = reader.GetDouble("percentage");
                int type = reader.GetInt32("type");
                string category = reader.GetString("category");
                string productName = reader.GetString("productName");
                string dueDate = reader.GetString("dueDate");
                string restrictions = reader.GetString("restrictions");

                Coupon c = new Coupon(couponId, productInStoreId, percentage, dueDate);
                c.Type = type;
                c.Category = category;
                c.ProductName = productName;
                c.Restrictions = restrictions;

                Coupons.AddLast(c);
            }
            con.Close();
            return Coupons;
            }
            catch (Exception e)
            {
                con.Close();
                throw new Exception("DB ERROR");
            }
        }

        public override Boolean Add(Coupon p)
        {
            try
            {
                con.Open();

                string sql = "INSERT INTO Coupons (couponId, productInStoreId, percentage, type, category, productName, dueDate, restrictions)" +
                             " VALUES ('" + p.CouponId + "', " + p.ProductInStoreId + ", " + p.Percentage + ", " + p.Type + ", '" + p.Category +
                             "', '" + p.ProductName + "', '" + p.DueDate + "', '" + p.Restrictions + "')";
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

        public override Boolean Remove(Coupon p)
        {
            try
            {
                con.Open();

                string sql = "DELETE FROM Coupons WHERE CouponId = '"+p.CouponId+"; ";
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