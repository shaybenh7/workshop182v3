using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsep182.Domain;

namespace WebServices.DAL
{
    public class SaleDB : BaseDBConnector<Sale>
    {
        public SaleDB(String mode) : base(mode) { }

        public override bool Add(Sale s)
        {
            try
            {
                con.Open();
                string sql = "INSERT INTO Sale (saleId, productInStoreId, typeOfSale, amount, dueDate)" +
                             " VALUES (" + s.SaleId + ", " + s.ProductInStoreId + ", " + 
                               s.TypeOfSale + ", " + s.Amount + ", '" + s.DueDate +"')";
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

        public override LinkedList<Sale> Get()
        {
            string sql = " SELECT * FROM Sale";
            LinkedList<Sale> Sales = new LinkedList<Sale>();

            MySqlCommand cmd = new MySqlCommand(sql, con);

            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int saleId = reader.GetInt32("saleId");
                int productInStoreId = reader.GetInt32("productInStoreId");
                int typeOfSale = reader.GetInt32("typeOfSale");
                int amount = reader.GetInt32("amount");
                string dueDate = reader.GetString("dueDate");

                Sale s = new Sale(saleId, productInStoreId, typeOfSale, amount, dueDate);
                Sales.AddLast(s);
            }
            con.Close();
            return Sales;
        }

        public override bool Remove(Sale s)
        {
            try
            {
                con.Open();
                string sql = "DELETE FROM Sale WHERE saleId = " + s.SaleId + "; ";
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