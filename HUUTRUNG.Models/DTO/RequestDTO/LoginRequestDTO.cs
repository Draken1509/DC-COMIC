using System.ComponentModel.DataAnnotations;

namespace HUUTRUNG.Models.DTO.RequestDTO
{
    public class LoginRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
