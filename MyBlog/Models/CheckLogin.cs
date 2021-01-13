using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Models
{
    public class CheckLogin: ActionFilterAttribute
    {
        public bool IsCheck{ get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Action执行前
            if (IsCheck)
            {

                if (filterContext.HttpContext.Session["Token"].Equals(""))
                {

                    List<String> ListData = new List<string>();
                    base.OnActionExecuting(filterContext);
                    //ListData.Add("")
                    //filterContext.HttpContext.Items[]
                        
                        
                }

            }
            filterContext.HttpContext.Response.Write($"Action执行前");
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Action执行后

            filterContext.HttpContext.Response.Write($"Action执行后");
            base.OnActionExecuted(filterContext);
        }


    }
}