using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using WebServices.DAL;
using WebServices.Domain;


namespace wsep182.Domain
{
    public class UserManager
    {
        private static UserManager instance;
        private UserDB UDB;
        private LinkedList<User> users;
        private UserManager()
        {
            UDB = new UserDB(configuration.DB_MODE);
            users = UDB.Get();
        }
        public static UserManager getInstance()
        {
            if (instance == null)
                instance = new UserManager();
            return instance;
        }

        public static void restartInstance()
        {
            instance = new UserManager();
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
            newUser.setPassword(encrypt(newUser.getUserName() + newUser.getPassword()));
            UDB.Add(newUser);
            users.AddLast(newUser);
            return 0;
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

        public Boolean updateUser(User newUser)
        {
            foreach (User u in users)
            {
                if (u.getUserName().Equals(newUser.getUserName()))
                {
                    newUser.setPassword(encrypt(newUser.getUserName() + newUser.getPassword()));
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
