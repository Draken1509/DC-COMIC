using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace HUUTRUNG.Models.Domain
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        public string? Title { get; set; }

        public string? UrlVideo { get; set; }

        [DisplayName("Main video")]
        public bool? IsMain { get; set; }


        public string? Thumbnail { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }

        public string? Cast { get; set; }
        public string? Director { get; set; }
        public string? Writer { get; set; }
        public string? Producer { get; set; }
        public string? ExecutiveProducer { get; set; }

        public string? Discription { get; set; }
        [MaxLength(200)]
        public string? DiscriptionShort { get; set; }


        [Display(Name = "MovieCategoryId")]
        public int? MovieCategoryId { get; set; }
        [ForeignKey("MovieCategoryId")]
        [ValidateNever]
        public MovieCategory? MovieCategory { get; set; }


        [ValidateNever]
        public ICollection<Genre> Genres { get; set; }

        [ValidateNever]
        public ICollection<Gallery> Galleries { get; set; }
    }
}
