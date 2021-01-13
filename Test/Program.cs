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

namespace Test
{
    class Program
    {
        //public IUserBLL UserService { get; set; }
        public ICommentBLL CommentService_ { get; set; }
        static void Main(string[] args)
        {
            //var user =  Bolg.Common.Cache.CacheHelper.GetCache<bk_user>("User");
            //Bolg.Common.Cache.CacheHelper.AddCache("b6bc671b326aca515cc4de4a29d66d26", new bk_user{ Us_Phone="18929554451"}, DateTime.Now.AddMinutes(20));
            //var a = Bolg.Common.Cache.CacheHelper.GetCache<bk_user>("b6bc671b326aca515cc4de4a29d66d26") as bk_user;
            //ICommentBLL a = new CommentService();
            //new this.Ao();

            //CommentService.GetAllCommentDetailedInfo();
            //var a=DbSessionFactory.GetDbSession();
            System.Environment.Exit(0);
            return;
        }

        public void Ao()
        {
            List<ResponseTComments> Data = CommentService_.GetAllCommentDetailedInfo();
            return;
        }
    }
}
