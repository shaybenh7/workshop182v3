using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace WebServices.DAL
{
    public abstract class BaseDBConnector<DAO>
    {
        protected MySqlConnection con;
        //private string Production_DB = "check";
        private string Production_DB = "host=sql2.freemysqlhosting.net;user=sql2241146;password=aV9*bU7!;database=sql2241146; SslMode=none";
        private string Testing_DB = "host=sql2.freemysqlhosting.net;user=sql2241146;password=aV9*bU7!;database=sql2241146; SslMode=none";
        public BaseDBConnector(string mode)
        {
            try
            {
                if (mode == "Production")
                    con = new MySqlConnection(Production_DB);
                else if (mode == "Testing")
                    con = new MySqlConnection(Testing_DB);
            }
            catch(Exception e)
            {
                throw new Exception("DB ERROR");
            }
        }

        public abstract LinkedList<DAO> Get();
   
        public abstract Boolean Add(DAO obj);

        public abstract Boolean Remove(DAO obj);
    }
}