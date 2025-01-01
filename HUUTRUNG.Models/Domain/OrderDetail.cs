using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUUTRUNG.Models.Domain
{
    public class OrderDetail
    {
        public int Id { get; set; }
        [Required]
        public int OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }


        [Required]
        public int ComicId { get; set; }
        [ForeignKey("ComicId")]
        [ValidateNever]
        public Comic Comic { get; set; }

        public int Count { get; set; }
        public double Price { get; set; }
    }
}
