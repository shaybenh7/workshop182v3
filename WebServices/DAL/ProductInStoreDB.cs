using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServices.Domain;
using wsep182.Domain;

namespace WebServices.DAL
{
    public class ProductInStoreDB : BaseDBConnector<ProductInStore>
    {
        public ProductInStoreDB(string mode) : base(mode) { }

        public override bool Add(ProductInStore p)
        {
            try
            {
                con.Open();

                string sql = "INSERT INTO ProductInStore (product, store, quantity, price, isActive, productInStoreId, category)" +
                             " VALUES ('" + p.product.name + "', " + p.store.storeId + ", " + p.quantity + ", " + p.price +
                             ", " + p.isActive + ", " + p.productInStoreId + ", '" + p.category + "')";
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

        public override LinkedList<ProductInStore> Get()
        {
            try {
            string sql = " SELECT * FROM ProductInStore";
            LinkedList<ProductInStore> productInStores = new LinkedList<ProductInStore>();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            
            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();
            ProductDB pdb = new ProductDB(configuration.DB_MODE);
            while (reader.Read())
            {
                Product product = null;
                String productName = reader.GetString("product");
                LinkedList<Product> products = pdb.Get();
                foreach (Product p in products)
                    if (p.getProductName().Equals(productName))
                        product = p;
                Store store = StoreManagement.getInstance().getStore(reader.GetInt32("store"));
                int quantity = reader.GetInt32("quantity");
                double price = reader.GetDouble("price");
                int isActive = reader.GetInt32("isActive");
                int productInStoreId = reader.GetInt32("productInStoreId");
                string category = reader.GetString("category");

                ProductInStore toAdd = new ProductInStore(productInStoreId, category, product, price, quantity, store);
                productInStores.AddLast(toAdd);
            }
            con.Close();
            return productInStores;
            }
            catch (Exception e)
            {
                con.Close();
                throw new Exception("DB ERROR");
            }
        }

        public override bool Remove(ProductInStore p)
        {
            try
            {
                con.Open();
                string sql = "DELETE FROM ProductInStore WHERE productInStoreId=" + p.productInStoreId + " AND store="+p.store.storeId+"; ";
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