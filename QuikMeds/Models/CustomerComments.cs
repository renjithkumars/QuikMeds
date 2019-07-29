using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuikMeds
{
    public partial class CustomerComment
    {
        private CTXEntities _ctx = new CTXEntities();
        public List<CustomerComment> All
        {
            get
            {
                return _ctx.CustomerComments.ToList<CustomerComment>();

            }
        }
    }
}