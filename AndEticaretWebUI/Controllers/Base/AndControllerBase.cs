using AndEticaretCoreModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AndEticaretWebUI
{
    public class AndControllerBase : Controller
    {
        //<summary>
        //Kullanıcı login kontrolü
        //<summary>
        public bool IsLogin { get; private set; }
        //<summary>
        //giriş yapan kullnıcının Id'si
        //<summary>
        public int LoginUserID { get; private set; }
        //<summary>
        //Login USer Entity
        //<summary>
        public User LoginUserEntity { get; private set; }
        protected override void Initialize(RequestContext requestContext)
        {
            //Session["LoginUserId"]
            // Session["LoginUser"] =
            if (requestContext.HttpContext.Session["LoginUserId"] != null)
            {
                IsLogin = true;
                LoginUserID = (int)requestContext.HttpContext.Session["LoginUserId"];
                LoginUserEntity = (AndEticaretCoreModel.Entity.User)requestContext.HttpContext.Session["LoginUser"];
            }
            base.Initialize(requestContext);   
        }
    }
}