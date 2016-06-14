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
        public static List<Weike> GetAllWeike()
        {
            List<Weike> weikeList = new List<Weike>();
            string sql = "select * from weike";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"],(string)reader["title"], (string)reader["subject"], (string)reader["author"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"],(DateTime)reader["postdate"]);
                weikeList.Add(weike);

            }
            reader.Close();
            conn.Close();
            return weikeList;

        }

        public static void Insert(Weike weike)
        {
            string sql = "insert into weike VALUES ("+weike.weike_id + ",'" + weike.title + "','" +weike.subject + "','" +weike.author+ "','" +weike.src+ "','"+weike.size+ "','"+weike.description+"',"+weike.star + ","+weike.postdate + ")";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static List<Weike> FindByTitle(string title)
        {
            List<Weike> weikeList = new List<Weike>();
            string sql = "select * from weike where title like @title";
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
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], (string)reader["subject"], (string)reader["author"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"]);
                weikeList.Add(weike);

            }
            reader.Close();
            conn.Close();
            return weikeList;

        }

        public static List<Weike> FindBySubject(string subject)
        {
            List<Weike> weikeList = new List<Weike>();
            string sql = "select * from weike where subject like @subject";
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
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], (string)reader["subject"], (string)reader["author"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"]);
                weikeList.Add(weike);

            }
            reader.Close();
            conn.Close();
            return weikeList;

        }

        public static List<Weike> FindByAuthor(string author)
        {
            List<Weike> weikeList = new List<Weike>();
            string sql = "select * from weike where author like @author";
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
                Weike weike = new Weike((int)reader["weike_id"],(string)reader["title"], (string)reader["subject"], (string)reader["author"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"]);
                weikeList.Add(weike);

            }
            reader.Close();
            conn.Close();
            return weikeList;

        }

        public static List<Weike> FindByWeikeId(int weike_id)
        {
            List<Weike> weikeList = new List<Weike>();
            string sql = "select * from weike where weike_id = @id";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@id", weike_id);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], (string)reader["subject"], (string)reader["author"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"]);
                weikeList.Add(weike);

            }
            reader.Close();
            conn.Close();
            return weikeList;

        }

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

        static public List<Weike> FindWeikeByTime(DateTime time)
        {
            List<Weike> weikeList = new List<Weike>();
            DateTime high = new DateTime(time.Year, time.Month, time.Day + 1);
            string sql = "SELECT weike.weike_id,weike.title,weike.subject,weike.author,weike.src,weike.size,weike.description,weike.star,weike.postdate FROM weike.weike where postdate >= @low and postdate<@high;";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@low", time);
            cmd.Parameters.AddWithValue("@high", high);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], (string)reader["subject"], (string)reader["author"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"]);
                weikeList.Add(weike);

            }
            reader.Close();
            conn.Close();
            return weikeList;
        }

        static public List<Weike> FindWeikeByTime(string author,DateTime time)
        {
            List<Weike> weikeList = new List<Weike>();
            DateTime high = new DateTime(time.Year, time.Month, time.Day + 1);
            string sql = "SELECT weike.weike_id,weike.title,weike.subject,weike.author,weike.src,weike.size,weike.description,weike.star,weike.postdate FROM weike.weike where postdate >= @low and postdate<@high and author like @author;";
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
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], (string)reader["subject"], (string)reader["author"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"], (DateTime)reader["postdate"]);
                weikeList.Add(weike);

            }
            reader.Close();
            conn.Close();
            return weikeList;
        }

    }
  
}