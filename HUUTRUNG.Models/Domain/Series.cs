using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HUUTRUNG.Models.Domain
{
    public class Series
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Series Name")]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
