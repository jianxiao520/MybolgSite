using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        //用户ID
        public int Us_Id { get; set; }
        //用户名
        public string Us_UserName { get; set; }
        //昵称
        public string Us_NickName { get; set; }
        //密码
        public string Us_PassWord { get; set; }
        //邮箱
        public string Us_Eamil { get; set; }
        //头像
        public string Us_ProfilePhoto { get; set; }
        //注册时间
        public string Us_RegisterTime { get; set; }
        //出生日期
        public string Us_Birthday { get; set; }
        //年龄
        public int Us_Age { get; set; }
        //性别
        public int Us_Sex { get; set; }
        //手机号
        public string Us_Phone { get; set; }
        //注册Ip
        public string Us_Ip { get; set; }
    }
}
