using System;
using System.Collections.Generic;
using WebServices.DAL;

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
            SDB = new StoreDB("Production");
            SRDDB = new StoreRoleDictionaryDB("Production");
            stores = SDB.Get();
            archive = new Dictionary<int, Dictionary<String, StoreRole>>();
            LinkedList<Tuple<int, String, String>> temp = SRDDB.Get();
            foreach(Tuple<int, String, String> t in temp)
            {
                StoreRole sr=null;
                if (t.Item3 == "Manager")
                {
                    sr=new StoreManager(UserArchive.getInstance().getUser(t.Item2), getStore(t.Item1));
                }
                else if (t.Item3 == "Owner")
                {
                    sr = new StoreOwner(UserArchive.getInstance().getUser(t.Item2), getStore(t.Item1));
                }
                else if (t.Item3 == "Customer")
                {
                    sr = new Customer(UserArchive.getInstance().getUser(t.Item2), getStore(t.Item1));
                }
                try
                {
                    archive.Add(t.Item1, new Dictionary<String, StoreRole>());
                    archive[t.Item1].Add(t.Item2, sr);
                }
                catch (Exception) { };
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
            stores.AddLast(newStore);
            SDB.Add(newStore);
            return newStore;
        }

        public Boolean updateStore(Store newStore)
        {
            foreach (Store s in stores)
                if (s.getStoreId() == newStore.getStoreId())
                {
                    stores.Remove(s);
                    stores.AddLast(newStore);
                    SDB.Remove(s);
                    SDB.Add(newStore);
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
                    s.setIsActive(0);
                    SDB.Remove(s);
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
            archive[storeId].Add(userName,newPremissions);
            Tuple<int, String, String> t = new Tuple<int, String, String>(storeId, userName, newPremissions.type);
            SRDDB.Add(t);
            return true;
        }

        public Boolean updateStoreRole(StoreRole newPremissions, int storeId, string userName)
        {
            if (!archive.ContainsKey(storeId))
                return false;
            if (archive[storeId].ContainsKey(userName))
            {
                
                archive[storeId].Remove(userName);
                archive[storeId].Add(userName, newPremissions);
                Tuple<int, String, String> t = new Tuple<int, String, String>(storeId, userName, "");
                SRDDB.Remove(t);
                t = new Tuple<int, String, String>(storeId, userName, newPremissions.type);
                SRDDB.Add(t);
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

        public Boolean removeStoreRole(int storeId, string userName)
        {
            if (!archive.ContainsKey(storeId))
                return false;
            if (archive[storeId].ContainsKey(userName))
            {
                Tuple<int, String, String> t = new Tuple<int, String, String>(storeId, userName, "");
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
