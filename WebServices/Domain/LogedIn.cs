using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class LogedIn : UserState
    {

        public override Boolean isLogedIn()
        {
            return true;
        }

        public override LinkedList<Purchase> viewStoreHistory(Store store, User session)
        {
            StoreRole sR = StoreManagement.getInstance().getStoreRole(store, session);
            return sR.viewPurchasesHistory(session, store);
        }
    }
}
