using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuikMeds
{
    public partial class Information
    {
        private CTXEntities _ctx = new CTXEntities();
        public List<Information> All
        {
            get
            {
                return _ctx.Information.ToList<Information>();

            }
        }
    }
}