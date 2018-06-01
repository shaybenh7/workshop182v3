using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wsep182.Domain;

namespace WebServices.Domain
{
    public interface ShippingInterface
    {
        Boolean sendShippingRequest(User session, string country, string adress, string creditCard);
    }
}
