using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bolg.UI.Portal.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            var user = Bolg.Common.Cache.CacheHelper.GetCache<bk_user>("User");

            return View();
        }
    }
}