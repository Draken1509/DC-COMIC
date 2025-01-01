using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Xml.Linq;

namespace HUUTRUNG.Models.Domain
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }


        public string? City { get; set; }

        [Display(Name = "State/Province")]
        public string? Province { get; set; }

        [Display(Name = "Street Address")]
        public string? StreetAddress { get; set; }

        public string? Postal { get; set; }

        public string? PhoneNumber { get; set; }

    }
}
