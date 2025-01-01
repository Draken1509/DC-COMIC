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
	public class Rating
	{
		[Key]
		public int Id { get; set; } // ID của đánh giá

		public int? ComicId { get; set; } // ID của truyện tranh được đánh giá
		public Comic? Comic { get; set; } // Truyện tranh được đánh giá

		public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; } // Người dùng đã đánh giá

		public int? Score { get; set; } // Điểm đánh giá từ 1 đến 5
	}
}
