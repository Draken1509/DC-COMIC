using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using HUUTRUNG.Models.Domain;
using HUUTRUNG.Models.DTO.ResponseDTO;

namespace HUUTRUNG.Models.DTO.RequestDTO
{
    public class UpdateComicRequestDTO
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

        public SeriesDTO Series { get; set; }
        public ComicCategory ComicCategory { get; set; }

    }
}
