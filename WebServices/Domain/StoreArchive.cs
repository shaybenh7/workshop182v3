using System;
using System.Collections.Generic;
using WebServices.DAL;
using WebServices.Domain;

namespace wsep182.Domain
{
    public class storeArchive
    {
        private static storeArchive instance;
        private LinkedList<Store> stores;
        private static int storeIndex;

        private Dictionary<int,Dictionary<String,StoreRole>> archive;

        private StoreDB SDB;
        private StoreRoleDictionaryDB SRDDB;
        private storeArchive()
        {
            SDB = new StoreDB(configuration.DB_MODE);
            SRDDB = new StoreRoleDictionaryDB(configuration.DB_MODE);
            stores = SDB.Get();
            archive = new Dictionary<int, Dictionary<String, StoreRole>>();
            LinkedList<Tuple<int, String, String,String,String>> temp = SRDDB.Get();
            foreach(Tuple<int, String, String,String,String> t in temp)
            {
                StoreRole sr=null;
                if (t.Item3 == "Manager")
                {
                    sr=new StoreManager(UserManager.getInstance().getUser(t.Item2), getStore(t.Item1), t.Item4, t.Item5);
                }
                else if (t.Item3 == "Owner")
                {
                    sr = new StoreOwner(UserManager.getInstance().getUser(t.Item2), getStore(t.Item1), t.Item4, t.Item5);
                }
                else if (t.Item3 == "Customer")
                {
                    sr = new Customer(UserManager.getInstance().getUser(t.Item2), getStore(t.Item1),"customer");
                }
                try
                {
                    archive.Add(t.Item1, new Dictionary<String, StoreRole>());
                    archive[t.Item1].Add(t.Item2, sr);
                }
                catch (Exception) {
                    archive[t.Item1].Add(t.Item2, sr);
                };
            }

            storeIndex = currIndex();
        }
        public int currIndex()
        {
            LinkedList<Store> temp=SDB.Get();
            int index = 0;
            foreach(Store s in temp)
            {
                if (s.getStoreId() > index)
                {
                    index = s.getStoreId();
                }
            }
            return index;
        }
        public static storeArchive getInstance()
        {
            if (instance == null)
                instance = new storeArchive();
            return instance;
        }
        public static void restartInstance()
        {
            instance = new storeArchive();
        }
        public int getNextStoreId()
        {
            return ++storeArchive.storeIndex;
        }

        public Store addStore(String storeName, User storeOwner)
        {
            Store newStore;
            lock (this)
            {
                newStore = new Store(getNextStoreId(), storeName, storeOwner);
            }
            foreach (Store s in stores)
                if (s.getStoreId() == newStore.getStoreId())
                    return null;
            SDB.Add(newStore);
            stores.AddLast(newStore);
            return newStore;
        }

        public Boolean updateStore(Store newStore)
        {
            foreach (Store s in stores)
                if (s.getStoreId() == newStore.getStoreId())
                {
                    SDB.Remove(s);
                    stores.Remove(s);
                    SDB.Add(newStore);
                    stores.AddLast(newStore);
                    return true;
                }
            return false;
        }

        public Store getStore(int storeId)
        {
            foreach (Store s in stores)
                if (s.getStoreId() == storeId)
                    return s;
            return null;
        }

        public Boolean removeStore(int storeId)
        {
            foreach (Store s in stores)
                if (s.getStoreId() == storeId)
                {
                    SDB.Remove(s);
                    s.setIsActive(0);
                    SDB.Add(s);
                    return true;
                }
            return false;
        }
        public Boolean addStoreRole(StoreRole newPremissions, int storeId, string userName)
        {
            if (!archive.ContainsKey(storeId))
                archive.Add(storeId,new Dictionary<string, StoreRole>());
            if (archive[storeId].ContainsKey(userName))
                return false;
            Tuple<int, String, String,String,String> t = new Tuple<int, String, String,String,String>(storeId, userName, newPremissions.type, newPremissions.addedby,newPremissions.dateAdded);
            SRDDB.Add(t);
            archive[storeId].Add(userName, newPremissions);
            return true;
        }

