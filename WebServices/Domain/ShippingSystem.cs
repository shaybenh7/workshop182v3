using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class ShippingSystem
    {
        private static ShippingSystem instance;

        private ShippingSystem()
        {}
        public static ShippingSystem getInstance()
        {
            if (instance == null)
                instance = new ShippingSystem();
            return instance;
        }
        public Boolean sendShippingRequest()
        {
            return false;
        }
        public Boolean sendShippingRequest(User session, string country, string adress, string creditCard)
        {
            if (session == null || country == null || adress == null || creditCard == null || country == "" || adress == "" || creditCard == "")
                return false;
            return true;
        }
    }
}
