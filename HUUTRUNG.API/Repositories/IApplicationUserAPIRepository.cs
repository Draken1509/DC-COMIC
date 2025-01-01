using HUUTRUNG.Models.Domain;
using HUUTRUNG.Models.DTO;
using HUUTRUNG.Models.DTO.RequestDTO;
using HUUTRUNG.Models.DTO.ResponseDTO;


namespace HUUTRUNG_WEBAPI.Repositories
{
    public interface IApplicationUserAPIRepository
    {
        public Task<Comment> CreateComment(CommentRequestDTO CommentDTO);
		public Task<Rating> RateComic(RatingRequestDTO requestDTO);
	}
}
