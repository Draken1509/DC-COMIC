using HUUTRUNG.Models.Domain;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HUUTRUNG.Models.DTO.ResponseDTO
{
    public class ComicDTO
    {

        public int Id { get; set; }


        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Thumbnail { get; set; }

        public string? Writer { get; set; }

        public string? ArtBy { get; set; }

        public DateTime? OnSaleDate { get; set; }

        public int ComicCategoryId { get; set; }
        public int SeriesId { get; set; }

        public Series Series { get; set; }
        public ComicCategory ComicCategory { get; set; }
        public List<ComicDTO> RelatedComics { get; set; }

        public List<PageDTO>? Pages { get; set; } // Các trang của truyện tranh
        public List<CommentDTO>? Comments { get; set; } // Các bình luận của truyện tranh
        public List<RatingDTO>? Ratings { get; set; } // Các đánh giá của truyện tranh
        public double? AverageRating { get; set; }



    }
}
