using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServices.DAL;
using WebServices.Domain;

namespace wsep182.Domain
{
    public class BuyHistoryManager
    {

        private static BuyHistoryManager instance;
        private static int buyId;
        public BuyHistoryDB BHDB;
        public LinkedList<Purchase> buysHistory;

        private BuyHistoryManager()
        {
            BHDB = new BuyHistoryDB(configuration.DB_MODE);
            buysHistory = BHDB.Get();
            buyId = 0;
        }

        public static BuyHistoryManager getInstance()
        {
            if (instance == null)
                instance = new BuyHistoryManager();
            return instance;
        }

        public static void restartInstance()
        {
            instance = new BuyHistoryManager();
        }

        public int getNextBuyId()
        {
            return buyId++;
        }

        public Boolean addBuyHistory(int productId, int storeId , String userName, double price,
                                     String date, int amount, int typeOfSale)
        {
            int buyId = getNextBuyId();
            Purchase toAdd = new Purchase(buyId, productId, storeId, userName, price, date, amount, typeOfSale);
            BHDB.Add(toAdd);
            buysHistory.AddLast(toAdd);
            return true;
        }

        public LinkedList<Purchase> viewHistory()
        {
            return buysHistory;
        }

        public LinkedList<Purchase> viewHistoryByStoreId(int storeId)
        {
            LinkedList <Purchase> ans = new LinkedList<Purchase>();
            foreach(Purchase buy in buysHistory)
            {
                if (buy.StoreId == storeId)
                {
                    ans.AddLast(buy);
                }
            }
            return ans;
        }

        public LinkedList<Purchase> viewHistoryByUserName(String userName)
        {
            LinkedList<Purchase> ans = new LinkedList<Purchase>();
            foreach (Purchase buy in buysHistory)
            {
                if (buy.UserName.Equals(userName))
                {
                    ans.AddLast(buy);
                }
            }
            return ans;
        }





    }
}
