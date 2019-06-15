using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuikMeds
{
    public partial class Supplier
    {
        private CTXEntities _ctx = new CTXEntities();

        public IEnumerable<Supplier> All
        {
            get
            {
                return _ctx.Suppliers;
            }
        }
    }
}