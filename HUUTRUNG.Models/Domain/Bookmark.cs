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
    public class Bookmark
    {
        [Key]
        public int Id { get; set; }

        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }

        public bool IsSave { get; set; }
        public bool IsRead { get; set; }
        public bool IsCurrentlyReading { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public int? ComicId { get; set; }
        [ForeignKey("ComicId")]
        public Comic? Comic { get; set; }

        public DateTime? SavedAt { get; set; }
       
    }
}
