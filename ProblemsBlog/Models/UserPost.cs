﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProblemsBlog.Models
{
    public class UserPost
    {
        public int UserPostId { get; set; }
        public int UserId { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = " Write something to Post", MinimumLength = 1)]
        [Display(Name = "Post Content")]
        public string PostContent { get; set; }

        [Display(Name = "Post Title")]
        public string PostTitle { get; set; }

        public string Author { get; set; }

        private string _image = "Images/logo.JPG";
        public string Image { get { return _image; } set { _image = value; } }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }

        private int _tempvalue = 1;
        public int Tempvalue { get { return _tempvalue; } set { _tempvalue = value; } }
    }
}