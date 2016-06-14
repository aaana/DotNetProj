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
            string sql = "insert into comment VALUES (" + comment.comment_id + "," + comment.user_id + "," + comment.weike_id + ",'" + comment.date + "','" + comment.content + "')";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public static void DeleteById(int comment_id)
        {
            string sql = "delete from comment where comment_id = @id";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@id", comment_id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        static public List<CommentData> FindCommentWeikeByUserId(int user_id)
        {
            List<CommentData> cdList = new List<CommentData>();
            string sql = "SELECT weike.weike_id,weike.title,weike.subject,weike.author,weike.src,weike.size,weike.description,weike.star,weike.postdate,comment.date,comment.content FROM weike.weike inner join comment where weike.weike_id = comment.weike_id and user_id = @userid;";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@userid", user_id);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], (string)reader["subject"], (string)reader["author"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"],(DateTime)reader["postdate"]);
                CommentData cd = new CommentData(weike, (DateTime)reader["date"], reader.GetString("content"));
                cdList.Add(cd);

            }
            reader.Close();
            conn.Close();
            return cdList;
        }

        static public List<CommentData> FindCommentWeikeByTime(int user_id,DateTime time)
        {
            List<CommentData> cdList = new List<CommentData>();
            DateTime high = new DateTime(time.Year,time.Month,time.Day+1);
            string sql = "SELECT weike.weike_id,weike.title,weike.subject,weike.author,weike.src,weike.size,weike.description,weike.star,weike.postdate,comment.date,comment.content FROM weike.weike inner join comment where weike.weike_id = comment.weike_id and user_id = @userid and comment.date >= @low and comment.date<@high;";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@userid", user_id);
            cmd.Parameters.AddWithValue("@low", time);
            cmd.Parameters.AddWithValue("@high", high);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], (string)reader["subject"], (string)reader["author"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"]);
                CommentData cd = new CommentData( weike, (DateTime)reader["date"], reader.GetString("content"));
                cdList.Add(cd);

            }
            reader.Close();
            conn.Close();
            return cdList;
        }
    }
}