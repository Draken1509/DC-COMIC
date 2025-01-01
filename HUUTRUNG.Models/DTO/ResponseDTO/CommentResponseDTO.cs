using HUUTRUNG.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace HUUTRUNG.Models.DTO.ResponseDTO
{
    public class CommentResponseDTO
    {
        public string Content { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]

        public DateTime? CreateAt { get; set; }       

        public int? ParentCommentId { get; set; } = null;

        public string? ApplicationUserId { get; set; }

        public int? CommentId { get; set; } = null;

        public string? ReplyUserName { get; set; } = null;

        public string? Email { get; set; } = null;

    }
}