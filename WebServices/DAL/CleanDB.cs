using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebServices.DAL
{
    public class CleanDB 
    {
        protected MySqlConnection con;
        private string Testing_DB = "host=sql2.freesqldatabase.com;user=sql2239931;password=nL4!aB9%;database=sql2239931; SslMode=none";

        public CleanDB() {
            try
            {
                con = new MySqlConnection(Testing_DB);
            }
            catch (Exception e)
            {
                throw new Exception("DB ERROR");
            }
        }

        public bool insertData()
        {
            try
            {
                delData();
                addData();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Boolean emptyDB()
        {
            try
            {
                delData();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void delData()
        {
            con.Open();

            string sql = "DELETE from BuyHistory;";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "DELETE from Coupons;";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "DELETE from Discount;";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "DELETE from Product;";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "DELETE from ProductInStore;";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "DELETE from PurchasePolicy;";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "DELETE from RaffleSale;";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "DELETE from Sale;";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "DELETE from Store;";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "DELETE from StorePermission;";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "DELETE from StoreRoleDictionary;";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "DELETE from User;";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "DELETE from UserCart;";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
/*
            sql = "INSERT INTO `User`(`state`, `userName`, `password`, `isActive`) VALUES ( 3 , 'admin' , '" + encrypt("admin" + "123456") + "' , 1 );";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            sql = "INSERT INTO `User`(`state`, `userName`, `password`, `isActive`) VALUES ( 3 , 'admin1' , '" + encrypt("admin1" + "123456") + "' , 1 );";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            */
            con.Close();
        }
        /*
         * add Users
         * add stors
         * 
         * 
         * 
         * */
        private void addData()
        {
            try
            {
                con.Open();

                insertUsers();
                insertStore();
                insertStoreRole();
                insertStorePermission();
                insertProduct();
                insertProductInStore();
                String date = DateTime.Now.AddMonths(2).ToString();
                insertSale(date);
                insertSaleRaffle(date);

                con.Close();
            }
            catch(Exception e)
            {
                con.Close();
            }
        }
        /*
         * add 4 users:
         * aviad, shay, itamar, zahi paswword: 123
         * add admin paswword: 123
         * add notActiveUser paswword: 123 isActive=0
         * */
        private void insertUsers()
        {
            String password = "123";
            string sql = "INSERT INTO `User`(`state`, `userName`, `password`, `isActive`) VALUES ( 1 , 'aviad' , '" + encrypt("aviad" + password) + "' , 1 );";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `User`(`state`, `userName`, `password`, `isActive`) VALUES ( 3 , 'admin' , '" + encrypt("admin" + password) + "' , 1 );";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `User`(`state`, `userName`, `password`, `isActive`) VALUES ( 1 , 'shay' , '" + encrypt("shay" + password) + "' , 1 );";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `User`(`state`, `userName`, `password`, `isActive`) VALUES ( 1 , 'itamar' , '" + encrypt("itamar" + password) + "' , 1 );";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `User`(`state`, `userName`, `password`, `isActive`) VALUES ( 1 , 'zahi' , '" + encrypt("zahi" + password) + "' , 1 );";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `User`(`state`, `userName`, `password`, `isActive`) VALUES ( 1 , 'notActiveUser' , '" + encrypt("notActiveUser" + password) + "' , 0 );";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
        /*
         * add 5 stores:
         * 1-3 abowim, bros, MariaNettaInc owners: zahi, aviad, itamar
         * 4 NotActive owner: itamar
         * 5 adminStore owner admin
         * */
        private void insertStore()
        {
            string sql = "INSERT INTO `Store`(`storeId`, `isActive`, `name`, `storeCreator`) VALUES ( 1 , 1 , 'abowim' , 'zahi' )";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `Store`(`storeId`, `isActive`, `name`, `storeCreator`) VALUES ( 2 , 1 , 'bros' , 'aviad' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `Store`(`storeId`, `isActive`, `name`, `storeCreator`) VALUES ( 3 , 1 , 'MariaNettaInc' , 'itamar' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `Store`(`storeId`, `isActive`, `name`, `storeCreator`) VALUES ( 4 , 0 , 'NotActive' , 'itamar' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `Store`(`storeId`, `isActive`, `name`, `storeCreator`) VALUES ( 5 , 1 , 'adminStore' , 'admin' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
        /*
         * add 5 stores:
         * 1-3 abowim, bros, MariaNettaInc owners: zahi, aviad, itamar 
         * 2 Manager: zahi, itamar
         * 3 Manager: zahi
         * 4 NotActive owner: itamar
         * 5 adminStore owner admin
         * */
        private void insertStoreRole()
        {
            string sql = "INSERT INTO `StoreRoleDictionary`(`storeId`, `userName`, `storeRole`, `addedBy`, `timeAdded`) VALUES( 1 , 'zahi' , 'Owner' , 'zahi' , '"+ DateTime.Now.ToString() + "' )";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `StoreRoleDictionary`(`storeId`, `userName`, `storeRole`, `addedBy`, `timeAdded`) VALUES( 2 , 'aviad' , 'Owner' , 'aviad' , '" + DateTime.Now.ToString() + "' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `StoreRoleDictionary`(`storeId`, `userName`, `storeRole`, `addedBy`, `timeAdded`) VALUES( 3 , 'itamar' , 'Owner' , 'itamar' , '" + DateTime.Now.ToString() + "' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `StoreRoleDictionary`(`storeId`, `userName`, `storeRole`, `addedBy`, `timeAdded`) VALUES( 4 , 'itamar' , 'Owner' , 'itamar' , '" + DateTime.Now.ToString() + "' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `StoreRoleDictionary`(`storeId`, `userName`, `storeRole`, `addedBy`, `timeAdded`) VALUES( 5 , 'admin' , 'Owner' , 'admin' , '" + DateTime.Now.ToString() + "' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `StoreRoleDictionary`(`storeId`, `userName`, `storeRole`, `addedBy`, `timeAdded`) VALUES( 2 , 'zahi' , 'Manager' , 'aviad' , '" + DateTime.Now.ToString() + "' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `StoreRoleDictionary`(`storeId`, `userName`, `storeRole`, `addedBy`, `timeAdded`) VALUES( 2 , 'itamar' , 'Manager' , 'aviad' , '" + DateTime.Now.ToString() + "' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `StoreRoleDictionary`(`storeId`, `userName`, `storeRole`, `addedBy`, `timeAdded`) VALUES( 3 , 'zahi' , 'Manager' , 'itamar' , '" + DateTime.Now.ToString() + "' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
        /*
         * 2 Manager: zahi, itamar Permission: addProductInStore
         * 3 Manager: zahi         Permission: addProductInStore, removeProductFromStore
         * */
        private void insertStorePermission()
        {
            string sql = "INSERT INTO `StorePermission`(`storeId`, `username`, `premission`) VALUES ( 2 , 'itamar' , 'addProductInStore' )";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `StorePermission`(`storeId`, `username`, `premission`) VALUES ( 2 , 'zahi' , 'addProductInStore' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `StorePermission`(`storeId`, `username`, `premission`) VALUES ( 3 , 'zahi' , 'addProductInStore' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `StorePermission`(`storeId`, `username`, `premission`) VALUES ( 3 , 'zahi' , 'removeProductFromStore' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();   
        }
        /*
         * 1-5 cola sprit M&M chocolate mushroom
         * */
        private void insertProduct()
        {
            string sql = "INSERT INTO `Product`(`productId`, `name`) VALUES ( 1 , 'cola' )";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `Product`(`productId`, `name`) VALUES ( 2 , 'sprit' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `Product`(`productId`, `name`) VALUES (3, 'M&M')";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `Product`(`productId`, `name`) VALUES (4, 'chocolate')";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `Product`(`productId`, `name`) VALUES (5,'mushroom')";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
        /*
         * 1-6 cola sprit M&M chocolate mushroom  Stores: 1 (1,2) 2 (1,5) 1
         * Amount: 100 Active Category:Drink, Drink, Sweet, Sweet, vegetables
         * Price: 99 + PISid
         * 7 sprit Not Active
         * 
         * */
        private void insertProductInStore()
        {
            string sql = "INSERT INTO `ProductInStore`(`product`, `store`, `quantity`, `price`, `isActive`, `productInStoreId`, `category`) " +
                "VALUES ( 'cola' , 1 , 100 , 100 , 1 , 1 , 'Drink' )";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `ProductInStore`(`product`, `store`, `quantity`, `price`, `isActive`, `productInStoreId`, `category`) " +
                "VALUES ( 'sprit' , 1 , 100 , 101 , 1 , 2 , 'Drink' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `ProductInStore`(`product`, `store`, `quantity`, `price`, `isActive`, `productInStoreId`, `category`) " +
                "VALUES ( 'M&M' , 2 , 100 , 102 , 1 , 3 , 'Sweet' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `ProductInStore`(`product`, `store`, `quantity`, `price`, `isActive`, `productInStoreId`, `category`) " +
                "VALUES ( 'chocolate' , 5 , 100 , 103 , 1 , 4 , 'Sweet' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `ProductInStore`(`product`, `store`, `quantity`, `price`, `isActive`, `productInStoreId`, `category`) " +
                "VALUES ( 'mushroom' , 1 , 100 , 104 , 1 , 5 , 'vegetables' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `ProductInStore`(`product`, `store`, `quantity`, `price`, `isActive`, `productInStoreId`, `category`) " +
                "VALUES ( 'chocolate' , 1 , 100 , 105 , 1 , 6 , 'Sweet' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `ProductInStore`(`product`, `store`, `quantity`, `price`, `isActive`, `productInStoreId`, `category`) " +
                "VALUES ( 'sprit' , 2 , 100 , 106 , 0 , 7 , 'Drink' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        /*
         * insert Sales
         * 1-6 regular
         * 7-8 raffle
         * 
         * */
        private void insertSale(String date)
        {
            string sql = "INSERT INTO `Sale`(`saleId`, `productInStoreId`, `typeOfSale`, `amount`, `dueDate`) " +
                "VALUES ( 1 , 1 , 1 , 50 , '" + date + "' )";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `Sale`(`saleId`, `productInStoreId`, `typeOfSale`, `amount`, `dueDate`) " +
                "VALUES ( 2 , 2 , 1 , 50 , '" + date + "' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `Sale`(`saleId`, `productInStoreId`, `typeOfSale`, `amount`, `dueDate`) " +
                "VALUES ( 3 , 3 , 1 , 50 , '" + date + "' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `Sale`(`saleId`, `productInStoreId`, `typeOfSale`, `amount`, `dueDate`) " +
                "VALUES ( 4 , 4 , 1 , 50 , '" + date + "' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `Sale`(`saleId`, `productInStoreId`, `typeOfSale`, `amount`, `dueDate`) " +
                "VALUES ( 5 , 5 , 1 , 50 , '" + date + "' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `Sale`(`saleId`, `productInStoreId`, `typeOfSale`, `amount`, `dueDate`) " +
                "VALUES ( 6 , 6 , 1 , 50 , '" + date + "' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `Sale`(`saleId`, `productInStoreId`, `typeOfSale`, `amount`, `dueDate`) " +
                "VALUES ( 7 , 1 , 3 , 1 , '" + date + "' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `Sale`(`saleId`, `productInStoreId`, `typeOfSale`, `amount`, `dueDate`) " +
                "VALUES ( 8 , 1 , 3 , 1 , '" + date + "' )";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
        /*
         * insert Sales
         * 
         * */
        private void insertSaleRaffle(String date)
        {
            string sql = "INSERT INTO `RaffleSale`(`saleId`, `userName`, `offer`, `dueDate`) VALUES ( 7 ,'itamar', 5 ,'"+ date + "')";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO `RaffleSale`(`saleId`, `userName`, `offer`, `dueDate`) VALUES ( 8 ,'zahi', 5 ,'" + date + "')";
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();  
        }

        private static String encrypt(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA512.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

    }
}