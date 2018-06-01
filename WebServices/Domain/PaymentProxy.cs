using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServices.Domain;
using wsep182.Domain;

namespace WebServices.Domain
{
    public class PaymentProxy : PaymentInterface
    {
        PaymentInterface impl;

        public PaymentProxy()
        {
            impl = PaymentSystem.getInstance();
        }

         Boolean PaymentInterface.payForProduct(string creditCard, User session, UserCart product)
        {
            if(impl!=null)
                return impl.payForProduct(creditCard, session, product);

            return false;
        }

    }
}