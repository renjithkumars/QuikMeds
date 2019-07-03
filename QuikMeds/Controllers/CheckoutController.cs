using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading;


namespace QuikMeds.Controllers
{
    public class CheckoutController : BaseController
    {
        private List<object> states;
        private List<object> cards;

        public CheckoutController()
        {
            states = new List<object> {
                new { SID = "DBL", SName = "DUBLIN" },
                new { SID = "VIC", SName = "VICLAW" },
                new { SID = "KLD", SName = "KILDARE" },
                new { SID = "GLW", SName = "GALWAY" },
                new { SID = "CRK", SName = "CORK" },
                new { SID = "WTD", SName = "WATERFORD" }

            };

            cards = new List<object> {
                new { Type = "VISA" },
                new { Type = "Master Card" },
                new { Type = "Mastero" }
            };

        }

        // GET: Checkout
        public ActionResult Index()
        {
            ViewBag.Cart = _ctx.ShoppingCartDatas.ToList<ShoppingCartData>();
            return View();
        }

        public JsonResult QuanityChange(int type, int pId)
        {
            CTXEntities context = new CTXEntities();

            ShoppingCartData product = context.ShoppingCartDatas.FirstOrDefault(p => p.PID == pId);
            if (product == null)
            {
                return Json(new { d = "0" });
            }

            Product actualProduct = context.Products.FirstOrDefault(p => p.PID == pId);
            int quantity;
            // if type 0, decrease quantity
            // if type 1, increase quanity
            switch (type)
            {
                case 0:
                    product.Quantity--;
                    actualProduct.UnitsInStock++;
                    break;
                case 1:
                    product.Quantity++;
                    actualProduct.UnitsInStock--;
                    break;
                case -1:
                    actualProduct.UnitsInStock += product.Quantity;
                    product.Quantity = 0;
                    break;
                default:
                    return Json(new { d = "0" });
            }

            if (product.Quantity == 0)
            {
                context.ShoppingCartDatas.Remove(product);
                quantity = 0;
            }
            else
            {
                quantity = product.Quantity;
            }

            context.SaveChanges();
            return Json(new { d = quantity });
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

        public ActionResult Clear()
        {
            try
            {
                List<ShoppingCartData> carts = _ctx.ShoppingCartDatas.ToList();
                carts.ForEach(a => {
                    Product product = _ctx.Products.FirstOrDefault(p => p.PID == a.PID);
                    product.UnitsInStock += a.Quantity;
                });
                _ctx.ShoppingCartDatas.RemoveRange(carts);
                _ctx.SaveChanges();
            }
            catch (Exception) { }
            return RedirectToAction("Index", "Home", null);
        }

        public ActionResult Purchase()
        { 
            ViewBag.States = states;
            ViewBag.Cards = cards;
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login","Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Purchase(QuikMeds.Models.Customer customer)
        {
            ViewBag.States = states;
            ViewBag.Cards = cards;

            if (ModelState.IsValid)
            {
                if (customer.ExpDate <= DateTime.Now)
                {
                    ModelState.AddModelError("", "Credit card has already expired");
                }

               if (customer.CardNo.Length != 16)
                    {
                        ModelState.AddModelError("", customer.Ctype + "must be 16 digits");
                    }
             

                if (ModelState.IsValid)
                {
                    Customer c = new Customer
                    {
                        FName = customer.FName,
                        LName = customer.LName,
                        Email = customer.Email,
                        Phone = customer.Phone,
                        Address1 = customer.Address1,
                        Address2 = customer.Address2,
                        Suburb = customer.Suburb,
                        Postcode = customer.Postcode,
                        State = customer.State,
                        Ctype = customer.Ctype,
                        CardNo = customer.CardNo,
                        ExpDate = customer.ExpDate
                    };

                    Order o = new Order
                    {
                        OrderDate = DateTime.Now,
                        DeliveryDate = DateTime.Now.AddDays(5),
                        CID = c.CID
                    };

                    _ctx.Customers.Add(c);
                    _ctx.Orders.Add(o);

                    foreach (var i in _ctx.ShoppingCartDatas.ToList<ShoppingCartData>())
                    {
                        _ctx.Order_Products.Add(new Order_Products
                        {
                            OrderID = o.OrderID,
                            PID = i.PID,
                            Qty = i.Quantity,
                            TotalSale = i.Quantity * i.UnitPrice
                        });
                        _ctx.ShoppingCartDatas.Remove(i);
                    }

                    _ctx.SaveChanges();
                   //Email
                    string result = null;
                    
                    try
                    {
                        MailMessage mailMessage = new MailMessage("shilpa.eis2@gmail.com ", c.Email);
                        mailMessage.Subject = "new order";
                        mailMessage.Body = ("Thank you for using QuiKMeds.......    Your ordenumber : " + o.OrderID+ "   Order date :  "+o.OrderDate+"  Delivery date  : "+o.DeliveryDate);
                        mailMessage.IsBodyHtml = false;

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;

                        NetworkCredential nc = new NetworkCredential("shilpa.eis2@gmail.com", "Assignment");
                        smtp.UseDefaultCredentials = true;

                        smtp.Credentials = nc;
                        smtp.Send(mailMessage);
                        ViewBag.Message = "Mail has been sent successfuly";

                        if (ModelState.IsValid)
                        {

                            //saving
                            if (string.IsNullOrEmpty(result))
                            {
                                return RedirectToAction("Index");
                            }
                        }
                    }
                    catch
                    {
                        ViewBag.Message = "Message Error!!! Please enter a valid Email Id";
                        return RedirectToAction("Index");
                    }

                    return RedirectToAction("Index", "Home");
               


                }
            }

            List<ModelError> errors = new List<ModelError>();
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errors.Add(error);
                }
            }
            return View(customer);
        }

        public ActionResult PurchasedSuccess()
        {
            return View();
        }
    }
}
