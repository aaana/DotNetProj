using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{
    public class NoticeDB
    {
        static public void Insert(Notice notice)
        {
            string sql;
            if (notice.weike_id == 0)
            {
                sql = "insert into notice VALUES (" + notice.notice_id + "," + notice.sender_id + "," + notice.receiver_id + "," + null + ",'" + notice.type + "'," + notice.isread +",'"+ notice.noticetime+"')";

            }
            else
            {
                sql = "insert into notice VALUES (" + notice.notice_id + "," + notice.sender_id + "," + notice.receiver_id + "," + notice.weike_id + ",'" + notice.type + "'," + notice.isread + ",'" + notice.noticetime + "')";

            }
            //string sql = "insert into favorite VALUES (" + favorite.user_id + "," + favorite.weike_id + ",'" + favorite.date + "',"+ favorite.isread+ ")";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        static public List<NoticeData> FindUnreadNoticeByUserId(int user_id)
        {
            List<NoticeData> ndList = new List<NoticeData>();
            string sql = "select notice.*,weike.title,user.name from weike inner join user inner join notice on user.user_id = notice.sender_id and notice.weike_id = weike.weike_id where receiver_id = @userid and notice.isread = false";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@userid", user_id);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Notice notice = new Notice((int)reader["notice_id"], (int)reader["sender_id"], (int)reader["receiver_id"], (int)reader["weike_id"], reader.GetString("type"), (Boolean)reader["isread"],(DateTime)reader["noticetime"]);
                NoticeData nd = new NoticeData(notice, reader.GetString("name"),reader.GetString("title"));
                ndList.Add(nd);

            }
            reader.Close();
            conn.Close();
            return ndList;
        }

        static public List<NoticeData> FindAllNoticeByUserId(int user_id)
        {
            List<NoticeData> ndList = new List<NoticeData>();
            string sql = "select notice.*,weike.title,user.name from weike inner join user inner join notice on user.user_id = notice.sender_id and notice.weike_id = weike.weike_id where receiver_id = @userid";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@userid", user_id);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Notice notice = new Notice((int)reader["notice_id"], (int)reader["sender_id"], (int)reader["receiver_id"], (int)reader["weike_id"], reader.GetString("type"), (Boolean)reader["isread"], (DateTime)reader["noticetime"]);
                NoticeData nd = new NoticeData(notice, reader.GetString("name"), reader.GetString("title"));
                ndList.Add(nd);

            }
            reader.Close();
            conn.Close();
            return ndList;
        }

        static public List<NoticeData> FindAllNoticeByUserIdNType(int userId, string type)
        {
            List<NoticeData> ndList = new List<NoticeData>();
            string sql = "select notice.*,weike.title,user.name from weike inner join user inner join notice on user.user_id = notice.sender_id and notice.weike_id = weike.weike_id where receiver_id = @userid and type like @type";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@userid", userId);
            cmd.Parameters.AddWithValue("@type", "%"+type+"%");
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Notice notice = new Notice((int)reader["notice_id"], (int)reader["sender_id"], (int)reader["receiver_id"], (int)reader["weike_id"], reader.GetString("type"), (Boolean)reader["isread"], (DateTime)reader["noticetime"]);
                NoticeData nd = new NoticeData(notice, reader.GetString("name"), reader.GetString("title"));
                ndList.Add(nd);

            }
            reader.Close();
            conn.Close();
            return ndList;
        }

        static public List<NoticeData> FindUnReadNoticeByUserIdNType(int userId,string type)
        {
            List<NoticeData> ndList = new List<NoticeData>();
            string sql = "select notice.*,weike.title,user.name from weike inner join user inner join notice on user.user_id = notice.sender_id and notice.weike_id = weike.weike_id where isread = false and receiver_id = @userid and type like @type";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@userid", userId);
            cmd.Parameters.AddWithValue("@type", "%" + type + "%");
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Notice notice = new Notice((int)reader["notice_id"], (int)reader["sender_id"], (int)reader["receiver_id"], (int)reader["weike_id"], reader.GetString("type"), (Boolean)reader["isread"], (DateTime)reader["noticetime"]);
                NoticeData nd = new NoticeData(notice, reader.GetString("name"), reader.GetString("title"));
                ndList.Add(nd);

            }
            reader.Close();
            conn.Close();
            return ndList;
        }

        static public void UpdateIsread(int notice_id, Boolean isread)
        {
            string sql = "update notice set isread = @isread where notice_id = @noticeid";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@isread", isread);
            cmd.Parameters.AddWithValue("@noticeid", notice_id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}