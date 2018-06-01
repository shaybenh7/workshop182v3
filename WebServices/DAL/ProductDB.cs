using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsep182.Domain;

namespace WebServices.DAL
{
    public class ProductDB: BaseDBConnector<Product>
    {
        public ProductDB(string mode) : base(mode) { }

        public override bool Add(Product p)
        {
            try
            {
                con.Open();

                string sql = "INSERT INTO Product (productId, name)" +
                             " VALUES (" + p.productId + ", '" + p.name + "')";
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

        public override LinkedList<Product> Get()
        {
            try {
            string sql = " SELECT * FROM Product";
            LinkedList<Product> products = new LinkedList<Product>();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int productId = reader.GetInt32("productId");
                string productName = reader.GetString("name");

                Product toAdd = new Product(productName);
                toAdd.productId = productId;
                products.AddLast(toAdd);
            }
            con.Close();
            return products;
            }
            catch (Exception e)
            {
                con.Close();
                throw new Exception("DB ERROR");
            }
        }

        public override bool Remove(Product p)
        {
            try
            {
                con.Open();
                string sql = "DELETE FROM Product WHERE name = '" + p.name + "'; ";
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