using HUUTRUNG.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HUUTRUNG.Models.DTO.RequestDTO
{
    public class BookmarkRequestDTO
    {
        public bool IsSave { get; set; }
        public bool IsRead { get; set; }
        public bool IsCurrentlyReading { get; set; }
    }
}
