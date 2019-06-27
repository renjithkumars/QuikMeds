using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuikMeds.Models;
using System.Net;

namespace QuikMeds.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Add_Product()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Product(QuikMeds.Product products)
        {
            
            if (ModelState.IsValid)
            {
                Product a = new Product
                {
                    
                    PName = products.PName,
                    Brand=products.Brand,
                    UnitPrice=products.UnitPrice,
                    UnitsInStock=products.UnitsInStock,
                    Category=products.Category,
                    Description=products.Description,
                    SID=products.SID,
                    ROL=products.ROL

                };
                _ctx.Products.Add(a);
                _ctx.SaveChanges();


            }
            return View();
        }
        public ActionResult Details()
        {
            var detail = _ctx.Products.ToList();
            return View(detail);
        }
    }
}