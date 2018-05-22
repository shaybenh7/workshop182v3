using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace wsep182.Domain
{
    public class DiscountsArchive
    {
        private LinkedList<Discount> discounts;
        private static DiscountsArchive instance;
        System.Timers.Timer DiscountCollector;

        private DiscountsArchive()
        {
            discounts = new LinkedList<Discount>();
            DiscountCollector = new System.Timers.Timer();
            DiscountCollector.Elapsed += new ElapsedEventHandler(CheckFinishedDiscounts);
            DiscountCollector.Interval = 60 * 60 * 1000; // interval of one hour
            DiscountCollector.Enabled = true;
        }
        public static DiscountsArchive getInstance()
        {
            if (instance == null)
                instance = new DiscountsArchive();
            return instance;
        }
        public static void restartInstance()
        {
            instance = new DiscountsArchive();
        }

        private void CheckFinishedDiscounts(object source, ElapsedEventArgs e)
        {
            LinkedList<Discount> discountToRemove = new LinkedList<Discount>();
            foreach (Discount d in discounts)
            {
                if (DateTime.Now.CompareTo(DateTime.Parse(d.DueDate)) > 0)
                {
                    discountToRemove.AddLast(d);
                }
            }
            foreach (Discount d in discountToRemove)
            {
                discounts.Remove(d);
            }
        }


        public int addNewDiscounts(int type, List<int> pisId,List<string> catOrProductsNames
           , int percentage, string dueDate, string restrictions)
        {
            if(type == 1)
            {
                foreach (int pid in pisId)
                {
                    Discount toAdd = new Discount(pid, 1, "", percentage, dueDate, restrictions);
                    discounts.AddLast(toAdd);
                }
            }
            else
            {
                foreach (string name in catOrProductsNames)
                {
                    Discount toAdd = new Discount(-1, type, name, percentage, dueDate, restrictions);
                    discounts.AddLast(toAdd);
                }
            }
            return 1;
        }

        // type: 1-productInStore, 2 - category, 3- Product
        public Boolean addNewDiscount(int productInStoreId,int type, string categoryOrProductName ,
            int percentage, String dueDate,string restrictions)
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
            foreach (Discount d in discounts)
            {
                if(d.Type == type)
                {
                    switch (type)
                    {
                        case 1:
                            if (productInStoreId == d.ProductInStoreId && dueDate.Equals(d.DueDate))
                                return false;
                            break;
                        case 2:
                            if (d.Category.Equals(categoryOrProductName) && dueDate.Equals(d.DueDate))
                                return false;
                            break;
                        case 3:
                            if (d.ProductName.Equals(categoryOrProductName) && dueDate.Equals(d.DueDate))
                                return false;
                            break;
                    }
                }
            }

            Discount toAdd = new Discount(productInStoreId,type, categoryOrProductName, percentage, dueDate, restrictions);
            discounts.AddLast(toAdd);
            return true;
        }
        public Boolean removeDiscount(int productInStoreId)
        {
            foreach (Discount discount in discounts)
            {
                if (discount.ProductInStoreId == productInStoreId)
                {
                    discounts.Remove(discount);
                    return true;
                }
            }
            return false;
        }

        /*
         * enter empty string if no date is needed
         */
        public Boolean removeDiscountByCategory(string category,string dueDate)
        {
            Boolean flag = false;
            if (dueDate != "")
            {
                foreach (Discount discount in discounts)
                {
                    if (discount.Category.Equals(category) && dueDate.Equals(discount.DueDate))
                    {
                        discounts.Remove(discount);
                        return true;
                    }
                }
            }
            else
            {
                foreach (Discount discount in discounts)
                {
                    if (discount.Category.Equals(category))
                    {
                        discounts.Remove(discount);
                        flag = true;
                    }
                }
            }
            return flag;
        }
        public Boolean editDiscount(int productInStoreId, int newPercentage, String newDueDate)
        {
            foreach (Discount discount in discounts)
            {
                if (discount.ProductInStoreId == productInStoreId)
                {
                    discount.Percentage = newPercentage;
                    discount.DueDate = newDueDate;
                    return true;
                }
            }
            return false;
        }

        public LinkedList<Discount> getAllDiscounts()
        {
            return discounts;
        }

        public Discount getDiscount(int productInStoreId)
        {
            foreach (Discount discount in discounts)
            {
                if (discount.ProductInStoreId == productInStoreId)
                {
                    return discount;
                }
            }
            return null;
        }

        public LinkedList<Discount> getAllDiscountsById(int productInStoreId)
        {
            LinkedList<Discount> ans = new LinkedList<Discount>();
            foreach(Discount d in discounts)
            {
                if (DateTime.Compare(DateTime.Parse(d.DueDate), DateTime.Now) < 0)
                    continue;
                ProductInStore p = ProductArchive.getInstance().getProductInStore(productInStoreId);
                string category = p.category;
                string productName = p.product.name;

                switch (d.Type)
                {
                    case 1: // discount on a product in store
                        if(d.ProductInStoreId == productInStoreId)
                        {
                            ans.AddLast(d);
                        }
                        break;
                    case 2: // discount on a category
                        if (d.Category.Equals(category))
                        {
                            ans.AddLast(d);
                        }
                        break;
                    case 3: // discount on a PRODUCT (not in store)
                        if (d.ProductName.Equals(productName))
                        {
                            ans.AddLast(d);
                        }
                        break;
                }
            }
            return ans;
        }














    }
}
