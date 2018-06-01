using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServices.Domain;

namespace wsep182.Domain
{
    public class PaymentSystem : PaymentInterface
    {
        private static PaymentSystem instance;

        private PaymentSystem()
        {}
        public static PaymentSystem getInstance()
        {
            if (instance == null)
                instance = new PaymentSystem();
            return instance;
        }
        public Boolean payForProduct(string creditCard,User session,UserCart product)
        {
            if (creditCard==null||creditCard == "" || session == null || product == null)
                return false;
            return true;
        }

    }
}
