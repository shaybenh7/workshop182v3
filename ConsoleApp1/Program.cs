    using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using WebServices.DAL;

namespace ConsoleApp1
{
    class Program
    {
        private static string Production_DB = "host=sql7.freemysqlhosting.net;user=sql7239352;password=QlLSHgNryg;database=sql7239352; SslMode=none";

        static void Main(string[] args)
        {
            CleanDB cDB = new CleanDB();
            cDB.insertData();
            /*
            string sql = " SELECT * FROM Users  ";
            MySqlConnection con = new MySqlConnection(Production_DB);
            MySqlCommand cmd = new MySqlCommand(sql, con);

            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader.GetString("Username"));
            }
            Console.ReadLine();*/
        }
    }
}
