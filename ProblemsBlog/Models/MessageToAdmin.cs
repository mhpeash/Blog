using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.DataHandler.Serializer;

namespace ProblemsBlog.Models
{
    public class MessageToAdmin
    {
        public int MessageToAdminId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Subject")]
        public string MessageSubject { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string UserMessage { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }

    }
}