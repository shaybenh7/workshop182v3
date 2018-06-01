using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsep182.Domain;

namespace WebServices.DAL
{
    public class PendingMessagesDB : BaseDBConnector<Tuple<String,String>>
    {
        public PendingMessagesDB(string mode) : base(mode) { }

        public override bool Add(Tuple<String, String> msg)
        {
            try
            {
                con.Open();

                string sql = "INSERT INTO PendingMessages (userName, message)" +
                             " VALUES (" + msg.Item1 + ", '" + msg.Item2+ "')";
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

        public override LinkedList<Tuple<String, String>> Get()
        {
            try
            {
                string sql = " SELECT * FROM PendingMessages";
                LinkedList<Tuple<String, String>> msgs = new LinkedList<Tuple<String, String>>();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string name = reader.GetString("userName");
                    string msg = reader.GetString("message");
                    msgs.AddLast(new Tuple<String,String>(name,msg));
                }
                con.Close();
                return msgs;
            }
            catch (Exception e)
            {
                con.Close();
                throw new Exception("DB ERROR");
            }
        }

        public override bool Remove(Tuple<String, String> msg)
        {
            try
            {
                con.Open();
                string sql = "DELETE FROM PendingMessages WHERE userName = '" + msg.Item1+"; ";
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