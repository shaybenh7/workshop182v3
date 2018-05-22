using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class PurchasePolicyArchive
    {
        // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
        private int productNUM = 1;
        private int storeNUM = 2;
        private int categoryNUM = 3;
        private int productInStoreNUM = 4;
        private int countryNUM = 5;

        private LinkedList<PurchasePolicy> policys;
        private static PurchasePolicyArchive instance;

        private PurchasePolicyArchive()
        {
            policys = new LinkedList<PurchasePolicy>();
        }
        public static PurchasePolicyArchive getInstance()
        {
            if (instance == null)
                instance = new PurchasePolicyArchive();
            return instance;
        }
        public static void restartInstance()
        {
            instance = new PurchasePolicyArchive();
        }

        public string showPolicy(int productInStoreId)
        {
            string ans = "";
            LinkedList<PurchasePolicy> relevantPolicys = getAllRelevantPolicysForProductInStore(productInStoreId, "");
            if (relevantPolicys.Count == 0)
                return "There are no resrictions for this product!\n";

            if (isExistNoDiscountsRestrictions(relevantPolicys))
                ans += "-There are no discounts for this product.\n";
            if(isExistNoCouponsRestrictions(relevantPolicys))
                ans += "-This product does not support the use of coupons.\n";
            if (isExistAmountRestrictions(relevantPolicys))
            {
                // finding amount intersection of all restricions
                int minAvailable = -1;
                int maxAvailable = -1;
                foreach(PurchasePolicy p in relevantPolicys)
                {
                    if (!p.NoLimit)
                    {
                        if(minAvailable == -1 && maxAvailable == -1)
                        {
                            minAvailable = p.MinAmount;
                            maxAvailable = p.MaxAmount;
                        }
                        else
                        {
                            if (p.MinAmount > minAvailable)
                                minAvailable = p.MinAmount;
                            if (p.MaxAmount < maxAvailable)
                                maxAvailable = p.MaxAmount;
                        }
                    }
                }
                ans += "-Minimum amount per order: " + minAvailable.ToString() +"\n";
                ans += "-Maximum amount per order: " + maxAvailable.ToString() + "\n";
            }
            return ans;
        }

        private bool isExistAmountRestrictions(LinkedList<PurchasePolicy> list)
        {
            foreach(PurchasePolicy p in list)
            {
                if (!p.NoLimit)
                    return true;
            }
            return false;
        }
        private bool isExistNoDiscountsRestrictions(LinkedList<PurchasePolicy> list)
        {
            foreach (PurchasePolicy p in list)
            {
                if (p.NoDiscount)
                    return true;
            }
            return false;
        }
        private bool isExistNoCouponsRestrictions(LinkedList<PurchasePolicy> list)
        {
            foreach (PurchasePolicy p in list)
            {
                if (p.NoCoupons)
                    return true;
            }
            return false;
        }





        private void appendLists(LinkedList<PurchasePolicy> first, LinkedList<PurchasePolicy> second)
        {
            foreach (PurchasePolicy p in second)
                first.AddLast(p);
        }
        public LinkedList<PurchasePolicy> getAllRelevantPolicysForProductInStore(int productInStoreId,string country)
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            LinkedList<PurchasePolicy> ans = new LinkedList<PurchasePolicy>();
            ProductInStore pis = ProductArchive.getInstance().getProductInStore(productInStoreId);
            appendLists(ans, getAllStorePolicys(pis.store.storeId));
            appendLists(ans, getAllCategoryPolicys(pis.category, pis.store.storeId));
            appendLists(ans, getAllCountryPolicys(country, pis.store.storeId));
            appendLists(ans, getAllProductInStorePolicys(productInStoreId));
            appendLists(ans, getAllProductPolicys(pis.product.name));
            return ans;
        }

        public LinkedList<PurchasePolicy> getAllProductPolicys(string productName)
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            LinkedList<PurchasePolicy> ans = new LinkedList<PurchasePolicy>();
            foreach(PurchasePolicy p in policys)
            {
                if(p.TypeOfPolicy == productNUM && p.ProductName.Equals(productName))
                {
                    ans.AddLast(p);
                }
            }
            return ans;
        }
        public LinkedList<PurchasePolicy> getAllStorePolicys(int storeId)
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            LinkedList<PurchasePolicy> ans = new LinkedList<PurchasePolicy>();
            foreach (PurchasePolicy p in policys)
            {
                if (p.TypeOfPolicy == storeNUM && p.StoreId == storeId)
                {
                    ans.AddLast(p);
                }
            }
            return ans;
        }
        public LinkedList<PurchasePolicy> getAllCategoryPolicys(string category, int storeId)
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            LinkedList<PurchasePolicy> ans = new LinkedList<PurchasePolicy>();
            foreach (PurchasePolicy p in policys)
            {
                if (p.TypeOfPolicy == categoryNUM && p.Category.Equals(category) && p.StoreId == storeId)
                {
                    ans.AddLast(p);
                }
            }
            return ans;
        }
        public LinkedList<PurchasePolicy> getAllProductInStorePolicys(int productInStoreId)
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            LinkedList<PurchasePolicy> ans = new LinkedList<PurchasePolicy>();
            foreach (PurchasePolicy p in policys)
            {
                if (p.TypeOfPolicy == productInStoreNUM && p.ProductInStoreId == productInStoreId)
                {
                    ans.AddLast(p);
                }
            }
            return ans;
        }
        public LinkedList<PurchasePolicy> getAllCountryPolicys(string country,int storeId)
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            LinkedList<PurchasePolicy> ans = new LinkedList<PurchasePolicy>();
            foreach (PurchasePolicy p in policys)
            {
                if (p.TypeOfPolicy == countryNUM && p.Country.Equals(country) && p.StoreId == storeId)
                {
                    ans.AddLast(p);
                }
            }
            return ans;
        }

        // ========================= AMOUNT CONSTRAINTS !!!! ================================================
        public int setAmountPolicyOnProduct(string productName, int minAmount, int maxAmount)
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            PurchasePolicy toAdd = new PurchasePolicy();
            toAdd.TypeOfPolicy = 1;
            toAdd.ProductName = productName;
            toAdd.MinAmount = minAmount;
            toAdd.MaxAmount = maxAmount;
            toAdd.NoLimit = false;
            policys.AddLast(toAdd);
            return 1;
        }
        public int setAmountPolicyOnStore(int storeId, int minAmount, int maxAmount)
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            PurchasePolicy toAdd = new PurchasePolicy();
            toAdd.TypeOfPolicy = 2;
            toAdd.StoreId = storeId;
            toAdd.MinAmount = minAmount;
            toAdd.MaxAmount = maxAmount;
            toAdd.NoLimit = false;
            policys.AddLast(toAdd);
            return 1;
        }
        public int setAmountPolicyOnCategory(int storeId, string category, int minAmount, int maxAmount)
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            PurchasePolicy toAdd = new PurchasePolicy();
            toAdd.TypeOfPolicy = 3;
            toAdd.Category = category;
            toAdd.StoreId = storeId;
            toAdd.MinAmount = minAmount;
            toAdd.MaxAmount = maxAmount;
            toAdd.NoLimit = false;
            policys.AddLast(toAdd);
            return 1;
        }
        public int setAmountPolicyOnProductInStore(int productInStoreId, int minAmount, int maxAmount)
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            PurchasePolicy toAdd = new PurchasePolicy();
            toAdd.TypeOfPolicy = 4;
            toAdd.ProductInStoreId = productInStoreId;
            toAdd.MinAmount = minAmount;
            toAdd.MaxAmount = maxAmount;
            toAdd.NoLimit = false;
            policys.AddLast(toAdd);
            return 1;
        }
        public int setAmountPolicyOnCountry(int storeId, string country, int minAmount, int maxAmount)
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            PurchasePolicy toAdd = new PurchasePolicy();
            toAdd.TypeOfPolicy = 5;
            toAdd.Country = country;
            toAdd.StoreId = storeId;
            toAdd.MinAmount = minAmount;
            toAdd.MaxAmount = maxAmount;
            toAdd.NoLimit = false;
            policys.AddLast(toAdd);
            return 1;
        }

