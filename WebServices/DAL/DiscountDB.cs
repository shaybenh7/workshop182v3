using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using wsep182.Domain;

namespace WebServices.DAL
{
    public class DiscountDB : BaseDBConnector<Discount>
    {
        private static DiscountDB instance = null;

        public DiscountDB(string mode) : base(mode) { }

        public override LinkedList<Discount> Get()
        {
            string sql = " SELECT * FROM Discount";
            LinkedList<Discount> discounts = new LinkedList<Discount>();

            MySqlCommand cmd = new MySqlCommand(sql, con);

            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int productInStoreId = reader.GetInt32("productInStoreId");
                double percentage = reader.GetDouble("percentage");
                int type = reader.GetInt32("type");  // type: 1-productInStore, 2 - category, 3- Product
                string category = reader.GetString("category");
                string productName = reader.GetString("productName");
                string dueDate = reader.GetString("dueDate");
                string restrictions = reader.GetString("restrictions");
                Discount d;
                if (type==1)
                    d = new Discount(productInStoreId,type, productName,percentage, dueDate, restrictions);
                else if (type == 2)
                {
                    d = new Discount(productInStoreId, type, category, percentage, dueDate, restrictions);
                }
                else
                {
                    d = new Discount(productInStoreId, type, productName, percentage, dueDate, restrictions);
                }
                discounts.AddLast(d);
            }
            con.Close();
            return discounts;
        }

        public override Boolean Add(Discount d)
        {
            try
            {
                con.Open();

                string sql = "INSERT INTO Discount (productInStoreId, percentage, type, category, productName, dueDate, restrictions)" +
                             " VALUES (" + d.ProductInStoreId + ", " + d.Percentage + ", " + d.Type + ", '" + d.Category + "', '" + d.ProductName +
                             "', '" + d.DueDate + "', '" + d.Restrictions + "')";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }
        }

        public override Boolean Remove(Discount d)
        {
            try
            {
                con.Open();

                string sql = "DELETE FROM Discount WHERE productInStoreId = " + d.ProductInStoreId + " AND percentage = "+ d.Percentage +
                    " AND type = "+d.Type+ " AND category is '"+d.Category+ "' AND productName is '"+d.ProductName+ "' AND dueDate is '"+d.DueDate+ "' AND restrictions is '"+d.Restrictions+"'; ";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception /*ex*/)
            {
                con.Close();
                return false;
            }
        }
    }
}