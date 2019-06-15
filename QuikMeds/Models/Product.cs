using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuikMeds
{
   public partial class Product
    {
        private CTXEntities _ctx = new CTXEntities();
        public List<Product> All
        {
            get
            {
                return _ctx.Products.ToList<Product>();

            }
        }
    }
}