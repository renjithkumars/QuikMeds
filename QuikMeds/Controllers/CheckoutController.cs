using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuikMeds.Controllers
{
    public class CheckoutController : BaseController
    {


        // GET: Checkout
        public ActionResult Index()
        {
            ViewBag.Cart = _ctx.ShoppingCartDatas.ToList<ShoppingCartData>();
            return View();
        }

        

        [HttpGet]
        public JsonResult UpdateTotal()
        {
            CTXEntities context = new CTXEntities();
            decimal total;
            try
            {

                total = context.ShoppingCartDatas.Select(p => p.UnitPrice * p.Quantity).Sum();
            }
            catch (Exception) { total = 0; }

            return Json(new { d = String.Format("{0:c}", total) }, JsonRequestBehavior.AllowGet);
        }

        


    }
}
