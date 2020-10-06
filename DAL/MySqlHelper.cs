using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MySqlHelper
    {
        private static string strCon = ConfigurationManager.ConnectionStrings["strConlocalhost"].ConnectionString;
        public static void ExecuteSql(string sql, params MySqlParameter[] p)
        {
            strCon = ConfigurationManager.ConnectionStrings["strConlocalhost"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strCon);
            con.Open();
            MySqlCommand com = new MySqlCommand(sql, con);
            com.Parameters.AddRange(p);  //把参数数组p中的值全部收入com对象下的Parameters
            int len = com.ExecuteNonQuery();
            con.Close();

        }
        public static DataTable QueryData(string sql, params MySqlParameter[] p)
        {
            strCon = ConfigurationManager.ConnectionStrings["strConlocalhost"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strCon);
            DataTable dt = new DataTable();
            MySqlDataAdapter mda = new MySqlDataAdapter(sql, con);
            mda.SelectCommand.Parameters.AddRange(p);  //设置mda对象中的command对象的参数值
            mda.Fill(dt);
            return dt;

        }
    }
}
