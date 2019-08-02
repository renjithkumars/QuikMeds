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
        public ActionResult Index()
        {

            return View();
        }

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
                    Brand = products.Brand,
                    UnitPrice = products.UnitPrice,
                    UnitsInStock = products.UnitsInStock,
                    Category = products.Category,
                    Description = products.Description,
                    SID = products.SID,
                    ROL = products.ROL

                };
                try
                {
                    _ctx.Products.Add(a);
                    _ctx.SaveChanges();

                }
                catch (Exception) { }

            }
            return View("Index");
        }
        public ActionResult Details()
        {
            var detail = _ctx.Products.ToList();
            return View(detail);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var detail = _ctx.Products.Where(p => p.PID == id).Single();
            return View(detail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuikMeds.Product products)
        {

            if (ModelState.IsValid)
            {
                Product b = new Product
                {
                    PID = products.PID,
                    PName = products.PName,
                    Brand = products.Brand,
                    UnitPrice = products.UnitPrice,
                    UnitsInStock = products.UnitsInStock,
                    Category = products.Category,
                    Description = products.Description,
                    SID = products.SID,
                    ROL = products.ROL

                };
                try
                {
                    _ctx.Entry(b).State = System.Data.Entity.EntityState.Modified;
                    _ctx.SaveChanges();

                }
                catch (Exception) { RedirectToAction("Error"); }


            }
            return View("Index");
        }
        public ActionResult Error()
        {

            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var detail = _ctx.Products.Where(p => p.PID == id).Single();
            return View(detail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(QuikMeds.Product products)
        {
            if (ModelState.IsValid)
            {
                Product c = new Product
                {

                    PName = products.PName,
                    Brand = products.Brand,
                    UnitPrice = products.UnitPrice,
                    UnitsInStock = products.UnitsInStock,
                    Category = products.Category,
                    Description = products.Description,
                    SID = products.SID,
                    ROL = products.ROL

                };
                _ctx.Entry(c).State = System.Data.Entity.EntityState.Deleted;
                _ctx.SaveChanges();
            }
            return View("Index");
        }

    }
}