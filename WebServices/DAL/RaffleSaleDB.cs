using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsep182.Domain;

namespace WebServices.DAL
{
    public class RaffleSaleDB : BaseDBConnector<RaffleSale>
    {
        public RaffleSaleDB(string mode) : base(mode) { }

        public override bool Add(RaffleSale rs)
        {
            try
            {
                con.Open();
                string sql = "INSERT INTO RaffleSale (saleId, userName, offer, dueDate)" +
                             " VALUES (" + rs.SaleId + ", '" + rs.UserName + "', " +
                               rs.Offer + ", '" + rs.DueDate + "')";
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

        public override LinkedList<RaffleSale> Get()
        {
            string sql = " SELECT * FROM RaffleSale";
            LinkedList<RaffleSale> raffleSale = new LinkedList<RaffleSale>();

            MySqlCommand cmd = new MySqlCommand(sql, con);

            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int saleId = reader.GetInt32("saleId");
                string userName = reader.GetString("userName");
                double offer = reader.GetDouble("offer");
                string dueDate = reader.GetString("dueDate");

                RaffleSale rs = new RaffleSale(saleId, userName, offer, dueDate);
                raffleSale.AddLast(rs);
            }
            con.Close();
            return raffleSale;
        }

        public override bool Remove(RaffleSale rs)
        {
            try
            {
                con.Open();
                string sql = "DELETE FROM RaffleSale WHERE saleId = " + rs.SaleId + "; ";
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