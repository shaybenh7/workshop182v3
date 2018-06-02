using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsep182.Domain;

namespace WebServices.DAL
{
    public class UsersNotificationPreferencesDB : BaseDBConnector<Tuple<int, String, int>>
    {

        public UsersNotificationPreferencesDB(String mode) : base(mode) { }

        public override bool Add(Tuple<int, String, int> pref)
        {
            try
            {
                con.Open();

                string sql = "INSERT INTO UsersNotificationPreferences (category, username, storeId)" +
                             " VALUES (" + pref.Item1 + ", '" + pref.Item2 + "', " + pref.Item3 + ")";
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

        public override LinkedList<Tuple<int, String, int>> Get()
        {
            try
            {
                string sql = " SELECT * FROM UsersNotificationPreferences";
                LinkedList<Tuple<int, String, int>> pref = new LinkedList<Tuple<int, String, int>>();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int category = reader.GetInt32("category");
                    string username = reader.GetString("username");
                    int storeId = reader.GetInt32("storeId");
                    pref.AddLast(new Tuple<int, String, int>(category,username, storeId));
                }
                con.Close();
                return pref;
            }
            catch (Exception e)
            {
                con.Close();
                throw new Exception("DB ERROR");
            }

        }

        public override bool Remove(Tuple<int, String, int> pref)
        {
            try
            {
                con.Open();
                string sql = "DELETE FROM UsersNotificationPreferences WHERE category = " + pref.Item1 + " AND username = '"+ pref.Item2+"' AND storeId = "+pref.Item3+"; ";
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