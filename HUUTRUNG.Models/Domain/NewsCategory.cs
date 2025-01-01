using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HUUTRUNG.Models.Domain
{
    public class NewsCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string? Description { get; set; }


    }
}
