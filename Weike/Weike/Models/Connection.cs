using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{
    public class Connection
    {
        public static MySqlConnection getMySqlCon()
        {
            String mysqlStr = "Database=weike;Data Source=localhost;User Id=root;Password=;pooling=false;CharSet=utf8;port=3306";
            // String mySqlCon = ConfigurationManager.ConnectionStrings["MySqlCon"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(mysqlStr);
            return conn;
        }
    }
}