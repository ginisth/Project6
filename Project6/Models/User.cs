using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project6.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(3,ErrorMessage ="The Username field should have at least 3 characters")]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(3, ErrorMessage = "The Password field should have at least 3 characters")]
        public string Password { get; set; }

        [Display(Name ="Role")]
        [Range(1,5,ErrorMessage ="The Role field is required.")]
        public Role UserRole { get; set; }

        public bool UserRegistration { get; set; }

        public ICollection<Document> Documents { get; set; }


        public enum Role
        {
            [Display(Name ="Analyst")]
            Analyst = 1,
            [Display(Name ="Architect")]
            Architect = 2,
            [Display(Name ="Programmer")]
            Programmer = 3,
            [Display(Name ="Tester")]
            Tester = 4,
            [Display(Name ="Manager")]
            Manager = 5
        }


    }
}