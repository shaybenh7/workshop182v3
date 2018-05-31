using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServices.Domain;

namespace wsep182.Domain
{
    public class Purchase 
    {
        private int buyId;
        private int productId;
        private int storeId;
        private String userName;
        private double price;
        private String date;
        private int amount;
        private int typeOfSale;

        public Purchase(int buyId, int productId, int storeId, String userName, double price
            , String date, int amount , int typeOfSale)
        {
            this.buyId = buyId;
            this.productId = productId;
            this.storeId = storeId;
            this.userName = userName;
            this.price = price;
            this.date = date;
            this.amount = amount;
            this.typeOfSale = typeOfSale;
        }

        public int BuyId { get => buyId; set => buyId = value; }
        public int ProductId { get => productId; set => productId = value; }
        public int StoreId { get => storeId; set => storeId = value; }
        public string UserName { get => userName; set => userName = value; }
        public double Price { get => price; set => price = value; }
        public string Date { get => date; set => date = value; }
        public int Amount { get => amount; set => amount = value; }
        public int TypeOfSale { get => typeOfSale; set => typeOfSale = value; }


        //ALERTS ALL LISTENERS, NOT JUST THE OWNERS
        public static void alertOwnersOnPurchase(LinkedList<StoreOwner> so, int productInStoreId, int typeOfSale)
        {
            string inline;
            if (typeOfSale == 1)
                inline = "using Instant Sale";
            else
                inline = "using Raffle Sale";
            foreach (StoreOwner s in so)
            {
                string message = "A user have purchased " + inline + " the product id: " + productInStoreId.ToString() + ", from the store-id: " + s.store.storeId.ToString();
                NotificationPublisher.getInstance().publish(NotificationPublisher.NotificationCategories.Purchase, message, s.store.getStoreId());
                //NotificationManager.getInstance().notifyUser(s.user.userName, message);
            }
        }



    }
}
