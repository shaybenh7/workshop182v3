using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServices.DAL;
using WebServices.Domain;

namespace wsep182.Domain
{
    public class StorePremissions
    {
        Dictionary<string, Premissions> privileges;
        int storeId;

        public Premissions getPrivileges(string username)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            return privileges[username];
        }
        public Dictionary<string, Premissions> getAllPrivileges()
        {
            return this.privileges;
        }
        //        bool addProduct;
        public StorePremissions(int storeId)
        {
            this.storeId = storeId;
            privileges = new Dictionary<string, Premissions>();
        }
        public void addProductInStore(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username,new Premissions());
            StorePremissionsDB SPDB = new StorePremissionsDB(configuration.DB_MODE);
            if (allow)
            { 
                if(!checkPrivilege(username, "addProductInStore"))
                    SPDB.Add(new Tuple<int, String, String>(storeId, username, "addProductInStore"));
            }
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "addProductInStore"));
            privileges[username].addProductInStore(allow);
        }

        public void removeProductFromStore(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            StorePremissionsDB SPDB = new StorePremissionsDB(configuration.DB_MODE);
            if (allow)
            {
                if (!checkPrivilege(username, "removeProductFromStore"))
                    SPDB.Add(new Tuple<int, String, String>(storeId, username, "removeProductFromStore"));
            }
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "removeProductFromStore"));
            privileges[username].removeProductFromStore(allow);
        }

        public void editProductInStore(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            StorePremissionsDB SPDB = new StorePremissionsDB(configuration.DB_MODE);
            if (allow)
            {
                if (!checkPrivilege(username, "editProductInStore"))
                    SPDB.Add(new Tuple<int, String, String>(storeId, username, "editProductInStore"));
            }
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "editProductInStore"));
            privileges[username].editProductInStore(allow);
        }
        public void addStoreManager(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            StorePremissionsDB SPDB = new StorePremissionsDB(configuration.DB_MODE);
            if (allow)
            {
                if (!checkPrivilege(username, "addStoreManager"))
                    SPDB.Add(new Tuple<int, String, String>(storeId, username, "addStoreManager"));
            }
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "addStoreManager"));
            privileges[username].addStoreManager(allow);
        }
        public void viewPurchasesHistory(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            StorePremissionsDB SPDB = new StorePremissionsDB(configuration.DB_MODE);
            if (allow)
            {
                if (!checkPrivilege(username, "viewPurchasesHistory"))
                    SPDB.Add(new Tuple<int, String, String>(storeId, username, "viewPurchasesHistory"));
            }
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "viewPurchasesHistory"));
            privileges[username].viewPurchasesHistory(allow);
        }
        public void removeStoreManager(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            StorePremissionsDB SPDB = new StorePremissionsDB(configuration.DB_MODE);
            if (allow)
            {
                if (!checkPrivilege(username, "removeStoreManager"))
                    SPDB.Add(new Tuple<int, String, String>(storeId, username, "removeStoreManager"));
            }
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "removeStoreManager"));
            privileges[username].removeStoreManager(allow);
        }

        public void addManagerPermission(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            StorePremissionsDB SPDB = new StorePremissionsDB(configuration.DB_MODE);
            if (allow)
            {
                if (!checkPrivilege(username, "addManagerPermission"))
                    SPDB.Add(new Tuple<int, String, String>(storeId, username, "addManagerPermission"));
            }
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "addManagerPermission"));
            privileges[username].addManagerPermission(allow);
        }
        public void removeManagerPermission(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            StorePremissionsDB SPDB = new StorePremissionsDB(configuration.DB_MODE);
            if (allow)
            {
                if (!checkPrivilege(username, "removeManagerPermission"))
                    SPDB.Add(new Tuple<int, String, String>(storeId, username, "removeManagerPermission"));
            }
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "removeManagerPermission"));
            privileges[username].removeManagerPermission(allow);
        }

        public void addSaleToStore(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            StorePremissionsDB SPDB = new StorePremissionsDB(configuration.DB_MODE);
            if (allow)
            {
                if (!checkPrivilege(username, "addSaleToStore"))
                    SPDB.Add(new Tuple<int, String, String>(storeId, username, "addSaleToStore"));
            }
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "addSaleToStore"));
            privileges[username].addSaleToStore(allow);
        }

        public void removeSaleFromStore(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            StorePremissionsDB SPDB = new StorePremissionsDB(configuration.DB_MODE);
            if (allow)
            {
                if (!checkPrivilege(username, "removeSaleFromStore"))
                    SPDB.Add(new Tuple<int, String, String>(storeId, username, "removeSaleFromStore"));
            }
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "removeSaleFromStore"));
            privileges[username].removeSaleFromStore(allow);
        }

        public void editSale(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            StorePremissionsDB SPDB = new StorePremissionsDB(configuration.DB_MODE);
            if (allow)
            {
                if (!checkPrivilege(username, "editSale"))
                    SPDB.Add(new Tuple<int, String, String>(storeId, username, "editSale"));
            }
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "editSale"));
            privileges[username].editSale(allow);
        }

        public void addNewCoupon(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            StorePremissionsDB SPDB = new StorePremissionsDB(configuration.DB_MODE);
            if (allow)
            {
                if (!checkPrivilege(username, "addNewCoupon"))
                    SPDB.Add(new Tuple<int, String, String>(storeId, username, "addNewCoupon"));
            }
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "addNewCoupon"));
            privileges[username].addNewCoupon(allow);
        }

        public void addDiscount(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            StorePremissionsDB SPDB = new StorePremissionsDB(configuration.DB_MODE);
            if (allow)
            {
                if (!checkPrivilege(username, "addDiscount"))
                    SPDB.Add(new Tuple<int, String, String>(storeId, username, "addDiscount"));
            }
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "addDiscount"));
            privileges[username].addDiscount(allow);
        }

        public void removeDiscount(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            StorePremissionsDB SPDB = new StorePremissionsDB(configuration.DB_MODE);
            if (allow)
            {
                if (!checkPrivilege(username, "removeDiscount"))
                    SPDB.Add(new Tuple<int, String, String>(storeId, username, "removeDiscount"));
            }
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "removeDiscount"));
            privileges[username].removeDiscount(allow);
        }

        public void removeCoupon(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            StorePremissionsDB SPDB = new StorePremissionsDB(configuration.DB_MODE);
            if (allow)
            {
                if (!checkPrivilege(username, "removeCoupon"))
                    SPDB.Add(new Tuple<int, String, String>(storeId, username, "removeCoupon"));
            }
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "removeCoupon"));
            privileges[username].removeCoupon(allow);
        }

        public void changePolicy(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            StorePremissionsDB SPDB = new StorePremissionsDB(configuration.DB_MODE);
            if (allow)
            {
                if (!checkPrivilege(username, "changePolicy"))
                    SPDB.Add(new Tuple<int, String, String>(storeId, username, "changePolicy"));
            }
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "changePolicy"));
            privileges[username].changePolicy(allow);
        }

        public Boolean checkPrivilege(string username, string privilege)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            return privileges[username].checkPrivilege(privilege);
        }

    }
}
