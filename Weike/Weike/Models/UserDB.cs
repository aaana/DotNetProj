using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{
    public class UserDB
    {
        //0 -> 信息不完整 >=1-> 成功 -1 -> 已存在
        public static int Insert(int id, string email, string name, string password)
        {
            int result = 0;
            if (email.Length != 0 && name.Length != 0 && password.Length != 0)
            {
                if (FindByEmail(email) == null)
                {
                   
                    string sql = "insert into user VALUES (" + id + ",'" + email + "','" + name + "','" + password + "'," + 0 + "," + 0+ "," + 0 + ")";
                    MySqlConnection conn = Connection.getMySqlCon();
                    conn.Open();
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    sql = "select * from user where email = @email";
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@email", email);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        User user = new User((int)reader["user_id"], reader.GetString("email"), reader.GetString("name"), reader.GetString("password"),(int)reader["followNum"],(int)reader["favoriteNum"],(int)reader["postNum"]);
                        result = user.id;
                    }
                    reader.Close();
                    conn.Close();
                }
                else
                {
                    result = -1;
                }
            }
            return result;

        }

        public static bool UpdatePassword(int user_id, string password)
        {
            string sql = "update user set password = @pwd where user_id = @userid";
            try{
                MySqlConnection conn = Connection.getMySqlCon();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@pwd", password);
                cmd.Parameters.AddWithValue("@userid", user_id);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
        }

        public static bool UpdateName(int user_id, string name)
        {
            string sql = "update user set name = @name where user_id = @userid";
            try
            {
                MySqlConnection conn = Connection.getMySqlCon();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@userid", user_id);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
           
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
                user = new User((int)reader["user_id"], reader.GetString("email"), reader.GetString("name"), reader.GetString("password"), (int)reader["followNum"], (int)reader["favoriteNum"], (int)reader["postNum"]);
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
                user = new User((int)reader["user_id"], reader.GetString("email"), reader.GetString("name"), reader.GetString("password"), (int)reader["followNum"], (int)reader["favoriteNum"], (int)reader["postNum"]);
            }
            reader.Close();
            conn.Close();
            return user;
        }


    }    
}