﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class Sale
    {

        int saleId;
        int productInStoreId;
        int typeOfSale;
        int amount;
        String dueDate;

        public Sale(int saleId, int productInStoreId, int typeOfSale, int amount, String dueDate)
        {
            this.saleId = saleId;
            this.productInStoreId = productInStoreId;
            this.typeOfSale = typeOfSale;
            this.amount = amount;
            this.dueDate = dueDate;
        }

        public int SaleId { get => saleId; set => saleId = value; }
        public int ProductInStoreId { get => productInStoreId; set => productInStoreId = value; }
        public int Amount { get => amount; set => amount = value; }
        public int TypeOfSale { get => typeOfSale; set => typeOfSale = value; }
        public string DueDate { get => dueDate; set => dueDate = value; }

        public double getPriceBeforeDiscount(int amount)
        {
            ProductInStore p = ProductManager.getInstance().getProductInStore(productInStoreId);
            return p.getPrice() * amount;
        }
        public double getPriceAfterDiscount(int amount)
        {
            ProductInStore p = ProductManager.getInstance().getProductInStore(productInStoreId);
            LinkedList<Discount> dis = DiscountsManager.getInstance().getAllDiscountsById(productInStoreId);
            if (dis.Count != 0)
            {
                double initialPrice = p.getPrice();
                foreach(Discount d in dis)
                {
                    initialPrice = initialPrice - initialPrice * (d.Percentage / 100);
                }
                return initialPrice*amount;
            }
            else
            {
                return getPriceBeforeDiscount(amount);
            }
        }

		public static LinkedList<Sale> searchSales(String searchString)
		{
			LinkedList<Sale> allAvailableSales = new LinkedList<Sale>();
			LinkedList<ProductInStore> allProducts = ProductManager.getInstance().searchProducts(searchString);
			foreach (ProductInStore p in allProducts)
			{
				LinkedList<Sale> allSalesOfAProduct = SalesManager.getInstance().getSalesByProductInStoreId(p.getProductInStoreId());
				foreach (Sale sale in allSalesOfAProduct)
					allAvailableSales.AddLast(sale);
			}
			return allAvailableSales;
		}

		public static LinkedList<Sale> getAllSales()
        {
            return SalesManager.getInstance().getAllSales();
        }

    }
}
