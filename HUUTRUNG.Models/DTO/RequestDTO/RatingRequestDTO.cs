using HUUTRUNG.Models.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUUTRUNG.Models.DTO.RequestDTO
{
    public  class RatingRequestDTO
    {
        
        public int? ComicId { get; set; }       
        public string? ApplicationUserId { get; set; }  
        public int? Score { get; set; } 
    }
}
