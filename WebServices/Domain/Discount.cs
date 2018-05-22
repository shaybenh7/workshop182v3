using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class Discount
    {
        private int productInStoreId;
        private double percentage;
        private int type;  // type: 1-productInStore, 2 - category, 3- Product
        private string category;
        private string productName;
        private string dueDate;
        private string restrictions;



        public Discount(int productInStoreId, int percentage, String dueDate){
            this.productInStoreId = productInStoreId;
            this.percentage = percentage;
            this.dueDate = dueDate;
        }

        public Discount(int productInStoreId, int type,string productNameOrCategory, double percentage, String dueDate,string restrictions)
        {
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

        public int ProductInStoreId { get => productInStoreId; set => productInStoreId = value; }
        public double Percentage { get => percentage; set => percentage = value; }
        public string DueDate { get => dueDate; set => dueDate = value; }
        public int Type { get => type; set => type = value; }
        public string Restrictions { get => restrictions; set => restrictions = value; }
        public string Category { get => category; set => category = value; }
        public string ProductName { get => productName; set => productName = value; }

        public Double getPriceAfterDiscount(double pricePerUnit, int amount)
        {
            return (pricePerUnit * amount) - ((((Double)(pricePerUnit * amount * percentage)) / 100));
        }

    }
}
