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
	public class Page
	{
		[Key]
		public int Id { get; set; } // ID của trang truyện
		public string? ImageUrl { get; set; } // Hình ảnh của trang truyện

		public int ? PageNumber { get; set; }
		//add FK
		public int? ComicId { get; set; }	
		[ValidateNever]
		public Comic? Comic { get; set; } // Truyện tranh chứa trang truyện này
	}
}
