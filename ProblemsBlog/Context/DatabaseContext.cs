using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ProblemsBlog.Models;

namespace ProblemsBlog.Context
{
    public class DatabaseContext:DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserPost> Post { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<AdminControl> TblAdminControls { get; set; }

        public DbSet<MessageToAdmin> TblFromUser { get; set; }
        public DbSet<MessageToUser> TblToUser { get; set; }
    }
}