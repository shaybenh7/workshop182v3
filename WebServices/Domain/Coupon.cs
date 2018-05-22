using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class Coupon
    {
        private String couponId;
        private int productInStoreId;
        private double percentage;
        private int type; // type: 1- product in store , 2- category, 3- product
        private string category;
        private string productName;
        private string dueDate;
        private string restrictions;

        public Coupon(String couponId, int productInStoreId, double percentage, String dueDate)
        {
            this.couponId = couponId;
            this.productInStoreId = productInStoreId;
            this.percentage = percentage;
            this.dueDate = dueDate;
        }

        public Coupon(String couponId, int productInStoreId, int type, string productNameOrCategory, double percentage, String dueDate, string restrictions)
        {
            this.couponId = couponId;
            this.percentage = percentage;
            this.dueDate = dueDate;
            this.type = type;
            this.restrictions = restrictions;
            if (type == 1)
                this.productInStoreId = productInStoreId;
            else if (type == 2)
                this.category = productNameOrCategory;
            else
                this.productName = productNameOrCategory;
        }

        public string CouponId { get => couponId; set => couponId = value; }
        public int ProductInStoreId { get => productInStoreId; set => productInStoreId = value; }
        public double Percentage { get => percentage; set => percentage = value; }
        public int Type { get => type; set => type = value; }
        public string Category { get => category; set => category = value; }
        public string ProductName { get => productName; set => productName = value; }
        public string DueDate { get => dueDate; set => dueDate = value; }
        public string Restrictions { get => restrictions; set => restrictions = value; }

        public double getPriceAfterCouponDiscount(double price)
        {
            return price * (percentage / 100);
        }

    }
}
