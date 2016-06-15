﻿using MySql.Data.MySqlClient;
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
            //string sql = "insert into comment VALUES (" + comment.comment_id + "," + comment.user_id + "," + comment.weike_id + ",'" + comment.date + "','" + comment.content + "',"+comment.isread+ ")";string sql = "insert into comment VALUES (" + comment.comment_id + "," + comment.user_id + "," + comment.weike_id + ",'" + comment.date + "','" + comment.content + "',"+comment.isread+ ")";
            string sql = "insert into comment VALUES (" + comment.comment_id + "," + comment.user_id + "," + comment.weike_id + ",'" + comment.date + "','" + comment.content + "',"+comment.parent+ ")";
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
            string sql = "select weike.weike_id ,weike.title,weike.subject,weike.user_id as author_id,weike.src,weike.size,weike.description,weike.star,weike.postdate,weike.commentNum, comment.comment_id,comment.user_id as commenter_id,comment.date,comment.content,comment.parent,user1.name as author, user2.name as commenter from weike inner join comment inner join user as user1 inner join user as user2 where comment.weike_id = weike.weike_id and user1.user_id = weike.user_id and user2.user_id = comment.user_id and comment.user_id = @userid order by comment.date desc;";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@userid", user_id);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], reader.GetString("subject"), (int)reader["author_id"], (string)reader["src"], (string)reader["size"], (string)reader["description"],(int)reader["star"],(DateTime)reader["postdate"],(int)reader["commentNum"]);
                Comment comment = new Comment((int)reader["comment_id"], (int)reader["commenter_id"], (int)reader["weike_id"], (DateTime)reader["date"], (string)reader["content"],(int)reader["parent"]);
                CommentData cd = new CommentData(weike, comment, (string)reader["author"], (string)reader["commenter"]);
                cdList.Add(cd);

            }
            reader.Close();
            conn.Close();
            return cdList;
        }

        static public List<CommentData> FindCommentWeikeByWeikeId(int weike_id)
        {
            List<CommentData> cdList = new List<CommentData>();
            string sql = "select weike.weike_id ,weike.title,weike.subject,weike.user_id as author_id,weike.src,weike.size,weike.description,weike.star,weike.postdate,weike.commentNum, comment.comment_id,comment.user_id as commenter_id,comment.date,comment.content,comment.parent,user1.name as author, user2.name as commenter from weike inner join comment inner join user as user1 inner join user as user2 where comment.weike_id = weike.weike_id and user1.user_id = weike.user_id and user2.user_id = comment.user_id and comment.weike_id = @weikeid order by comment.date desc;";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@weikeid", weike_id);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], reader.GetString("subject"), (int)reader["author_id"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"], (int)reader["commentNum"]);
                Comment comment = new Comment((int)reader["comment_id"], (int)reader["commenter_id"], (int)reader["weike_id"], (DateTime)reader["date"], (string)reader["content"],(int)reader["parent"]);
                CommentData cd = new CommentData(weike, comment, (string)reader["author"], (string)reader["commenter"]);
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
            string sql = "select weike.weike_id ,weike.title,weike.subject,weike.user_id as author_id,weike.src,weike.size,weike.description,weike.star,weike.postdate,weike.commentNum, comment.comment_id,comment.user_id as commenter_id,comment.date,comment.content,comment.parent,user1.name as author, user2.name as commenter from weike inner join comment inner join user as user1 inner join user as user2 where comment.weike_id = weike.weike_id and user1.user_id = weike.user_id and user2.user_id = comment.user_id and comment.user_id = @userid and comment.date >= @low and comment.date<@high order by comment.date desc;";
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
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], reader.GetString("subject"), (int)reader["author_id"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"], (int)reader["commentNum"]);
                Comment comment = new Comment((int)reader["comment_id"], (int)reader["commenter_id"], (int)reader["weike_id"], (DateTime)reader["date"], (string)reader["content"], (int)reader["parent"]);
                CommentData cd = new CommentData(weike, comment, (string)reader["author"], (string)reader["commenter"]);
                cdList.Add(cd);

            }
            reader.Close();
            conn.Close();
            return cdList;
        }
        /*
        public static void UpdateIsRead(int comment_id,Boolean isread)
        {
            string sql = "update comment set isread = @isread where comment_id = @id";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@id", comment_id);
            cmd.Parameters.AddWithValue("@isread", isread);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        */
    }
}