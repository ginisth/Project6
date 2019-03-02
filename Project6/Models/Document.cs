using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project6.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public int UserId { get; set; }
        public DateTime SubmitDate { get; set; }

        [Range(2,5)]
        public int ProgressStatus { get; set; }
        public bool DocumentRegistration { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}