// ========================= DISCOUNT CONSTRAINTS !!!! ================================================
        public int setNoDiscountPolicyOnProduct(string productName)
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            PurchasePolicy toAdd = new PurchasePolicy();
            toAdd.TypeOfPolicy = 1;
            toAdd.ProductName = productName;
            toAdd.NoDiscount = true;
            policys.AddLast(toAdd);
            return 1;
        }
        public int setNoDiscountPolicyOnStore(int storeId)
        {
            // 1-Product (system level) , 2- Store, 3-category, 4- product in store, 5-country
            PurchasePolicy toAdd = new PurchasePolicy();
            toAdd.TypeOfPolicy = 2;
            toAdd.StoreId = storeId;
            toAdd.NoDiscount = true;
            policys.AddLast(toAdd);
            return 1;
        }
        public int setNoDiscountPolicyOnCategoty(int storeId, String category)
        {
            // 1-Product (system level) , 2- Store, 3-category, 4- product in store, 5-country
            PurchasePolicy toAdd = new PurchasePolicy();
            toAdd.TypeOfPolicy = 3;
            toAdd.StoreId = storeId;
            toAdd.Category = category;
            toAdd.NoDiscount = true;
            policys.AddLast(toAdd);
            return 1;
        }
        public int setNoDiscountPolicyOnProductInStore(int productInStoreId)
        {
            // 1-Product (system level) , 2- Store, 3-category, 4- product in store, 5-country
            PurchasePolicy toAdd = new PurchasePolicy();
            toAdd.TypeOfPolicy = 4;
            toAdd.ProductInStoreId = productInStoreId;
            toAdd.NoDiscount = true;
            policys.AddLast(toAdd);
            return 1;
        }
        public int setNoDiscountPolicyOnCountry(int storeId, string country)
        {
            // 1-Product (system level) , 2- Store, 3-category, 4- product in store, 5-country
            PurchasePolicy toAdd = new PurchasePolicy();
            toAdd.TypeOfPolicy = 5;
            toAdd.StoreId = storeId;
            toAdd.Country = country;
            toAdd.NoDiscount = true;
            policys.AddLast(toAdd);
            return 1;
        }

