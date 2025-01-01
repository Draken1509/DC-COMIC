using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUUTRUNG.Models.Domain
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public int ComicId { get; set; }
        [ForeignKey("ComicId")]
        [ValidateNever]
        public Comic Comic { get; set; }

        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 100")]
        public int Count { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        [NotMapped]
        public double Price { get; set; }

    }
}
