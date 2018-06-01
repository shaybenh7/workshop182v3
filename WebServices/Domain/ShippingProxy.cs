using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServices.Domain;
using wsep182.Domain;

namespace WebServices.Domain
{
    public class ShippingProxy : ShippingInterface
    {

        ShippingInterface impl;

        public ShippingProxy()
        {
            impl = ShippingSystem.getInstance();
        }

        public Boolean sendShippingRequest(User session, string country, string adress, string creditCard)
        {
            if (impl != null)
                return impl.sendShippingRequest(session, country, adress, creditCard);
            return false;
        }
    }
}