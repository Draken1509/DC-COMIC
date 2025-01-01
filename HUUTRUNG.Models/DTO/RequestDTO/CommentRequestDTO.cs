using HUUTRUNG.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace HUUTRUNG.Models.DTO.ResponseDTO
{
    public class CommentRequestDTO
    {
        public string Content { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
     
        public int? ComicId { get; set; }

        public int? ParentCommentId { get; set; } = null;

        public string? ApplicationUserId { get; set; }


     
    }
}