// ========================= COUPONS CONSTRAINTS !!!! ================================================
        public int setNoCouponsPolicyOnProduct(string productName)
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            PurchasePolicy toAdd = new PurchasePolicy();
            toAdd.TypeOfPolicy = 1;
            toAdd.ProductName = productName;
            toAdd.NoCoupons = true;
            policys.AddLast(toAdd);
            return 1;
        }
        public int setNoCouponsPolicyOnStore(int storeId)
        {
            // 1-Product (system level) , 2- Store, 3-category, 4- product in store, 5-country
            PurchasePolicy toAdd = new PurchasePolicy();
            toAdd.TypeOfPolicy = 2;
            toAdd.StoreId = storeId;
            toAdd.NoCoupons = true;
            policys.AddLast(toAdd);
            return 1;
        }
        public int setNoCouponPolicyOnCategoty(int storeId, String category)
        {
            // 1-Product (system level) , 2- Store, 3-category, 4- product in store, 5-country
            PurchasePolicy toAdd = new PurchasePolicy();
            toAdd.TypeOfPolicy = 3;
            toAdd.StoreId = storeId;
            toAdd.Category = category;
            toAdd.NoCoupons = true;
            policys.AddLast(toAdd);
            return 1;
        }
        public int setNoCouponPolicyOnProductInStore(int productInStoreId)
        {
            // 1-Product (system level) , 2- Store, 3-category, 4- product in store, 5-country
            PurchasePolicy toAdd = new PurchasePolicy();
            toAdd.TypeOfPolicy = 4;
            toAdd.ProductInStoreId = productInStoreId;
            toAdd.NoCoupons = true;
            policys.AddLast(toAdd);
            return 1;
        }
        public int setNoCouponPolicyOnCountry(int storeId, string country)
        {
            // 1-Product (system level) , 2- Store, 3-category, 4- product in store, 5-country
            PurchasePolicy toAdd = new PurchasePolicy();
            toAdd.TypeOfPolicy = 5;
            toAdd.StoreId = storeId;
            toAdd.Country = country;
            toAdd.NoCoupons = true;
            policys.AddLast(toAdd);
            return 1;
        }


        //==================== REMOVE POLICYS !!!!! =============================

        // ========================= AMOUNT CONSTRAINTS !!!! ==================================
        public int removeAmountPolicyOnProduct(string productName)
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            foreach(PurchasePolicy p in policys)
            {
                if (p.TypeOfPolicy == productNUM && p.ProductName.Equals(productName) && p.NoLimit == false)
                {
                    policys.Remove(p);
                    return 1;
                }
            }
            return -1;
        }
        public int removeAmountPolicyOnStore(int storeId)
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            foreach (PurchasePolicy p in policys)
            {
                if (p.TypeOfPolicy == storeNUM && p.StoreId == storeId && p.NoLimit == false)
                {
                    policys.Remove(p);
                    return 1;
                }
            }
            return -1;
        }
        public int removeAmountPolicyOnCategory(int storeId, string category)
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            foreach (PurchasePolicy p in policys)
            {
                if (p.TypeOfPolicy == categoryNUM && p.StoreId == storeId && p.Category.Equals(category) && p.NoLimit == false)
                {
                    policys.Remove(p);
                    return 1;
                }
            }
            return -1;
        }
        public int removeAmountPolicyOnProductInStore(int productInStoreId)
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            foreach (PurchasePolicy p in policys)
            {
                if (p.TypeOfPolicy == productInStoreNUM && p.ProductInStoreId == productInStoreId && p.NoLimit == false)
                {
                    policys.Remove(p);
                    return 1;
                }
            }
            return -1;
        }
        public int removeAmountPolicyOnCountry(int storeId, string country)
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            foreach (PurchasePolicy p in policys)
            {
                if (p.TypeOfPolicy == countryNUM && p.StoreId == storeId && p.Country.Equals(country) && p.NoLimit == false)
                {
                    policys.Remove(p);
                    return 1;
                }
            }
            return -1;
        }

        // ========================= DISCOUNT CONSTRAINTS !!!! ================================================
        public int removeNoDiscountPolicyOnProduct(string productName)
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            foreach (PurchasePolicy p in policys)
            {
                if (p.TypeOfPolicy == productNUM && p.ProductName.Equals(productName) &&  p.NoDiscount == true)
                {
                    policys.Remove(p);
                    return 1;
                }
            }
            return -1;
        }
        public int removeNoDiscountPolicyOnStore(int storeId)
        {
            // 1-Product (system level) , 2- Store, 3-category, 4- product in store, 5-country
            foreach (PurchasePolicy p in policys)
            {
                if (p.TypeOfPolicy == storeNUM && p.StoreId == storeId && p.NoDiscount == true)
                {
                    policys.Remove(p);
                    return 1;
                }
            }
            return -1;
        }
        public int removeNoDiscountPolicyOnCategoty(int storeId, String category)
        {
            // 1-Product (system level) , 2- Store, 3-category, 4- product in store, 5-country
            foreach (PurchasePolicy p in policys)
            {
                if (p.TypeOfPolicy == categoryNUM && p.StoreId == storeId && p.Category.Equals(category) && p.NoDiscount == true)
                {
                    policys.Remove(p);
                    return 1;
                }
            }
            return -1;
        }
        public int removeNoDiscountPolicyOnProductInStore(int productInStoreId)
        {
            // 1-Product (system level) , 2- Store, 3-category, 4- product in store, 5-country
            foreach (PurchasePolicy p in policys)
            {
                if (p.TypeOfPolicy == productInStoreNUM && p.ProductInStoreId == productInStoreId && p.NoDiscount == true)
                {
                    policys.Remove(p);
                    return 1;
                }
            }
            return -1;
        }
        public int removeNoDiscountPolicyOnCountry(int storeId, string country)
        {
            // 1-Product (system level) , 2- Store, 3-category, 4- product in store, 5-country
            foreach (PurchasePolicy p in policys)
            {
                if (p.TypeOfPolicy == countryNUM && p.StoreId == storeId && p.Country.Equals(country) && p.NoDiscount == true)
                {
                    policys.Remove(p);
                    return 1;
                }
            }
            return -1;
        }

        // ========================= COUPONS CONSTRAINTS !!!! ================================================
        public int removeNoCouponsPolicyOnProduct(string productName)
        {
            // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
            foreach (PurchasePolicy p in policys)
            {
                if (p.TypeOfPolicy == productNUM && p.ProductName.Equals(productName) && p.NoCoupons == true)
                {
                    policys.Remove(p);
                    return 1;
                }
            }
            return -1;
        }
        public int removeNoCouponsPolicyOnStore(int storeId)
        {
            // 1-Product (system level) , 2- Store, 3-category, 4- product in store, 5-country
            foreach (PurchasePolicy p in policys)
            {
                if (p.TypeOfPolicy == storeNUM && p.StoreId == storeId && p.NoCoupons == true)
                {
                    policys.Remove(p);
                    return 1;
                }
            }
            return -1;
        }
        public int removeNoCouponPolicyOnCategoty(int storeId, String category)
        {
            // 1-Product (system level) , 2- Store, 3-category, 4- product in store, 5-country
            foreach (PurchasePolicy p in policys)
            {
                if (p.TypeOfPolicy == categoryNUM && p.StoreId == storeId && p.Category.Equals(category) && p.NoCoupons == true)
                {
                    policys.Remove(p);
                    return 1;
                }
            }
            return -1;
        }
        public int removeNoCouponPolicyOnProductInStore(int productInStoreId)
        {
            // 1-Product (system level) , 2- Store, 3-category, 4- product in store, 5-country
            foreach (PurchasePolicy p in policys)
            {
                if (p.TypeOfPolicy == productInStoreNUM && p.ProductInStoreId == productInStoreId && p.NoCoupons == true)
                {
                    policys.Remove(p);
                    return 1;
                }
            }
            return -1;
        }
        public int removeNoCouponPolicyOnCountry(int storeId, string country)
        {
            // 1-Product (system level) , 2- Store, 3-category, 4- product in store, 5-country
            foreach (PurchasePolicy p in policys)
            {
                if (p.TypeOfPolicy == countryNUM && p.Country.Equals(country) && p.NoCoupons == true)
                {
                    policys.Remove(p);
                    return 1;
                }
            }
            return -1;
        }













    }
}
