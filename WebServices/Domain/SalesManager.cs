using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServices.DAL;
using WebServices.Domain;

namespace wsep182.Domain
{
    public class SalesManager
    {
        private LinkedList<Sale> sales;
        private SaleDB SDB;
        private static SalesManager instance;
        private static int saleId=0;

        private SalesManager()
        {
            SDB = new SaleDB(configuration.DB_MODE);
            sales = SDB.Get();
            saleId = currSaleIndex();
        }
        public static void restartInstance()
        {
            instance = new SalesManager();
        }
        public int getNextSaleId()
        {
            saleId = currSaleIndex();
            return ++saleId;
        }

        public int currSaleIndex()
        {
            LinkedList<Sale> temp = SDB.Get();
            int index = 0;
            foreach (Sale s in temp)
            {
                if (s.SaleId > index)
                {
                    index = s.SaleId;
                }
            }
            return index;
        }

        public static SalesManager getInstance()
        {
            if (instance == null)
                instance = new SalesManager();
            return instance;
        }

        public Boolean removeSale(int saleId)
        {
            foreach(Sale s in sales)
            {
                if (s.SaleId == saleId)
                {
                    SDB.Remove(s);
                    sales.Remove(s);
                    return true;
                }
            }
            return false;
        }

        public Sale addSale(int productInStoreId, int typeOfSale, int amount, String dueDate)
        {
            DateTime dueDateTime;
            try
            {
                dueDateTime = DateTime.Parse(dueDate);
            }
            catch (System.FormatException e)
            {
                return null;
            }
            if (DateTime.Compare(dueDateTime, DateTime.Now) < 0)
                return null;
            foreach (Sale sale in sales)
            {
                if(sale.ProductInStoreId==productInStoreId && sale.TypeOfSale == typeOfSale && sale.DueDate.Equals(dueDate))
                {
                    sale.Amount += amount;
                    return sale;
                }
            }
            int saleId = getNextSaleId();
            Sale toAdd = new Sale(saleId, productInStoreId, typeOfSale, amount, dueDate);
            SDB.Add(toAdd);
            sales.AddLast(toAdd);
            return toAdd;
        }

        public Sale getSale(int saleId)
        {
            foreach (Sale sale in sales)
            {
                if (sale.SaleId == saleId)
                {
                    return sale;
                }
            }
            return null;
        }
        public Boolean editSale(int saleId, int amount, String dueDate)
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
            foreach (Sale sale in sales)
            {
                if (sale.SaleId == saleId)
                {
                    SDB.Remove(sale);
                    sale.Amount = amount;
                    sale.DueDate = dueDate;
                    SDB.Add(sale);
                    return true;
                }
            }
            return false;
        }
        public Boolean setNewAmountForSale(int saleId, int amount)
        {
            foreach (Sale sale in sales)
            {
                if (sale.SaleId == saleId)
                {
                    SDB.Remove(sale);
                    sale.Amount = amount;
                    SDB.Add(sale);
                    return true;
                }
            }
            return false;
        }

        public LinkedList<Sale> getAllSales()
        {
            return sales;
        }

        public LinkedList<Sale> getSalesByProductInStoreId(int productInStoreId)
        {
            LinkedList<Sale> ans = new LinkedList<Sale>();
            foreach(Sale sale in sales)
            {
                if(sale.ProductInStoreId == productInStoreId)
                {
                    ans.AddLast(sale);
                }
            }

            return ans;
        }

	}
}
