using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServices.DAL;
using wsep182.Domain;

namespace WebServices.Domain
{
    public class NotificationPublisher
    {
        public enum NotificationCategories { RaffleSale = 1, Store, Purchase }; //IN DB- 1-RaffleSale, 2-Store, 3-Purchase
        private Dictionary<NotificationCategories, LinkedList<StoreRole>> usersPreferences;
        private static NotificationPublisher instance;
        UsersNotificationPreferencesDB UNPDB;
        private NotificationPublisher()
        {
            UNPDB = new UsersNotificationPreferencesDB(configuration.DB_MODE);
            usersPreferences = initUsersPreferences();
            
        }
        public static NotificationPublisher getInstance()
        {
            if (instance == null)
                instance = new NotificationPublisher();
            return instance;
        }
        
        public Dictionary<NotificationCategories, LinkedList<StoreRole>> initUsersPreferences()
        {
            LinkedList<Tuple<int, String, int>> temp = UNPDB.Get();
            Dictionary<NotificationCategories, LinkedList<StoreRole>> ans = new Dictionary<NotificationCategories, LinkedList<StoreRole>>();
            foreach (Tuple<int, String, int> pref in temp)
            {
                if (!ans.ContainsKey((NotificationCategories)pref.Item1))
                {
                    ans.Add((NotificationCategories)pref.Item1, new LinkedList<StoreRole>());
                }
                StoreRole toadd = storeArchive.getInstance().getStoreRoleByNameAndStore(pref.Item3, pref.Item2);
                if(toadd!=null)
                    ans[(NotificationCategories)pref.Item1].AddFirst(toadd);
            }
            return ans;
        }

        public void publish(NotificationCategories category, string message, int storeId)
        {
            foreach (StoreRole sR in usersPreferences[category])
            {
                if (sR.store.getStoreId() == storeId)
                    NotificationManager.getInstance().notifyUser(sR.user.getUserName(), message);
            }
        }

        public void signToCategory(StoreRole storeRole, NotificationCategories category)
        {
            if (!usersPreferences.ContainsKey(category))
                usersPreferences.Add(category, new LinkedList<StoreRole>());
            foreach (StoreRole sR in usersPreferences[category])
                if (sR.user.getUserName() == storeRole.user.getUserName() && sR.store.getStoreId() == storeRole.store.getStoreId())
                    return;
            UNPDB.Add(new Tuple<int, string, int>((int)category, storeRole.getUser().getUserName(), storeRole.getStore().getStoreId()));
            usersPreferences[category].AddLast(storeRole);
        }

        public void removeFromCategory(StoreRole storeRole, NotificationCategories category)
        {
            if (!usersPreferences.ContainsKey(category))
                return;
            foreach (StoreRole sR in usersPreferences[category])
                if (sR.user.getUserName() == storeRole.user.getUserName() && sR.store.getStoreId() == storeRole.store.getStoreId())
                {
                    UNPDB.Remove(new Tuple<int, string, int>((int)category, storeRole.getUser().getUserName(), storeRole.getStore().getStoreId()));
                    usersPreferences[category].Remove(sR);
                    return;
                }
        }
    }
}