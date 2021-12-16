using AndEticaretCoreModel;
using AndEticaretCoreModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AndEticaretWebUI.Controllers
{
    public class HomeController : AndControllerBase
    {
        AndDB db = new AndDB();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.IsLogin = this.IsLogin;
            var data = db.Products.OrderByDescending(x => x.CreateDate).Take(5).ToList();
            return View(data);
        }
        public PartialViewResult GetMenu()
        {
            var menus = db.Categories.Where(x => x.ParentId == 0).ToList();
            return PartialView(menus);
        }
        [Route("Uye-Giris")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Uye-Giris")]
        public ActionResult Login(string EMail, string Password)
        {
            var users = db.Users.Where(x => 
            x.EMail == EMail && 
            x.Password == Password && 
            x.IsActive == true && 
            x.IsAdmin == false).ToList();
            if (users.Count==1)
            {
                //giriş yapıldı
                Session["LoginUserId"] = users.FirstOrDefault().Id;
                Session["LoginUser"] = users.FirstOrDefault();
                return Redirect("/");
            }
            else
            {
                ViewBag.Error = "Hatalı Kullanıcı veya şifre";
                return View();
            }
            return View();
        }
        [Route("Uye-Kayit")]
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        [Route("Uye-Kayit")]
        public ActionResult CreateUser(User entity)
        {
            try
            {
                entity.CreateDate = DateTime.Now;
                entity.CreateUserId = 1;
                entity.IsActive = true;
                entity.IsAdmin = false;

                db.Users.Add(entity);
                db.SaveChanges();
                return Redirect("/");

    }
            catch (Exception ex)
            {
                return View();
            }

        }
    }
}