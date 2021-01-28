using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ASP_MVC_PROJECT.Models
{
    public class UserWithRoleViewModel
    {
        public string Id { get; set; }
        [Display(Name="Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        [Display(Name="Numer telefonu")]
        [StringLength(10, ErrorMessage = "Numer telefonu musi mieć maksymalnie 10 znaków")]
        [RegularExpression("([+]?[0-9]+)", ErrorMessage = "W numerze telefonu mogą być tylko liczby (numer może zaczynać się od +)")]
        public string phoneNumber { get; set; }
        public bool Activated { get; set; }
        [Display(Name = "Rola")]
        public string Role { get; set; }

    }
}