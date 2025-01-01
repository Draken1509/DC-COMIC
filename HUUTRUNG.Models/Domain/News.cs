using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUUTRUNG.Models.Domain
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Tittle { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Content { get; set; }
        [ValidateNever]
        public string Thumbnail { get; set; }

        //add foreign key - TypeComic
        [Display(Name = "Type News")]
        public int? NewsCategoryId { get; set; }
        [ForeignKey("NewsCategoryId")]
        [ValidateNever]
        [Display(Name = "Type")]
        public NewsCategory? NewsCategory { get; set; }

    }
}
