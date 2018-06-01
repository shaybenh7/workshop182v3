using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsep182.Domain;

namespace WebServices.Domain
{
    public class NotificationPublisher
    {
        public enum NotificationCategories { RaffleSale, Store, Purchase };
        private Dictionary<NotificationCategories, LinkedList<StoreRole>> usersPreferences;
        private static NotificationPublisher instance;
        private NotificationPublisher()
        {
            usersPreferences = new Dictionary<NotificationCategories, LinkedList<StoreRole>>();
        }
        public static NotificationPublisher getInstance()
        {
            if (instance == null)
                instance = new NotificationPublisher();
            return instance;
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
            usersPreferences[category].AddLast(storeRole);
        }

        public void removeFromCategory(StoreRole storeRole, NotificationCategories category)
        {
            if (!usersPreferences.ContainsKey(category))
                return;
            foreach (StoreRole sR in usersPreferences[category])
                if (sR.user.getUserName() == storeRole.user.getUserName() && sR.store.getStoreId() == storeRole.store.getStoreId())
                {
                    usersPreferences[category].Remove(sR);
                    return;
                }
        }
    }
}