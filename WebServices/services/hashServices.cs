using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wsep182.Domain;

namespace wsep182.services
{
    public class hashServices
    {
        public static Boolean configureUser(string hash, User user)
        {
            return HashArchive.getInstance().configureUser(hash, user);
        }

        public static string generateID()
        {
            return HashArchive.getInstance().generateID();
        }


        public static User getUserByHash(string hash)
        {
            return HashArchive.getInstance().getUserByHash(hash);
        }

        public static String getHashByUserName(String username)
        {
            return HashArchive.getInstance().getHashByUserName(username);
        }

    }
}
