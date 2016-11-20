using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ProblemsBlog.Models;

namespace ProblemsBlog.Core.DAL
{
    public class UserGateway
    {

        private string connectionString = @"Data Source=DESKTOP-1FDNDJJ\SQLEXPRESS;Initial Catalog=ProblemBlog;Integrated Security=true";

        public User GetAllUserInfo(int userId)
        {

            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM Users WHERE UserId=" + userId;

            SqlCommand command = new SqlCommand(query, connection);

            User userInfo = null;
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                userInfo = new User();
                userInfo.UserId = Convert.ToInt32(reader["UserId"].ToString());
                userInfo.Name = reader["Name"].ToString();
                userInfo.Image = reader["Image"].ToString();
                userInfo.Email = reader["Email"].ToString();
                userInfo.Location = reader["Location"].ToString();
                userInfo.UserName = reader["UserName"].ToString();


            }
            connection.Close();

            return userInfo;

        }

        public List<UserPost> GetAllPostbyUserID(int userId)
        {

            // write insert command

            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM UserPosts WHERE UserId=" + userId + " ORDER BY Time DESC ";

            SqlCommand command = new SqlCommand(query, connection);

            List<UserPost> postList = new List<UserPost>();

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                UserPost aPost = new UserPost();

                // aPost=new UserPost();

                aPost.UserId = Convert.ToInt32(reader["UserId"].ToString());
                aPost.PostContent = reader["PostContent"].ToString();
                aPost.Image = reader["Image"].ToString();
                aPost.Time = Convert.ToDateTime(reader["Time"].ToString());
                aPost.Author = reader["Author"].ToString();
                aPost.PostTitle = reader["PostTitle"].ToString();

                postList.Add(aPost);

            }
            connection.Close();

            return postList;

        }


       


    }
}