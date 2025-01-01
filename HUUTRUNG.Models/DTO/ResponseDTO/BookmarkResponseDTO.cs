using HUUTRUNG.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HUUTRUNG.Models.DTO
{
    public class BookmarkResponseDTO
    {
        public BookmarkResponseDTO()
        {
            
        }
        public BookmarkResponseDTO(bool IsSave, bool IsRead, bool IsCurrentlyReading)
        {
            this.IsSave = IsSave;
            this.IsRead = IsRead;
            this.IsCurrentlyReading = IsCurrentlyReading;
        }
        public bool IsSave { get; set; }
        public bool IsRead { get; set; }
        public bool IsCurrentlyReading { get; set; }
    }
}
