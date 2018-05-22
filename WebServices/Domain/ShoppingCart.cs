using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class ShoppingCart
    {
        LinkedList<UserCart> products;
        LinkedList<string> usedCoupons;
        public ShoppingCart()
        {
            products = new LinkedList<UserCart>();
            usedCoupons = new LinkedList<string>();
        }

        public LinkedList<UserCart> getShoppingCartProducts(User session)
        {
            if (!(session.getState() is Guest))
                 products = UserCartsArchive.getInstance().getUserShoppingCart(session.getUserName());
            updateRegularPricesForCart();
            return products;
        }

        private void updateRegularPricesForCart()
        {
            foreach (UserCart uc in products)
            {
                Sale s = SalesArchive.getInstance().getSale(uc.getSaleId());
                double pricePerUnit = ProductArchive.getInstance().getProductInStore(s.ProductInStoreId).getPrice();
                uc.Price = pricePerUnit * uc.getAmount();
            }
        }

        public LinkedList<UserCart> getShoppingCartBeforeCheckout(User session)
        {
            if (!(session.getState() is Guest))
                products = UserCartsArchive.getInstance().getUserShoppingCart(session.getUserName());

            updateRegularPricesForCart(); // set all the regular prices for the cart - before discount
            foreach(UserCart uc in products)
            {
                Sale sale = SalesArchive.getInstance().getSale(uc.getSaleId());
                LinkedList<Discount> discounts = DiscountsArchive.getInstance().getAllDiscountsById(sale.ProductInStoreId);
                uc.PriceAfterDiscount = uc.Price;
                foreach(Discount d in discounts)
                {
                    if (fulfillTypeOfSaleRestriction(sale.TypeOfSale, d))
                    {
                        double dis = uc.PriceAfterDiscount * (d.Percentage / 100);
                        double updatedPrice = uc.PriceAfterDiscount - dis;
                        uc.PriceAfterDiscount = updatedPrice;
                    }
                }
            }
            return products;
        }

        private Boolean fulfillTypeOfSaleRestriction(int typeOfSale, Discount d)
        {
            string restrictions = d.Restrictions;
            if (!restrictions.Contains("TOS"))
                return true;
            /* STRING SHOULD LOOK LIKE : COUNTRY=ISRAEL,ENGLAND/TOS=1,3
             * or: TOS=1
            */
            string[] afterSplit = restrictions.Split('/');
            for(int i = 0; i < afterSplit.Length; i++)
            {
                string temp = afterSplit[i];
                if (temp.Contains("TOS") && temp.Contains(typeOfSale.ToString()))
                    return true;
            }
            return false;
        }

        public Tuple<int,LinkedList<UserCart>> checkout(User session, string country, string address)
        {
            if (!(session.getState() is Guest))
                products = UserCartsArchive.getInstance().getUserShoppingCart(session.getUserName());

            /*
             *  first we check all the products in the cart fulfill their terms of amount
             * */
            int checkAmountFulfillmentAns = checkAmountFulfillment(country);
            if (checkAmountFulfillmentAns!=-1)
                return Tuple.Create(checkAmountFulfillmentAns,products);

            checkAndUpdateDiscountsByPolicys(country);
            return Tuple.Create(-1,products);
        }

        private void checkAndUpdateDiscountsByPolicys(string country)
        {
            Boolean skip = false;
            foreach (UserCart uc in products)
            {
                Sale s = SalesArchive.getInstance().getSale(uc.getSaleId());
                LinkedList<Discount> relevantDiscounts = DiscountsArchive.getInstance().getAllDiscountsById(s.ProductInStoreId);
                uc.PriceAfterDiscount = uc.Price;
                LinkedList<PurchasePolicy> policys = PurchasePolicyArchive.getInstance().getAllRelevantPolicysForProductInStore(s.ProductInStoreId, country);
                foreach (PurchasePolicy p in policys)
                {
                    if (p.NoDiscount)
                        skip = true;
                }
                if (skip)
                {
                    skip = false;
                    continue;
                }
                    
                foreach (Discount d in relevantDiscounts)
                {
                    checkPolicysAndUpdatePrice(uc, d, country, s.TypeOfSale);
                }
            }
        }

        private void checkPolicysAndUpdatePrice(UserCart uc, Discount d, string country,int typeOfSale)
        {
            string restrictions = d.Restrictions;
            if(restrictions.Equals(""))
                uc.PriceAfterDiscount -= uc.PriceAfterDiscount * (d.Percentage / 100);
            else
            {
                if(restrictions.Contains("TOS") && restrictions.Contains("COUNTRY"))
                {
                    if(restrictions.Contains(typeOfSale.ToString()) && restrictions.Contains(country))
                        uc.PriceAfterDiscount -= uc.PriceAfterDiscount * (d.Percentage / 100);
                }
                else if (restrictions.Contains("TOS"))
                {
                    if(restrictions.Contains(typeOfSale.ToString()))
                        uc.PriceAfterDiscount -= uc.PriceAfterDiscount * (d.Percentage / 100);
                }
                else if (restrictions.Contains("COUNTRY"))
                {
                    if(restrictions.Contains(country))
                        uc.PriceAfterDiscount -= uc.PriceAfterDiscount * (d.Percentage / 100);
                }
            }
        }


        private int checkAmountFulfillment(string country)
        {
            foreach (UserCart uc in products)
            {
                Sale s = SalesArchive.getInstance().getSale(uc.getSaleId());
                ProductInStore theProduct = ProductArchive.getInstance().getProductInStore(s.ProductInStoreId);
                LinkedList<PurchasePolicy> storePolicys = PurchasePolicyArchive.getInstance().getAllStorePolicys(theProduct.store.storeId);
                LinkedList<PurchasePolicy> countrysPolicys = PurchasePolicyArchive.getInstance().getAllCountryPolicys(country,theProduct.store.storeId);
                LinkedList<PurchasePolicy> categorysPolicys = PurchasePolicyArchive.getInstance().getAllCategoryPolicys(theProduct.Category, theProduct.store.storeId);
                LinkedList<PurchasePolicy> productPolicys = PurchasePolicyArchive.getInstance().getAllProductPolicys(theProduct.getProduct().name);
                LinkedList<PurchasePolicy> productInStorePolicys = PurchasePolicyArchive.getInstance().getAllProductInStorePolicys(theProduct.getProductInStoreId());

                int currAmount = uc.getAmount();
                foreach (PurchasePolicy p in storePolicys)
                {
                    if (!p.NoLimit)
                    {
                        if (currAmount < p.MinAmount || currAmount > p.MaxAmount)
                            return uc.getSaleId();
                    }
                }
                foreach (PurchasePolicy p in countrysPolicys)
                {
                    if (!p.NoLimit)
                    {
                        if (currAmount < p.MinAmount || currAmount > p.MaxAmount)
                            return uc.getSaleId();
                    }
                }
                foreach (PurchasePolicy p in categorysPolicys)
                {
                    if (!p.NoLimit)
                    {
                        if (currAmount < p.MinAmount || currAmount > p.MaxAmount)
                            return uc.getSaleId();
                    }
                }
                foreach (PurchasePolicy p in productPolicys)
                {
                    if (!p.NoLimit)
                    {
                        if (currAmount < p.MinAmount || currAmount > p.MaxAmount)
                            return uc.getSaleId();
                    }
                }
                foreach (PurchasePolicy p in productInStorePolicys)
                {
                    if (!p.NoLimit)
                    {
                        if (currAmount < p.MinAmount || currAmount > p.MaxAmount)
                            return uc.getSaleId();
                    }
                }
            }
            return -1;
        }



        

        public int addToCart(User session, int saleId, int amount)
        {
            Sale saleExist = SalesArchive.getInstance().getSale(saleId);
            if (saleExist == null)
            {
                return -3; //-3 = saleId entered doesn't exist
            }
            if (!checkValidDate(saleExist))
            {
                return -4; // -4 = the date for the sale is no longer valid
            }
            if (saleExist.TypeOfSale != 1)
                return -5; //-5 = trying to add a sale with type different from regular sale type
            int amountInStore = ProductArchive.getInstance().getProductInStore(saleExist.ProductInStoreId).getAmount();
            if (amount > amountInStore || amount <= 0)
                return -6; // -6 = amount is bigger than the amount that exist in stock

            int amountForSale = SalesArchive.getInstance().getSale(saleId).Amount;
            if (amount > amountForSale || amount <= 0)
                return -7; //amount is bigger than the amount currently up for sale

            if (!(session.getState() is Guest))
                UserCartsArchive.getInstance().updateUserCarts(session.getUserName(), saleId, amount);

            UserCart toAdd = new UserCart(session.getUserName(), saleId, amount);
            foreach (UserCart c in products)
            {
                if(c.getUserName().Equals(toAdd.getUserName()) && c.getSaleId() == toAdd.getSaleId()){
                    if(c.getAmount() + amount <= amountForSale)
                    {
                        c.setAmount(c.getAmount() + amount);
                        return 1; // OK
                    }
                    return -7;
                }
            }

            products.AddLast(toAdd);
            return 1;
        }

        public int addToCartRaffle(User session, int saleId, double offer)
        {
            Sale sale = SalesArchive.getInstance().getSale(saleId);
            if (sale == null)
                return -3; // sale id entered does not exist
            if (sale.TypeOfSale != 3)
                return -4; // sale is not of type raffle
            if (!checkValidDate(sale))
                return -5; // the date for the sale is no longer valid

            UserCart isExist = UserCartsArchive.getInstance().getUserCart(session.getUserName(), sale.SaleId);
            if (isExist != null)
            {
                return -6; // already have an instance of the raffle sale in the cart
            }
            double remainingSum = getRemainingSumForOffers(sale.SaleId);
            if(offer > remainingSum || offer <= 0)
            {
                return -8; // offer is bigger than remaining sum to pay
            }
            if (!(session.getState() is Guest))
            {
                UserCartsArchive.getInstance().updateUserCarts(session.getUserName(), sale.SaleId, 1, offer);
            }
            else
            {
                return -7; // cannot add a raffle sale to cart while on guest mode
            }

            //UserCart toAdd = UserCartsArchive.getInstance().getUserCart(session.getUserName(), sale.SaleId);
            UserCart toAdd = new UserCart(session.getUserName(), sale.SaleId, 1);
            toAdd.setOffer(offer);
            session.getShoppingCart().AddLast(toAdd);
            return 1;
        }

        public int editCart(User session, int saleId, int newAmount)
        {
            Sale sale = SalesArchive.getInstance().getSale(saleId);
            if (sale == null)
                return -2;

            if (sale.TypeOfSale == 3)
                return -3; // trying to edit amount of a raffle sale

            ProductInStore p = ProductArchive.getInstance().getProductInStore(sale.ProductInStoreId);
            if (newAmount > sale.Amount)
                return -4; // new amount is bigger than currently up for sale
            if (newAmount > p.getAmount())
                return -5; // new amount is bigger than currently exist in stock
            if (newAmount <= 0)
                return -6; // new amount can't be zero or lower

            if (!(session.getState() is Guest))
                UserCartsArchive.getInstance().editUserCarts(session.getUserName(), saleId, newAmount);

            foreach (UserCart product in products)
            {
                if (product.getUserName().Equals(session.getUserName()) && saleId == product.getSaleId())
                {
                    product.setAmount(newAmount);
                    return 1;
                }
            }
            return -7; // trying to edit amount of product that does not exist in cart
        }

        public int removeFromCart(User session, int saleId)
        {
            Sale isExist = SalesArchive.getInstance().getSale(saleId);
            if (isExist == null)
            {
                return -2; // -2 = the sale id does not exist
            }
            if (!(session.getState() is Guest))
            {
                if (!UserCartsArchive.getInstance().removeUserCart(session.getUserName(), saleId))
                    return -3; // trying to remove a product that does not exist in the cart
            }

            foreach(UserCart c in products)
            {
                if (c.getUserName().Equals(session.getUserName()) && c.getSaleId() == saleId)
                {
                    products.Remove(c);
                    return 1;
                }
            }
            return -3;
        }

        public Boolean buyProducts(User session, String creditCard, String couponId)
        {
            LinkedList<UserCart> toDelete = new LinkedList<UserCart>();
            Boolean allBought = true;
            if (creditCard == null || creditCard.Equals(""))
                return false;
            foreach (UserCart product in products)
            {
                if (couponId != null && couponId != "")
                {
                    product.activateCoupon(couponId);
                }
                Sale sale = SalesArchive.getInstance().getSale(product.getSaleId());
                if (sale.TypeOfSale == 1 && checkValidAmount(sale, product) && checkValidDate(sale)) //regular buy
                {
                    if (PaymentSystem.getInstance().payForProduct(creditCard, session, product))
                    {
                        ShippingSystem.getInstance().sendShippingRequest();
                        ProductInStore p = ProductArchive.getInstance().getProductInStore(sale.ProductInStoreId);
                        int productId = p.getProduct().getProductId();
                        int storeId = p.getStore().getStoreId();
                        String userName = session.getUserName();
                        double price = product.updateAndReturnFinalPrice(couponId);
                        DateTime currentDate = DateTime.Today;
                        String date = currentDate.ToString();
                        int amount = product.getAmount();
                        int typeOfSale = sale.TypeOfSale;
                        BuyHistoryArchive.getInstance().addBuyHistory(productId, storeId, userName, price, date, amount,
                            typeOfSale);
                        //products.Remove(product);
                        toDelete.AddLast(product);
                        SalesArchive.getInstance().setNewAmountForSale(product.getSaleId(), sale.Amount - product.getAmount());
                    }
                    else
                    {
                        allBought = false;
                    }
                }
                else if (sale.TypeOfSale == 2) // auction buy
                {}
                else if (sale.TypeOfSale == 3 && checkValidDate(sale)) // raffle buy
                {
                    double offer = product.getOffer();
                    double remainingSum = getRemainingSumForOffers(sale.SaleId);
                    if (offer > remainingSum)
                    {
                        allBought = false;
                    }
                    else
                    {
                        if (RaffleSalesArchive.getInstance().addRaffleSale(sale.SaleId, session.getUserName(), offer, sale.DueDate))
                        {
                            PaymentSystem.getInstance().payForProduct(creditCard, session, product);
                            ProductInStore p = ProductArchive.getInstance().getProductInStore(sale.ProductInStoreId);
                            int productId = p.getProduct().getProductId();
                            int storeId = p.getStore().getStoreId();
                            String userName = session.getUserName();
                            DateTime currentDate = DateTime.Today;
                            String date = currentDate.ToString();
                            int amount = product.getAmount();
                            int typeOfSale = sale.TypeOfSale;
                            BuyHistoryArchive.getInstance().addBuyHistory(productId, storeId, userName, offer, date, amount,
                                typeOfSale);
                            //products.Remove(product);
                            toDelete.AddLast(product);
                        }
                        else
                        {
                            allBought = false;
                        }
                    }
                }
            }
            foreach(UserCart uc in toDelete)
            {
                products.Remove(uc);
            }
            return allBought;
        }


        public int buyProductsInCart(User session, string country, string adress, string creditCard)
        {
            int allBought = 1;
            LinkedList<UserCart> toDelete = new LinkedList<UserCart>();
            if (creditCard == null || creditCard.Equals(""))
                return -2;
            foreach (UserCart product in products)
            {
                Sale sale = SalesArchive.getInstance().getSale(product.getSaleId());
                if (sale.TypeOfSale == 1 && checkValidAmount(sale, product) && checkValidDate(sale)) //regular buy
                {
                    if (PaymentSystem.getInstance().payForProduct(creditCard, session, product))
                    {
                        ShippingSystem.getInstance().sendShippingRequest();
                        ProductInStore p = ProductArchive.getInstance().getProductInStore(sale.ProductInStoreId);
                        int productId = p.getProduct().getProductId();
                        int storeId = p.getStore().getStoreId();
                        String userName = session.getUserName();
                        DateTime currentDate = DateTime.Today;
                        String date = currentDate.ToString();
                        int amount = product.getAmount();
                        int typeOfSale = sale.TypeOfSale;
                        BuyHistoryArchive.getInstance().addBuyHistory(productId, storeId, userName, product.PriceAfterDiscount, date, amount,
                            typeOfSale);
                        toDelete.AddLast(product);
                        SalesArchive.getInstance().setNewAmountForSale(product.getSaleId(), sale.Amount - product.getAmount());
                        alertOwnersOnPurchase(storeArchive.getInstance().getAllOwners(p.store.storeId), p.productInStoreId, 1);
                    }
                    else
                    {
                        allBought = -4;
                    }
                }
                else if (sale.TypeOfSale == 2) // auction buy
                { }
                else if (sale.TypeOfSale == 3 && checkValidDate(sale)) // raffle buy
                {
                    double offer = product.getOffer();
                    double remainingSum = getRemainingSumForOffers(sale.SaleId);
                    if (offer > remainingSum)
                    {
                        allBought = -4;
                    }
                    else
                    {
                        if (PaymentSystem.getInstance().payForProduct(creditCard, session, product))
                        {
                            RaffleSalesArchive.getInstance().addRaffleSale(sale.SaleId, session.getUserName(), offer, sale.DueDate);
                            ProductInStore p = ProductArchive.getInstance().getProductInStore(sale.ProductInStoreId);
                            int productId = p.getProduct().getProductId();
                            int storeId = p.getStore().getStoreId();
                            String userName = session.getUserName();
                            DateTime currentDate = DateTime.Today;
                            String date = currentDate.ToString();
                            int amount = product.getAmount();
                            int typeOfSale = sale.TypeOfSale;
                            BuyHistoryArchive.getInstance().addBuyHistory(productId, storeId, userName, offer, date, amount,
                                typeOfSale);
                            RaffleSalesArchive.getInstance().sendMessageTORaffleWinner(sale.SaleId);
                            SalesArchive.getInstance().setNewAmountForSale(product.getSaleId(), sale.Amount - product.getAmount());
                            alertOwnersOnPurchase(storeArchive.getInstance().getAllOwners(p.store.storeId), p.productInStoreId, 3);
                            toDelete.AddLast(product);
                        }
                        else
                        {
                            allBought = -4;
                        }
                    }
                }
                else
                {
                    return -5; // unknown error - should not happen
                }
            }
            foreach (UserCart uc in toDelete)
            {
                products.Remove(uc);
                if (!(session.getState() is Guest))
                    UserCartsArchive.getInstance().removeUserCart(session.userName,uc.SaleId);
            }
            return allBought;
        }

        public void alertOwnersOnPurchase(LinkedList<StoreOwner> so, int productInStoreId, int typeOfSale)
        {
            string inline;
            if (typeOfSale == 1)
                inline = "using Instant Sale";
            else
                inline = "using Raffle Sale";
            foreach (StoreOwner s in so)
            {
                NotificationManager.getInstance().notifyUser(s.user.userName, "A user have purchased "+inline+" the product id: " + productInStoreId.ToString() + ", from the store-id: "+ s.store.storeId.ToString());
            }
        }

        private Boolean checkValidAmount(Sale sale, UserCart cart)
        {
            if (cart.getAmount() <= sale.Amount)
                return true;
            return false;
        }

        private Boolean checkValidDate(Sale sale)
        {
            DateTime now = DateTime.Now;
            DateTime dueDateTime;
            try
            {
                dueDateTime = DateTime.Parse(sale.DueDate);
            }
            catch (System.FormatException e)
            {
                return false;
            }
            if (DateTime.Compare(DateTime.Now, dueDateTime) > 0)
                return false;
            return true;
        }

        private double getRemainingSumForOffers(int saleId)
        {
            Sale currSale = SalesArchive.getInstance().getSale(saleId);
            double totalPrice = ProductArchive.getInstance().getProductInStore(currSale.ProductInStoreId).getPrice();
            LinkedList<RaffleSale> sales = RaffleSalesArchive.getInstance().getAllRaffleSalesBySaleId(saleId);
            if (sales.Count() == 0)
                return totalPrice;
            else
            {
                foreach (RaffleSale sale in sales)
                {
                    totalPrice -= sale.Offer;
                }
                return totalPrice;
            }
        }

        public LinkedList<UserCart> applyCoupon(User session, string couponId,string country)
        {
            if (couponId == null || couponId.Equals(""))
                return products;
            if (country == null)
                country = "";
            if (usedCoupons.Contains(couponId))
                return products;
            Coupon coupon = CouponsArchive.getInstance().getCoupon(couponId);
            if (coupon == null)
                return products;
            Boolean skip = false;
            foreach (UserCart uc in products)
            {
                Sale s = SalesArchive.getInstance().getSale(uc.getSaleId());
                LinkedList<Coupon> relevantCoupons = CouponsArchive.getInstance().getAllCouponsById(s.ProductInStoreId);
                LinkedList<PurchasePolicy> policys = PurchasePolicyArchive.getInstance().getAllRelevantPolicysForProductInStore(s.ProductInStoreId, country);
                foreach (PurchasePolicy p in policys)
                {
                    if (p.NoCoupons)
                        skip = true;
                }
                if (skip)
                {
                    skip = false;
                    continue;
                }
                if (relevantCoupons.Contains(coupon))
                    checkAndUpdateCouponByPolicy(uc, coupon, country, s.TypeOfSale);
            }
            usedCoupons.AddLast(couponId);
            return products;
        }

        public void checkAndUpdateCouponByPolicy(UserCart uc, Coupon c, string country, int typeOfSale)
        {
            if (DateTime.Compare(DateTime.Parse(c.DueDate), DateTime.Now) < 0)
                return;
            string restrictions = c.Restrictions;
            if (restrictions.Equals(""))
                uc.PriceAfterDiscount -= uc.PriceAfterDiscount * (c.Percentage / 100);
            else
            {
                if (restrictions.Contains("TOS") && restrictions.Contains("COUNTRY"))
                {
                    if (restrictions.Contains(typeOfSale.ToString()) && restrictions.Contains(country))
                        uc.PriceAfterDiscount -= uc.PriceAfterDiscount * (c.Percentage / 100);
                }
                else if (restrictions.Contains("TOS"))
                {
                    if (restrictions.Contains(typeOfSale.ToString()))
                        uc.PriceAfterDiscount -= uc.PriceAfterDiscount * (c.Percentage / 100);
                }
                else if (restrictions.Contains("COUNTRY"))
                {
                    if (restrictions.Contains(country))
                        uc.PriceAfterDiscount -= uc.PriceAfterDiscount * (c.Percentage / 100);
                }
            }
        }




    }
}
