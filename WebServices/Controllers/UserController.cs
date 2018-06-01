using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using wsep182.Domain;
using wsep182.services;

namespace WebService.Controllers
{
    public class UserController : ApiController
    {
        [Route("api/user/register")]
        [HttpGet]
        public string register(String Username, String Password)
        {
            try
            {
                User session = hashServices.getUserByHash(System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value);
                int ans = userServices.getInstance().register(session, Username, Password);
                switch (ans)
                {
                    case 0:
                        return "user successfuly added";
                    case -1:
                        return "error: username is not entered";
                    case -2:
                        return "error: password is not entered";
                    case -3:
                        return "error: username contains spaces";
                    case -4:
                        return "error: username allready exist in the system";
                    case -5:
                        return "error: you are allready logged in";
                }
                return "server error: not suppose to happend";
            }
            catch(Exception e)
            {
                return "could not connect to the Database, please try again later.";
            }
        }


        [Route("api/user/viewProductsInStore")]
        [HttpGet]
        public HttpResponseMessage viewProductsInStore(int storeId)
        {
            LinkedList<ProductInStore> pis = userServices.getInstance().viewProductsInStore(storeId);
            HttpResponseMessage response;
            try
            {
                if (pis == null)
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound, "Store is not exist");
                    return response;
                }
                response = Request.CreateResponse(HttpStatusCode.OK, pis);
                return response;
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "could not connect to the Database, please try again later.");
                return response;
            }
        }

        [Route("api/user/viewProductsInStores")]
        [HttpGet]
        public HttpResponseMessage viewProductsInStores()
        {
            try
            {
                LinkedList<ProductInStore> pis = userServices.getInstance().viewProductsInStores();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, pis);
                return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response;
                response = Request.CreateResponse(HttpStatusCode.NotFound, "could not connect to the Database, please try again later.");
                return response;
            }
        }

        [Route("api/user/viewAllSales")]
        [HttpGet]
        public HttpResponseMessage viewAllSales()
        {
            try {
            LinkedList<Sale> pis = userServices.getInstance().viewAllSales();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, pis);
            return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response;
                response = Request.CreateResponse(HttpStatusCode.NotFound, "could not connect to the Database, please try again later.");
                return response;
            }
        }

        [Route("api/user/viewSaleById")]
        [HttpGet]
        public HttpResponseMessage viewSaleById(int saleId)
        {
            try {
            Sale sale = userServices.getInstance().viewSalesBySaleId(saleId);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, sale);
            return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response;
                response = Request.CreateResponse(HttpStatusCode.NotFound, "could not connect to the Database, please try again later.");
                return response;
            }
        }


        [Route("api/user/viewStores")]
        [HttpGet]
        public HttpResponseMessage viewStores()
        {
            try {
            LinkedList<Store> stores = userServices.getInstance().viewStores();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, stores);
            return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response;
                response = Request.CreateResponse(HttpStatusCode.NotFound, "could not connect to the Database, please try again later.");
                return response;
            }
        }

        [Route("api/user/login")]
        [HttpGet]
        public Object login(String Username, String Password)
        {
            try {
            User session = userServices.getInstance().startSession();
            int ans = userServices.getInstance().login(session, Username, Password);
            switch (ans)
            {
                case 0:
                    String hash = System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value;
                    hashServices.configureUser(hash, session);
                    return "user successfuly logged in";
                case -1:
                    return "error: username not exist";
                case -2:
                    return "error: wrong password";
                case -3:
                    return "error: user is removed";
                case -4:
                    return "error: you are allready logged in";
            }
            return "server error: not suppose to happend";
            }
            catch (Exception e)
            {
                return "could not connect to the Database, please try again later.";
            }
        }


        [Route("api/user/generateHash")]
        [HttpGet]
        public Object generateHash()
        {
            String hash = hashServices.generateID();
            User user = new User(hash,hash);
            hashServices.configureUser(hash, user);
            return hash;
        }

        [Route("api/user/removeUser")]
        [HttpGet]
        public string removeUser(String userDeleted)
        {
            if (System.Web.HttpContext.Current.Request.Cookies["HashCode"] == null)
            {
                return "Not logged in";
            }
            try {
            User session = hashServices.getUserByHash(System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value);
            int ans = userServices.getInstance().removeUser(session, userDeleted);
            switch(ans)
            {
                case 0:
                    return "user has been removed succesfully";
                case -1:
                    return "you are not admin";
                case -2:
                    return "the user you want to remove is not exist";
                case -3:
                    return "the user you want to remove is allready removed";
                case -4:
                    return "you are not allowed to remove yourself";
                case -5:
                    return "the user you want to remove have raffle sale";
                case -6:
                    return "the user you want to remove is a owner or creator of other stores";
            }
            return "not implemented";
            }
            catch (Exception e)
            {
               return "could not connect to the Database, please try again later.";
            }
        }

        [Route("api/user/setAmountPolicyOnProduct")]
        [HttpGet]
        public String setAmountPolicyOnProduct(string productName, int minAmount, int maxAmount)
        {
            if (System.Web.HttpContext.Current.Request.Cookies["HashCode"] == null)
            {
                return "Not logged in";
            }
            try {
            User session = hashServices.getUserByHash(System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value);
            int ans = userServices.getInstance().setAmountPolicyOnProduct(session,productName,minAmount,maxAmount);
            if(ans>0)
                return "Policy added successfully";
            return "Policy failed";
            }
            catch (Exception e)
            {
                return "could not connect to the Database, please try again later.";
            }
        }

        [Route("api/user/setNoCouponsPolicyOnProduct")]
        [HttpGet]
        public String setNoCouponsPolicyOnProduct(string productName)
        {
            if (System.Web.HttpContext.Current.Request.Cookies["HashCode"] == null)
            {
                return "Not logged in";
            }
            try {
            User session = hashServices.getUserByHash(System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value);
            int ans = userServices.getInstance().setNoCouponsPolicyOnProduct(session, productName);
            if (ans > 0)
                return "Policy added successfully";
            return "Policy failed";
            }
            catch (Exception e)
            {
                return "could not connect to the Database, please try again later.";
            }
        }



        [Route("api/user/setAmountPolicyOnProduct")]
        [HttpGet]
        public String setNoDiscountPolicyOnProduct(string productName)
        {
            if (System.Web.HttpContext.Current.Request.Cookies["HashCode"] == null)
            {
                return "Not logged in";
            }
            try {
            User session = hashServices.getUserByHash(System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value);
            int ans = userServices.getInstance().setNoDiscountPolicyOnProduct(session, productName);
            if (ans > 0)
                return "Policy added successfully";
            return "Policy failed";
            }
            catch (Exception e)
            {
                return "could not connect to the Database, please try again later.";
            }
        }


        [Route("api/user/removeAmountPolicyOnProduct")]
        [HttpGet]
        public String removeAmountPolicyOnProduct(string productName)
        {
            if (System.Web.HttpContext.Current.Request.Cookies["HashCode"] == null)
            {
                return "Not logged in";
            }
            try {
            User session = hashServices.getUserByHash(System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value);
            int ans = userServices.getInstance().removeAmountPolicyOnProduct(session, productName);
            if (ans > 0)
                return "Policy removed successfully";
            return "Policy failed";
            }
            catch (Exception e)
            {
                return "could not connect to the Database, please try again later.";
            }
        }

        [Route("api/user/removeNoDiscountPolicyOnProduct")]
        [HttpGet]
        public String removeNoDiscountPolicyOnProduct(string productName)
        {
            if (System.Web.HttpContext.Current.Request.Cookies["HashCode"] == null)
            {
                return "Not logged in";
            }
            try {
            User session = hashServices.getUserByHash(System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value);
            int ans = userServices.getInstance().removeNoDiscountPolicyOnProduct(session, productName);
            if (ans > 0)
                return "Policy removed successfully";
            return "Policy failed";
            }
            catch (Exception e)
            {
                return "could not connect to the Database, please try again later.";
            }
        }

        [Route("api/user/removeNoCouponsPolicyOnProduct")]
        [HttpGet]
        public String removeNoCouponsPolicyOnProduct(string productName)
        {
            if (System.Web.HttpContext.Current.Request.Cookies["HashCode"] == null)
            {
                return "Not logged in";
            }
            try {
            User session = hashServices.getUserByHash(System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value);
            int ans = userServices.getInstance().removeNoCouponsPolicyOnProduct(session, productName);
            if (ans > 0)
                return "Policy removed successfully";
            return "Policy failed";
            }
            catch (Exception e)
            {
                return "could not connect to the Database, please try again later.";
            }
        }



        [Route("api/user/getAllStoresUnderUser")]
        [HttpGet]
        public HttpResponseMessage getAllStoreRolesOfAUser()
        {
            HttpResponseMessage response;
            try {
                User session = hashServices.getUserByHash(System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value);
                response = Request.CreateResponse(HttpStatusCode.OK, userServices.getInstance().getAllStoreRolesOfAUser(session, session.getUserName()));
                return response;
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "could not connect to the Database, please try again later.");
                return response;
            }
        }

        [Route("api/user/getPremissionsOfAManager")]
        [HttpGet]
        public HttpResponseMessage getPremissionsOfAManager(string username, int storeId)
        {
            HttpResponseMessage response;
            try {
                User session = hashServices.getUserByHash(System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value);
                response = Request.CreateResponse(HttpStatusCode.OK, userServices.getInstance().getPremissions(session, username, storeId));
                return response;
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "could not connect to the Database, please try again later.");
                return response;
            }
        }

        [Route("api/user/getPremissionsOfAManager")]
        [HttpGet]
        public HttpResponseMessage getPremissionsOfAManager(int storeId)
        {
            HttpResponseMessage response;
            try {
            User session = hashServices.getUserByHash(System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value);
            string username = session.getUserName();
            response = Request.CreateResponse(HttpStatusCode.OK, userServices.getInstance().getPremissions(session, username, storeId));
            return response;
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "could not connect to the Database, please try again later.");
                return response;
            }
        }

        [Route("api/user/signUserToNotifications")]
        [HttpGet]
        public HttpResponseMessage signUserToNotifications(int storeId, string notification)
        {
            HttpResponseMessage response;
            if (System.Web.HttpContext.Current.Request.Cookies["HashCode"] == null)
            {
                response = Request.CreateResponse(HttpStatusCode.ExpectationFailed, "Not logged in ");
                return response;
            }
            try
            {
                User session = hashServices.getUserByHash(System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value);
                userServices.getInstance().signUserToNotifications(session, storeId, notification);
                response = Request.CreateResponse(HttpStatusCode.OK,"Notification Updated");
                return response;
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "could not connect to the Database, please try again later.");
                return response;
            }
        }



    }
}