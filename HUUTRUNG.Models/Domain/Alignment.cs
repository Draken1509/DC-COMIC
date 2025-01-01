using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUUTRUNG.Models.Domain
{
    public class Alignment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Alignment")]
        [MaxLength(30)]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")]
        public int DisplayOrder { get; set; }

        public string? Description { get; set; }

    }
}
