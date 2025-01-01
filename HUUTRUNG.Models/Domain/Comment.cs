using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUUTRUNG.Models.Domain
{
	public class Comment
	{
		public int? CommentId { get; set; } // ID của bình luận

		public string? Content { get; set; } // Nội dung bình luận
			
		public int? LikeCount { get; set; } // Số lượt like của bình luận
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreateAt { get; set; }

        public string? ApplicationUserId { get; set; } // ID người dùng bình luận
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; } // Người dùng bình luận

		public int? ComicId { get; set; } // ID truyện tranh bình luận
        [ForeignKey("ComicId")]
        [ValidateNever]
		public Comic? Comic { get; set; } // Truyện tranh được bình luận
	
		public ICollection<Comment>? Replies { get; set; } 

		public int? ParentCommentId { get; set; } // ID của bình luận cha (nếu có)
		[ValidateNever]
        [ForeignKey("ParentCommentId")]
        public Comment? ParentComment { get; set; } // Bình luận cha

		public string? ReplyUserName { get; set; }



    }
}
