﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServices.Domain;

namespace wsep182.Domain
{
    public abstract class UserState
    {
        public virtual int login(String username, String password)
        {
            return -4;
        }

        public virtual LinkedList<Purchase> viewUserHistory(User userToGetHistory)
        {
            return null;
        }

        public abstract Boolean isLogedIn();

        public abstract LinkedList<Purchase> viewStoreHistory(Store store, User session);

        public virtual UserState register(String username, String password)
        {
            return null;
        }

        public virtual int createStore(String storeName, User session)
        {
            Store newStore = StoreManagement.getInstance().addStore(storeName, session);
            StoreRole sR= new StoreOwner(session, newStore, session.getUserName());
            StoreManagement.getInstance().addStoreRole(sR, newStore.getStoreId(), session.getUserName());
            NotificationPublisher.getInstance().signToCategory(sR, NotificationPublisher.NotificationCategories.Purchase);
            NotificationPublisher.getInstance().signToCategory(sR, NotificationPublisher.NotificationCategories.RaffleSale);
            NotificationPublisher.getInstance().signToCategory(sR, NotificationPublisher.NotificationCategories.Store);
            return newStore.getStoreId();
        }


        public virtual int removeUser(User session, string userDeleted)
        {
            return -1;
        }

        public virtual int setAmountPolicyOnProduct(string productName, int minAmount, int maxAmount)
        {
            return -1;
        }

        public virtual int setNoDiscountPolicyOnProduct(string productName)
        {
            return -1;
        }

        public virtual int setNoCouponsPolicyOnProduct(string productName)
        {
            return -1;
        }

        public virtual int removeAmountPolicyOnProduct(string productName)
        {
            return -1;
        }

        public virtual int removeNoDiscountPolicyOnProduct(string productName)
        {
            return -1;
        }

        public virtual int removeNoCouponsPolicyOnProduct(string productName)
        {
            return -1;
        }

        public virtual Premissions getPremissions(User manager, Store s)
        {
            StoreRole sR = StoreRole.getStoreRole(s, manager);
            if (sR != null)
                return sR.getPremissions(manager, s);
            return null;
        }
    }
}
