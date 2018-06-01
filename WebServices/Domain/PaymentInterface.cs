using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wsep182.Domain;

namespace WebServices.Domain
{
    public interface PaymentInterface
    {
        Boolean payForProduct(string creditCard, User session, UserCart product);
    }
}
