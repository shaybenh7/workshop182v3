using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class Customer : StoreRole
    {
        public Customer(User u, Store s) : base(u, s)
        {
            type = "Customer";
        }

        public LinkedList<Purchase> viewStoreHistory(Store store, User session)
        {
            return null;
        }


        public override int addProductInStore(User session, Store s, String productName, double price, int amount, string category)
        {
            return -4;//-4 if don't have premition
        }
        public override Boolean addDiscount(User session, ProductInStore p, int percentage, String dueDate)
        {
            return false;
        }

        public override Boolean addNewCoupon(User session, String couponId, ProductInStore p, int percentage, String dueDate)
        {
            return false;
        }

        public override int editProductInStore(User session, ProductInStore p, int quantity, double price)
        {
            return -4;//-4 if don't have premition
        }

        public override int removeProductFromStore(User session, Store s, ProductInStore p)
        {
            return -4;//-4 if don't have premition
        }

        public override int addStoreManager(User session, Store s, String newManager)
        {
            return -4;//-4 if don't have premition
        }

        public override int removeStoreManager(User session, Store s, String oldManager)
        {
            return -4;//-4 if don't have premition
        }

        public override Boolean addStoreOwner(User session, Store s, String newOwner)
        {
            return false;
        }

        public override int removeStoreOwner(User session, Store s, String ownerToDelete)
        {
            return -4;//-4 if don't have premition

        }

        public override int addManagerPermission(User session, String permission, Store s, String manager)
        {
            return -4;//-4 if don't have premition

        }

        public override int addSaleToStore(User session, Store s, int productInStoreId, int typeOfSale, int amount, String dueDate)
        {
            return -4;//-4 if don't have premition
        }

        public override int removeManagerPermission(User session, String permission, Store s, String manager)
        {
            return -4;//-4 if don't have premition
        }

        public override int removeSaleFromStore(User session, Store s, int saleId)
        {
            return -4;//-4 if don't have premition
        }

        public override int editSale(User session, Store s, int saleId, int amount, String dueDate)
        {
            return -4;//-4 if don't have premition
        }

        public override LinkedList<Purchase> viewPurchasesHistory(User session, Store s)
        {
            return null;
        }

        public override Premissions getPremissions(User session, Store s)
        {
            return null;
        }

    }
}
