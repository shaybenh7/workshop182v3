using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace wsep182.Domain
{
    public class RaffleSalesArchive
    {
        private LinkedList<RaffleSale> raffleSales;
        private static RaffleSalesArchive instance;
        System.Timers.Timer RaffelCollector;

        private RaffleSalesArchive()
        {
            raffleSales = new LinkedList<RaffleSale>();
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
                    NotificationManager.getInstance().notifyUser(rs.UserName, "the Raffle sale " + rs.SaleId + " has been canceled");
                    raffleSalesToRemove.AddLast(rs);
                }
            }
            foreach (RaffleSale rs in raffleSalesToRemove)
            {
                raffleSales.Remove(rs);
            }
        }


        public static RaffleSalesArchive getInstance()
        {
            if (instance == null)
                instance = new RaffleSalesArchive();
            return instance;
        }
        public static void restartInstance()
        {
            instance = new RaffleSalesArchive();
        }

        public Boolean addRaffleSale(int saleId, String userName, double offer, String dueDate)
        {
            RaffleSale toAdd = new RaffleSale(saleId, userName, offer, dueDate);
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
            Sale s = SalesArchive.getInstance().getSale(saleId);
            if (s == null)
                return -2;
            ProductInStore p = ProductArchive.getInstance().getProductInStore(s.ProductInStoreId);
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
            Sale s = SalesArchive.getInstance().getSale(saleId);
            ProductInStore p = ProductArchive.getInstance().getProductInStore(s.ProductInStoreId);
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
                        NotificationManager.getInstance().notifyUser(r.UserName, "YOU WON THE RAFFLE SALE ON PRODUCT: " + getProductNameFromSaleId(r.SaleId));
                        winnerS = r;
                        break;
                    }
                    else
                    {
                        index += (int)r.Offer;
                    }
                }
                if (winnerS != null) { 
                    raffleSales.Remove(winnerS);
                    relevant.Remove(winnerS);
                }
                foreach (RaffleSale r in relevant)
                {
                    NotificationManager.getInstance().notifyUser(r.UserName, "YOU LOST THE RAFFLE SALE ON PRODUCT: " + getProductNameFromSaleId(r.SaleId));
                    raffleSales.Remove(r);
                }


            }
        }

        private string getProductNameFromSaleId(int saleId)
        {
            string ans = "";
            Sale s = SalesArchive.getInstance().getSale(saleId);
            ProductInStore p = ProductArchive.getInstance().getProductInStore(s.ProductInStoreId);
            ans = p.product.name;
            return ans;
        }





    }
}
