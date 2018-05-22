using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class StoreManager : StoreRole
    {
        public StorePremissionsArchive premissions;
        public StoreManager(User u, Store s) : base(u, s)
        {
            premissions = StorePremissionsArchive.getInstance();
            type = "Manager";
        }

        public override int addProductInStore(User session, Store s, String productName, double price, int amount, string category)
        {
            if (premissions.checkPrivilege(s.getStoreId(), session.getUserName(),"addProductInStore"))
                return base.addProductInStore(session, s, productName, price, amount, category);
            return -4; //-4 if don't have permission
        }
		

        public override Boolean addDiscount(User session, ProductInStore p, int percentage, String dueDate)
        {
            if (premissions.checkPrivilege(p.getStore().getStoreId(), session.getUserName(), "addDiscount"))
                return base.addDiscount(session, p, percentage, dueDate);
            return false;
        }

        public override int addDiscounts(User session, int storeId,List<int> productInStores, int type,
           int percentage, List<string> categorysOrProductsName, string dueDate, string restrictions)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "addDiscount"))
                return base.addDiscounts(session, storeId,productInStores, type, percentage, categorysOrProductsName, dueDate, restrictions);
            return -1;
        }
        public override Boolean removeDiscount(User session, ProductInStore p)
        {
            if (premissions.checkPrivilege(p.getStore().getStoreId(), session.getUserName(), "removeDiscount"))
                return base.removeDiscount(session, p);
            return false;
        }

        public override Boolean addNewCoupon(User session, String couponId, ProductInStore p, int percentage, String dueDate)
        {
            if (premissions.checkPrivilege(p.getStore().getStoreId(), session.getUserName(), "addNewCoupon"))
                return base.addNewCoupon(session, couponId, p, percentage, dueDate);
            return false;
        }

        public override Boolean addNewCoupon(User session,int storeId, String couponId, int productInStoreId, int type, string categoryOrProductName,
                                                int percentage, String dueDate, string restrictions)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "addNewCoupon"))
                return base.addNewCoupon(session, storeId, couponId, productInStoreId, type, categoryOrProductName, percentage, dueDate, restrictions);
            return false;
        }

        public override int addNewCoupons(User session,int storeId, String couponId, int type, List<int> pisId, List<string> catOrProductsNames
                                            , int percentage, string dueDate, string restrictions)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "addNewCoupon"))
                return base.addNewCoupons(session, storeId, couponId, type, pisId, catOrProductsNames, percentage, dueDate, restrictions);
            return -1;
        }

        public override Boolean removeCoupon(User session, Store s, String couponId)
        {
            if (premissions.checkPrivilege(s.getStoreId(), session.getUserName(), "removeCoupon"))
                return base.removeCoupon(session, s, couponId);
            return false;
        }

        public override int editProductInStore(User session, ProductInStore p, int quantity, double price)
        {
            if (session != null && p != null && price >= 0 && quantity >= 0)
                if (premissions.checkPrivilege(p.getStore().getStoreId(), session.getUserName(), "editProductInStore"))
                    return base.editProductInStore(session, p, quantity, price);
            return -4;//-4 if don't have permission
        }

        public override Boolean addStoreOwner(User session, Store s, String newOwner)
        {
            return false;
        }

        public override int removeStoreOwner(User session, Store s, String ownerToDelete)
        {
            return -4;// -4 if don't have permission
        }

        public override int removeProductFromStore(User session, Store s, ProductInStore p)
        {
            if (premissions.checkPrivilege(s.getStoreId(), session.getUserName(),"removeProductFromStore"))
                return base.removeProductFromStore(session, s, p);
            return -4;// -4 if don't have permission
        }

        public override int addStoreManager(User session, Store s, String newManager)
        {
            if (premissions.checkPrivilege(s.getStoreId(), session.getUserName(), "addStoreManager"))
                return base.addStoreManager(session, s, newManager);
            return -4;// -4 if don't have permission
        }
        public override int removeStoreManager(User session, Store s, String oldManager)
        {
            if (premissions.checkPrivilege(s.getStoreId(), session.getUserName(), "removeStoreManager"))
                return base.removeStoreManager(session, s, oldManager);
            return -4;// -4 if don't have permission

        }

        public override Premissions getPremissions(User manager, Store s)
        {
            return premissions.getAllPremissions(s.getStoreId(), manager.getUserName());
        }

        public override int addManagerPermission(User session, String permission, Store s, String manager)
        {
            if (premissions.checkPrivilege(s.getStoreId(), session.getUserName(), "addManagerPermission"))
                return base.addManagerPermission(session, permission, s, manager);
            return -4;//-4 if don't have permission
        }

        public override int removeManagerPermission(User session, String permission, Store s, String manager)
        {
            if (premissions.checkPrivilege(s.getStoreId(), session.getUserName(), "removeManagerPermission"))
                return base.removeManagerPermission(session, permission, s, manager);
            return -4;//-4 if don't have permission
        }

        public override int addSaleToStore(User session, Store s, int productInStoreId, int typeOfSale, int amount, String dueDate)
        {
            if (premissions.checkPrivilege(s.getStoreId(), session.getUserName(), "addSaleToStore"))
                return base.addSaleToStore(session, s, productInStoreId, typeOfSale, amount, dueDate);
            return -4;//-4 if don't have permission
        }

        public override int removeSaleFromStore(User session, Store s, int saleId)
        {
            if (premissions.checkPrivilege(s.getStoreId(), session.getUserName(), "removeSaleFromStore"))
                return base.removeSaleFromStore(session, s, saleId);
            return -4;//-4 if don't have permission
        }

        public override int editSale(User session, Store s, int saleId, int amount, String dueDate)
        {            
            if (premissions.checkPrivilege(s.getStoreId(), session.getUserName(), "editSale"))
                return base.editSale(session, s, saleId, amount, dueDate);
            return -4;//-4 if don't have permission
        }

        public override int setAmountPolicyOnStore(User session, int storeId, int minAmount, int maxAmount)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.setAmountPolicyOnStore(session, storeId, minAmount, maxAmount);
            return -4;//-4 if don't have permission
        }

        public override int setAmountPolicyOnProductInStore(User session, int storeId, int productInStoreId, int minAmount, int maxAmount)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.setAmountPolicyOnProductInStore(session, storeId, productInStoreId, minAmount, maxAmount);
            return -4;//-4 if don't have permission
        }

        public override int setAmountPolicyOnCountry(User session, int storeId, string country, int minAmount, int maxAmount)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.setAmountPolicyOnCountry(session, storeId, country, minAmount, maxAmount);
            return -4;//-4 if don't have permission
        }



        public override int setAmountPolicyOnCategory(User session, int storeId, String category, int minAmount, int maxAmount)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.setAmountPolicyOnCategory(session, storeId, category,minAmount, maxAmount);
            return -4;//-4 if don't have permission
        }

        public override int setNoDiscountPolicyOnStore(User session, int storeId)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.setNoDiscountPolicyOnStore(session, storeId);
            return -4;//-4 if don't have permission
        }

        public override int setNoDiscountPolicyOnCategoty(User session, int storeId, String category)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.setNoDiscountPolicyOnCategoty(session, storeId,category);
            return -4;//-4 if don't have permission
        }

        public override int setNoDiscountPolicyOnCountry(User session, int storeId, string country)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.setNoDiscountPolicyOnCountry(session, storeId, country);
            return -4;//-4 if don't have permission
        }

        public override int setNoCouponsPolicyOnStore(User session, int storeId)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.setNoCouponsPolicyOnStore(session, storeId);
            return -4;//-4 if don't have permission
        }

        public override int setNoCouponPolicyOnCategoty(User session, int storeId, String category)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.setNoCouponPolicyOnCategoty(session, storeId,category);
            return -4;//-4 if don't have permission
        }

        public override int setNoCouponPolicyOnProductInStore(User session, int storeId, int productInStoreId)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.setNoCouponPolicyOnProductInStore(session, storeId, productInStoreId);
            return -4;//-4 if don't have permission
        }

        public override int setNoDiscountPolicyOnProductInStore(User session, int storeId, int productInStoreId)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.setNoDiscountPolicyOnProductInStore(session, storeId, productInStoreId);
            return -4;//-4 if don't have permission
        }

        public override int setNoCouponPolicyOnCountry(User session, int storeId, string country)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.setNoCouponPolicyOnCountry(session, storeId, country);
            return -4;//-4 if don't have permission
        }

        public override int removeAmountPolicyOnStore(User session, int storeId)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.removeAmountPolicyOnStore(session, storeId);
            return -4;//-4 if don't have permission
        }

        public override int removeAmountPolicyOnCategory(User session, int storeId, string category)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.removeAmountPolicyOnCategory(session, storeId, category);
            return -4;//-4 if don't have permission
        }

        public override int removeAmountPolicyOnProductInStore(User session, int storeId, int productInStoreId)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.removeAmountPolicyOnProductInStore(session, storeId, productInStoreId);
            return -4;//-4 if don't have permission
        }

        public override int removeAmountPolicyOnCountry(User session, int storeId, string country)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.removeAmountPolicyOnCountry(session, storeId, country);
            return -4;//-4 if don't have permission
        }

        public override int removeNoDiscountPolicyOnStore(User session, int storeId)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.removeNoDiscountPolicyOnStore(session, storeId);
            return -4;//-4 if don't have permission
        }

        public override int removeNoDiscountPolicyOnCategoty(User session, int storeId, String category)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.removeNoDiscountPolicyOnCategoty(session, storeId,category);
            return -4;//-4 if don't have permission
        }

        public override int removeNoDiscountPolicyOnProductInStore(User session, int storeId, int productInStoreId)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.removeNoDiscountPolicyOnProductInStore(session, storeId, productInStoreId);
            return -4;//-4 if don't have permission
        }

        public override int removeNoDiscountPolicyOnCountry(User session, int storeId,string country)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.removeNoDiscountPolicyOnCountry(session, storeId, country);
            return -4;//-4 if don't have permission
        }

        public override int removeNoCouponsPolicyOnStore(User session, int storeId)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.removeNoCouponsPolicyOnStore(session, storeId);
            return -4;//-4 if don't have permission
        }


        public override int removeNoCouponPolicyOnProductInStore(User session, int storeId, int productInStoreId)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.removeNoCouponPolicyOnProductInStore(session, storeId, productInStoreId);
            return -4;//-4 if don't have permission
        }

        public override int removeNoCouponPolicyOnCountry(User session, int storeId, string country)
        {
            if (premissions.checkPrivilege(storeId, session.getUserName(), "changePolicy"))
                return base.removeNoCouponPolicyOnCountry(session, storeId, country);
            return -4;//-4 if don't have permission
        }

        public override LinkedList<Purchase> viewPurchasesHistory(User session, Store s)
        {
            if (premissions.checkPrivilege(s.getStoreId(), session.getUserName(), "viewPurchasesHistory"))
                return base.viewPurchasesHistory(session, s);
            return null;
        }


    }
}
