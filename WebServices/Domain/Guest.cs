using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class Guest : UserState
    {
        public override UserState register(String username, String password)
        {
            if (username == null || password == null)
                return null;
            if (username.Equals("") || password.Equals(""))
                return null;
            if (username.Contains(" "))
                return null;
            return new LogedIn();
        }
        /*
        * return:
        *          0 if login success
        *          -1 user is not exist
        *          -2 password not exist
        *          -3 user is removed
        */
        public override int login(String username, String password)
        {
            User u = UserManager.getInstance().getUser(username);
            if (u != null)
            {
                password = encrypt(username + password);
                if (u.getPassword() == password)
                {
                    if (!u.getIsActive())
                        return -3;
                    return 0;
                }
                else
                    return -2;
            }
            return -1;
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
        public override Boolean isLogedIn()
        {
            return false;
        }

        public override LinkedList<Purchase> viewStoreHistory(Store store, User session)
        {
            return null;
        }

        public override int createStore(String storeName, User session)
        {
            return -1;// -1 if user Not Login
        }

        public override Premissions getPremissions(User manager, Store s)
        {
            return null;
        }
    }
}
