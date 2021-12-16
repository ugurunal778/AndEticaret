using AndEticaretCoreModel;
using AndEticaretCoreModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AndEticaretWebUI.Controllers
{
    public class OrderController : AndControllerBase
    {
        // GET: Order
        [Route("Siparisver")]
        public ActionResult AdressList()
        {
            var db = new AndDB();
            var data = db.Adresses.Where(x => x.UserId == LoginUserID).ToList();
            return View(data);
        }
        public ActionResult CreateUserAdress()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUserAdress(UserAdress entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.CreateUserId = LoginUserID;
            entity.IsActive = true;
            entity.UserId = LoginUserID;

            var db = new AndDB();
            db.Adresses.Add(entity);
            db.SaveChanges();
            return RedirectToAction("AdressList");
        }
        public ActionResult CreateOrder(int id)
        {
            var db = new AndDB();
            var sepet = db.Baskets.Include("Product").Where(x => x.UserId == LoginUserID).ToList();
            Order order = new Order();
            order.CreateDate = DateTime.Now;
            order.CreateUserId = LoginUserID;
            order.StatusID = 1;
            order.TotalProductPrice = sepet.Sum(x => x.Product.Price);
            order.TotalTaxPrice = sepet.Sum(x => x.Product.Tax);
            order.TotalDiscount = sepet.Sum(x => x.Product.Discount);
            order.TotalPrice = order.TotalProductPrice + order.TotalTaxPrice;
            order.UserAdressId = id;
            order.UserId = LoginUserID;
            order.OrderProducts = new List<OrderProduct>();

            foreach (var item in sepet)
            {
                order.OrderProducts.Add(new OrderProduct
                {
                    CreateDate = DateTime.Now,
                    CreateUserId = LoginUserID,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
                db.Baskets.Remove(item);
            }
            db.Orders.Add(order);
            db.SaveChanges();

            return RedirectToAction("Detail", new { id= order.Id});
        }
        public ActionResult Detail(int id) 
        {
            var db = new AndDB();
            var data = db.Orders.
                Include("OrderProducts").
                Include("OrderProducts.Product").
                Include("OrderPayments").
                Include("Status").
                Include("UserAdress").
                Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
        [Route("Siparislerim")]
        public ActionResult Index()
        {
            var db = new AndDB();
            var data = db.Orders.Include("Status").Where(x => x.UserId == LoginUserID).ToList();
            return View(data);
        }
        public ActionResult Pay(int id)
        {
            var db = new AndDB();
            var order = db.Orders.Where(x => x.Id == id).FirstOrDefault();
            order.StatusID = 8;
            db.SaveChanges();
            return RedirectToAction("Detail", new { id = order.Id });
        }

    }
}