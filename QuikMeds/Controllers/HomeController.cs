using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuikMeds.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {

            List<Product> products = _ctx.Products.ToList<Product>();
            ViewBag.Products = products;
            return View();
        }
        public ActionResult Index1()
        {
            List<Product> products = _ctx.Products.ToList<Product>();
            ViewBag.Products = products;
            return View();

        }


        public ActionResult Category(string catName)
        {
            List<Product> products;
            if (catName == "")
            {
                products = _ctx.Products.ToList<Product>();
                try
                {
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "EmployeeInfo", "Create"));
                }
            }
            else
            {
                products = _ctx.Products.Where(p => p.Category == catName).ToList<Product>();
            }
            ViewBag.Products = products;
            return View("Index1");
        }

        public ActionResult Contents(string discription)
        {
            List<Product> products;
            if (discription == "")
            {
                products = _ctx.Products.ToList<Product>();
            }
            else
            {
                products = _ctx.Products.Where(p => p.Description == discription).ToList<Product>();
            }
            ViewBag.Products = products;
            return View("Index");
        }

        public ActionResult Suppliers()
        {
            List<Supplier> suppliers = _ctx.Suppliers.ToList<Supplier>();
            ViewBag.Suppliers = suppliers;
            return View();
        }

        public ActionResult AddToCart(int id)
        {
            addToCart(id);
            return RedirectToAction("Index");
        }

        private void addToCart(int pId)
        {
            // check if product is valid
            Product product = _ctx.Products.FirstOrDefault(p => p.PID == pId);
            if (product != null && product.UnitsInStock > 0)
            {
                // check if product already existed
                ShoppingCartData cart = _ctx.ShoppingCartDatas.FirstOrDefault(c => c.PID == pId);
                if (cart != null)
                {
                    cart.Quantity++;
                }
                else
                {

                    cart = new ShoppingCartData
                    {
                        PName = product.PName,
                        PID = product.PID,
                        UnitPrice = product.UnitPrice,
                        Quantity = 1
                    };

                    _ctx.ShoppingCartDatas.Add(cart);
                }
                product.UnitsInStock--;
                _ctx.SaveChanges();
            }
        }

        public ActionResult Informations()
        {
            List<Information> product_details = _ctx.Information.ToList<Information>();
            ViewBag.Products = product_details;
            return View();
            
        }
        public ActionResult Information(string discription)
        {
            List<Information> product_details;
            if (discription == "")
            {
               product_details = _ctx.Information.ToList<Information>();
            }
            else
            {
               product_details = _ctx.Information.Where(i => i.Category==discription).ToList<Information>();
            }
            ViewBag.Products = product_details;
            return View("Informations");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}