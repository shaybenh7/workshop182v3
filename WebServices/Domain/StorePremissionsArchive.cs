using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServices.DAL;
using WebServices.Domain;

namespace wsep182.Domain
{
    public class StorePremissionsArchive
    {
        private static StorePremissionsArchive instance;
        Dictionary<int, StorePremissions> privilegesOfaStore;

        private StorePremissionsDB SPDB;
        private LinkedList<Tuple<int, String, String>> storePriv;

        private StorePremissionsArchive()
        {
            privilegesOfaStore = new Dictionary<int, StorePremissions>();
            SPDB = new StorePremissionsDB(configuration.DB_MODE);
            storePriv = SPDB.Get();
            
            foreach(Tuple<int, String, String> s in storePriv)
            {
                String permission = s.Item3;
                String username = s.Item2;
                int storeId = s.Item1;
                switch (permission)
                {
                    case "addProductInStore":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
                        privilegesOfaStore[storeId].getPrivileges(username).addProductInStore(true);
                        break;

                    case "editProductInStore":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
                        privilegesOfaStore[storeId].getPrivileges(username).editProductInStore(true);
                        break;

                    case "removeProductFromStore":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
                        privilegesOfaStore[storeId].getPrivileges(username).removeProductFromStore(true);

                        break;

                    case "addStoreManager":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
                        privilegesOfaStore[storeId].getPrivileges(username).addStoreManager(true);
                        break;

                    case "removeStoreManager":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
                        privilegesOfaStore[storeId].getPrivileges(username).removeStoreManager(true);
                        break;
                    case "addManagerPermission":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
                        privilegesOfaStore[storeId].getPrivileges(username).addManagerPermission(true);
                        break;
                    case "removeManagerPermission":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
                        privilegesOfaStore[storeId].getPrivileges(username).removeManagerPermission(true);
                        break;
                    case "viewPurchasesHistory":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
                        privilegesOfaStore[storeId].getPrivileges(username).viewPurchasesHistory(true);
                        break;
                    case "removeSaleFromStore":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
                        privilegesOfaStore[storeId].getPrivileges(username).removeSaleFromStore(true);
                        break;
                    case "editSale":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
                        privilegesOfaStore[storeId].getPrivileges(username).editSale(true);
                        break;
                    case "addSaleToStore":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
                        privilegesOfaStore[storeId].getPrivileges(username).addSaleToStore(true);
                        break;
                    case "addDiscount":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
                        privilegesOfaStore[storeId].getPrivileges(username).addDiscount(true);
                        break;
                    case "addNewCoupon":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
                        privilegesOfaStore[storeId].getPrivileges(username).addNewCoupon(true);
                        break;
                    case "removeDiscount":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
                        privilegesOfaStore[storeId].getPrivileges(username).removeDiscount(true);
                        break;
                    case "removeCoupon":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
                        privilegesOfaStore[storeId].getPrivileges(username).removeCoupon(true);
                        break;
                    case "changePolicy":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
                        privilegesOfaStore[storeId].getPrivileges(username).changePolicy(true);
                        break;
                    
                    default:
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
                        break;
                }
            }
        }
        public static StorePremissionsArchive getInstance()
        {
            if (instance == null)
                instance = new StorePremissionsArchive();
            return instance;
        }

        public StorePremissions GetPremissionsOfAStore(int storeId)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
            {
                privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
            }
            return privilegesOfaStore[storeId];
        }
        public Premissions getAllPremissions(int storeId, string username)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
            return privilegesOfaStore[storeId].getPrivileges(username);
        }

        public static void restartInstance()
        {
            instance = new StorePremissionsArchive();
        }


        public void addProductInStore(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
            if (allow)
            {
                if (!checkPrivilege(storeId,username, "addProductInStore"))
                    SPDB.Add(new Tuple<int, String, String>(storeId, username, "addProductInStore"));
            }
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "addProductInStore"));
            privilegesOfaStore[storeId].getPrivileges(username).addProductInStore(allow);
        }

        public void editProductInStore(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "editProductInStore"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "editProductInStore"));
            privilegesOfaStore[storeId].getPrivileges(username).editProductInStore(allow);
        }

        public void removeProductFromStore(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "removeProductFromStore"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "removeProductFromStore"));
            privilegesOfaStore[storeId].getPrivileges(username).removeProductFromStore(allow);
        }

        public void addStoreManager(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "addStoreManager"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "addStoreManager"));
            privilegesOfaStore[storeId].getPrivileges(username).addStoreManager(allow);
        }

        public void addDiscount(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "addDiscount"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "addDiscount"));
            privilegesOfaStore[storeId].getPrivileges(username).addDiscount(allow);
        }

        public void addNewCoupon(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "addNewCoupon"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "addNewCoupon"));
            privilegesOfaStore[storeId].getPrivileges(username).addNewCoupon(allow);
        }

        public void removeDiscount(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "removeDiscount"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "removeDiscount"));
            privilegesOfaStore[storeId].getPrivileges(username).removeDiscount(allow);
        }

        public void removeCoupon(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "removeCoupon"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "removeCoupon"));
            privilegesOfaStore[storeId].getPrivileges(username).removeCoupon(allow);
        }

        public void removeStoreManager(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "removeStoreManager"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "removeStoreManager"));
            privilegesOfaStore[storeId].getPrivileges(username).removeStoreManager(allow);
        }

        public void addManagerPermission(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "addManagerPermission"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "addManagerPermission"));
            privilegesOfaStore[storeId].getPrivileges(username).addManagerPermission(allow);
        }

        public void removeManagerPermission(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "removeManagerPermission"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "removeManagerPermission"));
            privilegesOfaStore[storeId].getPrivileges(username).removeManagerPermission(allow);
        }

        public void addSaleToStore(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "addSaleToStore"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "addSaleToStore"));
            privilegesOfaStore[storeId].getPrivileges(username).addSaleToStore(allow);
        }

        public void removeSaleFromStore(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "removeSaleFromStore"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "removeSaleFromStore"));
            privilegesOfaStore[storeId].getPrivileges(username).removeSaleFromStore(allow);
        }

        public void editSale(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "editSale"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "editSale"));
            privilegesOfaStore[storeId].getPrivileges(username).editSale(allow);
        }

        public void viewPurchasesHistory(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "viewPurchasesHistory"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "viewPurchasesHistory"));
            privilegesOfaStore[storeId].getPrivileges(username).viewPurchasesHistory(allow);
        }

        public Boolean checkPrivilege(int storeId, string username, string privilege)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions(storeId));
            return privilegesOfaStore[storeId].getPrivileges(username).checkPrivilege(privilege);
        }

        public LinkedList<Tuple<int, String, String>> getManagersPermissionsInStore(int storeId)
        {
            if (StoreManagement.getInstance().getStore(storeId) == null)
                return null;
            LinkedList<Tuple<int, String, String>> ans = new LinkedList<Tuple<int, String, String>>();
            StorePremissions SP;
            try
            {
                SP= this.privilegesOfaStore[storeId];
            }
            catch(Exception e)
            {
                return ans;
            }
            Dictionary<string, Premissions> privileges = SP.getAllPrivileges();
            foreach (KeyValuePair<string, Premissions> entry in privileges)
            {
                foreach (KeyValuePair<string, Boolean> entry2 in entry.Value.getPrivileges())
                    if (entry2.Value)
                        ans.AddFirst(new Tuple<int, String, String>(storeId, entry.Key, entry2.Key));
            }
            return ans;
        }



        //public Boolean addNewCoupon(string username, string privilege)
        //{
        //   return privilegesOfaUser[username].checkPrivilege(privilege);
        //}

    }
}
