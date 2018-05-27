using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using wsep182.Domain;


namespace WebServices.DAL
{
    public class StorePremissionsDB : BaseDBConnector< Tuple<int, String, String> >
    {
        public StorePremissionsDB(string mode) : base(mode) { }

        public override Boolean Add(Tuple<int, String, String> t)
        {
            try
            {
            string sql = "INSERT INTO StorePermission (storeId, username, premission, isActive)" +
                             " VALUES (" + t.Item1 + ", '" + t.Item2 + "', '" + t.Item3 + "', "+ 1 + ") " +
                             " ON DUPLICATE KEY UPDATE " +
                             " isActive ="+1;
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

        public override LinkedList<Tuple<int, String, String>> Get()
        {
            string sql = " SELECT * FROM StorePermission WHERE isActive= " + 1;
            LinkedList<Tuple<int, String, String>> StorePer = new LinkedList<Tuple<int, String, String>>();

            MySqlCommand cmd = new MySqlCommand(sql, con);

            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int storeId = reader.GetInt32("storeId");
                string userName = reader.GetString("username");
                string premission = reader.GetString("premission");
                Tuple<int, String, String> t = new Tuple<int, String, String>(storeId, userName, premission);
                StorePer.AddLast(t);
            }
            con.Close();
            return StorePer;
        }

        public override bool Remove(Tuple<int, String, String> t)
        {
            try
            {
                con.Open();

                string sql = "UPDATE FROM StorePermission SET isActive=" + 0 +"WHERE storeId = " + t.Item1 + " AND username = '" + t.Item2 + "' AND premission ='" + t.Item3 +" ; ";
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