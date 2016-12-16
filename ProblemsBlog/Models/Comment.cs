using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProblemsBlog.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Your Comment")]
        public string UserComment { get; set; }
    }
}