﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using WebServices.DAL;
using WebServices.Domain;


namespace wsep182.Domain
{
    public class UserArchive
    {
        private static UserArchive instance;
        private UserDB UDB;
        private LinkedList<User> users;
        private UserArchive()
        {
            UDB = new UserDB(configuration.DB_MODE);
            users = UDB.Get();
        }
        public static UserArchive getInstance()
        {
            if (instance == null)
                instance = new UserArchive();
            return instance;
        }

        public static void restartInstance()
        {
            instance = new UserArchive();
        }

        /*
         * return :
         *          -4 if username allready exist in the system
         *          0 on success
         */
        public int addUser(User newUser)
        {
            foreach (User u in users)
                if (u.getUserName().Equals(newUser.getUserName()))
                    return -4;
            //newUser.setPassword(encrypt(newUser.getUserName() + newUser.getPassword()));
            UDB.Add(newUser);
            users.AddLast(newUser);
            return 0;
        }

        private String encrypt(String password)
        {
            byte[] pwd;
            using (SHA512 shaM = new SHA512Managed())
            {
                pwd = System.Text.Encoding.UTF8.GetBytes(password);
                pwd = shaM.ComputeHash(pwd);
            }
            return System.Text.Encoding.UTF8.GetString(pwd);
        }

        public Boolean updateUser(User newUser)
        {
            foreach (User u in users)
            {
                if (u.getUserName().Equals(newUser.getUserName()))
                {
                    //newUser.setPassword(encrypt(newUser.getUserName() + newUser.getPassword()));
                    UDB.Remove(u);
                    users.Remove(u);
                    UDB.Add(newUser);
                    users.AddLast(newUser);
                    return true;
                }
            }
            return false;
        }
        public User getUser(string userName)
        {
            foreach (User u in users)
                if (u.getUserName().Equals(userName))
                    return u;
            return null;
        }

        /*
         *   0 if user removed successfuly
         *  -2 user to remove is not exist
         *  -6 user who is owner or creator of store can not be removed
         */

        public int removeUser(string userName)
        {
            foreach (User u in users)
                if (u.getUserName().Equals(userName))
                {
                    UDB.Remove(u);
                    LinkedList<Store> allStores = storeArchive.getInstance().getAllStore();
                    foreach(Store s in allStores)
                    {
                        if(s.getStoreCreator().getUserName().Equals(u.getUserName()) && s.getIsActive() == 1)
                            return -6;
                    }
                    //users.Remove(u);
                    u.setIsActive(false);
                    UDB.Add(u);
                    return 0;
                }
            return -2;
        }

    }
}
