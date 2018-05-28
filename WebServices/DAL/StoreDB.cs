using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using wsep182.Domain;

namespace WebServices.DAL
{
    public class StoreDB : BaseDBConnector<Store>
    {
        private static StoreDB instance = null;

        public StoreDB(string mode) : base(mode) { }

        public override LinkedList<Store> Get()
        {
            try {
            string sql = " SELECT * FROM Store";
            LinkedList<Store> stores = new LinkedList<Store>();

            MySqlCommand cmd = new MySqlCommand(sql, con);

            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int storeId = reader.GetInt32("storeId");
                int isActive = reader.GetInt32("isActive");
                string name = reader.GetString("name");
                string storeCreator = reader.GetString("storeCreator");
                User creator = UserManager.getInstance().getUser(storeCreator);
                Store s=new Store(storeId,name, creator, isActive);
                stores.AddLast(s);
            }
            con.Close();
            return stores;
            }
            catch (Exception e)
            {
                con.Close();
                throw new Exception("DB ERROR");
            }
        }

        public override Boolean Add(Store s)
        {
            try
            {
                con.Open();

                string sql = "INSERT INTO Store (storeId, isActive, name, storeCreator)" +
                             " VALUES (" + s.storeId + ", " + s.isActive+ ", '" + s.name + "', '" + s.storeCreator.getUserName() + "')";
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

        public override Boolean Remove(Store s)
        {
            try
            {
                con.Open();

                string sql = "DELETE FROM Store WHERE storeId = " + s.storeId + "; ";
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