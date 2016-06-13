using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{
    public class FavoriteDB
    {
        static public void Insert(Favorite favorite)
        {
            string sql = "insert into favorite VALUES (" + favorite.user_id + "," + favorite.weike_id + ",'" + favorite.date + "')";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        static public void Delete(int user_id,int weike_id)
        {
            string sql = "delete from favorite where user_id = @userid and weike_id = @weikeid";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@userid", user_id);
            cmd.Parameters.AddWithValue("@weikeid", weike_id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}