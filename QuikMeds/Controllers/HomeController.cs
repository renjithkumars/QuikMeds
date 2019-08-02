using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace QuikMeds.Controllers
{
    public  class HomeController : BaseController
    {
        
        public ActionResult Index()
        {
            ViewBag.Products = getdetails();
            string a = User.Identity.GetUserName();
            var patient = _ctx.Patients.Where(x => x.Email == a).ToList();
            ViewBag.Patient = patient;
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }
        public ActionResult Index1()
        {

            ViewBag.Products = getdetails();
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");

        }


        public ActionResult Category(string catName)
        {
            List<Product> products;

            if (catName == "")
            {
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
                products = _ctx.Products.Where(p => p.Category == catName).OrderBy(p => p.Description).ToList<Product>();

            }
            ViewBag.Products = products;
            if (Request.IsAuthenticated)
            {
                return View("Index1");
            }
            return RedirectToAction("Login", "Account");
            
        }

        public ActionResult Contents(string discription)
        {
            List<Product> products;
            if (discription == "")
            {

                return RedirectToAction("Index");
            }
            else
            {
                string a = User.Identity.GetUserName();
                products = _ctx.Products.Where(p => p.Description == discription).ToList<Product>();
                var patient = _ctx.Patients.Where(x => x.Email == a).ToList();
                ViewBag.Patient = patient;
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
                product_details = _ctx.Information.Where(i => i.Category == discription).ToList<Information>();
            }
            ViewBag.Products = product_details;
            return View("Informations");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult comments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.ProductId = id.Value;

            List<CustomerComment> comments = _ctx.CustomerComments.Where(c => c.PID == id).ToList<CustomerComment>();
            ViewBag.Comments = comments;

            List<Product> productName = _ctx.Products.Where(p => p.PID == id).ToList<Product>();
            ViewBag.productName = productName;

            List<CustomerComment> ratings = _ctx.CustomerComments.Where(c => c.PID == id).ToList<CustomerComment>();
            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(c => c.Rating.Value);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
                return View();
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FormCollection form)
        {
            var comment = form["Comment"].ToString();
            var articleId = int.Parse(form["ProductId"]);
            var rating = int.Parse(form["Rating"]);
            var email = form["Email"].ToString();

            CustomerComment comments = new CustomerComment()
            {
                PID = articleId,
                Comments = comment,
                Rating = rating,
                Email = email,
                ThisDateTime = DateTime.Now
            };

            _ctx.CustomerComments.Add(comments);
            _ctx.SaveChanges();

            return RedirectToAction("comments", "Home", new { id = articleId });
        }

        public ActionResult Search(string search)
        {
            List<Product> sub;
            List<Product> product = _ctx.Products.Where(p => p.PName.StartsWith(search)).ToList<Product>();
            ViewBag.Products = product;
            sub = _ctx.Products.Where(s=>s.Category=="").ToList<Product>();
            ViewBag.substitute = sub;
            return View();

        }

        public ActionResult Substitute(string category,string content,string PName)
        {
           
            List<Product> product = _ctx.Products.Where(p => p.PName==PName).ToList<Product>();
      
             List <Product> substitute=_ctx.Products.Except(_ctx.Products.Where(p => p.PName == PName))
                                         .Where(p=>p.Category==category && p.Description==content ).ToList<Product>();
            ViewBag.substitute = substitute;
            ViewBag.Products = product;

            return View("Search");
        }
         public ActionResult Substitute1(string category,string PName)
        {
           
            List<Product> product = _ctx.Products.Where(p => p.PName==PName).ToList<Product>();
      
             List <Product> substitute=_ctx.Products.Except(_ctx.Products.Where(p => p.PName == PName))
                                         .Where(p=>p.Category==category ).ToList<Product>();
            ViewBag.substitute = substitute;
            ViewBag.Products = product;

            return View("Search");
        }


        public static List<Product> getdetails()
        {
            CTXEntities _ctx = new CTXEntities();
            List<Product> products = _ctx.Products.OrderBy(p => p.Category).ToList<Product>();
            foreach (Product p in products)
            {
                int? sum = 0;
                int? average = 0;
                if (p.CustomerComments.Count > 0)
                {
                    foreach (CustomerComment r in p.CustomerComments)
                    {
                        sum += r.Rating;
                    }
                    if (sum > 0)
                    {
                        average = sum / p.CustomerComments.Count;
                    }
                }
                p.Average = average;

            }
            products = products.OrderByDescending(x => x.Average).ToList<Product>();

            return products;
        }

    }

}