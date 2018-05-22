using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace wsep182.Domain
{
    public class CouponsArchive
    {
        private LinkedList<Coupon> coupons;
        private static CouponsArchive instance;
        System.Timers.Timer couponCollector;

        private CouponsArchive()
        {
            coupons = new LinkedList<Coupon>();
            couponCollector = new System.Timers.Timer();
            couponCollector.Elapsed += new ElapsedEventHandler(CheckFinishedcoupon);
            couponCollector.Interval = 60 * 60 * 1000; // interval of one hour
            couponCollector.Enabled = true;
        }
        public static CouponsArchive getInstance()
        {
            if (instance == null)
                instance = new CouponsArchive();
            return instance;
        }
        public static void restartInstance()
        {
            instance = new CouponsArchive();
        }

        private void CheckFinishedcoupon(object source, ElapsedEventArgs e)
        {
            LinkedList<Coupon> CouponToRemove = new LinkedList<Coupon>();
            foreach (Coupon c in coupons)
            {
                if (DateTime.Now.CompareTo(DateTime.Parse(c.DueDate)) > 0)
                {
                    CouponToRemove.AddLast(c);
                }
            }
            foreach (Coupon c in CouponToRemove)
            {
                coupons.Remove(c);
            }
        }


        public Boolean addNewCoupon(String couponId, int productInStoreId, int percentage, String dueDate)
        {
            DateTime dueDateTime;
            try
            {
                dueDateTime = DateTime.Parse(dueDate);
            }
            catch (System.FormatException e)
            {
                return false;
            }
            if (DateTime.Compare(dueDateTime, DateTime.Now) < 0)
                return false;
            Coupon toAdd = new Coupon(couponId, productInStoreId, percentage, dueDate);
            foreach (Coupon coupon in coupons)
            {
                if (coupon.CouponId.Equals(couponId) && coupon.ProductInStoreId == productInStoreId)
                    return false;
            }
            coupons.AddLast(toAdd);
            return true;
        }

        public Boolean removeCouponForSpecificProduct(String couponId, int productInStoreId)
        {
            foreach (Coupon coupon in coupons)
            {
                if (coupon.CouponId.Equals(couponId) && coupon.ProductInStoreId == productInStoreId)
                {
                    coupons.Remove(coupon);
                    return true;
                }
            }
            return false;
        }

        public Boolean removeCoupon(String couponId)
        {
            Boolean found = false;
            LinkedList<int> indexes = new LinkedList<int>();
            for (int i = 0; i < coupons.Count; i++)
            {
                if (coupons.ElementAt(i).CouponId.Equals(couponId))
                {
                    indexes.AddLast(i);
                    found = true;
                }
            }
            if (!found)
                return false;
            for (int i = indexes.Count - 1; i >= 0; i--)
            {
                coupons.Remove(coupons.ElementAt(indexes.ElementAt(i)));
            }
            if (!found)
                return false;

            return true;
        }

        public Boolean editCoupon(String couponId, int newPercentage, String newDueDate)
        {
            Boolean found = false;
            foreach (Coupon coupon in coupons)
            {
                if (coupon.CouponId.Equals(couponId))
                {
                    coupon.Percentage = newPercentage;
                    coupon.DueDate = newDueDate;
                    found = true;
                }
            }
            if (!found)
                return false;
            return true;
        }

        public Coupon getCoupon(String couponId)
        {
            foreach (Coupon coupon in coupons)
            {
                if (coupon.CouponId.Equals(couponId))
                {
                    return coupon;
                }
            }
            return null;
        }

        public Coupon getCoupon(String couponId, int productInStoreId)
        {
            foreach (Coupon coupon in coupons)
            {
                if (coupon.CouponId.Equals(couponId) && coupon.ProductInStoreId == productInStoreId)
                {
                    return coupon;
                }
            }
            return null;
        }

        public LinkedList<Coupon> getAllCoupons()
        {
            return coupons;
        }


        public Boolean addNewCoupon(String couponId, int productInStoreId, int type, string categoryOrProductName,
         int percentage, String dueDate, string restrictions)
        {
            DateTime dueDateTime;
            try
            {
                dueDateTime = DateTime.Parse(dueDate);
            }
            catch (System.FormatException e)
            {
                return false;
            }
            if (DateTime.Compare(dueDateTime, DateTime.Now) < 0)
                return false;
            foreach (Coupon c in coupons)
            {
                if (c.CouponId.Equals(couponId))
                {
                    return false;
                }
                if (c.Type == type)
                {
                    switch (type)
                    {
                        case 1:
                            if (productInStoreId == c.ProductInStoreId && dueDate.Equals(c.DueDate))
                                return false;
                            break;
                        case 2:
                            if (c.Category.Equals(categoryOrProductName) && dueDate.Equals(c.DueDate))
                                return false;
                            break;
                        case 3:
                            if (c.ProductName.Equals(categoryOrProductName) && dueDate.Equals(c.DueDate))
                                return false;
                            break;
                    }
                }
            }

            Coupon toAdd = new Coupon(couponId,productInStoreId, type, categoryOrProductName, percentage, dueDate, restrictions);
            coupons.AddLast(toAdd);
            return true;
        }

        public int addNewCoupons(String couponId, int type, List<int> pisId, List<string> catOrProductsNames
            , int percentage, string dueDate, string restrictions)
        {
            if (type == 1)
            {
                foreach (int pid in pisId)
                {
                    Coupon toAdd = new Coupon(couponId, pid, 1, "", percentage, dueDate, restrictions);
                    coupons.AddLast(toAdd);
                }
            }
            else
            {
                foreach (string name in catOrProductsNames)
                {
                    Coupon toAdd = new Coupon(couponId , -1, type, name, percentage, dueDate, restrictions);
                    coupons.AddLast(toAdd);
                }
            }
            return 1;
        }


        public LinkedList<Coupon> getAllCouponsById(int productInStoreId)
        {
            LinkedList<Coupon> ans = new LinkedList<Coupon>();
            foreach (Coupon c in coupons)
            {
                ProductInStore p = ProductArchive.getInstance().getProductInStore(productInStoreId);
                string category = p.category;
                string productName = p.product.name;

                switch (c.Type)
                {
                    case 1: // discount on a product in store
                        if (c.ProductInStoreId == productInStoreId)
                        {
                            ans.AddLast(c);
                        }
                        break;
                    case 2: // discount on a category
                        if (c.Category.Equals(category))
                        {
                            ans.AddLast(c);
                        }
                        break;
                    case 3: // discount on a PRODUCT (not in store)
                        if (c.ProductName.Equals(productName))
                        {
                            ans.AddLast(c);
                        }
                        break;
                }
            }
            return ans;
        }






    }
}
