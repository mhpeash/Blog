using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProblemsBlog.Models
{
    public class UserJoinComment
    {
        public int UserJoinCommentId { get; set; }
        public string  CommenterName { get; set; }
        public string Image { get; set; }
        public string Comment { get; set; }
    }
}