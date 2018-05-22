using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using wsep182.Domain;
using wsep182.services;

namespace WebService.Controllers
{
    public class SellController : ApiController
    {
        [Route("api/store/viewSalesByProductInStoreId")]
        [HttpGet]
        public HttpResponseMessage viewSalesByProductInStoreId(int ProductInStoreId)
        {
            User session = hashServices.getUserByHash(System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value);
            Object sales = sellServices.getInstance().viewSalesByProductInStoreId(ProductInStoreId);
            HttpResponseMessage response;
            if (sales != null)
                response = Request.CreateResponse(HttpStatusCode.OK, sellServices.getInstance().viewSalesByProductInStoreId(ProductInStoreId));
            else
                response = Request.CreateResponse(HttpStatusCode.OK, "Error: No sales found for the entered product id");
            return response;
        }


        [Route("api/store/addProductToCart")]
        [HttpPut]
        public HttpResponseMessage addProductToCart(int saleId, int amount)
        {
            //User session = hashServices.getUserByHash(System.Web.HttpContext.Current.Request.Cookies["Session"].Value);
            User session = hashServices.getUserByHash(System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value);

            
            /* Confimation = 1
             * Errors:
             * -1 = user is null (should not ever happen)
             * -2 = amount can't be zero or lower
             * -3 = saleId entered doesn't exist
             * -4 = the date for the sale is no longer valid
             * -5 = trying to add a sale with type different from regular sale type
             * -6 = amount is bigger than the amount that exist in stock
             * -7 = amount is bigger than the amount currently up for sale
             */
            int added = sellServices.getInstance().addProductToCart(session, saleId, amount);
            HttpResponseMessage response;
            switch (added)
            {
                case 1:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Product was added successfully!");
                    break;
                case -1:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: user is not valid!");
                    break;
                case -2:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: amount can't be zero or lower!");
                    break;
                case -3:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: The sale id does not exist!");
                    break;
                case -4:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: The sale has ended!");
                    break;
                case -5:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: Trying to add to cart sale different than immediate sale!");
                    break;
                case -6:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: Amount in bigger than currently exist in stock!");
                    break;
                case -7:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: Amount in bigger than currently available for purchase!");
                    break;
                default:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: Unkown error!");
                    break;
            }
            return response;
        }

        [Route("api/store/addRaffleProductToCart")]
        [HttpGet]
        public HttpResponseMessage addRaffleProductToCart(int saleId, double offer)
        {
            User session = hashServices.getUserByHash(System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value);
            /* Confimation = 1
             * Errors:
             * -1 = user is null (should not ever happen)
             * -2 = offer can't be zero or lower
             * -3 = sale id entered does not exist
             * -4 = sale is not of type raffle
             * -5 = the date for the sale is no longer valid
             * -6 = already have an instance of the raffle sale in the cart
             * -7 = cannot add a raffle sale to cart while on guest mode
             * -8 = offer is bigger than remaining sum to pay
             */
            int added = sellServices.getInstance().addRaffleProductToCart(session, saleId, offer);
            HttpResponseMessage response;
            switch (added)
            {
                case 1:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Product was added successfully!");
                    break;
                case -1:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: user is not valid!");
                    break;
                case -2:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error:  offer can't be zero or lower!");
                    break;
                case -3:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: The sale id does not exist!");
                    break;
                case -4:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: sale is not of type raffle sale!");
                    break;
                case -5:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error:  The sale has ended!");
                    break;
                case -6:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: You Already have an instance of the raffle sale in the cart!");
                    break;
                case -7:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: Cannot add a raffle sale to cart while on guest mode!");
                    break;
                case -8:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: Bid is bigger than remaining sum to pay!");
                    break;
                default:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: Unkown error!");
                    break;
            }
            return response;
        }

