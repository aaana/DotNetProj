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

        static public List<FavoriteData> FindFavoriteWeikeByUserId(int user_id)
        {
            List<FavoriteData> fdList = new List<FavoriteData>();
            string sql = "SELECT weike.weike_id,weike.title,weike.subject,weike.author,weike.src,weike.size,weike.description,weike.star,favorite.date FROM weike.weike inner join favorite where weike.weike_id = favorite.weike_id and user_id = @userid;";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@userid", user_id);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Weike weike = new Weike((int)reader["weike_id"], (string)reader["title"], (string)reader["subject"], (string)reader["author"], (string)reader["src"], (string)reader["size"], (string)reader["description"], (int)reader["star"],(DateTime)reader["postdate"]);
                FavoriteData fd = new FavoriteData( weike, (DateTime)reader["fdate"]);
                fdList.Add(fd);

            }
            reader.Close();
            conn.Close();
            return fdList;
        }
    }
}