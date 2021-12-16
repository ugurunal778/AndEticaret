using AndEticaretCoreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AndEticaretWebUI.Controllers
{
    public class BasketController : AndControllerBase
    {
        // GET: Basket
        [HttpPost]
        public JsonResult AddProduct(int productId, int quantity)
        {
            var db = new AndDB();
            db.Baskets.Add(new AndEticaretCoreModel.Entity.Basket
            {
                CreateDate = DateTime.Now,
                CreateUserId = LoginUserID,
                ProductId = productId,
                Quantity = quantity,
                UserId = LoginUserID
            });
            var rt = db.SaveChanges();
            return Json(rt, JsonRequestBehavior.AllowGet);
        }
        [Route("Sepetim")]
        public ActionResult Index()
        {
            var db = new AndDB();
            var data = db.Baskets.Include("Product").Where(x => x.UserId == LoginUserID).ToList();
            return View(data);
        }
        public ActionResult Delete(int id)
        {
            var db = new AndDB();
            var deleteItem = db.Baskets.Where(x => x.Id == id).FirstOrDefault(); 
            db.Baskets.Remove(deleteItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}