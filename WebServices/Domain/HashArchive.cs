using System;
using System.Collections.Generic;
using System.Security.Cryptography;


namespace wsep182.Domain
{
    public class HashArchive
    {
        private static HashArchive instance;
        private Dictionary<string, User> hashes;
        private HashArchive()
        {
            hashes = new Dictionary<string,User>();
        }
        public static HashArchive getInstance()
        {
            if (instance == null)
                instance = new HashArchive();
            return instance;
        }

        public static void restartInstance()
        {
            instance = new HashArchive();
        }
        public Boolean configureUser(string hash, User user)
        {
            if (hash == null || user == null)
                return false;
            if (!hashes.ContainsKey(hash))
            {
                hashes.Add(hash, user);
                return true;
            }
            else
            {
                hashes[hash] = user;
                return true;
            }
        }
        
        public string generateID()
        {
            return Guid.NewGuid().ToString("N");
        }


        public User getUserByHash(string hash)
        {
            if (hash == null)
                return null;
            if (hashes.ContainsKey(hash))
            {
                return hashes[hash];
            }
            return null;
        }

        public String getHashByUserName(String userName)
        {
            foreach (KeyValuePair<string, User> entry in hashes)
            {
                if (entry.Value.getUserName() == userName)
                    return entry.Key;
            }
            return null;
        }

        public void UpdateUserName(String OldUsername, User NewUsername)
        {
            foreach (KeyValuePair<string, User> entry in hashes)
            {
                if (entry.Value.getUserName() == OldUsername)
                {
                    hashes[entry.Key] = NewUsername;
                }
            }
        }

    }
}
