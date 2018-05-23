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

        private string Production_DB = "host=sql7.freemysqlhosting.net;user=sql7239352;password=QlLSHgNryg;database=sql7239352; SslMode=none";
        public BaseDBConnector(string mode)
        {
            if (mode == "Production")
                con = new MySqlConnection(Production_DB);
        }

        public abstract LinkedList<DAO> Get();
   
        public abstract Boolean Add(DAO obj);

        public abstract Boolean Remove(DAO obj);
    }
}