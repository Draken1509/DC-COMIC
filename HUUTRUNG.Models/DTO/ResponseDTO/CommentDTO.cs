using HUUTRUNG.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace HUUTRUNG.Models.DTO.ResponseDTO
{
    public class CommentDTO
    {
        public int? CommentId { get; set; }
        [Required]
        public string Content { get; set; }
        public int? LikeCount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreateAt { get; set; }
        public List<CommentDTO>? Replies { get; set; }
        public ApplicationUserDTO? ApplicationUser { get; set; }


        public int? ParentCommentId { get; set; }
        public string? ReplyUserName { get; set; }
    }
}