using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using wsep182.Domain;

namespace WebServices.DAL
{
    public class StoreRoleDictionaryDB : BaseDBConnector<Tuple<int,String,String,String,String>>
    {

        public StoreRoleDictionaryDB(string mode) : base(mode) { }

        public override LinkedList<Tuple<int, String, String,String,String>> Get()
        {
            try {
            string sql = " SELECT * FROM StoreRoleDictionary";
            LinkedList<Tuple<int, String, String,String,String>> storeRoles = new LinkedList<Tuple<int, String, String,String,String>>();

            MySqlCommand cmd = new MySqlCommand(sql, con);

            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int storeId = reader.GetInt32("storeId");
                string userName = reader.GetString("userName");
                string storeRole = reader.GetString("storeRole");
                string addedBy = reader.GetString("addedBy");
                string timeAdded = reader.GetString("timeAdded");
                Tuple<int, String, String,String,String> t = new Tuple<int, String, String,String,String>(storeId,userName,storeRole, addedBy, timeAdded);
                storeRoles.AddLast(t);
            }
            con.Close();
            return storeRoles;
            }
            catch (Exception e)
            {
                con.Close();
                throw new Exception("DB ERROR");
            }
        }

        public override Boolean Add(Tuple<int, String, String,String,String> t)
        {
            try
            {
                con.Open();

                string sql = "INSERT INTO StoreRoleDictionary (storeId, userName, storeRole, addedBy, timeAdded)" +
                             " VALUES (" + t.Item1+ ", '" + t.Item2 + "', '" + t.Item3 + "', '" + t.Item4 + "', '" + t.Item5 + "')";
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

        public override Boolean Remove(Tuple<int, String, String, String,String> t)
        {
            try
            {
                con.Open();

                string sql = "DELETE FROM StoreRoleDictionary WHERE storeId = " + t.Item1 + " AND userName = '"+t.Item2+"'; ";
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