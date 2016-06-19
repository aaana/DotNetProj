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
            //string sql = "insert into favorite VALUES (" + favorite.user_id + "," + favorite.weike_id + ",'" + favorite.date + "',"+ favorite.isread+ ")";
            string sql = "insert into favorite VALUES (" + favorite.user_id + "," + favorite.weike_id + ",'" + favorite.date  + "')";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            sql = "update weike set star = star+1 where weike_id = @weike_id";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@weike_id", favorite.weike_id);
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
            sql = "update weike set star = star-1 where weike_id = @weike_id";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@weike_id", weike_id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        static public List<FavoriteData> FindFavoriteWeikeByUserId(int user_id)
        {
            List<FavoriteData> fdList = new List<FavoriteData>();
            string sql = "SELECT weike.weike_id,weike.title,weike.subject,weike.user_id,weike.src,weike.size,weike.description,weike.star,weike.postdate,weike.commentNum,user.name,favorite.date FROM weike.weike inner join user inner join favorite where weike.weike_id = favorite.weike_id and user.user_id = weike.user_id and favorite.user_id = @userid;";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@userid", user_id);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], (string)reader["subject"], (int)reader["user_id"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"],(DateTime)reader["postdate"], (int)reader["commentNum"]);
                FavoriteData fd = new FavoriteData( weike, (string)reader["name"],(DateTime)reader["date"]);
                fdList.Add(fd);

            }
            reader.Close();
            conn.Close();
            return fdList;
        }

        /*
        public static void UpdateIsRead(int user_id, int weike_id, Boolean isread)
        {
            string sql = "update favorite set isread = @isread where user_id = @user_id and weike_id = @weike_id";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@user_id", user_id);
            cmd.Parameters.AddWithValue("@weike_id", weike_id);
            cmd.Parameters.AddWithValue("@isread", isread);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        */
    }
}