using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace HUUTRUNG.Models.Domain
{
    public class ComicCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Type Name")]
        [MaxLength(30)]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")]
        public int DisplayOrder { get; set; }

        public string Description { get; set; }

        
    }
}
