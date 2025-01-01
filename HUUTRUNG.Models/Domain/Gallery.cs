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
    public class Gallery
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Caption { get; set; }
        public string? ImageUrl { get; set; }

        public int? MovieId { get; set; }
        [ForeignKey("MovieId")]
        [ValidateNever]
        public Movie? Movie { get; set; }
    }
}