        public Boolean updateStoreRole(StoreRole newPremissions, int storeId, string userName)
        {
            if (!archive.ContainsKey(storeId))
                return false;
            if (archive[storeId].ContainsKey(userName))
            {
                Tuple<int, String, String, String, String> t = new Tuple<int, String, String, String, String>(storeId, userName, "", "", "");
                SRDDB.Remove(t);
                archive[storeId].Remove(userName);
                t = new Tuple<int, String, String, String, String>(storeId, userName, newPremissions.type, newPremissions.addedby, newPremissions.dateAdded);
                SRDDB.Add(t);
                archive[storeId].Add(userName, newPremissions);
                return true;
            }
            return false;
        }

        public StoreRole getStoreRole(Store store, User user)
        {
            if(store == null || user == null)
            {
                return null;
            }
            if (!archive.ContainsKey(store.getStoreId()) || !archive.ContainsKey(store.getStoreId()) || !archive[store.getStoreId()].ContainsKey(user.getUserName()))
                addStoreRole(new Customer(user, store),store.getStoreId(),user.getUserName());
            return archive[store.getStoreId()][user.getUserName()];
        }

        public StoreRole getStoreRoleByNameAndStore(int storeId,String username)
        {
            if (archive.ContainsKey(storeId))
                if (archive[storeId].ContainsKey(username))
                    return archive[storeId][username];
            return null;
        }

        public LinkedList<Tuple<int, String, String, String, String>> getStoreRolesStats(int storeId)
        {
            if (getStore(storeId)==null)
            {
                return null;
            }
            LinkedList<Tuple<int, String, String, String, String>> ans = new LinkedList<Tuple<int, String, String, String, String>>();
            Dictionary <String, StoreRole> temp = archive[storeId];
            foreach (KeyValuePair<String, StoreRole> entry in temp)
            {
                ans.AddFirst(new Tuple<int, string, string, string, string>(storeId, entry.Key, entry.Value.type, entry.Value.addedby, entry.Value.dateAdded));
            }
            return ans;
        }

        public Boolean removeStoreRole(int storeId, string userName)
        {
            if (!archive.ContainsKey(storeId))
                return false;
            if (archive[storeId].ContainsKey(userName))
            {
                Tuple<int, String, String,String,String> t = new Tuple<int, String, String,String,String>(storeId, userName, "","","");
                SRDDB.Remove(t);
                archive[storeId].Remove(userName);
                return true;
            }
            return false;
        }

        public LinkedList<StoreOwner> getAllOwners(int storeId)
        {
            LinkedList<StoreOwner> res = new LinkedList<StoreOwner>();
            foreach (String userName in archive[storeId].Keys)
            {
                if (archive[storeId][userName] is StoreOwner)
                    res.AddLast((StoreOwner)archive[storeId][userName]);
            }
            return res;
        }

        public LinkedList<StoreManager> getAllManagers(int storeId)
        {
            LinkedList<StoreManager> res = new LinkedList<StoreManager>();
            foreach (String userName in archive[storeId].Keys)
            {
                if (archive[storeId][userName] is StoreManager)
                    res.AddLast((StoreManager)archive[storeId][userName]);
            }
            return res;
        }

        public LinkedList<StoreRole> getAllStoreRolesOfAUser(String username)
        {
            LinkedList<StoreRole> res = new LinkedList<StoreRole>();
            foreach (int key in archive.Keys){
                foreach (String key2 in archive[key].Keys)
                {
                    if (key2 == username)
                        res.AddLast(archive[key][key2]);
                }
            }
            return res;
        }

        public LinkedList<Store> getAllStore()
        {
        
            return stores;
        }


        
    }
}
