using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using wsep182.Domain;

namespace WebServices.DAL
{
    public class StoreRoleDictionaryDB : BaseDBConnector<Tuple<int,String,String>>
    {
        private static couponDB instance = null;

        public StoreRoleDictionaryDB(string mode) : base(mode) { }

        public override LinkedList<Tuple<int, String, String>> Get()
        {
            string sql = " SELECT * FROM StoreRoleDictionary";
            LinkedList<Tuple<int, String, String>> storeRoles = new LinkedList<Tuple<int, String, String>>();

            MySqlCommand cmd = new MySqlCommand(sql, con);

            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int storeId = reader.GetInt32("storeId");
                string userName = reader.GetString("userName");
                string storeRole = reader.GetString("storeRole");
                Tuple<int, String, String> t = new Tuple<int, String, String>(storeId,userName,storeRole);
                storeRoles.AddLast(t);
            }
            con.Close();
            return storeRoles;
        }

        public override Boolean Add(Tuple<int, String, String> t)
        {
            try
            {
                con.Open();

                string sql = "INSERT INTO StoreRoleDictionary (storeId, userName, storeRole)" +
                             " VALUES (" + t.Item1+ ", '" + t.Item2 + "', '" + t.Item3 + "')";
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

        public override Boolean Remove(Tuple<int, String, String> t)
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
            catch (Exception /*ex*/)
            {
                con.Close();
                return false;
            }
        }
    }
}