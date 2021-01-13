using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bolg.Common
{
    public class MD5Helper
    {
        /// <summary>
        /// 校验MD5
        /// </summary>
        /// <param name="Content">被加密的字符串</param>
        /// <returns></returns>
        public static string getMD5String(string Content)
        {
            //1、创造MD5对象
            MD5 md = MD5.Create();
            //2、将字符串转换为字节数组
            byte[] b = Encoding.UTF8.GetBytes(Content);
            //3、加密，计算
            byte[] bb = md.ComputeHash(b);
            //4、把加密后的bb数组转换为字符串
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bb.Length; i++)
            {
                //把数组中每一位转换为16进制码值，然后整个字符串全大写
                sb.Append(bb[i].ToString("x2").ToLower());
            }
            return sb.ToString();
        }
    }
}
