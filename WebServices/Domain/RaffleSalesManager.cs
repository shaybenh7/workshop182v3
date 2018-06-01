using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WebServices.DAL;
using WebServices.Domain;

namespace wsep182.Domain
{
    public class RaffleSalesManager
    {
        private LinkedList<RaffleSale> raffleSales;
        private RaffleSaleDB RSDB;
        private static RaffleSalesManager instance;
        System.Timers.Timer RaffelCollector;

        private RaffleSalesManager()
        {
            RSDB = new RaffleSaleDB(configuration.DB_MODE);
            raffleSales = RSDB.Get();
            RaffelCollector = new System.Timers.Timer();
            RaffelCollector.Elapsed += new ElapsedEventHandler(CheckFinishedRaffelSales);
            RaffelCollector.Interval = 60*60*1000; // interval of one hour
            RaffelCollector.Enabled = true;
        }

        private void CheckFinishedRaffelSales(object source, ElapsedEventArgs e)
        {
            LinkedList<RaffleSale> raffleSalesToRemove = new LinkedList<RaffleSale>();
            foreach (RaffleSale rs in raffleSales)
            {
                if (DateTime.Now.CompareTo(DateTime.Parse(rs.DueDate)) > 0)
                {
                    string message = "the Raffle sale " + rs.SaleId + " has been canceled";
                    NotificationPublisher.getInstance().publish(NotificationPublisher.NotificationCategories.RaffleSale, message, rs.SaleId);
                    //NotificationManager.getInstance().notifyUser(rs.UserName, message);
                    raffleSalesToRemove.AddLast(rs);
                }
            }
            foreach (RaffleSale rs in raffleSalesToRemove)
            {
                RSDB.Remove(rs);
                raffleSales.Remove(rs);
            }
        }


        public static RaffleSalesManager getInstance()
        {
            if (instance == null)
                instance = new RaffleSalesManager();
            return instance;
        }
        public static void restartInstance()
        {
            instance = new RaffleSalesManager();
        }

        public Boolean addRaffleSale(int saleId, String userName, double offer, String dueDate)
        {
            RaffleSale toAdd = new RaffleSale(saleId, userName, offer, dueDate);
            ProductInStore pis = ProductManager.getInstance().getProductInStore(SalesManager.getInstance().getSale(saleId).ProductInStoreId);
            StoreRole sR = StoreRole.getStoreRole(pis.store, UserManager.getInstance().getUser(userName));
            NotificationPublisher.getInstance().signToCategory(sR, NotificationPublisher.NotificationCategories.RaffleSale);
            RSDB.Add(toAdd);
            raffleSales.AddLast(toAdd);
            return true;
        }
        public LinkedList<RaffleSale> getAllRaffleSalesBySaleId(int saleId)
        {
            LinkedList<RaffleSale> ans = new LinkedList<RaffleSale>();
            foreach (RaffleSale sale in raffleSales)
            {
                if (sale.SaleId == saleId)
                {
                    ans.AddLast(sale);
                }
            }
            return ans;
        }

        public LinkedList<RaffleSale> getAllRaffleSalesByUserName(String username)
        {
            LinkedList<RaffleSale> ans = new LinkedList<RaffleSale>();
            foreach (RaffleSale sale in raffleSales)
            {
                if (sale.UserName == username)
                {
                    ans.AddLast(sale);
                }
            }
            return ans;
        }

        public double getRemainingSumToPayInRaffleSale(int saleId)
        {
            Sale s = SalesManager.getInstance().getSale(saleId);
            if (s == null)
                return -2;
            ProductInStore p = ProductManager.getInstance().getProductInStore(s.ProductInStoreId);
            if (p == null)
                return -3;
            double price = p.getPrice();

            foreach(RaffleSale rs in raffleSales)
            {
                if (rs.SaleId == saleId)
                    price -= rs.Offer;
            }
            return price;
        }

        public void sendMessageTORaffleWinner(int saleId)
        {
            Sale s = SalesManager.getInstance().getSale(saleId);
            ProductInStore p = ProductManager.getInstance().getProductInStore(s.ProductInStoreId);
            LinkedList<RaffleSale> relevant = new LinkedList<RaffleSale>();
            double realPrice = p.price;
            double acc = 0;
            foreach(RaffleSale rs in raffleSales)
            {
                if (rs.SaleId == saleId)
                {
                    acc += rs.Offer;
                    relevant.AddLast(rs);
                }
            }
            if (acc == realPrice)
            {
                int index = 1;
                Random rand = new Random();
                int winner = rand.Next(1, (int)realPrice);
                RaffleSale winnerS = null;
                foreach (RaffleSale r in relevant)
                {
                    if (winner <= r.Offer + index && winner >= index)
                    {
                        string message = "YOU WON THE RAFFLE SALE ON PRODUCT: " + getProductNameFromSaleId(r.SaleId);
                        NotificationPublisher.getInstance().publish(NotificationPublisher.NotificationCategories.RaffleSale, message, r.SaleId);
                        //NotificationManager.getInstance().notifyUser(r.UserName, message);
                        winnerS = r;
                        break;
                    }
                    else
                    {
                        index += (int)r.Offer;
                    }
                }
                if (winnerS != null) {
                    RSDB.Remove(winnerS);
                    raffleSales.Remove(winnerS);
                    relevant.Remove(winnerS);
                }
                foreach (RaffleSale r in relevant)
                {
                    string message = "YOU LOST THE RAFFLE SALE ON PRODUCT: " + getProductNameFromSaleId(r.SaleId);
                    NotificationPublisher.getInstance().publish(NotificationPublisher.NotificationCategories.RaffleSale, message, r.SaleId);

                    //NotificationManager.getInstance().notifyUser(r.UserName, message);
                    RSDB.Remove(winnerS);
                    raffleSales.Remove(r);
                    
                }


            }
        }

        private string getProductNameFromSaleId(int saleId)
        {
            string ans = "";
            Sale s = SalesManager.getInstance().getSale(saleId);
            ProductInStore p = ProductManager.getInstance().getProductInStore(s.ProductInStoreId);
            ans = p.product.name;
            return ans;
        }





    }
}
