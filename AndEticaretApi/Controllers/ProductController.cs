
using AndEticaretCoreModel;
using AndEticaretCoreModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AndEticaretApi.Controllers
{
    public class ProductController : ApiController
    {
        public List<Product> Get()
        {
            var db = new AndDB();
            var data = db.Products.Where(x => x.IsActive == true).ToList();
            return data;
        }
        public Product Get(int id)
        {
            var db = new AndDB();
            var data = db.Products.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }
    }
}
