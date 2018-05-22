﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wsep182.services;
using wsep182.Domain;

namespace WebServices.Controllers
{
    public class CheckLoggedIn : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (System.Web.HttpContext.Current.Request.Cookies["HashCode"] != null)
            {
                String hash = System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value;
                if (hash == null || hashServices.getUserByHash(hash) == null || !hashServices.getUserByHash(hash).getState().isLogedIn())
                {
                    filterContext.Result = new RedirectResult(string.Format("/error/"));
                }
            }
            else filterContext.Result = new RedirectResult(string.Format("/error/"));
        }
    }

    public class CheckAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (System.Web.HttpContext.Current.Request.Cookies["HashCode"] != null)
            {

                String hash = System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value;
                if (hash == null || hashServices.getUserByHash(hash) == null || !(hashServices.getUserByHash(hash).getState() is Admin))
                {
                    filterContext.Result = new RedirectResult(string.Format("/error/"));
                }
            }
        }
    }
    public class PagesController : Controller
    {

        public PagesController()
        {
            ViewData["numberOfProductsInCart"] = 10; 
        }

        // GET: Pages
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        public ActionResult AllProducts()
        {
            return View();
        }

        public ActionResult AllStores()
        {
            return View();
        }

        public ActionResult viewInstantSale(int saleId)
        {
            ViewData["saleId"] = saleId;
            return View();
        }

        public ActionResult viewRaffleSale(int saleId)
        {
            ViewData["saleId"] = saleId;
            return View();
        }

        public ActionResult shoppingCart()
        {
            return View();
        }

        public ActionResult viewStore(int storeId)
        {
            ViewData["storeId"] = storeId;
            return View();
        }

        [CheckLoggedIn]
        public ActionResult MyStores()
        {
            return View();
        }

        [CheckAdmin]
        public ActionResult Admin()
        {
            
            return View();
        }

        public ActionResult register()
        {
            return View();
        }

        public ActionResult error()
        {
            return View();
        }

        public ActionResult test()
        {
            return View();
        }
    }
}