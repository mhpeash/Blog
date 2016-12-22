using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProblemsBlog.Models
{
    public class MessageToUser
    {
        public int MessageToUserId { get; set; }

        [Display(Name="Message Title")]
        public string MessageTitle { get; set; }

        [Display(Name = "Admin Message")]
        public string AdminMessage { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
    }
}