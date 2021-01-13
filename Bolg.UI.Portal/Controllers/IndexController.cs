using Bolg.BLL;
using Bolg.UI.Portal.Controllers;
using IBLL;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spring.Context;
using Spring.Context.Support;
using Spring.Objects.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bolg.UI.Portal
{
    public class IndexController : BaseController
    {
        public IUserBLL UserService_ { get; set; }
        public ITokenBLL TokenService_ { get; set; }
        public ICommentBLL CommentService_ { get; set; }
        public IGameBLL GameService_ { get; set; }


        #region 视图
        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //往前端抛登录状态

            return View();
        }
        /// <summary>
        /// 评论框架
        /// </summary>
        /// <returns></returns>
        public ActionResult Comment()
        {



            return View();
        }
        /// <summary>
        /// Socket聊天室
        /// </summary>
        /// <returns></returns>
        public ActionResult ChatRoom()
        {


            return View();
        }
        /// <summary>
        /// 注册登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// 音乐播放页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Music()
        {
            return View();
        }
        /// <summary>
        /// 资源分享页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DailyShare()
        {
            return View();
        }
        /// <summary>
        /// 关于我主页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AboutWeb()
        {
            return View();
        }
        /// <summary>
        /// 关于博客介绍页面
        /// </summary>
        /// <returns></returns>
        public ActionResult BlogIntroduction()
        {
            return View();
        }
        /// <summary>
        /// 博客设计页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Design_Document()
        {
            return View();
        }
        /// <summary>
        /// 个人介绍界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Self_Introduction()
        {
            return View();
        }
        /// <summary>
        /// 作品时间线页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Timeline()
        {
            return View();
        }
        /// <summary>
        /// 小游戏页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Game()
        {
            //List<UserScoreInfo> ScoreInfo = GameService_.QueryRanking();
            // GameService_.UpScore(5, 70);
            return View();
        }
        #endregion


        /// <summary>
        /// 游戏页面事务处理
        /// </summary>
        /// <param name="Method"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Game_(string Method,string UserToken,string Score)
        {
            switch (Method)
            {
                case "UpScore":
                    Response<string> Result = GameService_.UpScore(UserToken, Int32.Parse(Score));
                    return Json(Result);
                case "QueryScoreInfo":
                    List<UserScoreInfo> Data = GameService_.QueryRanking();
                    return Json(Data);
                default:
                    return Json("未知接口类型");
            }
        }
        /// <summary>
        /// 获取音乐留言
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Comment_(string Method, string Content, string Comment_Id, string UserToken)
        {
            switch (Method)
            {
                case "QueryComment":
                    List<ResponseTComments> Data = CommentService_.GetAllCommentDetailedInfo();
                    return Json(Data);
                case "AddComment":
                    Response<string> Result = CommentService_.AddComments(Content, UserToken);
                    return Json(Result);
                case "UpVote":
                    return Json(CommentService_.AddLikeNum(UserToken, new bk_comments { Comment_Id = Int32.Parse(Comment_Id) }));
                default:
                    return Json("未知接口类型");
            }

        }
        /// <summary>
        /// 前端异步登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login_(string Type, string U_UserName, string U_PassWord, string U_Phone, string Us_Ip, string CodeToken)
        {
            //页面Action
            switch (Type)
            {
                case "Login":
                    #region 登录事务
                    var LoginText = UserService_.Check_Pass(U_UserName.Trim(), U_PassWord.Trim(), CodeToken, out bk_user UserInfo);
                    JObject Reader = (JObject)JsonConvert.DeserializeObject(LoginText);
                    if (Reader["State"].ToString() == "1")
                    {
                        //登录成功，帮助客户端设置Cookie
                        Response.Cookies["UserToken"].Value = Reader["Msg"].ToString();
                        Response.Cookies["UserToken"].Expires = DateTime.Now.AddDays(1);
                        Bolg.Common.Cache.CacheHelper.AddCache(Reader["Msg"].ToString(), UserInfo, DateTime.Now.AddMinutes(20));
                    }
                    #endregion
                    return Json(LoginText);
                case "Register":
                    #region 注册事务
                    List<Response<String>> ResponseT = new List<Response<String>>();
                    if (UserService_.Register(new bk_user
                    {
                        Us_UserName = U_UserName,
                        Us_PassWord = U_PassWord,
                        Us_Phone = U_Phone,
                        Us_Ip = Us_Ip
                    }, CodeToken) == true)
                    {
                        ResponseT.Add(new Response<String>
                        {
                            State =1,
                            Msg = "注册成功!"
                        });
                    }
                    else
                    {
                        ResponseT.Add(new Response<String>
                        {
                            State = 0,
                            Msg = "用户名已存在，请更换!"
                        });
                    }

                    #endregion
                    return Json(ResponseT);
                case "OutLogin":
                    #region 退出登录事务
                    Common.Cache.CacheHelper.RemoveCache(Request.Cookies["UserToken"].Value);
                    ViewData["IsLogin"] = "0"; UserIsLogin = false;
                    #endregion
                    return Json(Json_Conver(new Response<String>
                    {
                        State = 1,
                        Msg = "删除成功"
                    }));
                default:
                    return Json("接口参数错误");
            }
        }
        /// <summary>
        /// 对象转换成JSON，方便内部使用
        /// </summary>
        /// <param name="FromObject"></param>
        /// <returns></returns>
        private string Json_Conver(object FromObject)
        {
            return JsonConvert.SerializeObject(FromObject);
        }

    }
}