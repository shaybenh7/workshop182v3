using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsep182.Domain;

namespace WebServices.DAL
{
    public class PurchasePolicyDB : BaseDBConnector<PurchasePolicy>
    {

        public PurchasePolicyDB(string mode) : base(mode) { }

        public override LinkedList<PurchasePolicy> Get()
        {
            try {
            string sql = " SELECT * FROM PurchasePolicy";
            LinkedList<PurchasePolicy> purchasePolicies = new LinkedList<PurchasePolicy>();

            MySqlCommand cmd = new MySqlCommand(sql, con);

            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int typeOfPolicy = reader.GetInt32("typeOfPolicy");
                string productName = reader.GetString("productName");
                int storeId = reader.GetInt32("storeId");
                string category = reader.GetString("category");
                int productInStoreId = reader.GetInt32("productInStoreId");
                string country = reader.GetString("country");
                int minAmount = reader.GetInt32("minAmount");
                int maxAmount = reader.GetInt32("maxAmount");
                Boolean noLimit = reader.GetBoolean("noLimit");
                Boolean noDiscount = reader.GetBoolean("noDiscount");
                Boolean noCoupons = reader.GetBoolean("noCoupons");

                PurchasePolicy c = new PurchasePolicy();
                c.TypeOfPolicy = typeOfPolicy;
                c.ProductName = productName;
                c.StoreId = storeId;
                c.Category = category;
                c.ProductInStoreId = productInStoreId;
                c.Country = country;
                c.MinAmount = minAmount;
                c.MaxAmount = maxAmount;
                c.NoLimit = noLimit;
                c.NoDiscount = noDiscount;
                c.NoCoupons = noCoupons;

                purchasePolicies.AddLast(c);
            }
            con.Close();
            return purchasePolicies;
            }
            catch (Exception e)
            {
                con.Close();
                throw new Exception("DB ERROR");
            }
        }

        public override Boolean Add(PurchasePolicy p)
        {
            try
            {
                con.Open();
      
        string sql = "INSERT INTO PurchasePolicy (typeOfPolicy, productName, storeId, category, productInStoreId, country," +
                    "minAmount, maxAmount, noLimit, noDiscount, noCoupons)" +
                             " VALUES (" + p.TypeOfPolicy + ", '" + p.ProductName + "', " + p.StoreId + ", '" + p.Category + "', " + p.ProductInStoreId +
                             ", '" + p.Country + "', " + p.MinAmount + ", " + p.MaxAmount + ", " + p.NoLimit + ", " + p.NoDiscount + ", " + p.NoCoupons +  ")";
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

        public override Boolean Remove(PurchasePolicy p)
        {
            try
            {
                con.Open();

                string sql = "DELETE FROM PurchasePolicy WHERE typeOfPolicy=" + p.TypeOfPolicy + " AND productName is '"+ p.ProductName + "' "+
                    "AND storeId="+ p.StoreId + " AND category is '" + p.Category +"'" + " AND productInStoreId="+p.ProductInStoreId+ "AND country is '"+p.Country+"'; ";
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