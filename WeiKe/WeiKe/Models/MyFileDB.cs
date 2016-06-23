using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{
    public class MyFileDB
    {
        public static void Insert(MyFile file)
        {
            string sql = "insert into file VALUES (" + file.id + ",'" + file.fileName + "','"+file.mimeType+"','" + file.fileUrl +"',"+file.weike_id +")";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}