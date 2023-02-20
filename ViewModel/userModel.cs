using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;

namespace Artistesta.ViewModel
{
    public class userModel
    {
        public int USERID { get; set; }

        [Required(ErrorMessage = "Field Cannot Be Empty")]
        [Display(Name = "Enter Username")]
        public string USERNAME { get; set; }

        [Required(ErrorMessage = "Field Cannot Be Empty")]
        [Display(Name = "Enter First Name")]
        public string FIRST_NAME { get; set; }

        [Required(ErrorMessage = "Field Cannot Be Empty")]
        [Display(Name = "Enter Last Name")]
        public string LAST_NAME { get; set; }

        [Required(ErrorMessage = "Field Cannot Be Empty")]
        [Display(Name = "Enter Email")]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "Field Cannot Be Empty")]
        [Display(Name = "Enter Birthday")]
        public string BIRTHDAY { get; set; }

        [Required(ErrorMessage = "Field Cannot Be Empty")]
        [Display(Name = "Enter Your Pronouns")]
        public string PRONOUNS { get; set; }

        [Required(ErrorMessage = "Field Cannot Be Empty")]
        [Display(Name = "Enter Country")]
        public string COUNTRY { get; set; }


        [Required(ErrorMessage = "Field Cannot Be Empty")]
        [Display(Name = "Enter Password")]
        [DataType(DataType.Password)]
        public string PASSWORD { get; set; }

        [Required(ErrorMessage = "Field Cannot Be Empty")]
        [Display(Name = "Re-Enter Password")]
        [DataType(DataType.Password)]
        [NotMapped]
        [Compare("PASSWORD", ErrorMessage = "Passwords Do Not Match!")]
        public string ConfirmPASSWORD { get; set; }

        public IFormFile DP { get; set; }
        public IFormFile COVER { get; set; }
        public string MOTTO { get; set; }
        public string CARD_NO { get; set; }
        public string CREATION_TIME { get => DateTime.Now.ToString(); set => DateTime.Now.ToString(); }
    }
}