using AndEticaretCoreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AndEticaretWebUI.Controllers
{
    public class CategoryController : AndControllerBase
    {
        // GET: Category
        [Route("Kategori/{isim}/{id}")]
        public ActionResult Index (string isim, int id)
        {
            var db = new AndDB();
            var data = db.Products.Where(x => x.IsActive == true && x.CategoryId == id).ToList();
            ViewBag.category = db.Categories.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}