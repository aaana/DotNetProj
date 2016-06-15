using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{
    public class FollowDB
    {
        //关注
        static public void Insert(Follow follow)
        {
            //string sql = "insert into favorite VALUES (" + favorite.user_id + "," + favorite.weike_id + ",'" + favorite.date + "',"+ favorite.isread+ ")";
            string sql = "insert into favorite VALUES (" + follow.user_id + "," + follow.following_id + ",'" + follow.followDate + "')";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        
        //取关
        static public void Delete(int user_id,int following_id)
        {
            string sql = "delete from follow where user_id = @userid and following_id = @followingid";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@userid", user_id);
            cmd.Parameters.AddWithValue("@followingid", following_id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        
        //找所有关注的人
        static public List<FollowData> FindAllFollowings(int user_id)
        {
            List<FollowData> fdList = new List<FollowData>();
            string sql = "SELECT follow.user_id,follow.following_id,follow.followDate,user.name FROM weike.weike inner join user inner join follow where user.user_id = follow.following_id and follow.user_id = @userid;";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@userid", user_id);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Follow follow = new Follow((int)reader["user_id"], (int)reader["following_id"], (DateTime)reader["followDate"]);
                FollowData fd = new FollowData(follow, (string)reader["name"],"following");
                fdList.Add(fd);

            }
            reader.Close();
            conn.Close();
            return fdList;
        }

        //找所有粉丝
        static public List<FollowData> FindAllFollowers(int user_id)
        {
            List<FollowData> fdList = new List<FollowData>();
            string sql = "SELECT follow.user_id,follow.following_id,follow.followDate,user.name FROM weike.weike inner join user inner join follow where user.user_id = follow.user_id and follow.following_id = @userid;";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@userid", user_id);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Follow follow = new Follow((int)reader["user_id"], (int)reader["following_id"], (DateTime)reader["followDate"]);
                FollowData fd = new FollowData(follow, (string)reader["name"],"follower");
                fdList.Add(fd);

            }
            reader.Close();
            conn.Close();
            return fdList;
        }

    }
}