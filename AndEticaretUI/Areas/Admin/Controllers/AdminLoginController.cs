using AndEticaretCoreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AndEticaretWebUI.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        AndDB db = new AndDB();
        // GET: Admin/AdminController
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string EMail, string Password)
        {
            var data = db.Users.Where(
                x => x.EMail == EMail && 
                x.Password == Password && 
                x.IsActive == true && 
                x.IsAdmin == true).ToList();
            if (data.Count==1)
            {
                //doğru giriş
                Session["AdminLoginUser"] = data.FirstOrDefault();
                return Redirect("/admin");
            }
            else
            {
                //hatalı giriş
                return View();
            }
        }
    }
}