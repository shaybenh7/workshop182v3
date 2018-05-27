﻿using System;
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
                            privilegesOfaStore.Add(storeId, new StorePremissions());
                        privilegesOfaStore[storeId].addProductInStore(username, true);
                        
                        break;

                    case "editProductInStore":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions());
                        privilegesOfaStore[storeId].editProductInStore(username, true);
                        break;

                    case "removeProductFromStore":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions());
                         privilegesOfaStore[storeId].removeProductFromStore(username, true);

                        break;

                    case "addStoreManager":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions());
                        privilegesOfaStore[storeId].addStoreManager(username, true);
                        break;

                    case "removeStoreManager":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions());
                        privilegesOfaStore[storeId].removeStoreManager(username, true);
                        break;
                    case "addManagerPermission":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions());
                        privilegesOfaStore[storeId].addManagerPermission(username, true);
                        break;
                    case "removeManagerPermission":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions());
                        privilegesOfaStore[storeId].removeManagerPermission(username, true);
                        break;
                    case "viewPurchasesHistory":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions());
                        privilegesOfaStore[storeId].viewPurchasesHistory(username, true);
                        break;
                    case "removeSaleFromStore":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions());
                        privilegesOfaStore[storeId].removeSaleFromStore(username, true);
                        break;
                    case "editSale":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions());
                        privilegesOfaStore[storeId].editSale(username, true);
                        break;
                    case "addSaleToStore":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions());
                        privilegesOfaStore[storeId].addSaleToStore(username, true);
                        break;
                    case "addDiscount":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions());
                        privilegesOfaStore[storeId].addDiscount(username, true);
                        break;
                    case "addNewCoupon":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions());
                        privilegesOfaStore[storeId].addNewCoupon(username, true);
                        break;
                    case "removeDiscount":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions());
                        privilegesOfaStore[storeId].removeDiscount(username, true);
                        break;
                    case "removeCoupon":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions());
                        privilegesOfaStore[storeId].removeCoupon(username, true);
                        break;
                    case "changePolicy":
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions());
                        privilegesOfaStore[storeId].changePolicy(username, true);
                        break;
                    
                    default:
                        if (!privilegesOfaStore.ContainsKey(storeId))
                            privilegesOfaStore.Add(storeId, new StorePremissions());
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

        public Premissions getAllPremissions(int storeId, string username)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            return privilegesOfaStore[storeId].getPrivileges(username);
        }

        public static void restartInstance()
        {
            instance = new StorePremissionsArchive();
        }


        public void addProductInStore(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            if(allow)
                SPDB.Add(new Tuple<int,String,String>(storeId, username, "addProductInStore"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "addProductInStore"));
            privilegesOfaStore[storeId].addProductInStore(username, allow);
        }

        public void editProductInStore(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "editProductInStore"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "editProductInStore"));
            privilegesOfaStore[storeId].editProductInStore(username, allow);
        }

        public void removeProductFromStore(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "removeProductFromStore"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "removeProductFromStore"));
            privilegesOfaStore[storeId].removeProductFromStore(username, allow);
        }

        public void addStoreManager(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "addStoreManager"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "addStoreManager"));
            privilegesOfaStore[storeId].addStoreManager(username, allow);
        }

        public void addDiscount(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "addDiscount"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "addDiscount"));
            privilegesOfaStore[storeId].addDiscount(username, allow);
        }

        public void addNewCoupon(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "addNewCoupon"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "addNewCoupon"));
            privilegesOfaStore[storeId].addNewCoupon(username, allow);
        }

        public void removeDiscount(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "removeDiscount"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "removeDiscount"));
            privilegesOfaStore[storeId].removeDiscount(username, allow);
        }

        public void removeCoupon(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "removeCoupon"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "removeCoupon"));
            privilegesOfaStore[storeId].removeCoupon(username, allow);
        }

        public void removeStoreManager(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "removeStoreManager"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "removeStoreManager"));
            privilegesOfaStore[storeId].removeStoreManager(username, allow);
        }

        public void addManagerPermission(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "addManagerPermission"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "addManagerPermission"));
            privilegesOfaStore[storeId].addManagerPermission(username, allow);
        }

        public void removeManagerPermission(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "removeManagerPermission"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "removeManagerPermission"));
            privilegesOfaStore[storeId].removeManagerPermission(username, allow);
        }

        public void addSaleToStore(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "addSaleToStore"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "addSaleToStore"));
            privilegesOfaStore[storeId].addSaleToStore(username, allow);
        }

        public void removeSaleFromStore(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "removeSaleFromStore"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "removeSaleFromStore"));
            privilegesOfaStore[storeId].removeSaleFromStore(username, allow);
        }

        public void editSale(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "editSale"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "editSale"));
            privilegesOfaStore[storeId].editSale(username, allow);
        }

        public void viewPurchasesHistory(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            if(allow)
                SPDB.Add(new Tuple<int, String, String>(storeId, username, "viewPurchasesHistory"));
            else
                SPDB.Remove(new Tuple<int, String, String>(storeId, username, "viewPurchasesHistory"));
            privilegesOfaStore[storeId].viewPurchasesHistory(username, allow);
        }

        public Boolean checkPrivilege(int storeId, string username, string privilege)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            return privilegesOfaStore[storeId].checkPrivilege(username, privilege);
        }
        

        
        //public Boolean addNewCoupon(string username, string privilege)
        //{
        //   return privilegesOfaUser[username].checkPrivilege(privilege);
        //}
        
    }
}
