using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public abstract class StoreRole
    {
        public User user;
        public Store store;
        public string type;

        public StoreRole(User u, Store s) {
            user = u;
            store = s;
        }

        public static StoreRole getStoreRole(Store store, User user)
        {
            if (store == null || user == null)
                return null;
            return storeArchive.getInstance().getStoreRole(store, user);
        }


        public virtual int addProductInStore(User session, Store s, String productName, double price, int amount,string category)
        {
            if (productName == null ||  productName == ""
                || productName[productName.Length - 1] == ' ')
                return -3;//-3 if illegal product name
            if (session == null)
                return -1;// -1 if user Not Login
            if (s == null)
                return -6;// -6 if illegal store id
            if (amount <= 0)
                return -5;// -5 if illegal amount
            if (price <= 0)
                return -7;// -7 if illegal price
            //if(check if session is owner or manager with the right permission)
            Product p2 = ProductArchive.getInstance().getProductByName(productName);
            if (p2 == null)
            {
                p2 = Product.addProduct(productName);
            }
            ProductArchive pa = ProductArchive.getInstance();
            ProductInStore pis = pa.addProductInStore(p2, s, amount, price,category);
            if (pis != null)
            {
                return pis.getProductInStoreId();
            }
            else
                return -8;
           
        }
        /*
       * return:
       *           0 < on sucess
       *          -1 if user Not Login
       *          -2 if Store Name already exist
       *          -3 if illegal product name
       *          -4 if don't have premition
       *          -5 if illegal amount
       *          -6 if illegal store id
       *          -7 if illegal price
       *          -8 if illegal product in store Id
       */
        public virtual int editProductInStore(User session, ProductInStore p, int quantity, double price)
        {
            if (session == null)
                return -1;// -1 if user Not Login
            if (p == null)
                return -8;// -8 if illegal product in store Id
            if (price < 0)
                return -7;// -7 if illegal price
            if (quantity < 0)
                return -5;// -7 if illegal price

            p.Price = price;
            p.Quantity = quantity;
            if (ProductArchive.getInstance().updateProductInStore(p))
                return 0;// OK
            return -9;// -9 database eror


        }
        public virtual int removeProductFromStore(User session, Store s, ProductInStore p)
        {
            if (session == null)
                return -1;//-1 if user Not Login
            if (s == null)
                return -6;//-6 if illegal store id
            if (p == null)
                return -8;//-8 if illegal product in store Id
            if (ProductArchive.getInstance().removeProductInStore(p.getProductInStoreId(), s.getStoreId()))
                return 0;
            return -9;

        }
     
        public virtual int addStoreManager(User session, Store s, String newManagerUserName)
        {
            User newManager = UserArchive.getInstance().getUser(newManagerUserName);
            if (session == null)
                return -1;//-1 if user Not Login
            if (s == null)
                return -3;//-3 if illegal store id
            if (newManager == null)
                return -2;//-2 if new manager name not exist

            StoreRole sr = storeArchive.getInstance().getStoreRole(s, newManager);
            if (sr != null && (sr is StoreOwner || sr is StoreManager))
                return -6;//-6 already owner or manneger
            if (sr != null && (sr is Customer))
                storeArchive.getInstance().removeStoreRole(s.getStoreId(), newManager.getUserName());
            StoreRole m = new StoreManager(newManager, s);
            if (storeArchive.getInstance().addStoreRole(m, s.getStoreId(), newManager.getUserName()))
                return 0;
            return -5;//-5 database error
        }

        public virtual int removeStoreManager(User session, Store s, String oldManager)
        {
            User session2 = UserArchive.getInstance().getUser(oldManager);
            if (session == null  )
                return -1;//-1 if user Not Login
            if (s == null)
                return -3;//-3 if illegal store id
            if (oldManager == null)
                return -6;// -6 old manager name doesn't exsist
            StoreRole sr = storeArchive.getInstance().getStoreRole(s, session2);
            if (sr != null && !(sr is StoreManager))
                return -7;
            if ( storeArchive.getInstance().removeStoreRole(s.getStoreId(), oldManager))
                return 0;//OK
            return -5;//-5 database eror
        }

        public virtual Boolean addStoreOwner(User session, Store s, String newOwnerUserName)
        {
            User newOwner = UserArchive.getInstance().getUser(newOwnerUserName);
            if (newOwner == null || s == null || session == null)
            {
                return false;
            }
            StoreRole sr = storeArchive.getInstance().getStoreRole(s, newOwner);
            if (sr != null && (sr is StoreOwner))
                return false;
            if (sr != null && (sr is StoreManager))
                removeStoreManager(session, s, newOwnerUserName);
            if (sr != null && (sr is Customer))
                storeArchive.getInstance().removeStoreRole(s.getStoreId(), newOwner.getUserName());
            StoreRole owner = new StoreOwner(newOwner, s);
            return storeArchive.getInstance().addStoreRole(owner, s.getStoreId(), newOwner.getUserName());
        }

        public virtual int removeStoreOwner(User session, Store s, String ownerToDelete)
        {
            User oldOwner = UserArchive.getInstance().getUser(ownerToDelete);
            StoreRole sR2 = StoreRole.getStoreRole(s, oldOwner);
            if (ownerToDelete == user.getUserName())
                return -10;//-10 can't remove himself
            if (!(sR2 is StoreOwner))
                return -11;//-11 not a owner
            if (s.getStoreCreator().getUserName().Equals(ownerToDelete))
                return -12;//-12 if dealet creator
             if(storeArchive.getInstance().removeStoreRole(s.getStoreId(), ownerToDelete))
                return 0;
            return -9;//-9 database eror
        }
        public virtual int addManagerPermission(User session, String permission, Store s, String managerUserName)
        {
    
            User manager = UserArchive.getInstance().getUser(managerUserName);
            if (session == null)
                return -1;//-1 if user Not Login
            if (permission == null)
                return -7;//-7 no such premition
            if (manager == null)
                return -6;//-6 manager name doesn't exsist
            if (s == null)
                return -3;// -3 if illegal store id
            StoreRole sR = storeArchive.getInstance().getStoreRole(s, manager);
            if (!(sR is StoreManager))
                return -8;
            if( correlate(manager, s, permission, sR, true))
                return 0;
            return -7;//-7 no such premition

        }
        public virtual int removeManagerPermission(User session, String permission, Store s, String managerUsername)
        {
            if (managerUsername == null)
                return -6; //manager name doesn't exists
            User manager = UserArchive.getInstance().getUser(managerUsername);

            if (permission == null)
                return -7; //No permissions
            if (manager == null)
                return -6; //manager name doesnt exists
            if (session == null)
                return -1; //user not logged in
            if (s == null)
                return -3; //Illegal store id
            StoreRole sR = storeArchive.getInstance().getStoreRole(s, manager);
            if (correlate(manager, s, permission, sR, false))
                return 0;
            return -7; //No permissions
        }
      
        public virtual int addSaleToStore(User session, Store s, int productInStoreId, int typeOfSale, int amount, String dueDate)
        {
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(productInStoreId);
            if (session == null )
                return -1;// -1 if user Not Login
            if (s == null)
                return -6; //-6 if illegal store id
            if (pis == null)
                return -8;//-8 if illegal product in store Id
            if (typeOfSale > 3 || typeOfSale < 1)
                return -11;// -11 illegal type of sale not 
            if (pis.getAmount() < amount)
                return -5;//-5 if illegal amount
            if (amount < 0)
                return -12;// -12 if illegal amount
            try {
                DateTime.Parse(dueDate);
            }
            catch(Exception)
            {
                return -10;
            }
            if (dueDate == null || DateTime.Compare(DateTime.Parse(dueDate),DateTime.Now) < 0)
                return -10;//-10 due date not good

            if (pis.getStore().getStoreId() != s.getStoreId())
                return -13;//-13 product not in this store
            if (typeOfSale == 2)
            {
                //will be implemented next version
                return -11;// -11 illegal type of sale not 
            }
            Sale sale = SalesArchive.getInstance().addSale(productInStoreId, typeOfSale, amount, dueDate);
            
            return (sale == null) ? -9 : sale.SaleId;
        }

        public virtual int removeSaleFromStore(User session, Store s, int saleId)
        {
            if (session == null  )
                return -1;// -1 if user Not Login
            if (s == null)
                return -6; //-6 if illegal store id
            if (SalesArchive.getInstance().getSale(saleId) == null)
                return -8;// -8 if illegal sale id
            if(SalesArchive.getInstance().removeSale(saleId))
                return 0;
            return -9; //database error
        }

        public virtual int editSale(User session, Store s, int saleId, int amount, String dueDate)
        {
            if ( SalesArchive.getInstance().getSale(saleId) == null)
                return -8;// -8 if illegal sale id
            if (session == null)
                return -1;// -1 if user Not Login
            if (s == null)
                return -6; //-6 if illegal store id
            if (ProductArchive.getInstance().getProductInStore(SalesArchive.getInstance().getSale(saleId).ProductInStoreId).getAmount() < amount)
                return -5;// -5 if illegal amount bigger then amount in stock
            if (amount < 0)
                return -12;// -12 if illegal amount
            try
            {
                DateTime.Parse(dueDate);
            }
            catch (Exception)
            {
                return -10;
            }
            if (dueDate == null || DateTime.Compare(DateTime.Parse(dueDate), DateTime.Now) < 0)
                return -10;//-10 due date not good
            if(SalesArchive.getInstance().editSale(saleId, amount, dueDate))
                return 0;
            return -9;//-9 database eror
        }

        
        public virtual Boolean addDiscount(User session, ProductInStore p, int percentage, String dueDate)
        {
            if (session == null || p == null || percentage < 0 || percentage >= 100 || dueDate == null)
                return false;
            return DiscountsArchive.getInstance().addNewDiscount(p.getProductInStoreId(),1,"", percentage, dueDate,"");
        }

        public virtual int addDiscounts(User session,int storeId, List<int> productInStores, int type,
           int percentage, List<string> categorysOrProductsName, string dueDate, string restrictions)
        {
            if (session == null || percentage < 0 || percentage >= 100 || dueDate == null)
                return -1;
            return DiscountsArchive.getInstance().addNewDiscounts(type, productInStores, categorysOrProductsName , percentage , dueDate, restrictions);
        }

        public virtual Boolean addNewCoupon(User session, String couponId, ProductInStore p, int percentage, String dueDate)
        {
            if (session == null || couponId == null || p == null || percentage < 0 || dueDate == null || percentage<=0)
                return false;
            return CouponsArchive.getInstance().addNewCoupon(couponId, p.getProductInStoreId(), percentage, dueDate);
        }

        public virtual Boolean addNewCoupon(User session, int storeId, String couponId, int productInStoreId, int type, string categoryOrProductName,
         int percentage, String dueDate, string restrictions)
        {
            if (session == null || couponId == null || percentage < 0 || dueDate == null || percentage <= 0)
                return false;
            return CouponsArchive.getInstance().addNewCoupon(couponId, productInStoreId, type, categoryOrProductName, percentage, dueDate, restrictions);
        }

        public virtual int addNewCoupons(User session, int storeId, String couponId, int type, List<int> pisId, List<string> catOrProductsNames
            , int percentage, string dueDate, string restrictions)
        {
            if (session == null || couponId == null || percentage <= 0 || dueDate == null || percentage <= 0 || pisId==null || restrictions==null)
                return -1;
            if (DateTime.Compare(DateTime.Parse(dueDate), DateTime.Now) < 0)
                return -1;
            return CouponsArchive.getInstance().addNewCoupons(couponId, type, pisId, catOrProductsNames, percentage, dueDate, restrictions);
        }

        public virtual Boolean removeDiscount(User session, ProductInStore p)
        {
            if (p == null || session == null)
                return false;
            return DiscountsArchive.getInstance().removeDiscount(p.getProductInStoreId());
        }

        public virtual Boolean removeCoupon(User session, Store s, String couponId)
        {
            if (session == null || couponId==null)
                return false;
            return CouponsArchive.getInstance().removeCoupon(couponId);
        }


        public virtual LinkedList<Purchase> viewPurchasesHistory(User session,Store s)
        {
            if (session == null || s == null)
                return null;
            return BuyHistoryArchive.getInstance().viewHistoryByStoreId(s.getStoreId());
        }

        public virtual int setAmountPolicyOnStore(User session,int storeId, int minAmount, int maxAmount)
        {
            if (storeArchive.getInstance().getStore(storeId) == null || minAmount < 0 || maxAmount < 0)
                return -1;
            return PurchasePolicyArchive.getInstance().setAmountPolicyOnStore(storeId, minAmount, maxAmount);
        }

        public virtual int setAmountPolicyOnCategory(User session, int storeId, String category, int minAmount, int maxAmount)
        {
            if (storeArchive.getInstance().getStore(storeId) == null || minAmount < 0 || maxAmount < 0)
                return -1;
            return PurchasePolicyArchive.getInstance().setAmountPolicyOnCategory(storeId, category, minAmount, maxAmount);
        }

        public virtual int setAmountPolicyOnProductInStore(User session, int storeId, int productInStoreId, int minAmount, int maxAmount)
        {
            if (storeArchive.getInstance().getStore(storeId) == null || minAmount < 0 || maxAmount < 0)
                return -1;
            return PurchasePolicyArchive.getInstance().setAmountPolicyOnProductInStore(productInStoreId, minAmount, maxAmount);
        }

        public virtual int setAmountPolicyOnCountry(User session, int storeId, string country, int minAmount, int maxAmount)
        {
            if (storeArchive.getInstance().getStore(storeId) == null || minAmount < 0 || maxAmount < 0)
                return -1;
            return PurchasePolicyArchive.getInstance().setAmountPolicyOnCountry(storeId, country, minAmount, maxAmount);
        }

        public virtual int setNoDiscountPolicyOnStore(User session, int storeId)
        {
            if (storeArchive.getInstance().getStore(storeId) == null)
                return -1;
            return PurchasePolicyArchive.getInstance().setNoDiscountPolicyOnStore(storeId);
        }

        public virtual int setNoDiscountPolicyOnCategoty(User session, int storeId,String category)
        {
            if (storeArchive.getInstance().getStore(storeId) == null)
                return -1;
            return PurchasePolicyArchive.getInstance().setNoDiscountPolicyOnCategoty(storeId, category);
        }

        public virtual int setNoDiscountPolicyOnCountry(User session, int storeId, String country)
        {
            if (storeArchive.getInstance().getStore(storeId) == null)
                return -1;
            return PurchasePolicyArchive.getInstance().setNoDiscountPolicyOnCountry(storeId, country);
        }

        public virtual int setNoCouponsPolicyOnStore(User session, int storeId)
        {
            if (storeArchive.getInstance().getStore(storeId) == null)
                return -1;
            return PurchasePolicyArchive.getInstance().setNoCouponsPolicyOnStore(storeId);
        }

        public virtual int setNoCouponPolicyOnCategoty(User session, int storeId, String category)
        {
            if (storeArchive.getInstance().getStore(storeId) == null)
                return -1;
            return PurchasePolicyArchive.getInstance().setNoCouponPolicyOnCategoty(storeId, category);
        }

        public virtual int setNoCouponPolicyOnProductInStore(User session, int storeId, int productInStoreId)
        {
            if (storeArchive.getInstance().getStore(storeId) == null)
                return -1;
            return PurchasePolicyArchive.getInstance().setNoCouponPolicyOnProductInStore(productInStoreId);
        }

        public virtual int setNoDiscountPolicyOnProductInStore(User session, int storeId, int productInStoreId)
        {
            if (storeArchive.getInstance().getStore(storeId) == null)
                return -1;
            return PurchasePolicyArchive.getInstance().setNoDiscountPolicyOnProductInStore(productInStoreId);
        }

        public virtual int setNoCouponPolicyOnCountry(User session, int storeId, string country)
        {
            if (storeArchive.getInstance().getStore(storeId) == null)
                return -1;
            return PurchasePolicyArchive.getInstance().setNoCouponPolicyOnCountry(storeId,country);
        }

        public virtual int removeAmountPolicyOnStore(User session, int storeId)
        {
            if (storeArchive.getInstance().getStore(storeId) == null)
                return -1;
            return PurchasePolicyArchive.getInstance().removeAmountPolicyOnStore(storeId);
        }

        public virtual int removeAmountPolicyOnCategory(User session, int storeId, string category)
        {
            if (storeArchive.getInstance().getStore(storeId) == null)
                return -1;
            return PurchasePolicyArchive.getInstance().removeAmountPolicyOnCategory(storeId,category);
        }

        public virtual int removeAmountPolicyOnProductInStore(User session, int storeId, int productInStoreId)
        {
            if (storeArchive.getInstance().getStore(storeId) == null)
                return -1;
            return PurchasePolicyArchive.getInstance().removeAmountPolicyOnProductInStore(productInStoreId);
        }

        public virtual int removeAmountPolicyOnCountry(User session, int storeId, string country)
        {
            if (storeArchive.getInstance().getStore(storeId) == null)
                return -1;
            return PurchasePolicyArchive.getInstance().removeAmountPolicyOnCountry(storeId,country);
        }

        public virtual int removeNoDiscountPolicyOnStore(User session, int storeId)
        {
            if (storeArchive.getInstance().getStore(storeId) == null)
                return -1;
            return PurchasePolicyArchive.getInstance().removeNoDiscountPolicyOnStore(storeId);
        }

        public virtual int removeNoDiscountPolicyOnCategoty(User session, int storeId, String category)
        {
            if (storeArchive.getInstance().getStore(storeId) == null)
                return -1;
            return PurchasePolicyArchive.getInstance().removeNoDiscountPolicyOnCategoty(storeId, category);
        }

        public virtual int removeNoDiscountPolicyOnProductInStore(User session, int storeId, int productInStoreId)
        {
            if (storeArchive.getInstance().getStore(storeId) == null)
                return -1;
            return PurchasePolicyArchive.getInstance().removeNoDiscountPolicyOnProductInStore(productInStoreId);
        }

        public virtual int removeNoDiscountPolicyOnCountry(User session, int storeId, string country)
        {
            if (storeArchive.getInstance().getStore(storeId) == null)
                return -1;
            return PurchasePolicyArchive.getInstance().removeNoDiscountPolicyOnCountry(storeId,country);
        }

        public virtual int removeNoCouponsPolicyOnStore(User session, int storeId)
        {
            if (storeArchive.getInstance().getStore(storeId) == null)
                return -1;
            return PurchasePolicyArchive.getInstance().removeNoCouponsPolicyOnStore(storeId);
        }

        public virtual int removeNoCouponPolicyOnCategoty(User session, int storeId, String category)
        {
            if (storeArchive.getInstance().getStore(storeId) == null)
                return -1;
            return PurchasePolicyArchive.getInstance().removeNoCouponPolicyOnCategoty(storeId,category);
        }

        public virtual int removeNoCouponPolicyOnProductInStore(User session,int storeId, int productInStoreId)
        {
            if (storeArchive.getInstance().getStore(storeId) == null)
                return -1;
            return PurchasePolicyArchive.getInstance().removeNoCouponPolicyOnProductInStore(productInStoreId);
        }

        public virtual int removeNoCouponPolicyOnCountry(User session, int storeId, string country)
        {
            if (storeArchive.getInstance().getStore(storeId) == null)
                return -1;
            return PurchasePolicyArchive.getInstance().removeNoCouponPolicyOnCountry(storeId, country);
        }

        public Boolean correlate(User session, Store s, String permission, StoreRole sR, Boolean allow)
        {
            switch (permission)
            {
                case "addProductInStore":
                    sR.getPremissions(session,s).addProductInStore(allow);
                    return true;

                case "editProductInStore":
                    sR.getPremissions(session, s).editProductInStore(allow);
                    return true;

                case "removeProductFromStore":
                    sR.getPremissions(session, s).removeProductFromStore(allow);
                    return true;

                case "addStoreManager":
                    sR.getPremissions(session, s).addStoreManager(allow);
                    return true;
                case "removeStoreManager":
                    sR.getPremissions(session, s).removeStoreManager(allow);
                    return true;
                case "addManagerPermission":
                    sR.getPremissions(session, s).addManagerPermission(allow);
                    return true;
                case "removeManagerPermission":
                    sR.getPremissions(session, s).removeManagerPermission(allow);
                    return true;
                case "viewPurchasesHistory":
                    sR.getPremissions(session, s).viewPurchasesHistory(allow);
                    return true;
                case "removeSaleFromStore":
                    sR.getPremissions(session, s).removeSaleFromStore(allow);
                    return true;
                case "editSale":
                    sR.getPremissions(session, s).editSale(allow);
                    return true;
                case "addSaleToStore":
                    sR.getPremissions(session, s).addSaleToStore(allow);
                    return true;
                case "addDiscount":
                    sR.getPremissions(session, s).addDiscount(allow);
                    return true;
                case "addNewCoupon":
                    sR.getPremissions(session, s).addNewCoupon(allow);
                    return true;
                case "removeDiscount":
                    sR.getPremissions(session, s).removeDiscount(allow);
                    return true;
                case "removeCoupon":
                    sR.getPremissions(session, s).removeCoupon(allow);
                    return true;
                case "changePolicy":
                    sR.getPremissions(session, s).changePolicy(allow);
                    return true;



                case "getPremissions":
                    return true;
                case "addStoreOwner":
                    return false;
                case "removeStoreOwner":
                    return false;

                default:
                    return false;
            }
        }


        public virtual Premissions getPremissions(User manager, Store s)
        {
            return StorePremissionsArchive.getInstance().getAllPremissions(s.getStoreId(),manager.getUserName());
        }

        public User getUser()
        {
            return user;
        }
        public Store getStore()
        {
            return store;
        }
        public void setUser(User u)
        {
            user = u;
        }
        public void setStore(Store s)
        {
            store = s;
        }

    }
}
