using HUUTRUNG.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HUUTRUNG.Models.DTO.ResponseDTO
{
    public class BookmarkDTO
    {

        public int Id { get; set; }

        public bool IsSave { get; set; }
        public bool IsRead { get; set; }
        public bool IsCurrentlyReading { get; set; }

        public string? ApplicationUserId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public int? ComicId { get; set; }
        public DateTime? SavedAt { get; set; }
    }
}
