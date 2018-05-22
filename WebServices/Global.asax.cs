using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using wsep182.services;
using wsep182.Domain;

namespace WebServices
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            
            HttpCookie authCookie = Context.Request.Cookies["HashCode"];

            if (authCookie == null || hashServices.getUserByHash(authCookie.Value) == null)
            {
                User u = userServices.getInstance().startSession();
                String hash = hashServices.generateID();
                hashServices.configureUser(hash,u);
                Response.Cookies[""]["HashCode"] = hash;
            }
            
        }
    }
}
