using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class Admin : UserState
    {
        /*
         * return :
         *          0 if user removed successfuly
         *          -2 user to remove is not exist
         *          -3 user to remove allready removed
         *          -4 user cannot remove himself
         *          -5 user who has raffle sale can not be removed
         *          -6 user who is owner or creator of store can not be removed
         */


        public override int removeUser(User session, string userDeleted)
        {
            User userToDelete = UserArchive.getInstance().getUser(userDeleted);
            if (userToDelete == null )
                return -2;
            if (!userToDelete.getIsActive())
                return -3;
            if (userToDelete.getUserName() == session.getUserName())
                return -4;
            if (RaffleSalesArchive.getInstance().getAllRaffleSalesByUserName(userDeleted).Count > 0)
                return -5;
            LinkedList<StoreRole> roles = storeArchive.getInstance().getAllStoreRolesOfAUser(userDeleted);
            if (checkLoneOwnerOrCreator(roles))
                return -6;
            removeAllRolesOfAUser(roles);
            return UserArchive.getInstance().removeUser(userDeleted);
        }

        public override int setAmountPolicyOnProduct(string productName, int minAmount, int maxAmount)
        {
            if (productName == null || minAmount<0 || maxAmount<0)
                return -1;
            return PurchasePolicyArchive.getInstance().setAmountPolicyOnProduct(productName, minAmount, maxAmount);
        }

        public override int setNoDiscountPolicyOnProduct(string productName)
        {
            if (productName == null)
                return -1;
            return PurchasePolicyArchive.getInstance().setNoDiscountPolicyOnProduct(productName);
        }

        public override int setNoCouponsPolicyOnProduct(string productName)
        {
            if (productName == null)
                return -1;
            return PurchasePolicyArchive.getInstance().setNoCouponsPolicyOnProduct(productName);
        }

        public override int removeAmountPolicyOnProduct(string productName)
        {
            if (productName == null)
                return -1;
            return PurchasePolicyArchive.getInstance().removeAmountPolicyOnProduct(productName);
        }

        public override int removeNoDiscountPolicyOnProduct(string productName)
        {
            if (productName == null)
                return -1;
            return PurchasePolicyArchive.getInstance().removeNoDiscountPolicyOnProduct(productName);
        }

        public override int removeNoCouponsPolicyOnProduct(string productName)
        {
            if (productName == null)
                return -1;
            return PurchasePolicyArchive.getInstance().removeNoCouponsPolicyOnProduct(productName);
        }

        private Boolean checkLoneOwnerOrCreator(LinkedList<StoreRole> roles)
        {
            foreach (StoreRole sr in roles)
            {
                if (sr is StoreOwner)
                {
                    //check if he's a lone owner
                    if (sr.getStore().getOwners().Count == 1)
                        return true;
                    //owner which is a creater
                    if (sr.getStore().getStoreCreator().getUserName() == sr.getUser().getUserName())
                        return true;
                }
            }
            return false;
        }

        private Boolean removeAllRolesOfAUser(LinkedList<StoreRole> roles)
        {
            Boolean res = true;
            foreach (StoreRole sr in roles)
            {
                if (sr is StoreOwner)
                {
                    if (sr.getStore().getOwners().Count > 1)
                        res = res && storeArchive.getInstance().removeStoreRole(sr.getStore().getStoreId(), sr.getUser().getUserName());
                    else throw new Exception("something went seriously wrong"); // in the interval between the call to the safety check to now, something occured
                }
                else
                    res = res && storeArchive.getInstance().removeStoreRole(sr.getStore().getStoreId(), sr.getUser().getUserName());
            }
            return res;
        }


        public override Boolean isLogedIn()
        {
            return true;
        }

        public override LinkedList<Purchase> viewStoreHistory(Store store, User session)
        {
            return BuyHistoryArchive.getInstance().viewHistoryByStoreId(store.getStoreId());
        }

        public override LinkedList<Purchase> viewUserHistory(User userToGetHistory)
        {
            return BuyHistoryArchive.getInstance().viewHistoryByUserName(userToGetHistory.getUserName());
        }

        public override Premissions getPremissions(User manager, Store s)
        {
            return StorePremissionsArchive.getInstance().getAllPremissions(s.getStoreId(), manager.getUserName());
        }
    }
}