        [Route("api/sell/viewCart")]
        [HttpGet]
        public HttpResponseMessage viewCart()
        {
            User session = hashServices.getUserByHash(System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value);
            Object cart = sellServices.getInstance().getShoppingCartBeforeCheckout(session);
            HttpResponseMessage response;
            if (cart == null)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, "Errror: user is not valid!");
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, cart);
            }
            return response;
        }

        [Route("api/sell/editCart")]
        [HttpPut]
        public HttpResponseMessage editCart(int saleId, int newAmount)
        {
            string hash = System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value;
            User session = hashServices.getUserByHash(hash);
            /* Confimation = 1
             * Errors:
             * -1 = user is null (should not ever happen)
             * -2 = the sale id does not exist
             * -3 = trying to edit amount of a raffle sale
             * -4 = new amount is bigger than currently up for sale
             * -5 = new amount is bigger than currently exist in stock
             * -6 = new amount can't be zero or lower
             * -7 = trying to edit amount of product that does not exist in cart
             */
            int edited = sellServices.getInstance().editCart(session, saleId, newAmount);
            HttpResponseMessage response;
            switch (edited)
            {
                case 1:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Product was edited successfully!");
                    break;
                case -1:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: user is not valid!");
                    break;
                case -2:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error:The sale id does not exist!");
                    break;
                case -3:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: Trying to edit amount of a raffle sale!");
                    break;
                case -4:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: New amount is bigger than currently up for sale!");
                    break;
                case -5:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: New amount is bigger than currently exist in stock!");
                    break;
                case -6:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: New amount can't be zero or lower!");
                    break;
                case -7:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: Trying to edit amount of product that does not exist in cart!");
                    break;
                default:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: Unkown error!");
                    break;
            }
            return response;
        }

        [Route("api/sell/removeFromCart")]
        [HttpGet]
        public HttpResponseMessage removeFromCart(int saleId)
        {
            User session = hashServices.getUserByHash(System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value);
            /* Confimation = 1
             * Errors:
             * -1 = user is null (should not ever happen)
             * -2 = the sale id does not exist
             * -3 = trying to remove a product that does not exist in the cart
             */
            int removed = sellServices.getInstance().removeFromCart(session, saleId);
            HttpResponseMessage response;
            switch (removed)
            {
                case 1:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Product was Removed successfully!");
                    break;
                case -1:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: user is not valid!");
                    break;
                case -2:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error:The sale id does not exist!");
                    break;
                case -3:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: Trying to remove a product that does not exist in the cart!");
                    break;
                default:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: Unkown error!");
                    break;
            }
            return response;
        }

        [Route("api/sell/buyProductsInCart")]
        [HttpGet]
        public HttpResponseMessage buyProductsInCart(string country, string address,string creditcard)
        {
            string hash = System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value;
            User session = hashServices.getUserByHash(hash);
            HttpResponseMessage response;
            int bought = sellServices.getInstance().buyProductsInCart(session, country, address, creditcard);
            /* Confimation = 1
             * Errors:
             * -1 = user is null (should not ever happen)
             * -2 = address or country is empty
             * -3 = the credit card field is empty 
             * -4 = 
             */
            switch (bought)
            {
                case -1:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: user is not valid!");
                    break;
                case -2:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: country and address fields cannot be empty!");
                    break;
                case -3:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: creditcard field is empty!");
                    break;
                case -4:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Warning: some of the products had not been purchased! check your shopping cart");
                    break;
                case 1:
                    response = Request.CreateResponse(HttpStatusCode.OK, "All the items in your cart were purchased!");
                    break;
                default:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: unknown!");
                    break;
            }
            return response;
        }
        [Route("api/sell/checkout")]
        [HttpGet]
        public HttpResponseMessage checkout(string country, string address, string creditcard)
        {
            string hash = System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value;
            User session = hashServices.getUserByHash(hash);
            HttpResponseMessage response;
            Tuple<int, LinkedList<UserCart>> ans = sellServices.getInstance().checkout(session, country, address, creditcard);
            int confimation = ans.Item1;
            /* Confimation = -1 = everything is o.k
             * Errors:
             * -2 = user is null
             * -3 = country or address is empty error
             * * ANY POSITIVE NUMBER IS AN ERROR SPECIFYING THE SALEID THAT FAILED THE CHECKOUT
             */
            switch (confimation)
            {
                case -1:
                    response = Request.CreateResponse(HttpStatusCode.OK, ans.Item2);
                    break;
                case -2:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: user error!");
                    break;
                case -3:
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: country and address fields cannot be empty!");
                    break;
                default: 
                    response = Request.CreateResponse(HttpStatusCode.OK, "Error: item with sale Id "+confimation.ToString()+" does not fulfill its buying policy!");
                    break;
            }

            return response;
        }
        [Route("api/sell/getShoppingCartBeforeCheckout")]
        [HttpGet]
        public HttpResponseMessage getShoppingCartBeforeCheckout()
        {
            string hash = System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value;
            User session = hashServices.getUserByHash(hash);           
            HttpResponseMessage response;
            response = Request.CreateResponse(HttpStatusCode.OK, session.getShoppingCartBeforeCheckout());
            return response;
        }

        [Route("api/sell/applyCoupon")]
        [HttpGet]
        public HttpResponseMessage applyCoupon(string couponId,string country)
        {
            string hash = System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value;
            User session = hashServices.getUserByHash(hash);
            HttpResponseMessage response;
            LinkedList<UserCart> updatedCart = sellServices.getInstance().applyCoupon(session, couponId, country);
            if(updatedCart == null)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, "Error: user error");
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, updatedCart);
            }
            return response;
        }


    }
}