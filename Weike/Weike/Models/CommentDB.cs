using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{
    public class CommentDB
    {
        public static void Insert(Comment comment)
        {
            string sql = "insert into comment VALUES (" + comment.id + "," + comment.user_id + "," + comment.weike_id + ",'" + comment.date +comment.content +"','" + "')";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public static void DeleteById(int id)
        {
            string sql = "delete from comment where id = @id";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}