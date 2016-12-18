using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProblemsBlog.Context;
using ProblemsBlog.Core.DAL;
using ProblemsBlog.Models;

namespace ProblemsBlog.Core.BLL
{
    public class UserManager
    {

        UserGateway gateway = new UserGateway();

        public User GetAllUserInfo(int userId)
        {
            return gateway.GetAllUserInfo(userId);
        }

        public List<UserPost> GetAllPostbyUserID(int userId)
        {
            return gateway.GetAllPostbyUserID(userId);
        }



      

    }
}