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

        public static MyFile FindByWeikeId(int weikeId)
        {
            MyFile result = null;
            string sql = "select * from file where weike_id = @weike_id";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@weike_id", weikeId);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) {
                result = new MyFile((int)reader["id"], reader.GetString("fileName"), reader.GetString("mimeType"), reader.GetString("fileUrl"), (int)reader["weike_id"]);
            }

            cmd.ExecuteNonQuery();
            conn.Close();
            return result;
        }
    }
}