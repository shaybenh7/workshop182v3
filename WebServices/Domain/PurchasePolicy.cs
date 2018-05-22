using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class PurchasePolicy
    {
        // 1-Product(system level) , 2- Store, 3-category, 4- product in store, 5-country
        private int typeOfPolicy;
        string productName = "";
        int storeId;
        string category = "";
        int productInStoreId;
        string country = "";

        private int minAmount;
        private int maxAmount;
        private Boolean noLimit;
        private Boolean noDiscount;
        private Boolean noCoupons;

        public PurchasePolicy()
        {
            minAmount = 0;
            maxAmount = -1;
            noLimit = true;
            noDiscount = false;
            noCoupons = false;
        }

        public int TypeOfPolicy { get => typeOfPolicy; set => typeOfPolicy = value; }
        public string ProductName { get => productName; set => productName = value; }
        public int StoreId { get => storeId; set => storeId = value; }
        public string Category { get => category; set => category = value; }
        public int ProductInStoreId { get => productInStoreId; set => productInStoreId = value; }
        public string Country { get => country; set => country = value; }
        public int MinAmount { get => minAmount; set => minAmount = value; }
        public int MaxAmount { get => maxAmount; set => maxAmount = value; }
        public bool NoLimit { get => noLimit; set => noLimit = value; }
        public bool NoDiscount { get => noDiscount; set => noDiscount = value; }
        public bool NoCoupons { get => noCoupons; set => noCoupons = value; }

    }
}
