using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.DataHandler.Serializer;

namespace ProblemsBlog.Models
{
    public class MessageToAdmin
    {
        public int MessageToAdminId { get; set; }
        public int UserId { get; set; }

        [Display(Name = "Name")]

        public string UserName { get; set; }



        [DataType(DataType.EmailAddress, ErrorMessage = "Provide a valid Email ")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]

        public string Email { get; set; }




        [Required]
        [Display(Name = "Subject")]
        public string MessageSubject { get; set; }


        [Required]
        [AllowHtml]
        [Display(Name = "Message")]
        public string UserMessage { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }

    }
}