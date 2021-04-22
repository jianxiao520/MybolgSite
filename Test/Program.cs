using Bolg.DAL;
using Bolg.DALFactory;
using IBLL;
using Bolg.BLL;
using IDAL;
using Model;
using ServiceStack.Redis;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Bolg.Common.Log;

namespace Test
{

    class Program
    {
        // public IUserBLL UserService { get; set; }
        //public ICommentBLL CommentService_ { get; set; }
        static public string Name{ set; get; }
        static void Main(string[] args)
        {
            //IUserDAL UserDAL = new UserDAL();
            //DbSession Db = new DbSession();
            //Db.UserDAL = UserDAL;

            //Console.WriteLine(Name);
            Bolg.Common.Cache.CacheHelper.AddCache("a", "q");
            Bolg.Common.Cache.CacheHelper.AddCache("b", "q");
            Bolg.Common.Cache.CacheHelper.AddCache("c", "q");
            Bolg.Common.Cache.CacheHelper.AddCache("d", "q");
            Bolg.Common.Cache.CacheHelper.AddCache("e", "q");
            Bolg.Common.Cache.CacheHelper.AddCache("f", "q");
            //UserService GetUserInfo = new UserService();
            //GetUserInfo.db = Db;
            //GetUserInfo.currentDal = UserDAL;
            //var a = GetUserInfo.QueryAllData().ToList();
            //Console.WriteLine(a);
            //Console.ReadLine();
            //new Bolg.Common.Log.ErrorLogHelp();
            //Bolg.Common.Log.ErrorLogHelp.WriterLog("哈哈哈");
            //Bolg.Common.Log.ErrorLogHelp.WriterLog("哈哈哈");
            //Bolg.Common.Log.ErrorLogHelp.WriterLog("哈哈哈");
            //LogHelper.Write("123");
            // log4net.Config.XmlConfigurator.Configure();
            ////对应loginfo配置
            //log4net.ILog log = log4net.LogManager.GetLogger("loginfo");//获取一个日志记录器
            //log.Info(DateTime.Now.ToString() + ": loginfo1");//写入一条新log
            // log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));//获取一个日志记录器
            // log.Debug("123");
            // log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));
            //log.Debug("哈哈哈");
            Console.ReadKey();


            return;
        }


    }
}
