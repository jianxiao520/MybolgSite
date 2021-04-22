using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bolg.UI.Portal.Filter
{
    //因为微软已经提供了一个HandleErrorAttribute类（它其实也是继承了IExceptionFilter），所以我们只需继承它即可
    public class MyExceptionFilter : HandleErrorAttribute
    {
        //重写OnException方法
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            Bolg.Common.Log.ErrorLogHelp.WriterLog(filterContext.Exception.ToString());
        }
    }

}