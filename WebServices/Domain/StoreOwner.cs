﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class StoreOwner : StoreRole
    {
        public StoreOwner(User u, Store s) : base(u,s)
        {
            type = "Owner";
        }
        public StoreOwner(User u, Store s,String addedBy) : base(u, s, addedBy)
        {
            type = "Owner";
        }
        public StoreOwner(User u, Store s, String addedBy,String timeAdded) : base(u, s, addedBy, timeAdded)
        {
            type = "Owner";
        }
    }
}
