using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wsep182.services;
using wsep182.Domain;


namespace WebServices.Views.Pages
{
    public partial class MyStores : System.Web.Mvc.ViewPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Request.Cookies["HashCode"] != null)
            {
                User u = hashServices.getUserByHash(System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value);
                if (u != null && u.getState() is Admin)
                {
                    productOptionForAddCopun.Visible = true;
                    PlaceHolder2.Visible = true;
                }
            }

        }
    }
}