using Bolg.BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class BackEndController : Controller
    {
        // GET: BackEnd
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult workplace()
        {

            return View();
        }
        public ActionResult UserList()
        {

            return View();
        }

        public ActionResult GetList(string Type)
        {
            List<bk_user> UserData = new UserService().QueryAllData().ToList();
            var person = new
            {
                code = 0,
                msg = "",
                count = "300",
                data = UserData
            };
            return Json(person, JsonRequestBehavior.AllowGet);
        }
    }
}