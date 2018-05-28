using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsep182.Domain;

namespace WebServices.DAL
{
    public class BuyHistoryDB : BaseDBConnector<Purchase>
    {
        public BuyHistoryDB(string mode):base(mode){ }


        public override LinkedList<Purchase> Get()
        {
            try {
            string sql = " SELECT * FROM BuyHistory";
            LinkedList<Purchase> buysHistory = new LinkedList<Purchase>();

            MySqlCommand cmd = new MySqlCommand(sql, con);

            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int buyId = reader.GetInt32("buyId");
                int productId = reader.GetInt32("productId");
                int storeId = reader.GetInt32("storeId");
                String userName = reader.GetString("userName"); 
                double price = reader.GetDouble("price");
                String date = reader.GetString("date");
                int amount = reader.GetInt32("amount");
                int typeOfSale = reader.GetInt32("typeOfSale");

                Purchase p = new Purchase(buyId, productId, storeId, userName, price, date, amount, typeOfSale);
                buysHistory.AddLast(p);
            }
            con.Close();
            return buysHistory;
            }
            catch (Exception e)
            {
                con.Close();
                throw new Exception("DB ERROR");
            }
        }

        public override Boolean Add(Purchase p)
        {
            try
            {
                con.Open();

                string sql = "INSERT INTO BuyHistory (buyId, productId, storeId, userName, price, date, amount, typeOfSale)"+
                             " VALUES ("+p.BuyId+", "+p.ProductId+", "+p.StoreId+", '"+p.UserName+"', "+p.Price+
                             ", '"+p.Date+"', "+p.Amount+", "+ p.TypeOfSale+")";
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

        public override Boolean Remove(Purchase p)
        {
            throw new NotImplementedException();
        }
    }
}