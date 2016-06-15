using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{
    public class UserDB
    {
        //0 -> 信息不完整 1-> 成功 -1 -> 已存在
        public static int Insert(int id, string email, string name, string password)
        {
            int result = 0;
            if (email.Length != 0 && name.Length != 0 && password.Length != 0)
            {
                if (FindByEmail(email) == null)
                {
                   
                    string sql = "insert into user VALUES (" + id + ",'" + email + "','" + name + "','" + password + ")";
                    MySqlConnection conn = Connection.getMySqlCon();
                    conn.Open();
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = sql;
                    result =  cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    result = -1;
                }
            }
            return result;

        }

        public static void UpdatePassword(string email, string password)
        {
            string sql = "update user set password = @pwd where email = @email";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@pwd", password);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void UpdateName(string email, string name)
        {
            string sql = "update user set name = @name where email = @email";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static User FindByEmail(string email)
        {
            User user = null;
            string sql = "select * from user where email = @email";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@email", email);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                user = new User((int)reader["user_id"], reader.GetString("email"), reader.GetString("name"), reader.GetString("password"));
            }
            reader.Close();
            conn.Close();
            return user;
        }

        public static User FindById(int user_id)
        {
            User user = null;
            string sql = "select * from user where user_id = @user_id";
            MySqlConnection conn = Connection.getMySqlCon();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@user_id", user_id);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                user = new User((int)reader["user_id"], reader.GetString("email"), reader.GetString("name"), reader.GetString("password"));
            }
            reader.Close();
            conn.Close();
            return user;
        }


    }    
}