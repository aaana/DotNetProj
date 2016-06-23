using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{
    public class WeikeDB
    {
        public static List<WeikeData> GetAllWeike()
        {
            List<WeikeData> wdList = new List<WeikeData>();
            string sql = "select weike.weike_id,weike.title,weike.subject,weike.user_id,weike.src,weike.size,weike.description,weike.star,weike.postdate,weike.commentNum,user.name,user.avatar from weike natural join user";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"],(string)reader["title"], (string)reader["subject"], (int)reader["user_id"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"],(DateTime)reader["postdate"], (int)reader["commentNum"]);
                WeikeData wd = new WeikeData(weike, reader.GetString("name"),reader.GetString("avatar"));
                wdList.Add(wd);

            }
            reader.Close();
            conn.Close();

            return wdList;

        }

        public static List<WeikeData> GetAllWeikeOrderByDate()
        {
            List<WeikeData> wdList = new List<WeikeData>();
            string sql = "select weike.weike_id,weike.title,weike.subject,weike.user_id,weike.src,weike.size,weike.description,weike.star,weike.postdate,weike.commentNum,user.name,user.avatar from weike natural join user order by postdate desc";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], (string)reader["subject"], (int)reader["user_id"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"], (int)reader["commentNum"]);
                WeikeData wd = new WeikeData(weike, reader.GetString("name"), reader.GetString("avatar"));
                wdList.Add(wd);

            }
            reader.Close();
            conn.Close();
            foreach (WeikeData wd in wdList)
            {
                wd.attachment = MyFileDB.FindByWeikeId(wd.weike.weike_id);
            }
            return wdList;

        }

        public static List<WeikeData> GetAllWeikeOrderByStar()
        {
            List<WeikeData> wdList = new List<WeikeData>();
            string sql = "select weike.weike_id,weike.title,weike.subject,weike.user_id,weike.src,weike.size,weike.description,weike.star,weike.postdate,weike.commentNum,user.name,user.avatar from weike natural join user order by star desc";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], (string)reader["subject"], (int)reader["user_id"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"], (int)reader["commentNum"]);
                WeikeData wd = new WeikeData(weike, reader.GetString("name"),reader.GetString("avatar"));
                wdList.Add(wd);

            }
            reader.Close();
            conn.Close();
            foreach (WeikeData wd in wdList)
            {
                wd.attachment = MyFileDB.FindByWeikeId(wd.weike.weike_id);
            }
            return wdList;

        }

        public static List<WeikeData> GetAllWeikeOrderByComment()
        {
            List<WeikeData> wdList = new List<WeikeData>();
            string sql = "select weike.weike_id,weike.title,weike.subject,weike.user_id,weike.src,weike.size,weike.description,weike.star,weike.postdate,weike.commentNum,user.name,user.avatar from weike natural join user order by commentNum desc";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], (string)reader["subject"], (int)reader["user_id"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"], (int)reader["commentNum"]);
                WeikeData wd = new WeikeData(weike, reader.GetString("name"), reader.GetString("avatar"));
                wdList.Add(wd);

            }
            reader.Close();
            conn.Close();
            foreach (WeikeData wd in wdList)
            {
                wd.attachment = MyFileDB.FindByWeikeId(wd.weike.weike_id);
            }
            return wdList;

        }

        public static int Insert(Weike weike)
        {
            int result = 0;
            string sql = "insert into weike VALUES ("+weike.weike_id + ",'" + weike.title + "','" +weike.subject + "','" +weike.user_id+ "','" +weike.src+ "','"+weike.size+ "','"+weike.description+"',"+weike.star + ",'"+weike.postdate + "'," +weike.commentNum+","+ 1 +")";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            sql = "update user set postNum = postNum+1 where user_id = @user_id";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@user_id", weike.user_id);
            cmd.ExecuteNonQuery();
            sql = "select weike_id from weike order by weike_id desc limit 0,1";
            cmd.CommandText = sql;
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                result = (int)reader[0];
            }
            reader.Close();
            conn.Close();

            return result;
        }

        public static List<WeikeData> FindByTitle(string title)
        {
            List<WeikeData> wdList = new List<WeikeData>();
            string sql = "select weike.weike_id,weike.title,weike.subject,weike.user_id,weike.src,weike.size,weike.description,weike.star,weike.postdate,weike.commentNum,user.name,user.avatar from weike natural join user where title like @title";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            string condition = "";
            for(int i=0;i<title.Length;i++)
            {
                condition = condition + "%" + title[i];
            }
            condition = condition + "%";
            cmd.Parameters.AddWithValue("@title",condition);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], (string)reader["subject"], (int)reader["user_id"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"],(int)reader["commentNum"]);
                WeikeData wd = new WeikeData(weike, reader.GetString("name"), reader.GetString("avatar"));
                wdList.Add(wd);

            }
            reader.Close();
            conn.Close();
            foreach (WeikeData wd in wdList)
            {
                wd.attachment = MyFileDB.FindByWeikeId(wd.weike.weike_id);
            }
            return wdList;

        }

        public static List<WeikeData> FindBySubject(string subject)
        {
            List<WeikeData> wdList = new List<WeikeData>();
            string sql = "select weike.weike_id,weike.title,weike.subject,weike.user_id,weike.src,weike.size,weike.description,weike.star,weike.postdate,weike.commentNum,user.name,user.avatar from weike natural join user where subject like @subject";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            string condition = "";
            for (int i= 0;i<subject.Length;i++)
            {
                condition = condition + "%" + subject[i];
            }
            condition = condition + "%";
            Console.WriteLine(condition);
            cmd.Parameters.AddWithValue("@subject", condition);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], (string)reader["subject"], (int)reader["user_id"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"], (int)reader["commentNum"]);
                WeikeData wd = new WeikeData(weike, reader.GetString("name"), reader.GetString("avatar"));
                wdList.Add(wd);

            }
            reader.Close();
            conn.Close();
            foreach (WeikeData wd in wdList)
            {
                wd.attachment = MyFileDB.FindByWeikeId(wd.weike.weike_id);
            }
            return wdList;

        }

        public static List<WeikeData> FindByAuthor(string author)
        {
            
            List<WeikeData> wdList = new List<WeikeData>();
            string sql = "select weike.weike_id,weike.title,weike.subject,weike.user_id,weike.src,weike.size,weike.description,weike.star,weike.postdate,weike.commentNum,user.name,user.avatar from weike natural join user where name like @author";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            string condition = "";
            for (int i = 0; i < author.Length; i++)
            {
                condition = condition + "%" + author[i];
            }
            condition = condition + "%";
            Console.WriteLine(condition);
            cmd.Parameters.AddWithValue("@author", condition);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"],(string)reader["title"], (string)reader["subject"], (int)reader["user_id"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"], (int)reader["commentNum"]);
                WeikeData wd = new WeikeData(weike, reader.GetString("name"), reader.GetString("avatar"));
                wdList.Add(wd);

            }
            reader.Close();
            conn.Close();
            foreach (WeikeData wd in wdList)
            {
                wd.attachment = MyFileDB.FindByWeikeId(wd.weike.weike_id);
            }
            return wdList;

        }

        public static WeikeData FindByWeikeId(int weike_id)
        {
            WeikeData wd = null;
            string sql = "select weike.weike_id,weike.title,weike.subject,weike.user_id,weike.src,weike.size,weike.description,weike.star,weike.postdate,weike.commentNum,user.name,user.avatar from weike natural join user where weike_id = @weike_id";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@weike_id", weike_id);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], (string)reader["subject"], (int)reader["user_id"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"], (int)reader["commentNum"]);
                wd = new WeikeData(weike, reader.GetString("name"), reader.GetString("avatar"));

            }
            reader.Close();
            conn.Close();

            wd.attachment = MyFileDB.FindByWeikeId(wd.weike.weike_id);
          
            return wd;

        }

        public static List<WeikeData> FindByUserId(int user_id)
        {
            List<WeikeData> wdList = new List<WeikeData>();
            string sql = "select weike.weike_id,weike.title,weike.subject,weike.user_id,weike.src,weike.size,weike.description,weike.star,weike.postdate,weike.commentNum,user.name,user.avatar from weike natural join user where user_id = @id";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@id", user_id);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], (string)reader["subject"], (int)reader["user_id"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"], (int)reader["commentNum"]);
                WeikeData wd = new WeikeData(weike, reader.GetString("name"), reader.GetString("avatar"));
                wdList.Add(wd);
            }
            reader.Close();
            conn.Close();
            foreach (WeikeData wd in wdList)
            {
                wd.attachment = MyFileDB.FindByWeikeId(wd.weike.weike_id);
            }
            return wdList;

        }

        public static List<WeikeData> FindByUserId(int user_id,int top)
        {
            List<WeikeData> wdList = new List<WeikeData>();
            string sql = "select weike.weike_id,weike.title,weike.subject,weike.user_id,weike.src,weike.size,weike.description,weike.star,weike.postdate,weike.commentNum,user.name,user.avatar from weike inner join user on user.user_id = weike.user_id where user.user_id = 2 ORDER BY weike.star  DESC limit 0,@top";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@id", user_id);
            cmd.Parameters.AddWithValue("@top", top);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], (string)reader["subject"], (int)reader["user_id"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"], (int)reader["commentNum"]);
                WeikeData wd = new WeikeData(weike, reader.GetString("name"), reader.GetString("avatar"));
                wdList.Add(wd);
            }
            reader.Close();
            conn.Close();
            foreach (WeikeData wd in wdList)
            {
                wd.attachment = MyFileDB.FindByWeikeId(wd.weike.weike_id);
            }
            return wdList;
        }
        /*
        public static Weike FindWeikeByWeikeId(int weike_id)
        {
            Weike weike = null;
            string sql = "select weike.* from weike where weike_id = @weike_id";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@weike_id", weike_id);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                weike = new Weike((int)reader["weike_id"], (string)reader["title"], (string)reader["subject"], (int)reader["user_id"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"], (int)reader["commentNum"]);
 

            }
            reader.Close();
            conn.Close();
            return weike;

        }
        */
        public static void DeleteById(int weike_id)
        {
            string sql = "delete from weike where weike_id = @weike_id";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@weike_id", weike_id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        /*
        static public List<WeikeData> FindWeikeByTime(DateTime time)
        {
            List<WeikeData> wdList = new List<WeikeData>();
            DateTime high = new DateTime(time.Year, time.Month, time.Day + 1);
            string sql = "SELECT weike.weike_id,weike.title,weike.subject,weike.user_id,weike.src,weike.size,weike.description,weike.star,weike.postdate,weike.commentNum,user.name FROM weike.weike natural join user where postdate >= @low and postdate<@high;";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@low", time);
            cmd.Parameters.AddWithValue("@high", high);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], (string)reader["subject"], (int)reader["user_id"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"], (int)reader["commentNum"]);
                WeikeData wd = new WeikeData(weike, reader.GetString("name"));
                wdList.Add(wd);

            }
            reader.Close();
            conn.Close();
            return wdList;
        }

        static public List<WeikeData> FindWeikeByTime(string author,DateTime time)
        {
            List<WeikeData> wdList = new List<WeikeData>();
            DateTime high = new DateTime(time.Year, time.Month, time.Day + 1);
            string sql = "SELECT weike.weike_id,weike.title,weike.subject,weike.user_id,weike.src,weike.size,weike.description,weike.star,weike.postdate,weike.commentNum,user.name FROM weike.weike natural join user where postdate >= @low and postdate<@high and name like @author;";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            string condition = "";
            for (int i = 0; i < author.Length; i++)
            {
                condition = condition + "%" + author[i];
            }
            condition = condition + "%";
            Console.WriteLine(condition);
            cmd.Parameters.AddWithValue("@author", condition);
            cmd.Parameters.AddWithValue("@low", time);
            cmd.Parameters.AddWithValue("@high", high);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], (string)reader["subject"], (int)reader["user_id"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"], (int)reader["commentNum"]);
                WeikeData wd = new WeikeData(weike, reader.GetString("name"));
                wdList.Add(wd);

            }
            reader.Close();
            conn.Close();
            return wdList;
        }*/

    }
  
}