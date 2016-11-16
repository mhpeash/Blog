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
    }
}